using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using YANMFA.Core;
using YANMFA.Games.Benjamin.Utils.Ui;

namespace YANMFA.Games.Benjamin.Utils
{
	abstract class Game : IGameInstance
	{
		public abstract string AssetDirectory { get; }
		public abstract string GameName { get; }
		public abstract string GameDescription { get; }
		public abstract GameMode GameType { get; }
		public virtual Style Style { get => Style.DefaultStyle; }

		public Image GetTitleImage() => new Bitmap(1, 1); // TODO

		public bool IsStopRequested() => false;
		public void UpdateSplash() {}
		public void RenderSplash(Graphics g) {}

		public Dictionary<string, Image> Images { get; set; }
		public UiPanel Ui { get; set; }

		public bool RequestStop;
		public bool Paused;

		public void Start(GameMode gameMode)
		{
			Paused = false;
			RequestStop = false;
			if (AssetDirectory != "")
				Images = LoadAssets<Image>(AssetDirectory, filePath => new Bitmap(filePath), "bmp gif exif jpg png tiff".Split());
			Ui = new UiPanel((0, 0), (100, 100), null, true, false, false, false);
			//
			UiPanel escapeMenu = new UiPanel((30, 30), (40, 40), null, true, true, true, false);
			escapeMenu["question"] = new UiLabel((0, 0), (100, 60), null, "Du pisser willst echt das Spiel verlassen?!", borderVisible: false, backgroundVisible: false, hightlightVisible: false, visible: false);
			escapeMenu["yes"] = new UiLabel((10, 60), (45, 30), () => RequestStop = true, "Yes");
			escapeMenu["no"] = new UiLabel((55, 60), (45, 30), () => escapeMenu.Visible = false, "No");
		}
		public void Update()
		{
			if (StaticMouse.WasButtonPressed(MouseButtons.Left))
				Ui.FindClickedButton((StaticDisplay.DisplayWidth / StaticMouse.MouseX, StaticDisplay.DisplayHeight / StaticMouse.MouseY)).ClickAction();
			if (!Paused)
				InternalUpdate();
		}
		public void Render(Graphics g)
		{
			g.FillRectangle(Style.Background.Brush, 0, 0, g.ClipBounds.Width, g.ClipBounds.Height);
			Vector2D pixelSize = (StaticDisplay.DisplayWidth, StaticDisplay.DisplayHeight);
			Vector2D mousePos = pixelSize / (StaticMouse.MouseX, StaticMouse.MouseY);
			Ui.Render(g, Style, true, pixelSize, mousePos);
			if (!Paused)
				InternalRender(g);
		}
		public void Stop()
		{
			Images.Values.ToList().ForEach(i => i.Dispose());
			Images.Clear();
			Ui = null;
		}
		public abstract void InternalUpdate();
		public abstract void InternalRender(Graphics g);

		public static Dictionary<string, T> LoadAssets<T>(string directory, Func<string, T> buildAsset, params string[] fileEndings)
		{			
			var files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories).Where(filePath => fileEndings.Any(fileEnding => filePath.EndsWith('.' + fileEnding)));
			return files.ToDictionary(filePath => Path.GetFileName(filePath), filePath => buildAsset(filePath));
		}
	}
}
