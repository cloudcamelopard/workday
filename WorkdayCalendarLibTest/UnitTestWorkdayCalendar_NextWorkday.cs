namespace WorkdayCalendarLibTest
{
    [TestClass]
    public class UnitTestWorkdayCalendar_NextWorkday
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
        //public void Test_NextWorkday_WhenGivenDateTime_ShouldReturnNextWorkdayDateTime(DateTime input, DateTime expected)
        public void Test_NextWorkday_WhenGivenDateTime_ShouldReturnNextWorkdayDateTime(string inputDateTime, string expectedDateTime)
        {
            var input = DateTime.ParseExact(inputDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            var expected = DateTime.ParseExact(expectedDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            Assert.AreEqual(expected, _workDayCalendar.NextWorkday(input));
        }

        public static IEnumerable<object[]> TestDataGenerator() {

            yield return new object[] { "2023-09-01 13:00:00", "2023-09-04 09:00:00" };
            yield return new object[] { "2023-09-04 09:00:00", "2023-09-05 09:00:00" };
            yield return new object[] { "2023-05-16 09:30:00", "2023-05-18 09:00:00" };
        } 
    }
}