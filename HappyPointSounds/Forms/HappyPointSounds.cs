using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text.Json;
using System.Windows.Forms;
using HappyPointSounds.Events;
using HappyPointSounds.Models;

namespace HappyPointSounds.Forms
{
	public partial class HappyPointSounds : Form
	{
		public string[] LoginDetails = new string[3];

		private bool _connected = false;
		private List<Sound> _sounds = new List<Sound>();
		private List<ToolStripMenuItem> _soundMenuItems = new List<ToolStripMenuItem>();

		PubSubHandler _pubSubHandler = new PubSubHandler();

		public HappyPointSounds()
		{
			InitializeComponent();
		}

		private void ConnectButton_Click(object sender, EventArgs e)
		{
			ToggleConnectBox(false);

			if (_connected)
			{
				_connected = !_connected;
				_pubSubHandler.Disconnect();
				ConnectButton.Text = "Connect to Chat";
				return;
			}


			ConnectToChat connectForm = new ConnectToChat();

			DialogResult result = connectForm.ShowDialog();
			if (result == DialogResult.OK)
			{
				if (connectForm.LoginDetails[0].Length > 2
					&& connectForm.LoginDetails[1].Length > 5
					&& connectForm.LoginDetails[2].Length > 5)

					LoginDetails = connectForm.LoginDetails;

				_connected = true;
			}
			else
				return;

			ConnectButton.Text = "Disconnect";

			_pubSubHandler.OnSocketConnected += onSocketConnected;
			_pubSubHandler.OnSocketError += onSocketError;
			_pubSubHandler.OnListenResponse += onListenResponse;
			_pubSubHandler.OnSocketCleaning += onSocketCleaning;
			_pubSubHandler.OnSocketReady += onSocketReady;
			_pubSubHandler.OnChannelCommerceReceived += onPointsSpend;

			_pubSubHandler.Connect();

		}

		private void onPointsSpend(object sender, OnChannelCommerceReceivedArgs e)
		{
			MessageBox.Show(e.ItemDescription);
		}

		private void ToggleConnectBox(bool enabled)
		{
			ConnectButton.Enabled = enabled;
		}

		private void onSocketReady(object sender, EventArgs e)
		{
			ToggleConnectBox(true);
		}

		private void onSocketCleaning(object sender, EventArgs e)
		{
			ToggleConnectBox(false);
		}

		private void onSocketError(object sender, OnErrorEventArgs e)
		{
			MessageBox.Show(e.Exception.Message);

			_connected = !_connected;
			_pubSubHandler.Disconnect();
			ConnectButton.Text = "Connect to Chat";
		}

		private void AddSoundOptionButton_Click(object sender, EventArgs e)
		{
			AddPointSound soundForm = new AddPointSound();

			DialogResult result = soundForm.ShowDialog();
			if (result == DialogResult.OK)
			{
				Sound newSound = soundForm.NewSound;

				foreach (Sound sound in _sounds)
					if (ReferenceEquals(sound.Keyword, newSound.Keyword))
					{
						MessageBox.Show("Either this is fucked, or this keyword already exists, which would make it a duplicate.", "Ahbi wtf?", MessageBoxButtons.OK);
						return;
					}

				_sounds.Add(newSound);

				ToolStripMenuItem newSoundButton = new ToolStripMenuItem();
				newSoundButton.Click += NewSoundButton_Click;
				newSoundButton.Text = newSound.Keyword;
				newSoundButton.Name = newSound.Keyword;

				SoundDropDown.DropDownItems.Add(newSoundButton);
				_soundMenuItems.Add(newSoundButton);
			}
		}

		private void NewSoundButton_Click(object sender, EventArgs e)
		{
			MessageBox.Show($"Removed: {((ToolStripMenuItem)sender).Name}", "YEP", MessageBoxButtons.OK);
			SoundDropDown.DropDownItems.Remove((ToolStripMenuItem)sender);
		}

		private string GetChannelID()
		{
			string result;
			string url = $"https://api.twitch.tv/helix/users?login={LoginDetails[0]}";//HappyThoughts0001";//

			HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);

			httpRequest.Headers["Authorization"] = $"Bearer {LoginDetails[2]}";
			httpRequest.Headers["Client-Id"] = $"{AuthImplicitFlow.ClientID}";

			HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
			using (StreamReader streamReader = new StreamReader(httpResponse.GetResponseStream()))
				result = streamReader.ReadToEnd();


			JsonElement jsonData = JsonDocument.Parse(result).RootElement.GetProperty("data");
			foreach (JsonElement property in jsonData.EnumerateArray())
				if (property.TryGetProperty("id", out JsonElement id))
					return id.GetString();

			return string.Empty;
		}

		private void onSocketConnected(object sender, EventArgs e)
		{
			// SendTopics accepts an oauth optionally, which is necessary for some topics
			_pubSubHandler.SendTopics(GetChannelID());
		}

		private void onListenResponse(object sender, OnListenResponseArgs e)
		{
			MessageBox.Show(e.Response.Error);
			if (!e.Successful)
				throw new Exception($"Failed to listen! Response: {e.Response.Error}");
		}
	}
}
