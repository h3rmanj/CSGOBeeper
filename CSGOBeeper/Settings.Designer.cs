namespace CSGOBeeper
{
	partial class Settings
	{
		private System.ComponentModel.IContainer components = null;
		
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose (bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		private System.Windows.Forms.OpenFileDialog openFile;
		private System.Windows.Forms.Button chooseFile;
		private System.Windows.Forms.Label alertSoundLabel;
		private System.Windows.Forms.TextBox soundFileName;
		private System.Windows.Forms.Label volumeLabel;
		private System.Windows.Forms.TrackBar volumeSlider;
		private System.Windows.Forms.Button playSound;
	}
}