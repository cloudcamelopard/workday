using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WorkdayCalendarLibTest
{
    [TestClass]
    public class UnitTestWorkdayCalendar_AddRemoveHolidays
    {
        private WorkdayCalendar _workDayCalendar = new();

        [TestInitialize]
        public void Initialize()
        {
            _workDayCalendar.WorkdayStart = 8;
            _workDayCalendar.WorkdayEnd = 16;
            _workDayCalendar.AddHolidays(new RecurringHoliday(Month.May, 17), new SingleHoliday(2004, Month.May, 27));
        }

        [TestMethod]
        [DynamicData("SingleHolidayGenerator", DynamicDataSourceType.Method)]
        public void AddSingleHoliday(int year, Month month, int day)
        {
            _workDayCalendar.AddHolidays(new SingleHoliday(year, month, day));
            Assert.IsFalse(_workDayCalendar.IsWorkingDay(new DateTime(year,(int)month,day)));
        }

        [TestMethod]
        [DynamicData("RecurringHolidayGenerator", DynamicDataSourceType.Method)]
        public void AddRecurringHoliday(Month month, int day)
        {
            _workDayCalendar.AddHolidays(new RecurringHoliday(month, day));
            Assert.IsFalse(_workDayCalendar.IsWorkingDay(new DateTime(2000,(int)month,day)));
        }

        [TestMethod]
        public void RemoveSingleHoliday()
        {
            Assert.IsFalse(_workDayCalendar.IsWorkingDay(new DateTime(2004,5,27)));
            _workDayCalendar.RemoveSingleHoliday(2004, Month.May, 27);
            Assert.IsTrue(_workDayCalendar.IsWorkingDay(new DateTime(2004,5,27)));
        }

        [TestMethod]
        public void RemoveRecurringHoliday()
        {
            Assert.IsFalse(_workDayCalendar.IsWorkingDay(new DateTime(2000,5,17)));
            _workDayCalendar.RemoveRecurringHoliday(Month.May, 17);
            Assert.IsTrue(_workDayCalendar.IsWorkingDay(new DateTime(2000,5,17)));
        }

        public static IEnumerable<object[]> SingleHolidayGenerator() {
            yield return new object[] { 1970, Month.October, 6 };
            yield return new object[] { 1976, Month.Februar, 29 };
        }

        public static IEnumerable<object[]> RecurringHolidayGenerator() {
            yield return new object[] { Month.May, 17 };
            yield return new object[] { Month.Februar, 29 };
        }
    }
}