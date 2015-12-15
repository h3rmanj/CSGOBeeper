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
			trayMenu.MenuItems.Add("Exit", OnExit);
			trayIcon = new NotifyIcon();
			trayIcon.Text = "CSGOBeeper";
			trayIcon.Icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
			trayIcon.ContextMenu = trayMenu;
			trayIcon.Visible = true;
			RunServer();
			freezed = false;
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
								PlaySound();
						}
						break;
					default:
						if (freezed)
							freezed = false;
						break;
				}
			}
		}

		public static void PlaySound ()
		{
			System.Media.SoundPlayer player = new System.Media.SoundPlayer("alert.wav");
			player.Play();
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
				trayIcon.Dispose();

			base.Dispose(isDisposing);
		}
	}
}
