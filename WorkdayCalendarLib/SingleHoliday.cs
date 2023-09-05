namespace WorkdayCalendarLib
{
    public class SingleHoliday: RecurringHoliday 
    {
        public int Year { get; private set; }

        public SingleHoliday(int year, Month month, int day, string name = ""): base(month, day, name) 
        {
            if (year < 1 || year > 9999) {
                throw new ArgumentOutOfRangeException(nameof(year), "Parametet year must be between 1 and 9999.");
            }
            if (month == Month.Februar && !DateTime.IsLeapYear(year) && day == 29) 
            {
                day = 1; // reset day number to 1 if exception caught.
                throw new ArgumentOutOfRangeException(nameof(year), 
                    string.Format("Day number 29 only allowed for leap year, and {0} is not.", year));
            }
            Year = year;
        }

        public override bool Match(DateTime dt)
        {
            return base.Match(dt) && dt.Year == Year;
        }

        public override string ToString()
        {
            return Name != string.Empty ? string.Format("Single holiday '{0}' on {0} {1}, {2}.", Name, Month, Day, Year) 
                : string.Format("Recurring holiday on {0} {1}, {2}.", Month, Day, Year);
        }
    }

}