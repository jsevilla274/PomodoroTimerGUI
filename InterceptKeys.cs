using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace PomodoroTimerForm
{
    /// <summary>
    /// Low-Level Keyboard Hooks
    /// </summary>
    /// Credit to Stephen Toub for the majority of this implementation, the original found here
    /// https://web.archive.org/web/20190828074433/https://blogs.msdn.microsoft.com/toub/2006/05/03/low-level-keyboard-hook-in-c/
    public static class InterceptKeys
    {
        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
        private static LowLevelKeyboardProc _proc = HookCallback;
        private static IntPtr _hookID = IntPtr.Zero;

        public delegate void UserCallback(int keyCode);
        private static UserCallback _userCallback = null;

        /// <summary>
        /// Hooks the keyboard, capturing all keystrokes and passing the key codes to the supplied
        /// callback
        /// </summary>
        /// <param name="cb">The callback method that will received key codes</param>
        public static void Start(UserCallback cb)
        {
            Stop();
            _hookID = SetHook(_proc);
            _userCallback = cb;
        }

        private static IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        /// <summary>
        /// Stops capturing keystrokes and manually removes keypress hooks if necessary
        /// </summary>
        /// Note from Microsoft: "Hooks tend to slow down the system because they increase the 
        /// amount of processing the system must perform for each message. You should install a 
        /// hook only when necessary, and remove it as soon as possible."
        public static void Stop()
        {
            if (_hookID != IntPtr.Zero)
            {
                UnhookWindowsHookEx(_hookID);
                _hookID = IntPtr.Zero;
                _userCallback = null;
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                // Pass in the keycode to the user callback
                int vkCode = Marshal.ReadInt32(lParam);
                _userCallback?.Invoke(vkCode);
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook, LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);
    }
}
