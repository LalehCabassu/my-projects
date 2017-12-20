using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
    public class Player
    {
        private Int16 playerID;
        public Int16 PlayerID
        {
            get { return playerID; }
            set { playerID = value; }
        }

        private string myName;
        public string Name
        {
            get { return myName; }
            set { myName = value;}
        }
        private Int16 myAge;
        public Int16 Age
        {
            get { return myAge; }
            set { myAge = value; }
        }

        private bool myGender;
        public bool Gender
        {
            get { return myGender; }
            set { myGender = value; }
        }

        public Int16 HitByAmountOfWater;
        //public Int16 NumberOfCurrentBalloons;
        public List<PlayerLocation> LocationsList;
        public List<WaterBalloon> BalloonsList;
        private List<WaterFightGame> FightsList;
        private List<int> inprocessFightsList;
        private Dictionary<Int16, List<Location>> opponentsLocationsList;
        private Dictionary<int, List<Int16>> fightPlayersList;
        
        //For Balloon Manager
        //public Player(Int16 Id)
        //{
          //  BalloonsList = new List<WaterBalloon>();
           // NumberOfCurrentBalloons = 0;
            //LocationsList = new List<PlayerLocation>();
        //}

        public Player(Int16 playerID)
        {
            PlayerID = playerID;
            //NumberOfCurrentBalloons = 0;
            BalloonsList = new List<WaterBalloon>();
            LocationsList = new List<PlayerLocation>();
        }

        public Player(string name, Int16 age, bool gender, Location location)
        {
            myName = name;
            myAge = age;
            myGender = gender;
            HitByAmountOfWater = 0;
            LocationsList = new List<PlayerLocation>();
            opponentsLocationsList = new Dictionary<Int16, List<Location>>();
            PlayerLocation playerLocation = new PlayerLocation(location);
            LocationsList.Add(playerLocation);
            BalloonsList = new List<WaterBalloon>();
            //NumberOfCurrentBalloons = 0;
            FightsList = new List<WaterFightGame>();
            inprocessFightsList = new List<int>();
            fightPlayersList = new Dictionary<int, List<Int16>>();

        }

        public int NumberOfCurrentBalloons()
        {
            return BalloonsList.Count;
        }

        public int GetNewBalloon(int id, WaterBalloon.PossibleSize balloonSize, WaterBalloon.PossibleColor balloonColor)
        {
            if (BalloonsList.Count < 4)
            {
                WaterBalloon balloon = new WaterBalloon(id, balloonSize, balloonColor);
                BalloonsList.Add(balloon);
                return balloon.BalloonID;
            }
            return -1;
        }

        public void EmptyBalloonList()
        {
            BalloonsList.Clear();
        }

        public void FillBalloon(WaterBalloon balloon, Int16 percentage)
        {
            foreach (WaterBalloon b in BalloonsList)
                if (b == balloon)
                    b.IncreasePercentFilled(percentage);
        }

        public void FillBalloon(int balloonID, Int16 amountOfWater)
        {
            WaterBalloon balloon = FindBalloon(balloonID);
            if (balloon != null)
                balloon.AmountOfWater = amountOfWater;
        }

        public WaterBalloon FindBalloon(int balloonID)
        {
            foreach (WaterBalloon b in BalloonsList)
            {
                if (b.BalloonID == balloonID)
                    return b;
            }
            return null;
        }

        public Int16 IncreaseAmountOfWater(Int16 amount)
        {
            return HitByAmountOfWater += amount;
        }

        public Location GetCurrentLocation()
        {
            return LocationsList.Last().Location;
        }

        public Int16 IncreaseAmountOfWater(int balloonID, Int16 percentFilled, WaterBalloon.PossibleSize size)
        {
            WaterBalloon balloon = FindBalloon(balloonID);
            if (balloon != null)
            {
                balloon.Size = size;
                balloon.IncreasePercentFilled(percentFilled);
                return balloon.AmountOfWater;
            }
            return 0;
        }

        public void MoveToNewLocation(Location newLocation)
        {
            PlayerLocation location = new PlayerLocation(newLocation);
            LocationsList.Add(location);
        }

        public void MoveToNewLocation(Location newLocation, DateTime time)
        {
            if (LocationsList.Last().Location.X != newLocation.X || LocationsList.Last().Location.Y != newLocation.Y)
            {
                PlayerLocation location = new PlayerLocation(newLocation, time);
                LocationsList.Add(location);
            }
        }

        public bool GetNewBalloon(WaterBalloon balloon)
        {
            if (BalloonsList.Count < 4 && !hasBalloon(balloon.BalloonID))
            {
                BalloonsList.Add(balloon);
                return true;
            }
            return false;
        }

        private bool hasBalloon(int balloonID)
        {
            foreach (WaterBalloon b in BalloonsList)
                if (b.BalloonID == balloonID)
                    return true;
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

        public void NewFight(WaterFightGame newFight)
        {
            FightsList.Add(newFight);
        }

        public int NumberOfFightParticipated()
        {
            return FightsList.Count;
        }

        public void updateOpponentLocationList(int[,] playersLocationList)
        {
            Location newLocation;
            for (int i = 0; i < playersLocationList.GetLength(0); i++)
            {
                newLocation = new Location(playersLocationList[i, 1], playersLocationList[i, 2]);
                if (!opponentsLocationsList.ContainsKey((Int16)playersLocationList[i, 0]))
                {
                    List<Location> newLocationList = new List<Location>();
                    newLocationList.Add(newLocation);
                    opponentsLocationsList.Add((Int16)playersLocationList[i, 0], newLocationList);
                }
                else
                    opponentsLocationsList[(Int16)playersLocationList[i, 0]].Add(newLocation);
            }
        }

        public void addToInprocessFightsList(int[] figthList)
        {
            foreach (int i in figthList)
            {
                inprocessFightsList.Add(i);
            }
        }

        public void emptyInprocessFightsList()
        {
            inprocessFightsList.Clear();
        }

        public void addToFightPlayersList(int fightID, Int16[] playerList)
        {
            foreach (Int16 i in playerList)
            {
                fightPlayersList[fightID].Add(i);
            }
        }

        public void emptyFightPlayersList(int fightID)
        {
            fightPlayersList[fightID].Clear();
        }

        public void removePlayerID()
        {
            playerID = 0;
        }
    }
}
