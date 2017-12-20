/*
 * Laleh Rostami Hosoori
 * A01772483
 * CS5200
 * HW 1
 */

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace ConsoleVersion
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\n\n======================WORD GAME=======================\n\n");
            
            WordGame myGame = new WordGame();
            bool flag = false;    //True: An new game started

            Console.WriteLine("\nN: Start a NEW game");
            Console.WriteLine("I: Change the Server's IP Address");
            Console.WriteLine("X: EXIT");
            Console.Write("\nCOMMAND? ");
            string command = Console.ReadLine();
            
            while (command.Length > 0)
            {
                command = command.Trim().ToUpper().Substring(0, 1);
                if (command == "N")
                {
                    myGame.newGame();
                    flag = true;
                }
                else if (command == "G" && flag)
                {
                    Console.Write("Your guess? ");
                    string guess = Console.ReadLine();
                    myGame.makeGuess(guess);
                    flag = false;
                }
                else if (command == "H" && flag)
                    myGame.getHint();
                else if (command == "I")
                {
                    myGame.getServerEndPoint();
                    flag = false;
                }
                else if (command == "X")
                    break;
                else
                    Console.WriteLine("First start a NEW GAME and then enter a proper command.");
                Console.WriteLine("\n============================================");
                Console.WriteLine("N-> New game           G-> Make a GUESS");
                Console.WriteLine("H-> Get a HINT         I-> Server's IP Address");
                Console.WriteLine("X-> Exit");
                Console.Write("\nCOMMAND? ");
                command = Console.ReadLine();
            }
        }
    }
}
