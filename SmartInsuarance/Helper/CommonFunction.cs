using Newtonsoft.Json;
using RestSharp;
using SmartInsuarance.Model;
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
    }
}