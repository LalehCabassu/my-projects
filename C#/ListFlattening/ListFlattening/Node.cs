using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListFlattening
{
    class Node
    {
        public Node Next { get; set; }
        public Node Previous { get; set; }
        public Node Child { get; set; }
        public int Value { get; private set; }

        public Node(int value)
        {
            this.Value = value;
        }

        public bool HasChild()
        {
            return (this.Child == null)? false: true;
        }
    }
}
