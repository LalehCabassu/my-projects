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
    public class TestJoinFight : TestProtocols
    {
        [TestMethod]
        public void JoinFightTester()
        {
            // Players-> Send registration request
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            fourthPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(2000);

            // Fill new balloons
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(WaterBalloon.PossibleSize.XLarge, WaterBalloon.PossibleColor.Brown);
            Thread.Sleep(1000);
            firstNewBalloon = firstPlayer.BalloonsList.Last();
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstNewBalloon, 50);
            Thread.Sleep(2000);

            thirdPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(WaterBalloon.PossibleSize.Small, WaterBalloon.PossibleColor.Red);
            Thread.Sleep(1000);
            secondNewBalloon = thirdPlayer.BalloonsList.Last();
            thirdPlayerDoer.MyWaterRequestDoer.SendRequest(secondNewBalloon, 30);
            Thread.Sleep(2000);

            fourthPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(WaterBalloon.PossibleSize.Medium, WaterBalloon.PossibleColor.Gray);
            Thread.Sleep(1000);
            thirdNewBalloon = fourthPlayer.BalloonsList.Last();
            fourthPlayerDoer.MyWaterRequestDoer.SendRequest(thirdNewBalloon, 10);
            Thread.Sleep(2000);

            // Instigate a new fight between first player and second player
            int preNoFights = myFightManager.FightList.Count;
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, secondLocation, firstNewBalloon.AmountOfWater, firstNewBalloon.BalloonID);
            Thread.Sleep(2000);
            int newNoFights = myFightManager.FightList.Count;
            Assert.IsTrue(newNoFights - preNoFights > 0);

            // Join the fight: third player joint fight between first player and second player
            int fightID = myFightManager.FightList[0].FightID;
            int preNoPlayers = myFightManager.FindFight(fightID).PlayerList.Count;
            thirdPlayerDoer.MyJointFightRequestDoer.SendRequest(fightID, secondPlayer.PlayerID, secondLocation, secondNewBalloon.AmountOfWater, secondNewBalloon.BalloonID);
            Thread.Sleep(100);
            int curNoPlayers = myFightManager.FindFight(fightID).PlayerList.Count;
            Assert.IsTrue(curNoPlayers - preNoPlayers == 1);
            Assert.AreEqual(curNoPlayers, 3);

            // Not join the fight -> Jane (Laleh, Sara, Maria)
            fourthPlayerDoer.MyJointFightRequestDoer.SendRequest(fightID, secondPlayer.PlayerID, thirdLocation, thirdNewBalloon.AmountOfWater, thirdNewBalloon.BalloonID);
            Thread.Sleep(100);
            curNoPlayers = myFightManager.FindFight(fightID).PlayerList.Count;
            Assert.AreEqual(curNoPlayers, 3);

            Assert.AreEqual(myFightManagerDoer.MyJoinFightReplyDoer.wfssResult, "SUCCESS");
            
            StopThreads();
        }
    }
}
