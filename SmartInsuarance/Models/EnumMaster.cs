using System.Collections.Generic;

namespace SmartInsuarance.Model
{
    public class EnumMaster
    {
        public int iPK_CustEnum { get; set; }
        public int iFK_EnumNo { get; set; }
        public string EnumName { get; set; }
        public string SettigName { get; set; }

    }
    public class InsuranceCheckout
    {
        public string PackageGst { get; set; }
        public int GstVal { get; set; }
        public string PackName { get; set; }
    }
}
