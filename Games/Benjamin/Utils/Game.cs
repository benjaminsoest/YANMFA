using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

		public virtual void Start(GameMode mode)
		{
			if (AssetDirectory != "")
			{
				Images = LoadAssets(AssetDirectory, filePath => new Bitmap(filePath), "bmp gif exif jpg png tiff".Split());
			}
			UiElements = new Dictionary<string, UiElement>();
		}
		public virtual void Update() {}
		public virtual void Render(Graphics g)
		{
			g.FillRectangle(Style.Background.Brush, 0, 0, g.ClipBounds.Width, g.ClipBounds.Height);
			RenderUI(g);
		}
		public virtual void Stop()
		{
			Images.Values.ToList().ForEach(i => i.Dispose());
			Images.Clear();
			UiElements.Clear();
		}

		public void RenderUI(Graphics g)
		{	
			foreach (var e in UiElements.Values.Where(e => e.Enabled))
				e.Render(g, Style, e.Contains(StaticMouse.MouseX, StaticMouse.MouseY));
		}
		public static Dictionary<string, T> LoadAssets<T>(string directory, Func<string, T> buildAsset, params string[] fileEndings)
		{			
			var files = Directory.GetFiles(directory, "*", SearchOption.AllDirectories).Where(filePath => fileEndings.Any(fileEnding => filePath.EndsWith('.' + fileEnding)));
			return files.ToDictionary(filePath => Path.GetFileName(filePath), filePath => buildAsset(filePath));
		}
	}
}
