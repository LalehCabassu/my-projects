using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net;
using System.Threading;

using Common;
using Common.Communicator;
using Objects;
using Player;
using FightManager;
using BalloonManager;
using WaterManager;

namespace TestVirtualWaterFight
{
    [TestClass]
    public class ProtocolTester
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
        public FightManager.FightManager myFightManager;
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
        public ProtocolTester()
        {
            
        }

        public void StartThreads()
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
            myFightManager = new FightManager.FightManager("Laleh's Fight Manager", "Laleh Rostami Hosoori", "laleh.rostami@aggiemail.usu.edu", balloonManagerEP, waterManagerEP);

            firstNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.Medium, WaterBalloon.PossibleColor.Blue);
            secondNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.Small, WaterBalloon.PossibleColor.Red);
            thirdNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.XLarge, WaterBalloon.PossibleColor.Orange);
            fourthNewBalloon = new WaterBalloon(WaterBalloon.PossibleSize.XSmall, WaterBalloon.PossibleColor.Gray);

            firstLocation = new Location(1, 2);
            secondLocation = new Location(2, 3);
            thirdLocation = new Location(3, 4);
            fourthLocation = new Location(400, 500);

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
        #endregion
        
        
        
        [TestMethod]
        public void InprocessFightsListTester()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            fourthPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(10000);

            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(secondNewBalloon.Size, secondNewBalloon.Color);
            thirdPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(thirdNewBalloon.Size, thirdNewBalloon.Color);
            Thread.Sleep(15000);

            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstNewBalloon.BalloonID, 80);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondNewBalloon.BalloonID, 50);
            thirdPlayerDoer.MyWaterRequestDoer.SendRequest(thirdNewBalloon.BalloonID, 30);
            Thread.Sleep(15000);

            firstNewBalloon = firstPlayer.BalloonsList.Last();
            secondNewBalloon = secondPlayer.BalloonsList.Last();
            thirdNewBalloon = thirdPlayer.BalloonsList.Last();

            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(fourthPlayer.PlayerID, fourthLocation, firstNewBalloon.AmountOfWater, firstNewBalloon.BalloonID);
            secondPlayerDoer.MyInstigateFightRequestDoer.SendRequest(fourthPlayer.PlayerID, fourthLocation, secondNewBalloon.AmountOfWater, secondNewBalloon.BalloonID);
            thirdPlayerDoer.MyInstigateFightRequestDoer.SendRequest(fourthPlayer.PlayerID, fourthLocation, thirdNewBalloon.AmountOfWater, thirdNewBalloon.BalloonID);
            Thread.Sleep(100000);

            firstPlayerDoer.MyInprogressFightsListRequestDoer.SendRequest();
            Thread.Sleep(10000);

            foreach (WaterFightGame f in firstPlayer.InprocessFightsList)
                Assert.IsNotNull(myFightManager.FindFight(f.FightID));

            StopThreads();
        }

        [TestMethod]
        public void PlayersOfSpecificFight()
        {
            StartThreads();

            firstPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            secondPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            thirdPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            fourthPlayerDoer.MyRegistrationRequestDoer.SendRequest();
            Thread.Sleep(10000);

            firstPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(firstNewBalloon.Size, firstNewBalloon.Color);
            secondPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(secondNewBalloon.Size, secondNewBalloon.Color);
            thirdPlayerDoer.MyEmptyBalloonRequestDoer.SendRequest(thirdNewBalloon.Size, thirdNewBalloon.Color);
            Thread.Sleep(15000);

            firstPlayerDoer.MyWaterRequestDoer.SendRequest(firstNewBalloon.BalloonID, 80);
            secondPlayerDoer.MyWaterRequestDoer.SendRequest(secondNewBalloon.BalloonID, 50);
            thirdPlayerDoer.MyWaterRequestDoer.SendRequest(thirdNewBalloon.BalloonID, 30);
            Thread.Sleep(15000);

            firstNewBalloon = firstPlayer.BalloonsList.Last();
            secondNewBalloon = secondPlayer.BalloonsList.Last();
            thirdNewBalloon = thirdPlayer.BalloonsList.Last();

            firstPlayerDoer.MyInstigateFightRequestDoer.SendRequest(fourthPlayer.PlayerID, fourthLocation, firstNewBalloon.AmountOfWater, firstNewBalloon.BalloonID);
            secondPlayerDoer.MyInstigateFightRequestDoer.SendRequest(fourthPlayer.PlayerID, fourthLocation, secondNewBalloon.AmountOfWater, secondNewBalloon.BalloonID);
            thirdPlayerDoer.MyInstigateFightRequestDoer.SendRequest(fourthPlayer.PlayerID, fourthLocation, thirdNewBalloon.AmountOfWater, thirdNewBalloon.BalloonID);
            Thread.Sleep(100000);

            firstPlayerDoer.MyInprogressFightsListRequestDoer.SendRequest();
            Thread.Sleep(10000);

            firstPlayerDoer.MyPlayersOfSpecificFightRequestDoer.SendRequest(myFightManager.FightList.Last().FightID);
            Thread.Sleep(10000);

            WaterFightGame currentFight = myFightManager.FindFight(myFightManager.FightList.Last().FightID);

            foreach (Objects.Player p in firstPlayer.InprocessFightsList.Last().PlayerList)
                Assert.IsNotNull(currentFight.FindPlayer(p.PlayerID));

            StopThreads();
        }
        
    }
}
