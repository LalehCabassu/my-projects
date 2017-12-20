// Fig. 20.3: GenericMethod.cs
// Using overloaded methods to display arrays of different types.
using System;
using System.Collections.Generic;

class GenericMethod
{
    public static void Main(string[] args)
    {
        // create arrays of int, double and char
        int[] intArray = { 1, 2, 3, 4, 5, 6 };
        double[] doubleArray = { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6, 7.7 };
        char[] charArray = { 'H', 'E', 'L', 'L', 'O' };

        Console.WriteLine("Array intArray contains:");
        DisplayArray(intArray);
        Console.WriteLine("Array doubleArray contains:");
        DisplayArray(doubleArray);
        Console.WriteLine("Array charArray contains:");
        DisplayArray(charArray);
    }

    // output array of all types
    private static void DisplayArray<T>(T[] inputArray)
    {
        foreach (T element in inputArray)
            Console.Write(element + " ");

        Console.WriteLine("\n");
    }
}
