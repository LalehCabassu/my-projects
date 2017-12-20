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
    public class TestInstigateFight : TestProtocols
    {
        [TestMethod]
        public void InstigateFightTester()
        {/*
            //Communicators
            Common.Communicator.Communicator fightManagerCommunicator;
            Common.Communicator.Communicator balloonManagerCommunicator;
            Common.Communicator.Communicator waterManagerCommunicator;
            Common.Communicator.Communicator firstPlayerCommunicator;
            Common.Communicator.Communicator secondPlayerCommunicator;
            IPEndPoint fightManagerEP;
            IPEndPoint balloonManagerEP;
            IPEndPoint waterManagerEP;

            //Objects
            //BalloonManager.BalloonManager myBalloonManager;
            //-> Add Water Manager
            Server.FightManager myFightManager;
            WaterBalloon firstNewBalloon;
            WaterBalloon secondNewBalloon;
            Location firstLocation;
            Location secondLocation;
            Location thirdLocation;
            Player.Player firstPlayer;
            Player.Player secondPlayer;
            
            //Doers
            //BalloonManagerDoer myBalloonManagerDoer;
            FightManagerDoer myFightManagerDoer;
            PlayerDoer firstPlayerDoer;
            PlayerDoer secondPlayerDoer;
            
            fightManagerCommunicator = new Common.Communicator.Communicator();
            balloonManagerCommunicator = new Common.Communicator.Communicator();
            waterManagerCommunicator = new Common.Communicator.Communicator();
            firstPlayerCommunicator = new Common.Communicator.Communicator();
            secondPlayerCommunicator = new Common.Communicator.Communicator();
            
            fightManagerCommunicator.Start();
            balloonManagerCommunicator.Start();
            waterManagerCommunicator.Start();
            firstPlayerCommunicator.Start();
            secondPlayerCommunicator.Start();
            
            fightManagerEP = fightManagerCommunicator.LocalEP;
            balloonManagerEP = balloonManagerCommunicator.LocalEP;
            waterManagerEP = waterManagerCommunicator.LocalEP;

            //myBalloonManager = new BalloonManager.BalloonManager(fightManagerEP, waterManagerEP);
            myFightManager = new Server.FightManager("Laleh's Fight Manager", "Laleh Rostami Hosoori", "laleh.rostami@aggiemail.usu.edu", balloonManagerEP, waterManagerEP);

            firstNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.Medium, WaterBalloon.PossibleColor.Blue);
            secondNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.Large, WaterBalloon.PossibleColor.Red);
            
            firstLocation = new Location(1, 2);
            secondLocation = new Location(2, 3);
            thirdLocation = new Location(3, 4);
            
            firstPlayer = new Player.Player("Laleh", 28, true, firstLocation, fightManagerEP, balloonManagerEP, waterManagerEP);
            secondPlayer = new Player.Player("Sara", 32, true, secondLocation, fightManagerEP, balloonManagerEP, waterManagerEP);
            
            //myBalloonManagerDoer = new BalloonManagerDoer(balloonManagerCommunicator, myBalloonManager);
            myFightManagerDoer = new FightManagerDoer(fightManagerCommunicator, myFightManager);
            firstPlayerDoer = new PlayerDoer(firstPlayerCommunicator, firstPlayer);
            secondPlayerDoer = new PlayerDoer(secondPlayerCommunicator, secondPlayer);
            
            //myBalloonManagerDoer.Start();
            myFightManagerDoer.Start();
            firstPlayerDoer.Start();
            secondPlayerDoer.Start();
           */ 
            // Players-> Registration requests
            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(1000);

            //Create a new balloon and fill it
            firstNewBalloon.IncreasePercentFilled(50);
            firstPlayer.GetNewBalloon(firstNewBalloon);

            // Instigate a new fight - Hit
            int preNoFights =  myFightManager.FightList.Count;
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, secondLocation, firstNewBalloon.AmountOfWater, firstNewBalloon.BalloonID);
            Thread.Sleep(2000);
            int newNoFights = myFightManager.FightList.Count;
            
            Assert.AreEqual(newNoFights - preNoFights, 1);
            Assert.AreEqual(myFightManager.FightList.Last().PlayerList.Count, 2);
            
            // Instigate a new fight - Not Hit
            preNoFights = myFightManager.FightList.Count;
            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(secondPlayer.PlayerID, thirdLocation, secondNewBalloon.AmountOfWater, secondNewBalloon.BalloonID);
            newNoFights = myFightManager.FightList.Count;
            
            Assert.IsTrue(newNoFights - preNoFights == 0);
            Assert.AreEqual(myFightManager.FightList.Last().PlayerList.Count, 2);
            /*
            fightManagerCommunicator.Stop();
            balloonManagerCommunicator.Stop();
            waterManagerCommunicator.Stop();
            firstPlayerCommunicator.Stop();
            secondPlayerCommunicator.Stop();
            
            //myBalloonManagerDoer.Stop();
            myFightManagerDoer.Stop();
            firstPlayerDoer.Stop();
            secondPlayerDoer.Stop();
            */
            StopThreads();
        }
    }
}
