# Workday repo
This repo contains a .NET solution with three .Net 7 project. One library project, one console project and one testproject.

## WorkdayCalendarLib

### WorkdayCalendar class
This class is used to set up holidays and then to calculate workdays.
#### Public properties and methods
**public  TimeOfDay  WorkdayStart [set]**  
Property use to set the start of a working day. The TimeOfDay has an override  so it is possible to set it to an int. 
  
**public  TimeOfDay  WorkdayEnd[set]**  
Property use to set the end of a working day. The TimeOfDay has an override  so it is possible to set it to an int.  
  
**public  WorkdayCalendar  AddHolidays(params  Holiday[] holidays)**  
This method is used to add holidays to the calendar.  
  
**public  void  RemoveRecurringHoliday(Month  month, int  day)**  
Method to remove recurring holidays from the calendar  
  
**public  void  RemoveSingleHoliday(int  year, Month  month, int  day)**  
Method to remove single holidays from the calendar  
  
**public  DateTime  AddWorkdays(DateTime  dt, double  workdays)**  
Method used to navigate workdays in the calendar.  

#### Example how to use
To set up a WorkCalendar you:
```
var calendar = new WorkdayCalendar();
calendar.WorkdayStart = new TimeOfDay(8, 0);
calendar.WorkdayEnd = new  TimeOfDay(16, 0);
calendar.AddHolidays(new RecurringHoliday(Month.May, 17));
calendar.AddHolidays(new SingleHoliday(2004, Month.May, 27));
```
This can be written a bit compacter:
```
var calendar = new WorkdayCalendar() {
	WorkdayStart = 8, WorkdayEnd = 16
}.AddHolidays(new RecurringHoliday(Month.May, 17), new SingleHoliday(2004, Month.May, 27));
```
Here is complete code to test the WorkdayCalendar in a simple console:
```
using  WorkdayCalendarLib;

class  Program
{
	static void Main()
	{
		var  calendar = new  WorkdayCalendar() {
			WorkdayStart = 8, WorkdayEnd = 16,
		}.AddHolidays(new RecurringHoliday(Month.May, 17), new SingleHoliday(2004, Month.May, 27));

		var start = new DateTime(2004, 5, 24, 18, 5, 0);
		var move = -5.5;
		var result = calendar.AddWorkdays(start, move);
 
		Console.WriteLine("From {0} and move {1} workdays you end up on {2}", start, move, result);
	}
}
``` 

## WorkdayCalendarLibTest
Contains unit tests for WorkdayCalendarLib. To run the tests:

> C:\repos\workday>  ***dotnet test WorkdayCalendarLibTest/WorkdayCalendarLibTest.csproj***

## ConsoleWorkdayCalendar
A console program where you can enter dates and workdays to move and get the result.
To run the code standing in the workday folder, bold italic indicate user entry.

> C:\repos\workday>  ***dotnet run --project ConsoleWorkdays/ConsoleWorkdays.csproj***  
Enter a date and days (YYYY-MM-dd HH:mm:ss d) or 'q' to quit: ***2004-05-24 18:05:00 -5.5***  
Resulting date and time: 2004-05-14 12:00:00  
Enter a date and days (YYYY-MM-dd HH:mm:ss d) or 'q' to quit: ***q***  
Program terminated.  
