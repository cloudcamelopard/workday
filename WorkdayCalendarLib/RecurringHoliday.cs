namespace WorkdayCalendarLib
{
    public class RecurringHoliday: Holiday
    {
        public Month Month { get; private set; }
        public int Day { get; private set; }

        public RecurringHoliday(Month month, int day, string name = ""): base(name) 
        {           
            (bool ok, string errorMsg) = MonthAndDay.IsValid(month, day);
            if(!ok) {
                throw new ArgumentOutOfRangeException(nameof(day), errorMsg); 
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