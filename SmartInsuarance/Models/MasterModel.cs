using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartInsuarance.Models
{
    public class MasterModel
    {
    }
    public class FinYear
    {
        public int iPk_Id { get; set; }
        public string sName { get; set; }
        public string dtInsertStrDt { get; set; }
        public string dtInsertEndDt { get; set; }
        public int iStts { get; set; }
    }
    public class FinYearView: FinYear
    {
        public DateTime? dtStrdate { get; set; }
        public DateTime? dtEnddate { get; set; }
       
        public string StartDate
        {
            get
            {
                if (!string.IsNullOrEmpty(dtStrdate.ToString()))
                {
                    return dtStrdate.Value.ToString("ddd dd MMM yyyy");
                }
                else
                {
                    return null;
                }
            }
        }
        public string EndDate
        {
            get
            {
                if (!string.IsNullOrEmpty(dtEnddate.ToString()))
                {
                    return dtEnddate.Value.ToString("ddd dd MMM yyyy");
                }
                else
                {
                    return null;
                }
            }
        }
       
    }

    public class FEATRATMST
    {
        public int iPk_FetRatMstId { get; set; }

        public string dtInsertStrDt { get; set; }
        public string dtInsertEndDt { get; set; }
        public int iStts { get; set; }
        public int iFK_FetId { get; set; }
        public int iFK_UomId { get; set; }
        public int iFk_FinYearId { get; set; }
        public decimal dRate { get; set; }
    }
    public class FEATRATMSTView : FEATRATMST
    {
        public DateTime? dtStrdate { get; set; }
        public DateTime? dtEnddate { get; set; }

        public string StartDate
        {
            get
            {
                if (!string.IsNullOrEmpty(dtStrdate.ToString()))
                {
                    return dtStrdate.Value.ToString("ddd dd MMM yyyy");
                }
                else
                {
                    return null;
                }
            }
        }
        public string EndDate
        {
            get
            {
                if (!string.IsNullOrEmpty(dtEnddate.ToString()))
                {
                    return dtEnddate.Value.ToString("ddd dd MMM yyyy");
                }
                else
                {
                    return null;
                }
            }
        }
        public string FeatureName
        {
            get
            {
                switch (iFK_FetId)
                {
                    case 1:
                        return "SMS";
                    case 2:
                        return "What's up";
                    case 3:
                        return "Storage";
                    case 4:
                        return "Notifiaction";
                    case 5:
                        return "User";
                    default:
                        return "";
                }
            }
        }
        public string UOMeName
        {
            get
            {
                switch (iFK_UomId)
                {
                    case 1:
                        return "Year";
                    case 2:
                        return "Month";
                    case 3:
                        return "Day";
                 
                    default:
                        return "";
                }
            }
        }
    }
    public class CoverLiceTemp
    {
        public int IPk_CovLicTempId { get; set; }
        public string  IFK_LiceId { get; set; }
        public int iMnuid { get; set; }
        public int? iSubMnuid { get; set; }
        public int iqty { get; set; }
        public int iStts { get; set; }

    }
    public class CoverLiceTempView: CoverLiceTemp
    {
        public string sMenuname { get; set; }
        public string sSubMenuname { get; set; }
        public string Rate { get; set; }
    }
    public class LICEMST
    {
        public string iPK_LicMstId { get; set; }
        public string dtInsertStrDt { get; set; }
        public string dtInsertEndDt { get; set; }
        public int iStts { get; set; }
        public string iLicNameId { get; set; }
        public int iUsrTypId { get; set; }
        public int iValidityId { get; set; }
        public int iNumReg { get; set; }
        public int iFK_FinYR { get; set; }
        public int iTypId { get; set; }
        public int iValidityvalue { get; set; }
        public string LicenseName { get; set; }
    }
    public class LICEMSTViEW: LICEMST
    {
        public DateTime? dtStrdate { get; set; }
        public DateTime? dtEnddate { get; set; }
        public DateTime? dtCrtDate { get; set; }

        public string StartDate
        {
            get
            {
                if (!string.IsNullOrEmpty(dtStrdate.ToString()))
                {
                    return dtStrdate.Value.ToString("dd-MM-yyyy ");
                }
                else
                {
                    return null;
                }
            }
        }
        public string EndDate
        {
            get
            {
                if (!string.IsNullOrEmpty(dtEnddate.ToString()))
                {
                    return dtEnddate.Value.ToString("dd-MM-yyyy ");
                }
                else
                {
                    return null;
                }
            }
        }
        public string ValidityName
        {
            get
            {
                switch (iValidityId)
                {
                    case 1:
                        return "Day";
                    case 2:
                        return "Month";
                    case 3:
                        return "Year";

                    default:
                        return "";
                }
            }
        }
        public string RegistrationName
        {
            get
            {
                switch (iTypId)
                {
                    case 1:
                        return "Single";
                    case 2:
                        return "Multiple";


                    default:
                        return "";
                }
            }
        }
        public string UsereName
        {
            get
            {
                switch (iUsrTypId)
                {
                    case 1:
                        return "Agent";
                    case 2:
                        return "Broker";

                    case 3:
                        return "User";
                    case 4:
                        return "Trial User";
                    default:
                        return "";
                }
            }
        }

    }
}