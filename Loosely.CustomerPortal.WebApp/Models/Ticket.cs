using Loosely.Bus.Contracts;
using System;

namespace Loosely.CustomerPortal.WebApp.Models
{
  public class Ticket : TicketOpened
  {
    private string _id;
    
    public string Id { get { return _id; } }
    public string CustomerEmail { get; set; }
    public string Message { get; set; }

    public Ticket()
    {
      _id = Guid.NewGuid().ToString();
    }

    public void Save()
    {
      MvcApplication.Bus.Publish<TicketOpened>(this, x => { 
        x.SetDeliveryMode(MassTransit.DeliveryMode.Persistent);
      });
    }
  }
}