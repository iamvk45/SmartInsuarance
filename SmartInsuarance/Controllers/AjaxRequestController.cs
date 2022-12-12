using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
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
    public class AjaxRequestController : Controller
    {
        JavaScriptSerializer _JsonSerializer = new JavaScriptSerializer();
        // GET: AjaxRequest
        #region InsertCoverLicenseTemp

        public JsonResult InsertCoverLicenseTemp(int MenuId, int SubMenuId, int qty,string Id,int IPk_CovLicTempId)
        {
            CoverLiceTemp Master = new CoverLiceTemp();
            Master.IPk_CovLicTempId = IPk_CovLicTempId;
            Master.iqty = qty;
            Master.iMnuid = MenuId;
            Master.iSubMnuid = SubMenuId;
            Master.IFK_LiceId = Id;
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/InsertCoverLicenseTemp");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json", _JsonSerializer.Serialize(Master), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            //var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/InsertCoverLicenseTemp");
            //var request = new RestRequest(Method.POST);
            //request.AddHeader("cache-control", "no-cache");
            ////request2.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            //request.AddParameter("application/json", _JsonSerializer.Serialize(temp), ParameterType.RequestBody);
            //IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponseData = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponseData.statusCode == 1 && objResponseData.responseCode==1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.statusCode == 1 && objResponseData.responseCode == 0)
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
        #region ChangeStatus
        public JsonResult ChangeStatus(string TableName, int Id, int type)
        {
          
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/ChangeStatus?TableName="+ TableName +"&Id="+Id+"&type="+ type);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json","", ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
          
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponseData = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponseData.statusCode == 1 && objResponseData.responseCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg =objResponseData.message, isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.statusCode == 1 && objResponseData.responseCode == 0)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.message, isvalid = 1 },
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