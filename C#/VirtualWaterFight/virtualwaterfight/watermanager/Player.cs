using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Common.Threading;
using Common.Messages;
using Common.Communicator;
using Objects;


// Defined for Water Manager
namespace WaterManager
{
    public class Player
    {
        public Int16 PlayerID { get; set; }
        public Int16 NumberOfCurrentBalloons;
        
        public List<WaterBalloon> BalloonsList;
        
        public Player(Int16 playerID)
        {
            PlayerID = playerID;
            NumberOfCurrentBalloons = 0;
            BalloonsList = new List<WaterBalloon>();
        }

        public bool GetNewBalloon(WaterBalloon balloon)
        {
            if (NumberOfCurrentBalloons < 4)
            {
                BalloonsList.Add(balloon);
                NumberOfCurrentBalloons++;
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
            if(balloon != null)
                BalloonsList.Remove(balloon);
        }

        public void EmptyBalloonList()
        {
            BalloonsList.Clear();
        }

        public Int16 IncreaseAmountOfWater(int balloonID, Int16 percentFilled, WaterBalloon.PossibleSize size)
        {
            WaterBalloon balloon = FindBalloon(balloonID);
            balloon.Size = size;
            if (balloon != null)
            {
                balloon.IncreasePercentFilled(percentFilled);
                return balloon.CalcAmountOfWater();
            }
            return 0;
        }

        public WaterBalloon FindBalloon(int balloonID)
        {
            foreach (WaterBalloon b in BalloonsList)
                if (b.BalloonID == balloonID)
                    return b;
            return null;
        }
    }
}
