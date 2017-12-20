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
    public class EmpyBalloonTester : ProtocolTester
    {
        //Player
        [TestMethod]
        public void TestEmptyBalloonRequestDoer()
        {
            StartThreads();

            // Player-> Send registration request
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            // Player-> Send Empty balloon request
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(3000);

            // Player-> Receive and process reply
            Assert.AreEqual(firstPlayerDoer.MyPlayer.BalloonsList.Count, 1);
            Assert.AreEqual(firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID, 1);
            Assert.AreEqual(firstPlayerDoer.MyPlayer.BalloonsList.Last().Size, firstNewBalloon.Size);
            Assert.AreEqual(firstPlayerDoer.MyPlayer.BalloonsList.Last().Color, firstNewBalloon.Color);

            StopThreads();
        }

        //Balloon Manager
        [TestMethod]
        public void TestEmpyBalloonReplyDoer()
        {
            StartThreads();

            // Player-> Send registration request
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            // Player-> Send Empty balloon request
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(3000);

            // Balloon Manager-> Receive and process request
            Assert.AreEqual(myBalloonManager.PlayerList.Last().BalloonsList.Count, 1);
            Assert.AreEqual(myBalloonManager.PlayerList.Last().BalloonsList.Last().BalloonID, 1);
            Assert.AreEqual(myBalloonManager.PlayerList.Last().BalloonsList.Last().Size, firstNewBalloon.Size);
            Assert.AreEqual(myBalloonManager.PlayerList.Last().BalloonsList.Last().Color, firstNewBalloon.Color);

            StopThreads();
        }

        //Fight Manager and Water Manager
        [TestMethod]
        public void TestEmpyBalloonDoer()
        {
            StartThreads();

            // Player-> Send registration request
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            // Player-> Send Empty balloon request
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(3000);

            // Fight Manager-> Receive and process reply
            Assert.AreEqual(myFightManager.PlayerList.Last().Value.NumberOfCurrentBalloons(), 1);
            Assert.AreEqual(myFightManager.PlayerList.Last().Value.BalloonsList.Last().BalloonID, 1);

            // Water Manager-> Receive and process reply
            Assert.AreEqual(myWaterManager.PlayerList.Last().NumberOfCurrentBalloons(), 1);
            Assert.AreEqual(myWaterManager.PlayerList.Last().BalloonsList.Last().BalloonID, 1);

            StopThreads();
        }
    }
}
