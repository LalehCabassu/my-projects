using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;
using System.Net;

using Player;
using Server;
using Objects;
using Common.Messages;
using BalloonManager;

namespace ProtocolTester
{
    [TestClass]
    public class TestInstigateFight : TestProtocols
    {
        [TestMethod]
        public void InstigateFightTester()
        { 
            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            //Get a new balloon and fill it
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(WaterBalloon.PossibleSize.XLarge, WaterBalloon.PossibleColor.Brown);
            Thread.Sleep(2000);
            firstNewBalloon = firstPlayer.BalloonsList.Last();
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstNewBalloon, 50);
            Thread.Sleep(2000);
            
            // Instigate a new fight - Hit
            int preNoFights =  myFightManager.FightList.Count;
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, secondLocation, firstNewBalloon.AmountOfWater, firstNewBalloon.BalloonID);
            Thread.Sleep(15000);
            int newNoFights = myFightManager.FightList.Count;
            
            Assert.AreEqual(newNoFights - preNoFights, 1);
            Assert.AreEqual(myFightManager.FightList.Last().PlayerList.Count, 2);
            
            // Instigate a new fight - Not Hit
            preNoFights = myFightManager.FightList.Count;
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, thirdLocation, secondNewBalloon.AmountOfWater, secondNewBalloon.BalloonID);
            Thread.Sleep(15000);
            newNoFights = myFightManager.FightList.Count;
            
            Assert.IsTrue(newNoFights - preNoFights == 0);
            Assert.AreEqual(myFightManager.FightList.Last().PlayerList.Count, 2);
            
            StopThreads();
        }
    }
}
