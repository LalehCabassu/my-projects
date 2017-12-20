using System;
using System.Collections.Generic;
using System.Text;

using Vitruvian.Serialization;
using Vitruvian.Services;
using Vitruvian.Distribution;
using Vitruvian.Distribution.SyncPatterns;
using Vitruvian.Logging;

namespace Ants
{
    [OptimisticSerialization]
    [DistributionInfo]
    public class SimulationSettings : DistributedService
    {
        #region Private Data Members

        private Logger _logger = Logger.GetLogger(typeof(SimulationSettings));

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

        public SimulationSettings() : base("CommonSettings") { }

        #endregion

        #region Public Properties Used by the Simulation, occassionally Set by the ControlSettingForm

        [SyncPattern("RPC")]
        virtual public int MovementInterval
        {
            get { return movementInterval; }
            set { movementInterval = value; }
        }

        [SyncPattern("RPC")]
        virtual public int AntPayload
        {
            get { return antPayload; }
            set { antPayload = value; }
        }

        [SyncPattern("RPC")]
        virtual public int InitPheromone
        {
            get { return initPheromone; }
            set { initPheromone = value; }
        }

        [SyncPattern("RPC")]
        virtual public int PheromoneDecayAmount
        {
            get { return pheromoneDecayAmount; }
            set { pheromoneDecayAmount = value; }
        }

        [SyncPattern("RPC")]
        virtual public int MaxPheromoneLevel
        {
            get { return maxPheromoneLevel; }
            set { maxPheromoneLevel = value; }
        }

        [SyncPattern("RPC")]
        virtual public float GroundBorder
        {
            get { return groundBorder; }
            set { groundBorder = value; }
        }

        [SyncPattern("RPC")]
        virtual public float MinSymbolSize
        {
            get { return minSymbolSize; }
            set { minSymbolSize = value; }
        }

        [SyncPattern("RPC")]
        virtual public float PheromoneRelativeMarkerSize
        {
            get { return pheromoneRelativeMarkerSize; }
            set { pheromoneRelativeMarkerSize = value; }
        }

        [SyncPattern("RPC")]
        virtual public float MinPheromoneMarkerSize
        {
            get { return minPheromoneMarkerSize; }
            set { minPheromoneMarkerSize = value; }
        }

        [SyncPattern("RPC")]
        virtual public int DisplayRefreshInterval
        {
            get { return displayRefreshInterval; }
            set { displayRefreshInterval = value; }
        }

        [SyncPattern("RPC")]
        virtual public int AntBiasTowardsHome
        {
            get { return antBiasTowardsHome; }
            set { antBiasTowardsHome = value; }
        }

        [SyncPattern("RPC")]
        virtual public int AntDirectionSteadiness
        {
            get { return antDirectionSteadiness; }
            set { antDirectionSteadiness = value; }
        }

        [SyncPattern("RPC")]
        virtual public bool ShowPheromone
        {
            get { return showPheromone; }
            set { showPheromone = value; }
        }

        #endregion

    }
}
