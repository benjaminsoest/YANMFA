using YANMFA.Core;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace YANMFA.Games.Paolo.MyGameMenu
{
    class GameMenu : IGameInstance
    {

        private static readonly Image SplashImage = Image.FromFile("./Assets/Paolo/MyGameMenu/yanmfa-logo.png");

        public string GameName => "Game Menu";
        public string GameDescription => "A game selection menu, to start games";

        public GuiMainMenu GuiMainMenu { get; } = new GuiMainMenu()
        {
            Visible = true,
            Active = true
        };

        public GuiComponent CurrentScreen { get; private set; }

        public void Start()
        {
            StaticDisplay.AddResizeListener(Resize);

            StaticMouse.AddMouseDownListener(MouseDown);
            StaticMouse.AddMouseUpListener(MouseUp);
            StaticMouse.AddMouseWheelListener(MouseWheel);

            StaticKeyboard.AddKeyDownListener(KeyDown);
            StaticKeyboard.AddKeyUpListener(KeyUp);
            ChangeScreen(GuiMainMenu);
        }

        public void Stop()
        {
            ChangeScreen(null);
            StaticDisplay.RemoveResizeListener(Resize);

            StaticMouse.RemoveMouseDownListener(MouseDown);
            StaticMouse.RemoveMouseUpListener(MouseUp);
            StaticMouse.RemoveMouseWheelListener(MouseWheel);

            StaticKeyboard.RemoveKeyDownListener(KeyDown);
            StaticKeyboard.RemoveKeyUpListener(KeyUp);
        }

        public void UpdateSplash() { }
        public void RenderSplash(Graphics g)
        {
            lock(SplashImage) // Assure that only this thread uses SplashImage (when rendered)
                g.DrawImage(SplashImage, 0f, 0f, StaticDisplay.DisplayWidth, StaticDisplay.DisplayHeight);
        }

        public void Update() => CurrentScreen?.Update();
        public void Render(Graphics g) => CurrentScreen?.Render(g);

        private void Resize(object sender, EventArgs e) => CurrentScreen?.Resize(StaticDisplay.DisplayWidth, StaticDisplay.DisplayHeight);

        private void MouseDown(object sender, MouseEventArgs e) => CurrentScreen?.MouseDown(e);
        private void MouseUp(object sender, MouseEventArgs e) => CurrentScreen?.MouseUp(e);
        private void MouseWheel(object sender, MouseEventArgs e) => CurrentScreen?.MouseWheel(e);

        private void KeyDown(object sender, KeyEventArgs e) => CurrentScreen?.KeyDown(e);
        private void KeyUp(object sender, KeyEventArgs e) => CurrentScreen?.KeyUp(e);

        public bool IsStopRequested() => false;
        public Image GetTitleImage() => SplashImage;

        public void ChangeScreen(GuiComponent screen)
        {
            CurrentScreen?.Hide();
            CurrentScreen = screen;
            CurrentScreen?.Show();

            CurrentScreen?.SetBounds(0, 0, StaticDisplay.DisplayWidth, StaticDisplay.DisplayHeight);
        }

    }
}
