using System;

namespace StructuredLogging.Extensions
{
    static class DateTimeExtensions
    {
        public static DateTime FromUnixTimeStamp(this double unixTimeStamp)
        {
            DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            dateTime = dateTime.AddSeconds(unixTimeStamp).ToLocalTime();

            return dateTime;
        }

        public static double ToUnixTimeStamp(this DateTime dateTime)
        {
            double seconds = (TimeZoneInfo.ConvertTimeToUtc(dateTime) - new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds;
            return Math.Round(seconds, 0);
        }
    }
}