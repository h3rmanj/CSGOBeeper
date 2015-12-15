using CSGOGameObserverSDK;
using CSGOGameObserverSDK.GameDataTypes;
using Newtonsoft.Json.Linq;
using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Forms;


namespace CSGOBeeper
{
	class Program : Form
	{
		private WMPLib.WindowsMediaPlayer player;
		private NotifyIcon trayIcon;
		private ContextMenu trayMenu;
		private static bool freezed;

		[STAThread]
		static void Main ()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Program());
		}

		public Program ()
		{
			trayMenu = new ContextMenu();
			trayMenu.MenuItems.Add("Settings", OnSettings);
			trayMenu.MenuItems.Add("Exit", OnExit);
			trayIcon = new NotifyIcon();
			trayIcon.Text = "CSGOBeeper";
			trayIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
			trayIcon.ContextMenu = trayMenu;
			trayIcon.Visible = true;
			RunServer();
			freezed = false;
			player = new WMPLib.WindowsMediaPlayer();
		}

		public void OnSettings (object sender, EventArgs e)
		{
			new Settings(player).Show();
		}

		public void RunServer ()
		{
			var server = new CSGOGameObserverServer("http://127.0.0.1:3000/");
			server.receivedCSGOServerMessage += OnRecievedCSGOServerMessage;
			server.Start();
		}

		public void OnRecievedCSGOServerMessage (object sender, JObject gameData)
		{
			var state = new CSGOGameState(gameData);

			if (state.Round != null && state.Round.Phase != null)
			{
				switch (state.Round.Phase)
				{
					case "freezetime":
						if (!freezed)
						{
							freezed = true;
							if (CSGOIsMinimized())
								PlaySound(player);
						}
						break;
					default:
						if (freezed)
							freezed = false;
						break;
				}
			}
		}

		public static void PlaySound (WMPLib.WindowsMediaPlayer player)
		{
			player.URL = Properties.Settings.Default.filename;
			player.settings.volume = Properties.Settings.Default.volume;
			player.controls.play();
		}

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		static extern bool IsIconic (IntPtr hWnd);
		public static bool CSGOIsMinimized ()
		{
			foreach (Process p in Process.GetProcesses())
				if (p.ProcessName == "csgo")
					return IsIconic(p.MainWindowHandle);
			return false;
		}

		protected override void OnLoad (EventArgs e)
		{
			Visible = false;
			ShowInTaskbar = false;

			base.OnLoad(e);
		}

		private void OnExit (object sender, EventArgs e)
		{
			Application.Exit();
		}

		protected override void Dispose (bool isDisposing)
		{
			if (isDisposing)
			{
				trayIcon.Dispose();
				Properties.Settings.Default.Save();
			}

			base.Dispose(isDisposing);
		}

		private void InitializeComponent ()
		{
			this.SuspendLayout();
			// 
			// Program
			// 
			this.ClientSize = new System.Drawing.Size(116, 58);
			this.Name = "Program";
			this.ResumeLayout(false);

		}
	}
}
