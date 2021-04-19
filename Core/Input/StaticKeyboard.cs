using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace YANMFA.Core
{
    public class StaticKeyboard
    {

        private static readonly List<KeyEventHandler> KEY_DOWN_LISTENER = new List<KeyEventHandler>();
        private static readonly List<KeyEventHandler> KEY_UP_LISTENER = new List<KeyEventHandler>();
        private static readonly List<KeyPressEventHandler> KEY_PRESS_LISTENER = new List<KeyPressEventHandler>();

        private static readonly HashSet<Keys> KEY_STATE_LIST = new HashSet<Keys>();
        public static HashSet<char> PRESSED_KEYS = new HashSet<char>();

        private StaticKeyboard() { }

        #region GameEngine region
        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        internal static void InvokeKeyDownListener(object sender, KeyEventArgs e)
        {
            for (int i = KEY_DOWN_LISTENER.Count - 1; i >= 0; i--)
                KEY_DOWN_LISTENER[i]?.Invoke(sender, e);
            KEY_STATE_LIST.Add(e.KeyCode);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        internal static void InvokeKeyUpListener(object sender, KeyEventArgs e)
        {
            for (int i = KEY_UP_LISTENER.Count - 1; i >= 0; i--)
                KEY_UP_LISTENER[i]?.Invoke(sender, e);
            KEY_STATE_LIST.Remove(e.KeyCode);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        internal static void InvokeKeyPressListener(object sender, KeyPressEventArgs e)
        {
            for (int i = KEY_PRESS_LISTENER.Count - 1; i >= 0; i--)
                KEY_PRESS_LISTENER[i]?.Invoke(sender, e);
        }
        #endregion

        public static bool IsKeyDown(Keys key) => KEY_STATE_LIST.Contains(key);
        public static bool WasKeyPressed(char keyChar) => PRESSED_KEYS.Contains(keyChar);

        public static void AddKeyDownListener(KeyEventHandler handler)
        {
            if(!KEY_DOWN_LISTENER.Contains(handler))
                KEY_DOWN_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));
        }

        public static void AddKeyUpListener(KeyEventHandler handler)
        {
            if (!KEY_UP_LISTENER.Contains(handler))
                KEY_UP_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));
        }

        public static void AddKeyPressListener(KeyPressEventHandler handler)
        {
            if (!KEY_PRESS_LISTENER.Contains(handler))
                KEY_PRESS_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));
        }

        public static bool RemoveKeyDownListener(KeyEventHandler handler) => KEY_DOWN_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveKeyUpListener(KeyEventHandler handler) => KEY_UP_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveKeyPressListener(KeyPressEventHandler handler) => KEY_PRESS_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

    }
}
