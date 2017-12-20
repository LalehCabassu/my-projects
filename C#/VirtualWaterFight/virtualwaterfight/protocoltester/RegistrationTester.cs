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
    public class TestRegistration : TestProtocols
    {
        [TestMethod]
        public void RegisterationTester()
        {
            // Player-> Send
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();

            // Fight Manager-> Receive, process, reply 
            int timeout = 1000;
            while (!firstPlayerCommunicator.IncomingAvailable() && timeout > 0)
            {
                Thread.Sleep(10);
                timeout -= 10;
            }
            Assert.AreEqual(myFightManager.PlayerList.Count, 1);

            // Player-> Receive and process
            timeout = 1000;
            while (firstPlayerDoer.MyPlayer.PlayerID == 0 && timeout > 0)
            {
                Thread.Sleep(10);
                timeout -= 10;
            }
            Assert.AreNotEqual(firstPlayerDoer.MyPlayer.PlayerID, 0);
            Assert.AreEqual(myBalloonManager.PlayerList.Count, 1);
            Assert.AreEqual(myWaterManager.PlayerList.Count, 1);
            
            StopThreads();
        }
    }
}
