using System.Drawing;
using System.Windows.Forms;
using YANMFA.Core;

namespace YANMFA.Games.Alex.SpiderFighter.Models.Decorations
{
    public class Door : Item
    {
        public bool OpenState { get; private set; }
        
        public Door(RectangleF rectangle, Bitmap bitmap)
        {
            Hitbox = new RectangleF(rectangle.X, rectangle.Y,rectangle.Width -60,rectangle.Height);
            CurrentTexture = bitmap;           
        }

        public override void Render(Graphics graphics)
        {
            graphics.DrawImage(CurrentTexture, Hitbox);
        }

        public override void Update()
        {            
            if (OpenState == false)
                MobResistance();       
        }

        public override void MouseDown(MouseEventArgs mouseEventArgs)
        {
        }

        public override void Resize(int width, int height)
        {

        }

        public override void KeyDown(KeyEventArgs g)
        {
            if ((Round.PlayerOne.Hitbox.X - Hitbox.X < 0 && Round.PlayerOne.Hitbox.X - Hitbox.X > -200) || Round.PlayerOne.Hitbox.X - Hitbox.X > 0 && Round.PlayerOne.Hitbox.X - Hitbox.X < 200)
            {
                if (g.KeyCode == Keys.E && OpenState == true)
                    CloseDoor();
                else if (g.KeyCode == Keys.E && OpenState == false)
                    OpenDoor();
            }
        }

        public void OpenDoor()
        {
            OpenState = true;

            Textures.ItemBitmaps.TryGetValue("Irondoor", out Bitmap b);
            {
                CurrentTexture = b;
                Hitbox = new RectangleF(Hitbox.X, Hitbox.Y, Hitbox.Width + 60, Hitbox.Height);
            }
                               
        }

        public void CloseDoor()
        {
            OpenState = false;

            Textures.ItemBitmaps.TryGetValue("IrondoorClose", out Bitmap b);
            {
                CurrentTexture = b;
                Hitbox = new RectangleF(Hitbox.X, Hitbox.Y, Hitbox.Width - 60, Hitbox.Height);
            }

        }
    }
}