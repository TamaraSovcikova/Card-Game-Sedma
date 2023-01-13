using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedma_Game
{
    internal class Table
    {

        protected int winningPlayer = 0; //the player that beat the cardTpBeat

        protected int leadPlayer = 0; //the player that starts the round


        public List<Player> players;
        protected List<Card> cards;
        protected List<Card> pile;
        protected List<Card> playedCards = new List<Card>();
        public Card cardToBeat = null;
        public int teamAPoints = 0;
        public int teamBPoints = 0;
        public bool pileDone = false;
        public bool gameover = false;


        public List<Card> CreateDeck()
        {
            //makes a deck of 32 cards 
            List<Card> cards = new List<Card>();
            string[] suits = { "heart", "leaf", "bell", "acorn" };
            string[] faces = { "7", "8", "9", "10", "lower", "upper", "king", "ace" };
            for (int i = 0; i < suits.GetLength(0); i++)
            {
                for (int y = 0; y < faces.GetLength(0); y++)
                {
                    cards.Add(new Card(suits[i], faces[y]));
                }
            }
            return cards;
        }
        public List<Card> CreateGamePile()
        {
            //makes a new list which will hold the shuffled card
            List<Card> pile = new List<Card>();
            Random rnd = new Random();
            pile = cards.OrderBy(x => rnd.Next(0, cards.Count)).ToList();
            return pile;
        }
        public bool PileHasCards()
        {
            return pile.Count() > 0;
        }
        public void HandOutCards()
        {
            //gives each player the amount of cards they are missing
            for (int i = 0; i < players.Count(); i++)
            {
                int playerToGetCards = (i + leadPlayer) % players.Count();
                if (pile.Count() > 4)
                {
                    while (players[playerToGetCards].PlayerNeedsCards() == true)
                    {
                        if (PileHasCards())
                            players[playerToGetCards].GetCard(pile);
                        else
                            break;
                    }
                }
                else
                {
                    if (players[playerToGetCards].PlayerNeedsCards() == true)
                    {
                        if (PileHasCards())
                            players[playerToGetCards].GetCard(pile);
                        else
                            break;
                    }
                }
            }
        }
        public void ShowPlayerCards()
        {
            //Shows the cards of all players
            foreach (Player p in players)
            {
                Console.WriteLine("\n" + p.GetName() + " has cards: ");
                p.ShowOnHand();
            }
        }
        public void StartGame(List<Player> players)
        {
            //Does all the neccesery stuff to start a new game
            Console.WriteLine("----A New Game Has Begun----");
            this.players = players;
            cards = CreateDeck();
            pile = CreateGamePile();
        }
        public void PlayGame(List<Player> players)
        {
            StartGame(players);
            do
            {
                if (PileHasCards())
                    HandOutCards();
                //to not have to repeat it will only say this once;
                else if (!PileHasCards() && pileDone == false)
                {
                    Console.WriteLine("\n-----------------THE PILE HAS RUN OUT OF CARDS!---------------");
                    pileDone = true;
                }

                PlayRound();

            }
            while (!gameover);
            EvaluateGame();
            ShowResults();
        }
        public void PlayRound()
        {
            winningPlayer = leadPlayer;
            //Check if player has any cards left or else end the game
            int currentPlayer = 0;
            bool endRound = false;
            bool roundPass = false;

            do
            {
                for (int i = 0; i < players.Count(); i++)
                {
                    currentPlayer = (leadPlayer + i) % players.Count();
                    if (players[currentPlayer].HaveCards())
                    {
                        if (cardToBeat != null)
                        {
                            Console.WriteLine("--------->Card to Beat: " + cardToBeat.suit + " " + cardToBeat.face + " <----------");
                        }

                        Card cardChosen = players[currentPlayer].PlayerChooseCard(currentPlayer == leadPlayer && roundPass, cardToBeat, winningPlayer);


                        if (cardChosen != null)
                        {
                            playedCards.Add(cardChosen); //add the played card to the second pile
                            Console.WriteLine("\n" + players[currentPlayer].GetName() + " played: " + cardChosen.suit + " " + cardChosen.face);
                        }

                        if (cardChosen != null)
                        {
                            if (cardToBeat == null)
                            {
                                //if card to beat isnt set yet, set it
                                cardToBeat = cardChosen;
                            }
                            else
                            {
                                //check if it matches
                                if (cardToBeat.face == cardChosen.face || cardChosen.face == "7")
                                {
                                    winningPlayer = currentPlayer;
                                    Console.WriteLine("!!! " + players[currentPlayer].GetName() + " Now OWNS the pile !!!");
                                }
                            }
                        }
                        else
                        {
                            endRound = true;
                            break;
                        }
                    }
                    else
                    {
                        endRound = true;
                    }
                }
                roundPass = true;
            }
            while (endRound != true);
            leadPlayer = winningPlayer;
            cardToBeat = null;
            players[leadPlayer].CollectWonCards(playedCards);
            playedCards.Clear();
            Console.WriteLine("\nWinner of the Round IS --> " + players[leadPlayer].GetName());

            if (!PileHasCards() && EndGame())
            {
                gameover = true;
            }
        }
        public void EvaluateGame()
        {
            foreach (Player p in players)
            {
                p.CollectedPoints();
            }

            if (players.Count == 4)
            {
                for (int i = 0; i < 4; i++)
                {
                    if (players[i].team == "A")
                        teamAPoints += players[i].collectedPoints;
                    else
                        teamBPoints += players[i].collectedPoints;
                }
            }
            Console.WriteLine("END");
            Console.WriteLine("\n\n---------------------------------------------------------------------------------");
            Console.WriteLine("Now to sum up all the points...");
        }
        public void ShowResults()
        {
            if (players.Count == 4)
            {
                if (teamAPoints > teamBPoints)
                {
                    Console.WriteLine("\n--->Congratulations! Team A, Won this game!<---");
                    Console.WriteLine("With a total of: " + teamAPoints + "!");
                }
                else if (teamBPoints > teamAPoints)
                {
                    Console.WriteLine("\n--->Congratulations! Team B, Won this game!<---");
                    Console.WriteLine("With a total of: " + teamBPoints + "!");
                }
                else
                {
                    Console.WriteLine("\nCongratulations to both teams! It seems that this game ended in a DRAW!");
                }
            }
            else
            {
                if (players[0].collectedPoints > players[1].collectedPoints)
                    Console.WriteLine("\nCongratulation! Seems like " + players[0].GetName() + " won this game!");
                else if (players[0].collectedPoints < players[1].collectedPoints)
                    Console.WriteLine("\nCongratulation! Seems like " + players[1].GetName() + " won this game!");
                else
                    Console.WriteLine("\nCongratulations to both players! It seems that htis game ended in a DRAW!");
            }
        }
        public bool EndGame()
        {
            foreach (Player p in players)
            {
                if (p.HaveCards())
                {
                    return false;
                }
            }
            return true;
        }
    }
}
