using System;
using System.Collections.Generic;
using System.Text;

using Vitruvian.Distribution;
using Vitruvian.Logging;
using Vitruvian.Services;
using Vitruvian.Serialization;
using Vitruvian.Distribution.SyncPatterns;

namespace Ants
{
    [OptimisticSerialization]
    public class Pheromone
    {
        private static Logger _logger = Logger.GetLogger(typeof(Pheromone));

        private int level = 0;

        public Pheromone()
        { }

        public Pheromone(int level) { this.level = level; }

		public int Level
        {
            get { return level; }
            set
            {
				try
				{
                    SimulationSettings settings = ServiceRegistry.GetPreferredService<SimulationSettings>();
                    if (settings != null)
                        level = Math.Min(value, settings.MaxPheromoneLevel);
                    else
                        level = value;

                    _logger.DebugFormat("Set level to {0}", level);
				}
				catch (RemoteException ex)
				{
					if (_logger.IsDebugEnabled)
						_logger.Debug(ex);
				}
            }
        }

        public void Decay()
        {
            _logger.Debug("Entering Decay");

			try
			{
                SimulationSettings settings = ServiceRegistry.GetPreferredService<SimulationSettings>();
                if (settings != null)
                {
                    if (level > settings.PheromoneDecayAmount)
                        level = Convert.ToUInt16(level - settings.PheromoneDecayAmount);
                    else
                        level = 0;
                    _logger.DebugFormat("Decaying level to {0}", level);
                }
			}
			catch (RemoteException ex)
			{
				if (_logger.IsDebugEnabled)
					_logger.Debug(ex);
			}

            _logger.Debug("Entering Decay");

        }
    }
}
