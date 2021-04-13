using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace YANMFA.Games.Paolo.MyGameMenu
{
    class GuiComponent : List<GuiComponent>
    {

        public Rectangle Bounds { get; private set; }

        public bool Visible { get; set; }
        public bool Active { get; set; }

        public GuiComponent() { }

        public virtual void Show()
        {
            for (int i = Count - 1; i >= 0; i--)
                this[i].Show();
        }

        public virtual void Hide()
        {
            for (int i = Count - 1; i >= 0; i--)
                this[i].Hide();
        }

        public virtual void Update()
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if(this[i].Visible)
                    this[i].Update();
            }
        }

        public virtual void Render(Graphics g)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (this[i].Visible)
                    this[i].Render(g);
            }
        }

        public virtual void Resize(int width, int height)
        {
            for (int i = Count - 1; i >= 0; i--)
                this[i].Resize(width, height);
        }

        public virtual void MouseDown(MouseEventArgs e)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if(this[i].Active)
                    this[i].MouseDown(e);
            }
        }

        public virtual void MouseUp(MouseEventArgs e)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (this[i].Active)
                    this[i].MouseUp(e);
            }
        }

        public virtual void MouseWheel(MouseEventArgs e)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (this[i].Active)
                    this[i].MouseWheel(e);
            }
        }

        public virtual void KeyDown(KeyEventArgs e)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (this[i].Active)
                    this[i].KeyDown(e);
            }
        }

        public virtual void KeyUp(KeyEventArgs e)
        {
            for (int i = Count - 1; i >= 0; i--)
            {
                if (this[i].Active)
                    this[i].KeyUp(e);
            }
        }

        public virtual void SetBounds(Rectangle bounds)
        {
            SetBounds(bounds.X, bounds.Y, bounds.Width, bounds.Height);
        }

        public virtual void SetBounds(int x, int y, int width, int height)
        {
            Bounds = new Rectangle(x, y, width, height);
        }

    }
}
