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
    public class TestPlayerLocation : TestProtocols
    {
        [TestMethod]
        public void PlayerLocationTester()
        {
            // Player-> Send
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(1000);

            firstPlayer.MoveToNewLocation(firstLocation, DateTime.Now);
            myFightManagerDoer.MyPlayerLocationRequestDoer.SendRequest();
            Thread.Sleep(3000);

            secondLocation = myFightManager.PlayerList.Last().Value.LocationsList.Last().Location;
            Assert.AreEqual(firstLocation.X, secondLocation.X);
            Assert.AreEqual(firstLocation.Y, secondLocation.Y);
            //Assert.AreEqual(firstPlayer.LocationsList.Last().AtTime, myFightManager.PlayerList.Last().Value.LocationsList.Last().AtTime);
        }
    }
}
