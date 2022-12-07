using Newtonsoft.Json;
using RestSharp;
using SmartInsuarance.Helper;
using SmartInsuarance.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace SmartInsuarance.Controllers
{
    public class DashboardController : Controller
    {
        Api_CommonResponse _CommonResponse = new Api_CommonResponse();
        public ActionResult Index()
        {
            return View();
        }
    }
}