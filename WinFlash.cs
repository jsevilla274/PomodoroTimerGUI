using System;
using System.Runtime.InteropServices;

namespace PomodoroTimer
{
    /// <summary>
    /// Flash Window in Taskbar via Win32 FlashWindowEx
    /// </summary>
    /// Credit to Chris Pietschmann for this implementation, his breakdown found here
    /// https://pietschsoft.com/post/2009/01/26/csharp-flash-window-in-taskbar-via-win32-flashwindowex
    public static class WinFlash
    {
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

        [StructLayout(LayoutKind.Sequential)]
        public struct FLASHWINFO
        {
            /// <summary>
            /// The size of the structure in bytes.
            /// </summary>
            public uint cbSize;
            /// <summary>
            /// A Handle to the Window to be Flashed. The window can be either opened or minimized.
            /// </summary>
            public IntPtr hwnd;
            /// <summary>
            /// The Flash Status.
            /// </summary>
            public FlashWindowFlags dwFlags; //uint
            /// <summary>
            /// The number of times to Flash the window.
            /// </summary>
            public uint uCount;
            /// <summary>
            /// The rate at which the Window is to be flashed, in milliseconds. If Zero, the function uses the default cursor blink rate.
            /// </summary>
            public uint dwTimeout;
        }

        public enum FlashWindowFlags : uint
        {
            /// <summary>
            /// Stop flashing. The system restores the window to its original state.
            /// </summary>
            FLASHW_STOP = 0,

            /// <summary>
            /// Flash the window caption.
            /// </summary>
            FLASHW_CAPTION = 1,

            /// <summary>
            /// Flash the taskbar button.
            /// </summary>
            FLASHW_TRAY = 2,

            /// <summary>
            /// Flash both the window caption and taskbar button.
            /// This is equivalent to setting the FLASHW_CAPTION | FLASHW_TRAY flags.
            /// </summary>
            FLASHW_ALL = 3,

            /// <summary>
            /// Flash continuously, until the FLASHW_STOP flag is set.
            /// </summary>
            FLASHW_TIMER = 4,

            /// <summary>
            /// Flash continuously until the window comes to the foreground.
            /// </summary>
            FLASHW_TIMERNOFG = 12
        }

        /// <summary>
        /// Flashes window caption or taskbar
        /// </summary>
        /// <param name="hWnd">The handle of the window to be flashed</param>
        /// <param name="fOptions">The Flash Status</param>
        /// <param name="FlashCount">The number of times to Flash the window; default once;</param>
        /// <param name="FlashRate">
        /// The rate at which the Window is to be flashed, in milliseconds. 
        /// If Zero, the function uses the default cursor blink rate.
        /// </param>
        /// <returns>If the window needed flashing</returns>
        public static bool FlashWindow(IntPtr hWnd,
                                        FlashWindowFlags fOptions,
                                        uint FlashCount = 1,
                                        uint FlashRate = 0)
        {
            if (IntPtr.Zero != hWnd)
            {
                FLASHWINFO fi = new FLASHWINFO();
                fi.cbSize = (uint)Marshal.SizeOf(typeof(FLASHWINFO));
                fi.dwFlags = fOptions;
                fi.uCount = FlashCount;
                fi.dwTimeout = FlashRate;
                fi.hwnd = hWnd;

                return FlashWindowEx(ref fi);
            }
            return false;
        }

        /// <summary>
        /// Stop flashing the window
        /// </summary>
        // <param name = "hWnd" > The handle of the window to be flashed</param>
        /// <returns></returns>
        public static bool StopFlashingWindow(IntPtr hWnd)
        {
            if (IntPtr.Zero != hWnd)
            {
                FLASHWINFO fi = new FLASHWINFO();
                fi.cbSize = (uint)Marshal.SizeOf(typeof(FLASHWINFO));
                fi.dwFlags = (uint)FlashWindowFlags.FLASHW_STOP;
                fi.hwnd = hWnd;

                return FlashWindowEx(ref fi);
            }
            return false;
        }
    }
}
