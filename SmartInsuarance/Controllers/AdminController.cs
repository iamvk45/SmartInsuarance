using Newtonsoft.Json;
using RestSharp;
using SmartInsuarance.Helper;
using SmartInsuarance.Model;
using SmartInsuarance.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

namespace SmartInsuarance.Controllers
{
    public class AdminController : Controller
    {
        public ActionResult ApproveTrailuser()
        {
            ViewBag.TrailUsers = GetTrailuser();
            return View();
        }

        public List<TrailuserModal> GetTrailuser()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetTrailUsers");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse objResponse = new Api_CommonResponse();
            List<TrailuserModal> trailuserModals = new List<TrailuserModal>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                    trailuserModals = JsonConvert.DeserializeObject<List<TrailuserModal>>(objResponse.data.ToString());

            }

            return trailuserModals;
        }
        public JsonResult ActiveTrialUser(TrailuserModal trailuser)
        {
            var json = JsonConvert.SerializeObject(trailuser);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/ActiveTrialUser");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse objResponse = new Api_CommonResponse();

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
            }

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = objResponse, Failure = false, Message = objResponse.message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}