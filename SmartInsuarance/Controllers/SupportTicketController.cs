﻿using Newtonsoft.Json;
using SmartInsuarance.Helper;
using SmartInsuarance.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SmartInsuarance.Controllers

{
    public class SupportTicketController : Controller
    {
        Api_CommonResponse objResponse;

        // GET: SupportTicket
        public ActionResult Index()
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            if (userdetailsSession != null)
            {

                var ddlModule = GetModule();
                var ddlFunctionality = GetFunctionality();
                var ddlTicket = GetTicket();
                ViewBag.Module = ddlModule;
                ViewBag.Functionality = ddlFunctionality;
                ViewBag.Ticket = ddlTicket;
            }

            return View();
        }

        public List<Dropdown> GetModule()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Common/GetModule_st");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> data = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {
                    data = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.data.ToString());
                }
            }
            return data;
        }

        public List<Dropdown> GetFunctionality()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Common/GetFunctionality_st");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> data = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {
                    data = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.data.ToString());
                }
            }
            return data;
        }

        public List<Dropdown> GetTicket()
        {

            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "User/GetTicket");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> data = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {
                    data = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.data.ToString());
                }
            }
            return data;
        }

        public JsonResult SaveData(SupportTicket mapping)
        {
            var userdetailsSession = (UserModelSession)Session["UserDetails"];
            var Token = Session["Token"];
            var json = JsonConvert.SerializeObject(mapping);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Common/SaveSupportTicket_st");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "bearer " + Token + "");
            request.AddParameter("application/json", json, ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);

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