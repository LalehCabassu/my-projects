using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WF_EventHandling
{
    public partial class GreetingForm : Form
    {
        public GreetingForm()
        {
            InitializeComponent();
        }

        private void clickButton_Click(object sender, EventArgs e)
        {
            greetingLabel.Hide();
            if (validate())
            {
                greetingLabel.Text = "Welcome, " + name.Text + "!";
                greetingLabel.Show();
            }
            else
                MessageBox.Show("Please enter your name.");
        }

        private bool validate()
        {
            bool result = true;

            name.Text = name.Text.Trim();
            if (string.IsNullOrEmpty(name.Text))
                result = false;

            return result;
        }
    }
}
