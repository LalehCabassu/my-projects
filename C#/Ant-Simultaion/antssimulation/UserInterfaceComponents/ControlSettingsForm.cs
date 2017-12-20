using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

using Vitruvian.Serialization;
using Vitruvian.Services;
using Ants;

namespace UserInterfaceComponents
{
    [OptimisticSerialization]
    public partial class ControlSettingsForm : Form
    {
        private System.Timers.Timer timer = null;
        private SimulationSettings settings = null;
        private Panel errorPanel = null;
        private Label errorMessage = null;

        public ControlSettingsForm()
        {
            InitializeComponent();

            SetupErrorPanel();
        }


        #region Event Handlers

        private void ControlSettingsForm_Load(object sender, EventArgs e)
        {
            controlsPanel.Visible = false;
            errorPanel.Visible = true;

            timer = new System.Timers.Timer(1000);
            timer.Elapsed += new System.Timers.ElapsedEventHandler(TimerElapsed);
            timer.Enabled = true;

        }

        void TimerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer.Enabled = false;

            settings = ServiceRegistry.GetPreferredService<SimulationSettings>();
            timer.Interval = (settings != null) ? settings.DisplayRefreshInterval * 100 : 1000;

            DisplaySettings();

            timer.Enabled = true;
        }


        private void saveButton_Click(object sender, EventArgs e)
        {
            if (settings != null)
            {
                try
                {
                    int intSetting;
                    float floatSetting;


                    if (settings.MovementInterval != speed.Value)
                    {
                        settings.MovementInterval = speed.Value;
                        settings.DisplayRefreshInterval = speed.Value;
                    }

                    intSetting = Convert.ToUInt16(antPayload.Value);
                    if (settings.AntPayload != intSetting)
                        settings.AntPayload = Convert.ToUInt16(antPayload.Value);

                    intSetting = Convert.ToUInt16(antSteadiness.Value);
                    if (settings.AntDirectionSteadiness != intSetting)
                        settings.AntDirectionSteadiness = intSetting;

                    intSetting = Convert.ToUInt16(antBaisTowardsHome.Value);
                    if (settings.AntBiasTowardsHome != intSetting)
                        settings.AntBiasTowardsHome = intSetting;

                    intSetting = Convert.ToUInt16(initPheromoneStrength.Value);
                    if (settings.InitPheromone != intSetting)
                        settings.InitPheromone = intSetting;

                    intSetting = Convert.ToUInt16(pheromoneDecay.Value);
                    if (settings.PheromoneDecayAmount != intSetting)
                        settings.PheromoneDecayAmount = intSetting;

                    intSetting = Convert.ToUInt16(maxPheromoneLevel.Value);
                    if (settings.MaxPheromoneLevel != intSetting)
                        settings.MaxPheromoneLevel = intSetting;

                    floatSetting = ConvertFloat(borderWidth, settings.GroundBorder);
                    if (settings.GroundBorder != floatSetting)
                        settings.GroundBorder = floatSetting;

                    floatSetting = ConvertFloat(minAntSize, settings.MinSymbolSize);
                    if (settings.MinSymbolSize != floatSetting)
                        settings.MinSymbolSize = floatSetting;

                    floatSetting = ConvertFloat(pheromoneSizePercent, settings.PheromoneRelativeMarkerSize);
                    if (settings.PheromoneRelativeMarkerSize != floatSetting)
                        settings.PheromoneRelativeMarkerSize = floatSetting;

                    floatSetting = ConvertFloat(minPheromoneSize, settings.MinPheromoneMarkerSize);
                    if (settings.MinPheromoneMarkerSize != floatSetting)
                        settings.MinPheromoneMarkerSize = floatSetting;

                    MessageBox.Show("Done");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

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


        private void DisplaySettings()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new MethodInvoker(DisplaySettings));
            }
            else
            {
                if (settings != null)
                {
                    errorPanel.Visible = false;
                    controlsPanel.Visible = true;

                    speed.Value = settings.MovementInterval;
                    antPayload.Value = settings.AntPayload;
                    antSteadiness.Value = settings.AntDirectionSteadiness;
                    antBaisTowardsHome.Value = settings.AntBiasTowardsHome;
                    initPheromoneStrength.Value = settings.InitPheromone;
                    pheromoneDecay.Value = settings.PheromoneDecayAmount;
                    maxPheromoneLevel.Value = settings.MaxPheromoneLevel;
                    borderWidth.Text = settings.GroundBorder.ToString();
                    minAntSize.Text = settings.MinSymbolSize.ToString();
                    pheromoneSizePercent.Text = settings.PheromoneRelativeMarkerSize.ToString();
                    minPheromoneSize.Text = settings.MinPheromoneMarkerSize.ToString();

                    this.Invalidate();
                }
                else
                {
                    controlsPanel.Visible = false;

                    errorPanel.Visible = true;
                    errorMessage.Text = "No Simulation Settings Service is Available.  Please Wait.";
                }
            }
        }

        private float ConvertFloat(TextBox textBox, float defaultValue)
        {
            float result = defaultValue;
            try
            {
                result = Convert.ToSingle(textBox.Text.Trim());
                errorProvider.SetError(textBox, "");
            }
            catch
            {
                errorProvider.SetError(textBox, "Invalid number");
            }
            return result;
        }

        #endregion

    }
}