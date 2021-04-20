using System.Collections.Generic;
using System.Windows.Forms;

namespace YANMFA.Core
{
    public class StaticMouse
    {

        private static readonly HashSet<MouseButtons> TEMP_BUTTON_STATE_LIST = new HashSet<MouseButtons>();
        private static readonly HashSet<MouseButtons> CURR_BUTTON_STATE_LIST = new HashSet<MouseButtons>();

        /**
         * Display mouse position and delta wheel.
         */
        public static int MouseX { get; private set; }
        public static int MouseY { get; private set; }
        public static int DWheel { get; private set; }

        private StaticMouse() { }

        #region GameEngine region
        internal static void ResetCache()
        {
            TEMP_BUTTON_STATE_LIST.Clear();
            DWheel = 0;
        }

        internal static void InvokeMouseDownListener(MouseEventArgs e)
        {
            TEMP_BUTTON_STATE_LIST.Add(e.Button);
            CURR_BUTTON_STATE_LIST.Add(e.Button);
        }

        internal static void InvokeMouseUpListener(MouseEventArgs e) => CURR_BUTTON_STATE_LIST.Remove(e.Button);

        internal static void InvokeMouseMoveListener(MouseEventArgs e) => (MouseX, MouseY) = (e.Location.X, e.Location.Y);
        internal static void InvokeMouseWheelListener(MouseEventArgs e) => DWheel = e.Delta;
        #endregion

        public static bool WasButtonDown(MouseButtons button) => TEMP_BUTTON_STATE_LIST.Contains(button);
        public static bool IsButtonDown(MouseButtons button) => CURR_BUTTON_STATE_LIST.Contains(button);

        public static void AddMouseDownListener(MouseEventHandler handler) => StaticDisplay.Instance.MouseDown += handler;
        public static void AddMouseUpListener(MouseEventHandler handler) => StaticDisplay.Instance.MouseUp += handler;
        public static void AddMouseMoveListener(MouseEventHandler handler) => StaticDisplay.Instance.MouseMove += handler;
        public static void AddMouseWheelListener(MouseEventHandler handler) => StaticDisplay.Instance.MouseWheel += handler;
        public static void AddMouseClickListener(MouseEventHandler handler) => StaticDisplay.Instance.MouseClick += handler;
        public static void AddMouseDoubleClickListener(MouseEventHandler handler) => StaticDisplay.Instance.MouseDoubleClick += handler;

        public static void RemoveMouseDownListener(MouseEventHandler handler) => StaticDisplay.Instance.MouseDown -= handler;
        public static void RemoveMouseUpListener(MouseEventHandler handler) => StaticDisplay.Instance.MouseUp -= handler;
        public static void RemoveMouseMoveListener(MouseEventHandler handler) => StaticDisplay.Instance.MouseMove -= handler;
        public static void RemoveMouseWheelListener(MouseEventHandler handler) => StaticDisplay.Instance.MouseWheel -= handler;
        public static void RemoveMouseDoubleClickListener(MouseEventHandler handler) => StaticDisplay.Instance.MouseDoubleClick -= handler;

    }
}
