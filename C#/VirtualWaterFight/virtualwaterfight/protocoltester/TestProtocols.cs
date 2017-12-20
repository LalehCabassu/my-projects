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
using Common.Communicator;
using BalloonManager;
using WaterManager;

namespace ProtocolTester
{
    [TestClass]
    public class TestProtocols
    {
        #region Memebers
        //Communicators
        public Common.Communicator.Communicator fightManagerCommunicator;
        public Common.Communicator.Communicator balloonManagerCommunicator;
        public Common.Communicator.Communicator waterManagerCommunicator;
        public Common.Communicator.Communicator firstPlayerCommunicator;
        public Common.Communicator.Communicator secondPlayerCommunicator;
        public Common.Communicator.Communicator thirdPlayerCommunicator;
        public Common.Communicator.Communicator fourthPlayerCommunicator;
        public IPEndPoint fightManagerEP;
        public IPEndPoint balloonManagerEP;
        public IPEndPoint waterManagerEP;

        //Objects
        public BalloonManager.BalloonManager myBalloonManager;
        public WaterManager.WaterManager myWaterManager;
        public Server.FightManager myFightManager;
        public WaterBalloon firstNewBalloon;
        public WaterBalloon secondNewBalloon;
        public WaterBalloon thirdNewBalloon;
        public WaterBalloon fourthNewBalloon;
        public Location firstLocation;
        public Location secondLocation;
        public Location thirdLocation;
        public Location fourthLocation; 
        public Player.Player firstPlayer;
        public Player.Player secondPlayer;
        public Player.Player thirdPlayer;
        public Player.Player fourthPlayer;
        
        //Doers
        public BalloonManagerDoer myBalloonManagerDoer;
        public WaterManagerDoer myWaterManagerDoer;
        public FightManagerDoer myFightManagerDoer;
        public PlayerDoer firstPlayerDoer;
        public PlayerDoer secondPlayerDoer;
        public PlayerDoer thirdPlayerDoer;
        public PlayerDoer fourthPlayerDoer;
        #endregion

        #region Methods
        public TestProtocols()
        {
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

            myBalloonManager = new BalloonManager.BalloonManager(fightManagerEP, waterManagerEP);
            myWaterManager = new WaterManager.WaterManager(fightManagerEP, balloonManagerEP);
            myFightManager = new Server.FightManager("Laleh's Fight Manager", "Laleh Rostami Hosoori", "laleh.rostami@aggiemail.usu.edu", balloonManagerEP, waterManagerEP);

            firstNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.Medium, WaterBalloon.PossibleColor.Blue);
            secondNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.Large, WaterBalloon.PossibleColor.Red);
            thirdNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.Small, WaterBalloon.PossibleColor.Orange);
            fourthNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.XSmall, WaterBalloon.PossibleColor.Gray);

            firstLocation = new Location(1, 2);
            secondLocation = new Location(2, 3);
            thirdLocation = new Location(3, 4);
            fourthLocation = new Location(4, 5);
            
            firstPlayer = new Player.Player("Laleh", 28, true, firstLocation, fightManagerEP, balloonManagerEP, waterManagerEP);
            secondPlayer = new Player.Player("Sara", 32, true, secondLocation, fightManagerEP, balloonManagerEP, waterManagerEP);
            thirdPlayer = new Player.Player("Maria", 19, true, thirdLocation, fightManagerEP, balloonManagerEP, waterManagerEP);
            fourthPlayer = new Player.Player("Jane", 21, true, fourthLocation, fightManagerEP, balloonManagerEP, waterManagerEP);
            
            myBalloonManagerDoer = new BalloonManagerDoer(balloonManagerCommunicator, myBalloonManager);
            myWaterManagerDoer = new WaterManagerDoer(waterManagerCommunicator, myWaterManager);
            myFightManagerDoer = new FightManagerDoer(fightManagerCommunicator, myFightManager);
            firstPlayerDoer = new PlayerDoer(firstPlayerCommunicator, firstPlayer);
            secondPlayerDoer = new PlayerDoer(secondPlayerCommunicator, secondPlayer);
            thirdPlayerDoer = new PlayerDoer(thirdPlayerCommunicator, thirdPlayer);
            fourthPlayerDoer = new PlayerDoer(fourthPlayerCommunicator, fourthPlayer);

            myBalloonManagerDoer.Start();
            myWaterManagerDoer.Start();
            myFightManagerDoer.Start();
            firstPlayerDoer.Start();
            secondPlayerDoer.Start();
            thirdPlayerDoer.Start();
            fourthPlayerDoer.Start();
        }
        #endregion

        public void StopThreads()
        {
            fightManagerCommunicator.Stop();
            balloonManagerCommunicator.Stop();
            waterManagerCommunicator.Stop();
            firstPlayerCommunicator.Stop();
            secondPlayerCommunicator.Stop();
            thirdPlayerCommunicator.Stop();
            fourthPlayerCommunicator.Stop();

            myBalloonManagerDoer.Stop();
            myWaterManagerDoer.Stop();
            myFightManagerDoer.Stop();
            firstPlayerDoer.Stop();
            secondPlayerDoer.Stop();
            thirdPlayerDoer.Stop();
            fourthPlayerDoer.Stop();
        }
    }
}
