using System.Drawing;

namespace YANMFA.Games.Alex.SpiderFighter.Models.Items
{
    public class Weapon : Item
    {
        private RectangleF _hitbox;

        public Weapon(RectangleF rec, Bitmap texture)
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

        public override void Resize(int width, int height)
        {
            
        }
    }
}