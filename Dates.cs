using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Messanger
{
    public class Dates
    {
        private static Dictionary<int, string> months = new Dictionary<int, string>()
        {
            {1,"Jan" },
            {2,"Feb" },
            {3,"Mar" },
            {4,"Apr" },
            {5,"May" },
            {6,"Jun" },
            {7,"Jul" },
            {8,"Aug" },
            {9,"Sep" },
            {10,"Oct" },
            {11,"Nov" },
            {12,"Dec" },
        };

        private static string GetMonth(int month)
        {
            return months[month];
        }
        public static string GetMMDD(DateTime time)
        {
            return GetMonth(time.Month) + " " + time.Day;
        }
        public static string GetHHMM(DateTime time)
        {
            return time.Hour + ":" + time.Minute;
        }
        public static string GetFullDate(DateTime time)
        {
            return GetMMDD(time) + " " + GetHHMM(time);
        }
    }
}