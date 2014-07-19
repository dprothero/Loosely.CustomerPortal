using System.Web.Mvc;

namespace Loosely.CustomerPortal.WebApp.Controllers
{
  public class HomeController : Controller
  {
    // GET: Home
    public ActionResult Index()
    {
      return View();
    }
  }
}