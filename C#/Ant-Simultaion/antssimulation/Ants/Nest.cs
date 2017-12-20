using System;
using System.Collections.Generic;
using System.Text;

using Vitruvian.Distribution;
using Vitruvian.Serialization;
using Vitruvian.Distribution.SyncPatterns;
using System.ComponentModel;

namespace Ants
{
    [OptimisticSerialization]
    public class Nest
    {
        private Food stockPile = new Food();
        private Position location = null;

        public Nest() { }

        public Nest(Position location)
        {
            this.location = location;
        }

		public Position Location
        {
            get { return location; }
            set { location = value; }
        }

        public Food StockPile
        {
            get { return stockPile; }
            set { stockPile = value; }
        }
    }
}
