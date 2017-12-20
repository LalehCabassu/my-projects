using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace ReadTextFiles
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            pathTextBox.Text = Path.Combine(Directory.GetCurrentDirectory(), "test.txt");
        }

        private void displayButton_Click(object sender, EventArgs e)
        {
            string path = pathTextBox.Text;

            try
            {
                // Get an stream to read from the file
                using (StreamReader fileReader = new StreamReader(path))
                {
                    contentTextBox.Text = fileReader.ReadToEnd();
                }
            }
            catch (IOException exp)
            {
                MessageBox.Show("Unable to open the file.\nMessage: " + exp.Message, "Error", MessageBoxButtons.OK,  MessageBoxIcon.Error);
            }
            catch (ArgumentException exp)
            {
                MessageBox.Show("Path shouldn't be empty.\nMessage: " + exp.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
