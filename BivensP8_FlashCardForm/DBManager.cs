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
        // Create a connection string to the database
        string connStr = ConfigurationManager.ConnectionStrings["FlashCardDB"].ConnectionString;

        public void GetCards(BindingList<Card> cards)
        {
            // Create the query to get all cards from the database
            string sqlCardData = "SELECT * FROM Card";

            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                SqlCommand selectCmd = new SqlCommand(sqlCardData, connection);
                connection.Open();
                SqlDataReader dr = selectCmd.ExecuteReader();

                // Loop through the data reader and add cards to the list
                while (dr.Read())
                {
                    // Create a new card to hold db data
                    Card card = new Card
                    {
                        CardID = Convert.ToInt32(dr["CardID"]),
                        Title = Convert.ToString(dr["Title"]),
                        Question = Convert.ToString(dr["Question"]),
                        Answer = Convert.ToString(dr["Answer"]),
                        NumRight = Convert.ToInt32(dr["NumRight"]),
                        NumWrong = Convert.ToInt32(dr["NumWrong"])
                    };

                    // Add the card to the list
                    cards.Add(card);
                }

            }
        }

        public void AddCard(Card card)
        {
            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connStr))
            {
            
                // Open the connection
                connection.Open();

                // Create the query to add a card to the database
                string query = "INSERT INTO Card (Title, Question, Answer, NumRight, NumWrong) VALUES (@Title, @Question, @Answer, @NumRight, @NumWrong)";

                // Create the command to add the card to the database
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the query
                    command.Parameters.AddWithValue("@Title", card.Title);
                    command.Parameters.AddWithValue("@Question", card.Question);
                    command.Parameters.AddWithValue("@Answer", card.Answer);
                    command.Parameters.AddWithValue("@NumRight", card.NumRight);
                    command.Parameters.AddWithValue("@NumWrong", card.NumWrong);

                    // Execute the query/add to the database
                    command.ExecuteNonQuery();
                }
            }
        }

        public void RemoveCard(Card card)
        {
            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                // Open the connection
                connection.Open();

                // Create the query to remove a card from the database
                string query = "DELETE FROM Card WHERE CardID = @CardId";

                // Create the command to remove the card from the database
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add cardID to the query
                    command.Parameters.AddWithValue("@CardId", card.CardID);

                    // Execute the query/remove from the database
                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Card card)
        {
            // Create a connection to the database
            using (SqlConnection connection = new SqlConnection(connStr))
            {
                connection.Open();

                // Create the query to update the db entry
                string query = "UPDATE Card SET Title=@Title, Question = @Question, Answer = @Answer, NumRight= @NumRight, NumWrong= @NumWrong WHERE CardID=@CardId";

                // Create the command to update the db entry
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    // Add parameters to the query to update the db entry
                    command.Parameters.AddWithValue("@CardId", card.CardID);
                    command.Parameters.AddWithValue("@Title", card.Title);
                    command.Parameters.AddWithValue("@Question", card.Question);
                    command.Parameters.AddWithValue("@Answer", card.Answer);
                    command.Parameters.AddWithValue("@NumRight", card.NumRight);
                    command.Parameters.AddWithValue("@NumWrong", card.NumWrong);

                    // Execute the query
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}
