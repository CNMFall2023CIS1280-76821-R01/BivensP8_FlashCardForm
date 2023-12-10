using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BivensP8_FlashCardForm
{
    class DBManager
    {
        string connStr = ConfigurationManager.ConnectionStrings["FlashCardDB"].ConnectionString;

        public void GetCards(BindingList<Card> cards)
        {
            string sqlCardData = "SELECT * FROM Card";
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand selectCmd = new SqlCommand(sqlCardData, conn);
                conn.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();

                while (dr.Read())
                {
                    Card card = new Card
                    {
                        CardID = Convert.ToInt32(dr["CardID"]),
                        Title = Convert.ToString(dr["Title"]),
                        Question = Convert.ToString(dr["Question"]),
                        Answer = Convert.ToString(dr["Answer"]),
                        NumRight = Convert.ToInt32(dr["NumRight"]),
                        NumWrong = Convert.ToInt32(dr["NumWrong"])
                    };

                    cards.Add(card);
                }

            }
        }

        public void AddCard(Card card)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                string query = "INSERT INTO Card (Title, Question, Answer, NumRight, NumWrong) VALUES (@Title, @Question, @Answer, @NumRight, @NumWrong)";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Title", card.Title);
                    command.Parameters.AddWithValue("@Question", card.Question);
                    command.Parameters.AddWithValue("@Answer", card.Answer);
                    command.Parameters.AddWithValue("@NumRight", card.NumRight);
                    command.Parameters.AddWithValue("@NumWrong", card.NumWrong);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveCard(Card card)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                string query = "DELETE FROM Card WHERE CardID = @CardId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CardId", card.CardID);
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Card card)
        {
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                string query = "UPDATE Card SET Title=@Title, Question = @Question, Answer = @Answer, NumRight= @NumRight, NumWrong= @NumWrong WHERE CardID=@CardId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@CardId", card.CardID);
                    command.Parameters.AddWithValue("@Title", card.Title);
                    command.Parameters.AddWithValue("@Question", card.Question);
                    command.Parameters.AddWithValue("@Answer", card.Answer);
                    command.Parameters.AddWithValue("@NumRight", card.NumRight);
                    command.Parameters.AddWithValue("@NumWrong", card.NumWrong);

                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
