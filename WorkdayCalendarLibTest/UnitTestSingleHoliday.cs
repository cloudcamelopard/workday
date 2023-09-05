using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WorkdayCalendarLibTest
{
    [TestClass]
    public class UnitTestRecuringHoliday
    {
        [TestMethod]
        [DynamicData(nameof(TestDataGenerator), DynamicDataSourceType.Method)]
        public void TestDates(int year, Month month, int day, bool ok) 
        {
            try {
                var rh = new SingleHoliday(year, month, day);
            }
            catch {
                if(ok) {
                    Assert.Fail("Exception thrown when setting month {0} and day {1} for {2}", month, day, year);
                }
                return;
            }
            if(!ok)
                Assert.Fail("ArgumentOutOfRangeException should be thrown for month {0} and day {1} for {2}", month, day, year); 
        }
        public static IEnumerable<object[]> TestDataGenerator() {

            Month[] longMonths = { Month.Januar, Month.March, Month.May, Month.July, Month.August, Month.October, Month.December };
            Month[] shortMonths = { Month.April, Month.June, Month.September, Month.November };

            foreach(Month month in longMonths) {
                yield return new object[] { 1970, month, -1, false };
                yield return new object[] { 1970, month, 0, false };
                yield return new object[] { 1970, month, 1, true };
                yield return new object[] { 1970, month, 15, true };
                yield return new object[] { 1970, month, 31, true };
                yield return new object[] { 1970, month, 32, false };    
            }

            foreach(Month month in shortMonths) {
                yield return new object[] { 1970, month, -1, false };
                yield return new object[] { 1970, month, 0, false };
                yield return new object[] { 1970, month, 1, true };
                yield return new object[] { 1970, month, 15, true };
                yield return new object[] { 1970, month, 30, true };
                yield return new object[] { 1970, month, 31, false };    
            }

            int nonLeapYear = 1970;
            int leapYear = 1976;

            yield return new object[] { nonLeapYear, Month.Februar, -1, false };
            yield return new object[] { nonLeapYear, Month.Februar, 0, false };
            yield return new object[] { nonLeapYear, Month.Februar, 1, true };
            yield return new object[] { nonLeapYear, Month.Februar, 15, true };
            yield return new object[] { nonLeapYear, Month.Februar, 29, false };
            yield return new object[] { leapYear, Month.Februar, 29, true };
            yield return new object[] { nonLeapYear, Month.Februar, 30, false };   
        }
    }
}