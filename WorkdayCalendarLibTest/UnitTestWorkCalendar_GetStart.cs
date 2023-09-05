using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WorkdayCalendarLibTest
{
    [TestClass]
    public class UnitTestWorkCalendar_GetStart
    {
        private WorkdayCalendar _workDayCalendar = new WorkdayCalendar();

        [TestInitialize]
        public void Initialize()
        {
            _workDayCalendar.AddHolidays(new RecurringHoliday(Month.May, 17), new SingleHoliday(2023, Month.October, 6));
        }

        [TestMethod]
        [DynamicData(nameof(TestDataGenerator), DynamicDataSourceType.Method)]
        public void TestGetStart(DateTime input, DateTime expectedResult)
        {
            Assert.AreEqual(expectedResult, _workDayCalendar.GetStart(input));
        }

        public static IEnumerable<object[]> TestDataGenerator() {
            yield return new object[] { new DateTime(2023,9,1,7,0,0), new DateTime(2023,9,1,8,0,0) };
            yield return new object[] { new DateTime(2023,9,2,9,0,0), new DateTime(2023,9,4,8,0,0) };
        }
    }
}