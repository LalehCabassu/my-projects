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
            //Thread.Sleep(60000);
            
            // Fight Manager-> Receive, process, reply 
            int timeout = 30000;
            while (!firstPlayerCommunicator.IncomingAvailable() && timeout > 0)
            {
                Thread.Sleep(10);
                timeout -= 10;
            }
            Assert.AreEqual(myFightManager.PlayerList.Count, 1);
            //Thread.Sleep(90000);
            
            // Player-> Receive and process
            timeout = 90000;
            while ((!balloonManagerCommunicator.IncomingAvailable() || !waterManagerCommunicator.IncomingAvailable()) && timeout > 0)
            {
                Thread.Sleep(10);
                timeout -= 10;
            }
            Assert.AreEqual(firstPlayerDoer.MyPlayer.PlayerID, 1);
            Assert.AreEqual(myBalloonManager.PlayerList.Count, 1);
            Assert.AreEqual(myWaterManager.PlayerList.Count, 1);
            
            StopThreads();
        }
    }
}
