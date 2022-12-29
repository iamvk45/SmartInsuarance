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
    public class RoleController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        CommonController _common = new CommonController();
        // GET: Role
        public ActionResult MenuMaster(MenuMasters master)
        {
            MenuMasters obj = new MenuMasters();
            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            if (Request.HttpMethod == "POST")
            {



                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/InsertMenuMaster");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(master), ParameterType.RequestBody);
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

                        return RedirectToAction("MenuIndex", "Role");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("MenuIndex", "Role");
                    }
                }
            }
            return View();
        }
        public ActionResult MenuIndex()
        {


            List<MenuMasters> menus = new List<MenuMasters>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetMenuMasterList");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse CommonResponse = new Api_CommonResponse();
                CommonResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (CommonResponse.data != null)
                {
                    menus = JsonConvert.DeserializeObject<List<MenuMasters>>(CommonResponse.data.ToString());
                }
            }




            return View(menus);
        }

        public ActionResult SubMenuIndex()
        {

            List<SubMenuMaster> menus = new List<SubMenuMaster>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetSubMenuMasterList");
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
                    menus = JsonConvert.DeserializeObject<List<SubMenuMaster>>(CommonResponse.data.ToString());
                }

            }


            return View(menus);
        }

        #region Sub Menu Master
        public ActionResult SubMenuCreate(SubMenuMaster master)
        {
            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            SubMenuMaster obj = new SubMenuMaster();
            if (Request.HttpMethod == "POST")
            {
                var json = JsonConvert.SerializeObject(master);

                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/InsertSubMenuMaster");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", json, ParameterType.RequestBody);

                //request.AddHeader("Content-Type", "application/json");
                //request.AddHeader("Accept", "application/json");

                IRestResponse response = client.Execute(request);
                Api_CommonResponse CommonResponse = new Api_CommonResponse();
                if (response.StatusCode.ToString() == "OK")
                {
                    Api_CommonResponse objResponseData = _JsonSerializer.Deserialize<Api_CommonResponse>(response.Content);
                    if (objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("SubMenuIndex", "Role");
                    }
                    else if (objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("SubMenuIndex", "Role");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("SubMenuIndex", "Role");
                    }
                }
            }
            else
            {
                if (Id != 0)
                {

                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetSubMenuMaster?Id=" + Id);
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
                            obj = JsonConvert.DeserializeObject<SubMenuMaster>(CommonResponse.data.ToString());
                        }

                    }
                }
                List<ShowMenuDropDown> Menulist = new List<ShowMenuDropDown>();
                Menulist = _common.GetMenulist();
                ViewBag.Menulist = Menulist;
            }
            return View(obj);
        }
        #endregion

        #region RateMaster
        public ActionResult RateMaster()
        {
            List<ShowMenuDropDown> Menulist = new List<ShowMenuDropDown>();
            Menulist = _common.GetMenulist();
            ViewBag.Menulist = Menulist;

            return View();
        }
        public JsonResult GetSelectSubmenulist(int MenuId)
        {
            List<Dropdown> lst = new List<Dropdown>();
            lst = GetSelectMenulist("SubMenulist", MenuId);
            return new JsonResult
            {
                Data = new { Data = lst, failure = false, msg = "Success", isvalid = 1 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public List<Dropdown> GetSelectMenulist(string Type, int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetMenulist?MenuId=" + MenuId);
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
                if(objResponse.data!= null)
                    groups = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.data.ToString());
            }
            return groups;
        }
        public ActionResult RateMasterCreate(RateMaster Master)
            {
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/InsertRateMaster");
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
            return RedirectToAction("RateMaster", "Role");
        }
        #endregion
    }
}