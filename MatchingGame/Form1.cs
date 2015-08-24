//Please note: This application is purely for my own education, to run through coding 
//examples by following tutorials, and to just tinker around with coding.  
//I know it’s bad practice to heavily comment code (code smell), but comments in all of my 
//exercises will largely be left intact as this serves me 2 purposes:
//    I want to retain what my original thought process was at the time
//    I want to be able to look back in 1..5..10 years to see how far I’ve come
//    And I enjoy commenting on things, however redundant this may be . . . 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingGame
{
    public partial class Form1 : Form
    {
        #region Global Variables
        // Use this Random object to choose random icons for the squares
        // Random(Object) random(instance of Random(Object)) = new Random(Object)
        private Random random = new Random();

        // Setting up player selections, will change to != null in code once clicked
        // Set to null because these references are not keeping track of anything once the game is initially started
        // Reference objects such as these are used to keep track of objects
        private Label firstClicked = null;
        private Label secondClicked = null;

        // These letters make interesting symbols using the "webdings" font
        private List<string> icons = new List<string>()
        {
            "!",
            "!",
            "N",
            "N",
            ",",
            ",",
            "k",
            "k",
            "b",
            "b",
            "v",
            "v",
            "w",
            "w",
            "z",
            "z"
        };
        #endregion

        public Form1()
        {
            InitializeComponent();
            AssignIconsToSquares();
        }

        private void AssignIconsToSquares()
        {
            // Assigning the grid with randomized icons
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);
                    iconLabel.Text = icons[randomNumber];
                    iconLabel.ForeColor = iconLabel.BackColor;
                    icons.RemoveAt(randomNumber);
                }
            }// End foreach
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)
                return;

            // Setting up what happens when a square is clicked
            Label clickedLabel = sender as Label;

            if (clickedLabel != null)
            {
                // Ignores selection if already in use
                if (clickedLabel.ForeColor == Color.Black)
                    return;

                // assigns the 1st click if its the 1st selection
                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    clickedLabel.ForeColor = Color.Black;
                    return;
                }

                // if the player gets this far, the timer isn't running and firstClicked isn't null
                // must be the second icon clicked
                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                // Check to see if the player has won
                CheckForWinner();

                // This occurs when two matching icons are chosen to keep them on the game board
                if (firstClicked.Text == secondClicked.Text)
                {
                    firstClicked = null;
                    secondClicked = null;
                    return;
                }

                // If the player got this far, this is after the player selected 2 icons
                // Time to bust out the timer
                timer1.Start();
            }// End if statements
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // Stop the timer (property is set at .750 seconds)
            timer1.Stop();

            // Hide both icons
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;

            // Reset both clicks
            firstClicked = null;
            secondClicked = null;
        }

        private void CheckForWinner()
        {
            // Will check every icons color to verify if all icons have been selected to declare victory
            foreach (Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.ForeColor == iconLabel.BackColor)
                        return;
                }
            }

            // If the loop didn't return;, then all matches have been found and displays the victory box
            MessageBox.Show("You matched all the icons!", "Gratz!");
            Close();
        }
    }
}
