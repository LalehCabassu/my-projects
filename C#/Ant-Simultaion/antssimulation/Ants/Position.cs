using System;
using System.Collections.Generic;
using System.Text;

using Vitruvian.Services;
using Vitruvian.Serialization;
using Vitruvian.Distribution;
using Vitruvian.Logging;

namespace Ants
{
    [OptimisticSerialization]
    public class Position
    {
        Logger _logger = Logger.GetLogger(typeof(Position));

        private static Ground ground = null;

        #region Constructors

        public Position() { }

        public Position(int row, int column)
        {
            Row = row;
            Column = column;
        }

        #endregion

        public int Row { get; set; }

        public int Column { get; set; }

        public Direction Move(Direction direction)
        {
            _logger.Debug("Entering Move");
            _logger.DebugFormat("Currently at Row={0}, Column={1}", Row, Column);
            _logger.DebugFormat("Moving in {0}, {1}", direction.DeltaRow, direction.DeltaColumn);

            Direction result = direction;
            int maxRow = ground.Height - 1;
            int maxCol = ground.Width - 1;
            if (direction != null)
            {
                int tmpRow = Row + direction.DeltaRow;
                if (tmpRow < 0)
                {
                    tmpRow = Math.Abs(tmpRow);
                    direction.DeltaRow = -direction.DeltaRow;
                }
                else if (tmpRow > maxRow)
                {
                    tmpRow = maxRow * 2 - tmpRow;
                    direction.DeltaRow = -direction.DeltaRow;
                }

                int tmpColumn = Column + direction.DeltaColumn;
                if (tmpColumn < 0)
                {
                    tmpColumn = Math.Abs(tmpColumn);
                    direction.DeltaColumn = -direction.DeltaColumn;
                }
                else if (tmpColumn > maxCol)
                {
                    tmpColumn = maxCol * 2 - tmpColumn;
                    direction.DeltaColumn = -direction.DeltaColumn;
                }

                Row = Convert.ToUInt16(tmpRow);
                Column = Convert.ToUInt16(tmpColumn);

                _logger.DebugFormat("Now at Row={0}, Column={1}", Row, Column);
                _logger.Debug("Exiting Move");
            }
            return result;
        }

        public static Position CreateRandomPosition()
        {
            if (ground==null)
                ground = ServiceRegistry.GetPreferredService<Ground>();

            int row = RandomGen.Next(0, ground.Height);
            int column = RandomGen.Next(0, ground.Width);

            return new Position(row, column);
        }

        public static bool operator == (Position a, Position b)
        {
            // If both are null, or both are same instance, return true.
            if (System.Object.ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if (((object)a == null) || ((object)b == null))
            {
                return false;
            }

            return (a.Row == b.Row && a.Column == b.Column);
        }
        public static bool operator !=(Position a, Position b)
        {
            return !(a == b);
        }

        public override bool Equals(object obj)
        {
            return (this == ((Position)obj));
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            return "(" + Column.ToString() + "," + Row.ToString() + ")";
        }
    }
}
