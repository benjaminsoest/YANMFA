using System.Drawing;
using System.Windows.Forms;

namespace YANMFA.Games.Alex.SpiderFighter.Models.Decorations
{
    public class DecoBlock : Item
    {
        private RectangleF _hitbox;

        public DecoBlock(RectangleF rec, Bitmap texture)
        {
            CurrentTexture = texture;
            Hitbox = rec;
        }

        public override RectangleF Hitbox
        {
            get { return _hitbox; }
            set { _hitbox = value; }
        }

        public override void Render(Graphics g)
        {
            g.DrawImage(CurrentTexture, _hitbox);            
        }

        public override void Update()
        {

        }

        public override void MouseDown(MouseEventArgs mouseEventArgs)
        {

        }

        public override void Resize(int width, int height)
        {

        }

        public override void KeyDown(KeyEventArgs keyEventArgs)
        {

        }

       
    }
}