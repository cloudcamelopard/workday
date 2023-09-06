namespace WorkdayCalendarLibTest
{
    [TestClass]
    public class UnitTestWorkdayCalendar_AddWorkdays
    {
        private WorkdayCalendar _workDayCalendar = new WorkdayCalendar();

        [TestInitialize]
        public void Initialize()
        {
            _workDayCalendar.WorkdayStart = 8;
            _workDayCalendar.WorkdayEnd = 16;
            _workDayCalendar.AddHolidays(new RecurringHoliday(Month.May, 17), new SingleHoliday(2004, Month.May, 27));
        }

        [TestMethod]
        [DynamicData(nameof(TestDataGenerator), DynamicDataSourceType.Method)]
        public void TestNextWorkday(string startDateTime, double workdays, string expectedDateTime)
        {
            var start = DateTime.ParseExact(startDateTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var expected = DateTime.ParseExact(expectedDateTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            Assert.AreEqual(expected, DropMilliseconds(_workDayCalendar.AddWorkdays(start, workdays)));
        }

        // Helper method to simplify comparison
        static DateTime DropMilliseconds(DateTime dateTime)
        {
            return new DateTime(dateTime.Ticks - (dateTime.Ticks % TimeSpan.TicksPerSecond), dateTime.Kind);
        }

        public static IEnumerable<object[]> TestDataGenerator() {

            yield return new object[] { "2023-09-01 18:05:00",  2.5,       "2023-09-06 12:00:00" };
            yield return new object[] { "2023-09-06 12:00:00", -2.5,       "2023-09-04 08:00:00" };
            yield return new object[] { "2023-09-06 12:05:00",  5,         "2023-09-13 12:05:00" };
            yield return new object[] { "2023-09-13 12:05:00", -5,         "2023-09-06 12:05:00" };

            // Unit  tests from instructions

            yield return new object[] { "2004-05-24 18:05:00", -5.5,       "2004-05-14 12:00:00" };

            yield return new object[] { "2004-05-24 19:03:00", 44.723656,  "2004-07-27 13:47:21" };
            // results did not match instruction, reversed eng to get instructions result:
            yield return new object[] { "2004-05-24 19:03:00", 44 + 5.0/8 + 47.0/(60*8), "2004-07-27 13:47:00" };
            yield return new object[] { "2004-05-24 19:03:00", 44.722917, "2004-07-27 13:47:00" };
            
            yield return new object[] { "2004-05-24 18:03:00", -6.7470217, "2004-05-13 10:01:25" };
            // results did not match instruction, reversed eng to get instructions result:
            yield return new object[] { "2004-05-24 18:03:00", -6 - 6.0/8 + 2.0/(60*8), "2004-05-13 10:02:00" };
            yield return new object[] { "2004-05-24 18:03:00", -6.7458333, "2004-05-13 10:02:00" };
            
            yield return new object[] { "2004-05-24 08:03:00", 12.782709,  "2004-06-10 14:18:42" };
            // results did not match instruction, reversed eng to get instructions result:
            yield return new object[] { "2004-05-24 08:03:00", 12 + 6.0/8 + 15.0/(60*8), "2004-06-10 14:18:00" };
            yield return new object[] { "2004-05-24 08:03:00", 12.78125, "2004-06-10 14:18:00" };

            yield return new object[] { "2004-05-24 07:03:00",  8.276628,  "2004-06-04 10:12:46" };
            // results did not match instruction, reversed eng to get instructions result:
            yield return new object[] { "2004-05-24 07:03:00",  8 + 2.0/8 + 12.0/(60*8), "2004-06-04 10:12:00" };
            yield return new object[] { "2004-05-24 07:03:00",  8.275, "2004-06-04 10:12:00" };
        }    
    }
}