namespace WorkdayCalendarLibTest
{
    [TestClass]
    public class UnitTestTimeOfDay
    {
        [TestMethod]
        [DynamicData(nameof(TestDataGenerator), DynamicDataSourceType.Method)]
        public void Test_CreateTimeOfDay_WhenGivenHourMinute_ShouldThrowExceptionWhenInvalidInput(int hour, int minute, bool ok) 
        {
            try {
                var hm = new TimeOfDay(hour, minute);
            }
            catch {
                if(ok) {
                    Assert.Fail("Exception thrown when setting minute {0} on hour {1}", minute, hour);
                }
                return;
            }
            if(!ok)
                Assert.Fail("ArgumentOutOfRangeException should be thrown for hour {0} and minute {1}", hour, minute); 
        }
        public static IEnumerable<object[]> TestDataGenerator() {
            yield return new object[] { -1, -1, false };
            yield return new object[] { -1, 0, false };
            yield return new object[] { 0, -1, false };
            yield return new object[] { 24, 61, false };
            yield return new object[] { 24, 0, false };
            yield return new object[] { 0, 60, false };
            yield return new object[] { 0, 0, true };
            yield return new object[] { 1, 0, true };
            yield return new object[] { 10, 0, true };
            yield return new object[] { 23, 0, true };
            yield return new object[] { 0, 1, true };
            yield return new object[] { 0, 10, true };
            yield return new object[] { 0, 59, true };
            yield return new object[] { 1, 1, true };
            yield return new object[] { 10, 10, true };
            yield return new object[] { 23, 59, true };
            
        }

        public static IEnumerable<object[]> AllValidTimesGenerator() {
            for(var hour = 0; hour <= 23; hour++) {
                for (var minute = 0; minute <= 59; minute++) {
                    yield return new object[] { hour, minute };
                }
            }
        }
    }
}