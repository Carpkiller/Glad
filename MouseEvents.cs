using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Glad
{
    public class MouseEvents
    {
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, UIntPtr dwExtraInfo);

        // ReSharper disable InconsistentNaming
        private const int MOUSEEVENTF_MOVE = 0x0001;
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        //private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        //private const int MOUSEEVENTF_RIGHTUP = 0x10;
        private const int MOUSEEVENTF_ABSOLUTE = 0x8000;

        private static void DoMouseClick(int x, int y)
        {
            mouse_event(MOUSEEVENTF_ABSOLUTE | MOUSEEVENTF_MOVE, x, y, 0, new UIntPtr());
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, new UIntPtr());
        }

        public static void MouseClick(int x, int y)
        {
            var screenBounds = Screen.PrimaryScreen.Bounds;
            DoMouseClick(x * 65535 / screenBounds.Width, y * 65535 / screenBounds.Height);
        }
    }
}