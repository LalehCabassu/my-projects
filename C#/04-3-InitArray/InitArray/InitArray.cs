// Fig. 8.19: InitArray.cs
// Initializing rectangular and jagged arrays.
using System;

public class InitArray
{
    public static void Main(string[] args)
    {
        // with rectangular arrays,
        // every row must be the same length.
        int[,] rectangular = { { 1, 2, 3 }, { 4, 5, 6 } };

        // with jagged arrays,
        // we need to use "new int[]" for every row,
        // but every row does not need to be the same length.
        int[][] jagged = { new int[] { 1, 2 }, 
                         new int[] { 3 }, 
                         new int[] { 4, 5, 6 } };

        OutputArray(rectangular);
        Console.WriteLine();
        OutputArray(jagged);
    }

    // output rows and columns of a rectangular array
    public static void OutputArray(int[,] array)
    {
        Console.WriteLine("Values in the rectangular array by row are");

        for (int row = 0; row < array.GetLength(0); ++row)
        {
            for (int column = 0; column < array.GetLength(1); ++column)
                Console.Write("{0}  ", array[row, column]);

            Console.WriteLine();
        }
    }

    // output rows and columns of a jagged array
    public static void OutputArray(int[][] array)
    {
        Console.WriteLine("Values in the jagged array by row are");

        Console.WriteLine("For loop");
        for (int row = 0; row < array.GetLength(0); ++row)
        {

            for (int column = 0; column < array[row].GetLength(0); ++column)
                Console.Write("{0}  ", array[row][column]);

            Console.WriteLine();
        }

        Console.WriteLine("foreach loop");
        foreach (int[] row in array)
        {
            foreach (int element in row)
                Console.Write("{0}  ", element);

            Console.WriteLine();
        }
    }
}