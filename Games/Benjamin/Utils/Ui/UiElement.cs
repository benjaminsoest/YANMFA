using System;
using System.Drawing;

namespace YANMFA.Games.Benjamin.Utils.Ui
{
	class UiElement
	{
		public string Name { get; set; }
		public Vector2D Pos { get; set; }
		public Vector2D Size { get; set; }
		public bool Visible { get; set; }
		public bool BackgroundVisible { get; set; }
		public bool BorderVisible { get; set; }
		public bool HightlightVisible { get; set; }
		public Action ClickAction { get; set; }

		public UiElement(Vector2D pos, Vector2D size, Action clickAction, bool visible = true, bool backgroundVisible = true, bool borderVisible = true, bool hightlightVisible = true)
		{
			Pos = pos;
			Size = size;
			ClickAction = clickAction;
			Visible = visible;
			BackgroundVisible = backgroundVisible;
			BorderVisible = borderVisible;
			HightlightVisible = hightlightVisible;
		}

		public virtual void Render(Graphics g, Style style, bool highlight, Vector2D pixelSize, Vector2D mousePos)
		{
			if (BackgroundVisible)
				g.FillRectangle(style.GetBackground(HightlightVisible && highlight).Brush, 0, 0, (float)pixelSize.X - 1, (float)pixelSize.Y - 1);
			if (BorderVisible)
				g.DrawRectangle(style.GetBackground(HightlightVisible && highlight), 0, 0, (float)pixelSize.X - 1, (float)pixelSize.Y - 1);
		}
		public bool Contains(Vector2D v) => Pos.X < v.X && v.X < Pos.X + Size.X && Pos.Y < v.Y && v.Y < Pos.Y + Size.Y;
	}
}
