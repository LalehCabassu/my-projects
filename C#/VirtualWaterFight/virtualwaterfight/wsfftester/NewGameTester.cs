﻿using System;
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
    public class NewGameTester
    {
        [TestMethod]
        public void NewGameWFSS()
        {
            /*
            BalloonManager myBalloonManager = new BalloonManager();
            Server.FightManager myFightManager = new Server.FightManager("Laleh's Fight Manager", "Laleh Rostami Hosoori", "laleh.rostami@aggiemail.usu.edu", myBalloonManager);
            Common.Communicator.Communicator fightManagerCommunicator = new Common.Communicator.Communicator();
            FightManagerDoer myFightManagerDoer = new FightManagerDoer(fightManagerCommunicator, myFightManager);
            fightManagerCommunicator.Start();
            myFightManagerDoer.Start();
            
            // Player Side
            Location playerLocation = new Location(1, 2);
            Player.Player myPlayer = new Player.Player("Laleh", 28, true, playerLocation, fightManagerCommunicator.LocalEP);
            Common.Communicator.Communicator playerCommunicator = new Common.Communicator.Communicator();
            PlayerDoer myPlayerDoer = new PlayerDoer(playerCommunicator, myPlayer);
            playerCommunicator.Start();
            myPlayerDoer.Start();
            myPlayerDoer.MyRegistrationRequestDoer.SendRequest();

            // Opponent Side
            Location opponentLocation = new Location(2, 3);
            Player.Player opponent = new Player.Player("Sara", 32, true, opponentLocation, fightManagerCommunicator.LocalEP);
            Common.Communicator.Communicator opponentCommunicator = new Common.Communicator.Communicator();
            PlayerDoer opponentDoer = new PlayerDoer(opponentCommunicator, opponent);
            opponentCommunicator.Start();
            opponentDoer.Start();
            opponentDoer.MyRegistrationRequestDoer.SendRequest();

            // Instigate a new game
            //myPlayerDoer.MyInstigateFightRequestDoer.SendRequest(opponent.PlayerID, opponentLocation, 5, 13);
            
            wfssWebAPI.WFStatsSoapClient service = new wfssWebAPI.WFStatsSoapClient();
            string wfssResult = service.LogNewGame(myFightManager.FightManagerID.ToString(), 2, DateTime.Now);
                

            //Assert.AreEqual(myFightManagerDoer.resultWFSS, "SUCCESS");
            Assert.AreEqual(wfssResult, "SUCCESS");

            fightManagerCommunicator.Stop();
            myFightManagerDoer.Stop();
            */
            // wfssWebAPI.ArrayOfInt myGames = service.GetGameIds(MyFightManager.FightManagerID.ToString());
            //System.Console.WriteLine("{0}, number of games={1}", myFightManagerId, myGames.Count);
        }
    }
}