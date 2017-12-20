using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

using Objects;
using Common.Communicator;
using Common.Messages;

namespace Server
{
    public class FightManager
    {
        #region public members
        public Guid FightManagerGUID;
        private Int16 fightManegrID;
        public Int16 FightManagerID
        {
            get { return fightManegrID; }
            set { }
        }
        public string Name { get; set; }
        public string OperatorName { get; set; }
        public string OperatorEmail { get; set; }
        public List<WaterFightGame> FightList;
        public Dictionary<IPEndPoint, Player> PlayerList;
        public IPEndPoint BalloonManagerEP;
        public IPEndPoint WaterManagerEP;
        #endregion

        #region private members
        private static Int16 newID = 1;
        private static int fightIDGen = 1;
        #endregion

        #region public methods
        public FightManager() { }

        public FightManager(string name, string operatorName, string operatorEmail, IPEndPoint balloonManagerEP, IPEndPoint waterManagerEP)
        {
            FightManagerGUID = Guid.NewGuid();
            fightManegrID = Int16.MaxValue;
            Name = name;
            OperatorName = operatorName;
            OperatorEmail = operatorEmail;

            BalloonManagerEP = balloonManagerEP;
            WaterManagerEP = waterManagerEP;
            PlayerList = new Dictionary<IPEndPoint, Player>();
            FightList = new List<WaterFightGame>();
            
        }

        public int AddFight()
        {
            WaterFightGame newFight = new WaterFightGame(fightIDGen);
            FightList.Add(newFight);
            return fightIDGen++;
        }

        public void RemoveFight(int fightID)
        {
            WaterFightGame fight = null;
            foreach (WaterFightGame f in FightList)
            {
                if (f.FightID == fightID)
                {
                    fight = f;
                    break;
                }
            }
            if(fight != null)
                FightList.Remove(fight);
        }

        public Int16 AddNewPlayer(IPEndPoint playerEP, Player newPlayer)
        {
            if (!PlayerList.ContainsKey(playerEP))
            {
                newPlayer.PlayerID = newPlayerID();
                PlayerList.Add(playerEP, newPlayer);
                return newPlayer.PlayerID;
            }
            return PlayerList[playerEP].PlayerID;
        }

        public bool RemovePlayer(IPEndPoint playerEP)
        {
            if (PlayerList.ContainsKey(playerEP))
            {
                PlayerList.Remove(playerEP);
                return true;
            }
            return false;
        }
        
        public Player FindPlayer(IPEndPoint playerEP)
        {
            if (PlayerList.ContainsKey(playerEP))
                return PlayerList[playerEP];
            return null;
        }

        public IPEndPoint FindPlayerEP(Int16 playerID)
        {
            foreach (KeyValuePair<IPEndPoint, Player> p in PlayerList)
                if (p.Value.PlayerID == playerID)
                    return p.Key;
            return null;
        }

        public Player FindPlayer(Int16 playerID)
        {
            foreach (KeyValuePair<IPEndPoint, Player> p in PlayerList)
                if(p.Value.PlayerID == playerID)
                    return p.Value;
            return null;
        }

        public int [,] PlayerRecentLocations(Int16 ID, int number)
        {
            Player reqPlayer = null;
            foreach (KeyValuePair<IPEndPoint, Player> p in PlayerList)
                if (p.Value.PlayerID == ID)
                {
                    reqPlayer = p.Value;
                    break;
                }

            if (reqPlayer != null)
            {
                int lenght = reqPlayer.LocationsList.Count;
                int[,] list = new int[number, 2];
                for (int i = 0; i < number && (lenght - i - 1) >= 0; i++)
                {
                    list[i, 0] = reqPlayer.LocationsList[lenght - i - 1].Location.X;
                    list[i, 1] = reqPlayer.LocationsList[lenght - i - 1].Location.Y;
                }
                return list;
            }
            return null;
        }

        public Int16[] ListCurrentPlayers()
        {
            Int16[] list = new Int16[PlayerList.Count];
            int i = 0;
            foreach (KeyValuePair<IPEndPoint, Player> p in PlayerList)
                list[i++] = p.Value.PlayerID;
            return list;
        }

        public Int16[] ListPlayersOfspecificFight(WaterFightGame fight)
        {
            Int16[] list = new Int16[fight.PlayerList.Count];
            int i = 0;
            foreach (Objects.Player player in fight.PlayerList)
                list[i++] = player.PlayerID;
            return list;
        }

        public int[] ListInprocessFights()
        {
            int [] list = new int [FightList.Count];
            int i = 0;
            foreach (WaterFightGame fight in FightList)
                list[i++] = fight.FightID;
            return list;
        }

        public WaterFightGame FindFight(int fightID)
        {
            foreach(WaterFightGame f in FightList)
                if(f.FightID == fightID)
                    return f;
            return null;
        }

        public bool isInFight(WaterFightGame fight, Player thrower, Player opponent)
        {
            return (fight.PlayerList.Contains(thrower) && fight.PlayerList.Contains(opponent));
        }

        public void NewThrow(int fightID, Int16 throwerID, Int16 opponentID, int balloonID, Int16 amountOfWater, bool isHit)
        {
            Player thrower = FindPlayer(throwerID);
            Player opponent = FindPlayer(opponentID);
            WaterFightGame fight = FindFight(fightID);
            WaterBalloon balloon = thrower.FindBalloon(balloonID);
            if (balloon != null && balloon.AmountOfWater == amountOfWater)
                fight.ThrowBalloon(thrower, opponent, amountOfWater, isHit);
            thrower.DecrementNumberOfBalloon(balloonID);
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

        public void FillBalloon(Int16 playerID, int balloonID, Int16 amountOfWater)
        {
            Player curPlayer = FindPlayer(playerID);
            if(curPlayer != null)
                curPlayer.FillBalloon(balloonID, amountOfWater);
        }
        #endregion

        #region private methods
        private Int16 newPlayerID()
        {
            return newID++;
        }
        #endregion

    }
   
}
