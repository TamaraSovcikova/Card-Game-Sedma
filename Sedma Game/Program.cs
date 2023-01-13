using System;
using System.Collections.Generic;
using System.Linq;

namespace Sedma_Game
{
    internal class Program 
    {
        public static List<Player> players = new List<Player>();    

        static void Main(string[] args)
        {
            bool nextGame = true;
            do
            {
                CreatePlayers();
                AssignTeams();
                DisplayTeams();

                Table gametable = new Table();
                gametable.PlayGame(players);

                nextGame = PlayAgain();
                Console.WriteLine("\n\n");
            }
            while (nextGame);

            Console.WriteLine("\n\nGAME END");
            Console.ReadKey();
        }
        static bool PlayAgain()
        {            
            Console.Write("\n---------------------------------------------------------------------------------");
            Console.Write("\nWant to play a new Game? Press 1: ");
            string input = Console.ReadLine();

            if (input == "1")
                return true;
            else
            {
                Console.WriteLine("Thank you for playing, hope you had fun!");
                return false;
            }
        }
        static void CreatePlayers()
        {
            Console.WriteLine("-----------Welcome to a NEW game of SEDMA!-----------");
            bool playerValid = true;

            // will continue checking until the user enters a valid number of players
            do
            {
                Console.Write("To start, please enter the number of players: ");
                try
                {
                    int playerNum = int.Parse(Console.ReadLine());
                    if (playerNum == 2 || playerNum == 4)
                    {
                        //Since the players entered a valid number they will now get to make their profiles                        

                        for (int i = 0; i < playerNum; i++)
                        {
                            Console.Write("For player " + (i + 1) + " enter name: ");
                            string name = Console.ReadLine();
                            players.Add(new Player(name));
                        }
                        playerValid = true;
                    }
                    else
                    {
                        Console.WriteLine("For this game to work, please decide between 2 or 4 players!");
                        playerValid = false;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    playerValid= false;
                }
            }
            while (playerValid == false);
        }
        static void AssignTeams()
        {
            if (players.Count() == 4)
            {
                Console.WriteLine("Please choose the members of Team A (their index): ");

                for (int i = 0; i < 2; i++)
                {
                    bool valid = false;
                    do
                    {
                        try
                        {
                            int p = int.Parse(Console.ReadLine()) - 1;
                            if (players[p].team == "A")
                                Console.WriteLine("This player is already part of Team A");
                            else
                            {
                                players[p].team = "A";
                                valid = true;
                            }
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            valid = false;
                        }
                    }
                    while (!valid);
                }
            }
        }
        static void DisplayTeams()
        {
            for (int i = 0; i < 4; i++)
            {
                bool valid = false;
                do
                {
                    if (players[i].team == "A")
                        break;
                    else
                    {
                        players[i].team = "B";
                        valid = true;
                    }
                }
                while (!valid);
            }
            Console.WriteLine("\nSo The Teams Are: ");
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(players[i].GetName() + " is TEAM " + players[i].team);
            }
        }
      
    }
}







