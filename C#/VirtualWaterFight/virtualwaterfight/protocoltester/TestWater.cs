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
using WaterManager;

namespace ProtocolTester
{
    [TestClass]
    public class TestWater : TestProtocols
    {
        [TestMethod]
        public void WaterTester()
        {
            // Player-> Send registration request
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(3000);
            
            // Player-> Send Empty balloon request
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(2000);
            firstNewBalloon = firstPlayer.BalloonsList.Last();
            
            // Player-> Send Water request
            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstNewBalloon, 50);
            Thread.Sleep(3000);
            firstNewBalloon = firstPlayer.BalloonsList.Last();
            secondNewBalloon = myWaterManager.PlayerList.Last().BalloonsList.Last();
            thirdNewBalloon = myFightManager.PlayerList.Last().Value.BalloonsList.Last();
         
            Assert.AreEqual(firstNewBalloon.BalloonID, secondNewBalloon.BalloonID);
            Assert.AreEqual(firstNewBalloon.AmountOfWater, secondNewBalloon.AmountOfWater);
            Assert.AreEqual(secondNewBalloon.BalloonID, thirdNewBalloon.BalloonID);
            Assert.AreEqual(secondNewBalloon.AmountOfWater, thirdNewBalloon.AmountOfWater);

            StopThreads();
        }
    }
}
