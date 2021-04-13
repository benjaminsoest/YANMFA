using GDI_GameEngine.GameEngine;
using System;
using System.Windows.Forms;

namespace GDI_GameEngine
{
    public partial class Display : Form
    {

        public Display()
        {
            InitializeComponent();
            Display_Resize(this, EventArgs.Empty);
        }

        private void Display_Paint(object sender, PaintEventArgs e)
        {
            if (StaticEngine.IsGameRunning)
                StaticEngine.CurrentGame.Render(e.Graphics);
            else // Render Splash-Screen
                StaticEngine.CurrentGame.RenderSplash(e.Graphics);
        }

        private void tmrGameUpdate_Tick(object sender, EventArgs e)
        {
            tmrGameUpdate.Interval = Math.Max(1, 1000 / StaticDisplay.FPSCap);

            StaticDisplay.Begin();
            {
                if (StaticEngine.IsGameRunning)
                    StaticEngine.CurrentGame.Update();
                else // Update Splash-Screen
                    StaticEngine.CurrentGame.UpdateSplash();
                Refresh();
            }
            StaticDisplay.End();
            StaticMouse.ResetDelta(); // Reset delta values

            // Goes back to GameMenu when game requested to stop
            if (StaticEngine.CurrentGame.IsStopRequested())
                StaticEngine.ChangeGame(null);
        }

        private void Display_Resize(object sender, EventArgs e)
        {
            StaticDisplay.InvokeResizeListener(ClientSize, sender, e);
            tmrGameUpdate_Tick(this, EventArgs.Empty); // Update Game when Resizing
        }

        private void Display_MouseDown(object sender, MouseEventArgs e) => StaticMouse.InvokeMouseDownListener(sender, e);
        private void Display_MouseMove(object sender, MouseEventArgs e) => StaticMouse.InvokeMouseMoveListener(sender, e);
        private void Display_MouseUp(object sender, MouseEventArgs e) => StaticMouse.InvokeMouseUpListener(sender, e);
        private void Display_MouseWheel(object sender, MouseEventArgs e) => StaticMouse.InvokeMouseWheelListener(sender, e);

        private void Display_KeyDown(object sender, KeyEventArgs e) => StaticKeyboard.InvokeKeyDownListener(sender, e);
        private void Display_KeyUp(object sender, KeyEventArgs e) => StaticKeyboard.InvokeKeyUpListener(sender, e);

    }
}
