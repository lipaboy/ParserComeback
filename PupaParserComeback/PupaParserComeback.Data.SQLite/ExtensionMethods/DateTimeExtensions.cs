using System;

namespace PupaParserComeback.Data.SQLite.ExtensionMethods
{
    public static class DateTimeExtensions
    {
        public static DateTime? GetDateTime(this string dateTime)
        {
            if (string.IsNullOrWhiteSpace(dateTime)) return null;

            DateTime date;
            return DateTime.TryParse(dateTime, out date) 
                ? date 
                : (DateTime?)null;
        }
    }
}
