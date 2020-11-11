namespace HappyPointSounds.Forms
{
	partial class AddPointSound
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
			this.SoundFileLocationLabel = new System.Windows.Forms.Label();
			this.AddButton = new System.Windows.Forms.Button();
			this.TriggerKeywordLabel = new System.Windows.Forms.Label();
			this.BrowseButton = new System.Windows.Forms.Button();
			this.SoundFileLocationBox = new System.Windows.Forms.TextBox();
			this.TriggerKeywordBox = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// SoundFileLocationLabel
			// 
			this.SoundFileLocationLabel.AutoSize = true;
			this.SoundFileLocationLabel.Location = new System.Drawing.Point(12, 9);
			this.SoundFileLocationLabel.Name = "SoundFileLocationLabel";
			this.SoundFileLocationLabel.Size = new System.Drawing.Size(101, 13);
			this.SoundFileLocationLabel.TabIndex = 0;
			this.SoundFileLocationLabel.Text = "Sound File Location";
			// 
			// AddButton
			// 
			this.AddButton.Location = new System.Drawing.Point(371, 58);
			this.AddButton.Name = "AddButton";
			this.AddButton.Size = new System.Drawing.Size(75, 23);
			this.AddButton.TabIndex = 1;
			this.AddButton.Text = "Add";
			this.AddButton.UseVisualStyleBackColor = true;
			this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
			// 
			// TriggerKeywordLabel
			// 
			this.TriggerKeywordLabel.AutoSize = true;
			this.TriggerKeywordLabel.Location = new System.Drawing.Point(12, 35);
			this.TriggerKeywordLabel.Name = "TriggerKeywordLabel";
			this.TriggerKeywordLabel.Size = new System.Drawing.Size(84, 13);
			this.TriggerKeywordLabel.TabIndex = 2;
			this.TriggerKeywordLabel.Text = "Trigger Keyword";
			// 
			// BrowseButton
			// 
			this.BrowseButton.Location = new System.Drawing.Point(371, 4);
			this.BrowseButton.Name = "BrowseButton";
			this.BrowseButton.Size = new System.Drawing.Size(75, 23);
			this.BrowseButton.TabIndex = 3;
			this.BrowseButton.Text = "Browse...";
			this.BrowseButton.UseVisualStyleBackColor = true;
			this.BrowseButton.Click += new System.EventHandler(this.BrowseButton_Click);
			// 
			// SoundFileLocationBox
			// 
			this.SoundFileLocationBox.Location = new System.Drawing.Point(120, 6);
			this.SoundFileLocationBox.Name = "SoundFileLocationBox";
			this.SoundFileLocationBox.Size = new System.Drawing.Size(245, 20);
			this.SoundFileLocationBox.TabIndex = 4;
			// 
			// TriggerKeywordBox
			// 
			this.TriggerKeywordBox.Location = new System.Drawing.Point(120, 32);
			this.TriggerKeywordBox.Name = "TriggerKeywordBox";
			this.TriggerKeywordBox.Size = new System.Drawing.Size(245, 20);
			this.TriggerKeywordBox.TabIndex = 5;
			// 
			// AddPointSound
			// 
			this.AcceptButton = this.AddButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(464, 86);
			this.Controls.Add(this.TriggerKeywordBox);
			this.Controls.Add(this.SoundFileLocationBox);
			this.Controls.Add(this.BrowseButton);
			this.Controls.Add(this.TriggerKeywordLabel);
			this.Controls.Add(this.AddButton);
			this.Controls.Add(this.SoundFileLocationLabel);
			this.Name = "AddPointSound";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "AddPointSound";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label SoundFileLocationLabel;
		private System.Windows.Forms.Button AddButton;
		private System.Windows.Forms.Label TriggerKeywordLabel;
		private System.Windows.Forms.Button BrowseButton;
		private System.Windows.Forms.TextBox SoundFileLocationBox;
		private System.Windows.Forms.TextBox TriggerKeywordBox;
	}
}