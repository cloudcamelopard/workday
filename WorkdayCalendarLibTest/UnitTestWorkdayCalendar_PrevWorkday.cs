using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WorkdayCalendarLibTest
{
    [TestClass]
    public class UnitTestWorkdayCalendar_PrevWorkday
    {
        private WorkdayCalendar _workDayCalendar = new WorkdayCalendar();

        [TestInitialize]
        public void Initialize()
        {
            _workDayCalendar.WorkdayStart = 9;
            _workDayCalendar.WorkdayEnd = 19;
            _workDayCalendar.AddHolidays(new RecurringHoliday(Month.May, 17), new SingleHoliday(2023, Month.October, 6));
        }

        [TestMethod]
        [DynamicData(nameof(TestDataGenerator), DynamicDataSourceType.Method)]
        public void TestPrevWorkday(DateTime input, DateTime expected)
        {
            Assert.AreEqual(_workDayCalendar.PrevWorkday(input), expected);
        }

        public static IEnumerable<object[]> TestDataGenerator() {
            DateTimeBuilder dtb = new(new DateTime(2023, 9, 4, 13, 0, 0));
            yield return new object[] { dtb.Build(), dtb.Day(1).Hour(19).Build() };
            yield return new object[] { dtb.Build(), dtb.Month(Month.August).Day(31).Build() };
        }
    }
}