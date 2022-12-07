using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SmartInsuarance.Models
{
    public class PaymentTracking
    {
        public int iPk_PaymntId { get; set; }
        public string sTrackingId { get; set; }
        public string sUserID { get; set; }
        public decimal dPaidAmnt { get; set; }
        public int sPymntMode { get; set; }
        public int sPaidThrough { get; set; }
        public DateTime dtTxnDt { get; set; }
        public string sRefNo { get; set; }
        public string sCollectBy { get; set; }
        public int iFk_YearId { get; set; }
        public DateTime dtCrtdDt { get; set; }
        public string icrtBy { get; set; }

        public byte[] paymentReciept { get; set; }
    }
    public class PaymentHistory
    {
        public string iFk_UsrId { get; set; }
        public string dtTxnDt { get; set; }
        public decimal Paid { get; set; }
        public decimal Balance { get; set; }
      
    }
}