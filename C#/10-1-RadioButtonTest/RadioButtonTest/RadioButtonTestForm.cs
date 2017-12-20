// Fig. 14.28: RadioButtonTestForm.cs
// Using RadioButtons to set message window options.
using System;
using System.Windows.Forms;

namespace RadioButtonTest
{
    // Form contains several RadioButtons--user chooses one
    // from each group to create a custom MessageBox
    public partial class RadioButtonTestForm : Form
    {
        // create variables that store the user's choice of options
        private MessageBoxIcon iconType;
        private MessageBoxButtons buttonType;

        // default constructor
        public RadioButtonTestForm()
        {
            InitializeComponent();
        }

        // change Buttons based on option chosen by sender
        private void buttonType_CheckedChanged(
           object sender, EventArgs e)
        {
            if (sender == okRadioButton) // display OK Button
                buttonType = MessageBoxButtons.OK;

            // display OK and Cancel Buttons
            else if (sender == okCancelRadioButton)
                buttonType = MessageBoxButtons.OKCancel;

            // display Abort, Retry and Ignore Buttons
            else
                buttonType = MessageBoxButtons.AbortRetryIgnore;
        }

        // change Icon based on option chosen by sender
        private void iconType_CheckedChanged(object sender, EventArgs e)
        {
            // display error Icon
            if (sender == errorRadioButton)
                iconType = MessageBoxIcon.Error;

            // display exclamation point Icon
            else if (sender == exclamationRadioButton)
                iconType = MessageBoxIcon.Exclamation;

            // display question mark Icon
            else
                iconType = MessageBoxIcon.Question;
        }

        // display MessageBox and Button user pressed
        private void displayButton_Click(object sender, EventArgs e)
        {
            // display MessageBox and store
            // the value of the Button that was pressed
            DialogResult result = MessageBox.Show(
               "This is your Custom MessageBox.", "Custom MessageBox",
               buttonType, iconType);

            // check to see which Button was pressed in the MessageBox
            // change text displayed accordingly
            switch (result)
            {
                case DialogResult.OK:
                    displayLabel.Text = "OK was pressed.";
                    break;
                case DialogResult.Cancel:
                    displayLabel.Text = "Cancel was pressed.";
                    break;
                case DialogResult.Abort:
                    displayLabel.Text = "Abort was pressed.";
                    break;
                case DialogResult.Retry:
                    displayLabel.Text = "Retry was pressed.";
                    break;
                case DialogResult.Ignore:
                    displayLabel.Text = "Ignore was pressed.";
                    break;
            }
        }
    }
}