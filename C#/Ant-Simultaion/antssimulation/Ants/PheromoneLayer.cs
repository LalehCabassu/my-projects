using System;
using System.Collections.Generic;
using System.Text;

using Vitruvian.Logging;
using Vitruvian.Serialization;
using Vitruvian.Distribution.SyncPatterns;

namespace Ants
{
    [OptimisticSerialization]
    public class PheromoneLayer : INotifyMethodInvoked
    {
        #region Private Data Members
        private static Logger _logger = Logger.GetLogger(typeof(PheromoneLayer));

        protected Pheromone[,] pheromone = null;
        #endregion

        #region Constructors
        public PheromoneLayer() { }

        public PheromoneLayer(int height, int width)
        {
            Height = height;
            Width = width;
            Setup();
        }
        #endregion

        #region Setup methods
        virtual public void Setup()
        {
            if (Width > 0 && Height > 0)
                pheromone = new Pheromone[Height, Width];
            else
                pheromone = null;
        }
        #endregion

        #region Public Properties and Methods
        public int Width { get; set; }

        public int Height { get; set; }

        public Pheromone GetPheromone(Position position)
        {
            return GetPheromone(position.Row, position.Column);
        }

        public Pheromone GetPheromone(int row, int column)
        {
            Pheromone result = null;
            if (pheromone != null && column>=0 && column < Width && row>=0 && row < Height)
                result = pheromone[row, column];
            return result;
        }

        public void SetPheromone(Position position, Pheromone value)
        {
            if (MethodInvoked != null)
                MethodInvoked("SetPheromone", position, value);

            if (pheromone != null && position != null &&
                position.Row < Height && position.Column < Width)
                pheromone[position.Row, position.Column] = value;
        }

        public void Decay()
        {
            _logger.Debug("Entering Decay()");

            if (MethodInvoked != null)
                MethodInvoked("Decay");

            for (int row = 0; row < Height; row++)
            {
                for (UInt32 column = 0; column < Width; column++)
                {
                    if (pheromone[row, column] != null)
                    {
                        pheromone[row, column].Decay();

                        if (pheromone[row, column].Level <= 0)
                            pheromone[row, column] = null;
                    }
                }
            }

            _logger.Debug("Existing Decay()");
        }

        #endregion

        #region INotifyMethodInvoked Members

        public event MethodInvokedHandler MethodInvoked;

        #endregion
    }
}
