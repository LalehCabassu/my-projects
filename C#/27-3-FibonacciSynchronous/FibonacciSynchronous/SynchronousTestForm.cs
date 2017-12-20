// Fig. 28.2: SynchronousTestForm.cs
// Fibonacci calculations performed sequentially
using System;
using System.Windows.Forms;

namespace FibonacciSynchronous
{
   public partial class SynchronousTestForm : Form
   {
      public SynchronousTestForm()
      {
         InitializeComponent();
      }

      // start sequential calls to Fibonacci
      private void startButton_Click( object sender, EventArgs e )
      {
         // calculate Fibonacci (40)
         outputTextBox.Text = "Calculating Fibonacci(40)\r\n";
         outputTextBox.Refresh(); // force outputTextBox to repaint
         DateTime startTime1 = DateTime.Now; // time before calculation
         long result1 = Fibonacci( 40 ); // synchronous call
         DateTime endTime1 = DateTime.Now; // time after calculation

         // display results for Fibonacci(40) 
         outputTextBox.AppendText(
            String.Format( "Fibonacci(40) = {0}\r\n", result1 ) );
         outputTextBox.AppendText( String.Format(
            "Calculation time = {0:F6} minutes\r\n\r\n",
            endTime1.Subtract( startTime1 ).TotalMilliseconds / 
            60000.0 ) );

         // calculate Fibonacci (39)
         outputTextBox.AppendText( "Calculating Fibonacci(39)\r\n" );
         outputTextBox.Refresh(); // force outputTextBox to repaint
         DateTime startTime2 = DateTime.Now; // time before calculation
         long result2 = Fibonacci( 39 ); // synchronous call
         DateTime endTime2 = DateTime.Now; // time after calculation

         // display results for Fibonacci(39) 
         outputTextBox.AppendText( 
            String.Format( "Fibonacci( 39 ) = {0}\r\n", result2 ));
         outputTextBox.AppendText( String.Format(
            "Calculation time = {0:F6} minutes\r\n\r\n",
            endTime2.Subtract( startTime2 ).TotalMilliseconds / 
            60000.0 ) );

         // show total calculation time
         outputTextBox.AppendText( String.Format(
            "Total calculation time = {0:F6} minutes\r\n",
            endTime2.Subtract( startTime1 ).TotalMilliseconds / 
            60000.0 ) );
      }

      // Recursively calculates Fibonacci numbers
      public long Fibonacci( long n )
      {
         if ( n == 0 || n == 1 )
            return n;
         else
            return Fibonacci( n - 1 ) + Fibonacci( n - 2 );
      }
   }
}