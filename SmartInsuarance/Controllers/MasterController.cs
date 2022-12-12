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
using System.Web.Script.Serialization;

namespace SmartInsuarance.Controllers
{
    public class MasterController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonController _common = new CommonController();
        // GET: Master
        public ActionResult FinacialYear()
        {
            ViewBag.FinYear = GetFinacialyearlist();
            return View();
        }
        public ActionResult CreateFinacialyear(FinYear Master)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/InsertFinYear");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse CommonResponse = new Api_CommonResponse();
                CommonResponse = _JsonSerializer.Deserialize<Api_CommonResponse>(response.Content);
                if (CommonResponse.statusCode == 1)
                {
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalTitleMsg"] = "Success!";
                    if (CommonResponse.message == "Allready Exists In table")
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalTitleMsg"] = "warning!";
                    }
                    TempData["SwalMessage"] = CommonResponse.message;


                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";

                }
            }
            return RedirectToAction("FinacialYear", "Master");
        }

        public List<FinYearView> GetFinacialyearlist()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetFinYearList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<FinYearView> groups = new List<FinYearView>();
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {
                    groups = JsonConvert.DeserializeObject<List<FinYearView>>(objResponse.data.ToString());
                }
            }
            return groups;
        }

        public ActionResult FeatureRateMaster()
        {
            ViewBag.FinYear = GetFeatureRatelist();
            return View();
        }
        public ActionResult CreateFeatureData(FEATRATMST Master)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/InsertFeatRateMst");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse CommonResponse = new Api_CommonResponse();
                CommonResponse = _JsonSerializer.Deserialize<Api_CommonResponse>(response.Content);
                if (CommonResponse.statusCode == 1)
                {
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalTitleMsg"] = "Success!";
                    if (CommonResponse.message == "Allready Exists In table")
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalTitleMsg"] = "warning!";
                    }
                    TempData["SwalMessage"] = CommonResponse.message;


                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";

                }
            }
            return RedirectToAction("FeatureRateMaster", "Master");
        }

        public List<FEATRATMSTView> GetFeatureRatelist()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetFeatRateList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<FEATRATMSTView> groups = new List<FEATRATMSTView>();
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {
                    groups = JsonConvert.DeserializeObject<List<FEATRATMSTView>>(objResponse.data.ToString());
                }
            }
            return groups;
        }

        #region Licence Master
       // This page use to store information Licence
        public ActionResult LicenceMaster(string Id)
        {
            LICEMSTViEW seldata = new LICEMSTViEW();
            seldata = GetSelectLicence(Id);
            ViewBag.id = Id;
           

            return View(seldata);
        }
        public ActionResult LicenceIndex()
        {
            List<LICEMSTViEW> lstMst = new List<LICEMSTViEW>();
            lstMst = GetLicenceSublist();
            return View(lstMst);
        }
        public JsonResult GetSelectMenuSubmenulist(int Type, int MenuId,string LicMstId)
        {
            List<Dropdown> lst = new List<Dropdown>();
            lst = GetMenuSublist(Type, MenuId, LicMstId);
            return new JsonResult
            {
                Data = new { Data = lst, failure = false, msg = "Success", isvalid = 1 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public List<Dropdown> GetMenuSublist(int Type, int MenuId = 0,string LicMstId=null)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetFillMenuandSubMenulist?MenuId=" + MenuId + "&Type="+ Type + "&LicMstId="+ LicMstId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> groups = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {
                    groups = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.data.ToString());
                }
            }
            return groups;
        }
        public ActionResult CreateLicenceMaster(LICEMST Master)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/InsertLICEMST");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse CommonResponse = new Api_CommonResponse();
                CommonResponse = _JsonSerializer.Deserialize<Api_CommonResponse>(response.Content);
                if (CommonResponse.statusCode == 1)
                {
                    TempData["SwalStatusMsg"] = "success";
                    TempData["SwalTitleMsg"] = "Success!";
                    if (CommonResponse.message == "Allready Exists In table")
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalTitleMsg"] = "warning!";
                    }
                    TempData["SwalMessage"] = CommonResponse.message;


                }
                else
                {
                    TempData["SwalStatusMsg"] = "error";
                    TempData["SwalMessage"] = "Something wrong";
                    TempData["SwalTitleMsg"] = "error!";

                }
            }
            return RedirectToAction("LicenceMaster", "Master");
        }

        public List<LICEMSTViEW> GetLicenceSublist()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetLicenceSublist");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<LICEMSTViEW> groups = new List<LICEMSTViEW>();
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                groups = JsonConvert.DeserializeObject<List<LICEMSTViEW>>(objResponse.data.ToString());
            }
            return groups;
        }

        public LICEMSTViEW GetSelectLicence(string Id)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetSelectLicence?Id="+ Id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            LICEMSTViEW groups = new LICEMSTViEW();
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {
                    groups = JsonConvert.DeserializeObject<List<LICEMSTViEW>>(objResponse.data.ToString()).FirstOrDefault();
                }
            }
            return groups;
        }

        #endregion

    }
}