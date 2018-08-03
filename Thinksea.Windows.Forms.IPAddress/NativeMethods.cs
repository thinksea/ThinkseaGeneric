using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace Thinksea.Windows.Forms.IPAddress
{
    internal class NativeMethods
    {
        // Methods
        private NativeMethods()
        {
        }

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        public static extern bool DeleteObject(IntPtr hdc);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        public static extern bool GetTextMetrics(IntPtr hdc, out TEXTMETRIC lptm);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("gdi32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

        // Nested Types
        [Serializable, StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct TEXTMETRIC
        {
            public int tmHeight;
            public int tmAscent;
            public int tmDescent;
            public int tmInternalLeading;
            public int tmExternalLeading;
            public int tmAveCharWidth;
            public int tmMaxCharWidth;
            public int tmWeight;
            public int tmOverhang;
            public int tmDigitizedAspectX;
            public int tmDigitizedAspectY;
            public char tmFirstChar;
            public char tmLastChar;
            public char tmDefaultChar;
            public char tmBreakChar;
            public byte tmItalic;
            public byte tmUnderlined;
            public byte tmStruckOut;
            public byte tmPitchAndFamily;
            public byte tmCharSet;
        }
    }

}
