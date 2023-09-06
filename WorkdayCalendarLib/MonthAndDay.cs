namespace WorkdayCalendarLib
{
    public static class MonthAndDay
    {
        public static (bool, string) IsValid(int month, int day) {
            if (day < 1 || day > 31) {
                return (false, "Day can not be {0} for any month.");
            }
            if (month == 2 && day > 29) {
                return (false, string.Format("Day can not be {0} for Februar", day));
            }
            if ((month == 4 || month == 6 || month == 9 || month == 11) && day == 31) {
                return (false, string.Format("Day can not be {0} for April, June, September or November.", day));
            }
            return (true, "");
        }

        public static (bool, string) IsValid(Month month, int day) {
            return IsValid((int)month, day);
        }

        public static (bool, string) IsValid(int year, int month, int day) {
            (var ok, var msg) = IsValid(month, day);
            if(!ok) {
                return (false, msg);
            }

            if (month == 2 && day == 29 && !DateTime.IsLeapYear(year)) {
                return (false, string.Format("Day can not be {0} for Februar for non leap year {1}.", day, year));
            }
            return (true, "");
        }

        public static (bool, string) IsValid(int year, Month month, int day) {
            return IsValid(year, (int)month, day);
        }
    }
}