using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace BlogDemo2
{
    public static class Extensions
    {
        public static string ToConfigLocalTime(this DateTime utcDt)
        {
            var istTz = TimeZoneInfo.FindSystemTimeZoneById(ConfigurationManager.AppSettings["Timezone"]);
            return
                $"{TimeZoneInfo.ConvertTimeFromUtc(utcDt, istTz).ToShortDateString()}";
        }
    }
}