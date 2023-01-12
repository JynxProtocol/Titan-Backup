using Nager.Date;
using System;

namespace DateTimeExtentions
{
    public static class DateTimeExtentions
    {
        public static DateTime AddWorkDays(this DateTime date, int days)
        {
            // The purpose of this function is to add or remove a given number of work days to/from a given date.
            // Work days are defined as non-holiday, non-weekend days.

            // Separate the number of days we want to move, and the direction to move in.
            // The direction is positive if we want to add work days, and negative if we want to remove work days.
            var Direction = Math.Sign(days);
            days = Math.Abs(days);

            // For every day we want to add/remove:
            while (days > 0)
            {
                // Move the new date forward/back one day.
                date = date.AddDays(Direction);

                // Check if the new date is a weekend day (Saturday or Sunday).
                var IsWeekend = date.DayOfWeek == DayOfWeek.Saturday
                    || date.DayOfWeek == DayOfWeek.Sunday;

                // Check if the new date is an official public holiday in England.
                // NOTE: We must specify England as the county, otherwise we only see UK-wide holidays.
                var IsHoliday = DateSystem
                    .IsOfficialPublicHolidayByCounty(date, CountryCode.GB, "GB-ENG");

                // Check if the new date is a work day (non-holiday and non-weekend).
                // NOTE: the old implementation only counted holidays when removing work days
                // unsure whether this behavior is required
                var IsWorkDay = !IsHoliday && !IsWeekend;

                // If the day isn't a work day, don't count it.
                if (!IsWorkDay)
                {
                    // Keep the number of work days we want to add/remove the same.
                }
                else
                {
                    // Decrease the number of work days we want to add/remove by one.
                    days--;
                }
            }

            // Return the final date after adding/removing the given number of work days.
            return date;
        }
    }
}
