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
        public int? ConfigID { get; set; }
        public string Insurance { get; set; }
        public string User { get; set; }
        public string Company { get; set; }
        public int? iIsactive { get; set; }
    }
    public class UserLicenseConfigRequest
    {
        public int? iPK_ID { get; set; }
        public string sCnfigredUSRID { get; set; }
        public string sFK_prntID { get; set; }
        public string sChildID { get; set; }
        public int? iMenuID { get; set; }
        public int? iCmpnyID { get; set; }
        public string sRegstionNo { get; set; }
        public string dtExpry { get; set; }
        public string sCKYC_No { get; set; }
        public int? iBranchID { get; set; }
        public int? iFinyrID { get; set; }
        public DateTime? dtCreatedOn { get; set; }
        public List<UserRelationshipManager> relationshipManagers { get; set; }
    }

    public class UserRelationshipManager
    {
        public int? iPK_ID { get; set; }
        public int? iFK_insCmpCngID { get; set; }
        public string sName { get; set; }
        public string sMobileNo { get; set; }
        public string sEmail { get; set; }
        public int? iIsactive { get; set; }
    }
}