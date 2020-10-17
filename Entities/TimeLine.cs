using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Line_Production.Entities
{
    public class TimeLine
    {
        public int Id { get; set; }
        public int IdTimeLine { get; set; }
        public int TimeIndex { get; set; }
        public string TimeFrom { get; set; }
        public string TimeTo { get; set; }
    }
    public static class TimeLineString
    {
        public static string Id = "Id";
        public static string IdTimeLine = "IdTimeLine";
        public static string TimeIndex = "TimeIndex";
        public static string TimeFrom = "TimeFrom";
        public static string TimeTo = "TimeTo";
        public static string Line = "Line";
        public static string total = "total";

    }
}
