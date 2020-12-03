using System;
using System.Collections.Generic;

namespace WeekdaysBetweenDates
{

    //https://stackoverflow.com/questions/4600034/calculate-number-of-weekdays-between-two-dates-in-java
    //https://github.com/nodatime/nodatime/issues/6
    //https://gist.github.com/timgaunt/b77e7d34c0a7f05817fa5c132bf1009a
    //https://www.xspdf.com/resolution/56669228.html
    // https://www.ryadel.com/en/datetime-add-subtract-calculate-get-business-days-asp-net-c-sharp-helper-class-function/ ****
    public class Program
    {
        static void Main(string[] args)
        {
            HashSet<DateTime> holidays = new HashSet<DateTime>();
            DateTime christmas = new DateTime(2020, 12, 25);
            DateTime boxingDay = new DateTime(2020, 12, 26);
            

            Console.WriteLine("Hello World!");

            DaysBetweenDates calc = new DaysBetweenDates();
            Console.WriteLine(calc.calcWeekDaysLoop(new DateTime(1, 8, 13), new DateTime(9999, 8, 21)));
            calc.parseHolidaysSameDay("sameDayHolidays.txt");

            Console.WriteLine(calc.calcBusinessDays(new DateTime(2020, 12, 8), new DateTime(2020, 12, 16)));

        }
    }
}
