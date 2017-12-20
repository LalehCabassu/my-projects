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
    public class WaterTester : ProtocolTester
    {
        //Player
        [TestMethod]
        public void TestWaterRequestDoer()
        {
            StartThreads();

            // Player-> Send registration request
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            // Player-> Send Empty balloon request
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(6000);

            // Player-> Send Water request
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID, 50);
            Thread.Sleep(6000);

            Assert.AreNotEqual(firstPlayerDoer.MyPlayer.BalloonsList.Last().AmountOfWater, 0);

            StopThreads();
        }

        //Water Manager
        [TestMethod]
        public void TestWaterReplyDoer()
        {
            StartThreads();

            // Player-> Send registration request
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            // Player-> Send Empty balloon request
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(6000);

            // Player-> Send Water request
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID, 50);
            Thread.Sleep(6000);

            Assert.AreNotEqual(myWaterManagerDoer.myWaterManager.PlayerList.Last().BalloonsList.Last().AmountOfWater, 0);
            
            StopThreads();
        }

        //Fight Manager
        [TestMethod]
        public void TestWaterDoer()
        {
            StartThreads();

            // Player-> Send registration request
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            // Player-> Send Empty balloon request
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(6000);

            // Player-> Send Water request
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID, 50);
            Thread.Sleep(6000);

            Assert.AreNotEqual(myFightManagerDoer.myFightManager.PlayerList.Last().Value.BalloonsList.Last().AmountOfWater, 0);

            StopThreads();
        }
    }
}
