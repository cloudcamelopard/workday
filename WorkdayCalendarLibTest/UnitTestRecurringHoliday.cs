namespace WorkdayCalendarLibTest.TestResults
{
    [TestClass]
    public class UnitTestRecuringHoliday 
    {
        [TestMethod]
        [DynamicData(nameof(TestDataGenerator), DynamicDataSourceType.Method)]
        public void Test_CreateRecurringHoliday_WhenGivenMonthDay_ShouldThrowExceptionWhenInvalidInput(Month month, int day, bool ok) 
        {
            try {
                var rh = new RecurringHoliday(month, day);
            }
            catch {
                if(ok) {
                    Assert.Fail("Exception thrown when setting month {0} and day {1}", month, day);
                }
                return;
            }
            if(!ok)
                Assert.Fail("ArgumentOutOfRangeException should be thrown for month {0} and day {1}", month, day); 
        }
        public static IEnumerable<object[]> TestDataGenerator() {

            Month[] longMonths = { Month.Januar, Month.March, Month.May, Month.July, Month.August, Month.October, Month.December };
            Month[] shortMonths = { Month.April, Month.June, Month.September, Month.November };

            foreach(Month month in longMonths) {
                yield return new object[] { month, -1, false };
                yield return new object[] { month, 0, false };
                yield return new object[] { month, 1, true };
                yield return new object[] { month, 15, true };
                yield return new object[] { month, 31, true };
                yield return new object[] { month, 32, false };    
            }

            foreach(Month month in shortMonths) {
                yield return new object[] { month, -1, false };
                yield return new object[] { month, 0, false };
                yield return new object[] { month, 1, true };
                yield return new object[] { month, 15, true };
                yield return new object[] { month, 30, true };
                yield return new object[] { month, 31, false };    
            }

            yield return new object[] { Month.Februar, -1, false };
            yield return new object[] { Month.Februar, 0, false };
            yield return new object[] { Month.Februar, 1, true };
            yield return new object[] { Month.Februar, 15, true };
            yield return new object[] { Month.Februar, 29, true };
            yield return new object[] { Month.Februar, 30, false };

            
            
        }
    }
}