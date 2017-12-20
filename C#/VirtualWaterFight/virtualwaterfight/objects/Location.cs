using System;

using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
    public class Location
    {
        private int x;
        private int y;

        public int X
        {
            get { return x; }
            set { x = value; }
        }

        public int Y
        {
            get { return y; }
            set { y = value; }
        }

        public Location() { }

        public Location(int p1, int p2)
        {
            x = p1; y = p2;
        }
    }
}
