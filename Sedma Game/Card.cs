using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sedma_Game
{
    internal class Card
    {
        //It will assign card values 
        public string suit = "";
        public string face = "";
        public int points = 0;
        public Card(string suit, string face)
        {
            this.suit = suit;
            this.face = face;
            this.points = face == "ace" || face == "10" ? 10 : 0;
        }

    }
}
