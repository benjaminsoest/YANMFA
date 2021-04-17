using System.Drawing;
using System.Windows.Forms;

namespace YANMFA.Games.Alex.SpiderFighter.Models.Mobs
{
    public class Mob
    {
        private RectangleF _hitbox;
        private Bitmap _currentTexture;
        
        public int Healthpoints { get; set; }
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

        public virtual void Update(){}
        public virtual void Render(Graphics g){}
        public virtual void Resize(int width, int height){}
        public virtual void KeyDown(KeyEventArgs e){}
        public virtual void MouseDown(MouseEventArgs e){}

        public void DeleteMob() => Round.CurrentLevel.Mobs.Remove(this);

        public void AddMob() => Round.CurrentLevel.Mobs.Add(this);

    }
}