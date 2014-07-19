using System.Diagnostics;
using Topshelf;

namespace Loosely.CustomerPortal.Backend
{
  class Program
  {
    static void Main(string[] args)
    {
      HostFactory.Run(x =>
      {
        x.Service<TicketService>(s =>
        {
          s.ConstructUsing(name => new TicketService());
          s.WhenStarted(ts => ts.Start());
          s.WhenStopped(ts => ts.Stop());
        });
        x.RunAsLocalSystem();

        x.SetDescription("Loosely Coupled Labs Customer Portal Backend");
        x.SetDisplayName("Loosely.CustomerPortal.Backend");
        x.SetServiceName("Loosely.CustomerPortal.Backend");
      });
    }
  }
}
