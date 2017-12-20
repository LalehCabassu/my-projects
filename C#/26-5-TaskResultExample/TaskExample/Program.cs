using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Task<long> task = Task.Run(() =>
            {
                return Fibonacci(40);
            });

            Console.WriteLine("{0}", task.Result);  // Blocks until task is complete and return the result
        }

        // recursive method Fibonacci; calculates nth Fibonacci number
        static long Fibonacci(long n)
        {
            if (n == 0 || n == 1)
                return n;
            else
                return Fibonacci(n - 1) + Fibonacci(n - 2);
        } 
    }
}
