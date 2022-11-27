using Newtonsoft.Json;
using RestSharp;
using SmartInsuarance.Helper;
using SmartInsuarance.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartInsuarance.Controllers
{
    public class UserController : Controller
    {
        public ActionResult EditProfile()
        {
            CommonController _common = new CommonController();
            _common.GetCountryStateCity("Country", 0, 0);

            return View();
        }
    }
}