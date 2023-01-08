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
    public class FEATRATMSTDROP
    {
        public int iPk_FetRatMstId { get; set; }
        public string FeatureName { get; set; }
        public decimal dRate { get; set; }
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
        public int iQty { get; set; }
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
                    case 6:
                        return "System User";
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
                    case 3:
                        return "Year";
                    case 2:
                        return "Month";
                    case 1:
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
        public string UsereName { get; set; }
        public int? iLiceTypeId { get; set; }
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
        public string LiceType
        {
            get
            {
                switch (iLiceTypeId)
                {
                    case 1000:
                        return "Paid";
                    case 1001:
                        return "Trial";

                
                    default:
                        return "";
                }
            }
        }

    }

    public class PackFunctTemp
    {
        public int iPk_PackFunctTempId { get; set; }
        public string iFk_PackMstId { get; set; }
        public int iMnuId { get; set; }
        public int iSubMnuId { get; set; }
        public decimal dRate { get; set; }
        public int? iIsdisct { get; set; }
        public int? iDisTyp { get; set; }
        public decimal? dDisValue { get; set; }
        public string sDispvalu { get; set; }
        public int? iStts { get; set; }
        public int? iValiId { get; set; }
        public int? iValiVal { get; set; }
        public decimal? dWdiscRate { get; set; }
    }
    public class PackFunctTempview: PackFunctTemp
    {
       public string sMenuName { get; set; }
        public string sSubMnuName { get; set; }
        public string ValidityName
        {
            get
            {
                switch (iValiId)
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
    }

    public class PackageManagement 
    {
        public int iPk_PackId { get; set; }
        public string dtInsertStrDt { get; set; }
        public string dtInsertEndDt { get; set; }
        public string  sPackName { get; set; }
        public int iUsrTyp { get; set; }
        public string iFk_LiceId { get; set; }
        public int iStts { get; set; }
        public int iFk_FinyearId { get; set; }
        public Boolean? iisgstapplicable { get; set; }
        public Boolean? ipkgsts { get; set; }
        public decimal? iGstval { get; set; }
        public Boolean? bCompSts { get; set; }

    }
    public class PackageManagementView: PackageManagement
    {
        public DateTime? dtStrdate { get; set; }
        public DateTime? dtEnddate { get; set; }
        public DateTime? dtCrtDate { get; set; }
        public int? iValidityId { get; set; }
        public int? iValidityvalue { get; set; }
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
        public string UsereName {get;set;}
        public string LicenseName { get; set; }
        public decimal? dPackFunAmt { get; set; }
        public decimal? dPackFeaAmt { get; set; }
        public decimal? dPackSysAmt { get; set; }
    }
    public class PACKFUNC
    {
         public int iPk_PackFunctId{ get; set; }
         public int iFk_packid     { get; set; }
         public string iLicenseid     { get; set; }
         public int iMnuId         { get; set; }
         public int iSubMnuId      { get; set; }
         public int ivalidityvalue { get; set; }
         public int ivalidityid    { get; set; }
         public decimal drate          { get; set; }
         public Boolean iisdiscount    { get; set; }
         public int idistyp        { get; set; }
         public decimal ddisvalue      { get; set; }
         public decimal ddisplayvalue  { get; set; }
         public int? iStts          { get; set; }
         public int iFk_FinYear { get; set; }

    }
    public class PACKFUNCView : PACKFUNC
    {
        public string dtCtrDate { get; set; }
        public string MenuName { get; set; }
        public string SubName { get; set; }
        public string ValidityName
        {
            get
            {
                switch (ivalidityid)
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
        public string DisType
        {
            get
            {
                switch (idistyp)
                {
                    case 1:
                        return "Percentage";
                    case 2:
                        return "Amount";

                    default:
                        return "";
                }
            }
        }
    }
   public class PACKFEATURSPE
    {
        public int iPK_PackFeatId { get; set; }
        public int iFk_packid { get; set; }
        public int iFetId { get; set; }
        public int ivalidityvalue { get; set; }
        public int ivalidityid { get; set; }
        public decimal drate { get; set; }
        public Boolean iisdiscount { get; set; }
        public int idistyp { get; set; }
        public decimal ddisvalue { get; set; }
        public decimal ddisplayvalue { get; set; }
        public int? iStts { get; set; }
        public int iFk_FinYear { get; set; }
       
    }
    public class PACKFEATUESPEView:PACKFEATURSPE
    {
        public string ValidityName
        {
            get
            {
                switch (ivalidityid)
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
        public string sFeatureName { get; set; }
        public int iFK_FetId { get; set; }
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
        public string DisType
        {
            get
            {
                switch (idistyp)
                {
                    case 1:
                        return "Percentage";
                    case 2:
                        return "Amount";

                    default:
                        return "";
                }
            }
        }
    }

    public class PACKTAXDIS
    {
        public int? iPk_TaxDisId { get; set; }
        public int? iFk_packid { get; set; }
        public int? iPackFunt_iStts { get; set; }
        public int? iPackFeat_iStts { get; set; }
        public int? iPackSysUsr_iStts { get; set; }
        public int? iTotalDis_iStts { get; set; }
        public decimal? dPackFunt_Val { get; set; }
        public decimal? dPackFeat_Val { get; set; }
        public decimal? dPackSysUsr_Val { get; set; }
        public decimal? dTotalDis_Val { get; set; }
        public int? iPackFunt_DisTyp { get; set; }
        public int? iPackFeat_DisTyp { get; set; }
        public int? iPackSysUsr_DisTyp { get; set; }
        public int? iTotalDis_Typ { get; set; }
        public int? iStts { get; set; }
        public decimal? dPackFunAmt { get; set; }
        public decimal? dPackFeaAmt { get; set; }
        public decimal?dPackSysAmt { get; set; }
        public decimal? PackFunt_displayvalue { get; set; }
        public decimal? PackFeat_displayvalue { get; set; }
        public decimal? Total_displayvalue { get; set; }

        public decimal Total
        {
            get
            {
                decimal total = 0;
                total = Convert.ToDecimal(dPackFunAmt) + Convert.ToDecimal(dPackFeaAmt) + Convert.ToDecimal(dPackSysAmt);
                return total;
            }
        }

    }

    public class PackShowView: PackageManagement
    {
        public int iLiceTypeId{ get; set; }
        public int iValidityId{ get; set; }
        public int iNumReg{ get; set; }
        public int iTypId { get; set; } 
        public int iValidityvalue{ get; set; }
        public string LiceName
        {
            get
            {
                switch (iLiceTypeId)
                {
                    case 1000:
                        return "Paid";
                    case 1001:
                        return "Trial";
                    default:
                        return "";
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
    }

    public class PackFunctionShow: PACKFUNCView
    {
        public string sMenuName { get; set; }
        public string sSubMnuName { get; set; }
    }

}