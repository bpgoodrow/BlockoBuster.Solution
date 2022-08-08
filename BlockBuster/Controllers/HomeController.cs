using Microsoft.AspNetCore.Mvc;

namespace BlockBuster.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

    }
}