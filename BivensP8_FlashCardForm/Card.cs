// Joshua Bivens
// jbivens1@cnm.edu
// BivensP8 - Flash Card Form

// Card.cs

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BivensP8_FlashCardForm
{
    internal class Card
    {
        #region Properties
        public int CardID { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string Answer { get; set; }
        public int NumRight { get; set; }
        public int NumWrong { get; set; }
        public float RightWrongRatio { get; set; }
        #endregion

        #region Constructors
        public Card() : this(0, "Unknown Title", "Unknown Question", "Unknown Answer", 0, 0)
        {

        }

        public Card(int card_id, string title, string question, string answer, int num_right, int num_wrong)
        {
            CardID = card_id;
            Title = title;
            Question = question;
            Answer = answer;
            NumRight = num_right;
            NumWrong = num_wrong;
        }
        #endregion

        #region Methods
        public void Calc()
        {
            // What is Calc() supposed to do?
        }

        public override string ToString()
        {
            string output = $"{Title}: {NumRight} answers correct/{NumWrong} answers incorrect";
            return output;
        }
        #endregion
    }
}
