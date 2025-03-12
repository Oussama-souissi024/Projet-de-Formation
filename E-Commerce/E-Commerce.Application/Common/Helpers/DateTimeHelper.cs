namespace E_Commerce.Application.Common.Helpers
{
    public static class DateTimeHelper
    {
        public static DateTime GetCurrentUtcTime()
        {
            return DateTime.UtcNow;
        }

        public static bool IsValidFutureDate(DateTime date)
        {
            return date > DateTime.UtcNow;
        }

        public static string FormatDateTime(DateTime date, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return date.ToString(format);
        }
    }
}
