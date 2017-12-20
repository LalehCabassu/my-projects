using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Objects;

namespace Player
{
    class Program
    {
        static void Main(string[] args)
        {
            /* 
             Common.Communicator.Communicator MyCommunicator = new Common.Communicator.Communicator();
             MyCommunicator.Start();

             Location MyLocation = new Location(1, 2);
             Player MyPlayer = new Player("Laleh", 28, true, MyLocation);
             PlayerDoer MyPlayerDoer = new PlayerDoer(MyCommunicator, MyPlayer);
             MyPlayerDoer.Start();

             //RegistrationRequestDoer myReg = new RegistrationRequestDoer();
             //MyPlayerDoer.myRegistrationRequestDoer.Send();
             /*
             //EmptyBalloonRequestDoer MyEmptyBalloonRequestDoer = new EmptyBalloonRequestDoer();
             //MyEmptyBalloonRequestDoer.Start();

             WaterBalloon newBalloon = new WaterBalloon(WaterBalloon.PossibleSize.Medium, WaterBalloon.PossibleColor.Brown);
             //MyEmptyBalloonRequestDoer.Send(newBalloon);
             MyPlayerDoer.myEmptyBalloonRequestDoer.Start();
            
             WaterRequestDoer MyWaterRequestDoer = new WaterRequestDoer();
             //MyWaterRequestDoer.Start();
             MyPlayerDoer.myWaterRequestDoer.Start();
       
             string consoleCommand = string.Empty;
             while (consoleCommand.Trim().ToUpper() != "EXIT")
             {
                 System.Console.WriteLine("Type 'EXIT' to stop the player");
                 consoleCommand = System.Console.ReadLine();
             }
        
             MyCommunicator.Stop();
             MyPlayerDoer.Stop();
             //MyEmptyBalloonRequestDoer.Stop();
             //MyWaterRequestDoer.Stop();
           */
        }
    }
}
