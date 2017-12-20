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

namespace TestVirtualWaterFight
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
            Thread.Sleep(40000);

            firstPlayerDoer.MyCurrentPlayersListRequestDoer.SendRequest();
            Thread.Sleep(80000);

            Assert.AreEqual(secondPlayer.PlayerID, firstPlayer.PlayersList.ElementAt(0).PlayerID);
            Assert.AreEqual(thirdPlayer.PlayerID, firstPlayer.PlayersList.ElementAt(1).PlayerID);
            Assert.AreEqual(fourthPlayer.PlayerID, firstPlayer.PlayersList.ElementAt(2).PlayerID);

            StopThreads();
        }

    }
}
