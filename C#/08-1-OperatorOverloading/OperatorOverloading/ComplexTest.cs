// Fig 12.18: ComplexTest.cs
// Overloading operators for complex numbers.
using System;

public class ComplexTest
{
    public static void Main(string[] args)
    {
        // declare two variables to store complex numbers 
        // to be entered by user
        ComplexNumber x, y;

        // prompt the user to enter the first complex number
        Console.Write("Enter the real part of complex number x: ");
        double realPart = Convert.ToDouble(Console.ReadLine());
        Console.Write(
           "Enter the imaginary part of complex number x: ");
        double imaginaryPart = Convert.ToDouble(Console.ReadLine());
        x = new ComplexNumber(realPart, imaginaryPart);

        // prompt the user to enter the second complex number
        Console.Write("\nEnter the real part of complex number y: ");
        realPart = Convert.ToDouble(Console.ReadLine());
        Console.Write(
           "Enter the imaginary part of complex number y: ");
        imaginaryPart = Convert.ToDouble(Console.ReadLine());
        y = new ComplexNumber(realPart, imaginaryPart);

        // display the results of calculations with x and y
        Console.WriteLine();
        Console.WriteLine("{0} + {1} = {2}", x, y, x + y);
        Console.WriteLine("{0} - {1} = {2}", x, y, x - y);
        Console.WriteLine("{0} * {1} = {2}", x, y, x * y);
    }
}