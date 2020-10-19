using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line_Production.Entities
{
    public class PassRate
    {
        public string ProductionID { get; set; }
        public string Line { get; set; }
        public string Time { get; set; }
        public int ProductPlan { get; set; }
        public int Actual { get; set; }
        public int IDCount { get; set; }
        public int IDCountBox { get; set; }
        public string BoxCurrent { get; set; }
        public double TimeCycleActual { get; set; }
        public int Id { get; set; }
        public string[] TimeValue { get; set; }
    }

    public static class PassRateString
    {
        public static string Id = "Id";
        public static string ProductionID = "ProductionID";
        public static string Line = "Line";
        public static string Time = "Time";
        public static string ProductPlan = "ProductPlan";
        public static string Actual = "Actual";
        public static string IDCount = "IDCount";
        public static string IDCountBox = "IDCountBox";
        public static string BoxCurrent = "BoxCurrent";
        public static string TimeCycleActual = "TimeCycleActual";
        public static string TimeValue = "TimeValue";
    }
}
