using System;
namespace ConsoleWorkdays
{
    using WorkdayCalendarLib;

    class Program 
    {
        static void Main() {

            var wdc = new WorkdayCalendar
            {
                WorkdayStart = new TimeOfDay(8, 0),
                WorkdayEnd = new TimeOfDay(16, 0)
            }
            .AddHolidays(new RecurringHoliday(Month.May, 17), new SingleHoliday(2004, Month.May, 27));
            
            DateTime start = new DateTime(2004, 5, 24, 18, 5, 0);
            DateTimeBuilder builder = new DateTimeBuilder(start);
            
            var res = wdc.AddWorkdays(builder.Build(), -5.5);
            Console.WriteLine(res);
            res = wdc.AddWorkdays(builder.Hour(19).Minute(3).Build(), 44.723656);
            Console.WriteLine(res);
            res = wdc.AddWorkdays(builder.Hour(18).Build(), -6.7470217);
            Console.WriteLine(res);
            res = wdc.AddWorkdays(builder.Hour(8).Build(), 12.782709);
            Console.WriteLine(res);
            res = wdc.AddWorkdays(builder.Hour(7).Build(), 8.276628);
            Console.WriteLine(res);
        }
    }
    
}
