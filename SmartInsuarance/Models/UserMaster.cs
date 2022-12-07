using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartInsuarance.Models
{
    public class UserMaster
    {
        public int iPK_USRID { get; set; }
        public string sUSRCode { get; set; }
        public string sPrntID { get; set; }
        public string sNME { get; set; }
        public string sSurNME { get; set; }
        public string sUSRNME { get; set; }
        public string dtDOB { get; set; }
        public string sEmail { get; set; }
        public string sPhone { get; set; }
        public int iCountryID { get; set; }
        public int iStateID { get; set; }
        public int iCityID { get; set; }
        public string sPincode { get; set; }
        public string sAadhaar { get; set; }
        public string sPan { get; set; }
        public string sAddress { get; set; }
        public int iRoleID { get; set; }
        public int iFinYrID { get; set; }
        public int iFK_brnchID { get; set; }
        public string bPassword { get; set; }
        public string sSalt { get; set; }
        public string iIsactive { get; set; }
        public string sLatitude { get; set; }
        public string sLongitude { get; set; }
        public string sCrtedBy { get; set; }
        public string dtCrtedOn { get; set; }
        public string sUpdateBy { get; set; }
        public string dtUpdateOn { get; set; }
        public byte[] aadhaarImg { get; set; }
        public byte[] panImg { get; set; }
        public string type { get; set; }
    }

    public class UserMasterDetails
    {
        public string sUSRCode { get; set; }
        public string sPrntID { get; set; }
        public string Name { get; set; }
        public string sUSRNME { get; set; }
        public int iUsrTypeID { get; set; }
        public string UserType { get; set; }
        public string dtDOB { get; set; }
        public string sEmail { get; set; }
        public string sPhone { get; set; }
        public int iCountryID { get; set; }
        public string sContryName { get; set; }
        public int iStateID { get; set; }
        public string sStateName { get; set; }
        public int iCityID { get; set; }
        public string sCityName { get; set; }
        public string sPincode { get; set; }
        public string sAadhaar { get; set; }
        public string sPan { get; set; }
        public string sAddress { get; set; }
        public int iRoleID { get; set; }
        public string Role { get; set; }
        public string location { get; set; }
        public string sCrtedBy { get; set; }
        public string dtCrtedOn { get; set; }
        public string sUpdateBy { get; set; }
        public string dtUpdateOn { get; set; }


    }
    public class UserModelSession
    {
        public string sUSRCode { get; set; }

        public string Name { get; set; }
        public string Username { get; set; }
        public string sEmail { get; set; }
        public string sPhone { get; set; }
        public string IsActive { get; set; }
        public string profileImage { get; set; }
        public int iStateID { get; set; }
        public int iCountryID { get; set; }
        public int iCityID { get; set; }
    }
    public class LoginModal
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

    }

    public class TrailuserModal
    {
        public int RowNo { get; set; }
        public int iPk_USRFRTRLID { get; set; }
        public string sName { get; set; }
        public string sPincode { get; set; }
        public string sMobileNo { get; set; }
        public string sEmailId { get; set; }
        public int iUsrTyp { get; set; }
        public string userTypeName { get; set; }
        public DateTime? dtDateOfReg { get; set; }
        public int iIsactive { get; set; }
    }
}