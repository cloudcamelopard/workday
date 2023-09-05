using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WorkdayCalendarLibTest.TestResults
{
    [TestClass]
    public class UnitTestDateTimeBuilder
    {
        [TestMethod]
        [DynamicData(nameof(TestDataGenerator), DynamicDataSourceType.Method)]
        public void TestValidData(string inputDateTime, Func<DateTimeBuilder, DateTimeBuilder> manip, string expectedDateTime)
        {
            var input = DateTime.ParseExact(inputDateTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            var expected = DateTime.ParseExact(expectedDateTime, "yyyy-MM-dd HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

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
        }
    }
}