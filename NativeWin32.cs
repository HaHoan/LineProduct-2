using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Line_Production
{
    public static class NativeWin32
    {
        public const int GW_CHILD = 5;
        public const int GW_HWNDNEXT = 2;
        [DllImport("user32.dll")]
        public static extern int FindWindow(
        string lpClassName, // class name 
        string lpWindowName // window name 
    );
        [DllImport("user32.dll")]
        public static extern int SetForegroundWindow(
          int hWnd // handle to window
          );
        [DllImport("user32.dll")]
        public static extern IntPtr SetForegroundWindow(
                    IntPtr hWnd // handle to window
                    );
        [DllImport("user32")]
        public static extern int GetDesktopWindow();
        [DllImport("user32")]
        public static extern int GetWindow(int hwnd, int wCmd);
        [DllImport("user32")]
        public static extern int IsWindowVisible(int hwnd);
        [DllImport("User32.Dll")]
        public static extern void GetWindowText(int h, StringBuilder s, int nMaxCount);
    }
}
