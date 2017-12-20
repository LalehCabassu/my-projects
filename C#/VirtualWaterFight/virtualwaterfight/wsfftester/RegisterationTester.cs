using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

using Player;
using Server;
using Objects;
using Common.Messages;

namespace wsffTester
{
    [TestClass]
    public class RegisterationTester
    {
        [TestMethod]
        public void RegisterTestWFSS()
        {/*
            BalloonManager myBalloonManager = new BalloonManager();
            Server.FightManager myFightManager = new Server.FightManager("Laleh's Fight Manager", "Laleh Rostami Hosoori", "laleh.rostami@aggiemail.usu.edu", myBalloonManager);
            Common.Communicator.Communicator fightManagerCommunicator = new Common.Communicator.Communicator();
            FightManagerDoer myFightManagerDoer = new FightManagerDoer(fightManagerCommunicator, myFightManager);
            
            fightManagerCommunicator.Start();
            myFightManagerDoer.Start();
            
            Assert.AreEqual(myFightManagerDoer.resultWFSS, "SUCCESS");
            
            fightManagerCommunicator.Stop();
            myFightManagerDoer.Stop();
          */ 
        }
    }
}
