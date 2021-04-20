using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YANMFA.Games.Benjamin.Utils.Ui
{
	class UiImage : UiElement
	{
		public Image Image { get; set; }
		public Image HighlightImage { get; set; }

		public UiImage(Vector2D pos, Vector2D size, Image image, Image highlightImage, Action clickAction, bool visible = true, bool backgroundVisible = true, bool borderVisible = true, bool hightlightVisible = true) 
			: base(pos, size, clickAction, visible, backgroundVisible, borderVisible, hightlightVisible)
		{
			Image = image;
			HighlightImage = highlightImage;
		}

		public override void Render(Graphics g, Style style, bool highlight, Vector2D pixelSize, Vector2D mousePos)
		{
			base.Render(g, style, highlight, pixelSize, mousePos);			
			if (HightlightVisible && highlight)
				g.DrawImage(HighlightImage, 0, 0, (float)pixelSize.X, (float)pixelSize.Y);
			else
				g.DrawImage(Image, 0, 0, (float)pixelSize.X, (float)pixelSize.Y);
		}
	}
}
