using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Objects;

namespace BalloonManager
{
    public class BalloonManager
    {
        private static int balloonIDcreator = 1;
        
        public Int16 BalloonManagerID = Int16.MaxValue - 1;
        public List<Player> PlayerList;

        public IPEndPoint FightManagerEP;
        public IPEndPoint WaterManagerEP;
        
        public BalloonManager(IPEndPoint fightManagerEP, IPEndPoint waterManagerEP)
        {
            PlayerList = new List<Player>();
            FightManagerEP = fightManagerEP;
            WaterManagerEP = waterManagerEP;
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
            if (player != null)
                PlayerList.Remove(player);
        }

        public void RemoveBalloon(Int16 playerID, int balloonID)
        {
            Player p = FindPlayer(playerID);
            if (p != null)
                p.DecrementNumberOfBalloons(balloonID);
        }

        public int AddBalloon(Int16 playerID, WaterBalloon.PossibleSize size, WaterBalloon.PossibleColor color)
        {
            Player p = FindPlayer(playerID);
            if (p != null)
                return p.GetNewBalloon(newBalloonID(), size, color);
            return -1;
        }

        public Player FindPlayer(Int16 playerID)
        {
            foreach (Player p in PlayerList)
                if (p.PlayerID == playerID)
                    return p;
            return null;
        }

        #region private methods
        private int newBalloonID()
        {
            return balloonIDcreator++;
        }
        #endregion
    }
}
