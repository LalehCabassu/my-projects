// Fig. 14.30: PictureBoxTestForm.cs
// Using a PictureBox to display images.
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PictureBoxTest
{
    // Form to display different images when PictureBox is clicked
    public partial class PictureBoxTestForm : Form
    {
        private int imageNum = -1; // determines which image is displayed

        // default constructor
        public PictureBoxTestForm()
        {
            InitializeComponent();
        }

        // change image whenever Next Button is clicked
        private void nextButton_Click(object sender, EventArgs e)
        {
            imageNum = (imageNum + 1) % 8; // imageNum cycles from 0 to 7

            // create Image object from file, display in PicutreBox
            imagePictureBox.Image = (Image)
               (Properties.Resources.ResourceManager.GetObject(
               string.Format("image_{0}", imageNum)));
        }

        private void previousButton_Click(object sender, EventArgs e)
        {
            if (imageNum == 0)
                imageNum = 8;

            imageNum = (imageNum - 1) % 8; // imageNum cycles from 0 to 7
            
            // create Image object from file, display in PicutreBox
            imagePictureBox.Image = (Image)
               (Properties.Resources.ResourceManager.GetObject(
               string.Format("image_{0}", imageNum)));
        }

        private void imagePictureBox_MouseHover(object sender, EventArgs e)
        {
            imageToolTip.Show(string.Format("image_{0}", imageNum), imagePictureBox);
        }
    }
}