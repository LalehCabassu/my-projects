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
    public class TestDeregistration : TestProtocols
    {
        [TestMethod]
        public void DeregisterationTester()
        {
            // Player-> Send Registration request
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(6000);
            
            // Player-> Send Deregidteration request
            firstPlayerDoer.MyDeregistrationRequestDoer.SendRequest();
            
            // Fight Manager-> Receive, process, reply 
            int timeout = 6000;
            while (myFightManager.PlayerList.Count != 0 && timeout > 0)
            {
                Thread.Sleep(10);
                timeout -= 10;
            }
            Thread.Sleep(timeout);
            
            // Player-> Receive and process
            timeout = 9000;
            while (firstPlayerDoer.MyPlayer.PlayerID != 0 && timeout > 0)
            {
                Thread.Sleep(10);
                timeout -= 10;
            }
            
            Assert.AreEqual(firstPlayerDoer.MyPlayer.PlayerID, 0);
            Assert.AreEqual(myBalloonManager.PlayerList.Count, 0);
            Assert.AreEqual(myWaterManager.PlayerList.Count, 0);

            StopThreads();
        }
    }
}
