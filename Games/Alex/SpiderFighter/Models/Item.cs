using System.Drawing;
using System.Windows.Forms;

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

    }
}