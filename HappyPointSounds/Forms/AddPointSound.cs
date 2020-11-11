using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using HappyPointSounds.Models;

namespace HappyPointSounds.Forms
{
	public partial class AddPointSound : Form
	{
		internal Sound NewSound = null;

		public AddPointSound()
		{
			InitializeComponent();
			this.DialogResult = DialogResult.Abort;
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

		private void BrowseButton_Click(object sender, EventArgs e)
		{
			OpenFileDialog _browse = new OpenFileDialog();
			_browse.Filter = "Audio Files (*.mp3;*.wav)|*.mp3;*.wav|All files (*.*)|*.*";
			DialogResult _result = _browse.ShowDialog();

			if (_result == DialogResult.OK)
			{
				string _filePath = _browse.FileName;
				FileInfo _file = null;

				try
				{
					_file = new FileInfo(_filePath);
				}
				//Dirty, but hey. It works.
				catch (Exception ex)
				{
					MessageBox.Show($"Something went wrong:\n\n{ex.Message}", "Error?", MessageBoxButtons.OK);
					this.DialogResult = DialogResult.Abort;
					return;
				}


				if (!ReferenceEquals(_file, null))
					if (_file.Exists)
						NewSound = new Sound(_file);
					else
						MessageBox.Show("That file (path) doesn't exist.", "Ahbi wtf?", MessageBoxButtons.OK);
				else
					MessageBox.Show("That filename isn't valid.", "Ahbi wtf?", MessageBoxButtons.OK);

				SoundFileLocationBox.Text = _filePath;
			}
		}

		private void AddButton_Click(object sender, EventArgs e)
		{
			if(!ReferenceEquals(SoundFileLocationBox.Text, string.Empty) && !ReferenceEquals(TriggerKeywordBox.Text, string.Empty))
			{
				if (ReferenceEquals(NewSound, null))
					NewSound = new Sound(new FileInfo(SoundFileLocationBox.Text));
				else
					NewSound.SetFileName(new FileInfo(SoundFileLocationBox.Text));

				NewSound.SetKeyword(TriggerKeywordBox.Text);
				this.DialogResult = DialogResult.OK;
				return;
			}

			MessageBox.Show("Either this is fucked, or you gotta still fill in everything.", "Ahbi wtf?", MessageBoxButtons.OK);
		}
	}
}
