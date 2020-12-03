using System;
using Xunit;

namespace WeekdaysBetweenDates.Tests
{
    public class DaysBetweenDatesTest
    {
        [Fact]
        public void WeekDaysBetweenDatesTest_DaysOnSubsequentWeeks()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2014, 8, 7);
            var endDate = new DateTime(2014, 8, 11);

            var result = calc.calcWeekDays(startDate, endDate);

            Assert.Equal(1, result);
        }

        [Fact]
        public void WeekDaysBetweenDatesTest_DaysOnSameWeek_WeekDays()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 8);
            var endDate = new DateTime(2020, 12, 11);

            var result = calc.calcWeekDays(startDate, endDate);

            Assert.Equal(2, result);
        }

        [Fact]
        public void WeekDaysBetweenDatesTest_DaysOnSameWeek_EndDateOnSat()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 8);
            var endDate = new DateTime(2020, 12, 12);

            var result = calc.calcWeekDays(startDate, endDate);

            Assert.Equal(3, result);
        }

        [Fact]
        public void WeekDaysBetweenDatesTest_DaysOnSameWeek_EndDateOnSun()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 8);
            var endDate = new DateTime(2020, 12, 13);

            var result = calc.calcWeekDays(startDate, endDate);

            Assert.Equal(3, result);
        }

        [Fact]
        public void WeekDaysBetweenDatesTest_DaysOnSubsequentWeeks_StartDateOnSat()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 5);
            var endDate = new DateTime(2020, 12, 11);

            var result = calc.calcWeekDays(startDate, endDate);

            Assert.Equal(4, result);
        }

        [Fact]
        public void WeekDaysBetweenDatesTest_DaysOnSubsequentWeeks_StartDateOnSun()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 6);
            var endDate = new DateTime(2020, 12, 11);

            var result = calc.calcWeekDays(startDate, endDate);

            Assert.Equal(4, result);
        }

        [Fact]
        public void WeekDaysBetweenDatesTest_DaysOnSubsequentWeeks_StartDateOnSat_AND_EndDateOnSat()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 5);
            var endDate = new DateTime(2020, 12, 12);

            var result = calc.calcWeekDays(startDate, endDate);

            Assert.Equal(5, result);
        }

        [Fact]
        public void WeekDaysBetweenDatesTest_DaysOnSubsequentWeeks_StartDateOnSun_AND_EndDateOnSun()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 6);
            var endDate = new DateTime(2020, 12, 13);

            var result = calc.calcWeekDays(startDate, endDate);

            Assert.Equal(5, result);
        }

        [Fact]
        public void WeekDaysBetweenDatesTest_TwoWeekDiff_WeekDays()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 9);
            var endDate = new DateTime(2020, 12, 24);

            var result = calc.calcWeekDays(startDate, endDate);

            Assert.Equal(10, result);
        }

        [Fact]
        public void WeekDaysBetweenDatesTest_GivenTC1()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2014, 8, 13);
            var endDate = new DateTime(2014, 8, 21);

            var result = calc.calcWeekDays(startDate, endDate);

            Assert.Equal(5, result);
        }

        [Fact]
        public void WeekDaysBetweenDatesTest_LongerTC1()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2014, 8, 13);
            var endDate = new DateTime(2016, 8, 21);

            var result = calc.calcWeekDays(startDate, endDate);
            var expected = calc.calcWeekDaysLoop(startDate, endDate);

            Assert.Equal(expected, result);
        }

        [Fact]
        public void WeekDaysBetweenDatesTest_VeryLongTC1()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(123, 8, 13);
            var endDate = new DateTime(7000, 8, 21);

            var result = calc.calcWeekDays(startDate, endDate);
            var expected = calc.calcWeekDaysLoop(startDate, endDate);

            // Expected: 1794128 
            // Performance: 6ms vs 55ms (Optimised vs O(n))
            Assert.Equal(expected, result);
        }

        [Fact]
        public void WeekDaysBetweenDatesTest_VeryLongTC2()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(1, 8, 13);
            var endDate = new DateTime(9999, 8, 21);

            var result = calc.calcWeekDays(startDate, endDate);
            var expected = calc.calcWeekDaysLoop(startDate, endDate);

            // Expected: 2608359 
            // Performance: 7ms vs 71ms (Optimised vs O(n))
            Assert.Equal(expected, result);
        }
    }
}
