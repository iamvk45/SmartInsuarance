using Newtonsoft.Json;
using RestSharp;
using RestSharp.Serializers;
using SmartInsuarance.Helper;
using SmartInsuarance.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

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
        #region PostPackageFunctionlity
        public JsonResult InsertPackageFunctionality(List<PACKFUNC> Master)
        {
            PACKFUNC temp = new PACKFUNC();
            if (Master.Count > 0)
            {
                temp = Master.FirstOrDefault();
            }
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/InsertPackageFunctionality");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json", _JsonSerializer.Serialize(temp), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponseData = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponseData.statusCode == 1 && objResponseData.responseCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.message, isvalid = 1 },
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
        #region GetBindLicense
        public JsonResult GetBindLicense(int Id)
        {
            
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetBindLicense?Id=" + Id);
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
            return new JsonResult
            {
                Data = new { Data = groups, failure = false, msg = "Success", isvalid = 1 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

        #region
        public JsonResult GetPackageSelectMenuSubmenulist(string Id)
        {

            List<Dropdown> groups = new List<Dropdown>();
            groups = CommonFunction.GetLicenceCoverList(Id);
            return new JsonResult
            {
                Data = new { Data = groups, failure = false, msg = "Success", isvalid = 1 },
                ContentEncoding = System.Text.Encoding.UTF8,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        #endregion

        public JsonResult InsertPackageFunctionalityTemp(int MnuId,int SubMenuId,string LicMstId,string iFk_PackMstId)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/Package_Functionality_Save?iMnuId=" + MnuId + "&iSubMenuid="+ SubMenuId + "&LicMstId="+ LicMstId+ "&iFk_PackMstId="+ iFk_PackMstId);
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json","", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponseData = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                
                if (objResponseData.statusCode == 1 && objResponseData.responseCode == 1)
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

        public JsonResult FindRate(int iMnuId,int iSubMnuId)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetFindRate?iMnuId=" + iMnuId + "&iSubMenuid=" + iSubMnuId );
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<Dropdown> groups = new List<Dropdown>();
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponseData = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
         
                if (objResponseData.data != null)
                {

                    groups = JsonConvert.DeserializeObject<List<Dropdown>>(objResponseData.data.ToString());

                }
                if (objResponseData.statusCode == 1 && objResponseData.responseCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = groups, failure = false, msg = "Success", isvalid = 1 },
                        ContentEncoding = System.Text.Encoding.UTF8,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else if (objResponseData.statusCode == 1 && objResponseData.responseCode == 0)
                {
                    return new JsonResult
                    {
                        Data = new { Data = groups, failure = false, msg = "Success", isvalid = 1 },
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

        #region InsertPackageFeature
        public JsonResult InsertPackageFeature(List<PACKFEATURSPE> Master)
        {
            PACKFEATURSPE temp = new PACKFEATURSPE();
            if (Master.Count > 0)
            {
                temp = Master.FirstOrDefault();
            }
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/InsertPackageFeature");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");

            request.AddParameter("application/json", _JsonSerializer.Serialize(temp), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponseData = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponseData.statusCode == 1 && objResponseData.responseCode == 1)
                {
                    return new JsonResult
                    {
                        Data = new { Data = "", failure = false, msg = objResponseData.message, isvalid = 1 },
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