using System.Web.Mvc;

namespace Loosely.CustomerPortal.WebApp.Controllers
{
  public class TicketController : Controller
  {
    [HttpGet]
    public ActionResult Open()
    {
      var ticket = new Models.Ticket();
      return View(ticket);
    }

    [HttpPost]
    public ActionResult Open(Models.Ticket ticket)
    {
      ticket.Save();
      return Redirect("~/Ticket/Opened/" + ticket.Id);
    }

    public ActionResult Opened(string id)
    {
      ViewBag.TicketId = id;
      return View();
    }
  }
}