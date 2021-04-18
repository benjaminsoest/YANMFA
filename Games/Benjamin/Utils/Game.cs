using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using YANMFA.Core;

namespace YANMFA.Games.Benjamin.Utils
{
	abstract class Game : IGameInstance
	{
		public abstract string AssetDirectory { get; }
		public abstract string GameName { get; }
		public abstract string GameDescription { get; }
		public abstract GameMode GameType { get; }
		public abstract Style Style { get; }

		public Image GetTitleImage() => new Bitmap(1, 1); // TODO

		public bool IsStopRequested() => false;
		public void UpdateSplash() {}
		public void RenderSplash(Graphics g) {}

		public Dictionary<string, Bitmap> Images;
		public Dictionary<string, UiElement> UiElements;
		public MouseEventArgs LastMouseUpArgs;
		public MouseEventHandler MouseUpListener;

		public virtual void Start(GameMode mode)
		{
			if (AssetDirectory != "")
				Images = LoadAssets(AssetDirectory, filePath => new Bitmap(filePath), "bmp gif exif jpg png tiff".Split());
			UiElements = new Dictionary<string, UiElement>();
			LastMouseUpArgs = null;
			MouseUpListener = new MouseEventHandler((_, args) => LastMouseUpArgs = args);
			StaticMouse.AddMouseUpListener(MouseUpListener);
		}
		public virtual void Update()
		{

		}
		public virtual void Render(Graphics g)
		{
			g.FillRectangle(Style.Background.Brush, 0, 0, g.ClipBounds.Width, g.ClipBounds.Height);
			if (LastMouseUpArgs != null)
				UiElements.Values.FirstOrDefault(e => e.Enabled && e.Contains(StaticMouse.MouseX, StaticMouse.MouseY))?.ClickAction(LastMouseUpArgs.Button);
			foreach (var e in UiElements.Values.Where(e => e.Enabled))
				e.Render(g, Style, e.Contains(StaticMouse.MouseX, StaticMouse.MouseY));
			LastMouseUpArgs = null;
		}
		public virtual void Stop()
		{
			StaticMouse.RemoveMouseUpListener(MouseUpListener);
			Images.Values.ToList().ForEach(i => i.Dispose());
			Images.Clear();
			UiElements.Clear();
		}
		public static Dictionary<string, T> LoadAssets<T>(string directory, Func<string, T> buildAsset, params string[] fileEndings)
		{			
			var files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories).Where(filePath => fileEndings.Any(fileEnding => filePath.EndsWith('.' + fileEnding)));
			return files.ToDictionary(filePath => Path.GetFileName(filePath), filePath => buildAsset(filePath));
		}
	}
}
