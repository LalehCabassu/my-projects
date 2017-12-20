using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Common;
using Common.Messages;
using Common.Threading;
using Common.Communicator;
using Objects;

namespace WaterManager
{
    public class WaterManager
    {
        public Int16 WaterManagerID = Int16.MaxValue - 2;
        public List<Player> PlayerList;

        public IPEndPoint FightManagerEP;
        public IPEndPoint BalloonManagerEP;

        public WaterManager(IPEndPoint fightManagerEP, IPEndPoint balloonManagerEP)
        {
            PlayerList = new List<Player>();
            FightManagerEP = fightManagerEP;
            BalloonManagerEP = balloonManagerEP;
        }

        public void AddNewPlayer(Int16 playerID)
        {
            Player p = FindPlayer(playerID);
            if (p == null)
            {
                p = new Player(playerID);
                PlayerList.Add(p);
            }
            else
            {
                p.EmptyBalloonList();
            }
        }

        public void RemovePlayer(Int16 playerID)
        {
            Player player = null;
            foreach (Player p in PlayerList)
                if (p.PlayerID == playerID)
                {
                    player = p;
                    break;
                }
            if(player != null)
                PlayerList.Remove(player);
        }

        public void RemoveBalloon(Int16 playerID, int balloonID)
        {
            Player p = FindPlayer(playerID);
            if (p != null)
                p.DecrementNumberOfBalloons(balloonID);
        }

        public void AddBalloon(Int16 playerID, int balloonID)
        {
            Player currentPlayer = FindPlayer(playerID);
            if (currentPlayer != null)
            {
                WaterBalloon newBalloon = new WaterBalloon(balloonID);
                currentPlayer.GetNewBalloon(newBalloon);
            }
        }

        public Player FindPlayer(Int16 playerID)
        {
            foreach (Player p in PlayerList)
                if (p.PlayerID == playerID)
                    return p;
            return null;
        }

        public Int16 FillBalloon(Int16 playerID, int balloonID, Int16 percentFilled, WaterBalloon.PossibleSize size)
        {
            Player p = FindPlayer(playerID);
            if (p != null)
                return p.IncreaseAmountOfWater(balloonID, percentFilled, size);
            return 0;
        }

    }
}
