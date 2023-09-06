using System;
using System.Globalization;
using WorkdayCalendarLib;

class Program
{
    static void Main()
    {
        // Create the calendar, set up workday and holidays.
        var calendar = new WorkdayCalendar() {
            WorkdayStart = 8,
            WorkdayEnd = 16,
        }.AddHolidays(new RecurringHoliday(Month.May, 17), new SingleHoliday(2004, Month.May, 27));

        while (true)
        {
            Console.Write("Enter a date and days (YYYY-MM-dd HH:mm:ss d) or 'q' to quit: ");
            string input = Console.ReadLine() ?? "";

            if (input.ToLower() == "q")
            {
                break; // To terminate
            }

            string[] parts = input.Split(' ');

            if (parts.Length == 3)
            {
                var startTimeInput = string.Concat(parts[0], " ", parts[1]); // Space between YYYY-MM-dd and HH:mm:ss
                var daysToAddInput = parts[2];

                if(double.TryParse(daysToAddInput, NumberStyles.Float, CultureInfo.InvariantCulture, out double daysToAdd) &&
                    DateTime.TryParseExact(startTimeInput, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture, 
                        DateTimeStyles.None, out DateTime startTime))
                
                {
                    var result = calendar.AddWorkdays(startTime, daysToAdd);
                    Console.WriteLine("Resulting date and time: {0}", result);
                }
            }   
            else
            {
                Console.WriteLine("Invalid input format. Please use 'YYYY-MM-dd HH:mm:ss days'.");
            }
        }

        Console.WriteLine("Program terminated.");        
    }
    
}
