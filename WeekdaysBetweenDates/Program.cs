using System;
using System.Collections.Generic;

namespace WeekdaysBetweenDates
{

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

            
        }
    }
}
