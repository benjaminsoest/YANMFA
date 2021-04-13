using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace GDI_GameEngine.GameEngine
{
    public class StaticKeyboard
    {

        private static readonly HashSet<KeyEventHandler> KEY_DOWN_LISTENER = new HashSet<KeyEventHandler>();
        private static readonly HashSet<KeyEventHandler> KEY_UP_LISTENER = new HashSet<KeyEventHandler>();

        private static readonly HashSet<Keys> KEY_STATE_LIST = new HashSet<Keys>();

        private StaticKeyboard() { }

        #region GameEngine region
        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        public static void InvokeKeyDownListener(object sender, KeyEventArgs e)
        {
            foreach (KeyEventHandler handler in KEY_DOWN_LISTENER)
                handler?.Invoke(sender, e);
            KEY_STATE_LIST.Add(e.KeyCode);
        }

        [Obsolete("This method is reserved for the GameEngine. Don't use it!")]
        public static void InvokeKeyUpListener(object sender, KeyEventArgs e)
        {
            foreach (KeyEventHandler handler in KEY_UP_LISTENER)
                handler?.Invoke(sender, e);
            KEY_STATE_LIST.Remove(e.KeyCode);
        }
        #endregion

        public static bool IsKeyDown(Keys key) => KEY_STATE_LIST.Contains(key);

        public static void AddKeyDownListener(KeyEventHandler handler) => KEY_DOWN_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static void AddKeyUpListener(KeyEventHandler handler) => KEY_UP_LISTENER.Add(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveKeyDownListener(KeyEventHandler handler) => KEY_DOWN_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

        public static bool RemoveKeyUpListener(KeyEventHandler handler) => KEY_UP_LISTENER.Remove(handler ?? throw new ArgumentException("Parameter cannot be null", nameof(handler)));

    }
}
