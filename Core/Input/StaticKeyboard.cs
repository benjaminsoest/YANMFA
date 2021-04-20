using System.Collections.Generic;
using System.Windows.Forms;

namespace YANMFA.Core
{
    public class StaticKeyboard
    {

        private static readonly HashSet<Keys> TEMP_KEY_STATE_LIST = new HashSet<Keys>();
        private static readonly HashSet<Keys> CURR_KEY_STATE_LIST = new HashSet<Keys>();

        private StaticKeyboard() { }

        #region GameEngine region
        internal static void ResetCache() => TEMP_KEY_STATE_LIST.Clear();

        internal static void InvokeKeyDownListener(KeyEventArgs e)
        {
            TEMP_KEY_STATE_LIST.Add(e.KeyCode);
            CURR_KEY_STATE_LIST.Add(e.KeyCode);
        }

        internal static void InvokeKeyUpListener(KeyEventArgs e) => CURR_KEY_STATE_LIST.Remove(e.KeyCode);
        #endregion

        public static bool WasKeyDown(Keys key) => TEMP_KEY_STATE_LIST.Contains(key);
        public static bool IsKeyDown(Keys key) => CURR_KEY_STATE_LIST.Contains(key);

        public static void AddKeyDownListener(KeyEventHandler handler) => StaticDisplay.Instance.KeyDown += handler;
        public static void AddKeyUpListener(KeyEventHandler handler) => StaticDisplay.Instance.KeyUp += handler;

        public static void RemoveKeyDownListener(KeyEventHandler handler) => StaticDisplay.Instance.KeyDown -= handler;
        public static void RemoveKeyUpListener(KeyEventHandler handler) => StaticDisplay.Instance.KeyUp -= handler;

    }
}
