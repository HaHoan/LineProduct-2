using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line_Production
{
    public static class Constants
    {
        public static string BaudRate = "9600";
        public static string DataBits = "8";
        public static string Parity = "None";
        public static string StopBits = "1";

    }
    public static class ConstantsText
    {
        public static string NG = "NG";
        public static string OK = "OK";
    }
    public static class RegistryKeys
    {
        public static string id = "id";
        public static string useWip = "useWip";
        public static string pathWip = "pathWip";
        public static string station = "station";
        public static string COM = "COM";
        public static string LinkWip = "LinkWip";
        public static string Customer = "Customer";
        public static string Process = "Process";
    }
    public static class CaSX
    {
        public static int DAY = 0;
        public static int NIGHT = 1;
    }
    public static class CUSTOMER
    {

        public static Dictionary<string, string> CUSTOMERS = new Dictionary<string, string>()
        {
            {"CS002","TOYO" },
            {"CS005","CANON" },
            {"CS015","SE" },
            {"CS022","FUJI" },
            {"CS023","HONDALOCK" },
            {"CS029","NICHICON-JP" },
            {"CS038","VALEO" },
            {"CS045","YASKAWA" },
            {"CS048","ICHIKOH" }
        };
    }
   
}
