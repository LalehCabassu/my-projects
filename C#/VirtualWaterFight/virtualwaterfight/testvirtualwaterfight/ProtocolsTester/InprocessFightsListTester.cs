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
    public class InprocessFightsListTester : ProtocolTester
    {
        //Player
        [TestMethod]
        public void TestInprocessFightsListRequestDoer()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            fourthPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(10000);

            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(secondNewBalloon.Size, secondNewBalloon.Color);
            thirdPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(thirdNewBalloon.Size, thirdNewBalloon.Color);
            Thread.Sleep(20000);

            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID, 80);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID, 50);
            thirdPlayerDoer.MyWaterRequestDoer.SendRequest(thirdPlayerDoer.MyPlayer.BalloonsList.Last().BalloonID, 30);
            Thread.Sleep(200000);

            firstNewBalloon = firstPlayer.BalloonsList.Last();
            secondNewBalloon = secondPlayer.BalloonsList.Last();
            thirdNewBalloon = thirdPlayer.BalloonsList.Last();

            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(fourthPlayer.PlayerID, fourthLocation, firstNewBalloon.AmountOfWater, firstNewBalloon.BalloonID);
            secondPlayerDoer.MyInstigateFightRequestDoer.SendRequest(fourthPlayer.PlayerID, fourthLocation, secondNewBalloon.AmountOfWater, secondNewBalloon.BalloonID);
            thirdPlayerDoer.MyInstigateFightRequestDoer.SendRequest(fourthPlayer.PlayerID, fourthLocation, thirdNewBalloon.AmountOfWater, thirdNewBalloon.BalloonID);
            Thread.Sleep(100000);

            firstPlayerDoer.MyInprogressFightsListRequestDoer.SendRequest();
            Thread.Sleep(80000);

            foreach (WaterFightGame f in firstPlayer.InprocessFightsList)
                Assert.IsNotNull(myFightManager.FindFight(f.FightID));

            StopThreads();
        }
    }
}
