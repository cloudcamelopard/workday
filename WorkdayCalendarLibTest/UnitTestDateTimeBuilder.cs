namespace WorkdayCalendarLibTest.TestResults
{
    [TestClass]
    public class UnitTestDateTimeBuilder
    {
        [TestMethod]
        [DynamicData(nameof(TestDataGenerator), DynamicDataSourceType.Method)]
        public void Test_BuildNewDate_AfterSetParts_ShouldGiveCorrectDate(string inputDateTime, Func<DateTimeBuilder, DateTimeBuilder> manip, string expectedDateTime)
        {
            var input = DateTime.ParseExact(inputDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);
            var expected = DateTime.ParseExact(expectedDateTime, "yyyy-MM-dd HH:mm:ss", CultureInfo.InvariantCulture);

            var dtb = new DateTimeBuilder(input);
            var ret = manip(dtb);

            Assert.AreEqual(expected, dtb.Build());
        }

        public static IEnumerable<object[]> TestDataGenerator() {
            yield return new object[] { "2023-04-01 12:00:00", (DateTimeBuilder dtb) => dtb.Year(2024), "2024-04-01 12:00:00" };
            yield return new object[] { "2023-04-01 12:00:00", (DateTimeBuilder dtb) => dtb.Month(Month.October), "2023-10-01 12:00:00" };
            yield return new object[] { "2023-04-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(10), "2023-04-10 12:00:00" };
            yield return new object[] { "2023-04-01 12:00:00", (DateTimeBuilder dtb) => dtb.Hour(14), "2023-04-01 14:00:00" };
            yield return new object[] { "2023-04-01 12:00:00", (DateTimeBuilder dtb) => dtb.Minute(22), "2023-04-01 12:22:00" };
            yield return new object[] { "2023-04-01 12:00:00", (DateTimeBuilder dtb) => dtb.Second(23), "2023-04-01 12:00:23" };

            yield return new object[] { "2023-01-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(31), "2023-01-31 12:00:00" };
            yield return new object[] { "2023-02-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(28), "2023-02-28 12:00:00" };
            yield return new object[] { "2024-02-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(29), "2024-02-29 12:00:00" };
            yield return new object[] { "2023-03-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(31), "2023-03-31 12:00:00" };
            yield return new object[] { "2023-04-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(30), "2023-04-30 12:00:00" };
            yield return new object[] { "2023-05-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(31), "2023-05-31 12:00:00" };
            yield return new object[] { "2023-06-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(30), "2023-06-30 12:00:00" };
            yield return new object[] { "2023-07-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(31), "2023-07-31 12:00:00" };
            yield return new object[] { "2023-08-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(31), "2023-08-31 12:00:00" };
            yield return new object[] { "2023-09-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(30), "2023-09-30 12:00:00" };
            yield return new object[] { "2023-10-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(31), "2023-10-31 12:00:00" };
            yield return new object[] { "2023-11-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(30), "2023-11-30 12:00:00" };
            yield return new object[] { "2023-12-01 12:00:00", (DateTimeBuilder dtb) => dtb.Day(31), "2023-12-31 12:00:00" };
        }
    }
}