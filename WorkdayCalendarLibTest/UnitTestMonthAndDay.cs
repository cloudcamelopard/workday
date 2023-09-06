namespace WorkdayCalendarLibTest
{
    [TestClass]
    public class UnitTestMonthAndDay
    {
        [TestMethod]
        [DynamicData(nameof(GenerateValidYearMonthAndDay), DynamicDataSourceType.Method)]
        public void Test_Validate_WhenGivenValidYearMonthAndDay_ShouldReturnTrue(int year, Month month, int day)
        {
            (bool ok, string _) = MonthAndDay.IsValid(year, month, day);
            Assert.IsTrue(ok);
        }

        [TestMethod]
        [DynamicData(nameof(GenerateInvalidYearMonthAndDay), DynamicDataSourceType.Method)]
        public void Test_Validate_WhenGivenInvalidYearMonthAndDay_ShouldReturnFalse(int year, Month month, int day)
        {
            (bool ok, string _) = MonthAndDay.IsValid(year, month, day);
            Assert.IsFalse(ok);
        }

        public static IEnumerable<object[]> GenerateValidYearMonthAndDay() {
            yield return new object[] { 2003, Month.Januar, 1 };
            yield return new object[] { 2003, Month.Januar, 31 };
            yield return new object[] { 2003, Month.Februar, 1 };
            yield return new object[] { 2003, Month.Februar, 28 };
            yield return new object[] { 2004, Month.Februar, 29 };
            yield return new object[] { 2003, Month.March, 1 };
            yield return new object[] { 2003, Month.March, 31 };
            yield return new object[] { 2003, Month.April, 1 };
            yield return new object[] { 2003, Month.April, 30 };
            yield return new object[] { 2003, Month.May, 1 };
            yield return new object[] { 2003, Month.May, 31 };
            yield return new object[] { 2003, Month.June, 1 };
            yield return new object[] { 2003, Month.June, 30 };
            yield return new object[] { 2003, Month.July, 1 };
            yield return new object[] { 2003, Month.July, 31 };
            yield return new object[] { 2003, Month.August, 1 };
            yield return new object[] { 2003, Month.August, 31 };
            yield return new object[] { 2003, Month.September, 1 };
            yield return new object[] { 2003, Month.September, 30 };
            yield return new object[] { 2003, Month.October, 1 };
            yield return new object[] { 2003, Month.October, 31 };
            yield return new object[] { 2003, Month.November, 1 };
            yield return new object[] { 2003, Month.November, 30 };
            yield return new object[] { 2003, Month.December, 1 };
            yield return new object[] { 2003, Month.December, 31 };
        }

        public static IEnumerable<object[]> GenerateInvalidYearMonthAndDay() {
            yield return new object[] { 2003, Month.Januar, 0 };
            yield return new object[] { 2003, Month.Januar, 32 };
            yield return new object[] { 2003, Month.Februar, 0 };
            yield return new object[] { 2003, Month.Februar, 29 };
            yield return new object[] { 2004, Month.Februar, 30 };
            yield return new object[] { 2003, Month.March, 0 };
            yield return new object[] { 2003, Month.March, 32 };
            yield return new object[] { 2003, Month.April, 0 };
            yield return new object[] { 2003, Month.April, 31 };
            yield return new object[] { 2003, Month.May, 0 };
            yield return new object[] { 2003, Month.May, 32 };
            yield return new object[] { 2003, Month.June, 0 };
            yield return new object[] { 2003, Month.June, 31 };
            yield return new object[] { 2003, Month.July, 0};
            yield return new object[] { 2003, Month.July, 32 };
            yield return new object[] { 2003, Month.August, 0 };
            yield return new object[] { 2003, Month.August, 32 };
            yield return new object[] { 2003, Month.September, 0 };
            yield return new object[] { 2003, Month.September, 31 };
            yield return new object[] { 2003, Month.October, 0 };
            yield return new object[] { 2003, Month.October, 32 };
            yield return new object[] { 2003, Month.November, 0 };
            yield return new object[] { 2003, Month.November, 31 };
            yield return new object[] { 2003, Month.December, 0 };
            yield return new object[] { 2003, Month.December, 32 };
        }
    }
}