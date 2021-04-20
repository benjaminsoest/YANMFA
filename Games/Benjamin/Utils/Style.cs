using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace YANMFA.Games.Benjamin.Utils
{
	class Style
	{
		public enum FontType { SMALL, NORMAL, BIG, }
		public static Style DefaultStyle = new Style(Color.Black, Color.White, Color.Black, Color.LightGray);

		public Pen Foreground { get; }
		public Pen Background { get; }
		public Pen ForegroundHightlight { get; }
		public Pen BackgroundHightlight { get; }
		public Dictionary<FontType, Font> Fonts { get; }
		public Pen GetForeground(bool highlight) => highlight ? ForegroundHightlight : Foreground;
		public Pen GetBackground(bool highlight) => highlight ? BackgroundHightlight : Background;

		public Style(Color foreground, Color background, Color foregroundHightlight, Color backgroundHightlight, Dictionary<FontType, Font> fonts = null)
		{
			Foreground = new Pen(foreground);
			Background = new Pen(background);
			ForegroundHightlight = new Pen(foregroundHightlight);
			BackgroundHightlight = new Pen(backgroundHightlight);
			Fonts = fonts;
			if (Fonts == null)
			{
				Fonts = new Dictionary<FontType, Font>() { 
					{ FontType.SMALL, new Font(Control.DefaultFont.FontFamily, 12) }, 
					{ FontType.NORMAL, new Font(Control.DefaultFont.FontFamily, 24) }, 
					{ FontType.BIG, new Font(Control.DefaultFont.FontFamily, 48) } 
				};
			}
		}
	}
}
