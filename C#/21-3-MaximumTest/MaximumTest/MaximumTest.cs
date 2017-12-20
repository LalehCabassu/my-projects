// Fig. 20.4: MaximumTest.cs
// Generic method Maximum returns the largest of three objects.
using System;

class MaximumTest
{
   public static void Main( string[] args )
   {
      Console.WriteLine( "Maximum of {0}, {1} and {2} is {3}\n",
         3, 4, 5, Maximum( 3, 4, 5 ) );
      Console.WriteLine( "Maximum of {0}, {1} and {2} is {3}\n",
         6.6, 8.8, 7.7, Maximum( 6.6, 8.8, 7.7 ) );
      Console.WriteLine( "Maximum of {0}, {1} and {2} is {3}\n",
         "pear", "apple", "orange",
         Maximum( "pear", "apple", "orange" ) );
   }

   // generic function determines the
   // largest of the IComparable objects
   private static T Maximum< T >( T x, T y, T z ) 
       where T : IComparable
   {
      T max = x;

      // compare y with max
      if ( y.CompareTo(max) > 0 )
         max = y;

      // compare z with max
      if ( z.CompareTo(max) > 0 )
         max = z;

      return max;
   }
}