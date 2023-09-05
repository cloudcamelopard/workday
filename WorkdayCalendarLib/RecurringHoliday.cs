namespace WorkdayCalendarLib
{
    public class RecurringHoliday: Holiday
    {
        public Month Month { get; private set; }
        public int Day { get; private set; }

        public RecurringHoliday(Month month, int day, string name = ""): base(name) {           
            switch(month) {
                case Month.Februar:
                    if(day < 1 || day > 29) {
                        throw new ArgumentOutOfRangeException(nameof(day), "Day must be between 1 and 29 for Februar.");
                    }
                    break;
                case Month.April: case Month.June: case Month.September: case Month.November:
                    if(day < 1 || day > 30) {
                        throw new ArgumentOutOfRangeException(nameof(day), 
                            "Day must be between 1 and 30 for April, June, September and November.");
                    }
                    break;
                default:
                    if(day < 1 || day > 31) {
                        throw new ArgumentOutOfRangeException(nameof(day), 
                            "Day must be between 1 and 31 for Januar, March, May, July, August, October and December.");
                    }
                    break;
            }
            Month = month;
            Day = day;
        }

        public override bool Match(DateTime dt)
        {
            return dt.Month == (int)Month && dt.Day == Day;
        }

        public override string ToString()
        {
            return Name != string.Empty ? string.Format("Recurring holiday '{0}' on {0} {1}", Name, Month, Day) 
                : string.Format("Recurring holiday on {0} {1}.", Month, Day);
        }
    }

}