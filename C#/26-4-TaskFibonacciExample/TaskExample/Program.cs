using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace TaskExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = Task.Run(() =>
            {
                Console.WriteLine(Fibonacci(40));
            });
            
            Console.WriteLine(task.IsCompleted);  // False
            task.Wait();  // Blocks until task is complete
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
