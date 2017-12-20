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
    public class JoinFightTester : ProtocolTester
    {
        //Player
        [TestMethod]
        public void TestJoinFightRequestDoer_Successful()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);

            //Get new balloons
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(secondNewBalloon.Size, secondNewBalloon.Color);
            Thread.Sleep(12000);

            //Fill them
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 50);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondPlayer.BalloonsList.Last().BalloonID, 30);
            Thread.Sleep(12000);

            // Instigate a new fight - First player hits third player
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(thirdPlayer.PlayerID, thirdLocation,
                firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(30000);

            // Join the fight - Second player joints the fight by throwing a balloon to third player
            int fightID = thirdPlayer.joinedFightsList.Last().FightID;
            secondPlayerDoer.MyJointFightRequestDoer.SendRequest(fightID, thirdPlayer.PlayerID, thirdLocation, 
                                                                    secondPlayer.BalloonsList.Last().AmountOfWater, secondPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(100000);

            Assert.AreEqual(firstPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(firstPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreEqual(firstPlayer.NumberOfCurrentBalloons(), 0);
            
            Assert.AreEqual(secondPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(secondPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreEqual(secondPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(thirdPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(thirdPlayer.joinedFightsList.Last().PlayerList.Count, 2);
            Assert.AreNotEqual(thirdPlayer.HitByAmountOfWater, 0);

            StopThreads();
        }

        //Player, Balloon Manager and Water Manager
        [TestMethod]
        public void TestJoinFightRequestDoer_Successful_Deregister()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);

            //Get new balloons
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(thirdNewBalloon.Size, thirdNewBalloon.Color);
            Thread.Sleep(12000);

            //Fill them
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 100);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondPlayer.BalloonsList.Last().BalloonID, 100);
            Thread.Sleep(12000);

            // Instigate a new fight - First player hits third player
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(thirdPlayer.PlayerID, thirdLocation,
                firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(100000);

            // Join the fight - Second player joints the fight by throwing a balloon to third player
            int fightID = thirdPlayer.joinedFightsList.Last().FightID;
            secondPlayerDoer.MyJointFightRequestDoer.SendRequest(fightID, thirdPlayer.PlayerID, thirdLocation,
                                                                    secondPlayer.BalloonsList.Last().AmountOfWater, secondPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(100000);
            
            Assert.AreEqual(firstPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(firstPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreEqual(firstPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(secondPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(secondPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreEqual(secondPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(thirdPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(thirdPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreNotEqual(thirdPlayer.HitByAmountOfWater, 0);
            Assert.AreEqual(thirdPlayer.PlayerID, 0);

            Assert.AreEqual(myBalloonManager.PlayerList.Count, 2);
            Assert.AreEqual(myWaterManager.PlayerList.Count, 2);

            StopThreads();
        }

        //Player
        [TestMethod]
        public void TestJoinFightRequestDoer_WrongFightID()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);

            //Get new balloons
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(secondNewBalloon.Size, secondNewBalloon.Color);
            Thread.Sleep(12000);

            //Fill them
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 50);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondPlayer.BalloonsList.Last().BalloonID, 30);
            Thread.Sleep(12000);

            // Instigate a new fight - First player hits third player
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(thirdPlayer.PlayerID, thirdLocation,
                                                                    firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(30000);

            // Join the fight - Second player tries to joint the fight by throwing a balloon to third player but sends the wrong fightID(0)
            secondPlayerDoer.MyJointFightRequestDoer.SendRequest(0, thirdPlayer.PlayerID, thirdLocation,
                                                                    secondPlayer.BalloonsList.Last().AmountOfWater, secondPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(30000);

            Assert.AreEqual(firstPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(firstPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreEqual(firstPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(secondPlayer.joinedFightsList.Count, 0);
            Assert.AreEqual(secondPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(thirdPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(thirdPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreNotEqual(thirdPlayer.HitByAmountOfWater, 0);

            StopThreads();
        }

        //Player
        [TestMethod]
        public void TestJoinFightRequestDoer_WrongLocation()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);

            //Get new balloons
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(secondNewBalloon.Size, secondNewBalloon.Color);
            Thread.Sleep(12000);

            //Fill them
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 50);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondPlayer.BalloonsList.Last().BalloonID, 30);
            Thread.Sleep(12000);

            // Instigate a new fight - First player hits third player
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(thirdPlayer.PlayerID, thirdLocation,
                                                                    firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(30000);

            // Join the fight - Second player joints the fight by throwing a balloon to third player
            int fightID = thirdPlayer.joinedFightsList.Last().FightID;
            secondPlayerDoer.MyJointFightRequestDoer.SendRequest(fightID, thirdPlayer.PlayerID, fourthLocation,
                                                                    secondPlayer.BalloonsList.Last().AmountOfWater, secondPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(30000);

            Assert.AreEqual(firstPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(firstPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreEqual(firstPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(secondPlayer.joinedFightsList.Count, 0);
            Assert.AreEqual(secondPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(thirdPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(thirdPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreNotEqual(thirdPlayer.HitByAmountOfWater, 0);

            StopThreads();
        }

        //Player
        [TestMethod]
        public void TestJoinFightRequestDoer_EmptyBalloon()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);

            //Get new balloons
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(secondNewBalloon.Size, secondNewBalloon.Color);
            Thread.Sleep(12000);

            //Fill them
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 50);
            Thread.Sleep(6000);

            // Instigate a new fight - First player hits third player
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(thirdPlayer.PlayerID, thirdLocation,
                                                                    firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(35000);

            // Join the fight - Second player joints the fight by throwing a balloon to third player
            int fightID = thirdPlayer.joinedFightsList.Last().FightID;
            secondPlayerDoer.MyJointFightRequestDoer.SendRequest(fightID, thirdPlayer.PlayerID, thirdLocation,
                                                                    secondPlayer.BalloonsList.Last().AmountOfWater, secondPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(30000);

            Assert.AreEqual(firstPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(firstPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreEqual(firstPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(secondPlayer.joinedFightsList.Count, 0);
            Assert.AreEqual(secondPlayer.NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(thirdPlayer.joinedFightsList.Count, 1);
            Assert.AreEqual(thirdPlayer.joinedFightsList.Last().PlayerList.Count, 1);
            Assert.AreNotEqual(thirdPlayer.HitByAmountOfWater, 0);

            StopThreads();
        }

        //Fight Manager
        [TestMethod]
        public void TestJoinFightReplyDoer_Successful()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);

            //Get new balloons
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(secondNewBalloon.Size, secondNewBalloon.Color);
            Thread.Sleep(20000);

            //Fill them
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 50);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondPlayer.BalloonsList.Last().BalloonID, 30);
            Thread.Sleep(20000);

            // Instigate a new fight - First player hits third player
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(thirdPlayer.PlayerID, thirdLocation,
                firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(200000);

            // Join the fight - Second player joints the fight by throwing a balloon to third player
            int fightID = thirdPlayer.joinedFightsList.Last().FightID;
            secondPlayerDoer.MyJointFightRequestDoer.SendRequest(fightID, thirdPlayer.PlayerID, thirdLocation,
                                                                    secondPlayer.BalloonsList.Last().AmountOfWater, secondPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(200000);

            Assert.AreEqual(myFightManager.FightList.Count, 1);
            Assert.AreEqual(myFightManager.FightList.Last().PlayerList.Count, 3);
            Assert.AreEqual(myFightManager.FightList.Last().FindPlayer(firstPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreEqual(myFightManager.FightList.Last().FindPlayer(secondPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreEqual(myFightManager.FightList.Last().FightID, 1);
            Assert.AreEqual(myFightManager.FightList.Last().LargestNumberOfPlayers, 3);
            Assert.AreEqual(myFightManager.FightList.Last().Status, WaterFightGame.PossibleStatus.Inprocess);
            Assert.AreNotEqual(myFightManager.FightList.Last().TotalAmountOfWaterHit, 0);
            Assert.AreNotEqual(myFightManager.FightList.Last().TotalAmountOfWaterThrown, 0);
            Assert.AreEqual(myFightManager.FightList.Last().TotalNumberOfBalloonHit, 2);
            Assert.AreEqual(myFightManager.FightList.Last().TotalNumberOfBalloonsThrown, 2);

            StopThreads();
        }

        //Fight Manager
        [TestMethod]
        public void TestJoinFightReplyDoer_Successful_Deregister()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);

            //Get new balloons
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(thirdNewBalloon.Size, thirdNewBalloon.Color);
            Thread.Sleep(20000);

            //Fill them
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 100);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondPlayer.BalloonsList.Last().BalloonID, 100);
            Thread.Sleep(20000);

            // Instigate a new fight - First player hits third player
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(thirdPlayer.PlayerID, thirdLocation,
                firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(100000);

            // Join the fight - Second player joints the fight by throwing a balloon to third player
            int fightID = thirdPlayer.joinedFightsList.Last().FightID;
            secondPlayerDoer.MyJointFightRequestDoer.SendRequest(fightID, thirdPlayer.PlayerID, thirdLocation,
                                                                    secondPlayer.BalloonsList.Last().AmountOfWater, secondPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(100000);
            
            Assert.AreEqual(myFightManager.FightList.Count, 1);
            Assert.AreEqual(myFightManager.FightList.Last().PlayerList.Count, 2);
            Assert.AreEqual(myFightManager.FightList.Last().FindPlayer(firstPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreEqual(myFightManager.FightList.Last().FindPlayer(secondPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreNotEqual(myFightManager.FightList.Last().FightID, 0);
            Assert.AreEqual(myFightManager.FightList.Last().LargestNumberOfPlayers, 3);
            Assert.AreEqual(myFightManager.FightList.Last().Status, WaterFightGame.PossibleStatus.Inprocess);
            Assert.AreNotEqual(myFightManager.FightList.Last().TotalAmountOfWaterHit, 0);
            Assert.AreNotEqual(myFightManager.FightList.Last().TotalAmountOfWaterThrown, 0);
            Assert.AreEqual(myFightManager.FightList.Last().TotalNumberOfBalloonHit, 2);
            Assert.AreEqual(myFightManager.FightList.Last().TotalNumberOfBalloonsThrown, 2);

            StopThreads();
        }

        //Fight Manager
        [TestMethod]
        public void TestJoinFightReplyDoer_WrongFightID()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);

            //Get new balloons
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(secondNewBalloon.Size, secondNewBalloon.Color);
            Thread.Sleep(12000);

            //Fill them
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 50);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondPlayer.BalloonsList.Last().BalloonID, 30);
            Thread.Sleep(12000);

            // Instigate a new fight - First player hits third player
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(thirdPlayer.PlayerID, thirdLocation,
                                                                    firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(30000);

            // Join the fight - Second player tries to joint the fight by throwing a balloon to third player but sends the wrong fightID(0)
            secondPlayerDoer.MyJointFightRequestDoer.SendRequest(0, thirdPlayer.PlayerID, thirdLocation,
                                                                    secondPlayer.BalloonsList.Last().AmountOfWater, secondPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(30000);

            Assert.AreEqual(myFightManager.FightList.Count, 1);
            Assert.AreEqual(myFightManager.FightList.Last().PlayerList.Count, 2);
            Assert.AreEqual(myFightManager.FightList.Last().FindPlayer(firstPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreEqual(myFightManager.FindPlayer(secondPlayer.PlayerID).NumberOfCurrentBalloons(), 1);
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
        public void TestJoinFightReplyDoer_WrongLocation()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);

            //Get new balloons
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(secondNewBalloon.Size, secondNewBalloon.Color);
            Thread.Sleep(12000);

            //Fill them
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 50);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondPlayer.BalloonsList.Last().BalloonID, 30);
            Thread.Sleep(12000);

            // Instigate a new fight - First player hits third player
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(thirdPlayer.PlayerID, thirdLocation,
                                                                    firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(30000);

            // Join the fight - Second player joints the fight by throwing a balloon to third player
            int fightID = thirdPlayer.joinedFightsList.Last().FightID;
            secondPlayerDoer.MyJointFightRequestDoer.SendRequest(fightID, thirdPlayer.PlayerID, fourthLocation,
                                                                    secondPlayer.BalloonsList.Last().AmountOfWater, secondPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(30000);

            Assert.AreEqual(myFightManager.FightList.Count, 1);
            Assert.AreEqual(myFightManager.FightList.Last().PlayerList.Count, 2);
            Assert.AreEqual(myFightManager.FightList.Last().FindPlayer(firstPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreEqual(myFightManager.FindPlayer(secondPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreNotEqual(myFightManager.FightList.Last().FightID, 0);
            Assert.AreEqual(myFightManager.FightList.Last().LargestNumberOfPlayers, 2);
            Assert.AreEqual(myFightManager.FightList.Last().Status, WaterFightGame.PossibleStatus.Inprocess);
            Assert.AreNotEqual(myFightManager.FightList.Last().TotalAmountOfWaterHit, 0);
            Assert.AreNotEqual(myFightManager.FightList.Last().TotalAmountOfWaterThrown, 0);
            Assert.AreEqual(myFightManager.FightList.Last().TotalNumberOfBalloonHit, 1);
            Assert.AreEqual(myFightManager.FightList.Last().TotalNumberOfBalloonsThrown, 2);

            StopThreads();
        }

        //Fight Manager
        [TestMethod]
        public void TestJoinFightReplyDoer_EmptyBalloon()
        {
            StartThreads();

            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);

            //Get new balloons
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(secondNewBalloon.Size, secondNewBalloon.Color);
            Thread.Sleep(12000);

            //Fill them
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 50);
            Thread.Sleep(6000);

            // Instigate a new fight - First player hits third player
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(thirdPlayer.PlayerID, thirdLocation,
                                                                    firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(100000);

            // Join the fight - Second player joints the fight by throwing a balloon to third player
            int fightID = thirdPlayer.joinedFightsList.Last().FightID;
            secondPlayerDoer.MyJointFightRequestDoer.SendRequest(fightID, thirdPlayer.PlayerID, thirdLocation,
                                                                    secondPlayer.BalloonsList.Last().AmountOfWater, secondPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(100000);

            Assert.AreEqual(myFightManager.FightList.Count, 1);
            Assert.AreEqual(myFightManager.FightList.Last().PlayerList.Count, 2);
            Assert.AreEqual(myFightManager.FightList.Last().FindPlayer(firstPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreEqual(myFightManager.FindPlayer(secondPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreEqual(myFightManager.FightList.Last().FightID, 1);
            Assert.AreEqual(myFightManager.FightList.Last().LargestNumberOfPlayers, 2);
            Assert.AreEqual(myFightManager.FightList.Last().Status, WaterFightGame.PossibleStatus.Inprocess);
            Assert.AreNotEqual(myFightManager.FightList.Last().TotalAmountOfWaterHit, 0);
            Assert.AreNotEqual(myFightManager.FightList.Last().TotalAmountOfWaterThrown, 0);
            Assert.AreEqual(myFightManager.FightList.Last().TotalNumberOfBalloonHit, 1);
            Assert.AreEqual(myFightManager.FightList.Last().TotalNumberOfBalloonsThrown, 2);

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
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);

            //Get new balloons
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(secondNewBalloon.Size, secondNewBalloon.Color);
            Thread.Sleep(12000);

            //Fill them
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayer.BalloonsList.Last().BalloonID, 50);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondPlayer.BalloonsList.Last().BalloonID, 30);
            Thread.Sleep(12000);

            // Instigate a new fight - First player hits third player
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(thirdPlayer.PlayerID, thirdLocation,
                firstPlayer.BalloonsList.Last().AmountOfWater, firstPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(100000);

            // Join the fight - Second player joints the fight by throwing a balloon to third player
            int fightID = thirdPlayer.joinedFightsList.Last().FightID;
            secondPlayerDoer.MyJointFightRequestDoer.SendRequest(fightID, thirdPlayer.PlayerID, thirdLocation,
                                                                    secondPlayer.BalloonsList.Last().AmountOfWater, secondPlayer.BalloonsList.Last().BalloonID);
            Thread.Sleep(100000);

            Assert.AreEqual(myFightManager.FightList.Count, 1);
            Assert.AreEqual(myFightManager.FightList.Last().PlayerList.Count, 3);
            Assert.AreEqual(myFightManager.FightList.Last().FindPlayer(firstPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreEqual(myFightManager.FightList.Last().FindPlayer(secondPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            
            Assert.AreEqual(myBalloonManager.FindPlayer(firstPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreEqual(myBalloonManager.FindPlayer(secondPlayer.PlayerID).NumberOfCurrentBalloons(), 0);

            Assert.AreEqual(myWaterManager.FindPlayer(firstPlayer.PlayerID).NumberOfCurrentBalloons(), 0);
            Assert.AreEqual(myWaterManager.FindPlayer(secondPlayer.PlayerID).NumberOfCurrentBalloons(), 0);

            StopThreads();
        }
    }
}
