using System;

class StringConstructorMethods
{
   public static void Main( string[] args )
   {
      // string initialization
      char[] characterArray = 
         { 'b', 'i', 'r', 't', 'h', ' ', 'd', 'a', 'y' };
      string originalString = "Welcome to C# programming!";
      string string1 = originalString;
      string string2 = new string( characterArray );
      string string3 = new string( characterArray, 6, 3 );
      string string4 = new string( 'C', 5 );

      Console.WriteLine( "string1 = " + "\"" + string1 + "\"\n" +
         "string2 = " + "\"" + string2 + "\"\n" +
         "string3 = " + "\"" + string3 + "\"\n" +
         "string4 = " + "\"" + string4 + "\"\n" );

      // test Length property
      Console.WriteLine("Length of string1: " + string1.Length);

      // loop through characters in string1 and display reversed
      Console.Write("The string reversed is: ");

      for (int i = string1.Length - 1; i >= 0; --i)
          Console.Write(string1[i]);

      // copy characters from string1 into characterArray
      string1.CopyTo(0, characterArray, 0, characterArray.Length);
      Console.Write("\nThe character array is: ");

      for (int i = 0; i < characterArray.Length; ++i)
          Console.Write(characterArray[i]);

      Console.WriteLine("\n");
   } 
}