// Fig. 20.9: StackTest.cs
// Testing generic class Stack.
using System;
using System.Collections.Generic;

class StackTest
{
   // create arrays of doubles and ints
   private static double[] doubleElements =
      new double[] { 1.1, 2.2, 3.3, 4.4, 5.5, 6.6 };
   private static int[] intElements =
      new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

   private static Stack< double > doubleStack; // stack stores doubles
   private static Stack< int > intStack; // stack stores int objects

   public static void Main( string[] args )
   {
      doubleStack = new Stack< double >( 5 ); // stack of doubles
      intStack = new Stack< int >( 10 ); // stack of ints

      TestPush("doubleStack", doubleStack, doubleElements); // push doubles onto doubleStack
      TestPop("doubleStack", doubleStack); // pop doubles from doubleStack
      TestPush("intStack", intStack, intElements); // push ints onto intStack
      TestPop("intStack", intStack); // pop ints from intStack
   }

   // test Push method
   private static void TestPush< T >( string name, Stack< T > stack,
      IEnumerable< T > elements )
   {
      // push elements onto stack
      try
      {
         Console.WriteLine( "\nPushing elements onto " + name );
          
         // push elements onto stack
         foreach ( var element in elements )
         {
            Console.Write( "{0} ", element );
            stack.Push( element ); // push onto stack
         } 
      } 
      catch ( FullStackException exception )
      {
         Console.Error.WriteLine();
         Console.Error.WriteLine( "Message: " + exception.Message );
         Console.Error.WriteLine( exception.StackTrace );
      } 
   } 

   // test Pop method
   private static void TestPop< T >( string name, Stack< T > stack )
   {
      // pop elements from stack
      try
      {
         Console.WriteLine( "\nPopping elements from " + name );

         T popValue; // store element removed from stack

         // remove all elements from stack
         while ( true )
         {
            popValue = stack.Pop(); // pop from stack
            Console.Write( "{0} ", popValue );
         } 
      } 
      catch ( EmptyStackException exception )
      {
         Console.Error.WriteLine();
         Console.Error.WriteLine( "Message: " + exception.Message );
         Console.Error.WriteLine( exception.StackTrace );
      } 
   } 
} 