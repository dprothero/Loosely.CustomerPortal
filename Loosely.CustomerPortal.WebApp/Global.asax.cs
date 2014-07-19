using Loosely.Bus.Configuration;
using MassTransit;
using System.Web.Mvc;
using System.Web.Routing;

namespace Loosely.CustomerPortal.WebApp
{
  public class MvcApplication : System.Web.HttpApplication
  {
    public static IServiceBus Bus {get; set;}

    protected void Application_Start()
    {
      AreaRegistration.RegisterAllAreas();
      RouteConfig.RegisterRoutes(RouteTable.Routes);

      Bus = BusInitializer.CreateBus("CustomerPortal_WebApp", x => { });
    }

    protected void Application_End()
    {
      Bus.Dispose();
    }
  }
}
