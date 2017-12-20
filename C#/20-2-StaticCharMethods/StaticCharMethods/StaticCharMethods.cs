// Fig. 16.15: StaticCharMethods.cs
// Demonstrates static character testing and case conversion methods 
// from Char struct
using System;

class StaticCharMethods
{
    static void Main(string[] args)
    {
        Console.Write("Enter a character: ");
        char character = Convert.ToChar(Console.ReadLine());

        Console.WriteLine("is digit: {0}", Char.IsDigit(character));
        Console.WriteLine("is letter: {0}", Char.IsLetter(character));
        Console.WriteLine("is letter or digit: {0}",
           Char.IsLetterOrDigit(character));
        Console.WriteLine("is lower case: {0}",
           Char.IsLower(character));
        Console.WriteLine("is upper case: {0}",
           Char.IsUpper(character));
        Console.WriteLine("to upper case: {0}",
           Char.ToUpper(character));
        Console.WriteLine("to lower case: {0}",
           Char.ToLower(character));
        Console.WriteLine("is punctuation: {0}",
           Char.IsPunctuation(character));
        Console.WriteLine("is symbol: {0}", Char.IsSymbol(character));
    }
}