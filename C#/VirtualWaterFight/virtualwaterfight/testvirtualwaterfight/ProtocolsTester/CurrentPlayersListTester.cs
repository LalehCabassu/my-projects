using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading;

using Common;
using Common.Communicator;
using Objects;
using Player;
using FightManager;
using BalloonManager;
using WaterManager;

namespace TestVirtualWaterFight.ProtocolsTester
{
    [TestClass]
    public class CurrentPlayersListTester : ProtocolTester
    {
        //Player
        [TestMethod]
        public void TestCurrentPlayersListDoer()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            fourthPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(10000);

            firstPlayerDoer.MyCurrentPlayersListRequestDoer.SendRequest();
            Thread.Sleep(4000);

            foreach (Objects.Player p in firstPlayer.PlayersList)
                Assert.IsTrue(p.PlayerID == secondPlayer.PlayerID || p.PlayerID == thirdPlayer.PlayerID || p.PlayerID == fourthPlayer.PlayerID);

            StopThreads();
        }

    }
}
