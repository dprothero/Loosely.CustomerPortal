using Loosely.Bus.Configuration;
using MassTransit;

namespace Loosely.CustomerPortal.Backend
{
  class TicketService
  {
    IServiceBus _bus;

    public TicketService()  {  }

    public void Start()
    {
      _bus = BusInitializer.CreateBus("CustomerPortal_Backend", x =>
      {
        x.Subscribe(subs =>
        {
          subs.Consumer<TicketOpenedConsumer>().Permanent();
        });
      });
    }

    public void Stop()
    {
      _bus.Dispose();
    }
  }
}
