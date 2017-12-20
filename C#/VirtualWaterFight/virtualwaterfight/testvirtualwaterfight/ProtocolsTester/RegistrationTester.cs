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
    public class RegistrationTester : ProtocolTester
    {
        //Player
        [TestMethod]
        public void TestRegistrationRequestDoer()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);
            Assert.AreNotEqual(firstPlayerDoer.MyPlayer.PlayerID, 0);

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
            Assert.AreNotEqual(myFightManager.PlayerList.Last().Value.PlayerID, 0);

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
            Assert.AreNotEqual(myBalloonManager.PlayerList.Last().PlayerID, 0);
            Assert.AreEqual(myWaterManager.PlayerList.Count, 1);
            Assert.AreNotEqual(myWaterManager.PlayerList.Last().PlayerID, 0);

            StopThreads();
        }
    }
}
