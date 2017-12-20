using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Threading;

using Vitruvian.Logging;
using Vitruvian.Distribution;
using Vitruvian.Distribution.SyncPatterns;
using Vitruvian.Distribution.SyncPatterns.Mirrors;
using Vitruvian.Serialization;
using Vitruvian.Serialization.Formatters;
using Vitruvian.Services;

namespace Ants
{
    [OptimisticSerialization]
    [DistributionInfo]
    public class Colony : DistributedService
    {
        #region Private Data Members
        private static Logger _logger = Logger.GetLogger(typeof(Colony));

        private SimulationSettings settings = null;
        private Ground ground = null;

        private bool keepGoing = false;
        private Thread runThread = null;

        private static Color[] possibleColors = new Color[]
            {
                Color.Aqua,
                Color.Azure,
                Color.Blue,
                Color.Brown,
                Color.Chartreuse,
                Color.Coral,
                Color.CornflowerBlue,
                Color.Cornsilk,
                Color.Crimson,
                Color.Cyan,
                Color.DarkBlue,
                Color.DarkCyan,
                Color.DarkGoldenrod,
                Color.DarkGray,
                Color.DarkGreen,
                Color.DarkKhaki,
                Color.DarkOliveGreen,
                Color.DarkOrange,
                Color.DarkOrchid,
                Color.DarkRed,
                Color.DarkSalmon,
                Color.DarkSeaGreen,
                Color.DarkSlateBlue,
                Color.DarkTurquoise,
                Color.DarkViolet,
                Color.DeepPink,
                Color.DeepSkyBlue,
                Color.DimGray,
                Color.DodgerBlue,
                Color.Firebrick,
                Color.ForestGreen,
                Color.Fuchsia,
                Color.Gainsboro,
                Color.Gold,
                Color.Goldenrod,
                Color.Gray,
                Color.Green,
                Color.GreenYellow,
                Color.Honeydew,
                Color.HotPink,
                Color.IndianRed,
                Color.Indigo,
                Color.Ivory,
                Color.Khaki,
                Color.Lavender,
                Color.LavenderBlush,
                Color.LawnGreen,
                Color.LemonChiffon,
                Color.LightBlue,
                Color.LightCyan,
                Color.LightGray,
                Color.LightGreen,
                Color.LightSalmon,
                Color.LightSeaGreen,
                Color.Lime,
                Color.LimeGreen,
                Color.Magenta,
                Color.Maroon,
                Color.MediumAquamarine,
                Color.MediumBlue,
                Color.MediumOrchid,
                Color.MediumPurple,
                Color.MediumSeaGreen,
                Color.MediumSlateBlue,
                Color.MediumSpringGreen,
                Color.MediumTurquoise,
                Color.MediumVioletRed,
                Color.MidnightBlue,
                Color.MintCream,
                Color.MistyRose,
                Color.Moccasin,
                Color.NavajoWhite,
                Color.Navy,
                Color.OldLace,
                Color.Olive,
                Color.Orange,
                Color.OrangeRed,
                Color.Orchid,
                Color.PaleGoldenrod,
                Color.PaleGreen,
                Color.PaleTurquoise,
                Color.PaleVioletRed,
                Color.PaleGreen,
                Color.PaleVioletRed,
                Color.PapayaWhip,
                Color.PeachPuff,
                Color.Peru,
                Color.Pink,
                Color.Plum,
                Color.Purple,
                Color.Red
            };

        #endregion

        #region Constructors

        public Colony() : base("Colony")
        {
            ColonyColor = Color.Empty;             // Default color
        }

        #endregion

        #region Public Properties and Methods with SyncPatterns

        [SyncPattern("RPC")]
        [ColorFormatter]
        virtual public Color ColonyColor { get; set; }

        [SyncPattern("RPC")]
        virtual public List<Ant> Ants { get; set; }

        [SyncPattern("RPC")]
        [PheromoneLayerFormatter]
        virtual public PheromoneLayer PheromoneLayer { get; set; }

        [SyncPattern("RPC")]
        virtual public Nest Home { get; set; }

        [SyncPattern("RPC")]
        virtual public int InitialAntCount { get; set; }

        #endregion

        #region IService methods

        [SyncPattern("HostOnly")]
        public override void Init()
        {
            runThread = new Thread(runColony);
            keepGoing = true;
            runThread.Start();
        }

        public override void Cleanup()
        {
            if (runThread!=null)
            {
                keepGoing = false;
                int remainingTime = 10000;
                while (runThread.IsAlive && remainingTime > 0)
                {
                    Thread.Sleep(100);
                    remainingTime -= 100;
                }
            }
        }

        #endregion

        /// <summary>
        /// This method setups the colony and then periodically move the ants and decays the pheromone
        /// </summary>
        private void runColony()
        {
            Setup();

            while (keepGoing)
            {
                _logger.Debug("Top of the run loop");

                // Get the ground and common setting again from the registry, in case they dissappear (become disconnected)
                ground = ServiceRegistry.GetPreferredService<Ground>();
                settings = ServiceRegistry.GetPreferredService<SimulationSettings>();
                int sleepTime = (settings != null) ? settings.MovementInterval : 1000;

                if (ground != null && settings != null)
                {
                    _logger.Debug("Have proxies for ground and settings");

                    try
                    {
                        MoveAnts();
                        PheromoneLayer.Decay();
                    }
                    catch (RemoteException ex)
                    {
                        if (_logger.IsErrorEnabled)
                            _logger.Error(ex);
                    }
                }

                // Sleep for the movement interval if there is a setting; otherwise sleep for a second
                Thread.Sleep(sleepTime);
            }
        }

        private void Setup()
        {
            _logger.Debug("Entering Setup");

            if (ServiceRegistry.WaitForService<Ground>(60000))
            {
                _logger.Debug("Ground service exists in the registry");

                ground = ServiceRegistry.GetPreferredService<Ground>();
                if (ColonyColor.Equals(Color.Empty))
                    ColonyColor = FindAvailableColor();

                Ants = new List<Ant>();

                Home = new Nest(Position.CreateRandomPosition());

                PheromoneLayer = new PheromoneLayer(ground.Height, ground.Width);

                for (int i = 0; i < InitialAntCount; i++)
                    Add(new Ant(this, Position.CreateRandomPosition()));
            }

            _logger.Debug("Exiing Setup");
        }

        private Color FindAvailableColor()
        {
            List<Colony> colonies = ServiceRegistry.GetServices<Colony>();

            Color result = Color.Black;
            List<Color> availableColors = new List<Color>();
            foreach (Color color in possibleColors)
            {
                bool found = false;
                foreach (Colony colony in colonies)
                {
                    if (colony.ColonyColor.Equals(color))
                    {
                        found = true;
                        break;
                    }
                }

                if (!found)
                    availableColors.Add(color);
            }

            if (availableColors.Count > 0)
            {
                int choosenColorIdx = RandomGen.Next(0, availableColors.Count);
                result = availableColors[choosenColorIdx];
            }

            return result;
        }


        private void Add(Ant ant)
        {
            if (ant != null)
                Ants.Add(ant);
        }

        private void MoveAnts()
        {
            _logger.Debug("Entering MoveAnts");

            foreach (Ant ant in Ants)
                ant.Move();

            _logger.Debug("Exiting MoveAnts");

        }

    }
}
