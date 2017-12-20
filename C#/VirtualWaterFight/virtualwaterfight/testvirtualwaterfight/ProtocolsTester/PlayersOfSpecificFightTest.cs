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
    public class PlayersOfSpecificFightTest : ProtocolTester
    {
        [TestMethod]
        public void PlayersOfSpecificFight()
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
            Thread.Sleep(15000);

            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstNewBalloon.BalloonID, 80);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondNewBalloon.BalloonID, 50);
            thirdPlayerDoer.MyWaterRequestDoer.SendRequest(thirdNewBalloon.BalloonID, 30);
            Thread.Sleep(15000);

            firstNewBalloon = firstPlayer.BalloonsList.Last();
            secondNewBalloon = secondPlayer.BalloonsList.Last();
            thirdNewBalloon = thirdPlayer.BalloonsList.Last();

            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(fourthPlayer.PlayerID, fourthLocation, firstNewBalloon.AmountOfWater, firstNewBalloon.BalloonID);
            secondPlayerDoer.MyInstigateFightRequestDoer.SendRequest(fourthPlayer.PlayerID, fourthLocation, secondNewBalloon.AmountOfWater, secondNewBalloon.BalloonID);
            thirdPlayerDoer.MyInstigateFightRequestDoer.SendRequest(fourthPlayer.PlayerID, fourthLocation, thirdNewBalloon.AmountOfWater, thirdNewBalloon.BalloonID);
            Thread.Sleep(2000000);

            firstPlayerDoer.MyInprogressFightsListRequestDoer.SendRequest();
            Thread.Sleep(1500000);

            firstPlayerDoer.MyPlayersOfSpecificFightRequestDoer.SendRequest(myFightManager.FightList.Last().FightID);
            Thread.Sleep(80000);

            WaterFightGame currentFight = myFightManager.FindFight(myFightManager.FightList.Last().FightID);

            foreach (Objects.Player p in firstPlayer.InprocessFightsList.Last().PlayerList)
                Assert.IsNotNull(currentFight.FindPlayer(p.PlayerID));

            StopThreads();
        }
    }
}
