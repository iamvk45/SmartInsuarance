using Newtonsoft.Json;
using RestSharp;
using SmartInsuarance.Helper;
using SmartInsuarance.Model;
using SmartInsuarance.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using System.ComponentModel;
using License = SmartInsuarance.Models.License;
using System.Web.Services.Description;
using Microsoft.Ajax.Utilities;

namespace SmartInsuarance.Controllers
{
    public class AuthController : Controller
    {
        Api_CommonResponse _CommonResponse = new Api_CommonResponse();
        
        public ActionResult Login()
        {
            Session["OTPVerification"] = null;
            Session["packageID"] = null;
            Session["InsCoverID"] = null;
            Session["UserDetails"] = null;
            Session["LicenseDetails"] = null;
            Session["InsuaranceDetails"] = null;
            Session["UserPermissions"] = null;

            return View();
        }

        public ActionResult LoginAlt(int packID = 0)
        {
            ViewBag.packID = packID;
            return View();
        }

        public ActionResult Chackout()
        {
            var pkgID  = (int)Session["packageID"];
            var covID = (int)Session["InsCoverID"];

            CommonController common = new CommonController();
          var InsuranceData =  common.GetInsuranceCheckout("InsuranceCheckout", pkgID);
          var packageDetails =  common.GetPackageDetails(pkgID);

            ViewBag.InsuranceData = InsuranceData;
            ViewBag.packageDetails = packageDetails;
            ViewBag.CoverID = covID;
            return View();
        }

        public ActionResult Register(int packID = 0, int coverID = 0)
        {

            ViewBag.packID = packID;
            ViewBag.insCoverID = coverID;
            return View();
        }
        
        public ActionResult Resetpassword()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Resetpassword(ResetPassword reset)
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

                if (response.StatusCode.ToString() == "OK")
                {
                    _CommonResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);

                    TempData["IsUserDetailsExists"] = 1;
                    TempData["msg"] = "Password Updated Successfully...";
                    TempData["icon"] = "success";
                    return RedirectToAction("Login", "Auth");

                }
                else
                {
                    TempData["IsUserDetailsExists"] = 1;
                    TempData["msg"] = "Facing some internal issue, password not updated, kindly try after sometime...";
                    TempData["icon"] = "error";
                    return RedirectToAction("Login", "Auth");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult Login(LoginModal login)
        {
            try
            {
                List<UserModelSession> userModel = new List<UserModelSession>();

                var json = JsonConvert.SerializeObject(login);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/userLogin");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    _CommonResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);

                    if (_CommonResponse.data != null)
                    {
                        userModel = JsonConvert.DeserializeObject<List<UserModelSession>>(_CommonResponse.data.ToString());
                        List<UserPermissions> permissions = CommonController.GetPermissionDetails(userModel[0].iRoleID, userModel[0].iDeptID);


                        if (userModel[0].sUSRCode == "A000001")
                        {
                            //if (permissions != null)
                            //{
                            Session["UserPermissions"] = permissions;
                            Session["UserDetails"] = userModel[0];

                            //}
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else if (userModel[0].UsertypeID == 3026 || userModel[0].Usertype == "Company User")
                        {
                            //if (permissions != null)
                            //{
                            Session["UserPermissions"] = permissions;
                            Session["UserDetails"] = userModel[0];

                            //}
                            return RedirectToAction("EditProfile", "User");
                        }
                        else if (userModel[0].IsActive == "0")
                        {
                            TempData["IsUserDetailsExists"] = 1;
                            TempData["msg"] = "Your Account is blocked Please Contact to admin...";
                            TempData["icon"] = "error";
                            return RedirectToAction("Login", "Auth");
                        }
                        else
                        {
                            var LicencseDetails = GetLicenseData(userModel[0].sUSRCode, userModel[0].iFK_LicMstId);
                            var insuaranceDetails = GetInsuaranceData(userModel[0].sUSRCode, userModel[0].iFK_LicMstId);
                            Session["UserDetails"] = userModel[0];
                            Session["LicenseDetails"] = LicencseDetails;
                            Session["InsuaranceDetails"] = insuaranceDetails;
                            Session["UserPermissions"] = permissions;
                            return RedirectToAction("Index", "Welcome");
                            //return RedirectToAction("EditProfile", "User");
                        }
                    }

                    if (_CommonResponse.message.Contains("User Details Not Found..."))
                    {
                        TempData["IsUserDetailsExists"] = 1;
                        TempData["msg"] = "Invalid Credentials Please Try Again...";
                        TempData["icon"] = "error";
                        return RedirectToAction("Login", "Auth");
                    }
                    else
                    {
                        return RedirectToAction("Login", "Auth");
                    }
                }
                else
                {
                    TempData["IsUserDetailsExists"] = 1;
                    TempData["msg"] = "";
                    TempData["icon"] = "error";
                    return RedirectToAction("Login", "Auth");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public ActionResult LoginAlt(LoginModal login)
        {
            try
            {
                List<UserModelSession> userModel = new List<UserModelSession>();

                var json = JsonConvert.SerializeObject(login);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/userLogin");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    _CommonResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);

                    if (_CommonResponse.data != null)
                    {
                        userModel = JsonConvert.DeserializeObject<List<UserModelSession>>(_CommonResponse.data.ToString());
                        if (userModel[0].sUSRCode == "A000001")
                        {
                            //if (permissions != null)
                            //{
                            Session["UserDetails"] = userModel[0];

                            //}
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else if (userModel[0].IsActive == "0")
                        {
                            TempData["IsUserDetailsExists"] = 1;
                            TempData["msg"] = "Your Account is blocked Please Contact to admin...";
                            return RedirectToAction("LoginAlt", "Auth");
                        }
                        else
                        {
                            var LicencseDetails = GetLicenseData(userModel[0].sUSRCode, userModel[0].iFK_LicMstId);
                            var insuaranceDetails = GetInsuaranceData(userModel[0].sUSRCode, userModel[0].iFK_LicMstId);
                            var packDetails = getPackageData(Convert.ToInt32(userModel[0].UsertypeID));

                            Session["UserDetails"] = userModel[0];
                            Session["PackageData"] = packDetails;

                            if (insuaranceDetails != null)
                                Session["InsuaranceDetails"] = insuaranceDetails;

                            if (LicencseDetails != null)
                                Session["LicenseDetails"] = LicencseDetails;

                            //Session["LicenseDetails"] = LicencseDetails;
                            //Session["InsuaranceDetails"] = insuaranceDetails;

                            return RedirectToAction("Chackout", "Auth");
                            //return RedirectToAction("EditProfile", "User");
                        }
                    }

                    if (_CommonResponse.message.Contains("User Details Not Found..."))
                    {
                        TempData["IsUserDetailsExists"] = 1;
                        TempData["msg"] = "Invalid Credentials Please Try Again...";
                        return RedirectToAction("LoginAlt", "Auth");
                    }
                    else
                    {
                        return RedirectToAction("LoginAlt", "Auth");
                    }
                }
                else
                {
                    TempData["IsUserDetailsExists"] = 1;
                    return RedirectToAction("LoginAlt", "Auth");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Logout()
        {
            Session["UserDetails"] = null;
            Session["LicenseDetails"] = null;
            Session["InsuaranceDetails"] = null;
            Session["PackageData"] = null;
            return RedirectToAction("Login");
        }

        public ActionResult Verification()
        {
            return View();
        }

        public List<License> GetLicenseData(string usrID, string licensID, string type = "Licence")
        {
            //var json = JsonConvert.SerializeObject(geographical);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetLicense_InsuaranceData?type=" + type + "&usrID=" + usrID + "&licensID=" + licensID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse _CommonResponse = new Api_CommonResponse();
            List<License> licenses = new List<License>();

            if (response.StatusCode.ToString() == "OK")
            {
                _CommonResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (_CommonResponse.data != null)
                {
                    licenses = JsonConvert.DeserializeObject<List<License>>(_CommonResponse.data.ToString());
                }
            }
            return licenses;
        }
       
        public List<Insuarance> GetInsuaranceData(string usrID, string licensID, string type = "Insuarance")
        {
            //var json = JsonConvert.SerializeObject(geographical);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetLicense_InsuaranceData?type=" + type + "&usrID=" + usrID + "&licensID=" + licensID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse _CommonResponse = new Api_CommonResponse();
            List<Insuarance> insuarances = new List<Insuarance>();

            if (response.StatusCode.ToString() == "OK")
            {
                _CommonResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (_CommonResponse.data != null)
                {
                    insuarances = JsonConvert.DeserializeObject<List<Insuarance>>(_CommonResponse.data.ToString());
                }
            }
            return insuarances;
        }

        public List<PackDetails> getPackageData(int userType)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/PackDetails?PackID=" + userType);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse _CommonResponse = new Api_CommonResponse();
            List<PackDetails> insuarances = new List<PackDetails>();

            if (response.StatusCode.ToString() == "OK")
            {
                _CommonResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (_CommonResponse.data != null)
                {
                    insuarances = JsonConvert.DeserializeObject<List<PackDetails>>(_CommonResponse.data.ToString());
                }
            }
            return insuarances;

        }

        public ActionResult saveDetails(TrailuserModal trailuser, int pkID,int? insCoverID)
        {
            try
            {
                trailuser.iPackID = pkID;
                var json = JsonConvert.SerializeObject(trailuser);
                var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/Registeruser");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
                request.AddParameter("application/json", json, ParameterType.RequestBody);
                request.AddHeader("Content-Type", "application/json");
                request.AddHeader("Accept", "application/json");
                IRestResponse response = client.Execute(request);

                if (response.StatusCode.ToString() == "OK")
                {
                    _CommonResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);

                    //var packDetails = getPackageData(Convert.ToInt32(_CommonResponse.userCode));
                    //Session["PackageData"] = packDetails.Where(wh=>wh.iPk_PackId == pkID).ToList();

                    Session["packageID"] = pkID;
                    Session["InsCoverID"] = Convert.ToInt32(insCoverID);


                    if (string.IsNullOrEmpty(_CommonResponse.userID))
                    {
                        TempData["IsUserDetailsExists"] = 1;
                        TempData["msg"] = _CommonResponse.message;

                        return RedirectToAction("Register", "Auth");
                    }
                    else
                    {
                        Session["OTPVerification"] = _CommonResponse;
                        return RedirectToAction("Verification", "Auth");
                    }
                }
                else
                {
                    TempData["IsUserDetailsExists"] = 1;
                    TempData["msg"] = "Facing Some internal issue, Please try after some time";
                    return RedirectToAction("LoginAlt", "Auth");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public JsonResult updateStatus(VerificationRequest verification)
        {
            var json = JsonConvert.SerializeObject(verification);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UpadteVerificationStatus");
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