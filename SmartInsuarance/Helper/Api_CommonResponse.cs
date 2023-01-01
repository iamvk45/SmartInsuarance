namespace SmartInsuarance.Helper
{
    public class Api_CommonResponse
    {
        public int statusCode { get; set; }
        public int responseCode { get; set; }
        public string message { get; set; }
        public object data { get; set; }
        public int status { get; set; }
        public int mobileOTP { get; set; }
        public int emailOTP { get; set; }
        public string userID { get; set; }
        public string userCode { get; set; }
    }
}
