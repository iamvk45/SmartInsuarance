using System.Collections.Generic;

namespace SmartInsuarance.Model
{
    public class FillDropDown
    {
        public int DDLValue { get; set; }
        public string DDLText { get; set; }
        public string DDLValue1 { get; set; }
    
    }
    public partial class GlobalClass
    {

        public int Id { get; set; }
        public string strId { get; set; }
        public string value { get; set; }
        public string label { get; set; }
        public string graphbackcolor { get; set; }
        public string Text { get; set; }
        public int RecordCount { get; set; }
        public string Pincode { get; set; }
    }
   public class CommonList
    {
        public static List<GlobalClass> GetTypeList()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "Day",
                graphbackcolor= "#edc4be"

            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Month",
                  graphbackcolor = "#e0edbe"
            });
            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "Year",
                graphbackcolor = "#afeddf"

            });

            return Lst;

        }
    }
}
