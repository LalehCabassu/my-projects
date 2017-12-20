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
    public class RecentLocationsTester : ProtocolTester
    {
        //Player
        [TestMethod]
        public void TestRecentLocationsRequestDoer()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(8000);

            myFightManagerDoer.MyPlayerLocationRequestDoer.SendRequest();
            Thread.Sleep(30000);

            firstPlayerDoer.MyRecentLocationsRequestDoer.SendRequest(secondPlayer.PlayerID);
            Thread.Sleep(200000);

            Location playerLocation = firstPlayer.LocationsList.Last().Location;
            Assert.AreEqual(playerLocation.X, secondPlayer.LocationsList.Last().Location.X);
            Assert.AreEqual(playerLocation.Y, secondPlayer.LocationsList.Last().Location.Y);

            StopThreads();
        }

        //Fight Manager
        [TestMethod]
        public void TestRecentLocationsReplyDoer()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            fourthPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(10000);

            myFightManagerDoer.MyPlayerLocationRequestDoer.SendRequest();
            Thread.Sleep(40000);

            firstPlayerDoer.MyRecentLocationsRequestDoer.SendRequest(secondPlayer.PlayerID);
            Thread.Sleep(50000);

            Location playerLocation = firstPlayer.LocationsList.Last().Location;
            Location locationInFightManager = myFightManager.FindPlayer(secondPlayer.PlayerID).LocationsList.Last().Location;
            Assert.AreEqual(playerLocation.X, locationInFightManager.X);
            Assert.AreEqual(playerLocation.Y, locationInFightManager.Y);

            StopThreads();
        }
    }
}
