using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Objects
{
    public class WaterFightGame
    {
        public int FightID;
        private DateTime StartOn;
        private DateTime EndOn;
        public enum PossibleStatus
        {
            Inprocess = 1,
            Cancelled = 2,
            Won = 3
        };

        private PossibleStatus status;
        public PossibleStatus Status
        {
            get { return status; }
            set { }
        }

        public List<Player> PlayerList;
        private int largestNumberOfPlayers;
        public int LargestNumberOfPlayers
        {
            get { return largestNumberOfPlayers; }
            set { }
        }
        
        private int totalNumberOfBalloonsThrown;
        public int TotalNumberOfBalloonsThrown
        {
            get { return totalNumberOfBalloonsThrown; }
            set { }
        }

        private int totalAmountOfWaterThrown;
        public int TotalAmountOfWaterThrown
        {
            get { return totalAmountOfWaterThrown; }
            set { }
        }

        private int totalNumberOfBalloonHit;
        public int TotalNumberOfBalloonHit
        {
            get { return totalNumberOfBalloonHit; }
            set { }
        }

        private int totalAmountOfWaterHit;
        public int TotalAmountOfWaterHit
        {
            get { return totalAmountOfWaterHit; }
            set { }
        }

        private Player winner;
        public Player Winner
        {
            get { return winner; }
            set { }
        }

        public WaterFightGame(int id)
        {
           FightID = id;
           StartOn = DateTime.Now;
           status = PossibleStatus.Inprocess;
           PlayerList = new List<Player>();
           largestNumberOfPlayers = 0;
           totalNumberOfBalloonsThrown = 0;
           totalAmountOfWaterThrown = 0;
           totalNumberOfBalloonHit = 0;
           totalAmountOfWaterHit = 0;
           winner = null;
        }

        public int NumberOfPlayers()
        {
            return PlayerList.Count;
        }

        public void ThrowBalloon(Player thrower, Player opponent, Int16 amountOfWater, bool isHit)
        {
            newBalloonThrown(amountOfWater, isHit);
            if (isHit)
            {
                AddPlayer(thrower);
                AddPlayer(opponent);
                opponent.IncreaseAmountOfWater(amountOfWater);
                if (opponent.HitByAmountOfWater > 100)
                    removePlayer(opponent);
            }
        }

        private void newBalloonThrown(Int16 amountOfWater, bool isHit)
        {
            totalNumberOfBalloonsThrown++;
            totalAmountOfWaterThrown += amountOfWater;
            if (isHit)
            {
                totalNumberOfBalloonHit++;
                totalAmountOfWaterHit += amountOfWater;
            }
        }

        public void AddPlayer(Player newPlayer)
        {
            if (!PlayerList.Contains(newPlayer) && status == PossibleStatus.Inprocess)
            {
                PlayerList.Add(newPlayer);
                if (PlayerList.Count > largestNumberOfPlayers)
                    largestNumberOfPlayers = PlayerList.Count;
            }
        }

        public Player FindPlayer(Int16 id)
        {
            foreach (Player p in PlayerList)
                if (p.PlayerID == id)
                    return p;
            return null;
        }

        private void removePlayer(Player losedPlayer)
        {
            if(PlayerList.Contains(losedPlayer))
            {
                PlayerList.Remove(losedPlayer);
                if(PlayerList.Count == 1)
                    WinWaterFightGame(PlayerList.Last());
                else if (PlayerList.Count == 0)
                    CancelFightGame();
            }
        }

        private void CancelFightGame()
        {
            EndOn = DateTime.Now;
            status = PossibleStatus.Cancelled;
        }

        private void WinWaterFightGame(Player winner)
        {
            EndOn = DateTime.Now;
            status = PossibleStatus.Won;
            this.winner = winner;
        }
    }
}
