using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using Vitruvian.Distribution;
using Vitruvian.Logging;
using Vitruvian.Windows;
using Vitruvian.Services;
using Vitruvian.Serialization;
using Ants;

namespace UserInterfaceComponents
{
    [OptimisticSerialization]
    public partial class GroundForm : Form
    {
        private static Logger _logger = Logger.GetLogger(typeof(GroundForm));
        private System.Timers.Timer timer;

        private Dictionary<Colony, int> colorIndices = new Dictionary<Colony, int>();
        private ImageList smallImageList = new ImageList();
        private ImageList largeImageList = new ImageList();

        private Ground ground;
        private SimulationSettings settings;

        private Panel errorPanel = null;
        private Label errorMessage = null;

        private bool paintingInProgress = false;
        
        public GroundForm()
        {
            InitializeComponent();
            this.DoubleBuffered = true;

            SetupErrorPanel();

            errorPanel.Visible = true;
            normalPanel.Visible = false;
        }

        #region Event Handlers

        private void GroundForm_Load(object sender, EventArgs e)
        {
            _logger.Debug("Entering GroundForm_Load");

            normalPanel.Visible = false;
            errorPanel.Visible = true;

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsed);
            timer.Enabled = true;
            groundView.PaintingFinished += new MethodInvoker(AllowPaintingAgain);

            _logger.Debug("Exiting GroundForm_Load");

        }

        void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            _logger.DebugFormat("Entering TimerElapsed");

            timer.Enabled = false;

            DisplayStuff();

            _logger.DebugFormat("Restarting timer, with interval set to {0}", timer.Interval);
            timer.Enabled = true;
            _logger.DebugFormat("Exiting TimerElapsed");
        }

        void AllowPaintingAgain()
        {
            if (paintingInProgress)
                paintingInProgress = false;
        }

        private void DisplayStuff()
        {
            if (InvokeRequired)
                BeginInvoke(new MethodInvoker(DisplayStuff));
            else
                DisplayFormContent();
        }

        private void DisplayFormContent()
        {
            _logger.Debug("Entering DisplayFormContent");

            if (paintingInProgress == false)
            {
                _logger.Debug("No other painting in progress, so proceed");
                try
                {
                    paintingInProgress = true;

                    ground = ServiceRegistry.GetPreferredService<Ground>();
                    settings = ServiceRegistry.GetPreferredService<SimulationSettings>();

                    if (ground == null)
                        _logger.Debug("No grounds service yet");
                    if (settings == null)
                        _logger.Debug("No settings service yet");

                    timer.Interval = (settings != null) ? settings.DisplayRefreshInterval : 1000;

                    if (ground == null)
                    {
                        errorMessage.Text = "No Ground Service is Available. Please Wait...";
                        errorPanel.Visible = true;
                        normalPanel.Visible = false;
                    }
                    else if (settings == null)
                    {
                        errorMessage.Text = "No Simulation Settings Service is Available.  Please Wait...";
                        errorPanel.Visible = true;
                        normalPanel.Visible = false;
                    }
                    else
                    {
                        _logger.Debug("Hide error panel and show normal panel");
                        errorPanel.Visible = false;
                        normalPanel.Visible = true;

                        RefreshColonyList();
                        groundView.Invalidate();
                        _logger.Debug("groundView marked for Repaint");

                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
                _logger.Error("Painting is too slow for the Display Refresh Interval.  Improve performance or increase Display Refresh Interval");

            _logger.Debug("Exiting DisplayFormContent");

        }

        private void GroundForm_Resize(object sender, EventArgs e)
        {
            normalPanel.Size = ClientSize;
            errorPanel.Size = ClientSize;

            int margin = groundView.Location.X;
            groundView.Width = normalPanel.Width - margin * 2 - 6;
            groundView.Height = normalPanel.Height - groundView.Location.Y - margin - 34;
        }

        private void GroundForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (timer != null)
                timer = null;
        }
        #endregion

        #region Private Methods

        private void SetupErrorPanel()
        {
            errorMessage = new Label();
            errorMessage.AutoSize = false;
            errorMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            errorMessage.Location = new Point(0, 0);
            errorMessage.Name = "errorLabel";
            errorMessage.Size = this.ClientSize;
            errorMessage.TabIndex = 0;
            errorMessage.Text = "Loading...";
            errorMessage.TextAlign = ContentAlignment.MiddleCenter;

            errorPanel = new Panel();
            errorPanel.Location = new Point(0, 0);
            errorPanel.Size = ClientSize;
            errorPanel.Controls.Add(errorMessage);
            errorPanel.Visible = false;

            this.Controls.Add(errorPanel);
        }

        private void RefreshColonyList()
        {
            _logger.Debug("Entering RefreshColonyList");

            try
            {
                List<Colony> colonies = ServiceRegistry.GetServices<Colony>();

                foreach (Colony colony in colonies)
                {
                    if (colorIndices.ContainsKey(colony))
                        continue;

                    int index = AddToImageLists(colony.ColonyColor);
                    colorIndices.Add(colony, index);
                }

                numberOfColonies.Text = colonies.Count.ToString();
                colonyList.Items.Clear();
                colonyList.SmallImageList = smallImageList;
                colonyList.LargeImageList = largeImageList;

                foreach (Colony colony in colonies)
                {
                    try
                    {
                        string homeLocation = colony.Home.Location.ToString();
                        string stockPile = colony.Home.StockPile.Amount.ToString();
                        string antCount = colony.Ants.Count.ToString();
                        int colorIndex = colorIndices[colony];
                        ListViewItem item = new ListViewItem(new string[] { colony.ColonyColor.ToString(), homeLocation, stockPile, antCount }, colorIndex);
                        colonyList.Items.Add(item);
                    }
                    catch (Exception ex)
                    {
                        if (_logger.IsDebugEnabled)
                            _logger.Debug(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                if (_logger.IsDebugEnabled)
                    _logger.Debug(ex);
            }

            _logger.Debug("Existing RefreshColonyList");
        }

        private int AddToImageLists(Color color)
        {
            Bitmap smallImage = GenerateSmallBitMap(color);
            Bitmap largeImage = new Bitmap(smallImage, 24, 24);

            int colorIndex = smallImageList.Images.Count;
            smallImageList.Images.Add(smallImage);
            largeImageList.Images.Add(largeImage);
            return colorIndex;
        }

        private static Bitmap GenerateSmallBitMap(Color color)
        {
            Bitmap smallImage = new Bitmap(12, 12);
            for (int i = 1; i < 11; i++)
                for (int j = 1; j < 11; j++)
                    smallImage.SetPixel(i, j, color);
            return smallImage;
        }

        #endregion



    }
}
