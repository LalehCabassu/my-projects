// Fig. 21.7: HashtableTest.cs
// App counts the number of occurrences of each word in a string
// and stores them in a hash table.
using System;
using System.Text.RegularExpressions;
using System.Collections;

public class HashtableTest
{
   public static void Main( string[] args )
   {
      // create hash table based on user input
      Hashtable table = CollectWords();

      // display hash table content
      DisplayHashtable( table );
   }

   // create hash table from user input
   private static Hashtable CollectWords()
   {
      Hashtable table = new Hashtable(); // create a new hash table

      Console.WriteLine( "Enter a string: " ); // prompt for user input
      string input = Console.ReadLine(); // get input

      // split input text into tokens
      string[] words = Regex.Split( input, @"\s+" );

      // processing input words
      foreach ( var word in words )
      {
         string wordKey = word.ToLower(); // get word in lowercase

         // if the hash table contains the word
         if ( table.ContainsKey( wordKey ) )
         {
            table[ wordKey ] = ( ( int ) table[ wordKey ] ) + 1;
         }
         else
            // add new word with a count of 1 to hash table
            table.Add( wordKey, 1 );
      }
      
      return table;
   }

   // display hash table content
   private static void DisplayHashtable( Hashtable table )
   {
      Console.WriteLine( "\nHashtable contains:\n{0,-12}{1,-12}",
         "Key:", "Value:" );

      // generate output for each key in hash table
      // by iterating through the Keys property with a foreach statement
      foreach ( var key in table.Keys )
         Console.WriteLine( "{0,-12}{1,-12}", key, table[ key ] );

      Console.WriteLine( "\nsize: {0}", table.Count );
   }
}