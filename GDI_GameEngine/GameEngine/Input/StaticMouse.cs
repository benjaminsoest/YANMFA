using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GDI_GameEngine.GameEngine
{
    public class StaticMouse
    {

        private static readonly HashSet<MouseEventHandler> MOUSE_DOWN_LISTENER = new HashSet<MouseEventHandler>();
        private static readonly HashSet<MouseEventHandler> MOUSE_UP_LISTENER = new HashSet<MouseEventHandler>();
        private static readonly HashSet<MouseEventHandler> MOUSE_MOVE_LISTENER = new HashSet<MouseEventHandler>();
        private static readonly HashSet<MouseEventHandler> MOUSE_WHEEL_LISTENER = new HashSet<MouseEventHandler>();

        private static readonly HashSet<MouseButtons> BUTTON_STATE_LIST = new HashSet<MouseButtons>();

        /**
         * Display mouse position and delta wheel.
         */
        public static int MouseX { get; private set; }
        public static int MouseY { get; private set; }
        public static int DWheel { get; private set; }

        private StaticMouse() { }

        #region GameEngine region
        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        public static void ResetDelta() => DWheel = 0;

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        public static void InvokeMouseDownListener(object sender, MouseEventArgs e)
        {
            foreach (MouseEventHandler handler in MOUSE_DOWN_LISTENER)
                handler?.Invoke(sender, e);
            BUTTON_STATE_LIST.Add(e.Button);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        public static void InvokeMouseUpListener(object sender, MouseEventArgs e)
        {
            foreach (MouseEventHandler handler in MOUSE_UP_LISTENER)
                handler?.Invoke(sender, e);
            BUTTON_STATE_LIST.Remove(e.Button);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        public static void InvokeMouseMoveListener(object sender, MouseEventArgs e)
        {
            MouseX = e.Location.X;
            MouseY = e.Location.Y;
            foreach (MouseEventHandler handler in MOUSE_MOVE_LISTENER)
                handler?.Invoke(sender, e);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        public static void InvokeMouseWheelListener(object sender, MouseEventArgs e)
        {
            DWheel = e.Delta;
            foreach (MouseEventHandler handler in MOUSE_WHEEL_LISTENER)
                handler?.Invoke(sender, e);
        }
        #endregion

        public static bool IsButtonDown(MouseButtons button) => BUTTON_STATE_LIST.Contains(button);

        public static void AddMouseDownListener(MouseEventHandler handler) => MOUSE_DOWN_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static void AddMouseUpListener(MouseEventHandler handler) => MOUSE_UP_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static void AddMouseMoveListener(MouseEventHandler handler) => MOUSE_MOVE_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static void AddMouseWheelListener(MouseEventHandler handler) => MOUSE_WHEEL_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveMouseDownListener(MouseEventHandler handler) => MOUSE_DOWN_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveMouseUpListener(MouseEventHandler handler) => MOUSE_UP_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveMouseMoveListener(MouseEventHandler handler) => MOUSE_MOVE_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveMouseWheelListener(MouseEventHandler handler) => MOUSE_WHEEL_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

    }
}
