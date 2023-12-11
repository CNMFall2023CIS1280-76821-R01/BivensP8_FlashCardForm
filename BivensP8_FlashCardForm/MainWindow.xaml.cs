// Joshua Bivens
// jbivens1@cnm.edu
// BivensP8 - Flash Card Form

//Comments 15/15
//Meeting Requirements 40/40
//Presentation of Output 10/10
//Elegance of Code 25/25
//Design/Pseudocode 10/10
//Grade 100/100

// MainWindow.xaml.cs

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BivensP8_FlashCardForm
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        BindingList<Card> cards;
        Card currentCard;
        DBManager manager;

        public MainWindow()
        {
            // Create the window
            InitializeComponent();

            // Create the list of cards and the database manager
            cards = new BindingList<Card>();
            manager = new DBManager();

            // Get the cards from the database
            manager.GetCards(cards);

            // Display the cards in the listbox
            lbCards.ItemsSource = cards;

            // Refresh the listbox
            lbCards.Items.Refresh();
            
            // Display the first card
            GetRandomCard();

            // Disable the answer buttons
            btnRight.IsEnabled = false;
            btnWrong.IsEnabled = false;
        }

        private void GetRandomCard()
        {
            // Create a random number generator
            Random rand = new Random();

            // Get a random card from the list
            int index = rand.Next(0, cards.Count);
            currentCard = cards[index];

            // Display the card
            txbTitle.Text = currentCard.Title;
            txbQuestion.Text = currentCard.Question;
            lbCardID.Content = currentCard.CardID;
        }
        
        private void DisplayCardQuestion()
        {
            // Display the card
            lbCardID.Content = currentCard.CardID;
            txbTitle.Text = currentCard.Title;
            txbQuestion.Text = currentCard.Question;

            // Set the instructions
            txbInstructions.Text = "Think about the answer then click 'Show Answer'";
        }

        private void btnShowAnswer_Click(object sender, RoutedEventArgs e)
        {
            // Enable the answer buttons
            btnRight.IsEnabled = true;
            btnWrong.IsEnabled = true;
            btnShowAnswer.IsEnabled = false;

            // Display the answer
            txbAnswer.Text = currentCard.Answer;

            // Set the instructions
            txbInstructions.Text = "Click 'Right' or 'Wrong' to continue";
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            // Increment the number of right answers
            currentCard.NumRight++;

            // Update the card in the database
            manager.Update(currentCard);

            // Get a new card
            GetRandomCard();

            // Enable the show answer button
            btnShowAnswer.IsEnabled = true;
            btnRight.IsEnabled = false;
            btnWrong.IsEnabled = false;

            // Clear the answer
            txbAnswer.Text = "";

            // Refresh the listbox
            lbCards.Items.Refresh();
        }

        private void btnWrong_Click(object sender, RoutedEventArgs e)
        {
            // Increment the number of wrong answers
            currentCard.NumWrong++;

            // Update the card in the database
            manager.Update(currentCard);

            // Get a new card
            GetRandomCard();

            // Enable the show answer button
            btnShowAnswer.IsEnabled = true;
            btnWrong.IsEnabled = false;
            btnRight.IsEnabled = false;

            // Clear the answer
            txbAnswer.Text = "";

            // Refresh the listbox
            lbCards.Items.Refresh();
        }

        // Manager Tab
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            // Create a new card
            Card tempCard = new Card(
                0,
                txbAddTitle.Text,
                txbAddQuestion.Text,
                txbAddAnswer.Text,
                0,
                0
            );

            // Add the card to the database
            manager.AddCard(tempCard);

            // Clear the cards list
            cards.Clear();

            // Get the cards from the database
            manager.GetCards(cards);

            // Refresh the listbox
            lbCards.Items.Refresh();

            // Clear the textboxes
            txbAddTitle.Text = "";
            txbAddQuestion.Text = "";
            txbAddAnswer.Text = "";

            // Display the instructions
            txbManagerInstructions.Text = "Card Added";

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            // Create a new card from the selected card
            Card newCard = (Card)lbCards.SelectedItem;
            newCard.Title = txbAddTitle.Text;
            newCard.Question = txbAddQuestion.Text;
            newCard.Answer = txbAddAnswer.Text;
            newCard.CardID = (int)lbManagerCardID.Content;

            // Update the card in the database
            manager.Update(newCard);

            // Clear the cards list
            cards.Clear();

            // Get the cards from the database
            manager.GetCards(cards);

            // Refresh the listbox
            lbCards.Items.Refresh();

            // Clear the textboxes
            txbAddTitle.Text = "";
            txbAddQuestion.Text = "";
            txbAddAnswer.Text = "";
            lbManagerCardID.Content = "";

            // Display the instructions
            txbManagerInstructions.Text = "Card Updated";
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            // Create a new card from the selected card
            Card tempCard = (Card)lbCards.SelectedItem;

            // Remove the card from the database
            manager.RemoveCard(tempCard);

            // Clear the cards list
            cards.Clear();

            // Get the cards from the database
            manager.GetCards(cards);

            // Refresh the listbox
            lbCards.Items.Refresh();

            // Clear the textboxes
            txbAddTitle.Text = "";
            txbAddQuestion.Text = "";
            txbAddAnswer.Text = "";

            // Display the instructions
            txbManagerInstructions.Text = "Card has been removed.";
        }

        private void lbCards_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Create a new card from the selected card
            Card tempCard = (Card)lbCards.SelectedItem;

            // Display the card in the textboxes
            txbAddTitle.Text = tempCard.Title;
            txbAddQuestion.Text = tempCard.Question;
            txbAddAnswer.Text = tempCard.Answer;
            lbManagerCardID.Content = tempCard.CardID;

            // Display the instructions
            txbManagerInstructions.Text = "Card Ready";
        }
    }
}
