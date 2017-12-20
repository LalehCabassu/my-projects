// Fig. 20.8: StackTest.cs
// Testing generic class Stack.
using System;

class StackTest
{
   // create arrays of doubles and ints
   private static double[] doubleElements =
      new double[]{ 1.1, 2.2, 3.3, 4.4, 5.5, 6.6 };
   private static int[] intElements =
      new int[]{ 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

   // TODO: declare an stack of double
   // TODO: modify the stack of int
   private static Stack intStack; // stack stores int objects

   public static void Main( string[] args )
   {
      // TODO: instantiate an stack of double
      // TODO: modify the instantiation of the stack of int
      intStack = new Stack( 10 ); // stack of ints


      // TODO: use TestPush to push doubles onto doubleStack
      // TODO: use TestPop to pop doubles from doubleStack
      
      // TODO: use TestPush to push ints onto intStack
      // TODO: use TestPop to pop ints from intStack

      TestPushInt(); // push ints onto intStack
      TestPopInt(); // pop ints from intStack
   }

   // TODO: change TestPushInt to a generic method to test Push method
   // private static void TestPush <T>(string stackName, Stack<T> stack, T[] elements )
   // {
       // push elements onto stack
   // }

   // TODO: change TestPopInt to a generic method to test Pop method
   // private static void TestPop <T>(String stackName, Stack<T> stack)
   // {
       // pop elements from stack
   // } 

   // test Push method with intStack
   private static void TestPushInt()
   {
      // push elements onto stack
      try
      {
         Console.WriteLine( "\nPushing elements onto intStack" );

         // push elements onto stack
         foreach ( var element in intElements )
         {
            Console.Write( "{0} ", element );
            intStack.Push( element ); // push onto intStack
         } 
      } 
      catch ( FullStackException exception )
      {
         Console.Error.WriteLine();
         Console.Error.WriteLine( "Message: " + exception.Message );
         Console.Error.WriteLine( exception.StackTrace );
      } 
   } 

   // test Pop method with intStack
   private static void TestPopInt()
   {
      // pop elements from stack
      try
      {
         Console.WriteLine( "\nPopping elements from intStack" );

         int popValue; // store element removed from stack

         // remove all elements from stack
         while ( true )
         {
            popValue = intStack.Pop(); // pop from intStack
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