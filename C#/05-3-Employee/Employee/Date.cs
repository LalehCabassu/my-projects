// Fig. 10.7: Date.cs 
// Date class declaration.
using System;

public class Date
{
   private int month; // 1-12
   private int day; // 1-31 based on month

   // auto-implemented property Year
   public int Year { get; private set; }

   // constructor: use property Month to confirm proper value for month; 
   // use property Day to confirm proper value for day
   public Date( int theMonth, int theDay, int theYear )
   {
      Month = theMonth; // validate month
      Year = theYear; // could validate year
      Day = theDay; // validate day
      Console.WriteLine( "Date object constructor for date {0}", this );
   }
   
   // property that gets and sets the month
   public int Month
   {
      get
      {
         return month;
      } 
      private set // make writing inaccessible outside the class
      {
         if ( value > 0 && value <= 12 ) // validate month
            month = value;
         else // month is invalid
            throw new ArgumentOutOfRangeException(
               "Month", value, "Month must be 1-12" );
      }
   }

   // property that gets and sets the day
   public int Day
   {
      get
      {
         return day;
      }
      private set // make writing inaccessible outside the class
      {
         int[] daysPerMonth = { 0, 31, 28, 31, 30, 31, 30, 
                                 31, 31, 30, 31, 30, 31 };

         // check if day in range for month
         if ( value > 0 && value <= daysPerMonth[ Month ] )
            day = value;
         // check for leap year
         else if ( Month == 2 && value == 29 &&
            ( Year % 400 == 0 || ( Year % 4 == 0 && Year % 100 != 0 ) ) )
            day = value;
         else // day is invalid
            throw new ArgumentOutOfRangeException(
               "Day", value, "Day out of range for current month/year" );
      }
   }

   // return a string of the form month/day/year
   public override string ToString()
   {
      return string.Format( "{0}/{1}/{2}", Month, Day, Year );
   }
}