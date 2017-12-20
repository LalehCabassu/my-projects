using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Common.Threading;
using Common.Messages;
using Common.Communicator;
using Objects;


// Defined for Balloon Manager
namespace BalloonManager
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

        public int GetNewBalloon(WaterBalloon.PossibleSize balloonSize, WaterBalloon.PossibleColor balloonColor)
        {
            if (NumberOfCurrentBalloons < 4)
            {
                WaterBalloon balloon = new WaterBalloon(balloonSize, balloonColor);
                BalloonsList.Add(balloon);
                NumberOfCurrentBalloons++;
                return balloon.BalloonID;
            }
            return -1;
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
    }
}
