using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace SW.Framework.Extensions
{
    public static class DateAndTimeExtentions
    {
        public static string TimeFormat(this DateTime dateTime)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", dateTime.Hour, dateTime.Minute, dateTime.Second);
        }

        public static string TimeFormat(this TimeSpan span)
        {
            return string.Format("{0:00}:{1:00}:{2:00}", span.Hours, span.Minutes, span.Seconds);
        }

        public static string LocalizedTimeFormat(this DateTime dateTime, CultureInfo cultureInfo)
        {
            return dateTime.ToString("hh:mm tt", cultureInfo);
        }

        public static string LocalizedTimeFormat(this TimeSpan timeSpan, CultureInfo cultureInfo)
        {
            string formattedTimeSpan = timeSpan.ToString(@"hh\:mm\:ss");
            string timeSeparator = cultureInfo.DateTimeFormat.TimeSeparator;
            return formattedTimeSpan.Replace(":", timeSeparator);
        }
    }
}
