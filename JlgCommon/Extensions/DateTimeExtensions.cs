using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JlgCommon.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToMonthYearString(this DateTime dateTime)
        {
            var dateFormatInfo = new DateTimeFormatInfo();
            return String.Format("{0} {1}", dateFormatInfo.GetAbbreviatedMonthName(dateTime.Month), dateTime.Year);
        }

        public static string ToMonthNameDayYear(this DateTime dateTime)
        {
            var dateFormatInfo = new DateTimeFormatInfo();
            return String.Format("{0} {1} {2}", dateFormatInfo.GetAbbreviatedMonthName(dateTime.Month), dateTime.Day, dateTime.Year);
        }

        public static DateTime ToDateIgnoringTime(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, dateTime.Day);
        }

        public static DateTime ToMonthYear(this DateTime dateTime)
        {
            return new DateTime(dateTime.Year, dateTime.Month, 1);
        }

        public static int DifferenceInMonths(this DateTime currentDateTime, DateTime otherDateTime)
        {
            currentDateTime = currentDateTime.ToMonthYear();
            otherDateTime = otherDateTime.ToMonthYear();
            
            bool otherIsGreater = false;
            if (currentDateTime < otherDateTime)
            {
                otherIsGreater = true;
            }

            DateTime start;
            DateTime end;

            if (otherIsGreater)
            {
                start = currentDateTime;
                end = otherDateTime;
            }
            else
            {
                start = otherDateTime;
                end = currentDateTime;
            }

            int difference = 0;
            while (start < end)
            {
                start = start.AddMonths(1);
                difference++;
            }

            if (otherIsGreater)
            {
                return -difference;
            }
            else
            {
                return difference;
            }
        }
    }
}
