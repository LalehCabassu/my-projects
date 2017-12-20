using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _02_01_CreateInitializeOutputArrays
{
    public class CreateInitializeOutputArrays
    {
        static void Main(string[] args)
        {
            // 1st Array: Creating
            const int FIRST_ARRAY_LENGTH = 5;
            int[] firstArray = new int[FIRST_ARRAY_LENGTH];

            // 1st Array: Initializing
            Console.WriteLine("Please enter {0} integers:", firstArray.Length);
            for (int i = 0; i < firstArray.Length; i++)
            {
                Console.Write("Element {0}: ", i);
                firstArray[i] = Convert.ToInt32(Console.ReadLine());
            }

            // 2nd Array: Creating and Initializing
            int[] secondArray = { 11, 12, 13, 14, 15 };

            // 2nd Array: Outputing
            Console.WriteLine("\nThe elements of the second array: \n{0, 5} {1, 8}", "Index", "Elements");
            for (int i = 0; i < secondArray.Length; i++)
                Console.WriteLine("{0, 5} {1, 8}", i, secondArray[i]);
        }
    }
}
