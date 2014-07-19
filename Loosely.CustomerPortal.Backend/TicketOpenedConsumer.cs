using Loosely.Bus.Contracts;
using MassTransit;
using System.Diagnostics;

namespace Loosely.CustomerPortal.Backend
{
  class TicketOpenedConsumer : Consumes<TicketOpened>.Context
  {
    public void Consume(IConsumeContext<TicketOpened> envelope)
    {
      // Here is where you would persist the ticket to a data store of some kind.
      // For this example, we'll just write it to the trace log.
      Trace.WriteLine("=========== NEW TICKET ===========\r\n" +
                      "Id: " + envelope.Message.Id + "\r\n" +
                      "Email: " + envelope.Message.CustomerEmail + "\r\n" + 
                      "Message: " + envelope.Message.Message);

      // Send email confirmation to the customer.
      var messageBody = "Ticket ID " + envelope.Message.Id + " has been opened for you! " +
                        "We will respond to your inquiry ASAP.\n\n" + 
                        "Your Message:\n" + envelope.Message.Message;

      EmailHelper.Send(envelope.Message.CustomerEmail, "Ticket Opened", messageBody);
    }
  }
}
