using Vitruvian.Services;
namespace AntsUI
{
    partial class ConsoleWindow
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
            ServiceRegistry.ServiceAdded -= ServiceAdded;
            ServiceRegistry.ServiceRemoved -= ServiceRemoved;

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
            this.consoleWindow1 = new ConsoleWindow();
            this.objectBrowser1 = new ObjectBrowser();
            this.alignLinkDesigner1 = new Vitruvian.AlignLinks.Designers.AlignLinkDesigner(this.components);
            this.alignlink0 = new Vitruvian.AlignLinks.PinResizeAlignLink(this.components);
            this.alignlink1 = new Vitruvian.AlignLinks.PinResizeAlignLink(this.components);
            this.alignlink2 = new Vitruvian.AlignLinks.PinResizeAlignLink(this.components);
            this.SuspendLayout();
            // 
            // consoleWindow1
            // 
            this.consoleWindow1.Location = new System.Drawing.Point(191, 0);
            this.consoleWindow1.MinimumSize = new System.Drawing.Size(75, 25);
            this.consoleWindow1.Name = "consoleWindow1";
            this.consoleWindow1.Size = new System.Drawing.Size(480, 405);
            this.consoleWindow1.TabIndex = 0;
            // 
            // objectBrowser1
            // 
            this.objectBrowser1.Location = new System.Drawing.Point(-2, 0);
            this.objectBrowser1.Margin = new System.Windows.Forms.Padding(4);
            this.objectBrowser1.Name = "objectBrowser1";
            this.objectBrowser1.Size = new System.Drawing.Size(186, 405);
            this.objectBrowser1.TabIndex = 1;
            // 
            // alignLinkDesigner1
            // 
            this.alignLinkDesigner1.HiddenControls = "";
            this.alignLinkDesigner1.ShowCenterGlyphs = true;
            this.alignLinkDesigner1.ShowPercentPositionGlyphs = true;
            this.alignLinkDesigner1.ShowPercentSizeGlyphs = true;
            this.alignLinkDesigner1.ShowPinPositionGlyphs = true;
            this.alignLinkDesigner1.ShowPinResizeGlyphs = true;
            this.alignLinkDesigner1.ShowSizeGlyphs = true;
            this.alignLinkDesigner1.UnlockedControls = "";
            // 
            // alignlink0
            // 
            this.alignlink0.BottomOffset = -40;
            this.alignlink0.Child = this.objectBrowser1;
            this.alignlink0.ChildSide = System.Windows.Forms.AnchorStyles.Bottom;
            this.alignlink0.IsLocked = true;
            this.alignlink0.LeftOffset = 0;
            this.alignlink0.Parent = this;
            this.alignlink0.ParentSide = System.Windows.Forms.AnchorStyles.Bottom;
            this.alignlink0.RightOffset = 0;
            this.alignlink0.TopOffset = 0;
            // 
            // alignlink1
            // 
            this.alignlink1.BottomOffset = -40;
            this.alignlink1.Child = this.consoleWindow1;
            this.alignlink1.ChildSide = System.Windows.Forms.AnchorStyles.Bottom;
            this.alignlink1.IsLocked = true;
            this.alignlink1.LeftOffset = 0;
            this.alignlink1.Parent = this;
            this.alignlink1.ParentSide = System.Windows.Forms.AnchorStyles.Bottom;
            this.alignlink1.RightOffset = 0;
            this.alignlink1.TopOffset = 0;
            // 
            // alignlink2
            // 
            this.alignlink2.BottomOffset = 0;
            this.alignlink2.Child = this.consoleWindow1;
            this.alignlink2.ChildSide = System.Windows.Forms.AnchorStyles.Right;
            this.alignlink2.IsLocked = true;
            this.alignlink2.LeftOffset = 0;
            this.alignlink2.Parent = this;
            this.alignlink2.ParentSide = System.Windows.Forms.AnchorStyles.Right;
            this.alignlink2.RightOffset = -8;
            this.alignlink2.TopOffset = 0;
            // 
            // ConsoleWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(671, 405);
            this.Controls.Add(this.objectBrowser1);
            this.Controls.Add(this.consoleWindow1);
            this.Name = "ConsoleWindow";
            this.Text = "ConsoleWindow";
            this.ResumeLayout(false);

        }

        #endregion

        private ConsoleWindow consoleWindow1;
        private ObjectBrowser objectBrowser1;
        private Vitruvian.AlignLinks.Designers.AlignLinkDesigner alignLinkDesigner1;
        private Vitruvian.AlignLinks.PinResizeAlignLink alignlink0;
        private Vitruvian.AlignLinks.PinResizeAlignLink alignlink1;
        private Vitruvian.AlignLinks.PinResizeAlignLink alignlink2;
    }
}