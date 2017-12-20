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
    public class PlayerLocationTester : ProtocolTester
    {
        //Fight Manager
        [TestMethod]
        public void TestPlayerLocationRequestDoer()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            firstPlayer.MoveToNewLocation(secondLocation, DateTime.Now);
            myFightManagerDoer.MyPlayerLocationRequestDoer.SendRequest();
            Thread.Sleep(30000);

            thirdLocation = myFightManager.FindPlayer(firstPlayer.PlayerID).LocationsList.Last().Location;
            Assert.AreEqual(firstPlayer.LocationsList.Last().Location.X, thirdLocation.X);
            Assert.AreEqual(firstPlayer.LocationsList.Last().Location.Y, thirdLocation.Y);
            
            StopThreads();
        }
        /*
        //Fight Manager
        [TestMethod]
        public void TestPlayerLocationRequestDoer_LateReply()
        {
            StartThreads();

            // Player-> Send
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            firstPlayer.MoveToNewLocation(firstLocation, DateTime.Now);
            myFightManagerDoer.MyPlayerLocationRequestDoer.SendRequest();
            //Thread.Sleep(3000);
            //while (!myFightManagerDoer.MyPlayerLocationRequestDoer.MessageAvailable()) ;
            //Thread.Sleep(1000);
            //myFightManagerDoer.MyPlayerLocationRequestDoer.SetReceivedTimeToNow();
            //Thread.Sleep(1000);

            secondLocation = myFightManager.PlayerList.Last().Value.LocationsList.Last().Location;
            Assert.AreEqual(firstPlayer.LocationsList.Last().Location.X, secondLocation.X);
            Assert.AreEqual(firstPlayer.LocationsList.Last().Location.Y, secondLocation.Y);

            StopThreads();
        }
        */
        //Fight Manager
        [TestMethod]
        public void TestPlayerLocationRequestDoer_HighSpeed()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            firstPlayer.MoveToNewLocation(firstLocation, DateTime.Now);
            myFightManagerDoer.MyPlayerLocationRequestDoer.SendRequest();
            Thread.Sleep(30000);

            firstPlayer.MoveToNewLocation(fourthLocation, DateTime.Now);
            myFightManagerDoer.MyPlayerLocationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            Assert.AreEqual(myFightManager.PlayerList.Count, 0);
            
            StopThreads();
        }
        /*
        //Player
        [TestMethod]
        public void TestDeregisterReplyDoer_LateReply()
        {
            StartThreads();

            // Player-> Send
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            firstPlayer.MoveToNewLocation(firstLocation, DateTime.Now);
            myFightManagerDoer.MyPlayerLocationRequestDoer.SendRequest();
            //Thread.Sleep(3000);
            //while (!myFightManagerDoer.MyPlayerLocationRequestDoer.MessageAvailable()) ;
            //Thread.Sleep(1000);
            //myFightManagerDoer.MyPlayerLocationRequestDoer.SetReceivedTimeToNow();
            //Thread.Sleep(1000);

            secondLocation = myFightManager.PlayerList.Last().Value.LocationsList.Last().Location;
            Assert.AreEqual(firstPlayer.LocationsList.Last().Location.X, secondLocation.X);
            Assert.AreEqual(firstPlayer.LocationsList.Last().Location.Y, secondLocation.Y);

            StopThreads();
        }
        */
        //Player, Balloon Manager and Water Manager
        [TestMethod]
        public void TestDeregisterReplyDoer_HighSpeed()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            firstPlayer.MoveToNewLocation(firstLocation, DateTime.Now);
            myFightManagerDoer.MyPlayerLocationRequestDoer.SendRequest();
            Thread.Sleep(30000);

            firstPlayer.MoveToNewLocation(fourthLocation, DateTime.Now);
            myFightManagerDoer.MyPlayerLocationRequestDoer.SendRequest();
            Thread.Sleep(6000);

            Assert.AreEqual(firstPlayerDoer.MyPlayer.PlayerID, 0);
            Assert.AreEqual(myBalloonManager.PlayerList.Count, 0);
            Assert.AreEqual(myWaterManager.PlayerList.Count, 0);

            StopThreads();
        }
    }
}
