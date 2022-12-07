using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartInsuarance.Models
{
    public class PendingPayments
    {
        public int RowNo { get; set; }
        public string sOrderId { get; set; }
        public string dtTxnDt { get; set; }
        public string sPymntMode { get; set; }
        public string paymentMode { get; set; }
        public string iFk_UsrId { get; set; }
        public string iFk_UsrTyp { get; set; }
        public string Usertype { get; set; }
        public string dPrchsAmnt { get; set; }
        public string dTxnAmnt { get; set; }
        public string dTotalAmnt { get; set; }
        public int iFk_YearId { get; set; }
        public int iFk_PckgId { get; set; }
        public string PackageName { get; set; }

    }


    public class CollectPaymentData
    {
        public string orderID { get; set; }
        public string userName { get; set; }
        public string userID { get; set; }
        public string PackageName { get; set; }
        public string PurchaseCost { get; set; }
        public string TaxCost { get; set; }

    }
}