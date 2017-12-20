using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMultiThreadApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Kick off a new task writing Y
            Task task = Task.Run(() =>
            {
                for (int i = 0; i < 1000; i++)
                    Console.Write("Y");
            });

            // Simultaneously, do something on the main thread.
            for (int i = 0; i < 1000; i++)
                Console.Write("x");

            // Typical Output:
            //xxxxxxxxxxxxxxxxyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy
            //xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxyyyyyyyyyyyyy
            //yyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyxxxxxxxxxxxxxxxxxxxxxx
            //xxxxxxxxxxxxxxxxxxxxxxyyyyyyyyyyyyyyyyyyyyyyyyyyyyyyy
            //yyyyyyyyyyyyyxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
        }
    }
}
