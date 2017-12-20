// Fig. 16.11: StringBuilderAppend.cs
// Demonstrating StringBuilder Append methods.
using System;
using System.Text;

class StringBuilderAppend
{
    public static void Main(string[] args)
    {
        object objectValue = "hello";
        string stringValue = "good bye";
        char[] characterArray = { 'a', 'b', 'c', 'd', 'e', 'f' };
        bool booleanValue = true;
        char characterValue = 'Z';
        int integerValue = 7;
        long longValue = 1000000;
        float floatValue = 2.5F; // F suffix indicates that 2.5 is a float
        double doubleValue = 33.333;
        StringBuilder buffer = new StringBuilder();

        // use method Append to append values to buffer
        buffer.Append(objectValue);
        buffer.Append("  ");
        buffer.Append(stringValue);
        buffer.Append("  ");
        buffer.Append(characterArray);
        buffer.Append("  ");
        buffer.Append(characterArray, 0, 3);
        buffer.Append("  ");
        buffer.Append(booleanValue);
        buffer.Append("  ");
        buffer.Append(characterValue);
        buffer.Append("  ");
        buffer.Append(integerValue);
        buffer.Append("  ");
        buffer.Append(longValue);
        buffer.Append("  ");
        buffer.Append(floatValue);
        buffer.Append("  ");
        buffer.Append(doubleValue);

        Console.WriteLine("buffer = " + buffer.ToString() + "\n");
    }
}