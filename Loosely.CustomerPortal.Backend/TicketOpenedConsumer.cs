using Loosely.Bus.Contracts;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Loosely.CustomerPortal.Backend
{
  class TicketOpenedConsumer : Consumes<TicketOpened>.Context
  {
    static private Dictionary<int, int> DelayProgression = new Dictionary<int, int>()
      {
        {0, 60}, {60, 300}, {300, -1}
      };

    public void Consume(IConsumeContext<TicketOpened> envelope)
    {
      int retryDelay = 0;
      int.TryParse(envelope.Headers["loosely.retry-delay-seconds"], out retryDelay);
      var nextRetryDelay = DelayProgression[retryDelay];
      bool sleepAndRepublish = false;

      try
      {
        // Here is where you would persist the ticket to a data store of some kind.
        // For this example, we'll just write it to the trace log.
        Trace.WriteLine("=========== NEW TICKET ===========\r\n" +
                        "Id: " + envelope.Message.Id + "\r\n" +
                        "Email: " + envelope.Message.CustomerEmail + "\r\n" +
                        "Message: " + envelope.Message.Message + "\r\n" +
                        "Current/Next Retry Delay: " + retryDelay.ToString() + "/" +
                          nextRetryDelay.ToString() + "\r\n" +
                        "Current Time: " + DateTime.Now.ToString());

        CheckForContrivedErrorConditions(envelope);

        // Send email confirmation to the customer.
        var messageBody = "Ticket ID " + envelope.Message.Id + " has been opened for you! " +
                          "We will respond to your inquiry ASAP.\n\n" +
                          "Your Message:\n" + envelope.Message.Message;

        EmailHelper.Send(envelope.Message.CustomerEmail, "Ticket Opened", messageBody);

        // Here is where you would commit any open database transaction
        Trace.WriteLine("Message committed.");
      }
      catch (Exception ex)
      {
        Trace.WriteLine("Exception caught.");
        if (ex.Message.Contains("server is down") && nextRetryDelay > -1)
          sleepAndRepublish = true;
        else throw;
      }

      if (sleepAndRepublish)
      {
        Thread.Sleep(nextRetryDelay * 1000);
        envelope.Bus.Publish<TicketOpened>(envelope.Message, x =>
        {
          x.SetHeader("loosely.retry-delay-seconds", nextRetryDelay.ToString());
          x.SetDeliveryMode(MassTransit.DeliveryMode.Persistent);
        });
      }
    }

    private void CheckForContrivedErrorConditions(IConsumeContext<TicketOpened> envelope)
    {
      if (envelope.Message.Message.Contains("poison"))
        throw (new Exception("Something bad has happened!"));

      if (envelope.Message.Message.Contains("server-blip"))
      {
        envelope.Message.Message = envelope.Message.Message.Replace("server-blip",
          "server-online(blipped)");
        throw (new Exception("The mail server is down."));
      }

      if (envelope.Message.Message.Contains("server-down"))
      {
        envelope.Message.Message = envelope.Message.Message.Replace("server-down",
            "server-blip(downed)");
        throw (new Exception("The mail server is down."));
      }

      if (envelope.Message.Message.Contains("server-disaster"))
        throw (new Exception("The mail server is down."));

    }
  }
}
