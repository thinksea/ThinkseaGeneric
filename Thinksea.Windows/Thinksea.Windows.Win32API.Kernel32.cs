namespace Thinksea.Windows.Win32API
{
    /// <summary>
    /// 封装了对 Windows 库“Kernel32.dll”的调用接口。
    /// </summary>
    public static class Kernel32
    {
        [System.Runtime.InteropServices.DllImport("Kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetCurrentThreadId();

        /// <summary>
        /// 取得模块句柄
        /// </summary>
        /// <param name="lpModuleName">模块名称。</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        private static extern System.IntPtr GetModuleHandle(string lpModuleName);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern int VirtualAllocEx(System.IntPtr hProcess, int lpAddress, int dwSize, int flAllocationType, int flProtect);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, ExactSpelling = true)]
        public static extern bool VirtualFreeEx(System.IntPtr hProcess, int lpAddress, int dwSize, uint dwFreeType);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool CloseHandle(System.IntPtr hHandle);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        public static extern int ReadProcessMemory(System.IntPtr hProcess, int lpBaseAddress, System.IntPtr lpBuffer, int nSize, out int lpNumberOfBytesRead);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true, PreserveSig = true)]
        public static extern int ReadProcessMemory(System.IntPtr hProcess, int lpBaseAddress, byte[] lpBuffer, int nSize, out int lpNumberOfBytesRead);

        /// <summary>
        /// 打开一个已存在的进程对象，并返回进程的句柄。
        /// </summary>
        /// <param name="dwDesiredAccess">渴望得到的访问权限（标志）</param>
        /// <param name="bInheritHandle">是否继承句柄</param>
        /// <param name="dwProcessId">进程标示符</param>
        /// <returns>如成功，返回值为指定进程的句柄。如失败，返回值为空，可调用GetLastError获得错误代码。</returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern System.IntPtr OpenProcess(System.UInt32 dwDesiredAccess, System.Int32 bInheritHandle, System.UInt32 dwProcessId);

        /// <summary>
        /// 打开一个已存在的进程对象，并返回进程的句柄。
        /// </summary>
        /// <param name="dwDesiredAccess">渴望得到的访问权限（标志）</param>
        /// <param name="bInheritHandle">是否继承句柄</param>
        /// <param name="dwProcessId">进程标示符</param>
        /// <returns>如成功，返回值为指定进程的句柄。如失败，返回值为空，可调用GetLastError获得错误代码。</returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern System.IntPtr OpenProcess(ProcessAccessFlags dwDesiredAccess, bool bInheritHandle, System.UInt32 dwProcessId);

        [System.Runtime.InteropServices.DllImport("psapi.dll", SetLastError = true)]
        public static extern int GetProcessImageFileName(System.IntPtr hProcess, System.Text.StringBuilder lpImageFileName, int nSize);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern int QueryDosDevice(string lpDeviceName, System.Text.StringBuilder lpTargetPath, int ucchMax);

        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool GetExitCodeProcess(System.IntPtr hProcess, ref int lpExitCode);

        /// <summary>
        /// 该函数返回调用线程最近的错误代码值，错误代码以单线程为基础来维护的，多线程不重写各自的错误代码值。
        /// </summary>
        /// <returns>
        /// 返回值为调用的线程的错误代码值(unsigned long)，函数通过调 SetLastError 函数来设置此值，每个函数资料的返回值部分都注释了函数设置错误代码的情况。
        /// 在 Windows 95 和 Windows 98 中因为 SetLastError 仅是 32 位的函数，实际上以 16 位代码来操作的 Win32 不能设置错误代码值，应当在调用这些函数时忽略错误代码。它们包括窗口管理函数，GDI 函数和 Multimedia（多媒体）函数。
        /// </returns>
        /// <remarks>
        /// GetLastError返回的值通过在api函数中调用SetLastError或SetLastErrorEx设置。
        /// 函数并无必要设置上一次错误信息，所以即使一次GetLastError调用返回的是零值，也不能担保函数已成功执行。
        /// 只有在函数调用返回一个错误结果时，这个函数指出的错误结果才是有效的。
        /// 通常，只有在函数返回一个错误结果，而且已知函数会设置GetLastError变量的前提下，才应访问GetLastError；这时能保证获得有效的结果。
        /// SetLastError函数主要在对api函数进行模拟的dll函数中使用，所以对vb应用程序来说是没有意义的。
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", SetLastError = true)]
        public static extern long GetLastError();
    }
}
