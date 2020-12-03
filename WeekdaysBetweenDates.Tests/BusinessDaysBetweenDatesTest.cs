using System;
using Xunit;

namespace WeekdaysBetweenDates.Tests
{
    public class BusinessDaysBetweenDatesTest
    {
        [Fact]
        public void BusinessDaysBetweenDatesTest_WeekDayHoliday()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 8);
            var endDate = new DateTime(2020, 12, 11);

            calc.parseHolidaysSameDay("./testFiles/OrdinaryHoliday/WeekDayHoliday.txt");
            var result = calc.calcBusinessDays(startDate, endDate);
            var expected = calc.calcWeekDaysLoop(startDate, endDate) - 1;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void BusinessDaysBetweenDatesTest_WeekDayHolidayLong()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2018, 12, 8);
            var endDate = new DateTime(2020, 12, 11);

            calc.parseHolidaysSameDay("./testFiles/OrdinaryHoliday/WeekDayHoliday.txt");

            var result = calc.calcBusinessDays(startDate, endDate);
            var expected = calc.calcWeekDaysLoop(startDate, endDate) - 2;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void BusinessDaysBetweenDatesTest_WeekDayTwoHolidays()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 3);
            var endDate = new DateTime(2020, 12, 17);

            calc.parseHolidaysSameDay("./testFiles/OrdinaryHoliday/WeekDayTwoHolidays.txt");

            var result = calc.calcBusinessDays(startDate, endDate);
            var expected = calc.calcWeekDaysLoop(startDate, endDate) - 2;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void BusinessDaysBetweenDatesTest_WeekendHolidayMoveToMonday1()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 3);
            var endDate = new DateTime(2020, 12, 8);

            calc.parseHolidaysSameDayOrMonday("./testFiles/MoveToMondayHoliday/SimpleSunday.txt");

            var result = calc.calcBusinessDays(startDate, endDate);
            var expected = calc.calcWeekDaysLoop(startDate, endDate) - 1;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void BusinessDaysBetweenDatesTest_DayInMonth1()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 10);
            var endDate = new DateTime(2020, 12, 16);

            calc.parseHolidaysDayInMonth("./testFiles/DayInMonth/SimpleDayInMonth.txt");

            var result = calc.calcBusinessDays(startDate, endDate);
            var expected = calc.calcWeekDaysLoop(startDate, endDate) - 1;

            Assert.Equal(expected, result);
        }

        [Fact]
        public void BusinessDaysBetweenDatesTest_Combination()
        {
            var calc = new DaysBetweenDates();

            var startDate = new DateTime(2020, 12, 3);
            var endDate = new DateTime(2020, 12, 20);

            calc.parseHolidaysDayInMonth("./testFiles/DayInMonth/SimpleDayInMonth.txt");
            calc.parseHolidaysSameDay("./testFiles/OrdinaryHoliday/WeekDayTwoHolidays.txt");
            calc.parseHolidaysSameDayOrMonday("./testFiles/MoveToMondayHoliday/SimpleSunday.txt");

            var result = calc.calcBusinessDays(startDate, endDate);
            var expected = calc.calcWeekDaysLoop(startDate, endDate) - 4;

            Assert.Equal(expected, result);
        }
    }
}
