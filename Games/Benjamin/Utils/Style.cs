using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YANMFA.Games.Benjamin.Utils
{
	class Style
	{
		public Pen Foreground { get; }
		public Pen Background { get; }
		public Pen ForegroundHover { get; }
		public Pen BackgroundHover { get; }

		public Style(Pen foreground, Pen background, Pen foregroundHover, Pen backgroundHover)
		{
			Foreground = foreground;
			Background = background;
			ForegroundHover = foregroundHover;
			BackgroundHover = backgroundHover;
		}
		public Style(Color foreground, Color background, Color foregroundHover, Color backgroundHover)
			: this(new Pen(foreground), new Pen(background), new Pen(foregroundHover), new Pen(backgroundHover)) {}
	}
}
