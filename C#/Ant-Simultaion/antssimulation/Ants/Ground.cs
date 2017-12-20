using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

using Vitruvian.Distribution;
using Vitruvian.Logging;
using Vitruvian.Services;
using Vitruvian.Distribution.SyncPatterns;
using Vitruvian.Serialization;
using Vitruvian.Serialization.Xml;
using Vitruvian.Distribution.SyncPatterns.Mirrors;

namespace Ants
{
    [OptimisticSerialization]
    [DistributionInfo]
    public class Ground : DistributedService
    {
        #region Private Data Members

        private static Logger _logger = Logger.GetLogger(typeof(Ground));

        private int height = 0;
        private int width = 0;
        private int numberOfFoodPiles = 4;
        private int maxFoodInAPile = 500;

        private Food[,] food = null;

        #endregion

        #region Constructors
        public Ground() : base ("Ground") { }

        public Ground(int width, int height) : this()
        {
            this.height = height;
            this.width = width;
        }
        #endregion

        #region Public properties and methods for setup values

        // The properties in this region are set when the ground object is created (via the ServiceRegistry loading process)
        // They should not be changed after start up

        public int Height
        {
            get { return height; }
            set { height = value; }
        }

        public int Width
        {
            get { return width; }
            set { width = value; }
        }

        public int NumberOfFoodPiles
        {
            get { return numberOfFoodPiles; }
            set { numberOfFoodPiles = value; }
        }

        public int MaxFoodInAPile
        {
            get { return maxFoodInAPile; }
            set { maxFoodInAPile = value; }
        }

        #endregion

        #region Public Methods with SyncPatterns

        [SyncPattern("HostOnly")]
        public void SetFood(int row, int column, Food value)
        {
            if (row < height && column < width)
                food[row, column] = value;
        }

        [SyncPattern("RPC")]
        virtual public bool ContainsFood(int row, int column)
        {
            _logger.Debug("Entering ContainsFood");

            bool result = false;
            if (row>=0 && row < height && column>=0 && column < width && food[row, column] != null)
            {
                lock (food[row, column])
                {
                    result = !food[row, column].IsGone;
                }
            }

            _logger.DebugFormat("Exiting ContainsFood, with result={0}", result);

            return result;
        }

        [SyncPattern("RPC")]
        virtual public Food PeekAtFood(int row, int column)
        {
            _logger.Debug("Entering PeekAtFood");

            Food result = null;
            if (row < height && column < width && food[row, column] != null)
                    result = food[row, column];

            _logger.Debug("Exiting PeekAtFood");

            return result;
        }

        [SyncPattern("RPC")]
        virtual public Food PickupFood(int row, int column, int amount)
        {
            _logger.Debug("Entering PickUpFood");

            Food result = null;
            if (row < height && column < width && food[row, column] != null)
            {
                lock (food[row, column])
                {
                    result = food[row, column].Extract(amount);
                    if (food[row, column].IsGone)
                        food[row, column] = null;
                }
            }

            _logger.Debug("Exiting PickUpFood");

            return result;
        }

        #endregion

        #region Init and Clean Methods
        public override void Init()
        {
            SetupFood();
        }
        #endregion

        #region Private Methods

        private void SetupFood()
        {
            _logger.Debug("Entering SetupFood");

            // Create the food grid
            food = new Food[height, width];
            for (int row = 0; row < height; row++)
                for (int col = 0; col < width; col++)
                    food[row, col] = null;

            // Place the food
            for (int i = 0; i < numberOfFoodPiles; i++)
            {
                int row = 0;
                int column = 0;

                // Find an empty cell
                do
                {
                    row = RandomGen.Next(0, height);
                    column = RandomGen.Next(0, width);
                } while (food[row, column] != null);

                food[row, column] = new Food(RandomGen.Next(maxFoodInAPile / 2, maxFoodInAPile));
            }

            _logger.Debug("Entering SetupFood");
        }

        #endregion
    }
}
