using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartInsuarance.Models
{
    public class Roll
    {
    }
    public class ShowMenuDropDown
    {
        public int MenuId { get; set; }
        public string MenuName { get; set; }
    }
    public class MenuMasters
        {
            public int MenuId { get; set; }
            public string ControllerName { get; set; }
            public string ActionMethod { get; set; }
            public string MenuName { get; set; }
            public int Status { get; set; }
            
        }
  
    public class SubMenuMaster
    {
        public int iSubMnuId { get; set; }
        public string sCntrolName { get; set; }
        public string sActnMthd { get; set; }
        public int iStatus { get; set; }
        public int iFK_MnuId { get; set; }
        public string sMenuName { get; set; }
        public string sSubMnuName { get; set; }

    }
    public class Dropdown
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string ID1 { get; set; }
        public string PartyId { get; set; }
        public string Qty { get; set; }
    }
    public class RateMaster
    {
        public long iPk_RateMstId { get; set; }
        public int iMnuid { get; set; }
        public int iSbMnu { get; set; }
        public string sMnuNme { get; set; }
        public string sSbMnuNme { get; set; }
        public decimal dRate { get; set; }
        public int iTyp { get; set; }
        public string dtInsertStrDt { get; set; }
        public string dtInsertEndDt { get; set; }
        public int iStts { get; set; }
        public int iClnId { get; set; }
    
    }
    public class RateMasterview:RateMaster
    {
        public DateTime? dtStrDt { get; set; }
        public DateTime? dtEndDt { get; set; }
        public string Typename
        {
            get
            {
                switch (iTyp)
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
        public string StartDate
        {
            get
            {
                if (!string.IsNullOrEmpty(dtStrDt.ToString()))
                {
                    return dtStrDt.Value.ToString("ddd dd MMM yyyy");
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
                if (!string.IsNullOrEmpty(dtEndDt.ToString()))
                {
                    return dtEndDt.Value.ToString("ddd dd MMM yyyy");
                }
                else
                {
                    return null;
                }
            }
        }
    }
    public class AddDepartment
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }
        public string PartyId { get; set; }
        public string Status { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Type { get; set; }
    }
    public class Activeclass
    {
        public string Tablename { get; set; }
        public int Id { get; set; }
        public int status { get; set; }
        public string Display { get; set; }
    }
    public class RoleMastertable
    {
        public int iPk_RolId { get; set; }
        public string sRolName { get; set; }
        public int iStatus { get; set; }
        public string sCrtdByPrtyCode { get; set; }

    }
    public class AddGroup
    {
        public int ID { get; set; }
        public int GroupID { get; set; }
        public string GroupName { get; set; }
        public int MenuID { get; set; }
        public string Menu { get; set; }
        public int SubmenuId { get; set; }
        public string Submenu { get; set; }
        public string Status { get; set; }
        public string PartyId { get; set; }
        public string CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public string Type { get; set; }
    }
    public class MappingRoleWithDepartmentandGroup
    {
        public int iPK_RoleDeptId { get; set; }
        public int iDeptId { get; set; }
        public int iGrpId { get; set; }
        public int iRoleId { get; set; }
        public int iStatus { get; set; }
        public string PartyId { get; set; }
        public string DepartmentName { get; set; }
        public string GroupName { get; set; }
        public string RoleName { get; set; }
    }
    public class UserPagingPermission
    {
        public int ID { get; set; }
        public string MenuName { get; set; }
        public string SubMenuName { get; set; }
        public int perInsert { get; set; }
        public int perDelete { get; set; }
        public int perEdit { get; set; }
        public int perView { get; set; }
        public int perStatus { get; set; }
        public int UserID { get; set; }
        public int GroupID { get; set; }
        public int? Inserting { get; set; }
        public int? Editing { get; set; }
        public int? Deleting { get; set; }
        public int? Viewing { get; set; }
        public int? Status { get; set; }
    }
    public class Permissionclass
    {
        public string Type { get; set; }
        public int MappingId { get; set; }
        public int MstGroupId { get; set; }
        public int PermissionId { get; set; }
        public int status { get; set; }
        public string PartyId { get; set; }
    }
}