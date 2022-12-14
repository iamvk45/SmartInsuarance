using SmartInsuarance.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace SmartInsuarance.Controllers
{
    public class WelcomeController : Controller
    {
        public ActionResult Index()
        {
            var licences = (List<License>)Session["LicenseDetails"];
            var insuarances = (List<Insuarance>)Session["InsuaranceDetails"];
            if(licences.Count>0)
            {
            ViewBag.licences = licences[0];
            ViewBag.insuarances = insuarances;
            }
            else
            {
                ViewBag.licences = null;
                ViewBag.insuarances = null;
            }
            return View();
        }
    }
}