using System.Drawing;
using System.Windows.Forms;

namespace YANMFA.Games.Alex.SpiderFighter.Models.Decorations
{
    public class BarrierBlock : Item
    {
        private RectangleF _hitbox;

        public BarrierBlock(RectangleF rec, Bitmap texture)
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
            g.DrawImage(CurrentTexture,_hitbox);
        }

        public override void Update()
        {
            MobResistance();
        }

        public override void Resize(int width, int height)
        {

        }
      
        public void MobResistance()
        {
            RectangleF rec = Round.PlayerOne.Hitbox;
            if (Hitbox.IntersectsWith(rec))
            {
                if ((rec.Bottom >= Hitbox.Top) && (rec.Bottom < Hitbox.Bottom - Hitbox.Height / 2))
                {
                    Round.PlayerOne.Hitbox = new RectangleF(rec.X, Hitbox.Top - rec.Height, rec.Width, rec.Height);
                }
                else if ((rec.Right >= Hitbox.Left) && (rec.Right < Hitbox.Left + Hitbox.Width / 2))
                {
                    Round.PlayerOne.Hitbox = new RectangleF(Hitbox.Left - rec.Width, rec.Top, rec.Width, rec.Height);
                }
                else if ((rec.Left <= Hitbox.Right) && (rec.Left > Hitbox.Right - Hitbox.Width / 2))
                {
                    Round.PlayerOne.Hitbox = new RectangleF(Hitbox.Right, rec.Top, rec.Width, rec.Height);
                }
                else if((rec.Top <= Hitbox.Bottom) && (rec.Top > Hitbox.Top + Hitbox.Height /2))
                {
                    Round.PlayerOne.Hitbox = new RectangleF(rec.X, Hitbox.Bottom, rec.Width, rec.Height);
                }
            }
            else
            {
            }
        }
    }
}