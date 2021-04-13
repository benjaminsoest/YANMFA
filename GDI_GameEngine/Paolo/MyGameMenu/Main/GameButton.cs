using GDI_GameEngine.GameEngine;
using System.Drawing;
using System.Windows.Forms;

namespace GDI_GameEngine.Paolo.MyGameMenu
{
    class GameButton : GuiComponent
    {

        private static readonly SolidBrush HOVER_COLOR = new SolidBrush(Color.FromArgb(125, 0, 0, 0));

        private Bitmap ResizedImage { get; set; }

        private readonly IGameInstance Game;
        public GameButton(IGameInstance game) => Game = game;

        public override void Render(Graphics g)
        {
            base.Render(g);
            g.DrawImage(ResizedImage, Bounds.X, Bounds.Y);
            if (Bounds.Contains(StaticMouse.MouseX, StaticMouse.MouseY))
                g.FillRectangle(HOVER_COLOR, Bounds);
        }

        public override void MouseUp(MouseEventArgs e)
        {
            base.MouseUp(e);
            if (Bounds.Contains(StaticMouse.MouseX, StaticMouse.MouseY))
                StaticEngine.ChangeGame(Game);
        }

        public override void SetBounds(int x, int y, int width, int height)
        {
            ResizedImage?.Dispose();
            ResizedImage = new Bitmap(Game.GetTitleImage(), new Size(width, height));
            base.SetBounds(x, y, width, height);
        }

    }
}
