using Newtonsoft.Json;
using RestSharp;
using SmartInsuarance.Model;
using SmartInsuarance.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SmartInsuarance.Helper
{
    public class CommonFunction
    {

        public static List<FillDropDown> GetDataForDropdown(int enumNum)
        {
            //var json = JsonConvert.SerializeObject(geographical);
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Common/GetFillDropDowns?EnumId=" + enumNum);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            Api_CommonResponse _CommonResponse = new Api_CommonResponse();
            List<FillDropDown> dropDowns = new List<FillDropDown>();

            if (response.StatusCode.ToString() == "OK")
            {
                _CommonResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (_CommonResponse.data != null)
                {
                    dropDowns = JsonConvert.DeserializeObject<List<FillDropDown>>(_CommonResponse.data.ToString());

                }
            }

            return dropDowns;
        }

        public static LICEMSTViEW GetSelectLicence(string Id)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetSelectLicence?Id=" + Id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            LICEMSTViEW groups = new LICEMSTViEW();
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {
                    groups = JsonConvert.DeserializeObject<List<LICEMSTViEW>>(objResponse.data.ToString()).FirstOrDefault();
                }
            }
            return groups;
        }
        public static List<Dropdown>GetLicenceCoverList(string Id)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetPackageFillMenuandSubMenulist?Id=" + Id);
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
            return groups;
        }

        public static List<PackageManagementView> PackageManagementViewList(int Id=0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetPackageManagementViewlist?Id=" + Id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<PackageManagementView> groups = new List<PackageManagementView>();
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {
                   
                        groups = JsonConvert.DeserializeObject<List<PackageManagementView>>(objResponse.data.ToString());
                    
                }
        }
          
            return groups;
            
            
        }
        public static List<PACKTAXDIS> PackageTaxDisList(int Id = 0)
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "Master/GetPackageTaxDis?Id=" + Id);
            var request = new RestRequest(Method.GET);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<PACKTAXDIS> groups = new List<PACKTAXDIS>();
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                if (objResponse.data != null)
                {

                    groups = JsonConvert.DeserializeObject<List<PACKTAXDIS>>(objResponse.data.ToString());

                }
            }

            return groups;


        }

        public static List<AddDepartment> GetDepartments(string Type = "DepartmentList")
        {
            var client = new RestClient(ConfigurationManager.AppSettings["BaseUrl"] + "RoleMaster/GetData?Type=" + Type + "&MenuId=0");
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            //request.AddHeader("authorization", "bearer " + CurrentSessions.Token + "");
            request.AddParameter("application/json", "", ParameterType.RequestBody);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");
            IRestResponse response = client.Execute(request);
            List<AddDepartment> departments = new List<AddDepartment>();
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                departments = JsonConvert.DeserializeObject<List<AddDepartment>>(objResponse.data.ToString());
            }
            return departments;
        }

        public static List<Dropdown> GetMenusList(string Type = "MenuList", int MenuId = 0)
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
            if (response.StatusCode.ToString() == "OK")
            {
                Api_CommonResponse objResponse = JsonConvert.DeserializeObject<Api_CommonResponse>(response.Content);
                MenusList = JsonConvert.DeserializeObject<List<Dropdown>>(objResponse.data.ToString());
            }
            return MenusList;
        }
    }
}