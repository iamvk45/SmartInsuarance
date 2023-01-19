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
        public ActionResult PendingPayments()
        {
            ViewBag.pendingPayments = GetPendingPayments();
            return View();
        }

        public ActionResult RegisteredUser()
        {
            var sessionData = (UserModelSession)Session["UserDetails"];
            List<UsersList> users = new List<UsersList>();
            if (sessionData != null)
            {
                if (sessionData.sUSRCode != "A000001")
                {
                    users = GetUsersListToActiveInactive(sessionData.sUSRCode, "Childs");
                }
                else
                {
                    users = GetUsersListToActiveInactive(sessionData.sUSRCode);
                }
            }

            ViewBag.registeredUsers = users;
            return View();
        }
        public ActionResult RegistrationReport()
        {
            return View();
        }
        public ActionResult EarningReport()
        {
            return View();
        }

        public ActionResult PaymentHistory()
        {

            var userSession = (UserModelSession)Session["UserDetails"];
            if (userSession != null)
            {
                if (userSession.sUSRCode == "A000001")
                    ViewBag.paymentHistory = GetPaymentHistory();
                else
                    ViewBag.paymentHistory = GetPaymentHistory(userSession.sUSRCode);
            }
            else
            {
                ViewBag.paymentHistory = GetPaymentHistory();
            }
            return View();
        }
        public ActionResult LicenseConfiguration()
        {
            CommonController common = new CommonController();
            var userData = (UserModelSession)Session["UserDetails"];
            var licenseConfigData = common.GetLicenseConfigData(userData.sUSRCode);

            ViewBag.licenseConfigData = licenseConfigData;

            return View();
        }
        public ActionResult UserLicenseConfiguration(int configurationID)
        {
            CommonController common = new CommonController();
            var CompanyList = common.GetDataForDropdown("Companies", 0);
            var userSession = (UserModelSession)Session["UserDetails"];
            var InsuranceList = common.GetInsuranceForLicense(userSession.sUSRCode);
            var childData = common.GetChildID(userSession.sUSRCode);

            var LicenceDetails = LicenseDetailsForEdit(configurationID);

            ViewBag.childData = childData;
            ViewBag.companyList = CompanyList;
            ViewBag.InsuranceList = InsuranceList;
            ViewBag.LicenceDetailsForEdit = LicenceDetails;
            ViewBag.licenceIPKID = configurationID;

            return View();
        }

        public ActionResult CollectPayment(string orderID = "")
        {
            CommonController common = new CommonController();

            ViewBag.paymentMethod = common.GetDataForDropdown("PaymentMethod",0);
            ViewBag.orderDetails = GetOrderDetailsForPayment(orderID);
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

        public List<UsersList> GetUsersListToActiveInactive(string userCode, string selectiontype = "OnlyParents")
        {
            //var json = JsonConvert.SerializeObject(new { type = selectiontype, userID = userCode });
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetUsersList?userID=" + userCode + "&type=" + selectiontype);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse objResponse = new Api_CommonResponse();
            List<UsersList> registeredUsers = new List<UsersList>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                    registeredUsers = JsonConvert.DeserializeObject<List<UsersList>>(objResponse.data.ToString());

            }

            return registeredUsers;
        }
        public List<PaymentHistory> GetPaymentHistory(string userID = "0")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetPaymentHistory?userID=" + userID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse objResponse = new Api_CommonResponse();
            List<PaymentHistory> trailuserModals = new List<PaymentHistory>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                    trailuserModals = JsonConvert.DeserializeObject<List<PaymentHistory>>(objResponse.data.ToString());

            }

            return trailuserModals;
        }
        public JsonResult GetuserDetails(string userID)
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

            return new JsonResult
            {
                Data = new { StatusCode = objResponse.statusCode, Data = trailuserModals, Failure = false, Message = objResponse.message },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };

        }
        public List<PendingPayments> GetPendingPayments()
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetPendingPayments");
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse objResponse = new Api_CommonResponse();
            List<PendingPayments> pendingPayments = new List<PendingPayments>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                    pendingPayments = JsonConvert.DeserializeObject<List<PendingPayments>>(objResponse.data.ToString());

            }

            return pendingPayments;
        }
        public List<CollectPaymentData> GetOrderDetailsForPayment(string order)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetOrderDetailsForPayment?orderID=" + order);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse objResponse = new Api_CommonResponse();
            List<CollectPaymentData> pendingPayments = new List<CollectPaymentData>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                    pendingPayments = JsonConvert.DeserializeObject<List<CollectPaymentData>>(objResponse.data.ToString());

            }

            return pendingPayments;
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
        public JsonResult ActiveInActiveUser(string userCode, int isActive)
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UpdateStatus?status=" + isActive + "&userID=" + userCode);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
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

        public JsonResult ConfigLicencse(UserLicenseConfigRequest userLicense)
        {
            var json = JsonConvert.SerializeObject(userLicense);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/ConfigLicencse");
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
        public JsonResult UpdateLicencseConfiguration(UserLicenseConfigRequest userLicense)
        {
            var json = JsonConvert.SerializeObject(userLicense);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/UpdateLicencseConfiguration");
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
        
        public UserLicenseConfigRequest LicenseDetailsForEdit(int IpkID)
        {
            List<UserLicenseConfigRequest> userLicenseConfig = new List<UserLicenseConfigRequest>();
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/LicenceDetails?Ipk_Licence_ID="+ IpkID);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse objResponse = new Api_CommonResponse();

            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                    userLicenseConfig = JsonConvert.DeserializeObject<List<UserLicenseConfigRequest>>(objResponse.data.ToString());
                
                if(objResponse.data1 != null)
                    userLicenseConfig[0].relationshipManagers = JsonConvert.DeserializeObject<List<UserRelationshipManager>>(objResponse.data1.ToString());
            }
            if (userLicenseConfig != null && userLicenseConfig.Count>0)
                return userLicenseConfig[0];
            else
                return new UserLicenseConfigRequest();
            
        }
    }
}