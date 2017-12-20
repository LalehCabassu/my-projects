using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;


namespace Objects
{
    public class FightManager
    {

        public Int16 FightManagerID = Int16.MaxValue;
        public BalloonManager MyBalloonManager;
        private static int fightIDGen = 1;
        public List<WaterFightGame> FightList;
        private Dictionary<IPEndPoint, Player> playerList;
        public Dictionary<IPEndPoint, Player> PlayerList;

        private static Int16 newID = 1;

        public FightManager(BalloonManager myBalloonManager)
        {
            playerList = new Dictionary<IPEndPoint, Player>();
            FightList = new List<WaterFightGame>();
            MyBalloonManager = myBalloonManager;
        }

        public int AddFight()
        {
            WaterFightGame newFight = new WaterFightGame(fightIDGen);
            FightList.Add(newFight);
            return fightIDGen++;
        }

        public void RemoveFight(int fightID)
        {
            foreach (WaterFightGame f in FightList)
            {
                if (f.FightID == fightID)
                    FightList.Remove(f);
            }
        }


        public Int16 AddNewPlayer(IPEndPoint playerEP, Player newPlayer)
        {
            if (!playerList.ContainsKey(playerEP))
            {
                playerList.Add(playerEP, newPlayer);
                return newPlayerID();
            }
            return playerList[playerEP].PlayerID;
        }

        private Int16 newPlayerID()
        {
            return newID++;
        }

        public Player FindPlayer(IPEndPoint playerEP)
        {
            if (playerList.ContainsKey(playerEP))
                return playerList[playerEP];
            return null;
        }

        public Player FindPlayer(Int16 playerID)
        {
            foreach (KeyValuePair<IPEndPoint, Player> p in playerList)
                if(p.Value.PlayerID == playerID)
                    return p.Value;
            return null;
        }

        public int [,] PlayerRecentLocations(Int16 ID, int number)
        {
            Player reqPlayer = null;
            foreach (KeyValuePair<IPEndPoint, Player> p in playerList)
                if (p.Value.PlayerID == ID)
                     reqPlayer = p.Value;

            if (reqPlayer != null)
            {
                int lenght = reqPlayer.LocationsList.Capacity;
                int[,] list = new int[number, 2];
                for (int i = 0; i < number; i++)
                {
                    list[i, 0] = reqPlayer.LocationsList[lenght - i - 1].Location.X;
                    list[i, 1] = reqPlayer.LocationsList[lenght - i - 1].Location.Y;
                }
                return list;
            }
            return null;
        }
        
        public WaterFightGame FindFight(int fightID)
        {
            foreach(WaterFightGame f in FightList)
                if(f.FightID == fightID)
                    return f;
            return null;
        }

        /*public void AddPlayerToFight(int fightID, Int16 playerID)
        {
            Player player = FindPlayer(playerID);
            WaterFightGame fight = FindFight(fightID);
            if (player != null && fight != null)
                fight.AddPlayer(player);
        }
         */ 

    }
   
}
