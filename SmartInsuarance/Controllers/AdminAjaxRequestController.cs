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

            List<RateMasterview> menus = new List<RateMasterview>();
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
                    menus = JsonConvert.DeserializeObject<List<RateMasterview>>(CommonResponse.data.ToString());
                }

            }
            return View(menus);
        }

        public ActionResult BindCuverLiceTempTable(string Id,int iTypid)
        
        {
            List<CoverLiceTempView> menus = new List<CoverLiceTempView>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetCoverLicenseTemp?IFK_LiceId="+Id+ "&iTypid="+ iTypid);
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
                    menus = JsonConvert.DeserializeObject<List<CoverLiceTempView>>(CommonResponse.data.ToString());
                }

            }
            return View(menus);
        }
    }
}