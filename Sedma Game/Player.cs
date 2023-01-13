using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedma_Game
{
    internal class Player
    {
        protected string name;
        protected List<Card> onHand = new List<Card>();
        protected List<Card> cardsWon = new List<Card>();
        public int collectedPoints = 0;
        public string team = "";

        public Player(string name)
        {
            this.name = name;
        }
        public string GetName()
        {
            return name;
        }
        public void CollectedPoints()
        {
            foreach (Card c in cardsWon)
            {
                collectedPoints += c.points;
            }
        }
        public bool HaveCards()
        {
            return onHand.Count > 0;
        }
        public void CollectWonCards(List<Card> playedCards)
        {
            foreach (Card card in playedCards)
            {
                cardsWon.Add(card);
            }
        }
        public bool PlayerNeedsCards()
        {
            if (onHand.Count < 4)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void GetCard(List<Card> pile)
        {
            for (int i = 0; i < (4 - onHand.Count); i++)
            {
                if (pile.Count > 0)
                {
                    onHand.Add(pile[0]);
                    pile.RemoveAt(0);
                }
                else
                {
                    break;
                }
            }
        }
        public void ShowOnHand()
        {
            int cardNumber = 1;
            foreach (Card card in onHand)
                Console.WriteLine((cardNumber++) + "; " + card.suit + " " + card.face);
        }
        public Card PlayerChooseCard(bool canPass, Card cardToBeat, int winningPlayer)
        {
            Card cardChosen = null;
            if (onHand.Count != 0)
            {
                Console.WriteLine("\n---> " + GetName() + ", Your Turn <---");
                ShowOnHand();
                bool valid = false;
                do
                {
                    if (canPass)
                    {
                        Console.Write("Looped back to you, to pass press 5, or play a card: ");
                    }
                    else
                        Console.Write("Choose the index of the card:");
                    try
                    {
                        int c = int.Parse(Console.ReadLine()) - 1;
                        if (c == 4 && canPass)
                        {
                            valid = true;
                            return null;
                        }
                        cardChosen = onHand.ElementAt(c);
                        if (canPass && !(cardChosen.face == cardToBeat.face || cardChosen.face == "7"))
                        {
                            Console.WriteLine("The card you selected cant beat the card to beat, therefore passing");
                            valid = true;
                            return null;
                        }
                        onHand.RemoveAt(c);
                        valid = true;
                    }
                    catch
                    {
                        Console.WriteLine("That card does not exist");
                        valid = false;
                    }
                } while (!valid);
            }
            else
            {
                Console.WriteLine("Im sorry but you have no cards left.");
            }
            return cardChosen;
        }



    }
}
