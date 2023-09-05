using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WorkdayCalendarLibTest
{
    [TestClass]
    public class UnitTestWorkCalendar_IsWorkingDay
    {
        private WorkdayCalendar _workDayCalendar = new WorkdayCalendar();

        [TestInitialize]
        public void Initialize()
        {
            _workDayCalendar.AddHolidays(new RecurringHoliday(Month.May, 17), new SingleHoliday(2023, Month.October, 6));
        }

        [TestMethod]
        [DynamicData(nameof(TestDataGenerator), DynamicDataSourceType.Method)]
        public void TestIsWorkingDay(DateTime dt, bool expectedResult)
        {
            bool res = _workDayCalendar.IsWorkingDay(dt);
            Assert.AreEqual(res, expectedResult);
        }

        public static IEnumerable<object[]> TestDataGenerator() {
            yield return new object[] { new DateTime(2022,5,17), false };
            yield return new object[] { new DateTime(2023,5,17), false };
            yield return new object[] { new DateTime(2023,9,1), true };
            yield return new object[] { new DateTime(2023,9,2), false };
            yield return new object[] { new DateTime(2023,9,4), true };
            yield return new object[] { new DateTime(2022,10,6), true };
            yield return new object[] { new DateTime(2023,10,6), false };
            yield return new object[] { new DateTime(2023,11,6), true };
        }
    }
}