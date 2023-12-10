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
            InitializeComponent();

            cards = new BindingList<Card>();
            manager = new DBManager();

            manager.GetCards(cards);
            lbCards.ItemsSource = cards;
            lbCards.Items.Refresh();
            GetRandomCard();

            btnRight.IsEnabled = false;
            btnWrong.IsEnabled = false;
        }

        private void GetRandomCard()
        {
            Random rand = new Random();
            int index = rand.Next(0, cards.Count);
            currentCard = cards[index];
            txbTitle.Text = currentCard.Title;
            txbQuestion.Text = currentCard.Question;
            lbCardID.Content = currentCard.CardID;
        }
        
        private void DisplayCardQuestion()
        {
            lbCardID.Content = currentCard.CardID;
            txbTitle.Text = currentCard.Title;
            txbQuestion.Text = currentCard.Question;

            txbInstructions.Text = "Think about the answer then click 'Show Answer'";
        }

        private void btnShowAnswer_Click(object sender, RoutedEventArgs e)
        {
            btnRight.IsEnabled = true;
            btnWrong.IsEnabled = true;
            btnShowAnswer.IsEnabled = false;

            txbAnswer.Text = currentCard.Answer;
            txbInstructions.Text = "Click 'Right' or 'Wrong' to continue";
        }

        private void btnRight_Click(object sender, RoutedEventArgs e)
        {
            currentCard.NumRight++;
            manager.Update(currentCard);
            GetRandomCard();
            btnShowAnswer.IsEnabled = true;
            btnRight.IsEnabled = false;
            btnWrong.IsEnabled = false;
            txbAnswer.Text = "";

            lbCards.Items.Refresh();
        }

        private void btnWrong_Click(object sender, RoutedEventArgs e)
        {
            currentCard.NumWrong++;
            manager.Update(currentCard);
            GetRandomCard();
            btnShowAnswer.IsEnabled = true;
            btnWrong.IsEnabled = false;
            btnRight.IsEnabled = false;
            txbAnswer.Text = "";

            lbCards.Items.Refresh();
        }

        // Manager Tab
        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            Card tempCard = new Card(
                0,
                txbAddTitle.Text,
                txbAddQuestion.Text,
                txbAddAnswer.Text,
                0,
                0
            );

            manager.AddCard(tempCard);
            cards.Clear();
            manager.GetCards(cards);
            lbCards.Items.Refresh();

            txbAddTitle.Text = "";
            txbAddQuestion.Text = "";
            txbAddAnswer.Text = "";

            txbManagerInstructions.Text = "Card Added";

        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            Card newCard = (Card)lbCards.SelectedItem;
            newCard.Title = txbAddTitle.Text;
            newCard.Question = txbAddQuestion.Text;
            newCard.Answer = txbAddAnswer.Text;
            newCard.CardID = (int)lbManagerCardID.Content;

            manager.Update(newCard);

            cards.Clear();
            manager.GetCards(cards);
            lbCards.Items.Refresh();

            txbAddTitle.Text = "";
            txbAddQuestion.Text = "";
            txbAddAnswer.Text = "";
            lbManagerCardID.Content = "";

            txbManagerInstructions.Text = "Card Updated";
        }

        private void btnRemove_Click(object sender, RoutedEventArgs e)
        {
            Card tempCard = (Card)lbCards.SelectedItem;
            manager.RemoveCard(tempCard);
            cards.Clear();
            manager.GetCards(cards);
            lbCards.Items.Refresh();

            txbAddTitle.Text = "";
            txbAddQuestion.Text = "";
            txbAddAnswer.Text = "";
            txbManagerInstructions.Text = "Card has been removed.";
        }

        private void lbCards_MouseDoubleClick_1(object sender, MouseButtonEventArgs e)
        {
            Card tempCard = (Card)lbCards.SelectedItem;

            txbAddTitle.Text = tempCard.Title;
            txbAddQuestion.Text = tempCard.Question;
            txbAddAnswer.Text = tempCard.Answer;
            lbManagerCardID.Content = tempCard.CardID;

            txbManagerInstructions.Text = "Card Ready";
        }
    }
}
