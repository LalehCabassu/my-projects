// Fig. 16.12: StringBuilderAppendFormat.cs
// Demonstrating method AppendFormat.
using System;
using System.Text;

class StringBuilderAppendFormat
{
    public static void Main(string[] args)
    {
        StringBuilder buffer = new StringBuilder();

        // formatted string
        string string1 = "1-This {0} costs: {1,10:C}.\n";

        // string1 argument array
        object[] objectArray = new object[2];

        objectArray[0] = "car";
        objectArray[1] = 1234.56;

        // append to buffer formatted string with argument
        buffer.AppendFormat(string1, objectArray);

        // formatted string
        string string2 = "2-Number:{0:d3}.\n" +
           "Number right aligned with spaces:{0, 4}.\n" +
           "Number left aligned with spaces:{0, -4}.";

        // append to buffer formatted string with argument
        buffer.AppendFormat(string2, 5);

        // display formatted strings
        Console.WriteLine(buffer.ToString());
    }
}