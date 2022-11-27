namespace SmartInsuarance.Helper
{
    public class Api_CommonResponse
    {
        public int statusCode { get; set; }
        public int responseCode { get; set; }
        public string message { get; set; }
        public object data { get; set; }
    }
}
