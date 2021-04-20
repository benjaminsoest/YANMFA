using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace YANMFA.Core
{
    public partial class StaticDisplay : Form
    {

        internal static StaticDisplay Instance { get; private set; }

        private static readonly Stopwatch Stopwatch = new Stopwatch();

        /**
         * Skipped delta time.
         * Use this to make your Physics dependable on the framerate!
         */
        public static double FixedDelta { get; private set; }

        /**
         * Current framerate
         */
        public static int FPSCount { get; private set; }

        /**
         * Current window size
         */
        public static int DisplayWidth { get; private set; }
        public static int DisplayHeight { get; private set; }

        /**
         * Maximum framerate
         */
        private static int _fpsCap = 60;
        public static int FPSCap
        { // Frames capped from 1 to 1000
            get { return _fpsCap; }
            set { _fpsCap = Math.Max(1, Math.Min(value, 1000)); }
        }

        public StaticDisplay()
        {
            Instance = this;
            InitializeComponent();
            StaticDisplay_Resize(this, EventArgs.Empty);
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

            StaticMouse.ResetCache(); // Reset delta values
            StaticKeyboard.ResetCache(); // Reset delta values

            // Goes back to GameMenu when game requested to stop
            if (StaticEngine.CurrentGame.IsStopRequested())
                StaticEngine.ChangeGame(null, GameMode.SINGLEPLAYER);
        }

        private void StaticDisplay_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.Default;
            if (StaticEngine.IsGameRunning)
                StaticEngine.CurrentGame.Render(e.Graphics);
            else // Render Splash-Screen
                StaticEngine.CurrentGame.RenderSplash(e.Graphics);
        }

        private void StaticDisplay_MouseDown(object sender, MouseEventArgs e) => StaticMouse.InvokeMouseDownListener(e);
        private void StaticDisplay_MouseUp(object sender, MouseEventArgs e) => StaticMouse.InvokeMouseUpListener(e);
        private void StaticDisplay_MouseMove(object sender, MouseEventArgs e) => StaticMouse.InvokeMouseMoveListener(e);
        private void StaticDisplay_MouseWheel(object sender, MouseEventArgs e) => StaticMouse.InvokeMouseWheelListener(e);

        private void StaticDisplay_KeyDown(object sender, KeyEventArgs e) => StaticKeyboard.InvokeKeyDownListener(e);
        private void StaticDisplay_KeyUp(object sender, KeyEventArgs e) => StaticKeyboard.InvokeKeyUpListener(e);

        private void StaticDisplay_Resize(object sender, EventArgs e)
        {
            StaticDisplay.InvokeResizeListener(ClientSize);
            Refresh(); // Refresh screen when resizing
        }

		#region GameEngine region
        internal static void InvokeResizeListener(Size size) => (DisplayWidth, DisplayHeight) = (size.Width, size.Height);

        internal static void Begin() => Stopwatch.Restart();

        internal static void End()
        {
            double expected = 1000d / FPSCap;
            double minElapsedTime = Stopwatch.ElapsedMilliseconds + expected;

            FixedDelta = minElapsedTime / expected;
            FPSCount = (int)(1000d / minElapsedTime);
        }
        #endregion

        public static void AddResizeListener(EventHandler handler) => StaticDisplay.Instance.Resize += handler;
        public static void RemoveResizeListener(EventHandler handler) => StaticDisplay.Instance.Resize -= handler;

    }
}
