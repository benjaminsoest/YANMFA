using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace YANMFA.Games.Benjamin.Utils.Ui
{
	class UiPanel : UiElement
	{
		public List<UiElement> Children = new List<UiElement>();

		public UiPanel(Vector2D pos, Vector2D size, Action clickAction, bool visible = true, bool backgroundVisible = true, bool borderVisible = true, bool hightlightVisible = false)
			: base(pos, size, clickAction, visible, backgroundVisible, borderVisible, hightlightVisible)
		{}

		public UiElement this[string name]
		{
			get => Children.Find(e => e.Name == name);
			set
			{
				int i = Children.FindIndex(e => e.Name == name);
				if (i == -1)
					Children.Add(value);
				else
					Children[i] = value;
			}
		}

		public override void Render(Graphics g, Style style, bool highlight, Vector2D pixelSize, Vector2D mousePos)
		{
			base.Render(g, style, highlight, pixelSize, mousePos);
			foreach (var e in Children.AsEnumerable().Reverse())
				if (e.Visible)
				{
					var (x, y) = e.Pos / 100 * pixelSize;
					g.TranslateTransform((float)x, (float)y);					
					e.Render(g, style, Contains(mousePos), e.Size / 100 * pixelSize, mousePos - Pos);
				}
		}
		public UiElement FindClickedButton(Vector2D mousePos)
		{
			foreach (var e in Children)
				if (e.Contains(mousePos))
				{
					if (e.ClickAction != null)
						return e;
					var button = (e as UiPanel)?.FindClickedButton(mousePos - e.Pos);
					if (button != null)
						return button;
				}
			return null;
		}
	}
}
