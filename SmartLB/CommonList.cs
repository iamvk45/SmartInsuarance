using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartLB
{
    public class CommonList
    {
        public static List<GlobalClass> GetFeatureList()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "SMS",
               
            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "What's up",
              
            });

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "Storage",
               
            });

            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "Notifiaction",
              
            });
            Lst.Add(new GlobalClass
            {
                Id = 5,
                Text = "User",

            });
            return Lst;

        }
        public static List<GlobalClass> GetUOMList()
        {
            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "Year",

            });

            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Month",

            });

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "Day",

            });

            return Lst;

        }
        public static List<GlobalClass> GetLicensename()
        {

            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "Agent License",

            });

         

            return Lst;
        }
        public static List<GlobalClass> GetUserType()
        {

            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "Agent",

            });


            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Broker",

            });

            Lst.Add(new GlobalClass
            {
                Id = 3,
                Text = "User",

            });
            Lst.Add(new GlobalClass
            {
                Id = 4,
                Text = "Trial User",

            });
            return Lst;
        }
        public static List<GlobalClass> GetRegistrationType()
        {

            List<GlobalClass> Lst = new List<GlobalClass>();

            Lst.Add(new GlobalClass
            {
                Id = 1,
                Text = "Single",

            });


            Lst.Add(new GlobalClass
            {
                Id = 2,
                Text = "Multiple",

            });

            return Lst;
        }
    }
}
