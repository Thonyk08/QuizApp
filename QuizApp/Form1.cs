using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuizApp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        //Variables

        Random randomizer = new Random();

        int addend1;
        int addend2;

        int minuend;
        int subtrahend;

        int multiplicand;
        int multiplier;

        int dividend;
        int divisor;

        int timeLeft;

        public void StartTheQuiz()
        {

            //reset checkboxes to default value
            sumCheck.Checked = differenceCheck.Checked = productCheck.Checked
            = quotientCheck.Checked = false;


            // if level 1 is choosen - generate random numbers
            if (radioButton1.Checked == true)
            {
                addend1 = randomizer.Next(50);
                addend2 = randomizer.Next(50);

                minuend = randomizer.Next(1, 101);
                subtrahend = randomizer.Next(1, minuend);

                multiplicand = randomizer.Next(2, 11);
                multiplier = randomizer.Next(2, 11);

                divisor = randomizer.Next(2, 11);
                int temporaryQuotient = randomizer.Next(2, 11);
                dividend = divisor * temporaryQuotient;

                timeLeft = 30;
            }
           
            //if level 2 is choosen - generate random numbers
            else
            {
                addend1 = randomizer.Next(500);
                addend2 = randomizer.Next(500);

                minuend = randomizer.Next(1, 1000);
                subtrahend = randomizer.Next(1, minuend);

                multiplicand = randomizer.Next(2, 21);
                multiplier = randomizer.Next(2, 21);

                divisor = randomizer.Next(2, 21);
                int temporaryQuotient = randomizer.Next(2, 21);
                dividend = divisor * temporaryQuotient;

                timeLeft = 45;
            }


            //set values to variables
            plusLeftLabel.Text = addend1.ToString();
            plusRightLabel.Text = addend2.ToString();
            sum.Value = 0;

            minusLeftLabel.Text = minuend.ToString();
            minusRightLabel.Text = subtrahend.ToString();
            difference.Value = 0;

            timesLeftLabel.Text = multiplicand.ToString();
            timesRightLabel.Text = multiplier.ToString();
            product.Value = 0;

            dividedLeftLabel.Text = dividend.ToString();
            dividedRightLabel.Text = divisor.ToString();
            quotient.Value = 0;


            //start timer
            timeLabel.Text = timeLeft + " seconds";
            timer1.Start();
        }

        //checking whether the answers are correct - return true or false 
        private bool CheckTheAnswer()
        {
            if ((addend1 + addend2 == sum.Value)
                && (minuend - subtrahend == difference.Value)
                && (multiplicand * multiplier == product.Value)
                && (dividend / divisor == quotient.Value))
                return true;
            else
                return false;
        }

        //click on button - start of quiz, button disabled
        private void startButton_Click(object sender, EventArgs e)
        {
            StartTheQuiz();
            startButton.Enabled = false;
        }

        // checking whether the answers are correct during the quiz - if is correct - chceckbox.chcecked = true
        private void CheckCheck()
        {
            if (sum.Value == addend1 + addend2)
            {
                sumCheck.Checked = true;
            }

            if (difference.Value == minuend - subtrahend)
            {
                differenceCheck.Checked = true;
            }

            if (product.Value == multiplicand * multiplier)
            {
                productCheck.Checked = true;
            }

            if (quotient.Value == dividend / divisor)
            {
                quotientCheck.Checked = true;
            }
        }

        //change the background depending on the remaining time
        private void ChangeBg()
        {

            if (timeLeft < 6)
            {
                timeLabel.BackColor = Color.Red;
            }
            else if (timeLeft < 16)
            {
                timeLabel.BackColor = Color.Orange;
            }
            else
            {
                timeLabel.BackColor = Color.Green;
            }
        }


        // GAME logic ...
        private void timer1_Tick(object sender, EventArgs e)
        {
            //is answer correct chcecking
            CheckCheck();


            //is all answers correct?
            if (CheckTheAnswer()) 
            { 
                timer1.Stop();
                MessageBox.Show("You got all the answers right!",
                                "Congratulations!");
                startButton.Enabled = true;
            }
            //already playing the game
            else if (timeLeft > 0)
            {
                timeLeft--;
                timeLabel.Text = timeLeft + " seconds";
            }
            //the time is left
            else
            {
                timer1.Stop();
                timeLabel.Text = "Time's up!";
                MessageBox.Show("You didn't finish in time.", "Sorry!");
                sum.Value = addend1 + addend2;
                difference.Value = minuend - subtrahend;
                product.Value = multiplicand * multiplier;
                quotient.Value = dividend / divisor;
                startButton.Enabled = true;
            }

            ChangeBg();

        }

        //customize the game ...
        private void answer_Enter(object sender, EventArgs e)
        {
            NumericUpDown answerBox = sender as NumericUpDown;

            if (answerBox != null)
            {
                int lengthOfAnswer = answerBox.Value.ToString().Length;
                answerBox.Select(0, lengthOfAnswer);
            }
        }
    }
}
