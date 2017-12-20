// Fig. 14.36: interestCalculatorForm.cs
// Demonstrating the NumericUpDown control.
using System;
using System.Windows.Forms;

namespace NumericUpDownTest
{
   public partial class InterestCalculatorForm : Form
   {
      // default constructor
      public InterestCalculatorForm()
      {
         InitializeComponent();
      }

      private void calculateButton_Click(
         object sender, EventArgs e )
      {
         int year = Convert.ToInt32( yearUpDown.Value );
         displayTextBox.Text = yearlyAccountBalance(year); // display result
      }

      private string yearlyAccountBalance(int year)
      {
          // declare variables to store user input
          decimal principal;
          double rate;
          decimal amount;
          string output;

          // retrieve user input
          principal = Convert.ToDecimal(principalTextBox.Text);
          rate = Convert.ToDouble(interestTextBox.Text);

          // set output header
          output = "Year\tAmount on Deposit\r\n";

          // calculate amount after each year and append to output
          for (int yearCounter = 1; yearCounter <= year; ++yearCounter)
          {
              amount = principal *
                 ((decimal)Math.Pow((1 + rate / 100), yearCounter));
              output += (yearCounter + "\t" +
                 String.Format("{0:C}", amount) + "\r\n");
          }
          return output;
      }
   }
}