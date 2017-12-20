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
    public class InstigateFightTester : ProtocolTester
    {
        //Player
        [TestMethod]
        public void TestInstigateFightRequestDoer_Hit()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(4000);

            //Get a new balloon and fill it
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(6000);
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 50);
            Thread.Sleep(6000);

            // Instigate a new fight - Hit
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, secondLocation, 
                firstPlayerDoer.MyPlayer.BalloonsList.Last().AmountOfWater, firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(20000);

            Assert.AreEqual(firstPlayerDoer.MyPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(firstPlayerDoer.MyPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreEqual(firstPlayerDoer.MyPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(secondPlayerDoer.MyPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(secondPlayerDoer.MyPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreNotEqual(secondPlayerDoer.MyPlayer.HitByAmountOfWater, 0);

            StopThreads();
        }

        //Player, Balloon Manager and Water Manager
        [TestMethod]
        public void TestInstigateFightRequestDoer_Hit_Deregister()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(4000);

            //Get a new balloon and fill it
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(thirdNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(6000);
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 100);
            Thread.Sleep(6000);

            // Instigate a new fight - Hit
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, secondLocation,
                firstPlayerDoer.MyPlayer.BalloonsList.Last().AmountOfWater, firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(30000);

            Assert.AreEqual(firstPlayerDoer.MyPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(firstPlayerDoer.MyPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreEqual(firstPlayerDoer.MyPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(secondPlayerDoer.MyPlayer.joinedFightsList.Count, 0);
            Assert.AreEqual(secondPlayer.PlayerID, 0);

            Assert.AreEqual(myBalloonManager.PlayerList.Count, 1);
            Assert.AreEqual(myWaterManager.PlayerList.Count, 1);

            StopThreads();
        }


        //Player
        [TestMethod]
        public void TestInstigateFightRequestDoer_WrongLocation()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(4000);

            //Get a new balloon and fill it
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(6000);
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID, 50);
            Thread.Sleep(6000);

            // Instigate a new fight - Not Hit
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, thirdLocation, 
                firstPlayerDoer.MyPlayer.BalloonsList.Last().AmountOfWater, firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(20000);

            Assert.AreEqual(firstPlayerDoer.MyPlayer.joinedFightsList.Count, 0);
            Assert.AreEqual(firstPlayerDoer.MyPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(secondPlayerDoer.MyPlayer.joinedFightsList.Count, 0);
            Assert.AreEqual(secondPlayerDoer.MyPlayer.HitByAmountOfWater, 0);

            StopThreads();
        }

        //Player
        [TestMethod]
        public void TestInstigateFightRequestDoer_EmptyBalloon()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(4000);

            //Get a new balloon and fill it
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(6000);

            // Instigate a new fight - Not Hit
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, secondLocation, 
                firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(20000);

            Assert.AreEqual(firstPlayerDoer.MyPlayer.joinedFightsList.Count, 0);
            Assert.AreEqual(firstPlayerDoer.MyPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(secondPlayerDoer.MyPlayer.joinedFightsList.Count, 0);
            Assert.AreEqual(secondPlayerDoer.MyPlayer.HitByAmountOfWater, 0);

            StopThreads();
        }

        //Fight Manager
        [TestMethod]
        public void TestInstigateFightReplyDoer_Hit()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(4000);

            //Get a new balloon and fill it
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(6000);
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID, 50);
            Thread.Sleep(6000);

            // Instigate a new fight - Hit
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, secondLocation,
                firstPlayerDoer.MyPlayer.BalloonsList.Last().AmountOfWater, firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(20000);

            Assert.AreEqual(myFightManager.FightList.Count, 1);
            Assert.AreEqual(myFightManager.FightList.Last().PlayerList.Count, 2);
            Assert.AreEqual(myFightManager.FightList.Last().PlayerList.Last().NumberOfCurrentBalloons(), 0);
            Assert.AreEqual(myFightManager.FightList.Last().FightID, 1);
            Assert.AreEqual(myFightManager.FightList.Last().LargestNumberOfPlayers, 2);
            Assert.AreEqual(myFightManager.FightList.Last().Status, WaterFightGame.PossibleStatus.Inprocess);
            Assert.AreNotEqual(myFightManager.FightList.Last().TotalAmountOfWaterHit, 0);
            Assert.AreNotEqual(myFightManager.FightList.Last().TotalAmountOfWaterThrown, 0);
            Assert.AreEqual(myFightManager.FightList.Last().TotalNumberOfBalloonHit, 1);
            Assert.AreEqual(myFightManager.FightList.Last().TotalNumberOfBalloonsThrown, 1);
            
            StopThreads();
        }

        //Fight Manager
        [TestMethod]
        public void TestInstigateFightReplyDoer_WrongLocation()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(4000);

            //Get a new balloon and fill it
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(6000);
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID, 50);
            Thread.Sleep(6000);

            // Instigate a new fight - Not Hit
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, thirdLocation,
                firstPlayerDoer.MyPlayer.BalloonsList.Last().AmountOfWater, firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(20000);

            Assert.AreEqual(myFightManager.FightList.Count, 0);
            Assert.AreEqual(myFightManager.FindPlayer(firstPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            
            StopThreads();
        }

        //Fight Manager
        [TestMethod]
        public void TestInstigateFightReplyDoer_EmptyBalloon()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(4000);

            //Get a new balloon and fill it
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(6000);

            // Instigate a new fight - Not Hit
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, secondLocation,
                firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(20000);

            Assert.AreEqual(myFightManager.FightList.Count, 0);
            Assert.AreEqual(myFightManager.FindPlayer(firstPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            
            StopThreads();
        }

        //Balloon Manager and Water Manager
        [TestMethod]
        public void TestDecrementNumberOfBalloonDoer()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(4000);

            //Get a new balloon and fill it
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(6000);
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID, 50);
            Thread.Sleep(6000);

            // Instigate a new fight - Hit
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, secondLocation,
                firstPlayerDoer.MyPlayer.BalloonsList.Last().AmountOfWater, firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(20000);

            Assert.AreEqual(myBalloonManager.FindPlayer(firstPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreEqual(myWaterManager.FindPlayer(firstPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            
            StopThreads();
        }
    }
}
