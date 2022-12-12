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

namespace SmartInsuarance.Controllers
{
    public class AdminAjaxRequestController : Controller
    {
        // GET: AdminAjaxRequest
        public ActionResult BindSubmenu()
        {

            List<RateMaster> menus = new List<RateMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetRateMaterList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            Api_CommonResponse CommonResponse = new Api_CommonResponse();
            if (response.StatusCode.ToString() == "OK")
            {
                CommonResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (CommonResponse.data != null)
                {
                    menus = JsonConvert.DeserializeObject<List<RateMaster>>(CommonResponse.data.ToString());
                }

            }
            return View(menus);
        }
    }
}