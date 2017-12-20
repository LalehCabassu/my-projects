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
    public class TestRecentLocations : TestProtocols
    {
        [TestMethod]
        public void RecentLocationTester()
        {
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            fourthPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(4000);

            myFightManagerDoer.MyPlayerLocationRequestDoer.SendRequest();
            Thread.Sleep(4000);

            firstPlayerDoer.MyRecentLocationsRequestDoer.SendRequest(secondPlayer.PlayerID);
            Thread.Sleep(2000);

            Location locationInPlayer = firstPlayer.LocationsList.Last().Location;
            Location locationInFightManager = myFightManager.FindPlayer(secondPlayer.PlayerID).LocationsList.Last().Location;
            Assert.AreEqual(locationInPlayer.X, locationInFightManager.X);
            Assert.AreEqual(locationInPlayer.Y, locationInFightManager.Y);

        }
    }
}
