using System.Collections.Generic;
using System.Windows.Forms;

namespace YANMFA.Core
{
    public class StaticKeyboard
    {

        private static readonly HashSet<Keys> KEY_STATE_LIST = new HashSet<Keys>();

        private StaticKeyboard() { }

        #region GameEngine region
        internal static void InvokeKeyDownListener(KeyEventArgs e) => KEY_STATE_LIST.Add(e.KeyCode);
        internal static void InvokeKeyUpListener(KeyEventArgs e) => KEY_STATE_LIST.Remove(e.KeyCode);
        #endregion

        public static bool IsKeyDown(Keys key) => KEY_STATE_LIST.Contains(key);

        public static void AddKeyDownListener(KeyEventHandler handler) => StaticDisplay.Instance.KeyDown += handler;
        public static void AddKeyUpListener(KeyEventHandler handler) => StaticDisplay.Instance.KeyUp += handler;

        public static void RemoveKeyDownListener(KeyEventHandler handler) => StaticDisplay.Instance.KeyDown -= handler;
        public static void RemoveKeyUpListener(KeyEventHandler handler) => StaticDisplay.Instance.KeyUp -= handler;

    }
}
