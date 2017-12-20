using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListFlattening
{
    class MultiLevelList
    {
        public Node Head { get; set; }
        public Node Tail { get; set; }


        public bool FlattenList()
        {
            bool result = true;

            Node curNode = this.Head;
            while (curNode != null)
            {
                if (curNode.HasChild())
                    result = Append(curNode.Child);
                curNode = curNode.Next;
            }

            return result;
        }

        public bool UnflattenList()
        {
            bool result = true;



            return result;
        }


        public bool Append(Node newNode)
        {
            bool result = true;

            if (newNode == null)
                result = false;
            else
            {
                newNode.Previous = this.Tail;
                if (this.Tail == null)
                    this.Head = newNode;
                else
                    this.Tail.Next = newNode;
                
                while (newNode.Next != null)
                    newNode = newNode.Next;
                this.Tail = newNode;
            }

            return result;
        }
    }
}
