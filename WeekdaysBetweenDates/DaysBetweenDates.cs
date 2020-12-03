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


        // Performance: O(1)
        // No loop is used. The difference between the dates are used to calculate the number of weekdays.
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

            TimeSpan span = endDate - startDate;


            int weekDays = span.Days + 1;
            int weekCount = weekDays / 7;


            // Calculate the numeber of weekends
            if (weekDays > weekCount * 7)
            {
                // After taking away whole week, check whether one or two weekends need to be accounted for
                // DateTime starts the week off on a Sunday and assigns a value of 0, Monday = 2, Tuesday = 3...
                // To make out calculations work, we interject when it is a Sunday and replace the 0 value with a 7
                int firstDay = startDate.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)startDate.DayOfWeek;
                int lastDay = endDate.DayOfWeek == DayOfWeek.Sunday ? 7 : (int)endDate.DayOfWeek;

                if (lastDay < firstDay)
                {
                    lastDay += 7;
                }
                    
                if (firstDay <= 6)
                {
                    if (lastDay >= 7)
                    {
                        // Need to take away Saturday and Sunday
                        weekDays -= 2;
                    }
                    else if (lastDay >= 6)
                    {
                        // Need to only take away Sunday
                        weekDays -= 1;
                    }
                }
                else if (firstDay <= 7 && lastDay >= 7)
                {
                    // Take away Sunday
                    weekDays -= 1;
                }
            }

            weekDays -= weekCount + weekCount;

            return weekDays;

            
        }

        // Calculate business days inbetween two dates (i.e. Exclude holidays)
        // Performace: O((End Date Year - Start Date Year)  * Number of holidays)
        // As we can assume the holidays would be <= 365 and the Number of years would be inherently limited to 9999
        // Due to a limitation of the DateTime implementation in C#, we arrive at a constant complexity O(1)
        // If years beyond 9999 is needed, then we would need to implement our own DateTime class that extends the capability
        // That the in-built DateTime lacks
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
                        if (endDate.DayOfWeek == DayOfWeek.Sunday && (endDate - currHoliday).Days == 1)
                            continue;
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
                            currHoliday = currHoliday.AddDays(1);
                        }

                        for (int weekNum = 0; weekNum < week-1; weekNum++)
                        {
                            currHoliday = currHoliday.AddDays(7);
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
