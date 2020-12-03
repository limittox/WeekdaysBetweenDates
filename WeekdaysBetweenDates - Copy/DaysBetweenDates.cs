using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.IO;


namespace WeekdaysBetweenDates
{
    public class DaysBetweenDates
    {
        private List<DateTime> holidays;
        private List<DateTime> holidaysOrMondays;
        private List<int[]> holidaysDayInMonth;

        public DaysBetweenDates()
        {
            this.holidays = new List<DateTime>();
            this.holidaysOrMondays = new List<DateTime>();
            this.holidaysDayInMonth = new List<int[]>();
        }

        // Less efficient method for testing purposes
        public int calcWeekDaysLoop(DateTime startDate, DateTime endDate)
        {
            if (startDate > endDate)
            {
                throw new ArgumentException("Dates are not in the correct order");
            }

            if (startDate == endDate)
            {
                return 0;
            }

            int weekDays = 0;

            for (var iterDate = startDate.AddDays(1); iterDate < endDate; iterDate = iterDate.AddDays(1))
            {
                if (iterDate.DayOfWeek != DayOfWeek.Saturday && iterDate.DayOfWeek != DayOfWeek.Sunday)
                {
                    weekDays += 1;
                }
            }

            return weekDays;


        }

        public int calcWeekDays(DateTime startDate, DateTime endDate)
        {
            startDate = startDate.AddDays(1);
            endDate = endDate.AddDays(-1);
            if (startDate > endDate)
            {
                throw new ArgumentException("Dates are not in the correct order");
            }
            
            if (startDate == endDate)
            {
                return 0;
            }
            startDate = startDate.Date;
            endDate = endDate.Date;
            if (startDate > endDate)
                throw new ArgumentException("Incorrect last day " + endDate);

            TimeSpan span = endDate - startDate;
            int businessDays = span.Days + 1;
            int fullWeekCount = businessDays / 7;
            // find out if there are weekends during the time exceedng the full weeks
            if (businessDays > fullWeekCount * 7)
            {
                // we are here to find out if there is a 1-day or 2-days weekend
                // in the time interval remaining after subtracting the complete weeks
                int firstDayOfWeek = startDate.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)startDate.DayOfWeek;
                int lastDayOfWeek = endDate.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)endDate.DayOfWeek;
                if (lastDayOfWeek < firstDayOfWeek)
                    lastDayOfWeek += 7;
                if (firstDayOfWeek <= 6)
                {
                    if (lastDayOfWeek >= 7)// Both Saturday and Sunday are in the remaining time interval
                        businessDays -= 2;
                    else if (lastDayOfWeek >= 6)// Only Saturday is in the remaining time interval
                        businessDays -= 1;
                }
                else if (firstDayOfWeek <= 7 && lastDayOfWeek >= 7)// Only Sunday is in the remaining time interval
                    businessDays -= 1;
            }

            businessDays -= fullWeekCount + fullWeekCount;

            return businessDays;

            
        }

        // Calculate business days inbetween two dates (i.e. Exclude holidays)
        public int calcBusinessDays(DateTime startDate, DateTime endDate)
        {
            int weekDays = calcWeekDays(startDate, endDate);
            int businessDays = weekDays;

            foreach (var holiday in this.holidays)
            {
                for (int year = startDate.Year; year <= endDate.Year; year++)
                {
                    DateTime currHoliday = new DateTime(year, holiday.Month, holiday.Day);
                    if (currHoliday > startDate && currHoliday < endDate && currHoliday.DayOfWeek != DayOfWeek.Saturday && currHoliday.DayOfWeek != DayOfWeek.Sunday)
                    {
                        businessDays -= 1;
                    }
                }
            }

            foreach (var holiday in this.holidaysOrMondays)
            {
                for (int year = startDate.Year; year <= endDate.Year; year++)
                {
                    DateTime currHoliday = new DateTime(year, holiday.Month, holiday.Day);
                    if (currHoliday > startDate && currHoliday < endDate)
                    {
                        businessDays -= 1;
                    }
                }
            }

            foreach (var holiday in this.holidaysDayInMonth)
            {
                    for (int year = startDate.Year; year <= endDate.Year; year++)
                     {
                        var day = holiday[0];
                        var week = holiday[1];
                        var month = holiday[2];

                        var currHoliday = new DateTime(year, month, 1);
                        while ((int)currHoliday.DayOfWeek != day)
                        {
                            currHoliday.AddDays(1);
                        }

                        for (int weekNum = 0; weekNum < week; weekNum++)
                        {
                            currHoliday.AddDays(7);
                        }

                        if (currHoliday > startDate && currHoliday < endDate)
                        {
                            businessDays -= 1;
                        }
                }
            }

            return businessDays;
        }


        public void parseHolidaysSameDay(string file)
        {
            using FileStream holidayFile = File.OpenRead(file);
            using var sr = new StreamReader(holidayFile);

            string line;



            while((line = sr.ReadLine())!= null)
            {
                string[] splitLine = line.Split("/");
                var day = Int32.Parse(splitLine[0]);
                var month = Int32.Parse(splitLine[1]);
                var newHoliday = new DateTime(2020, month, day);
                this.holidays.Add(newHoliday);
            }
        }

        public void parseHolidaysSameDayOrMonday(string file)
        {
            using FileStream holidayFile = File.OpenRead(file);
            using var sr = new StreamReader(holidayFile);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] splitLine = line.Split("/");
                var day = Int32.Parse(splitLine[0]);
                var month = Int32.Parse(splitLine[1]);
                var newHoliday = new DateTime(2020, month, day);
                this.holidaysOrMondays.Add(newHoliday);
            }
        }

        // DAY/WEEK/MONTH
        // 1/2/6
        // Monday=1, Tuesday=2,..., Sunday=0
        public void parseHolidaysDayInMonth(string file)
        {
            using FileStream holidayFile = File.OpenRead(file);
            using var sr = new StreamReader(holidayFile);

            string line;

            while ((line = sr.ReadLine()) != null)
            {
                string[] splitLine = line.Split("/");
                var day = Int32.Parse(splitLine[0]);
                var week = Int32.Parse(splitLine[1]);
                var month = Int32.Parse(splitLine[2]);
                this.holidaysDayInMonth.Add(new int[] { day, week, month });
            }
        }
    }
}
