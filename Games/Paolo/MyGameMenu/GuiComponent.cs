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
            foreach (GuiComponent comp in this)
                comp.Show();
        }

        public virtual void Hide()
        {
            foreach (GuiComponent comp in this)
                comp.Hide();
        }

        public virtual void Update()
        {
            foreach (GuiComponent comp in this)
            {
                if(comp.Visible)
                    comp.Update();
            }
        }

        public virtual void Render(Graphics g)
        {
            foreach (GuiComponent comp in this)
            {
                if (comp.Visible)
                    comp.Render(g);
            }
        }

        public virtual void Resize(int width, int height)
        {
            foreach (GuiComponent comp in this)
                comp.Resize(width, height);
        }

        public virtual void MouseDown(MouseEventArgs e)
        {
            foreach (GuiComponent comp in this)
            {
                if(comp.Active)
                    comp.MouseDown(e);
            }
        }

        public virtual void MouseUp(MouseEventArgs e)
        {
            foreach (GuiComponent comp in this)
            {
                if (comp.Active)
                    comp.MouseUp(e);
            }
        }

        public virtual void MouseWheel(MouseEventArgs e)
        {
            foreach (GuiComponent comp in this)
            {
                if (comp.Active)
                    comp.MouseWheel(e);
            }
        }

        public virtual void KeyDown(KeyEventArgs e)
        {
            foreach (GuiComponent comp in this)
            {
                if (comp.Active)
                    comp.KeyDown(e);
            }
        }

        public virtual void KeyUp(KeyEventArgs e)
        {
            foreach (GuiComponent comp in this)
            {
                if (comp.Active)
                    comp.KeyUp(e);
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
