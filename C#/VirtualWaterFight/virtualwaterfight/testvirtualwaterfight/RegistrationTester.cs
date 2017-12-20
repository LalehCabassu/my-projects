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
    public class RegistrationTester : ProtocolTester
    {
        //Player
        [TestMethod]
        public void TestRegistrationRequestDoer()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);
            Assert.AreEqual(firstPlayerDoer.MyPlayer.PlayerID, 1);

            StopThreads();
        }

        //Fight Manager
        [TestMethod]
        public void TestRegistrationReplyDoer()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(1000);
            Assert.AreEqual(myFightManager.PlayerList.Count, 1);
            Assert.AreEqual(myFightManager.PlayerList.Last().Value.PlayerID, 1);

            StopThreads();
        }

        //Balloon Manager and Water Manager
        [TestMethod]
        public void TestRegistrationDoer()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);
            Assert.AreEqual(myBalloonManager.PlayerList.Count, 1);
            Assert.AreEqual(myBalloonManager.PlayerList.Last().PlayerID, 1);
            Assert.AreEqual(myWaterManager.PlayerList.Count, 1);
            Assert.AreEqual(myWaterManager.PlayerList.Last().PlayerID, 1);

            StopThreads();
        }
    }
}
