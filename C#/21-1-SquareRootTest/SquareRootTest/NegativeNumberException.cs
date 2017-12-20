// Fig. 13.6: NegativeNumberException.cs
// NegativeNumberException represents exceptions caused by
// illegal operations performed on negative numbers.
using System;

class NegativeNumberException : Exception
{
    // default constructor                                
    public NegativeNumberException()
        : base("Illegal operation for a negative number")
    {
    }

    // constructor for customizing error message         
    public NegativeNumberException(string messageValue)
        : base(messageValue)
    {
    }

    // constructor for customizing the exception's error
    // message and specifying the InnerException object 
    public NegativeNumberException(string messageValue,
       Exception inner)
        : base(messageValue, inner)
    {
    }
}