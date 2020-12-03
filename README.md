# WeekdaysBetweenDates
Calculate the weekdays/business days between two dates.

The program calculates the number of weekdays and/or business days between two given dates.
Business dates differ from weekdays, as this calculation omits any public holidays.

## How to use
Target Framework: .NET Core 3.1
*DaysBetweenDates* class has to be initialised in the Main func. Example code is given. 
`DaysBetweenDates.calcWeekDays(startDate, endDate)` -> Gives the non-inclusive week days between startDate and endDate
`DaysBetweenDates.calcBusinessDays(startDate, endDate)` -> Gives the non-inclusive business days betwee startDate and endDate
Before `calcBusinessDays` can be used the holidays need to be parsed into the `DaysBetweenDates` objects.
  - Normal constant date holidays can be given as `DD/MM` in the file you parse
  - Holidays that occur on weekends that need to pushed back to Monday, would follow the same convention as before: `DD/MM`
  - Holidays that are a certain day in a certain week, have to written differently. The file must contain entries in the format: `Day/WeekNumber/Month`.
    Where Day is an int that represents a day of the week. Following DateTime conventions, this would be represented as `Sunday=0, Monday=1,.., Saturday=6`.
    WeekNumber and Month are normal integers that represent the respective week and month the holiday occurs.
    
Many test cases are provided to tinker around with.

The program runs in `O(1)` time complexity. But 

## Design considerations for improvement
1. If a holiday occurs on a Sunday and needs to be pushed back to Monday, however another holiday occurs on Monday. Does this holiday gets pushed back to Tuesday?
2. More efficient method to deal with holidays that doesn't use loops
3. File opening can be simplified into one method, therefore reducing repeated code. i.e. Need to *refactor*
4. Holidays that happen to be on the same day, causes an issue. This can be solved with a HashSet that records the holidays that have been marked.
    If the same holiday occurs again, then it would not be deducted from the business days (ran out of time to implement this).
5. Console interface to input dates to test out the program.
6. DateTime restricts the dates be from `1/1/0001` to `12/31/9999`. A new custom class might be needed to be made to extend the functionality to check between more dates.
**7. I was unable to create a Web API through ASP.Net, as when I tried it couldn't find the program that I made. I am not sure why this would be the case.**

P.S. Very sorry about the delay. I have never programmed in C# before, I needed time to learn the syntax and get started. But really enjoyed the language, much easier to work with than Java
