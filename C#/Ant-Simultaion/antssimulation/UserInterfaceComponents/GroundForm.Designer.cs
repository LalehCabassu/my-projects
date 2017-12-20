namespace UserInterfaceComponents
{
    partial class GroundForm
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
            this.normalPanel = new System.Windows.Forms.Panel();
            this.colonyList = new System.Windows.Forms.ListView();
            this.colorColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.homeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.stockPileColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.antCountColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.numberOfColonies = new System.Windows.Forms.Label();
            this.groundView = new UserInterfaceComponents.GroundView();
            this.label1 = new System.Windows.Forms.Label();
            this.normalPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // normalPanel
            // 
            this.normalPanel.Controls.Add(this.colonyList);
            this.normalPanel.Controls.Add(this.numberOfColonies);
            this.normalPanel.Controls.Add(this.groundView);
            this.normalPanel.Controls.Add(this.label1);
            this.normalPanel.Location = new System.Drawing.Point(0, 0);
            this.normalPanel.Name = "normalPanel";
            this.normalPanel.Size = new System.Drawing.Size(632, 735);
            this.normalPanel.TabIndex = 9;
            // 
            // colonyList
            // 
            this.colonyList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colorColumnHeader,
            this.homeColumnHeader,
            this.stockPileColumnHeader,
            this.antCountColumnHeader});
            this.colonyList.Location = new System.Drawing.Point(22, 32);
            this.colonyList.Name = "colonyList";
            this.colonyList.Size = new System.Drawing.Size(392, 115);
            this.colonyList.TabIndex = 12;
            this.colonyList.UseCompatibleStateImageBehavior = false;
            this.colonyList.View = System.Windows.Forms.View.Details;
            // 
            // colorColumnHeader
            // 
            this.colorColumnHeader.Text = "Color";
            this.colorColumnHeader.Width = 111;
            // 
            // homeColumnHeader
            // 
            this.homeColumnHeader.Text = "Hill (x,y)";
            this.homeColumnHeader.Width = 87;
            // 
            // stockPileColumnHeader
            // 
            this.stockPileColumnHeader.Text = "Food Stockpile";
            this.stockPileColumnHeader.Width = 104;
            // 
            // antCountColumnHeader
            // 
            this.antCountColumnHeader.Text = "# of Ants";
            this.antCountColumnHeader.Width = 88;
            // 
            // numberOfColonies
            // 
            this.numberOfColonies.AutoSize = true;
            this.numberOfColonies.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numberOfColonies.Location = new System.Drawing.Point(101, 16);
            this.numberOfColonies.Name = "numberOfColonies";
            this.numberOfColonies.Size = new System.Drawing.Size(14, 13);
            this.numberOfColonies.TabIndex = 11;
            this.numberOfColonies.Text = "0";
            // 
            // groundView
            // 
            this.groundView.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.groundView.Location = new System.Drawing.Point(22, 165);
            this.groundView.Margin = new System.Windows.Forms.Padding(4);
            this.groundView.Name = "groundView";
            this.groundView.Size = new System.Drawing.Size(500, 500);
            this.groundView.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 13);
            this.label1.TabIndex = 9;
            this.label1.Text = "# of Colonies: ";
            // 
            // GroundForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(632, 697);
            this.Controls.Add(this.normalPanel);
            this.Name = "GroundForm";
            this.Text = "GroundForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GroundForm_FormClosing);
            this.Load += new System.EventHandler(this.GroundForm_Load);
            this.Resize += new System.EventHandler(this.GroundForm_Resize);
            this.normalPanel.ResumeLayout(false);
            this.normalPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel normalPanel;
        private System.Windows.Forms.ListView colonyList;
        private System.Windows.Forms.ColumnHeader colorColumnHeader;
        private System.Windows.Forms.ColumnHeader homeColumnHeader;
        private System.Windows.Forms.ColumnHeader stockPileColumnHeader;
        private System.Windows.Forms.ColumnHeader antCountColumnHeader;
        private System.Windows.Forms.Label numberOfColonies;
        private GroundView groundView;
        private System.Windows.Forms.Label label1;

    }
}