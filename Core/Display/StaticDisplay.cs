using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YANMFA.Core
{
    public partial class StaticDisplay : Form
    {

        internal static StaticDisplay Instance { get; private set; }

        /**
         * Skipped delta time.
         * Use this to make your Physics dependable on the framerate!
         */
        [Obsolete]
        public static double FixedDelta { get; private set; } = 1d;

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
        }

        private void StaticDisplay_Load(object sender, EventArgs e)
        {
            StaticDisplay_Resize(this, EventArgs.Empty);
            Task.Run(GameLoop);
        }

        private void GameLoop()
        {
            Action RefreshAction = () => Refresh();

            long lastUpdateTick = Environment.TickCount;
            long elapsedUpdateSteps = 0;

            long lastFpsTick = Environment.TickCount;
            int fpsCount = 0;

            while(Visible)
            {
                long startTick = Environment.TickCount;
                long elapsedTicks = startTick - lastUpdateTick;
                elapsedUpdateSteps += elapsedTicks;
                lastUpdateTick = startTick;

                long expectedMs = 1000 / FPSCap;
                while (elapsedUpdateSteps >= expectedMs)
                {
                    if (StaticEngine.IsGameRunning)
                        StaticEngine.CurrentGame.Update();
                    else // Update Splash-Screen
                        StaticEngine.CurrentGame.UpdateSplash();

                    StaticMouse.ResetCache(); // Reset delta values
                    StaticKeyboard.ResetCache(); // Reset delta values

                    // Goes back to GameMenu when game requested to stop
                    if (StaticEngine.CurrentGame.IsStopRequested())
                        StaticEngine.ChangeGame(null, GameMode.SINGLEPLAYER);
                    elapsedUpdateSteps -= expectedMs;
                }

                try
                {
                    Invoke(RefreshAction);

                    fpsCount++;
                    if (Environment.TickCount - lastFpsTick > 1000)
                    {
                        FPSCount = fpsCount;
                        lastFpsTick = Environment.TickCount;
                        fpsCount = 0;
                    }
                }
                catch (Exception) { }

                { // Syncronize
                    long endTime = startTick + 1; // 1000ms / 1000fps
                    while (Environment.TickCount < endTime)
                        Task.Delay(1);
                }
            }
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
        #endregion

        public static void AddResizeListener(EventHandler handler) => StaticDisplay.Instance.Resize += handler;
        public static void RemoveResizeListener(EventHandler handler) => StaticDisplay.Instance.Resize -= handler;

    }
}
