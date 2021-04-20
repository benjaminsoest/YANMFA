using YANMFA.Core;
using System.Drawing;
using System.Windows.Forms;

namespace YANMFA.Games.Paolo.MyGameMenu
{
    class GameButton : GuiComponent
    {

        private static readonly SolidBrush HOVER_COLOR = new SolidBrush(Color.FromArgb(125, 0, 0, 0));

        private Bitmap ResizedImage { get; set; }

        private readonly GameMenu Instance;
        private readonly IGameInstance Game;
        public GameButton(GameMenu instance, IGameInstance game)
        {
            Instance = instance;
            Game = game;
        }

        public override void Render(Graphics g)
        {
            base.Render(g);
            if(ResizedImage != null)
                g.DrawImage(ResizedImage, Bounds.X, Bounds.Y);
            if (Bounds.Contains(StaticMouse.MouseX, StaticMouse.MouseY))
                g.FillRectangle(HOVER_COLOR, Bounds);
        }

        public override void MouseUp(MouseEventArgs e)
        {
            base.MouseUp(e);
            if (Bounds.Contains(StaticMouse.MouseX, StaticMouse.MouseY))
            {
                Instance.GuiModeMenu.Game = Game;
                Instance.ChangeScreen(Instance.GuiModeMenu);
            }
        }

        public override void SetBounds(int x, int y, int width, int height)
        {
            lock(Game.GetTitleImage())
            { // Assure that only this thread uses SplashImage (when resizing)
                ResizedImage?.Dispose();
                ResizedImage = new Bitmap(Game.GetTitleImage(), new Size(width, height));
            }

            base.SetBounds(x, y, width, height);
        }

    }
}
