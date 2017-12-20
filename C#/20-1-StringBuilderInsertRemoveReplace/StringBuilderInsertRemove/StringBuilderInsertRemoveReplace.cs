// Fig. 16.13: StringBuilderInsertRemove.cs
// Demonstrating methods Insert and Remove of the 
// StringBuilder class.
using System;
using System.Text;

class StringBuilderInsertRemoveReplace
{
    public static void Main(string[] args)
    {
        object objectValue = "hello";
        string stringValue = "good bye";
        char[] characterArray = { 'a', 'b', 'c', 'd', 'e', 'f' };
        bool booleanValue = true;
        char characterValue = 'K';
        int integerValue = 7;
        long longValue = 10000000;
        float floatValue = 2.5F; // F suffix indicates that 2.5 is a float
        double doubleValue = 33.333;
        StringBuilder buffer = new StringBuilder();

        // insert values into buffer
        buffer.Insert(0, objectValue);
        buffer.Insert(0, "  ");
        buffer.Insert(0, stringValue);
        buffer.Insert(0, "  ");
        buffer.Insert(0, characterArray);
        buffer.Insert(0, "  ");
        buffer.Insert(0, booleanValue);
        buffer.Insert(0, "  ");
        buffer.Insert(0, characterValue);
        buffer.Insert(0, "  ");
        buffer.Insert(0, integerValue);
        buffer.Insert(0, "  ");
        buffer.Insert(0, longValue);
        buffer.Insert(0, "  ");
        buffer.Insert(0, floatValue);
        buffer.Insert(0, "  ");
        buffer.Insert(0, doubleValue);
        buffer.Insert(0, "  ");

        Console.WriteLine("buffer after Inserts: \n" + buffer + "\n");

        buffer.Remove(10, 1); // delete 2 in 2.5
        buffer.Remove(4, 4);  // delete .333 in 33.333

        Console.WriteLine("buffer after Removes:\n" + buffer);

         StringBuilder builder1 =
           new StringBuilder("Happy Birthday Jane");
        StringBuilder builder2 =
           new StringBuilder("goodbye greg");

        Console.WriteLine("Before replacements:\n" +
           builder1.ToString() + "\n" + builder2.ToString());

        builder1.Replace("Jane", "Greg");
        builder2.Replace('g', 'G', 0, 5);

        Console.WriteLine("\nAfter replacements:\n" +
           builder1.ToString() + "\n" + builder2.ToString());
    }
}
