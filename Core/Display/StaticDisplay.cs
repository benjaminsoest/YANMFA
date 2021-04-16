using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace YANMFA.Core
{
    public partial class StaticDisplay : Form
    {

        private static readonly List<EventHandler> RESIZE_LISTENER = new List<EventHandler>();
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
                StaticEngine.ChangeGame(null, GameMode.SINGLEPLAYER);
        }

        private void Display_Resize(object sender, EventArgs e)
        {
            StaticDisplay.InvokeResizeListener(ClientSize, sender, e);
            Refresh(); // Refresh screen when resizing
        }

        private void Display_MouseDown(object sender, MouseEventArgs e) => StaticMouse.InvokeMouseDownListener(sender, e);
        private void Display_MouseMove(object sender, MouseEventArgs e) => StaticMouse.InvokeMouseMoveListener(sender, e);
        private void Display_MouseUp(object sender, MouseEventArgs e) => StaticMouse.InvokeMouseUpListener(sender, e);
        private void Display_MouseWheel(object sender, MouseEventArgs e) => StaticMouse.InvokeMouseWheelListener(sender, e);

        private void Display_KeyDown(object sender, KeyEventArgs e) => StaticKeyboard.InvokeKeyDownListener(sender, e);
        private void Display_KeyUp(object sender, KeyEventArgs e) => StaticKeyboard.InvokeKeyUpListener(sender, e);

        #region GameEngine region
        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        static void InvokeResizeListener(Size size, object sender, EventArgs e)
        {
            DisplayWidth = size.Width;
            DisplayHeight = size.Height;
            for (int i = RESIZE_LISTENER.Count - 1; i >= 0; i--)
                RESIZE_LISTENER[i]?.Invoke(sender, e);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        static void Begin() => Stopwatch.Restart();

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        static void End()
        {
            double expected = 1000d / FPSCap;
            double minElapsedTime = Stopwatch.ElapsedMilliseconds + expected;

            FixedDelta = minElapsedTime / expected;
            FPSCount = (int)(1000d / minElapsedTime);
        }
        #endregion

        public static void AddResizeListener(EventHandler handler)
        {
            if(!RESIZE_LISTENER.Contains(handler))
                RESIZE_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));
        }

        public static bool RemoveResizeListener(EventHandler handler) => RESIZE_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

    }
}
