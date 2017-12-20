using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Common.Threading;
using Common.Messages;
using Common.Communicator;
using Objects;

namespace Player
{
    public class Player
    {
        public Int16 PlayerID { get; set; }
        public string Name { get; set; }
        public Int16 Age { get; set; }
        public bool Gender { get; set; }
        public IPEndPoint FightManagerEP { get; set; }
        public IPEndPoint BalloonManagerEP { get; set; }
        public IPEndPoint WaterManagerEP { get; set; }
        public Int16 HitByAmountOfWater;
        //public Int16 NumberOfCurrentBalloons;
        public List<PlayerLocation> LocationsList;
        public List<WaterBalloon> BalloonsList;
        //public List<Location>[] PlayersLocationsList;
        public List<WaterFightGame> InprocessFightsList;
        public List<Objects.Player> PlayersList;
        public List<WaterFightGame> joinedFightsList;
        
        public Player(string name, Int16 age, bool gender, Location location, IPEndPoint fightManagerEP, IPEndPoint balloonManagerEP, IPEndPoint waterManagerEP)
        {
            Name = name;
            Age = age;
            Gender = gender;
            HitByAmountOfWater = 0;
            LocationsList = new List<PlayerLocation>();
            //PlayersLocationsList = new List<Location> [Int16.MaxValue];
            PlayerLocation playerLocation = new PlayerLocation(location);
            LocationsList.Add(playerLocation);
            BalloonsList = new List<WaterBalloon>();
            //NumberOfCurrentBalloons = 0;
            joinedFightsList = new List<WaterFightGame>();
            InprocessFightsList = new List<WaterFightGame>();
            PlayersList = new List<Objects.Player>();
            FightManagerEP = fightManagerEP;
            BalloonManagerEP = balloonManagerEP;
            WaterManagerEP = waterManagerEP; 
        }

        public int NumberOfCurrentBalloons()
        {
            return BalloonsList.Count;
        }

        public void FillBalloon(WaterBalloon balloon, Int16 percentage)
        {
            foreach (WaterBalloon b in BalloonsList)
                if (b == balloon)
                    b.IncreasePercentFilled(percentage);
        }

        public Int16 IncreaseAmountOfWater(Int16 amount)
        {
            return HitByAmountOfWater += amount;
        }

        public Location GetCurrentLocation()
        {
            return LocationsList.Last().Location;
        }

        public void MoveToNewLocation(Location newLocation, DateTime time)
        {
            PlayerLocation location = new PlayerLocation(newLocation, time);
            LocationsList.Add(location);
        }

        public bool GetNewBalloon(WaterBalloon balloon)
        {
            if (BalloonsList.Count < 4)
            {
                BalloonsList.Add(balloon);
                return true;
            }
            return false;
        }

        public void DecrementNumberOfBalloons(int balloonID)
        {
            WaterBalloon balloon = null;
            foreach (WaterBalloon b in BalloonsList)
                if (b.BalloonID == balloonID)
                {
                    balloon = b;
                    break;
                }
            if (balloon != null)
               BalloonsList.Remove(balloon);
            
        }

        public WaterBalloon FindBalloon(int balloonID)
        {
            foreach (WaterBalloon b in BalloonsList)
                if (b.BalloonID == balloonID)
                    return b;
            return null;
        }

        public void NewFight(int fightID, Int16 opponentID, Location opponentLocation, Int16 water)
        {
            WaterFightGame fight = FindJoinedFight(fightID);
            if (fight == null)
            {
                fight = new WaterFightGame(fightID);
                joinedFightsList.Add(fight);
            }
            fight.AddPlayer(NewOpponent(opponentID, opponentLocation));
            IncreaseAmountOfWater(water);
        }

        public Objects.Player NewOpponent(Int16 opponentID, Location opponentLocation)
        {
            Objects.Player newPlayer = FindPlayer(opponentID);
            if (newPlayer != null)
                newPlayer.MoveToNewLocation(opponentLocation);
            else
            {
                newPlayer = new Objects.Player(opponentID);
                newPlayer.MoveToNewLocation(opponentLocation);
                PlayersList.Add(newPlayer);
            }
            return newPlayer;
        }

        public int NumberOfFightParticipated()
        {
            return joinedFightsList.Count;
        }

        public void UpdatePlayerLocation(Int16 playerID, int[,] locationList)
        {

            Objects.Player player = FindPlayer(playerID);
            if(player != null)    
                player = new Objects.Player(playerID);
            
            Location newLocation;
            for (int i = 0; i < locationList.Length; i++)
            {
                newLocation = new Location(locationList[i, 0], locationList[i, 1]);
                player.MoveToNewLocation(newLocation);
            }
        }

        public void UpdateCurrentPlayersList(Int16[] list)
        {
            Objects.Player player;
            foreach (Int16 id in list)
            {
                player = FindPlayer(id);
                if (player == null)
                {
                    player = new Objects.Player(id);
                    PlayersList.Add(player);
                }
            }
        }

        public void ClearInprocessFightsList()
        {
            InprocessFightsList.Clear();
        }

        public void UpdateInprocessFightsList(int[] figthList)
        {
            foreach (int i in figthList)
            {
                WaterFightGame fight = FindFight(i);
                if (fight == null)
                {
                    fight = new WaterFightGame(i);
                    InprocessFightsList.Add(fight);
                }
            }
        }

        public void ClearPlayersOfSpecificFight(int fightID)
        {
            WaterFightGame fight = FindFight(fightID);
            if (fight != null)
                fight.PlayerList.Clear();
        }

        public void UpdatePlayersOfSpecificFight(int fightID, Int16[] playerList)
        {
            WaterFightGame fight = FindFight(fightID);
            if (fight != null)
            {
                foreach (Int16 id in playerList)
                {
                    Objects.Player player = fight.FindPlayer(id);
                    if (player == null)
                    {
                        player = new Objects.Player(id);
                        fight.PlayerList.Add(player);
                    }
                }
            }
        }

        public void removePlayerID()
        {
            PlayerID = 0;
        }

        private WaterFightGame FindJoinedFight(int fightID)
        {
            foreach (WaterFightGame f in joinedFightsList)
                if (f.FightID == fightID)
                    return f;
            return null;
        }

        public WaterFightGame FindFight(int fightID)
        {
            foreach (WaterFightGame f in InprocessFightsList)
                if (f.FightID == fightID)
                    return f;
            return null;
        }

        public Objects.Player FindPlayer(Int16 id)
        {
            foreach (Objects.Player player in PlayersList)
                if (player.PlayerID == id)
                    return player;
            return null;
        }
    }
}
