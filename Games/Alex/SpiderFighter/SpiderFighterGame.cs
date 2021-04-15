using System;
using System.Drawing;
using System.Windows.Forms;
using YANMFA.Core;

namespace YANMFA.Games.Alex.SpiderFighter
{
    public sealed class SpiderFighterGame : IGameInstance
    {
        private static readonly Image SplashImage = Image.FromFile("./Assets/Paolo/MyGameMenu/yanmfa-logo.png");
        
        public string GameName => "Spider Fighter";

        public string GameDescription =>
            "I know it you hate them as well! Spiders ! Play Spider Fighter today and lose your friends and spiders";
        public void Start()
        {
            Round.CurrentLevel = new Models.Level();
            StaticDisplay.AddResizeListener(Resize);
            StaticMouse.AddMouseDownListener(MouseDown);
            StaticKeyboard.AddKeyDownListener(KeyDown);
        }

        public void Stop()
        {
            StaticDisplay.RemoveResizeListener(Resize);
            StaticMouse.RemoveMouseDownListener(MouseDown);
            StaticKeyboard.RemoveKeyDownListener(KeyDown);
        }

        public void UpdateSplash(){ }
        
        public void RenderSplash(Graphics g) => g.DrawImage(SplashImage, 0f, 0f, StaticDisplay.DisplayWidth, StaticDisplay.DisplayHeight);
        
        public void Update() => Round.Update();
        
        public void Render(Graphics g) => Round.Render(g);
        
        public bool IsStopRequested() => false;

        public Image GetTitleImage() => SplashImage;
        
        
        private void Resize(object sender, EventArgs e) => Round.Resize(StaticDisplay.DisplayWidth, StaticDisplay.DisplayHeight);
        private void MouseDown(object sender, MouseEventArgs e) => Round.MouseDown(e);
        private void KeyDown(object sender, KeyEventArgs e) => Round.KeyDown(e);
    }
}