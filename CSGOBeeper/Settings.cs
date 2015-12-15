using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSGOBeeper
{
	public partial class Settings : Form
	{
		private WMPLib.WindowsMediaPlayer player;

		public Settings (WMPLib.WindowsMediaPlayer player)
		{
			InitializeComponent();
			soundFileName.Text = openFile.SafeFileName;
			volumeLabel.Text = "Volume: " + volumeSlider.Value + "%";
			this.player = player;
		}

		private void InitializeComponent ()
		{
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Settings));
			this.openFile = new System.Windows.Forms.OpenFileDialog();
			this.chooseFile = new System.Windows.Forms.Button();
			this.alertSoundLabel = new System.Windows.Forms.Label();
			this.soundFileName = new System.Windows.Forms.TextBox();
			this.volumeLabel = new System.Windows.Forms.Label();
			this.volumeSlider = new System.Windows.Forms.TrackBar();
			this.playSound = new System.Windows.Forms.Button();
			((System.ComponentModel.ISupportInitialize)(this.volumeSlider)).BeginInit();
			this.SuspendLayout();
			// 
			// openFile
			// 
			this.openFile.FileName = global::CSGOBeeper.Properties.Settings.Default.filename;
			this.openFile.Filter = "Audio Files|*.mp3;*.wav";
			this.openFile.FileOk += new System.ComponentModel.CancelEventHandler(this.openFile_FileOk);
			// 
			// chooseFile
			// 
			this.chooseFile.Location = new System.Drawing.Point(208, 12);
			this.chooseFile.Name = "chooseFile";
			this.chooseFile.Size = new System.Drawing.Size(64, 23);
			this.chooseFile.TabIndex = 0;
			this.chooseFile.Text = "Browse...";
			this.chooseFile.UseVisualStyleBackColor = true;
			this.chooseFile.Click += new System.EventHandler(this.chooseFile_Click);
			// 
			// alertSoundLabel
			// 
			this.alertSoundLabel.AutoSize = true;
			this.alertSoundLabel.Location = new System.Drawing.Point(12, 17);
			this.alertSoundLabel.Name = "alertSoundLabel";
			this.alertSoundLabel.Size = new System.Drawing.Size(65, 13);
			this.alertSoundLabel.TabIndex = 1;
			this.alertSoundLabel.Text = "Alert Sound:";
			// 
			// soundFileName
			// 
			this.soundFileName.Enabled = false;
			this.soundFileName.Location = new System.Drawing.Point(12, 33);
			this.soundFileName.Name = "soundFileName";
			this.soundFileName.Size = new System.Drawing.Size(260, 20);
			this.soundFileName.TabIndex = 2;
			this.soundFileName.Text = this.openFile.SafeFileName;
			// 
			// volumeLabel
			// 
			this.volumeLabel.AutoSize = true;
			this.volumeLabel.Location = new System.Drawing.Point(12, 64);
			this.volumeLabel.Name = "volumeLabel";
			this.volumeLabel.Size = new System.Drawing.Size(45, 13);
			this.volumeLabel.TabIndex = 3;
			this.volumeLabel.Text = "Volume:";
			// 
			// volumeSlider
			// 
			this.volumeSlider.Location = new System.Drawing.Point(12, 81);
			this.volumeSlider.Maximum = 100;
			this.volumeSlider.Name = "volumeSlider";
			this.volumeSlider.Size = new System.Drawing.Size(213, 45);
			this.volumeSlider.TabIndex = 4;
			this.volumeSlider.TickFrequency = 5;
			this.volumeSlider.TickStyle = System.Windows.Forms.TickStyle.None;
			this.volumeSlider.Value = global::CSGOBeeper.Properties.Settings.Default.volume;
			this.volumeSlider.Scroll += new System.EventHandler(this.volumeSlider_Scroll);
			// 
			// playSound
			// 
			this.playSound.Location = new System.Drawing.Point(231, 81);
			this.playSound.Name = "playSound";
			this.playSound.Size = new System.Drawing.Size(41, 23);
			this.playSound.TabIndex = 6;
			this.playSound.Text = "Test";
			this.playSound.UseVisualStyleBackColor = true;
			this.playSound.Click += new System.EventHandler(this.playSound_Click);
			// 
			// Settings
			// 
			this.ClientSize = new System.Drawing.Size(284, 122);
			this.Controls.Add(this.playSound);
			this.Controls.Add(this.volumeSlider);
			this.Controls.Add(this.volumeLabel);
			this.Controls.Add(this.soundFileName);
			this.Controls.Add(this.alertSoundLabel);
			this.Controls.Add(this.chooseFile);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Settings";
			this.Text = "CSGOBeeper Settings";
			((System.ComponentModel.ISupportInitialize)(this.volumeSlider)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		private void openFile_FileOk (object sender, CancelEventArgs e)
		{
			Properties.Settings.Default.filename = openFile.FileName;
			soundFileName.Text = openFile.SafeFileName;
		}

		private void chooseFile_Click (object sender, EventArgs e)
		{
			openFile.ShowDialog();
		}

		private void volumeSlider_Scroll (object sender, EventArgs e)
		{
			Properties.Settings.Default.volume = volumeSlider.Value;
			volumeLabel.Text = "Volume: " + volumeSlider.Value + "%";
		}

		private void playSound_Click (object sender, EventArgs e)
		{
			if (player.playState == WMPLib.WMPPlayState.wmppsPlaying)
				player.controls.stop();
			else
				Program.PlaySound(player);
		}
	}
}
