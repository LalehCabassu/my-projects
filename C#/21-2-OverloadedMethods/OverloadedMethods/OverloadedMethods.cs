// Fig. 20.1: OverloadedMethods.cs
// Using overloaded methods to display arrays of different types.
using System;

class OverloadedMethods
{
    public static void Main(string[] args)
    {
        // create arrays of int, double and char
        int[] intArray = { 1, 2, 3, 4, 5, 6 };
        double[] doubleArray = { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7 };
        char[] charArray = { 'H', 'E', 'L', 'L', 'O' };

        Console.WriteLine("Array intArray contains:");
        DisplayArray(intArray); // pass an int array argument
        Console.WriteLine("Array doubleArray contains:");
        DisplayArray(doubleArray); // pass a double array argument
        Console.WriteLine("Array charArray contains:");
        DisplayArray(charArray); // pass a char array argument
    }

    // output int array
    private static void DisplayArray(int[] inputArray)
    {
        foreach (int element in inputArray)
            Console.Write(element + " ");

        Console.WriteLine("\n");
    }

    // output double array
    private static void DisplayArray(double[] inputArray)
    {
        foreach (double element in inputArray)
            Console.Write(element + " ");

        Console.WriteLine("\n");
    }

    // output char array
    private static void DisplayArray(char[] inputArray)
    {
        foreach (char element in inputArray)
            Console.Write(element + " ");

        Console.WriteLine("\n");
    }
}
