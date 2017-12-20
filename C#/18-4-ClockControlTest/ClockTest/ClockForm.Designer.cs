namespace ClockTest
{
    partial class ClockForm
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
            this.clockUserControl1 = new ClockControl.ClockUserControl();
            this.SuspendLayout();
            // 
            // clockUserControl1
            // 
            this.clockUserControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clockUserControl1.Location = new System.Drawing.Point(24, 5);
            this.clockUserControl1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.clockUserControl1.Name = "clockUserControl1";
            this.clockUserControl1.Size = new System.Drawing.Size(154, 67);
            this.clockUserControl1.TabIndex = 0;
            // 
            // ClockForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(223, 103);
            this.Controls.Add(this.clockUserControl1);
            this.Name = "ClockForm";
            this.Text = "Clock";
            this.ResumeLayout(false);

        }

        #endregion

        private ClockControl.ClockUserControl clockUserControl1;
    }
}

