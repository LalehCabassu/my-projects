using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Objects
{
    public class BalloonManager
    {
        public int Port { get; set; }
        public Int16 BalloonManagerID = Int16.MaxValue - 1;
        public Dictionary<IPEndPoint, Player> PlayerList;
        
        public BalloonManager()
        {
            PlayerList = new Dictionary<IPEndPoint, Player>();
        }


        public void AddNewPlayer(IPEndPoint playerEP, Player newPlayer)
        {
            if (!PlayerList.ContainsKey(playerEP))
                PlayerList.Add(playerEP, newPlayer);
        }

        
        public Player FindPlayer(IPEndPoint playerEP)
        {
            if (PlayerList.ContainsKey(playerEP))
                return PlayerList[playerEP];
            return null;
        }

        public Player FindPlayer(Int16 playerID)
        {
            foreach (KeyValuePair<IPEndPoint, Player> p in PlayerList)
                if (p.Value.PlayerID == playerID)
                    return p.Value;
            return null;
        }
    }
}
