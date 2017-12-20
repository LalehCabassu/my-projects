using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskWaitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task = Task.Run( () => {
                for (int i = 0; i < 1000; i++)
                    Console.Write("Y");
            });

            Console.WriteLine("Is task completed? {0}", task.IsCompleted);
            task.Wait();
            Console.WriteLine();
        }
    }
}
