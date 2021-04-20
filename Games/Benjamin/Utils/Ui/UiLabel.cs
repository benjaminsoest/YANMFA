using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YANMFA.Games.Benjamin.Utils.Ui
{
	class UiLabel : UiElement
	{
		Func<string> Text { get; set; }
		Style.FontType FontType { get; set; }

		public UiLabel(Vector2D pos, Vector2D size, Action clickAction, Func<string> text, Style.FontType fontType = Style.FontType.NORMAL, bool visible = true, bool backgroundVisible = true, bool borderVisible = true, bool hightlightVisible = true)
			: base(pos, size, clickAction, visible, backgroundVisible, borderVisible, hightlightVisible)
		{
			Text = text;
			FontType = fontType;
		}
		public UiLabel(Vector2D pos, Vector2D size, Action clickAction, string text, Style.FontType fontType = Style.FontType.NORMAL, bool visible = true, bool backgroundVisible = true, bool borderVisible = true, bool hightlightVisible = true)
			: base(pos, size, clickAction, visible, backgroundVisible, borderVisible, hightlightVisible)
		{
			Text = () => text;
			FontType = fontType;
		}

		public override void Render(Graphics g, Style style, bool highlight, Vector2D pixelSize, Vector2D mousePos)
		{
			base.Render(g, style, highlight, pixelSize, mousePos);
			g.DrawString(Text(), style.Fonts[FontType], style.GetForeground(HightlightVisible && highlight).Brush, new RectangleF(0, 0, (float)pixelSize.X, (float)pixelSize.Y));
		}
	}
}
