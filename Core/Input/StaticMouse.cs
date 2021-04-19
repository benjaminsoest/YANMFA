using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YANMFA.Core
{
    public class StaticMouse
    {

        private static readonly List<MouseEventHandler> MOUSE_DOWN_LISTENER = new List<MouseEventHandler>();
        private static readonly List<MouseEventHandler> MOUSE_UP_LISTENER = new List<MouseEventHandler>();
        private static readonly List<MouseEventHandler> MOUSE_MOVE_LISTENER = new List<MouseEventHandler>();
        private static readonly List<MouseEventHandler> MOUSE_CLICK_LISTENER = new List<MouseEventHandler>();
        private static readonly List<MouseEventHandler> MOUSE_DOUBLE_CLICK_LISTENER = new List<MouseEventHandler>();
        private static readonly List<MouseEventHandler> MOUSE_WHEEL_LISTENER = new List<MouseEventHandler>();

        private static readonly HashSet<MouseButtons> BUTTON_STATE_LIST = new HashSet<MouseButtons>();
        public static HashSet<MouseButtons> PRESSED_MOUSE_BUTTONS = new HashSet<MouseButtons>();

        /**
         * Display mouse position and delta wheel.
         */
        public static int MouseX { get; private set; }
        public static int MouseY { get; private set; }
        public static int DWheel { get; private set; }

        private StaticMouse() { }

        #region GameEngine region
        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        internal static void ResetDelta() => DWheel = 0;

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        internal static void InvokeMouseDownListener(object sender, MouseEventArgs e)
        {
            for (int i = MOUSE_DOWN_LISTENER.Count - 1; i >= 0; i--)
                MOUSE_DOWN_LISTENER[i]?.Invoke(sender, e);
            BUTTON_STATE_LIST.Add(e.Button);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        internal static void InvokeMouseUpListener(object sender, MouseEventArgs e)
        {
            for (int i = MOUSE_UP_LISTENER.Count - 1; i >= 0; i--)
                MOUSE_UP_LISTENER[i]?.Invoke(sender, e);
            BUTTON_STATE_LIST.Remove(e.Button);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        internal static void InvokeMouseMoveListener(object sender, MouseEventArgs e)
        {
            MouseX = e.Location.X;
            MouseY = e.Location.Y;
            for (int i = MOUSE_MOVE_LISTENER.Count - 1; i >= 0; i--)
                MOUSE_MOVE_LISTENER[i]?.Invoke(sender, e);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        internal static void InvokeMouseWheelListener(object sender, MouseEventArgs e)
        {
            DWheel = e.Delta;
            for (int i = MOUSE_WHEEL_LISTENER.Count - 1; i >= 0; i--)
                MOUSE_WHEEL_LISTENER[i]?.Invoke(sender, e);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        internal static void InvokeMouseClickListener(object sender, MouseEventArgs e)
        {
            for (int i = MOUSE_CLICK_LISTENER.Count - 1; i >= 0; i--)
                MOUSE_CLICK_LISTENER[i]?.Invoke(sender, e);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        internal static void InvokeMouseDoubleClickListener(object sender, MouseEventArgs e)
        {
            for (int i = MOUSE_DOUBLE_CLICK_LISTENER.Count - 1; i >= 0; i--)
                MOUSE_DOUBLE_CLICK_LISTENER[i]?.Invoke(sender, e);
        }
        #endregion

        public static bool IsButtonDown(MouseButtons button) => BUTTON_STATE_LIST.Contains(button);
        public static bool WasButtonPressed(MouseButtons button) => PRESSED_MOUSE_BUTTONS.Contains(button);

        public static void AddMouseDownListener(MouseEventHandler handler)
        {
            if(!MOUSE_DOWN_LISTENER.Contains(handler))
                MOUSE_DOWN_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));
        }

        public static void AddMouseUpListener(MouseEventHandler handler)
        {
            if (!MOUSE_UP_LISTENER.Contains(handler))
                MOUSE_UP_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));
        }

        public static void AddMouseMoveListener(MouseEventHandler handler)
        {
            if (!MOUSE_MOVE_LISTENER.Contains(handler))
                MOUSE_MOVE_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));
        }

        public static void AddMouseWheelListener(MouseEventHandler handler)
        {
            if (!MOUSE_WHEEL_LISTENER.Contains(handler))
                MOUSE_WHEEL_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));
        }

        public static void AddMouseClickListener(MouseEventHandler handler)
        {
            if (!MOUSE_CLICK_LISTENER.Contains(handler))
                MOUSE_CLICK_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));
        }

        public static void AddMouseDoubleClickListener(MouseEventHandler handler)
        {
            if (!MOUSE_DOUBLE_CLICK_LISTENER.Contains(handler))
                MOUSE_DOUBLE_CLICK_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));
        }

        public static bool RemoveMouseDownListener(MouseEventHandler handler) => MOUSE_DOWN_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveMouseUpListener(MouseEventHandler handler) => MOUSE_UP_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveMouseMoveListener(MouseEventHandler handler) => MOUSE_MOVE_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveMouseWheelListener(MouseEventHandler handler) => MOUSE_WHEEL_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveMouseClickListener(MouseEventHandler handler) => MOUSE_CLICK_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveMouseDoubleClickListener(MouseEventHandler handler) => MOUSE_DOUBLE_CLICK_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

    }
}
