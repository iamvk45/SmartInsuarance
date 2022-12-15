using Newtonsoft.Json;
using RestSharp;
using SmartInsuarance.Helper;
using SmartInsuarance.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Deployment.Internal;
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
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetCoverLicenseTemp?IFK_LiceId=" + Id+ "&iTypid="+ iTypid);
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

        public ActionResult DetailsOfLicense(string Id)
        {
            LICEMSTViEW seldata = new LICEMSTViEW();
            seldata = CommonFunction.GetSelectLicence(Id);
            List<Dropdown> groups = new List<Dropdown>();
            groups = CommonFunction.GetLicenceCoverList(Id);
            ViewBag.group = groups;
            return View(seldata);
        }

        public ActionResult BindFuncPackage(string iFk_PackMstId)
        {
            List<PackFunctTempview> menus = new List<PackFunctTempview>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"]+ "Master/Package_Functionality_Show?iFk_PackMstId=" + iFk_PackMstId + "&Type=0");
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
                    menus = JsonConvert.DeserializeObject<List<PackFunctTempview>>(CommonResponse.data.ToString());
                }

            }
            return View(menus);
        }

        public ActionResult BindPackageFunction(int Id,string Licenseid)
        {
            List<Dropdown> groups = new List<Dropdown>();
            groups = CommonFunction.GetLicenceCoverList(Licenseid);
            ViewBag.groups = groups;
            List<PACKFUNCView> menus = new List<PACKFUNCView>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetPackageFunction?Id=" + Id);
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
                    menus = JsonConvert.DeserializeObject<List<PACKFUNCView>>(CommonResponse.data.ToString());
                }

            }
            return View(menus);
        }
        public ActionResult BindPackageFeature(int Id)
        {
            
            List<PACKFEATUESPEView> menus = new List<PACKFEATUESPEView>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetPackageFeature?Id=" + Id);
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
                    menus = JsonConvert.DeserializeObject<List<PACKFEATUESPEView>>(CommonResponse.data.ToString());
                }

            }
            return View(menus);
        }
    }
}