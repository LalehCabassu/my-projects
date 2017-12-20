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
    public class DeregistrationTester : ProtocolTester
    {
        //Player
        [TestMethod]
        public void TestDeregistrationRequestDoer()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);
            firstPlayerDoer.MyDeregistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);
            Assert.AreEqual(firstPlayerDoer.MyPlayer.PlayerID, 0);

            StopThreads();
        }

        //Fight Manager
        [TestMethod]
        public void TestDeregistrationReplyDoer()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);
            firstPlayerDoer.MyDeregistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);
            Assert.AreEqual(myFightManager.PlayerList.Count, 0);
            
            StopThreads();
        }

        //Balloon Manager and Water Manager
        [TestMethod]
        public void TestDeregistrationDoer()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);
            firstPlayerDoer.MyDeregistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);
         
            Assert.AreEqual(myBalloonManager.PlayerList.Count, 0);
            Assert.AreEqual(myWaterManager.PlayerList.Count, 0);

            StopThreads();
        }
    }
}
