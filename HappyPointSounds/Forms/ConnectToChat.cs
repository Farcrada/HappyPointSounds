using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using HappyPointSounds.Models;

namespace HappyPointSounds.Forms
{
	public partial class ConnectToChat : Form
	{
		public string[] LoginDetails = new string[3];
		string _xmlFileLocation = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\HappyPointSounds\\CurrentUser.xml";
		AuthImplicitFlow _authFlow = new AuthImplicitFlow();
		string _authCode = string.Empty;

		string _prefix = "http://localhost:8969/";
		HttpListener _listener = null;

		public ConnectToChat()
		{
			InitializeComponent();
			APIErrorLabel.Hide();

			try
			{
				if (File.Exists(_xmlFileLocation))
				{
					XDocument doc = XDocument.Load(_xmlFileLocation);
					XElement LoginSettings = doc.Root.Element("LoginSettings");

					_authCode = LoginSettings.Element("API").Value;
					if (!string.IsNullOrWhiteSpace(_authCode))
						ToggleAPIBox(true);

					UsernameTextBox.Text = LoginSettings.Element("Username").Value;
					OAuthTextBox.Text = $"oauth:{LoginSettings.Element("OAuth").Value}";
					APITextBox.Text = _authCode;
				}
				else
					if (!Directory.Exists($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\HappyPointSounds"))
					Directory.CreateDirectory($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\HappyPointSounds");
			}
			catch (Exception) { }
		}

		protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
		{
			if (keyData == Keys.Escape)
			{
				this.DialogResult = DialogResult.Abort;
				return true;
			}
			return base.ProcessCmdKey(ref msg, keyData);
		}

		private void OAuthLinkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("https://twitchapps.com/tmi/");
		}

		private void ConnectButton_Click(object sender, EventArgs e)
		{
			try
			{
				XDocument doc = new XDocument();
				XElement root = null;
				XElement LoginSettings = new XElement("LoginSettings");

				// To make sure we don't remove any other settings
				// we'll need to copy the entire file into memory
				// and then replace what we update.
				try
				{
					// Load XML
					doc = XDocument.Load(_xmlFileLocation);
					// Locally store user settings.
					root = doc.Element("CurrentUser");
					// Remove the LoginSettings since we will be replacing it.
					root.Element("LoginSettings").Remove();

					// Clear document variable so we know it's empty and clean
					doc = new XDocument();
				}
				// If all that fails we know it's all null, clean and new.
				catch (Exception) { root = new XElement("CurrentUser"); }


				/// This was for IRC connections only.
				/// Currently serves no purpose for us because
				/// everything channelpoints-related goes through PubSub.
				/// 
				/// And yet it seems there is still a use for this.

				if (UsernameTextBox.Text.Length < 3)
				{
					MessageBox.Show("Please enter a valid name in the username field and try again.");
					if (OAuthTextBox.Text.Length < 7)
						MessageBox.Show("Please enter a valid OAuth in the field and try again.");
					else
					{ }
				}
				else
				{ }

				LoginDetails[0] = UsernameTextBox.Text;
				LoginDetails[1] = OAuthTextBox.Text.ToLower().Replace("oauth:", "");
				LoginDetails[2] = _authCode;

				LoginSettings.Add(new XElement("Username", LoginDetails[0]));
				LoginSettings.Add(new XElement("OAuth", LoginDetails[1]));
				LoginSettings.Add(new XElement("API", LoginDetails[2]));

				LoginSettings.Add(new XElement("Server", "irc.twitch.tv"));
				LoginSettings.Add(new XElement("Port", "6667"));

				root.Add(LoginSettings);
				doc.Add(root);
				doc.Save(_xmlFileLocation);
			}
			catch (UnauthorizedAccessException ex)
			{
				MessageBox.Show("Excuse the interruption but the program saves it's config in your Documents folder. The program has no access to that and will be unable to save. A pop-up should notify that there has been \"[...] unauthorized access\".\r\nYou can still connect, but it will not save.", "Oopsie", MessageBoxButtons.OK);
			}

			this.DialogResult = DialogResult.OK;
		}

		private void ConnectWithAPIButton_Click(object sender, EventArgs e)
		{
			if (APIBox.Checked)
			{
				DialogResult check = MessageBox.Show("Are you sure you want to force a reconnection?", "Double checking", MessageBoxButtons.YesNo);

				if (check.Equals(DialogResult.No))
					return;
			}

			// FIXME Move this one line over to ToggleAPIBox
			ConnectWithAPIButton.Visible = false;

			Task listenForResponseTask = new Task(ListenForResponseAsync);
			listenForResponseTask.Start();
			Process.Start(_authFlow.AuthRedirectUrl);
			listenForResponseTask.Wait();
		}

		void ToggleAPIBox(bool on, bool error = false)
		{
			APIBox.Text = error ? "ERROR" : $"{((!on) ? "Not " : "")}Connected";
			APIBox.Checked = !error ? on : !error;

			ConnectWithAPIButton.Text = error ? "Try again?" : $"{((on) ? "FORCE (Re)" : "")}Connect with API";
			ConnectWithAPIButton.Visible = !error ? on : error;

			APIErrorLabel.Visible = error;
		}

		public void ListenForResponseAsync()
		{
			_listener = new HttpListener();
			_listener.Prefixes.Add(_prefix);
			_listener.Start();
			_listener.BeginGetContext(InitialCallback, null);
		}

		private void InitialCallback(IAsyncResult ar)
		{
			HttpListenerContext context = _listener.EndGetContext(ar);

			// Report back to the user
			byte[] buffer = Encoding.UTF8.GetBytes(@"
<!DOCTYPE html>
<html>
	<body>
		Fetching anchor...
		</br> </br>

		<script type=""text/javascript"">
		window.onload = function () {
			let xhr = new XMLHttpRequest();
			xhr.open(""POST"", """ + _prefix + @""", true);
			xhr.setRequestHeader('Content-Type', 'application/json');
			xhr.send(JSON.stringify({
				anchor: window.location.hash
			}));
		}
		</script>

		The program should now load a new page for you, if it didn't - but did grab the token: </br>
		You can close this window.
	</body>
</html>");

			// listen for the next request before sending it to the user
			_listener.BeginGetContext(GetContextCallback, null);

			using (HttpListenerResponse response = context.Response)
			{
				response.ContentType = "text/html";
				response.ContentLength64 = buffer.Length;
				response.StatusCode = 200;
				response.OutputStream.Write(buffer, 0, buffer.Length);
			}
		}

		void GetContextCallback(IAsyncResult ar)
		{
			// Get the context
			HttpListenerContext context = _listener.EndGetContext(ar);

			/// This works with query but not an anchor, thus it is redundant.
			/// BUT, we send it through as json, so there's that.
			// Response is:
			//     [0]    [1]
			//  /# code=<TOKEN>			[0]
			//	&  scope=<SCOPE>		[1]
			//  &  state=<STATE>		[2]
			//string[] responseSplit = context.Request.RawUrl.Split('&');
			//_authCode = responseSplit[0].Split('=')[1];

			///

			// Response is:
			// {"token": '
			//				[0]          [1]
			//			#access_token=	<TOKEN>		[0]
			//			&scope=			<SCOPE(S)>	[1]
			//			&state=			<STATE>		[2]
			//			&token_type=	<TYPE>		[3]
			// '}
			HttpListenerRequest request = context.Request;
			string anchorJson;
			using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
				anchorJson = reader.ReadToEnd();

			if (anchorJson.Contains("&"))
			{
				_authCode = anchorJson.Split('&')[0].Split('=')[1];

				MethodInvoker mi = delegate () { ToggleAPIBox(true); APITextBox.Text = _authCode; }; this.Invoke(mi);
			}
			else
			{
				MethodInvoker mi = delegate () { ToggleAPIBox(false, true); }; this.Invoke(mi);
			}


			// Report back to the user
			byte[] buffer = Encoding.UTF8.GetBytes("<html><body>You can close this window now</body></html>");
			using (HttpListenerResponse response = context.Response)
			{
				response.ContentType = "text/html";
				response.ContentLength64 = buffer.Length;
				response.StatusCode = 200;
				response.OutputStream.Write(buffer, 0, buffer.Length);
			}

			_listener.Stop();
			_listener.Close();
		}

		private void ConnectToChat_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.DialogResult = DialogResult.Abort;
		}
	}
}
