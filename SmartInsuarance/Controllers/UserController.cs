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

namespace SmartInsuarance.Controllers
{
    public class UserController : Controller
    {
        public ActionResult EditProfile()
        {
            CommonController common = new CommonController();
            var userSession = (UserModelSession)Session["UserDetails"];

            var currentUser = GetuserDetails(userSession.sUSRCode);
            var roleList = common.GetDataForDropdown("Role", 0);

            ViewBag.roles = roleList;
            ViewBag.currentUser = currentUser;

            return View();
        }
        public ActionResult Profilestatus()
        {
            CommonController common = new CommonController();
            var userSession = (UserModelSession)Session["UserDetails"];
            var childData = common.GetChildID(userSession.sUSRCode);

            ViewBag.childData = childData;

            return View();
        }

        public ActionResult AddNewUser(string userCode = "")
        {
            CommonController common = new CommonController();

            var departmentList = common.GetDataForDropdown("Department", 0);
            var roleList = common.GetDataForDropdown("Role", 0);

            if (userCode == "")
            {
                ViewBag.departments = departmentList;
                ViewBag.roles = roleList;
                ViewBag.userCod = userCode;
                return View();
            }
            else
            {
                var UserMasterDetails = GetuserDetails(userCode);
                ViewBag.departments = departmentList;
                ViewBag.roles = roleList;
                ViewBag.userCod = userCode;
                return View(UserMasterDetails);
            }


        }
        public ActionResult AddNewUserIndex()
        {
            CommonController common = new CommonController();

            var userList = common.GetUsersListToActiveInactive("CompanyUser", 0);

            ViewBag.users = userList;

            return View();
        }
        public ActionResult AddUserMember(string type, string parentID = "")
        {
            //if(type!= "User")
            //{
            //var data = GetuserDetails(parentID);
            //ViewBag.type = type;
            //    return View(data);
            //}
            //else
            //{
            //    ViewBag.type = type;
            //    return View();
            //}

            var data = GetuserDetails(parentID);
            ViewBag.type = type;
            return View(data);
        }
        public UserMasterDetails GetuserDetails(string userID)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetuserMasterDetails?userID=" + userID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse objResponse = new Api_CommonResponse();
            List<UserMasterDetails> trailuserModals = new List<UserMasterDetails>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                    trailuserModals = JsonConvert.DeserializeObject<List<UserMasterDetails>>(objResponse.data.ToString());
            }

            return trailuserModals[0];

        }
        public JsonResult SaveUserDetails(UserMaster _userMaster)
        {
            var json = JsonConvert.SerializeObject(_userMaster);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/AddUserOrMember");
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
        public JsonResult UpdateUserDetails(UserMasterDetails _userMaster)
        {
            var json = JsonConvert.SerializeObject(_userMaster);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UpdateuserDetails");
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
        public JsonResult CollectPayment(PaymentTracking payment)
        {
            var json = JsonConvert.SerializeObject(payment);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/CollectPayment_PaymentTracking");
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

        public JsonResult Resetpassword(ResetPassword reset)
        {
            try
            {
                var json = JsonConvert.SerializeObject(reset);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/Reset");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
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
                    Data = new { StatusCode = _CommonResponse.statusCode, Data = _CommonResponse, Failure = false, Message = _CommonResponse.message },
                    ContentEncoding = System.Text.Encoding.UTF8,
                    JsonRequestBehavior = JsonRequestBehavior.AllowGet
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public ActionResult AddNewuser(UserMaster userMst)
        {
            try
            {
                var json = JsonConvert.SerializeObject(userMst);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/AddNewUser");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    var objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);

                    if (objResponse.message.Contains("Details Already Exists..!"))
                    {
                        TempData["IsUserDetailsExists"] = 1;
                        TempData["msg"] = "User Details Already Exists, Details Not Saved...";
                        return RedirectToAction("AddNewUserIndex", "User");
                    }
                    else
                    {
                        TempData["IsUserDetailsExists"] = 0;
                        TempData["msg"] = "User Details Saved Successfully!";
                        return RedirectToAction("AddNewUserIndex", "User");
                    }
                }
                else
                {
                    TempData["IsUserDetailsExists"] = 1;
                    TempData["msg"] = "Details Not Saved Due To Some Internal Issues...!";
                    return RedirectToAction("AddNewUserIndex", "User");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}