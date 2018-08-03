namespace Thinksea.Windows.Win32API
{
    /// <summary>
    /// 封装了对 Windows 库“gdi32.dll”的调用接口。
    /// </summary>
    public static class GDI32
    {
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        [System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        public static extern bool DeleteObject(System.IntPtr hdc);
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        [System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        public static extern bool GetTextMetrics(System.IntPtr hdc, out TEXTMETRIC lptm);
        [System.Runtime.InteropServices.DllImport("gdi32.dll", CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
        public static extern System.IntPtr SelectObject(System.IntPtr hdc, System.IntPtr hgdiobj);
    }
}
