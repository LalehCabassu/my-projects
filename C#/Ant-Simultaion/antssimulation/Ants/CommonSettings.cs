using System;
using System.Collections.Generic;
using System.Text;

using Vitruvian.Serialization;
using Vitruvian.Logging;

namespace Ants
{
    [OptimisticSerialization]
    public class CommonSettings
    {
        #region Private Data Members

        private Logger _logger = Logger.GetLogger(typeof(CommonSettings));

        private int defaultNumberOfAnts = 30;

        private int movementInterval = 100;
        private int antPayload = 10;
        private int initPheromone = 150;
        private int pheromoneDecayAmount = 2;
        private int maxPheromoneLevel = 300;

        private float groundBorder = 3.0F;
        private float minSymbolSize = 6.0F;
        private float pheromoneRelativeMarkerSize = 0.75F;
        private float minPheromoneMarkerSize = 3.0F;
        private int displayRefreshInterval = 100;

        private int antBiasTowardsHome = 80;
        private int antDirectionSteadiness = 60;

        private bool showPheromone = true;

        #endregion

        #region Constructors

        public CommonSettings() { }

        #endregion

        #region Startup Properties

        public int DefaultNumberOfAnts
        {
            get { return defaultNumberOfAnts; }
            set { defaultNumberOfAnts = value; }
        }

        public int MovementInterval
        {
            get { return movementInterval; }
            set { movementInterval = value; }
        }

        public int AntPayload
        {
            get { return antPayload; }
            set { antPayload = value; }
        }

        public int InitPheromone
        {
            get { return initPheromone; }
            set { initPheromone = value; }
        }

        public int PheromoneDecayAmount
        {
            get { return pheromoneDecayAmount; }
            set { pheromoneDecayAmount = value; }
        }

        public int MaxPheromoneLevel
        {
            get { return maxPheromoneLevel; }
            set { maxPheromoneLevel = value; }
        }

        public float GroundBorder
        {
            get { return groundBorder; }
            set { groundBorder = value; }
        }

        public float MinSymbolSize
        {
            get { return minSymbolSize; }
            set { minSymbolSize = value; }
        }

        public float PheromoneRelativeMarkerSize
        {
            get { return pheromoneRelativeMarkerSize; }
            set { pheromoneRelativeMarkerSize = value; }
        }

        public float MinPheromoneMarkerSize
        {
            get { return minPheromoneMarkerSize; }
            set { minPheromoneMarkerSize = value; }
        }

        public int DisplayRefreshInterval
        {
            get { return displayRefreshInterval; }
            set { displayRefreshInterval = value; }
        }

        public int AntBiasTowardsHome
        {
            get { return antBiasTowardsHome; }
            set { antBiasTowardsHome = value; }
        }

        public int AntDirectionSteadiness
        {
            get { return antDirectionSteadiness; }
            set { antDirectionSteadiness = value; }
        }

        public bool ShowPheromone
        {
            get { return showPheromone; }
            set { showPheromone = value; }
        }

        #endregion
    }
}
