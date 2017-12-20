using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LambdaExpressionTest
{
    class Program
    {
        delegate int Trasnformer(int i);

        static void Main(string[] args)
        {
            Trasnformer square1 = new Trasnformer(Square);
            Console.WriteLine("Without Lambda Expression: Square(3) = {0, -4}", square1(3));

            Trasnformer square2 = x => x * x;
            Console.WriteLine("With Lambda Expression: Square(3) = {0, -4}", square2(3));
        }

        private static int Square(int x)
        {
            return x * x;
        }
    }
}
