using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

using Objects;

//using log4net;
//using log4net.Config;

namespace Server
{
    class Program
    {

        static void Main(string[] args)
        {
            /*
            Common.Communicator.Communicator myCommunicator = new Common.Communicator.Communicator();
            myCommunicator.Start();
            
            BalloonManager  myBalloonManager = new BalloonManager();
            BalloonManagerDoer myBalloonManagerDoer = new BalloonManagerDoer(myCommunicator, myBalloonManager);
            myBalloonManagerDoer.Start();

            FightManager myFightManager = new FightManager(myBalloonManager);
            FightManagerDoer myFightManagerDoer = new FightManagerDoer(myCommunicator, myFightManager);
            myFightManagerDoer.Start();

            WaterManagerDoer myWaterManagerDoer = new WaterManagerDoer(myCommunicator);
            myWaterManagerDoer.Start();

            string consoleCommand = string.Empty;
            while (consoleCommand.Trim().ToUpper() != "EXIT")
            {
                System.Console.WriteLine("Type 'EXIT' to stop the server");
                consoleCommand = System.Console.ReadLine();
            }
            myCommunicator.Stop();
            myFightManagerDoer.Stop();
            myBalloonManagerDoer.Stop();
            myWaterManagerDoer.Stop();
            */
        }
    }
}
