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
    public class TestEmptyBalloon : TestProtocols
    {
        [TestMethod]
        public void EmpyBalloonTester()
        {
            // Player-> Send registration request
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(1000);
            Assert.AreEqual(myBalloonManager.PlayerList.Count, 1);
            Assert.AreEqual(myBalloonManager.PlayerList.Last().PlayerID, firstPlayer.PlayerID);
            
            // Player-> Send Empty balloon request
            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            Thread.Sleep(3000);
            firstNewBalloon = firstPlayer.BalloonsList.Last();
            secondNewBalloon = myBalloonManager.PlayerList.Last().BalloonsList.Last();
            thirdNewBalloon = myWaterManager.PlayerList.Last().BalloonsList.Last();
            
            Assert.AreEqual(myBalloonManager.PlayerList.Last().NumberOfCurrentBalloons, 1);
            Assert.AreEqual(firstPlayer.NumberOfCurrentBalloons, 1);
            Assert.AreEqual(myFightManager.PlayerList.Last().Value.NumberOfCurrentBalloons, 1);
            Assert.AreEqual(myWaterManager.PlayerList.Last().NumberOfCurrentBalloons, 1);

            Assert.AreEqual(firstNewBalloon.BalloonID, secondNewBalloon.BalloonID);
            Assert.AreEqual(firstNewBalloon.BalloonID, thirdNewBalloon.BalloonID);
            
            StopThreads();
        }
    }
}
