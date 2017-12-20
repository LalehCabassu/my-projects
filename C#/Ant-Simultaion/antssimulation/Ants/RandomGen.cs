using System;
using System.Collections.Generic;
using System.Text;

namespace Ants
{
    public static class RandomGen
    {
        private static Random generator = new Random(DateTime.Now.Millisecond);

        public static int Next(int min, int max)
        {
            return generator.Next(min, max);
        }
    }
}
