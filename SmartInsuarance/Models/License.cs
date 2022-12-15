using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartInsuarance.Models
{
    public class License
    {
        public string LicenseID { get; set; }
        public int? Validity { get; set; }
        public int? NoOfRegistraion { get; set; }
        public string LicenseeName { get; set; }
        public string Username { get; set; }
        public DateTime? TrialPeriodStartDate { get; set; }
        public DateTime TrialPeriodvalidTill { get; set; }
    }


    public class Insuarance
    {
        public string InsuaranceName { get; set; }
        public string QuantityAvailable { get; set; }
    }
    public class LicenseConfigDetails
    {
        public string Insurance { get; set; }
        public string User { get; set; }
        public string Company { get; set; }
    }
}