using System;
using System.Collections.Generic;
using System.Text;

namespace Ants
{
    public class Direction
    {
        private int deltaRow = 0;
        private int deltaColumn = 0;

        public Direction(int deltaRow, int deltaColumn)
        {
            this.deltaRow = deltaRow;
            this.deltaColumn = deltaColumn;
        }

        public int DeltaRow
        {
            get { return deltaRow; }
            set { deltaRow = value; }
        }

        public int DeltaColumn
        {
            get { return deltaColumn; }
            set { deltaColumn = value; }
        }

        public override string ToString()
        {
            return "(" + deltaColumn.ToString() + "," + deltaRow.ToString() + ")";
        }
    }
}
