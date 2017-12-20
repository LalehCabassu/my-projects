// Fig. 20.7: EmptyStackException.cs
// EmptyStackException indicates a stack is empty.
using System;

class EmptyStackException : Exception
{
   // parameterless constructor
   public EmptyStackException() : base( "Stack is empty" )
   {
      // empty constructor
   } 

   // one-parameter constructor
   public EmptyStackException( string exception ) : base( exception )
   {
      // empty constructor
   } 

   // two-parameter constructor
   public EmptyStackException( string exception, Exception inner )
      : base( exception, inner )
   {
      // empty constructor
   } 
}