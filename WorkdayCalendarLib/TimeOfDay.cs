namespace WorkdayCalendarLib
{
    public class TimeOfDay {
        public int Hours { get; private set; }
        public int Minutes { get; private set; }

        public TimeOfDay(int hours, int minutes = 0) 
        {
            if (hours < 0 || hours > 23 || minutes < 0 || minutes > 59) {
                throw new ArgumentOutOfRangeException(string.Format("Hour {0} or minute {1} outside of valid range", hours, minutes));
            }
            Hours = hours;
            Minutes = minutes;
        }

        public static implicit operator TimeOfDay(int hour) {
            return new TimeOfDay(hour);
        }

        public static implicit operator TimeSpan(TimeOfDay tod) 
        {
            return new TimeSpan(tod.Hours, tod.Minutes, 0);    
        }
    }
}