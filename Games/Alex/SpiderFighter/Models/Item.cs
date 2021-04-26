using System.Drawing;
using System.Windows.Forms;
using YANMFA.Games.Alex.SpiderFighter.Models.Mobs;

namespace YANMFA.Games.Alex.SpiderFighter.Models
{
    public class Item
    {
        private RectangleF _hitbox;
        private Bitmap _currentTexture;
        
        public virtual RectangleF Hitbox
        {
            get => _hitbox;
            set => _hitbox = value;
        }

        public Bitmap CurrentTexture
        {
            get => _currentTexture;
            set => _currentTexture = value;
        }

        public Item()
        {
            
        }
        
        public virtual void Update(){}
        public virtual void Render(Graphics g){}
        public virtual void Resize(int width, int height){}
        public virtual void KeyDown(KeyEventArgs e){}
        public virtual void MouseDown(MouseEventArgs e){}

        public void DeleteItem() => Round.CurrentLevel.Items.Remove(this);

        public void AddItem() => Round.CurrentLevel.Items.Add(this);

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
                else if ((rec.Top <= Hitbox.Bottom) && (rec.Top > Hitbox.Top + Hitbox.Height / 2))
                {
                    Round.PlayerOne.Hitbox = new RectangleF(rec.X, Hitbox.Bottom, rec.Width, rec.Height);
                }
            }
            else
            {
            }

            foreach (Mob item in Round.CurrentLevel.Mobs)
            {
                if (Hitbox.IntersectsWith(item.Hitbox))
                {
                    if ((item.Hitbox.Bottom >= Hitbox.Top) && (item.Hitbox.Bottom < Hitbox.Bottom - Hitbox.Height / 2))
                    {                        
                        item.Hitbox = new RectangleF(item.Hitbox.X, Hitbox.Top - item.Hitbox.Height, item.Hitbox.Width, item.Hitbox.Height);
                    }
                    else if ((item.Hitbox.Right >= Hitbox.Left) && (item.Hitbox.Right < Hitbox.Left + Hitbox.Width / 2))
                    {
                        item.Hitbox = new RectangleF(Hitbox.Left - item.Hitbox.Width, item.Hitbox.Y, item.Hitbox.Width, item.Hitbox.Height);
                    }
                    else if ((item.Hitbox.Left <= Hitbox.Right) && (item.Hitbox.Left > Hitbox.Right - Hitbox.Width / 2))
                    {
                        item.Hitbox = new RectangleF(Hitbox.Right, item.Hitbox.Y, rec.Width, rec.Height);
                    }
                    else if ((item.Hitbox.Top <= Hitbox.Bottom) && (item.Hitbox.Top > Hitbox.Top + Hitbox.Height / 2))
                    {
                        item.Hitbox = new RectangleF(rec.X, Hitbox.Bottom, rec.Width, rec.Height);
                    }
                }
                else
                {
                }
            }
        }
    }
}