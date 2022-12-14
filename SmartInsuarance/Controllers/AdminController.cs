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
            AuthController admin = new AuthController();
            var userData = (UserModelSession)Session["UserDetails"];
            var insuaranceDetails = admin.GetInsuaranceData(userData.sUSRCode, userData.iFK_LicMstId);

            ViewBag.insuarances = insuaranceDetails;

            return View();
        }
        public ActionResult UserLicenseConfiguration(string insuarance)
        {
            ViewBag.insuarance = insuarance;

            CommonController common = new CommonController();
            var CompanyList = common.GetDataForDropdown(1005);
            var userSession = (UserModelSession)Session["UserDetails"];
            var childData = common.GetChildID(userSession.sUSRCode);

            ViewBag.childData = childData;
            ViewBag.companyList = CompanyList;

            return View();
        }

        public ActionResult CollectPayment(string orderID = "")
        {
            CommonController common = new CommonController();

            ViewBag.paymentMethod = common.GetDataForDropdown(5);
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

    }
}