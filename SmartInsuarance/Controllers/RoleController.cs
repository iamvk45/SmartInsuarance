using Newtonsoft.Json;
using RestSharp;
using SmartInsuarance.Helper;
using SmartInsuarance.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
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

        #region Department Master
        [HttpGet]
        public ActionResult AddDepartment()
        {

                var departmentList = CommonFunction.GetDepartments();

                ViewBag.departmentList = departmentList;

                return View();
          
        }
        public JsonResult EditDepartment(int DepartmentID, string Type = "DepartmentSingle")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetData?Type=" + Type + "&MenuId=" + DepartmentID);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                var Department = JsonConvert.DeserializeObject<List<AddDepartment>>(d.data.ToString());

                return new JsonResult
                {
                    Data = new { StatusCode = d.statusCode, Data = Department, Failure = false, Message = d.message},
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }

            return new JsonResult
            {
                Data = new { StatusCode = -1, Data = "", Failure = false, Message = "Data Not Available" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public JsonResult AddDepartment(AddDepartment department)
        {

            //var UserDetails = (UserModelSession)Session["UserDetails"];
            //if (UserDetails != null)
            //{

                //department.PartyId = UserDetails.PartyId;
                //department.CreatedBy = UserDetails.PartyId;

                var json = JsonConvert.SerializeObject(department);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/AddDepartment");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var d = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);

                    return new JsonResult
                    {
                        Data = new { StatusCode = d.statusCode, Data = "", Failure = false, Message = d.message },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
            //}

            return new JsonResult
            {
                Data = new { StatusCode = -1, Data = "", Failure = false, Message = "Data Not Available" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

        #region chnage status
        public JsonResult ChangeStatus(string TableId, int type, int Id,string Display=null)
        {
            Activeclass objRatemaster = new Activeclass();
            objRatemaster.Id = Id;
            objRatemaster.Tablename = TableId;
            objRatemaster.status = type;
            objRatemaster.Display= Display;

            var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/ChangeStatus");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            //request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", _JsonSerializer.Serialize(objRatemaster), ParameterType.RequestBody);
            IRestResponse response = client2.Execute(request2);
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponseData = _JsonSerializer.Deserialize<Api_CommonResponse>(response.Content);
                if (objResponseData.statusCode ==1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.statusCode == 0)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }

            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        #endregion
        #region rolemastername
        public ActionResult RoleMasterIndex()
        {
           

            List<RoleMastertable> Roles = new List<RoleMastertable>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetRoleMasterInformation");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (d.data != null)
                {
                    Roles = JsonConvert.DeserializeObject<List<RoleMastertable>>(d.data.ToString());
                }

            }

           


            return View(Roles);
        }
        public ActionResult RoleMasterCreate(RoleMastertable Master)
        {
            //UserModelSession servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            //Master.PartyId = servicesCollectiondata.PartyId;

            int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            RoleMastertable obj = new RoleMastertable();
            if (Request.HttpMethod == "POST")
            {
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/InsertRoleMasterCreate");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    Api_CommonResponse objResponseData = _JsonSerializer.Deserialize<Api_CommonResponse>(response.Content);
                   if (objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("RoleMasterIndex", "Role");
                    }
                    else if ( objResponseData.statusCode == 0)
                    {
                        TempData["SwalStatusMsg"] = "warning";
                        TempData["SwalMessage"] = objResponseData.message;
                        TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("RoleMasterIndex", "Role");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("RoleMasterIndex", "Role");
                    }
                }
            }
            else
            {
                if (Id > 0)
                {
                    var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetRoleMasterInformation?Id=" + Id);
                    var request = new RestRequest(Method.GET);
                    request.AddHeader("cache-control", "no-cache");
                    //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                    request.AddParameter("application/json", "", ParameterType.RequestBody);
                    IRestResponse response = client.Execute(request);
                    if (response.StatusCode.ToString() == "OK")
                    {
                        var d = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                        if (d.data != null)
                        {
                            obj = JsonConvert.DeserializeObject<List<RoleMastertable>>(d.data.ToString()).FirstOrDefault();

                        }

                    }
                }

            }
            return View(obj);
        }
        #endregion
        #region Group
        [HttpGet]
        public ActionResult AddGroup()
        {

       
                var groups = GetGroups();
                var Menus = CommonFunction.GetMenusList();

                ViewBag.groups = groups;
                ViewBag.Menus = Menus;

                return View();
           
        }
        public List<AddGroup> GetGroups(string Type = "GroupList")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetData?Type=" + Type );
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AddGroup> groups = new List<AddGroup>();
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {
                    groups = JsonConvert.DeserializeObject<List<AddGroup>>(objResponse.data.ToString());
                }
            }
            return groups;
        }
        public JsonResult GetSubmenus(string Type = "Submenus", int MenuId = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetData?Type=" + Type + "&MenuId=" + MenuId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> MenusList = new List<Dropdown>();
            Api_CommonResponse objResponse = null;
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {
                    MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.data.ToString());
                }
            }
            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = MenusList, Failure = false, Message = objResponse.message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        [HttpPost]
        public JsonResult AddGroup(AddGroup group)
        {

                var json = JsonConvert.SerializeObject(group);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/AddGroup");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    var d = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);

                    return new JsonResult
                    {
                        Data = new { StatusCode = d.statusCode, Data = "", Failure = false, Message = d.message },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
        

            return new JsonResult
            {
                Data = new { StatusCode = -1, Data = "", Failure = false, Message = "Data Not Available" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

        #region RoleMappingDepartmentandGroup
        public ActionResult RoleMappingDepartmentandGroupIndex()
        {
            string PartyId = null;
            List<MappingRoleWithDepartmentandGroup> Roles = new List<MappingRoleWithDepartmentandGroup>();

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetRoleMappingDepartmentandGroup?Id=" + PartyId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (d.data != null)
                {
                    Roles = JsonConvert.DeserializeObject<List<MappingRoleWithDepartmentandGroup>>(d.data.ToString());
                }

            }
            return View(Roles);
        }
        public ActionResult RoleMappingDepartmentandGroupCreate(MappingRoleWithDepartmentandGroup Master)
        {
            //int Id = Convert.ToInt32(Request.RequestContext.RouteData.Values["Id"]);
            //MappingRoleWithDepartmentandGroup obj = new MappingRoleWithDepartmentandGroup();
            //UserModelSession servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            //var Type = servicesCollectiondata.Type;
            //var partyId = servicesCollectiondata.PartyId;
            if (Request.HttpMethod == "POST")
            {
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/InsertMappingRoleWithDepartmentandGroup");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");

                request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);
                if (response.StatusCode.ToString() == "OK")
                {
                    Api_CommonResponse objResponseData = _JsonSerializer.Deserialize<Api_CommonResponse>(response.Content);
                  if (objResponseData.statusCode == 1)
                    {
                        TempData["SwalStatusMsg"] = "success";
                        TempData["SwalMessage"] = objResponseData.message;
                        TempData["SwalTitleMsg"] = "Success!";
                        return RedirectToAction("RoleMappingDepartmentandGroupIndex", "Role");
                    }
                    else if (objResponseData.statusCode == 0)
                    {
                        //TempData["SwalStatusMsg"] = "warning";
                        //TempData["SwalMessage"] = objResponseData.Message;
                        //TempData["SwalTitleMsg"] = "warning!";
                        return RedirectToAction("RoleMappingDepartmentandGroupIndex", "Role");
                    }
                    else
                    {
                        TempData["SwalStatusMsg"] = "error";
                        TempData["SwalMessage"] = "Something wrong";
                        TempData["SwalTitleMsg"] = "error!";
                        return RedirectToAction("RoleMappingDepartmentandGroupIndex", "Role");
                    }
                }
            }

            //ViewBag.Type = Type;
            //ViewBag.partyId = partyId;
            List<Dropdown> dept = new List<Dropdown>();
            dept = GetDepartmentGroupMaster("Department");
            List<Dropdown> Group = new List<Dropdown>();
            Group = GetDepartmentGroupMaster("Group");
            List<Dropdown> Role = new List<Dropdown>();
            Role = GetDepartmentGroupMaster("Role");
            ViewBag.dept = dept;
            ViewBag.Group = Group;
            ViewBag.Role = Role;
            return View();
        }

        public JsonResult FillDepartmentandGroupMaster(string Type)
        {
          
            List<Dropdown> data = new List<Dropdown>();
            data = GetDepartmentGroupMaster(Type);
            return new JsonResult
            {
                Data = new { Data = data, failure = true, msg = "Failed" },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public static List<Dropdown> GetDepartmentGroupMaster(string Type, string PartyId=null)
        {

            List<Dropdown> obj = new List<Dropdown>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/FillDepartmentandGroupMaster?Type=" + Type + "&PartyId=" + PartyId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {
                    obj = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.data.ToString());
                }
            }
            return obj;
        }
        #endregion

        #region UserPagingPermission
        public ActionResult Userpermission(int MappingId, int GroupId)
        {
            List<UserPagingPermission> obj = new List<UserPagingPermission>();
            if (obj == null)
            {
                return RedirectToAction("SignOut", "Home");
            }
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetUserPagingPermission?MappingId=" + MappingId + "&GroupId=" + GroupId);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                var d = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (d.data != null)
                {
                    obj = JsonConvert.DeserializeObject<List<UserPagingPermission>>(d.data.ToString());

                }

            }
            ViewBag.mapping = MappingId;
            return View(obj);
        }
        public JsonResult OperationStatus(string Typedata, int MappingId, int PermissionId, int MstGroupId, int status)
        {
            //UserModelSession servicesCollectiondata = (UserModelSession)Session["UserDetails"];
            Permissionclass obj = new Permissionclass();
            obj.Type = Typedata;
            obj.MappingId = MappingId;
            obj.PermissionId = PermissionId;
            obj.MstGroupId = MstGroupId;
            obj.status = status;
           
          


            var client2 = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/OperationStatus");
            var request2 = new RestRequest(Method.POST);
            request2.AddHeader("cache-control", "no-cache");
            //request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request2.AddParameter("application/json", _JsonSerializer.Serialize(obj), ParameterType.RequestBody);
            IRestResponse response = client2.Execute(request2);
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponseData = _JsonSerializer.Deserialize<Api_CommonResponse>(response.Content);
                if (objResponseData.statusCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.statusCode == 0)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };

                }

            }
            return new JsonResult
            {
                Data = new { Data = "", failure = true, msg = "Failed", isvalid = 0 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        #endregion
    }
}