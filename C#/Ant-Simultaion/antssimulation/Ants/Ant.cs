using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

using Vitruvian.Distribution;
using Vitruvian.Logging;
using Vitruvian.Services;
using Vitruvian.Serialization;
using Vitruvian.Distribution.SyncPatterns;

namespace Ants
{
    [OptimisticSerialization]
    public class Ant
    {
        private static Logger _logger = Logger.GetLogger(typeof(Ant));

        private static Direction[] directions = new Direction[8]
                                                        {
                                                          new Direction(1, 0),
                                                          new Direction(1, 1),
                                                          new Direction(0, 1),
                                                          new Direction(-1, 1),
                                                          new Direction(-1, 0),
                                                          new Direction(-1, -1),
                                                          new Direction(0, -1),
                                                          new Direction(1, -1)
                                                        };

        private Food foodInTow = null;
        private Direction lastDirection;

        delegate bool PheromoneCheck(int level, int lastPhoromone, bool foodAvailable, bool atHome, ref int strength);

        #region Constructors
        public Ant()
        {
            this.lastDirection = directions[RandomGen.Next(0, 7)];
        }

        public Ant(Colony colony, Position location) : this()
        {
            Colony = colony;
            Location = location;
        }
        #endregion

        #region Public Properties and Methods

        public Position Location { get; set; }

        [DontSerialize]
        public Colony Colony { get; set; }

        public void Move()
        {
            _logger.Debug("Entering Move");

            SimulationSettings settings = ServiceRegistry.GetPreferredService<SimulationSettings>();
            if (settings != null)
            {
                if (foodInTow != null)
                {
                    _logger.DebugFormat("Has a food in tow and currently at {0}, {1}", Location.Row, Location.Column);

                    Position oldLocation = new Position(Location.Row, Location.Column);

                    int strength = 0;
                    Direction pheromoneDirection = FindDirectionOfMostPheromone(ref strength);

                    if (pheromoneDirection != null)
                        _logger.DebugFormat("Have a pheromeDirection, {0}, {1}", pheromoneDirection.DeltaRow, pheromoneDirection.DeltaColumn);

                    if (pheromoneDirection != null && RandomGen.Next(0, settings.MaxPheromoneLevel * 2) < strength)
                        lastDirection = Location.Move(pheromoneDirection);
                    else if (RandomGen.Next(0, 100) < settings.AntBiasTowardsHome)
                        MoveTowardsHome();
                    else
                        Wander();

                    if (oldLocation != Location)
                        DropPheromone(oldLocation);
                }
                else
                {
                    int strength = 0;
                    Direction pheromoneDirection = FindDirectionOfLeastPheromone(ref strength);

                    if (pheromoneDirection != null && RandomGen.Next(0, settings.InitPheromone) < strength * 2)
                        lastDirection = Location.Move(pheromoneDirection);
                    else
                        Wander();

                    try
                    {
                        Ground ground = ServiceRegistry.GetPreferredService<Ground>();

                        // Pick food if there some
                        if (ground.ContainsFood(Location.Row, Location.Column))
                        {
                            foodInTow = ground.PickupFood(Location.Row, Location.Column, settings.AntPayload);
                            lastDirection = new Direction(0, 0);
                        }
                    }
                    catch (RemoteException ex)
                    {
                        if (_logger.IsErrorEnabled)
                            _logger.Error(ex);
                    }
                }

                if (foodInTow != null && Location == Colony.Home.Location)
                {
                    Colony.Home.StockPile.Add(foodInTow);
                    foodInTow = null;
                    lastDirection = new Direction(0, 0);
                }
            }
            _logger.Debug("Existing Move");
        }
        #endregion

        #region Private Methods
        private Direction FindDirectionOfMostPheromone(ref int strength)
        {
            return FindPheromoneDirection(0, CheckForMorePheromone, ref strength);
        }

        private Direction FindDirectionOfLeastPheromone(ref int strength)
        {
            return FindPheromoneDirection(Int32.MaxValue, CheckForLessPheromone, ref strength);
        }

        private Direction FindPheromoneDirection(int lastPheromoneLevel, PheromoneCheck check, ref int strength)
        {
            _logger.Debug("Entering FindPheromoneDirection");

            Ground ground = ServiceRegistry.GetPreferredService<Ground>();

            Direction result = null;
            strength = 0;

            for (int rowDelta = -1; rowDelta <= 1 && strength != int.MaxValue; rowDelta++)
                for (int columnDelta = -1; columnDelta <= 1 && strength != int.MaxValue; columnDelta++)
                {
                    if ((rowDelta != 0 || columnDelta != 0) &&
                        (rowDelta != -lastDirection.DeltaRow || columnDelta != -lastDirection.DeltaColumn))
                    {
                        int possRow = Location.Row + rowDelta;
                        int possColumn = Location.Column + columnDelta;
                        Pheromone pheromone = Colony.PheromoneLayer.GetPheromone(possRow, possColumn);
                        int level = (pheromone == null) ? (int) 0 : pheromone.Level;

                        // Check the ground for food that the current location
                        bool theresFoodHere = ground.ContainsFood(possRow, possColumn);

                        _logger.DebugFormat("There Food Here: {0}", theresFoodHere);

                        if (check(  level,
                                    lastPheromoneLevel,
                                    theresFoodHere,
                                    (Colony.Home.Location.Row == possRow && Colony.Home.Location.Column == possColumn), ref strength))
                        {
                            _logger.DebugFormat("Found pheromone in direction {0}, {1}", rowDelta, columnDelta);

                            lastPheromoneLevel = level;
                            result = new Direction(rowDelta, columnDelta);
                        }
                    }
                }

            _logger.Debug("Exiting FindPheromoneDirection");
            return result;
        }

        private bool CheckForMorePheromone(int level, int lastPheromone, bool foodAvailable, bool atHome, ref int strength)
        {
            bool result = false;
            if (atHome)
            {
                result = true;
                strength = int.MaxValue;
            }
            else if (level > lastPheromone)
            {
                result = true;
                strength = level;
            }

            return result;
        }

        private bool CheckForLessPheromone(int level, int lastPheromone, bool foodAvailable, bool atHome, ref int strength)
        {
            bool result = false;
            if (foodAvailable)
            {
                result = true;
                strength = int.MaxValue;
            }
            else if (level < lastPheromone && level != 0)
            {
                result = true;
                strength = level;
            }
            return result;
        }

        private void MoveTowardsHome()
        {
            _logger.Debug("Entering Move towards home");

            int tmpRow = 0;
            int tmpColumn = 0;

            if (Colony.Home.Location.Row > Location.Row)
                tmpRow = 1;
            else if (Colony.Home.Location.Row < Location.Row)
                tmpRow = -1;

            if (Colony.Home.Location.Column > Location.Column)
                tmpColumn = 1;
            else if (Colony.Home.Location.Column < Location.Column)
                tmpColumn = -1;

            // Move and remember the direction
            lastDirection = Location.Move(new Direction(tmpRow, tmpColumn));

            _logger.Debug("Exiting MoveTowardsHome");
        }

        private void Wander()
        {
            _logger.Debug("Entering Wander");

            SimulationSettings settings = ServiceRegistry.GetPreferredService<SimulationSettings>();

            int newDirectionIndex = LookupDirectionsIndex(lastDirection);

            // Choose a next some of the time
            if (RandomGen.Next(0, 100) > settings.AntDirectionSteadiness)
            {
                newDirectionIndex += RandomGen.Next(0, 1) * 2 - 1;
                if (newDirectionIndex >= directions.Length)
                    newDirectionIndex = 0;
                else if (newDirectionIndex < 0)
                    newDirectionIndex = directions.Length - 1;
            }

            // Move and remember the direction
            lastDirection = Location.Move(directions[newDirectionIndex]);

            _logger.Debug("Entering Wander");
        }

        private void DropPheromone(Position position)
        {
            _logger.Debug("Entering DropPheromone");

            SimulationSettings settings = ServiceRegistry.GetPreferredService<SimulationSettings>();
            Pheromone pheromone = Colony.PheromoneLayer.GetPheromone(position);
            if (pheromone == null)
                pheromone = new Pheromone(settings.InitPheromone);
            else
                pheromone.Level += settings.InitPheromone;

            Colony.PheromoneLayer.SetPheromone(position, pheromone);

            _logger.Debug("Exiting DropPheromone");
        }

        private int LookupDirectionsIndex(Direction direction)
        {
            int result = -1;
            for (int i = 0; i < directions.Length && result == -1; i++)
            {
                if (directions[i].DeltaColumn == direction.DeltaColumn &&
                    directions[i].DeltaRow == direction.DeltaRow)
                    result = i;
            }
            if (result == -1) result = 0; ;
            return result;
        }
        #endregion
    }
}
