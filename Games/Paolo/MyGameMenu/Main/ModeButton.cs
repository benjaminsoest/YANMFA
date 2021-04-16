using System.Drawing;
using System.Windows.Forms;
using YANMFA.Core;

namespace YANMFA.Games.Paolo.MyGameMenu
{
    class ModeButton : GuiComponent
    {

        private static readonly Image SINGLEPLAYER_IMAGE = Image.FromFile("./Assets/Paolo/MyGameMenu/singleplayer.png");
        private static readonly Image MULTIPLAYER_IMAGE = Image.FromFile("./Assets/Paolo/MyGameMenu/multiplayer.png");
        private static readonly SolidBrush HOVER_COLOR = new SolidBrush(Color.FromArgb(125, 0, 0, 0));

        private Bitmap ResizedImage { get; set; }

        private readonly IGameInstance Game;
        private readonly GameMode GameMode;
        public ModeButton(IGameInstance game, GameMode mode)
        {
            Game = game;
            GameMode = mode;
        }

        public override void Render(Graphics g)
        {
            if (ResizedImage != null)
                g.DrawImage(ResizedImage, Bounds.X, Bounds.Y);
            if (Bounds.Contains(StaticMouse.MouseX, StaticMouse.MouseY))
                g.FillRectangle(HOVER_COLOR, Bounds);
            base.Render(g);
        }

        public override void MouseUp(MouseEventArgs e)
        {
            base.MouseUp(e);
            if (Bounds.Contains(StaticMouse.MouseX, StaticMouse.MouseY))
                StaticEngine.ChangeGame(Game, GameMode);
        }

        public override void SetBounds(int x, int y, int width, int height)
        {
            ResizedImage?.Dispose();
            if(GameMode == GameMode.SINGLEPLAYER)
                ResizedImage = new Bitmap(SINGLEPLAYER_IMAGE, new Size(width, height));
            else if(GameMode == GameMode.MULTIPLAYER)
                ResizedImage = new Bitmap(MULTIPLAYER_IMAGE, new Size(width, height));
            base.SetBounds(x, y, width, height);
        }

    }
}
