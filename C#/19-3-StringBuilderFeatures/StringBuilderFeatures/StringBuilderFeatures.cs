// Fig. 16.10: StringBuilderFeatures.cs
// Demonstrating some features of class StringBuilder.
using System;
using System.Text;

class StringBuilderFeatures
{
    public static void Main(string[] args)
    {
        StringBuilder buffer =
           new StringBuilder("Hello, how are you?");

        // use Length and Capacity properties
        Console.WriteLine("buffer = " + buffer +
           "\nLength = " + buffer.Length +
           "\nCapacity = " + buffer.Capacity);

      
        buffer.EnsureCapacity(75); // ensure a capacity of at least 75
        Console.WriteLine("\nAfter ensuring a capacity of at least 75\nNew capacity = " +
           buffer.Capacity +
           "\nNew length = " +
           buffer.Length);

        
        // truncate StringBuilder by setting Length property
        buffer.Length = 10;
        Console.Write("\nAfter truncating StringBuilder by setting Lenght property\nNew capacity = " +
           buffer.Capacity +
           "\nNew length = " +
           buffer.Length + "\nbuffer = ");

        // use StringBuilder indexer
        for (int i = 0; i < buffer.Length; ++i)
            Console.Write(buffer[i]);

        Console.WriteLine("\n");
    }
}