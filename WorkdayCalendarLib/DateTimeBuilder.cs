namespace WorkdayCalendarLib
{
    public class DateTimeBuilder
    {
        private int _year, _month, _day, _hour, _minute, _second;

        public static DateTimeBuilder New(DateTime dt) {
            return new DateTimeBuilder(dt);
        }

        public DateTimeBuilder(DateTime dt) {
            _year = dt.Year;
            _month = dt.Month;
            _day = dt.Day;
            _hour = dt.Hour;
            _minute = dt.Minute;
            _second = dt.Second;
        }

        // This might throw exception if any of the parts have an invalid value.
        public DateTime Build() {
            if (_day < 1 || _day > 31) {
                throw new Exception(string.Format("Day can not be {0} for any month.", _day));
            }
            if (_month == 2 && _day == 29 && !DateTime.IsLeapYear(_year)) {
                throw new Exception(string.Format("Day can not be {0} for Februar for non leap year {1}.", _day, _year));
            }
            if (_month == 2 && _day > 29) {
                throw new Exception(string.Format("Day can not be {0} for Februar", _day));
            }
            if ((_month == 4 || _month == 6 || _month == 9 || _month == 12) && _day == 31) {
                throw new Exception(string.Format("Day can not be {0} for April, June, September or November.", _day));
            }
            
            return new DateTime(_year, _month, _day, _hour, _minute, _second); 
        }

        public DateTimeBuilder Year(int year) 
        {
            if(year < 1 || year > 9999) 
            {
                throw new ArgumentOutOfRangeException(nameof(year), "Must be within 1 and 9999");
            }
            _year = year;
            return this;
        }

        public DateTimeBuilder Month(Month month)
        {
            _month = (int)month;
            return this;
        }

        public DateTimeBuilder Day(int day)
        {
            if(day < 1 || day > 31) 
            {
                throw new ArgumentOutOfRangeException(nameof(day), "Must be within 1 and 31");
            }
            _day = day;
            return this;
        }

        public DateTimeBuilder Hour(int hour) 
        {
            if(hour < 0 || hour > 23) 
            {
                throw new ArgumentOutOfRangeException(nameof(hour), "Must be within 0 and 23");
            }
            _hour = hour;
            return this;
        }

        public DateTimeBuilder Minute(int minute) 
        {
            if(minute < 0 || minute > 59) 
            {
                throw new ArgumentOutOfRangeException(nameof(minute), "Must be within 0 and 59");
            }
            _minute = minute;
            return this;
        }

        public DateTimeBuilder Second(int second) {
            if(second < 0 || second > 59) 
            {
                throw new ArgumentOutOfRangeException(nameof(second), "Must be within 0 and 59");
            }
            _second = second;
            return this;
        }
     }
}