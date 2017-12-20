using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading;

using Common;
using Common.Messages;
using Common.Communicator;
using Common.Threading;
using Objects;
using Player;
using FightManager;
using BalloonManager;
using WaterManager;


namespace TestVirtualWaterFight
{
    [TestClass]
    public class PlayerTester : ProtocolsTester.ProtocolTester
    {
        [TestMethod]
        public void TestUpdateCurrentPlayersList()
        {
            StartThreads();

            Int16 [] playerList = {2, 3, 4};
            firstPlayer.UpdateCurrentPlayersList(playerList);

            foreach (Objects.Player p in firstPlayer.PlayersList)
                Assert.IsTrue(playerList.Contains(p.PlayerID));
            
            StopThreads();
        }

        public void TestUpdateInprocessFightsList()
        {
            StartThreads();

            int[] fightList = { 2, 3, 4 };
            firstPlayer.UpdateInprocessFightsList(fightList);

            foreach (WaterFightGame f in firstPlayer.InprocessFightsList)
                Assert.IsTrue(fightList.Contains(f.FightID));

            StopThreads();
        }

        public void TestUpdatePlayersOfSpecificFight()
        {
            StartThreads();

            int[] fightList = { 2, 3, 4 };
            firstPlayer.UpdateInprocessFightsList(fightList);

            Int16[] playerList = { 2, 3, 4 };

            firstPlayer.UpdatePlayersOfSpecificFight(2, playerList);

            foreach (Objects.Player p in firstPlayer.FindFight(2).PlayerList)
                Assert.IsTrue(playerList.Contains(p.PlayerID));

            StopThreads();
        }
    
    }
}
