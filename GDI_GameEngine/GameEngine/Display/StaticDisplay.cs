using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;

namespace GDI_GameEngine.GameEngine
{
    public class StaticDisplay
    {

        private static readonly HashSet<EventHandler> RESIZE_LISTENER = new HashSet<EventHandler>();
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

        private StaticDisplay() { }

        #region GameEngine region
        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        public static void InvokeResizeListener(Size size, object sender, EventArgs e)
        {
            DisplayWidth = size.Width;
            DisplayHeight = size.Height;
            foreach (EventHandler handler in RESIZE_LISTENER)
                handler?.Invoke(sender, e);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        public static void Begin() => Stopwatch.Restart();

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        public static void End()
        {
            double expected = 1000d / FPSCap;
            double minElapsedTime = Stopwatch.ElapsedMilliseconds + expected;

            FixedDelta = minElapsedTime / expected;
            FPSCount = (int) (1000d / minElapsedTime);
        }
        #endregion

        public static void AddResizeListener(EventHandler handler) => RESIZE_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveResizeListener(EventHandler handler) => RESIZE_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

    }
}
