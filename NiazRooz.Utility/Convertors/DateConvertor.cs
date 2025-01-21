using MD.PersianDateTime.Standard;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace NiazRooz.Utility
{
    public static class DateConvertor
    {
       
        public static string ToShamsi(this DateTime value)
        {
            PersianCalendar pc=new PersianCalendar();
            return pc.GetYear(value) + "/" + pc.GetMonth(value).ToString("00") + "/" +
                   pc.GetDayOfMonth(value).ToString("00");
        }
        public static string ToShamsiForChart(this DateTime value)
        {
            var persianDateTime = new PersianDateTime(value);

            return persianDateTime.ToString("MMMM") + "" + persianDateTime.ToString("yy");
        }
        public static DateTime ToMiladiDate(this DateTime dt)
        {
            PersianCalendar pc = new PersianCalendar();
            return pc.ToDateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, 0, 0);
        }

    }
}
