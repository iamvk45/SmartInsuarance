using System.Web.Mvc;

namespace SmartInsuarance.Controllers
{
    public class WelcomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}