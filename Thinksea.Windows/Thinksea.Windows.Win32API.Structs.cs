namespace Thinksea.Windows.Win32API
{
    /// <summary>
    /// 键盘 Hook 结构函数
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class KeyBoardHookStruct
    {
        public int vkCode;
        public int scanCode;
        public int flags;
        public int time;
        public int dwExtraInfo;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, Pack = 1)]
    public class TBBUTTON
    {
        /// <summary>
        /// 按钮使用的位图编号
        /// </summary>
        public int iBitmap;
        /// <summary>
        /// 按钮按下时在WM_COMMAND中使用的ID
        /// </summary>
        public int idCommand;
        /// <summary>
        /// 按钮状态
        /// </summary>
        public byte fsState;
        /// <summary>
        /// 按钮风格
        /// </summary>
        public byte fsStyle;
        public byte bReserved0;
        public byte bReserved1;
        /// <summary>
        /// 自定义数据
        /// </summary>
        public int dwData;
        /// <summary>
        /// 按钮字符串索引
        /// </summary>
        public int iString;
    }

    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public class TRAYDATA
    {
        public System.IntPtr hwnd;
        public uint uID;
        public uint uCallbackMessage;
        public int Reserved0;
        public int Reserved1;
        /// <summary>
        /// 托盘图标的句柄
        /// </summary>
        public System.IntPtr hIcon;
    }

    /// <summary>
    /// 定义文件信息描述类型。
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential)]
    public struct SHFILEINFO
    {
        /// <summary>
        /// 文件的图标句柄
        /// </summary>
        public System.IntPtr hIcon;
        /// <summary>
        /// 图标的系统索引号
        /// </summary>
        public int iIcon;
        /// <summary>
        /// 文件的属性值
        /// </summary>
        public uint dwAttributes;
        /// <summary>
        /// 文件的显示名
        /// </summary>
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 260)]
        public string szDisplayName;
        /// <summary>
        /// 文件的类型名
        /// </summary>
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.ByValTStr, SizeConst = 80)]
        public string szTypeName;
    }

    [System.Serializable, System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Unicode)]
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
