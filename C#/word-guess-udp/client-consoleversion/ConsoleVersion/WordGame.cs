/*
 * Laleh Rostami Hosoori
 * A01772483
 * CS5200
 * HW 1
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace ConsoleVersion
{
    class WordGame
    {
        int gameID;
        string definition;
        IPEndPoint serverEP, clientEP;
        UdpClient udpClient;

        public WordGame()
        {
            getServerEndPoint();
            clientEP = new IPEndPoint(IPAddress.Any, 12400);
            udpClient = new UdpClient(clientEP);
        }

        public void getServerEndPoint()
        {
            string hostName;
            Console.WriteLine("*Default IP Address of the Word Game Server: 127.0.0.1*");
            Console.Write("Press Y to change it: ");
            string cIP = Console.ReadLine();
            cIP = cIP.Trim().ToUpper().Substring(0, 1);
            if (cIP == "Y")
            {
                Console.Write("\nNew IP Address ? ");
                hostName = Console.ReadLine();
            }
            else
                hostName = "127.0.0.1";
            serverEP = new IPEndPoint(GetHostAddress(hostName), 12001);
        }

        public void newGame()
        {
            sendMessage("newgame:");
            string[] subMessage = receiveMessage();
            if (subMessage == null || subMessage[0] != "def")
                Console.WriteLine("newGame-> Error");
            else
            {
                gameID = Convert.ToInt32(subMessage[1]);
                definition = subMessage[3];
                Console.WriteLine("\nDEFINITION: " + definition);
            }
        }

        public void makeGuess(string guess)
        {
            sendMessage("guess:" + gameID.ToString() + "," + guess);
            string[] subMessage = receiveMessage();
            if (subMessage == null || subMessage[0] != "answer" || Convert.ToInt32(subMessage[1]) != gameID)
                Console.WriteLine("makeGuess-> Error");
            else
            {
                if (subMessage[2] == "T")
                    Console.WriteLine("\n***WIN***");
                else
                    Console.WriteLine("\n***LOSE***");
                Console.WriteLine("Score: " + subMessage[3]);
            }
        }

        public void getHint()
        {
            sendMessage("gethint:" + gameID.ToString());
            string[] subMessage = receiveMessage();
            if (subMessage == null || subMessage[0] != "hint" || Convert.ToInt32(subMessage[1]) != gameID)
                Console.WriteLine("getHint-> Error");
            else
            {
                Console.Write("\nHINT: ");
                foreach(char c in subMessage[2])
                    Console.Write(c.ToString() + " " );
                Console.WriteLine();
            }
        }
       
        static private IPAddress GetHostAddress(string hostName)
        {
            IPAddress result = null;
            IPAddress[] addresses = Dns.GetHostAddresses(hostName);
            if (addresses.Length > 0 && addresses[0] != null)
                result = addresses[0];
            return result;
        }
        
        private string[] receiveMessage()
        {
            try
            {
                byte[] receiveBuffer = udpClient.Receive(ref serverEP);
                string message = Encoding.ASCII.GetString(receiveBuffer);
                string delimStr = ":,";
                char[] delimiter = delimStr.ToCharArray();
                string[] subMessage = null;
                subMessage = message.Split(delimiter);
                if (subMessage[0] == "error")
                    Console.WriteLine("Server-> Error");
                return subMessage;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        private void sendMessage(string message)
        {
            try
            {
                byte[] sendBuffer = Encoding.ASCII.GetBytes(message);
                udpClient.Send(sendBuffer, sendBuffer.Length, serverEP);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}

