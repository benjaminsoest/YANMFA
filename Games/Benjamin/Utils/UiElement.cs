using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YANMFA.Core;

namespace YANMFA.Games.Benjamin.Utils
{
	class UiElement
	{
		public Vector2D Pos;
		public Vector2D Size;
		public Action ClickAction;
		public Func<string> Content;
		public bool Enabled = true;

		public int X => (int)(StaticDisplay.DisplayWidth * Pos.X / 100);
		public int Y => (int)(StaticDisplay.DisplayHeight * Pos.Y / 100);
		public int W => (int)(StaticDisplay.DisplayWidth * Size.X / 100);
		public int H => (int)(StaticDisplay.DisplayHeight * Size.Y / 100);

		public UiElement(Vector2D pos, Vector2D size, Action clickAction, Func<string> content)
		{
			Pos = pos;
			Size = size;
			ClickAction = clickAction;
			Content = content;
		}

		public void Render(Graphics g, Style style, bool hover)
		{
			g.FillRectangle(hover ? style.BackgroundHover.Brush : style.Background.Brush, X, Y, W, H);
			g.DrawRectangle(hover ? style.ForegroundHover : style.Foreground, X, Y, W, H);
		}

		public bool Contains(int x, int y) => X < x && x < X + W && Y < y && y < Y + H;
	}
}
