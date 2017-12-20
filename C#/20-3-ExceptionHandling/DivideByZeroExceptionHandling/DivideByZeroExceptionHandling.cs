// Fig. 13.2: DivideByZeroExceptionHandling.cs
// FormatException and DivideByZeroException handlers.
using System;

class DivideByZeroExceptionHandling
{
   static void Main( string[] args )
   {
      bool continueLoop = true; // determines whether to keep looping

      do
      {
         // retrieve user input and calculate quotient                   
         try
         { 
            // Convert.ToInt32 generates FormatException                 
            // if argument cannot be converted to an integer
            Console.Write( "Enter an integer numerator: " );
            int numerator = Convert.ToInt32( Console.ReadLine() );
            Console.Write( "Enter an integer denominator: " );
            int denominator = Convert.ToInt32( Console.ReadLine() );

            // division generates DivideByZeroException                  
            // if denominator is 0                                       
            int result = numerator / denominator;

            // display result 
            Console.WriteLine( "\nResult: {0} / {1} = {2}", 
               numerator, denominator, result );
            continueLoop = false;
         }
         catch ( FormatException formatException )
         {
            Console.WriteLine( "\n" + formatException.Message );
            Console.WriteLine( 
               "You must enter two integers. Please try again.\n" );
         }
         catch ( DivideByZeroException divideByZeroException )
         {
            Console.WriteLine( "\n" + divideByZeroException.Message );
            Console.WriteLine( 
               "Zero is an invalid denominator. Please try again.\n" );
         }
      } while ( continueLoop );
   }
}