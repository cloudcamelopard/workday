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
        public void Test_GetStartMethod_WhenGivenDateTime_ShouldReturnCorrectDateTime(string inputDateTime, string expectedDateTime)
        {
            var input = DateTime.ParseExact(inputDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            var expected = DateTime.ParseExact(expectedDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);


            Assert.AreEqual(expected, _workDayCalendar.GetStart(input));
        }

        public static IEnumerable<object[]> TestDataGenerator() {
            yield return new object[] { "2023-09-01 07:00:00", "2023-09-01 08:00:00" };
            yield return new object[] { "2023-09-02 09:00:00", "2023-09-04 08:00:00" };
            yield return new object[] { "2023-02-28 18:00:00", "2023-03-01 08:00:00" };
            yield return new object[] { "2024-02-28 18:00:00", "2024-02-29 08:00:00" };
            yield return new object[] { "2024-12-31 18:00:00", "2025-01-01 08:00:00" };
        }
    }
}