namespace WorkdayCalendarLib
{
    public class WorkdayCalendar
    {
        private TimeSpan _workdayStart = new TimeOfDay(8, 0);
        private TimeSpan _workdayEnd = new TimeOfDay(16, 0);
        private readonly List<Holiday> _holidays = new();
        
        public TimeOfDay WorkdayStart { set => _workdayStart = value; }
        public TimeOfDay WorkdayEnd { set => _workdayEnd = value; }

        public WorkdayCalendar AddHolidays(params Holiday[] holidays) {
            _holidays.AddRange(holidays);
            return this;
        }

        public void RemoveRecurringHoliday(Month month, int day) {
            var holidayToRemove = _holidays.OfType<RecurringHoliday>().FirstOrDefault(h => h.Month == month 
                && h.Day == day);
            if (holidayToRemove != null) {
                _holidays.Remove(holidayToRemove);
            }
        }

        public void RemoveSingleHoliday(int year, Month month, int day) {
            var holidayToRemove = _holidays.OfType<SingleHoliday>()
                .FirstOrDefault(h => h.Month == month && h.Day == day && h.Year == year);
            if(holidayToRemove != null) {
                _holidays.Remove(item: holidayToRemove);
            }
        }

        public DateTime AddWorkdays(DateTime dt, double workdays) {
            var workTime = workdays * WorkingDayLength;         // Calculate how much work time to move
            var pos = GetStart(dt);                             // Determine starting position, the start of next workday.
            
            if(workdays < 0) {
                return SubtractWorkdays(pos, -workTime);        // Same logic as below but reverse.
            }

            for(;;) 
            {
                var leftInDay = _workdayEnd - pos.TimeOfDay;    // Calc how much time left in current workday.
                var toConsume = Min(leftInDay, workTime);       // Determine how much to consume in this workday.
                pos += toConsume;                               // Move position the amount consumed in this workday.                       
                workTime -= toConsume;                          // Calculate how much more time remains to consume.
                
                if(workTime <= TimeSpan.Zero) {                 // Indicates we are done.
                    break;
                }

                pos = NextWorkday(pos);                         // Move to next workday, jumps weekends and holidays.
            };
 
            return pos;
        }


        
        private DateTime SubtractWorkdays(DateTime pos, TimeSpan workTime) {
            
            for(;;) 
            {
                var leftInDay = pos.TimeOfDay - _workdayStart;
                var toConsume = Min(leftInDay, workTime);
                pos -= toConsume;
                workTime -= toConsume;
                
                if(workTime <= TimeSpan.Zero) {
                    break;
                }

                pos = PrevWorkday(pos);
            };

            return pos;
        }

        internal DateTime NextWorkday(DateTime dt) {
            dt = DateTimeBuilder.New(dt).Hour(_workdayStart.Hours).Minute(_workdayStart.Minutes)
                .Second(_workdayStart.Seconds).Build();
            do {
                dt = dt.AddDays(1);
            } while(!IsWorkingDay(dt));
            return dt;
        }

        internal DateTime PrevWorkday(DateTime dt) {
            dt = DateTimeBuilder.New(dt).Hour(_workdayEnd.Hours).Minute(_workdayEnd.Minutes).Second(_workdayEnd.Seconds)
                .Build();
            do {
                dt = dt.AddDays(-1);
            } while(!IsWorkingDay(dt));
            return dt;
        }

        // GetStart returns the DateTime of next workday at work start.
        internal DateTime GetStart(DateTime dt) {
            var start = dt;
            if(!InWorkingHours(dt)) {
                start = new DateTimeBuilder(dt).Hour(_workdayStart.Hours).Minute(_workdayStart.Minutes)
                    .Second(_workdayStart.Seconds).Build();
                if(dt.TimeOfDay > _workdayEnd) {
                    start = start.AddDays(1);
                }
                while(!IsWorkingDay(start)) {
                    start = start.AddDays(1);
                    continue;
                }
            }
            return start;
        }

        private TimeSpan WorkingDayLength => _workdayEnd - _workdayStart;

        private TimeSpan Min(params TimeSpan[] timeSpans) {
            return timeSpans.Aggregate((min, next) => (next < min) ? next : min);
        }

        private bool InWorkingHours(DateTime dt) => IsWorkingDay(dt) && dt.TimeOfDay >= _workdayStart 
            && dt.TimeOfDay <= _workdayEnd;

        internal bool IsWorkingDay(DateTime dt) => !IsWeekend(dt) && !IsHoliday(dt);

        private bool IsWeekend(DateTime dt) => dt.DayOfWeek == DayOfWeek.Saturday || dt.DayOfWeek == DayOfWeek.Sunday;

        private bool IsHoliday(DateTime dt) => _holidays.Any(d => d.Match(dt));
    }       
}