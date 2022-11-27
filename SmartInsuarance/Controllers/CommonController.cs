using Newtonsoft.Json;
using RestSharp;
using SmartInsuarance.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartInsuarance.Controllers
{
    public class CommonController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult GetCountryStateCity(string type, int countryID = 0, int stateID = 0)
        {
            //var json = JsonConvert.SerializeObject(geographical);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Common/GetCountryStateCity?countryID=" + countryID + "&stateID=" + stateID + "&type=" + type);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse _CommonResponse = new Api_CommonResponse();

            if (response.StatusCode.ToString() == "OK")
            {
                _CommonResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);

            }
            return new JsonResult
            {
                Data = new { StatusCode = _CommonResponse.statusCode, Data = _CommonResponse.data, Failure = false, Message = _CommonResponse.message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}