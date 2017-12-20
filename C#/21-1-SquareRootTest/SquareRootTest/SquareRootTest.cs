// Fig. 13.7: SquareRootTest.cs
// Demonstrating a user-defined exception class.
using System;

class SquareRootTest
{
    static void Main(string[] args)
    {
        bool continueLoop = true;

        do
        {
            // catch any NegativeNumberException thrown
            try
            {
                Console.Write(
                   "\nEnter a value to calculate the square root of: ");
                double inputValue = Convert.ToDouble(Console.ReadLine());
                double result = SquareRoot(inputValue);

                Console.WriteLine("The square root of {0} is {1:F6}\n",
                   inputValue, result);
                continueLoop = false;
            }
            catch (FormatException formatException)
            {
                Console.WriteLine("\n" + formatException.Message);
                Console.WriteLine("Please enter a double value.\n");
            }
            catch (NegativeNumberException negativeNumberException)
            {
                Console.WriteLine("\n" + negativeNumberException.Message);
                Console.WriteLine("Please enter a non-negative value.");
                Console.WriteLine("\nMethod-call stack: " + negativeNumberException.StackTrace);
            }
        } while (continueLoop);
    }

    // computes square root of parameter; throws 
    // NegativeNumberException if parameter is negative
    public static double SquareRoot(double value)
    {
        // if negative operand, throw NegativeNumberException
        if (value < 0)
            throw new NegativeNumberException(
               "Square root of negative number not permitted");
        else
            return Math.Sqrt(value); // compute square root
    }
}