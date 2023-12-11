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

        //TODO: Missing fields numRight and numWrong which should back NumRight and NumWrong -5pts
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
            // Calculate the ratio of right to wrong answers and store it in RightWrongRatio
            RightWrongRatio = (float)NumRight / (NumRight + NumWrong);
        }

        public override string ToString()
        {
            // Call calc to make sure the ratio is up to date
            Calc();

            // Return a string with the card's title, number of right answers, number of wrong answers, and the ratio
            string output = $"{Title}:\n" + 
                $"{NumRight} answers correct/{NumWrong} answers incorrect\n" +
                $"A ratio of {RightWrongRatio}\n";
            return output;
        }
        #endregion
    }
}
