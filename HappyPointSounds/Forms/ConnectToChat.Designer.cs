namespace HappyPointSounds.Forms
{
	partial class ConnectToChat
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
			this.ConnectButton = new System.Windows.Forms.Button();
			this.UsernameTextBox = new System.Windows.Forms.TextBox();
			this.OAuthTextBox = new System.Windows.Forms.TextBox();
			this.UsernameLabel = new System.Windows.Forms.Label();
			this.OAuthLinkLabel = new System.Windows.Forms.LinkLabel();
			this.ConnectWithAPIButton = new System.Windows.Forms.Button();
			this.APIBox = new System.Windows.Forms.CheckBox();
			this.APILabel = new System.Windows.Forms.Label();
			this.APITextBox = new System.Windows.Forms.TextBox();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.APITab = new System.Windows.Forms.TabPage();
			this.ChatTab = new System.Windows.Forms.TabPage();
			this.APIErrorLabel = new System.Windows.Forms.Label();
			this.tabControl1.SuspendLayout();
			this.APITab.SuspendLayout();
			this.ChatTab.SuspendLayout();
			this.SuspendLayout();
			// 
			// ConnectButton
			// 
			this.ConnectButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.ConnectButton.Location = new System.Drawing.Point(312, 129);
			this.ConnectButton.Name = "ConnectButton";
			this.ConnectButton.Size = new System.Drawing.Size(75, 23);
			this.ConnectButton.TabIndex = 0;
			this.ConnectButton.Text = "Connect";
			this.ConnectButton.UseVisualStyleBackColor = true;
			this.ConnectButton.Click += new System.EventHandler(this.ConnectButton_Click);
			// 
			// UsernameTextBox
			// 
			this.UsernameTextBox.Location = new System.Drawing.Point(118, 103);
			this.UsernameTextBox.Name = "UsernameTextBox";
			this.UsernameTextBox.Size = new System.Drawing.Size(195, 20);
			this.UsernameTextBox.TabIndex = 1;
			// 
			// OAuthTextBox
			// 
			this.OAuthTextBox.Location = new System.Drawing.Point(102, 6);
			this.OAuthTextBox.Name = "OAuthTextBox";
			this.OAuthTextBox.Size = new System.Drawing.Size(195, 20);
			this.OAuthTextBox.TabIndex = 2;
			// 
			// UsernameLabel
			// 
			this.UsernameLabel.AutoSize = true;
			this.UsernameLabel.Location = new System.Drawing.Point(32, 106);
			this.UsernameLabel.Name = "UsernameLabel";
			this.UsernameLabel.Size = new System.Drawing.Size(55, 13);
			this.UsernameLabel.TabIndex = 3;
			this.UsernameLabel.Text = "Username";
			// 
			// OAuthLinkLabel
			// 
			this.OAuthLinkLabel.AutoSize = true;
			this.OAuthLinkLabel.Location = new System.Drawing.Point(13, 9);
			this.OAuthLinkLabel.Name = "OAuthLinkLabel";
			this.OAuthLinkLabel.Size = new System.Drawing.Size(37, 13);
			this.OAuthLinkLabel.TabIndex = 4;
			this.OAuthLinkLabel.TabStop = true;
			this.OAuthLinkLabel.Text = "OAuth";
			this.OAuthLinkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OAuthLinkLabel_LinkClicked);
			// 
			// ConnectWithAPIButton
			// 
			this.ConnectWithAPIButton.Location = new System.Drawing.Point(117, 32);
			this.ConnectWithAPIButton.Name = "ConnectWithAPIButton";
			this.ConnectWithAPIButton.Size = new System.Drawing.Size(164, 23);
			this.ConnectWithAPIButton.TabIndex = 6;
			this.ConnectWithAPIButton.Text = "Connect with API";
			this.ConnectWithAPIButton.UseVisualStyleBackColor = true;
			this.ConnectWithAPIButton.Click += new System.EventHandler(this.ConnectWithAPIButton_Click);
			// 
			// APIBox
			// 
			this.APIBox.AutoSize = true;
			this.APIBox.Enabled = false;
			this.APIBox.Location = new System.Drawing.Point(167, 133);
			this.APIBox.Name = "APIBox";
			this.APIBox.Size = new System.Drawing.Size(98, 17);
			this.APIBox.TabIndex = 10;
			this.APIBox.Text = "Not Connected";
			this.APIBox.UseVisualStyleBackColor = true;
			// 
			// APILabel
			// 
			this.APILabel.AutoSize = true;
			this.APILabel.Location = new System.Drawing.Point(17, 9);
			this.APILabel.Name = "APILabel";
			this.APILabel.Size = new System.Drawing.Size(54, 13);
			this.APILabel.TabIndex = 11;
			this.APILabel.Text = "API code:";
			// 
			// APITextBox
			// 
			this.APITextBox.Location = new System.Drawing.Point(102, 6);
			this.APITextBox.Name = "APITextBox";
			this.APITextBox.Size = new System.Drawing.Size(195, 20);
			this.APITextBox.TabIndex = 12;
			// 
			// tabControl1
			// 
			this.tabControl1.Controls.Add(this.APITab);
			this.tabControl1.Controls.Add(this.ChatTab);
			this.tabControl1.Location = new System.Drawing.Point(12, 12);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(381, 88);
			this.tabControl1.TabIndex = 13;
			// 
			// APITab
			// 
			this.APITab.Controls.Add(this.APITextBox);
			this.APITab.Controls.Add(this.ConnectWithAPIButton);
			this.APITab.Controls.Add(this.APILabel);
			this.APITab.Location = new System.Drawing.Point(4, 22);
			this.APITab.Name = "APITab";
			this.APITab.Padding = new System.Windows.Forms.Padding(3);
			this.APITab.Size = new System.Drawing.Size(373, 62);
			this.APITab.TabIndex = 0;
			this.APITab.Text = "API";
			this.APITab.UseVisualStyleBackColor = true;
			// 
			// ChatTab
			// 
			this.ChatTab.Controls.Add(this.OAuthLinkLabel);
			this.ChatTab.Controls.Add(this.OAuthTextBox);
			this.ChatTab.Location = new System.Drawing.Point(4, 22);
			this.ChatTab.Name = "ChatTab";
			this.ChatTab.Padding = new System.Windows.Forms.Padding(3);
			this.ChatTab.Size = new System.Drawing.Size(373, 62);
			this.ChatTab.TabIndex = 1;
			this.ChatTab.Text = "Chat";
			this.ChatTab.UseVisualStyleBackColor = true;
			// 
			// APIErrorLabel
			// 
			this.APIErrorLabel.AutoSize = true;
			this.APIErrorLabel.ForeColor = System.Drawing.Color.Red;
			this.APIErrorLabel.Location = new System.Drawing.Point(13, 134);
			this.APIErrorLabel.Name = "APIErrorLabel";
			this.APIErrorLabel.Size = new System.Drawing.Size(141, 13);
			this.APIErrorLabel.TabIndex = 14;
			this.APIErrorLabel.Text = "Cancelled? Please try again.";
			// 
			// ConnectToChat
			// 
			this.AcceptButton = this.ConnectButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(399, 158);
			this.Controls.Add(this.APIErrorLabel);
			this.Controls.Add(this.UsernameTextBox);
			this.Controls.Add(this.tabControl1);
			this.Controls.Add(this.APIBox);
			this.Controls.Add(this.UsernameLabel);
			this.Controls.Add(this.ConnectButton);
			this.Name = "ConnectToChat";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Connect To Chat";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ConnectToChat_FormClosed);
			this.tabControl1.ResumeLayout(false);
			this.APITab.ResumeLayout(false);
			this.APITab.PerformLayout();
			this.ChatTab.ResumeLayout(false);
			this.ChatTab.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button ConnectButton;
		private System.Windows.Forms.TextBox UsernameTextBox;
		private System.Windows.Forms.TextBox OAuthTextBox;
		private System.Windows.Forms.Label UsernameLabel;
		private System.Windows.Forms.LinkLabel OAuthLinkLabel;
		private System.Windows.Forms.Button ConnectWithAPIButton;
		private System.Windows.Forms.CheckBox APIBox;
		private System.Windows.Forms.Label APILabel;
		private System.Windows.Forms.TextBox APITextBox;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage APITab;
		private System.Windows.Forms.TabPage ChatTab;
		private System.Windows.Forms.Label APIErrorLabel;
	}
}