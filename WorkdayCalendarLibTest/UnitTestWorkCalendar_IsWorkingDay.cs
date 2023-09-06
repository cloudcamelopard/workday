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
        public void Test_IsWorkingDay_WhenGivenDateTime_ShouldReturnIfWorkingDay(string inputDateTime, bool expectedResult)
        {
            var input = DateTime.ParseExact(inputDateTime, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            bool res = _workDayCalendar.IsWorkingDay(input);
            Assert.AreEqual(expectedResult, res);
        }

        public static IEnumerable<object[]> TestDataGenerator() {
            yield return new object[] { "2022-05-17", false };
            yield return new object[] { "2023-05-17", false };
            yield return new object[] { "2023-09-01", true };
            yield return new object[] { "2023-09-02", false };
            yield return new object[] { "2023-09-04", true };
            yield return new object[] { "2022-10-06", true };
            yield return new object[] { "2023-10-06", false };
            yield return new object[] { "2023-11-06", true };
        }
    }
}