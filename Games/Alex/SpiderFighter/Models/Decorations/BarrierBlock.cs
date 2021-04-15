using System.Drawing;
using System.Windows.Forms;

namespace YANMFA.Games.Alex.SpiderFighter.Models.Decorations
{
    public class BarrierBlock : Item
    {
        private RectangleF _hitbox;

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

        public override void MouseDown(MouseEventArgs mouseEventArgs)
        {

        }

        public override void Resize(int width, int height)
        {
        }

        public override void KeyDown(KeyEventArgs keyEventArgs)
        {

        }

        public void MobResistance()
        {
            foreach (var VARIABLE in Round.CurrentLevel.Mobs)
            {
                RectangleF rec = VARIABLE.Hitbox;
                if (_hitbox.IntersectsWith(rec))
                {
                    if ((rec.Bottom >= _hitbox.Top) && (rec.Bottom < _hitbox.Bottom - _hitbox.Height/2))
                    {
                        rec = new RectangleF(rec.X, _hitbox.Top - rec.Height, rec.Width, rec.Height);
                    }
                    else if ((rec.Right >= _hitbox.Left) && (rec.Right < _hitbox.Left + _hitbox.Width/2))
                    {                   
                        rec = new RectangleF(_hitbox.Left - rec.Width, rec.Top, rec.Width, rec.Height);
                    }
                    else if ((rec.Left <= _hitbox.Right) && (rec.Left > _hitbox.Right - _hitbox.Width/2))
                    {
                        rec =  new RectangleF(_hitbox.Right, rec.Top, rec.Width, rec.Height);
                    }                               
                    else
                    {
                        
                    }
                }
                else
                {
                }     
            }
        }
    }
}