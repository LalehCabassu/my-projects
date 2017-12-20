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
        {/*
            //Communicators
            Common.Communicator.Communicator fightManagerCommunicator;
            Common.Communicator.Communicator balloonManagerCommunicator;
            Common.Communicator.Communicator waterManagerCommunicator;
            Common.Communicator.Communicator firstPlayerCommunicator;
            Common.Communicator.Communicator secondPlayerCommunicator;
            Common.Communicator.Communicator thirdPlayerCommunicator;
            Common.Communicator.Communicator fourthPlayerCommunicator;
            IPEndPoint fightManagerEP;
            IPEndPoint balloonManagerEP;
            IPEndPoint waterManagerEP;

            //Objects
            //BalloonManager.BalloonManager myBalloonManager;
            //-> Add Water Manager
            Server.FightManager myFightManager;
            WaterBalloon firstNewBalloon;
            WaterBalloon secondNewBalloon;
            WaterBalloon thirdNewBalloon;
            Location firstLocation;
            Location secondLocation;
            Location thirdLocation;
            Location fourthLocation;
            Player.Player firstPlayer;
            Player.Player secondPlayer;
            Player.Player thirdPlayer;
            Player.Player fourthPlayer;

            //Doers
            //BalloonManagerDoer myBalloonManagerDoer;
            FightManagerDoer myFightManagerDoer;
            PlayerDoer firstPlayerDoer;
            PlayerDoer secondPlayerDoer;
            PlayerDoer thirdPlayerDoer;
            PlayerDoer fourthPlayerDoer;

            fightManagerCommunicator = new Common.Communicator.Communicator();
            balloonManagerCommunicator = new Common.Communicator.Communicator();
            waterManagerCommunicator = new Common.Communicator.Communicator();
            firstPlayerCommunicator = new Common.Communicator.Communicator();
            secondPlayerCommunicator = new Common.Communicator.Communicator();
            thirdPlayerCommunicator = new Common.Communicator.Communicator();
            fourthPlayerCommunicator = new Common.Communicator.Communicator();

            fightManagerCommunicator.Start();
            balloonManagerCommunicator.Start();
            waterManagerCommunicator.Start();
            firstPlayerCommunicator.Start();
            secondPlayerCommunicator.Start();
            thirdPlayerCommunicator.Start();
            fourthPlayerCommunicator.Start();

            fightManagerEP = fightManagerCommunicator.LocalEP;
            balloonManagerEP = balloonManagerCommunicator.LocalEP;
            waterManagerEP = waterManagerCommunicator.LocalEP;

            //myBalloonManager = new BalloonManager.BalloonManager(fightManagerEP, waterManagerEP);
            myFightManager = new Server.FightManager("Laleh's Fight Manager", "Laleh Rostami Hosoori", "laleh.rostami@aggiemail.usu.edu", balloonManagerEP, waterManagerEP);

            firstNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.Medium, WaterBalloon.PossibleColor.Blue);
            secondNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.Large, WaterBalloon.PossibleColor.Red);
            thirdNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.Small, WaterBalloon.PossibleColor.Orange);

            firstLocation = new Location(1, 2);
            secondLocation = new Location(2, 3);
            thirdLocation = new Location(3, 4);
            fourthLocation = new Location(4, 5);

            firstPlayer = new Player.Player("Laleh", 28, true, firstLocation, fightManagerEP, balloonManagerEP, waterManagerEP);
            secondPlayer = new Player.Player("Sara", 32, true, secondLocation, fightManagerEP, balloonManagerEP, waterManagerEP);
            thirdPlayer = new Player.Player("Maria", 19, true, thirdLocation, fightManagerEP, balloonManagerEP, waterManagerEP);
            fourthPlayer = new Player.Player("Jane", 21, true, fourthLocation, fightManagerEP, balloonManagerEP, waterManagerEP);

            //myBalloonManagerDoer = new BalloonManagerDoer(balloonManagerCommunicator, myBalloonManager);
            myFightManagerDoer = new FightManagerDoer(fightManagerCommunicator, myFightManager);
            firstPlayerDoer = new PlayerDoer(firstPlayerCommunicator, firstPlayer);
            secondPlayerDoer = new PlayerDoer(secondPlayerCommunicator, secondPlayer);
            thirdPlayerDoer = new PlayerDoer(thirdPlayerCommunicator, thirdPlayer);
            fourthPlayerDoer = new PlayerDoer(fourthPlayerCommunicator, fourthPlayer);


            //myBalloonManagerDoer.Start();
            myFightManagerDoer.Start();
            firstPlayerDoer.Start();
            secondPlayerDoer.Start();
            thirdPlayerDoer.Start();
            fourthPlayerDoer.Start();
            */
            // Players-> Send registration request
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            fourthPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(2000);

            // Fill new balloons
            firstNewBalloon.IncreasePercentFilled(50);
            firstPlayer.GetNewBalloon(firstNewBalloon);
            secondNewBalloon.IncreasePercentFilled(40);
            thirdPlayer.GetNewBalloon(secondNewBalloon);
            thirdNewBalloon.IncreasePercentFilled(10);
            fourthPlayer.GetNewBalloon(thirdNewBalloon);
            
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
            /*
            fightManagerCommunicator.Stop();
            balloonManagerCommunicator.Stop();
            waterManagerCommunicator.Stop();
            firstPlayerCommunicator.Stop();
            secondPlayerCommunicator.Stop();
            thirdPlayerCommunicator.Stop();
            fourthPlayerCommunicator.Stop();

            //myBalloonManagerDoer.Stop();
            myFightManagerDoer.Stop();
            firstPlayerDoer.Stop();
            secondPlayerDoer.Stop();
            thirdPlayerDoer.Stop();
            fourthPlayerDoer.Stop();
             */
            StopThreads();
        }
    }
}
