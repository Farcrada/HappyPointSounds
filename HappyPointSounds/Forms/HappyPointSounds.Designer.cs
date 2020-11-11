namespace HappyPointSounds.Forms
{
	partial class HappyPointSounds
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(HappyPointSounds));
			this.HappyPointSoundsToolStrip = new System.Windows.Forms.ToolStrip();
			this.ConnectButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.SoundDropDown = new System.Windows.Forms.ToolStripDropDownButton();
			this.AddSoundOptionButton = new System.Windows.Forms.ToolStripButton();
			this.ReportBox = new System.Windows.Forms.ListBox();
			this.HappyPointSoundsToolStrip.SuspendLayout();
			this.SuspendLayout();
			// 
			// HappyPointSoundsToolStrip
			// 
			this.HappyPointSoundsToolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.ConnectButton,
			this.toolStripSeparator1,
			this.SoundDropDown,
			this.AddSoundOptionButton});
			this.HappyPointSoundsToolStrip.Location = new System.Drawing.Point(0, 0);
			this.HappyPointSoundsToolStrip.Name = "HappyPointSoundsToolStrip";
			this.HappyPointSoundsToolStrip.Size = new System.Drawing.Size(800, 25);
			this.HappyPointSoundsToolStrip.TabIndex = 2;
			this.HappyPointSoundsToolStrip.Text = "Tool Strip";
			// 
			// ConnectButton
			// 
			this.ConnectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.ConnectButton.Image = ((System.Drawing.Image)(resources.GetObject("ConnectButton.Image")));
			this.ConnectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ConnectButton.Name = "ConnectButton";
			this.ConnectButton.Size = new System.Drawing.Size(98, 22);
			this.ConnectButton.Text = "Connect to Chat";
			this.ConnectButton.ToolTipText = "Connect to Twitch Chat with your OAuth key.";
			this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// SoundDropDown
			// 
			this.SoundDropDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.SoundDropDown.Image = ((System.Drawing.Image)(resources.GetObject("SoundDropDown.Image")));
			this.SoundDropDown.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SoundDropDown.Name = "SoundDropDown";
			this.SoundDropDown.Size = new System.Drawing.Size(59, 22);
			this.SoundDropDown.Text = "Sounds";
			// 
			// AddSoundOptionButton
			// 
			this.AddSoundOptionButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
			this.AddSoundOptionButton.Image = ((System.Drawing.Image)(resources.GetObject("AddSoundOptionButton.Image")));
			this.AddSoundOptionButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddSoundOptionButton.Name = "AddSoundOptionButton";
			this.AddSoundOptionButton.Size = new System.Drawing.Size(108, 22);
			this.AddSoundOptionButton.Text = "Add Sound option";
			this.AddSoundOptionButton.Click += new System.EventHandler(this.AddSoundOptionButton_Click);
			// 
			// ReportBox
			// 
			this.ReportBox.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.ReportBox.FormattingEnabled = true;
			this.ReportBox.HorizontalScrollbar = true;
			this.ReportBox.Location = new System.Drawing.Point(0, 40);
			this.ReportBox.Name = "ReportBox";
			this.ReportBox.ScrollAlwaysVisible = true;
			this.ReportBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
			this.ReportBox.Size = new System.Drawing.Size(800, 316);
			this.ReportBox.TabIndex = 4;
			// 
			// HappyPointSounds
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(800, 356);
			this.Controls.Add(this.ReportBox);
			this.Controls.Add(this.HappyPointSoundsToolStrip);
			this.Name = "HappyPointSounds";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Happy Point Sounds";
			this.HappyPointSoundsToolStrip.ResumeLayout(false);
			this.HappyPointSoundsToolStrip.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.ToolStrip HappyPointSoundsToolStrip;
		private System.Windows.Forms.ToolStripButton ConnectButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripDropDownButton SoundDropDown;
		private System.Windows.Forms.ToolStripButton AddSoundOptionButton;
		private System.Windows.Forms.ListBox ReportBox;
	}
}

