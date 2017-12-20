namespace UserInterfaceComponents
{
    partial class ControlSettingsForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.speedLabel = new System.Windows.Forms.Label();
            this.speed = new System.Windows.Forms.TrackBar();
            this.fastLabel1 = new System.Windows.Forms.Label();
            this.slowLabel1 = new System.Windows.Forms.Label();
            this.antPayloadLabel = new System.Windows.Forms.Label();
            this.initPheromoneLabel = new System.Windows.Forms.Label();
            this.pheomoneDecayLabel = new System.Windows.Forms.Label();
            this.maxPheromoneLevelLabel = new System.Windows.Forms.Label();
            this.borderWidthLabel = new System.Windows.Forms.Label();
            this.behaviorPanel = new System.Windows.Forms.Panel();
            this.antBaisTowardsHome = new System.Windows.Forms.NumericUpDown();
            this.homeBaisLabel = new System.Windows.Forms.Label();
            this.antSteadiness = new System.Windows.Forms.NumericUpDown();
            this.antSteadinessLabel = new System.Windows.Forms.Label();
            this.maxPheromoneLevel = new System.Windows.Forms.NumericUpDown();
            this.pheromoneDecay = new System.Windows.Forms.NumericUpDown();
            this.initPheromoneStrength = new System.Windows.Forms.NumericUpDown();
            this.antPayload = new System.Windows.Forms.NumericUpDown();
            this.appearancePanel = new System.Windows.Forms.Panel();
            this.minPheromoneSize = new System.Windows.Forms.TextBox();
            this.pheromoneSizePercent = new System.Windows.Forms.TextBox();
            this.minAntSize = new System.Windows.Forms.TextBox();
            this.borderWidth = new System.Windows.Forms.TextBox();
            this.minPheromoneSizeLabel = new System.Windows.Forms.Label();
            this.pheromoneSizeLabel = new System.Windows.Forms.Label();
            this.minAntSizeLabel = new System.Windows.Forms.Label();
            this.errorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.controlsPanel = new System.Windows.Forms.Panel();
            this.saveButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.speed)).BeginInit();
            this.behaviorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.antBaisTowardsHome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.antSteadiness)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxPheromoneLevel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pheromoneDecay)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.initPheromoneStrength)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.antPayload)).BeginInit();
            this.appearancePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).BeginInit();
            this.controlsPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // speedLabel
            // 
            this.speedLabel.AutoSize = true;
            this.speedLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.speedLabel.Location = new System.Drawing.Point(21, 23);
            this.speedLabel.Name = "speedLabel";
            this.speedLabel.Size = new System.Drawing.Size(53, 17);
            this.speedLabel.TabIndex = 0;
            this.speedLabel.Text = "Speed:";
            // 
            // speed
            // 
            this.speed.LargeChange = 100;
            this.speed.Location = new System.Drawing.Point(80, 17);
            this.speed.Maximum = 2000;
            this.speed.Minimum = 50;
            this.speed.Name = "speed";
            this.speed.Size = new System.Drawing.Size(485, 45);
            this.speed.TabIndex = 0;
            this.speed.TickFrequency = 50;
            this.speed.Value = 50;
            // 
            // fastLabel1
            // 
            this.fastLabel1.AutoSize = true;
            this.fastLabel1.Location = new System.Drawing.Point(88, 50);
            this.fastLabel1.Name = "fastLabel1";
            this.fastLabel1.Size = new System.Drawing.Size(24, 13);
            this.fastLabel1.TabIndex = 4;
            this.fastLabel1.Text = "fast";
            // 
            // slowLabel1
            // 
            this.slowLabel1.AutoSize = true;
            this.slowLabel1.Location = new System.Drawing.Point(537, 50);
            this.slowLabel1.Name = "slowLabel1";
            this.slowLabel1.Size = new System.Drawing.Size(28, 13);
            this.slowLabel1.TabIndex = 6;
            this.slowLabel1.Text = "slow";
            // 
            // antPayloadLabel
            // 
            this.antPayloadLabel.AutoSize = true;
            this.antPayloadLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.antPayloadLabel.Location = new System.Drawing.Point(105, 24);
            this.antPayloadLabel.Name = "antPayloadLabel";
            this.antPayloadLabel.Size = new System.Drawing.Size(88, 17);
            this.antPayloadLabel.TabIndex = 7;
            this.antPayloadLabel.Text = "Ant Payload:";
            // 
            // initPheromoneLabel
            // 
            this.initPheromoneLabel.AutoSize = true;
            this.initPheromoneLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.initPheromoneLabel.Location = new System.Drawing.Point(15, 117);
            this.initPheromoneLabel.Name = "initPheromoneLabel";
            this.initPheromoneLabel.Size = new System.Drawing.Size(179, 17);
            this.initPheromoneLabel.TabIndex = 8;
            this.initPheromoneLabel.Text = "Initial Pheromone Strength:";
            // 
            // pheomoneDecayLabel
            // 
            this.pheomoneDecayLabel.AutoSize = true;
            this.pheomoneDecayLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pheomoneDecayLabel.Location = new System.Drawing.Point(64, 149);
            this.pheomoneDecayLabel.Name = "pheomoneDecayLabel";
            this.pheomoneDecayLabel.Size = new System.Drawing.Size(129, 17);
            this.pheomoneDecayLabel.TabIndex = 9;
            this.pheomoneDecayLabel.Text = "Pheromone Decay:";
            // 
            // maxPheromoneLevelLabel
            // 
            this.maxPheromoneLevelLabel.AutoSize = true;
            this.maxPheromoneLevelLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.maxPheromoneLevelLabel.Location = new System.Drawing.Point(41, 181);
            this.maxPheromoneLevelLabel.Name = "maxPheromoneLevelLabel";
            this.maxPheromoneLevelLabel.Size = new System.Drawing.Size(152, 17);
            this.maxPheromoneLevelLabel.TabIndex = 10;
            this.maxPheromoneLevelLabel.Text = "Max Pheromone Level:";
            // 
            // borderWidthLabel
            // 
            this.borderWidthLabel.AutoSize = true;
            this.borderWidthLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.borderWidthLabel.Location = new System.Drawing.Point(66, 27);
            this.borderWidthLabel.Name = "borderWidthLabel";
            this.borderWidthLabel.Size = new System.Drawing.Size(95, 17);
            this.borderWidthLabel.TabIndex = 11;
            this.borderWidthLabel.Text = "Border Width:";
            // 
            // behaviorPanel
            // 
            this.behaviorPanel.Controls.Add(this.antBaisTowardsHome);
            this.behaviorPanel.Controls.Add(this.homeBaisLabel);
            this.behaviorPanel.Controls.Add(this.antSteadiness);
            this.behaviorPanel.Controls.Add(this.antSteadinessLabel);
            this.behaviorPanel.Controls.Add(this.maxPheromoneLevel);
            this.behaviorPanel.Controls.Add(this.pheromoneDecay);
            this.behaviorPanel.Controls.Add(this.initPheromoneStrength);
            this.behaviorPanel.Controls.Add(this.antPayload);
            this.behaviorPanel.Controls.Add(this.antPayloadLabel);
            this.behaviorPanel.Controls.Add(this.initPheromoneLabel);
            this.behaviorPanel.Controls.Add(this.maxPheromoneLevelLabel);
            this.behaviorPanel.Controls.Add(this.pheomoneDecayLabel);
            this.behaviorPanel.Location = new System.Drawing.Point(24, 90);
            this.behaviorPanel.Name = "behaviorPanel";
            this.behaviorPanel.Size = new System.Drawing.Size(277, 236);
            this.behaviorPanel.TabIndex = 1;
            // 
            // antBaisTowardsHome
            // 
            this.antBaisTowardsHome.Location = new System.Drawing.Point(199, 88);
            this.antBaisTowardsHome.Name = "antBaisTowardsHome";
            this.antBaisTowardsHome.Size = new System.Drawing.Size(59, 20);
            this.antBaisTowardsHome.TabIndex = 2;
            // 
            // homeBaisLabel
            // 
            this.homeBaisLabel.AutoSize = true;
            this.homeBaisLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.homeBaisLabel.Location = new System.Drawing.Point(30, 88);
            this.homeBaisLabel.Name = "homeBaisLabel";
            this.homeBaisLabel.Size = new System.Drawing.Size(163, 17);
            this.homeBaisLabel.TabIndex = 17;
            this.homeBaisLabel.Text = "Ant Bais Towards Home:";
            // 
            // antSteadiness
            // 
            this.antSteadiness.Location = new System.Drawing.Point(199, 56);
            this.antSteadiness.Name = "antSteadiness";
            this.antSteadiness.Size = new System.Drawing.Size(59, 20);
            this.antSteadiness.TabIndex = 1;
            // 
            // antSteadinessLabel
            // 
            this.antSteadinessLabel.AutoSize = true;
            this.antSteadinessLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.antSteadinessLabel.Location = new System.Drawing.Point(86, 56);
            this.antSteadinessLabel.Name = "antSteadinessLabel";
            this.antSteadinessLabel.Size = new System.Drawing.Size(107, 17);
            this.antSteadinessLabel.TabIndex = 15;
            this.antSteadinessLabel.Text = "Ant Steadiness:";
            // 
            // maxPheromoneLevel
            // 
            this.maxPheromoneLevel.Location = new System.Drawing.Point(199, 181);
            this.maxPheromoneLevel.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.maxPheromoneLevel.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.maxPheromoneLevel.Name = "maxPheromoneLevel";
            this.maxPheromoneLevel.Size = new System.Drawing.Size(59, 20);
            this.maxPheromoneLevel.TabIndex = 5;
            this.maxPheromoneLevel.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // pheromoneDecay
            // 
            this.pheromoneDecay.Location = new System.Drawing.Point(199, 149);
            this.pheromoneDecay.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.pheromoneDecay.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.pheromoneDecay.Name = "pheromoneDecay";
            this.pheromoneDecay.Size = new System.Drawing.Size(59, 20);
            this.pheromoneDecay.TabIndex = 4;
            this.pheromoneDecay.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // initPheromoneStrength
            // 
            this.initPheromoneStrength.Location = new System.Drawing.Point(199, 117);
            this.initPheromoneStrength.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.initPheromoneStrength.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.initPheromoneStrength.Name = "initPheromoneStrength";
            this.initPheromoneStrength.Size = new System.Drawing.Size(59, 20);
            this.initPheromoneStrength.TabIndex = 3;
            this.initPheromoneStrength.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // antPayload
            // 
            this.antPayload.Location = new System.Drawing.Point(199, 24);
            this.antPayload.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.antPayload.Name = "antPayload";
            this.antPayload.Size = new System.Drawing.Size(59, 20);
            this.antPayload.TabIndex = 0;
            this.antPayload.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // appearancePanel
            // 
            this.appearancePanel.Controls.Add(this.minPheromoneSize);
            this.appearancePanel.Controls.Add(this.pheromoneSizePercent);
            this.appearancePanel.Controls.Add(this.minAntSize);
            this.appearancePanel.Controls.Add(this.borderWidth);
            this.appearancePanel.Controls.Add(this.minPheromoneSizeLabel);
            this.appearancePanel.Controls.Add(this.pheromoneSizeLabel);
            this.appearancePanel.Controls.Add(this.minAntSizeLabel);
            this.appearancePanel.Controls.Add(this.borderWidthLabel);
            this.appearancePanel.Location = new System.Drawing.Point(318, 90);
            this.appearancePanel.Name = "appearancePanel";
            this.appearancePanel.Size = new System.Drawing.Size(247, 236);
            this.appearancePanel.TabIndex = 3;
            // 
            // minPheromoneSize
            // 
            this.minPheromoneSize.Location = new System.Drawing.Point(167, 119);
            this.minPheromoneSize.Name = "minPheromoneSize";
            this.minPheromoneSize.Size = new System.Drawing.Size(59, 20);
            this.minPheromoneSize.TabIndex = 24;
            // 
            // pheromoneSizePercent
            // 
            this.pheromoneSizePercent.Location = new System.Drawing.Point(167, 88);
            this.pheromoneSizePercent.Name = "pheromoneSizePercent";
            this.pheromoneSizePercent.Size = new System.Drawing.Size(59, 20);
            this.pheromoneSizePercent.TabIndex = 23;
            // 
            // minAntSize
            // 
            this.minAntSize.Location = new System.Drawing.Point(167, 58);
            this.minAntSize.Name = "minAntSize";
            this.minAntSize.Size = new System.Drawing.Size(59, 20);
            this.minAntSize.TabIndex = 22;
            // 
            // borderWidth
            // 
            this.borderWidth.Location = new System.Drawing.Point(167, 27);
            this.borderWidth.Name = "borderWidth";
            this.borderWidth.Size = new System.Drawing.Size(59, 20);
            this.borderWidth.TabIndex = 21;
            // 
            // minPheromoneSizeLabel
            // 
            this.minPheromoneSizeLabel.AutoSize = true;
            this.minPheromoneSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minPheromoneSizeLabel.Location = new System.Drawing.Point(19, 120);
            this.minPheromoneSizeLabel.Name = "minPheromoneSizeLabel";
            this.minPheromoneSizeLabel.Size = new System.Drawing.Size(142, 17);
            this.minPheromoneSizeLabel.TabIndex = 20;
            this.minPheromoneSizeLabel.Text = "Min Pheromone Size:";
            // 
            // pheromoneSizeLabel
            // 
            this.pheromoneSizeLabel.AutoSize = true;
            this.pheromoneSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.pheromoneSizeLabel.Location = new System.Drawing.Point(29, 88);
            this.pheromoneSizeLabel.Name = "pheromoneSizeLabel";
            this.pheromoneSizeLabel.Size = new System.Drawing.Size(132, 17);
            this.pheromoneSizeLabel.TabIndex = 18;
            this.pheromoneSizeLabel.Text = "Pheromone Size %:";
            // 
            // minAntSizeLabel
            // 
            this.minAntSizeLabel.AutoSize = true;
            this.minAntSizeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minAntSizeLabel.Location = new System.Drawing.Point(71, 59);
            this.minAntSizeLabel.Name = "minAntSizeLabel";
            this.minAntSizeLabel.Size = new System.Drawing.Size(90, 17);
            this.minAntSizeLabel.TabIndex = 16;
            this.minAntSizeLabel.Text = "Min Ant Size:";
            // 
            // errorProvider
            // 
            this.errorProvider.ContainerControl = this;
            // 
            // controlsPanel
            // 
            this.controlsPanel.Controls.Add(this.speed);
            this.controlsPanel.Controls.Add(this.appearancePanel);
            this.controlsPanel.Controls.Add(this.speedLabel);
            this.controlsPanel.Controls.Add(this.behaviorPanel);
            this.controlsPanel.Controls.Add(this.fastLabel1);
            this.controlsPanel.Controls.Add(this.slowLabel1);
            this.controlsPanel.Controls.Add(this.saveButton);
            this.controlsPanel.Location = new System.Drawing.Point(0, 0);
            this.controlsPanel.Name = "controlsPanel";
            this.controlsPanel.Size = new System.Drawing.Size(589, 385);
            this.controlsPanel.TabIndex = 7;
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(490, 341);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(75, 23);
            this.saveButton.TabIndex = 8;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // ControlSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.ClientSize = new System.Drawing.Size(590, 386);
            this.Controls.Add(this.controlsPanel);
            this.Name = "ControlSettingsForm";
            this.Text = "Control Settings";
            this.Load += new System.EventHandler(this.ControlSettingsForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.speed)).EndInit();
            this.behaviorPanel.ResumeLayout(false);
            this.behaviorPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.antBaisTowardsHome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.antSteadiness)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.maxPheromoneLevel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pheromoneDecay)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.initPheromoneStrength)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.antPayload)).EndInit();
            this.appearancePanel.ResumeLayout(false);
            this.appearancePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.errorProvider)).EndInit();
            this.controlsPanel.ResumeLayout(false);
            this.controlsPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label speedLabel;
        private System.Windows.Forms.TrackBar speed;
        private System.Windows.Forms.Label fastLabel1;
        private System.Windows.Forms.Label slowLabel1;
        private System.Windows.Forms.Label antPayloadLabel;
        private System.Windows.Forms.Label initPheromoneLabel;
        private System.Windows.Forms.Label pheomoneDecayLabel;
        private System.Windows.Forms.Label maxPheromoneLevelLabel;
        private System.Windows.Forms.Label borderWidthLabel;
        private System.Windows.Forms.Panel behaviorPanel;
        private System.Windows.Forms.NumericUpDown maxPheromoneLevel;
        private System.Windows.Forms.NumericUpDown pheromoneDecay;
        private System.Windows.Forms.NumericUpDown initPheromoneStrength;
        private System.Windows.Forms.NumericUpDown antPayload;
        private System.Windows.Forms.Panel appearancePanel;
        private System.Windows.Forms.Label pheromoneSizeLabel;
        private System.Windows.Forms.Label minAntSizeLabel;
        private System.Windows.Forms.Label minPheromoneSizeLabel;
        private System.Windows.Forms.NumericUpDown antBaisTowardsHome;
        private System.Windows.Forms.Label homeBaisLabel;
        private System.Windows.Forms.NumericUpDown antSteadiness;
        private System.Windows.Forms.Label antSteadinessLabel;
        private System.Windows.Forms.ErrorProvider errorProvider;
        private System.Windows.Forms.TextBox minPheromoneSize;
        private System.Windows.Forms.TextBox pheromoneSizePercent;
        private System.Windows.Forms.TextBox minAntSize;
        private System.Windows.Forms.TextBox borderWidth;
        private System.Windows.Forms.Panel controlsPanel;
        private System.Windows.Forms.Button saveButton;
    }
}