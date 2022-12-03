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
using System.Web.UI.WebControls;

namespace SmartInsuarance.Controllers
{
    public class AuthController : Controller
    {
        Api_CommonResponse _CommonResponse = new Api_CommonResponse();
        public ActionResult Login()
        {
            return View();
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

                    userModel = JsonConvert.DeserializeObject<List<UserModelSession>>(_CommonResponse.data.ToString());
                    if (_CommonResponse.data != null)
                    {
                        if (userModel[0].sUSRCode == "A000001")
                        {
                            //if (permissions != null)
                            //{
                            Session["UserDetails"] = userModel;

                            //}
                            return RedirectToAction("Index", "Dashboard");
                        }
                        else if (userModel[0].IsActive == "0")
                        {
                            TempData["IsUserDetailsExists"] = 1;
                            TempData["msg"] = "Your Account is blocked Please Contact to admin...";
                            return RedirectToAction("Login", "Auth");
                        }
                        else
                        {
                            Session["UserDetails"] = userModel[0];
                            return RedirectToAction("Index", "Dashboard");
                            //return RedirectToAction("EditProfile", "User");
                        }
                    }

                    if (_CommonResponse.message.Contains("User Details Not Found..."))
                    {
                        TempData["IsUserDetailsExists"] = 1;
                        TempData["msg"] = "Invalid Credentials Please Try Again...";
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
                    return RedirectToAction("Login", "Auth");
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
            return RedirectToAction("Login");
        }
    }
}