using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03_6_ExceptionHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter an integer: ");
            string input = Console.ReadLine();
            try{
                int num = Convert.ToInt32(input);
            }
            catch (FormatException e)
            {
                Console.WriteLine("An exception occurred: \n{0}\n input: {1}", e.Message, input); 
            }
        }
    }
}
