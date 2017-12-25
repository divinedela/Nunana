using Nunana.DTOs;
using System;
using System.Globalization;

namespace Nunana.Extensions
{
    public static class DateTimeExtensions
    {
        public static string GetDifferenceInMonths(DateTime startDate, DateTime endDate)
        {
            var difference = endDate.Subtract(startDate).Days / (365.25 / 12);
            return Math.Round(difference, 0).ToString();
        }

        public static DateTime GetRentalEndDate(SaveRentalDto saveRentalDto, DateTime startDate)
        {
            var numberOfMonths = saveRentalDto.Months;
            var endDate = startDate.AddMonths(numberOfMonths);
            return endDate;
        }

        public static DateTime ConvertToDateTime(string dateString)
        {
            return DateTime.ParseExact(dateString,
                "ddd MMM dd yyyy HH:mm:ss 'GMT'K '(GMT Standard Time)'",
                CultureInfo.InvariantCulture);
        }

        public static DateTime CalculateLastDayOfThisMonth(DateTime firstDayOfMonth)
        {
            var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddSeconds(-1);
            return lastDayOfMonth;
        }

        public static DateTime CalculateFirstDayOfThisMonth()
        {
            var date = DateTime.Today;

            var firstDayOfMonth = new DateTime(date.Year, date.Month, 1);
            return firstDayOfMonth;
        }
    }
}