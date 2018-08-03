namespace Thinksea.Windows.Win32API
{
    /// <summary>
    /// 定义窗口尺寸和定位的标志。
    /// </summary>
	[System.Flags]
	public enum FlagsSetWindowPos : uint
	{
        /// <summary>
        /// 维持当前尺寸（忽略 Width 和 Height 参数）。
        /// </summary>
		SWP_NOSIZE          = 0x0001,
        /// <summary>
        /// 维持当前位置（忽略X和Y参数）。
        /// </summary>
		SWP_NOMOVE          = 0x0002,
        /// <summary>
        /// 维持当前Z序（忽略hWndlnsertAfter参数）。
        /// </summary>
		SWP_NOZORDER        = 0x0004,
        /// <summary>
        /// 不重画改变的内容。如果设置了这个标志，则不发生任何重画动作。适用于客户区和非客户区（包括标题栏和滚动条）和任何由于窗回移动而露出的父窗口的所有部分。如果设置了这个标志，应用程序必须明确地使窗口无效并区重画窗口的任何部分和父窗口需要重画的部分。
        /// </summary>
		SWP_NOREDRAW        = 0x0008,
        /// <summary>
        /// 不激活窗口。如果未设置标志，则窗口被激活，并被设置到其他最高级窗口或非最高级组的顶部（根据参数 hWndlnsertAfter 设置）。
        /// </summary>
		SWP_NOACTIVATE      = 0x0010,
        /// <summary>
        /// 给窗口发送 WM_NCCALCSIZE 消息，即使窗口尺寸没有改变也会发送该消息。如果未指定这个标志，只有在改变了窗口尺寸时才发送 WM_NCCALCSIZE。
        /// </summary>
		SWP_FRAMECHANGED    = 0x0020,
        /// <summary>
        /// 显示窗口。
        /// </summary>
		SWP_SHOWWINDOW      = 0x0040,
        /// <summary>
        /// 隐藏窗口。
        /// </summary>
		SWP_HIDEWINDOW      = 0x0080,
        /// <summary>
        /// 清除客户区的所有内容。如果未设置该标志，客户区的有效内容被保存并且在窗口尺寸更新和重定位后拷贝回客户区。
        /// </summary>
		SWP_NOCOPYBITS      = 0x0100,
        /// <summary>
        /// 不改变z序中的所有者窗口的位置。
        /// </summary>
		SWP_NOOWNERZORDER   = 0x0200, 
        /// <summary>
        /// 防止窗口接收 WM_WINDOWPOSCHANGING 消息。
        /// </summary>
		SWP_NOSENDCHANGING  = 0x0400,
        /// <summary>
        /// 在窗口周围画一个边框（定义在窗口类描述中）。
        /// </summary>
		SWP_DRAWFRAME       = 0x0020,
        /// <summary>
        /// 与 SWP_NOOWNERZORDER 标志相同。
        /// </summary>
		SWP_NOREPOSITION    = 0x0200,
        /// <summary>
        /// 防止产生 WM_SYNCPAINT 消息。
        /// </summary>
		SWP_DEFERERASE      = 0x2000,
        /// <summary>
        /// 如果调用进程不拥有窗口，系统会向拥有窗口的线程发出需求。这就防止调用线程在其他线程处理需求的时候发生死锁。
        /// </summary>
		SWP_ASYNCWINDOWPOS  = 0x4000
	}

    //public enum ShowWindowStyles : uint
    //{
    //    SW_HIDE = 0,
    //    SW_SHOWNORMAL = 1,
    //    SW_NORMAL = 1,
    //    SW_SHOWMINIMIZED = 2,
    //    SW_SHOWMAXIMIZED = 3,
    //    SW_MAXIMIZE = 3,
    //    SW_SHOWNOACTIVATE = 4,
    //    SW_SHOW = 5,
    //    SW_MINIMIZE = 6,
    //    SW_SHOWMINNOACTIVE = 7,
    //    SW_SHOWNA = 8,
    //    SW_RESTORE = 9,
    //    SW_SHOWDEFAULT = 10,
    //    SW_FORCEMINIMIZE = 11,
    //    SW_MAX = 11
    //}

    /// <summary>
    /// 预定义 ShowWindow 操作类型
    /// </summary>
    public enum ShowWindowStyles : uint
	{
        /// <summary>
        /// 在WindowNT5.0中最小化窗口，即使拥有窗口的线程被挂起也会最小化。在从其他线程最小化窗口时才使用这个参数。
        /// </summary>
        SW_FORCEMINIMIZE = 0x0,
        /// <summary>
        /// 隐藏窗口并激活其他窗口
        /// </summary>
        SW_HIDE = 0x1,
        /// <summary>
        /// 最大化指定的窗口。
        /// </summary>
        SW_MAXIMIZE = 0x2,
        /// <summary>
        /// 最小化指定的窗口并且激活在Z序中的下一个顶层窗口。
        /// </summary>
        SW_MINIMIZE = 0x3,
        /// <summary>
        /// 激活并显示窗口。如果窗口最小化或最大化，则系统将窗口恢复到原来的尺寸和位置。在恢复最小化窗口时，应用程序应该指定这个标志。
        /// </summary>
        SW_RESTORE = 0x4,
        /// <summary>
        /// 在窗口原来的位置以原来的尺寸激活和显示窗口。
        /// </summary>
        SW_SHOW = 0x5,
        /// <summary>
        /// 依据在STARTUPINFO结构中指定的SW_FLAG标志设定显示状态，STARTUPINFO 结构是由启动应用程序的程序传递给CreateProcess函数的。
        /// </summary>
        SW_SHOWDEFAULT = 0x6,
        /// <summary>
        /// 激活窗口并将其最大化。
        /// </summary>
        SW_SHOWMAXIMIZED = 0x7,
        /// <summary>
        /// 激活窗口并将其最小化。
        /// </summary>
        SW_SHOWMINIMIZED = 0x8,
        /// <summary>
        /// 窗口最小化，激活窗口仍然维持激活状态。
        /// </summary>
        SW_SHOWMINNOACTIVE = 0x9,
        /// <summary>
        /// 以窗口原来的状态显示窗口。激活窗口仍然维持激活状态。
        /// </summary>
        SW_SHOWNA = 0xA,
        /// <summary>
        /// 以窗口最近一次的大小和状态显示窗口。激活窗口仍然维持激活状态。
        /// </summary>
        SW_SHOWNOACTIVATE = 0xB,
        /// <summary>
        /// 激活并显示一个窗口。如果窗口被最小化或最大化，系统将其恢复到原来的尺寸和大小。应用程序在第一次显示窗口的时候应该指定此标志。
        /// </summary>
        SW_SHOWNORMAL = 0xC,
        /// <summary>
        /// 当一个窗口或应用程序要关闭时发送一个信号
        /// </summary>
        WM_CLOSE = 0x10,
	}

    /// <summary>
    /// 窗口风格定义。
    /// </summary>
    public enum WindowStyles : uint
	{
        /// <summary>
        /// 产生一个层叠的窗口。一个层叠的窗口有一个标题条和一个边框。与WS_TILED风格相同。
        /// </summary>
		WS_OVERLAPPED       = 0x00000000,
        /// <summary>
        /// 创建一个弹出式窗口。该风格不能与WS_CHILD风格同时使用。
        /// </summary>
		WS_POPUP            = 0x80000000,
        /// <summary>
        /// 创建一个子窗口。这个风格不能与WS_POPUP风格合用。
        /// </summary>
		WS_CHILD            = 0x40000000,
		WS_MINIMIZE         = 0x20000000,
        /// <summary>
        /// 创建一个初始状态为可见的窗口。
        /// </summary>
		WS_VISIBLE          = 0x10000000,
        /// <summary>
        /// 创建一个初始状态为禁止的子窗口。一个禁止状态的窗口不能接受来自用户的输入信息。
        /// </summary>
		WS_DISABLED         = 0x08000000,
        /// <summary>
        /// 排除子窗口之间的相对区域，也就是，当一个特定的窗口接收到WM_PAINT消息时，WS_CLIPSIBLINGS 风格将所有层叠窗口排除在绘图之外，只重绘指定的子窗口。如果未指定WS_CLIPSIBLINGS风格，并且子窗口是层叠的，则在重绘子窗口的客户区时，就会重绘邻近的子窗口。
        /// </summary>
		WS_CLIPSIBLINGS     = 0x04000000,
        /// <summary>
        /// 当在父窗口内绘图时，排除子窗口区域。在创建父窗口时使用这个风格。
        /// </summary>
		WS_CLIPCHILDREN     = 0x02000000,
        /// <summary>
        /// 创建一个初始状态为最大化状态的窗口。
        /// </summary>
		WS_MAXIMIZE         = 0x01000000,
        /// <summary>
        /// 创建一个有标题框的窗口（包括WS_BODER风格）。
        /// </summary>
		WS_CAPTION          = 0x00C00000,
        /// <summary>
        /// 创建一个带边框的窗口。
        /// </summary>
		WS_BORDER           = 0x00800000,
        /// <summary>
        /// 创建一个带对话框边框风格的窗口。这种风格的窗口不能带标题条。
        /// </summary>
		WS_DLGFRAME         = 0x00400000,
        /// <summary>
        /// 创建一个有垂直滚动条的窗口。
        /// </summary>
		WS_VSCROLL          = 0x00200000,
        /// <summary>
        /// 创建一个有水平滚动条的窗口。
        /// </summary>
		WS_HSCROLL          = 0x00100000,
        /// <summary>
        /// 创建一个在标题条上带有窗口菜单的窗口，必须同时设定WS_CAPTION风格。
        /// </summary>
		WS_SYSMENU          = 0x00080000,
        /// <summary>
        /// 创建一个具有可调边框的窗口，与WS_SIZEBOX风格相同。
        /// </summary>
		WS_THICKFRAME       = 0x00040000,
        /// <summary>
        /// 指定一组控制的第一个控制。这个控制组由第一个控制和随后定义的控制组成，自第二个控制开始每个控制，具有WS_GROUP风格，每个组的第一个控制带有WS_TABSTOP风格，从而使用户可以在组间移动。用户随后可以使用光标在组内的控制间改变键盘焦点。
        /// </summary>
		WS_GROUP            = 0x00020000,
        /// <summary>
        /// 创建一个控制，这个控制在用户按下Tab键时可以获得键盘焦点。按下Tab键后使键盘焦点转移到下一具有WS_TABSTOP风格的控制。
        /// </summary>
		WS_TABSTOP          = 0x00010000,
		WS_MINIMIZEBOX      = 0x00020000,
        /// <summary>
        /// 创建一个具有最大化按钮的窗口。该风格不能与WS_EX_CONTEXTHELP风格同时出现，同时必须指定WS_SYSMENU风格。
        /// </summary>
		WS_MAXIMIZEBOX      = 0x00010000,
        /// <summary>
        /// 产生一个层叠的窗口。一个层叠的窗口有一个标题和一个边框。与WS_OVERLAPPED风格相同。
        /// </summary>
		WS_TILED            = 0x00000000,
        /// <summary>
        /// 创建一个初始状态为最小化状态的窗口。与WS_MINIMIZE风格相同。
        /// </summary>
		WS_ICONIC           = 0x20000000,
        /// <summary>
        /// 创建一个可调边框的窗口，与WS_THICKFRAME风格相同。
        /// </summary>
		WS_SIZEBOX          = 0x00040000,
        /// <summary>
        /// 创建一个具有WS_BORDER，WS_POPUP,WS_SYSMENU风格的窗口，WS_CAPTION和WS_POPUPWINDOW必须同时设定才能使窗口某单可见。
        /// </summary>
		WS_POPUPWINDOW      = 0x80880000,
        /// <summary>
        /// 创建一个具有WS_OVERLAPPED，WS_CAPTION，WS_SYSMENU WS_THICKFRAME，WS_MINIMIZEBOX，WS_MAXIMIZEBOX风格的层叠窗口，与WS_TILEDWINDOW风格相同。
        /// </summary>
		WS_OVERLAPPEDWINDOW = 0x00CF0000,
        /// <summary>
        /// 创建一个具有WS_OVERLAPPED，WS_CAPTION，WS_SYSMENU， WS_THICKFRAME，WS_MINIMIZEBOX，WS_MAXIMIZEBOX风格的层叠窗口。与WS_OVERLAPPEDWINDOW风格相同。
        /// </summary>
		WS_TILEDWINDOW      = 0x00CF0000,
        /// <summary>
        /// 与WS_CHILD相同。
        /// </summary>
		WS_CHILDWINDOW      = 0x40000000
	}

    /// <summary>
    /// 扩展的窗口风格定义
    /// </summary>
    public enum WindowExStyles
	{
        /// <summary>
        /// 指明一个具有双重边界的窗口，当你在dwStyle参数中指定了WS_CAPTION风格标志时，它可以具有标题条（可选）。 
        /// </summary>
		WS_EX_DLGMODALFRAME     = 0x00000001,
        /// <summary>
        /// 指定用这个风格创建的子窗口在被创建或销毁的时候将不向父窗口发送WM_PARENTNOTIFY消息。 
        /// </summary>
		WS_EX_NOPARENTNOTIFY    = 0x00000004,
        /// <summary>
        /// 指定用这个风格创建的窗口必须被放在所有非顶层窗口的上面，即使这个窗口已经不处于激活状态，它还是保留在最上面。应用程序可以用SetWindowsPos成员函数来加入或去掉这个属性。
        /// </summary>
		WS_EX_TOPMOST           = 0x00000008,
        /// <summary>
        /// 指定用此样式创建的窗口接收拖放文件。
        /// </summary>
		WS_EX_ACCEPTFILES       = 0x00000010,
        /// <summary>
        /// 指定了用这个风格创建的窗口是透明的。这意味着，在这个窗口下面的任何窗口都不会被这个窗口挡住。用这个风格创建的窗口只有当它下面的窗口都更新过以后才接收WM_PAINT消息。 
        /// </summary>
		WS_EX_TRANSPARENT       = 0x00000020,
        /// <summary>
        /// 创建一个MDI子窗口。
        /// </summary>
		WS_EX_MDICHILD          = 0x00000040,
        /// <summary>
        /// 创建一个工具窗口，目的是被用作浮动工具条。工具窗口具有标题条，比通常的标题条要短，窗口的标题是用小字体显示的。工具窗口不出现在任务条或用户按下ALT+TAB时出现的窗口中。
        /// </summary>
		WS_EX_TOOLWINDOW        = 0x00000080,
        /// <summary>
        /// 指定了具有凸起边框的窗口。 
        /// </summary>
		WS_EX_WINDOWEDGE        = 0x00000100,
        /// <summary>
        /// 指明窗口具有3D外观，这意味着，边框具有下沉的边界。
        /// </summary>
		WS_EX_CLIENTEDGE        = 0x00000200,
        /// <summary>
        /// 在窗口的标题条中包含问号。当用户单击问号时，鼠标光标的形状变为带指针的问号。如果用户随后单击一个子窗口，子窗口将接收到一个WM_HELP消息。
        /// </summary>
		WS_EX_CONTEXTHELP       = 0x00000400,
        /// <summary>
        /// 赋予窗口右对齐属性。这与窗口类有关。
        /// </summary>
		WS_EX_RIGHT             = 0x00001000,
        /// <summary>
        /// 指定窗口具有左对齐属性。这是缺省值。
        /// </summary>
		WS_EX_LEFT              = 0x00000000,
        /// <summary>
        /// 按照从右到左的顺序显示窗口文本。
        /// </summary>
		WS_EX_RTLREADING        = 0x00002000,
        /// <summary>
        /// 按照从左到右的方式显示窗口文本。这是缺省方式。 
        /// </summary>
		WS_EX_LTRREADING        = 0x00000000,
        /// <summary>
        /// 将垂直滚动条放在客户区的左边。
        /// </summary>
		WS_EX_LEFTSCROLLBAR     = 0x00004000,
        /// <summary>
        /// 将垂直滚动条（如果有）放在客户区的右边。这是缺省方式。 
        /// </summary>
		WS_EX_RIGHTSCROLLBAR    = 0x00000000,
        /// <summary>
        /// 允许用户用TAB键遍历窗口的子窗口。 
        /// </summary>
		WS_EX_CONTROLPARENT     = 0x00010000,
        /// <summary>
        /// 创建一个具有三维边界的窗口，用于不接受用户输入的项。
        /// </summary>
		WS_EX_STATICEDGE        = 0x00020000,
        /// <summary>
        /// 当窗口可见时，将一个顶层窗口放置到任务条上。
        /// </summary>
		WS_EX_APPWINDOW         = 0x00040000,
        /// <summary>
        /// 组合了WS_EX_CLIENTEDGE和WS_EX_WIND-OWEDGE风格。
        /// </summary>
		WS_EX_OVERLAPPEDWINDOW  = 0x00000300,
        /// <summary>
        /// 组合了WS_EX_WINDOWEDGE和WS_EX_TOPMOST风格。 
        /// </summary>
		WS_EX_PALETTEWINDOW     = 0x00000188,
        /// <summary>
        /// 窗口是一个 分层窗口 如果窗口具有 CS_OWNDC 或 CS_CLASSDC任意一个的class style ，则此样式不被使用。 但是，Windows 8 支持子窗口的 WS_EX_LAYERED 样式，之前的 Windows 版本仅对顶级窗口支持。
        /// </summary>
		WS_EX_LAYERED			= 0x00080000
	}

    public enum HitTest
	{
		HTERROR			= -2,
		HTTRANSPARENT   = -1,
		HTNOWHERE		= 0,
		HTCLIENT		= 1,
		HTCAPTION		= 2,
		HTSYSMENU		= 3,
		HTGROWBOX		= 4,
		HTSIZE			= 4,
		HTMENU			= 5,
		HTHSCROLL		= 6,
		HTVSCROLL		= 7,
		HTMINBUTTON		= 8,
		HTMAXBUTTON		= 9,
		HTLEFT			= 10,
		HTRIGHT			= 11,
		HTTOP			= 12,
		HTTOPLEFT		= 13,
		HTTOPRIGHT		= 14,
		HTBOTTOM		= 15,
		HTBOTTOMLEFT	= 16,
		HTBOTTOMRIGHT	= 17,
		HTBORDER		= 18,
		HTREDUCE		= 8,
		HTZOOM			= 9 ,
		HTSIZEFIRST		= 10,
		HTSIZELAST		= 17,
		HTOBJECT		= 19,
		HTCLOSE			= 20,
		HTHELP			= 21
	}

    /// <summary>
    /// 指定滚动条是否是窗口的非工作区的控件或部件。 
    /// </summary>
    public enum ScrollBars : uint
	{
        /// <summary>
        /// 显示或隐藏窗体的标准的水平滚动条。
        /// </summary>
		SB_HORZ = 0,
        /// <summary>
        /// 显示或隐藏窗体的标准的垂直滚动条。
        /// </summary>
		SB_VERT = 1,
        /// <summary>
        /// 显示或隐藏滚动条控制。参数hWnd必须是指向滚动条控制的句柄。
        /// </summary>
		SB_CTL = 2,
        /// <summary>
        /// 显示或隐藏窗体的标准的水平或垂直滚动条。
        /// </summary>
		SB_BOTH = 3
	}

    public enum GetWindowLongIndex : int
    {
        /// <summary>
        /// 获得窗口风格。
        /// </summary>
        GWL_STYLE = -16,
        /// <summary>
        /// 获得扩展窗日风格。
        /// </summary>
        GWL_EXSTYLE = -20,
        /// <summary>
        /// 获得窗口过程的地址，或代表窗口过程的地址的句柄。必须使用CallWindowProc函数调用窗口过程。
        /// </summary>
        GWL_WNDPROC = -4,
        /// <summary>
        /// 获得应用事例的句柄。
        /// </summary>
        GWL_HINSTANCE = -6,
        /// <summary>
        /// 如果父窗口存在，获得父窗口句柄。
        /// </summary>
        GWL_HWNDPARENT = -8,
        /// <summary>
        /// 获得窗口标识。
        /// </summary>
        GWL_ID = -12,
        /// <summary>
        /// 获得与窗口有关的32位值。每一个窗口均有一个由创建该窗口的应用程序使用的32位值。
        /// </summary>
        GWL_USERDATA = -21,
        /// <summary>
        /// 当hWnd参数标识了一个对话框时，获得对话框过程的地址，或一个代表对话框过程的地址的句柄。必须使用函数CallWindowProc来调用对话框过程。
        /// </summary>
        DWL_DLGPROC = 4,
        /// <summary>
        /// 当hWnd参数标识了一个对话框时，获得在对话框过程中一个消息处理的返回值。
        /// </summary>
        DWL_MSGRESULT = 0,
        /// <summary>
        /// 当hWnd参数标识了一个对话框时，获得应用程序私有的额外信息，例如一个句柄或指针。
        /// </summary>
        DWL_USER = 8,
    }

    /// <summary>
    /// 钩子类型
    /// </summary>
    public enum HookType : int
    {
        /// <summary>
        /// 安装一个挂钩处理过程,对寄送至系统消息队列的输入消息进行纪录.详情参见JournalRecordProc挂钩处理过程.
        /// </summary>
        WH_JOURNALRECORD = 0,
        /// <summary>
        /// 安装一个挂钩处理过程,对此前由WH_JOURNALRECORD 挂钩处理过程纪录的消息进行寄送.详情参见 JournalPlaybackProc挂钩处理过程
        /// </summary>
        WH_JOURNALPLAYBACK = 1,
        /// <summary>
        /// 安装一个挂钩处理过程对击键消息进行监视. 详情参见KeyboardProc挂钩处理过程
        /// </summary>
        WH_KEYBOARD = 2,
        /// <summary>
        /// 安装一个挂钩处理过程对寄送至消息队列的消息进行监视,详情参见 GetMsgProc 挂钩处理过程
        /// </summary>
        WH_GETMESSAGE = 3,
        /// <summary>
        /// 安装一个挂钩处理过程,在系统将消息发送至目标窗口处理过程之前,对该消息进行监视,详情参见CallWndProc挂钩处理过程
        /// </summary>
        WH_CALLWNDPROC = 4,
        /// <summary>
        /// 安装一个挂钩处理过程,接受对CBT应用程序有用的消息 ,详情参见 CBTProc 挂钩处理过程
        /// </summary>
        WH_CBT = 5,
        /// <summary>
        /// 安装一个挂钩处理过程,以监视由对话框、消息框、菜单条、或滚动条中的输入事件引发的消息.这个挂钩处理过程对系统中所有应用程序的这类消息都进行监视.详情参见 SysMsgProc挂钩处理过程
        /// </summary>
        WH_SYSMSGFILTER = 6,
        /// <summary>
        /// 安装一个挂钩处理过程,对鼠标消息进行监视. 详情参见 MouseProc挂钩处理过程
        /// </summary>
        WH_MOUSE = 7,
        /// <summary>
        /// 当调用GetMessage 或 PeekMessage 来从消息队列种查询非鼠标、键盘消息时
        /// </summary>
        WH_HARDWARE = 8,
        /// <summary>
        /// 安装一个挂钩处理过程以便对其他挂钩处理过程进行调试, 详情参见DebugProc挂钩处理过程
        /// </summary>
        WH_DEBUG = 9,
        /// <summary>
        /// 安装一个挂钩处理过程以接受对外壳应用程序有用的通知, 详情参见 ShellProc挂钩处理过程
        /// </summary>
        WH_SHELL = 10,
        /// <summary>
        /// 安装一个挂钩处理过程,该挂钩处理过程当应用程序的前台线程即将进入空闲状态时被调用,它有助于在空闲时间内执行低优先级的任务
        /// </summary>
        WH_FOREGROUNDIDLE = 11,
        /// <summary>
        /// 安装一个挂钩处理过程,它对已被目标窗口处理过程处理过了的消息进行监视,详情参见 CallWndRetProc 挂钩处理过程
        /// </summary>
        WH_CALLWNDPROCRET = 12,
        /// <summary>
        /// 键盘输入钩子。
        /// </summary>
        WH_KEYBOARD_LL = 13,
        /// <summary>
        /// 此挂钩只能在Windows NT中被安装,用来对底层的鼠标输入事件进行监视.详情参见LowLevelMouseProc挂钩处理过程
        /// </summary>
        WH_MOUSE_LL = 14
    }

    /// <summary>
    /// WINDOWS 消息常量标识符。
    /// </summary>
    /// <remarks>
    /// Windows是一消息（Message）驱动式系统，Windows消息提供了应用程序与应用程序之间、应用程序与Windows系统之间进行通讯的手段。应用
    /// 程序要实现的功能由消息来触发，并靠对消息的响应和处理来完成。Windows系统中有两种消息队列，一种是系统消息队列，另一种是应用程序
    /// 消息队列。计算机的所有输入设备由 Windows监控，当一个事件发生时，Windows先将输入的消息放入系统消息队列中，然后再将输入的消息拷
    /// 贝到相应的应用程序队列中，应用程序中的消息循环从它的消息队列中检索每一个消息并发送给相应的窗口函数中。一个事件的发生，到达处
    /// 理它的窗口函数必须经历上述过程。值得注意的是消息的非抢先性，即不论事件的急与缓，总是按到达的先后排队(一些系统消息除外)，这就
    /// 使得一些外部实时事件可能得不到及时的处理。 
    /// 
    /// 由于Windows本身是由消息驱动的，所以解密时跟踪一个消息会得到相当底层的答案。举一个例子来说明这个问题，打开记事本程序，该程序有
    /// 一个File菜单，那么，在运行该应用程序的时候，如果用户单击了File菜单里New命令时，这个动作将被Windows （而不是应用程序本身！）所
    /// 捕获，Windows经过分析得知这个动作应该由上面所说的那个应用程序去处理，既然是这样，Windows就发送了个叫做WM_COMMAND的消息给应用
    /// 程序，该消息所包含信息告诉应用程序："用户单击了New菜单"，应用程序得知这一消息之后，采取相应的动作来响应它，这个过程称为消息处
    /// 理。Windows为每一个应用程序(确切地说是每一个线程)维护了相应的消息队列，应用程序的任务就是不停的从它的消息队列中获取消息，分析
    /// 消息和处理消息，直到一条接到叫做WM_QUIT消息为止，这个过程通常是由一种叫做消息循环的程序结构来实现的。 
    /// 
    /// 消息本身是作为一个记录传递给应用程序的，这个记录中包含了消息的类型以及其他信息。例如，对于单击鼠标所产生的消息来说，这个记录
    /// 中包含了单击鼠标时的坐标。这个记录类型叫做TMsg，它在Windows单元中是这样声明的： 
    /// type 
    /// TMsg = packed record 
    /// hwnd: HWND; 		//窗口句柄 
    /// message: UINT; 		//消息常量标识符 
    /// wParam: WPA R A M ; 	// 32位消息的特定附加信息 
    /// lParam: LPA R A M ; 	// 32位消息的特定附加信息 
    /// time: DWORD; 		//消息创建时的时间 
    /// pt: TPoint; 		//消息创建时的鼠标位置 
    /// end; 
    /// 
    /// 消息中有什么？ 
    /// 是否觉得一个消息记录中的信息像希腊语一样？如果是这样，那么看一看下面的解释： 
    /// hwnd		32位的窗口句柄。窗口可以是任何类型的屏幕对象，因为Win32能够维护大多数可视对象的句柄(窗口、对话框、按钮、编辑框等)。 
    /// message		用于区别其他消息的常量值，这些常量可以是Windows单元中预定义的常量，也可以是自定义的常量。 
    /// wParam		通常是一个与消息有关的常量值，也可能是窗口或控件的句柄。 
    /// lParam		通常是一个指向内存中数据的指针。由于WParm、lParam和Pointer都是32位的，因此，它们之间可以相互转换。 
    /// </remarks>
    public enum WindowsNumber : int
    {
        WM_NULL                       = 0x0000, // 十进制：0 
        /// <summary>
        /// 应用程序创建一个窗口
        /// </summary>
        WM_CREATE                     = 0x0001, // 十进制：1 
        /// <summary>
        /// 一个窗口被销毁
        /// </summary>
        WM_DESTROY                    = 0x0002, // 十进制：2 
        /// <summary>
        /// 移动一个窗口
        /// </summary>
        WM_MOVE                       = 0x0003, // 十进制：3 
        PAGE_READWRITE                = 0x0004,
        /// <summary>
        /// 改变一个窗口的大小
        /// </summary>
        WM_SIZE                       = 0x0005, // 十进制：5 
        /// <summary>
        /// 一个窗口被激活或失去激活状态
        /// </summary>
        WM_ACTIVATE                   = 0x0006, // 十进制：6 
        /// <summary>
        /// 获得焦点后
        /// </summary>
        WM_SETFOCUS                   = 0x0007, // 十进制：7 
        /// <summary>
        /// 失去焦点
        /// </summary>
        WM_KILLFOCUS                  = 0x0008, // 十进制：8 
        /// <summary>
        /// 改变enable状态
        /// </summary>
        WM_ENABLE                     = 0x000a, // 十进制：10 
        /// <summary>
        /// 设置窗口是否能重画
        /// </summary>
        WM_SETREDRAW                  = 0x000b, // 十进制：11 
        /// <summary>
        /// 应用程序发送此消息来设置一个窗口的文本
        /// </summary>
        WM_SETTEXT                    = 0x000c, // 十进制：12 
        /// <summary>
        /// 应用程序发送此消息来复制对应窗口的文本到缓冲区
        /// </summary>
        WM_GETTEXT                    = 0x000d, // 十进制：13 
        /// <summary>
        /// 得到与一个窗口有关的文本的长度（不包含空字符）
        /// </summary>
        WM_GETTEXTLENGTH              = 0x000e, // 十进制：14 
        /// <summary>
        /// 要求一个窗口重画自己
        /// </summary>
        WM_PAINT                      = 0x000f, // 十进制：15 
        /// <summary>
        /// 当一个窗口或应用程序要关闭时发送一个信号
        /// </summary>
        WM_CLOSE                      = 0x0010, // 十进制：16 
        /// <summary>
        /// 当用户选择结束对话框或程序自己调用ExitWindows函数
        /// </summary>
        WM_QUERYENDSESSION            = 0x0011, // 十进制：17 
        /// <summary>
        /// 用来结束程序运行或当程序调用postquitmessage函数
        /// </summary>
        WM_QUIT                       = 0x0012, // 十进制：18 
        /// <summary>
        /// 当用户窗口恢复以前的大小位置时，把此消息发送给某个图标
        /// </summary>
        WM_QUERYOPEN                  = 0x0013, // 十进制：19 
        /// <summary>
        /// 当窗口背景必须被擦除时（例在窗口改变大小时）
        /// </summary>
        WM_ERASEBKGND                 = 0x0014, // 十进制：20 
        /// <summary>
        /// 当系统颜色改变时，发送此消息给所有顶级窗口
        /// </summary>
        WM_SYSCOLORCHANGE             = 0x0015, // 十进制：21 
        /// <summary>
        /// 当系统进程发出WM_QUERYENDSESSION消息后，此消息发送给应用程序，通知它对话是否结束
        /// </summary>
        WM_ENDSESSION                 = 0x0016, // 十进制：22 
        /// <summary>
        /// 当隐藏或显示窗口是发送此消息给这个窗口
        /// </summary>
        WM_SHOWWINDOW                 = 0x0018, // 十进制：24 
        /// <summary>
        /// 
        /// </summary>
        WM_CTLCOLOR                   = 0x0019, // 十进制：25 
        WM_WININICHANGE               = 0x001a, // 十进制：26 
		WM_SETTINGCHANGE              = 0x001A, // 十进制：26 
        WM_DEVMODECHANGE              = 0x001b, // 十进制：27 
        /// <summary>
        /// 发此消息给应用程序哪个窗口是激活的，哪个是非激活的
        /// </summary>
        WM_ACTIVATEAPP                = 0x001c, // 十进制：28 
        /// <summary>
        /// 当系统的字体资源库变化时发送此消息给所有顶级窗口
        /// </summary>
        WM_FONTCHANGE                 = 0x001d, // 十进制：29 
        /// <summary>
        /// 当系统的时间变化时发送此消息给所有顶级窗口
        /// </summary>
        WM_TIMECHANGE                 = 0x001e, // 十进制：30 
        /// <summary>
        /// 发送此消息来取消某种正在进行的摸态（操作）
        /// </summary>
        WM_CANCELMODE                 = 0x001f, // 十进制：31 
        /// <summary>
        /// 如果鼠标引起光标在某个窗口中移动且鼠标输入没有被捕获时，就发消息给某个窗口
        /// </summary>
        WM_SETCURSOR                  = 0x0020, // 十进制：32 
        /// <summary>
        /// 当光标在某个非激活的窗口中而用户正按着鼠标的某个键发送此消息给当前窗口
        /// </summary>
        WM_MOUSEACTIVATE              = 0x0021, // 十进制：33 
        /// <summary>
        /// 发送此消息给MDI子窗口当用户点击此窗口的标题栏，或当窗口被激活，移动，改变大小
        /// </summary>
        WM_CHILDACTIVATE              = 0x0022, // 十进制：34 
        /// <summary>
        /// 此消息由基于计算机的训练程序发送，通过WH_JOURNALPALYBACK的hook程序分离出用户输入消息
        /// </summary>
        WM_QUEUESYNC                  = 0x0023, // 十进制：35 
        /// <summary>
        /// 此消息发送给窗口当它将要改变大小或位置；
        /// </summary>
        WM_GETMINMAXINFO              = 0x0024, // 十进制：36 
        /// <summary>
        /// 发送给最小化窗口当它图标将要被重画
        /// </summary>
        WM_PAINTICON                  = 0x0026, // 十进制：38 
        /// <summary>
        /// 此消息发送给某个最小化窗口，仅当它在画图标前它的背景必须被重画
        /// </summary>
        WM_ICONERASEBKGND             = 0x0027, // 十进制：39 
        /// <summary>
        /// 发送此消息给一个对话框程序去更改焦点位置
        /// </summary>
        WM_NEXTDLGCTL                 = 0x0028, // 十进制：40 
        /// <summary>
        /// 每当打印管理列队增加或减少一条作业时发出此消息
        /// </summary>
        WM_SPOOLERSTATUS              = 0x002a, // 十进制：42 
        /// <summary>
        /// 当button，combobox，listbox，menu的可视外观改变时发送,此消息给这些空件的所有者
        /// </summary>
        WM_DRAWITEM                   = 0x002b, // 十进制：43 
        /// <summary>
        /// 当button, combo box, list box, list view control, or menu item 被创建时,发送此消息给控件的所有者
        /// </summary>
        WM_MEASUREITEM                = 0x002c, // 十进制：44 
        /// <summary>
        /// 当the list box 或 combo box 被销毁 或 当 某些项被删除通过LB_DELETESTRING, LB_RESETCONTENT, CB_DELETESTRING, or CB_RESETCONTENT 消息
        /// </summary>
        WM_DELETEITEM                 = 0x002d, // 十进制：45 
        /// <summary>
        /// 此消息有一个LBS_WANTKEYBOARDINPUT风格的发出给它的所有者来响应WM_KEYDOWN消息
        /// </summary>
        WM_VKEYTOITEM                 = 0x002e, // 十进制：46 
        /// <summary>
        /// 此消息由一个LBS_WANTKEYBOARDINPUT风格的列表框发送给他的所有者来响应WM_CHAR消息 
        /// </summary>
        WM_CHARTOITEM                 = 0x002f, // 十进制：47 
        /// <summary>
        /// 当绘制文本时程序发送此消息得到控件要用的颜色
        /// </summary>
        WM_SETFONT                    = 0x0030, // 十进制：48 
        /// <summary>
        /// 应用程序发送此消息得到当前控件绘制文本的字体
        /// </summary>
        WM_GETFONT                    = 0x0031, // 十进制：49 
        /// <summary>
        /// 应用程序发送此消息让一个窗口与一个热键相关连
        /// </summary>
        WM_SETHOTKEY                  = 0x0032, // 十进制：50 
        /// <summary>
        /// 应用程序发送此消息来判断热键与某个窗口是否有关联
        /// </summary>
        WM_GETHOTKEY                  = 0x0033, // 十进制：51 
        /// <summary>
        /// 此消息发送给最小化窗口，当此窗口将要被拖放而它的类中没有定义图标，应用程序能返回一个图标或光标的句柄，当用户拖放图标时系统显示这个图标或光标
        /// </summary>
        WM_QUERYDRAGICON              = 0x0037, // 十进制：55 
        /// <summary>
        /// 发送此消息来判定combobox或listbox新增加的项的相对位置
        /// </summary>
        WM_COMPAREITEM                = 0x0039, // 十进制：57 
        WM_GETOBJECT                  = 0x003d, // 十进制：61 
        /// <summary>
        /// 显示内存已经很少了
        /// </summary>
        WM_COMPACTING                 = 0x0041, // 十进制：65 
        WM_COMMNOTIFY                 = 0x0044, // 十进制：68 
        /// <summary>
        /// 发送此消息给那个窗口的大小和位置将要被改变时，来调用setwindowpos函数或其它窗口管理函数
        /// </summary>
        WM_WINDOWPOSCHANGING          = 0x0046, // 十进制：70 
        /// <summary>
        /// 发送此消息给那个窗口的大小和位置已经被改变时，来调用setwindowpos函数或其它窗口管理函数
        /// </summary>
        WM_WINDOWPOSCHANGED           = 0x0047, // 十进制：71 
        /// <summary>
        /// （适用于16位的windows）当系统将要进入暂停状态时发送此消息
        /// </summary>
        WM_POWER                      = 0x0048, // 十进制：72 
        /// <summary>
        /// 当一个应用程序传递数据给另一个应用程序时发送此消息
        /// </summary>
        WM_COPYDATA                   = 0x004a, // 十进制：74 
        /// <summary>
        /// 当某个用户取消程序日志激活状态，提交此消息给程序
        /// </summary>
        WM_CANCELJOURNAL              = 0x004b, // 十进制：75 
        /// <summary>
        /// 当某个控件的某个事件已经发生或这个控件需要得到一些信息时，发送此消息给它的父窗口
        /// </summary>
        WM_NOTIFY                     = 0x004e, // 十进制：78 
        /// <summary>
        /// 当用户选择某种输入语言，或输入语言的热键改变
        /// </summary>
        WM_INPUTLANGCHANGEREQUEST     = 0x0050, // 十进制：80 
        /// <summary>
        /// 当平台现场已经被改变后发送此消息给受影响的最顶级窗口
        /// </summary>
        WM_INPUTLANGCHANGE            = 0x0051, // 十进制：81 
        /// <summary>
        /// 当程序已经初始化windows帮助例程时发送此消息给应用程序
        /// </summary>
        WM_TCARD                      = 0x0052, // 十进制：82 
        /// <summary>
        /// 此消息显示用户按下了F1，如果某个菜单是激活的，就发送此消息个此窗口关联的菜单，否则就发送给有焦点的窗口，如果当前都没有焦点，就把此消息发送给当前激活的窗口
        /// </summary>
        WM_HELP                       = 0x0053, // 十进制：83 
        /// <summary>
        /// 当用户已经登入或退出后发送此消息给所有的窗口，当用户登入或退出时系统更新用户的具体设置信息，在用户更新设置时系统马上发送此消息
        /// </summary>
        WM_USERCHANGED                = 0x0054, // 十进制：84 
        /// <summary>
        /// 公用控件，自定义控件和他们的父窗口通过此消息来判断控件是使用ANSI还是UNICODE结构 在WM_NOTIFY消息，使用此控件能使某个控件与它的父控件之间进行相互通信
        /// </summary>
        WM_NOTIFYFORMAT               = 0x0055, // 十进制：85 
        /// <summary>
        /// 当用户某个窗口中点击了一下右键就发送此消息给这个窗口
        /// </summary>
        WM_CONTEXTMENU                = 0x007b, // 十进制：123 
        /// <summary>
        /// 当调用SETWINDOWLONG函数将要改变一个或多个 窗口的风格时发送此消息给那个窗口
        /// </summary>
        WM_STYLECHANGING              = 0x007c, // 十进制：124 
        /// <summary>
        /// 当调用SETWINDOWLONG函数一个或多个 窗口的风格后发送此消息给那个窗口
        /// </summary>
        WM_STYLECHANGED               = 0x007d, // 十进制：125 
        /// <summary>
        /// 当显示器的分辨率改变后发送此消息给所有的窗口
        /// </summary>
        WM_DISPLAYCHANGE              = 0x007e, // 十进制：126 
        /// <summary>
        /// 此消息发送给某个窗口来返回与某个窗口有关连的大图标或小图标的句柄
        /// </summary>
        WM_GETICON                    = 0x007f, // 十进制：127 
        /// <summary>
        /// 程序发送此消息让一个新的大图标或小图标与某个窗口关联
        /// </summary>
        WM_SETICON                    = 0x0080, // 十进制：128 
        /// <summary>
        /// 当某个窗口第一次被创建时，此消息在WM_CREATE消息发送前发送
        /// </summary>
        WM_NCCREATE                   = 0x0081, // 十进制：129 
        /// <summary>
        /// 此消息通知某个窗口，非客户区正在销毁
        /// </summary>
        WM_NCDESTROY                  = 0x0082, // 十进制：130 
        /// <summary>
        /// 当某个窗口的客户区域必须被核算时发送此消息
        /// </summary>
        WM_NCCALCSIZE                 = 0x0083, // 十进制：131 
        /// <summary>
        /// 移动鼠标，按住或释放鼠标时发生
        /// </summary>
        WM_NCHITTEST                  = 0x0084, // 十进制：132 
        /// <summary>
        /// 程序发送此消息给某个窗口当它（窗口）的框架必须被绘制时
        /// </summary>
        WM_NCPAINT                    = 0x0085, // 十进制：133 
        /// <summary>
        /// 此消息发送给某个窗口 仅当它的非客户区需要被改变来显示是激活还是非激活状态
        /// </summary>
        WM_NCACTIVATE                 = 0x0086, // 十进制：134 
        /// <summary>
        /// 发送此消息给某个与对话框程序关联的控件，widdows控制方位键和TAB键使输入进入此控件通过响应WM_GETDLGCODE消息，应用程序可以把他当成一个特殊的输入控件并能处理它
        /// </summary>
        WM_GETDLGCODE                 = 0x0087, // 十进制：135 
        WM_SYNCPAINT                  = 0x0088, // 十进制：136 
        /// <summary>
        /// 当光标在一个窗口的非客户区内移动时发送此消息给这个窗口 //非客户区为：窗体的标题栏及窗的边框体
        /// </summary>
        WM_NCMOUSEMOVE                = 0x00a0, // 十进制：160 
        /// <summary>
        /// 当光标在一个窗口的非客户区同时按下鼠标左键时提交此消息
        /// </summary>
        WM_NCLBUTTONDOWN              = 0x00a1, // 十进制：161 
        /// <summary>
        /// 当用户释放鼠标左键同时光标某个窗口在非客户区十发送此消息
        /// </summary>
        WM_NCLBUTTONUP                = 0x00a2, // 十进制：162 
        /// <summary>
        /// 当用户双击鼠标左键同时光标某个窗口在非客户区十发送此消息
        /// </summary>
        WM_NCLBUTTONDBLCLK            = 0x00a3, // 十进制：163 
        /// <summary>
        /// 当用户按下鼠标右键同时光标又在窗口的非客户区时发送此消息
        /// </summary>
        WM_NCRBUTTONDOWN              = 0x00a4, // 十进制：164 
        /// <summary>
        /// 当用户释放鼠标右键同时光标又在窗口的非客户区时发送此消息
        /// </summary>
        WM_NCRBUTTONUP                = 0x00a5, // 十进制：165 
        /// <summary>
        /// 当用户双击鼠标右键同时光标某个窗口在非客户区十发送此消息
        /// </summary>
        WM_NCRBUTTONDBLCLK            = 0x00a6, // 十进制：166 
        /// <summary>
        /// 当用户按下鼠标中键同时光标又在窗口的非客户区时发送此消息
        /// </summary>
        WM_NCMBUTTONDOWN              = 0x00a7, // 十进制：167 
        /// <summary>
        /// 当用户释放鼠标中键同时光标又在窗口的非客户区时发送此消息
        /// </summary>
        WM_NCMBUTTONUP                = 0x00a8, // 十进制：168 
        /// <summary>
        /// 当用户双击鼠标中键同时光标又在窗口的非客户区时发送此消息
        /// </summary>
        WM_NCMBUTTONDBLCLK            = 0x00a9, // 十进制：169 
        WM_NCXBUTTONDOWN              = 0x00ab, // 十进制：171 
        WM_NCXBUTTONUP                = 0x00ac, // 十进制：172 
        WM_NCXBUTTONDBLCLK            = 0x00ad, // 十进制：173 
        WM_INPUT                      = 0x00ff, // 十进制：255 
        /// <summary>
        /// 按下一个键
        /// </summary>
        WM_KEYDOWN                    = 0x0100, // 十进制：256 
        WM_KEYFIRST                   = 0x0100, // 十进制：256 
        /// <summary>
        /// 释放一个键
        /// </summary>
        WM_KEYUP                      = 0x0101, // 十进制：257 
        /// <summary>
        /// 按下某键，并已发出WM_KEYDOWN， WM_KEYUP消息
        /// </summary>
        WM_CHAR                       = 0x0102, // 十进制：258 
        /// <summary>
        /// 当用translatemessage函数翻译WM_KEYUP消息时发送此消息给拥有焦点的窗口
        /// </summary>
        WM_DEADCHAR                   = 0x0103, // 十进制：259 
        STILL_ACTIVE                  = 0x0103,
        /// <summary>
        /// 当用户按住ALT键同时按下其它键时提交此消息给拥有焦点的窗口
        /// </summary>
        WM_SYSKEYDOWN                 = 0x0104, // 十进制：260 
        /// <summary>
        /// 当用户释放一个键同时ALT 键还按着时提交此消息给拥有焦点的窗口
        /// </summary>
        WM_SYSKEYUP                   = 0x0105, // 十进制：261 
        /// <summary>
        /// 当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后提交此消息给拥有焦点的窗口
        /// </summary>
        WM_SYSCHAR                    = 0x0106, // 十进制：262 
        /// <summary>
        /// 当WM_SYSKEYDOWN消息被TRANSLATEMESSAGE函数翻译后发送此消息给拥有焦点的窗口
        /// </summary>
        WM_SYSDEADCHAR                = 0x0107, // 十进制：263 
        WM_KEYLAST                    = 0x0108, // 十进制：264 
        WM_WNT_CONVERTREQUESTEX       = 0x0109, // 十进制：265 
        WM_CONVERTREQUEST             = 0x010a, // 十进制：266 
        WM_CONVERTRESULT              = 0x010b, // 十进制：267 
        WM_INTERIM                    = 0x010c, // 十进制：268 
        WM_IME_STARTCOMPOSITION       = 0x010d, // 十进制：269 
        WM_IME_ENDCOMPOSITION         = 0x010e, // 十进制：270 
        WM_IME_COMPOSITION            = 0x010f, // 十进制：271 
        WM_IME_KEYLAST                = 0x010f, // 十进制：271 
        /// <summary>
        /// 在一个对话框程序被显示前发送此消息给它，通常用此消息初始化控件和执行其它任务
        /// </summary>
        WM_INITDIALOG                 = 0x0110, // 十进制：272 
        /// <summary>
        /// 当用户选择一条菜单命令项或当某个控件发送一条消息给它的父窗口，一个快捷键被翻译
        /// </summary>
        WM_COMMAND                    = 0x0111, // 十进制：273 
        /// <summary>
        /// 当用户选择窗口菜单的一条命令或当用户选择最大化或最小化时那个窗口会收到此消息
        /// </summary>
        WM_SYSCOMMAND                 = 0x0112, // 十进制：274 
        /// <summary>
        /// 发生了定时器事件
        /// </summary>
        WM_TIMER                      = 0x0113, // 十进制：275 
        /// <summary>
        /// 当一个窗口标准水平滚动条产生一个滚动事件时发送此消息给那个窗口，也发送给拥有它的控件
        /// </summary>
        WM_HSCROLL                    = 0x0114, // 十进制：276 
        /// <summary>
        /// 当一个窗口标准垂直滚动条产生一个滚动事件时发送此消息给那个窗口也，发送给拥有它的控件
        /// </summary>
        WM_VSCROLL                    = 0x0115, // 十进制：277 
        /// <summary>
        /// 当一个菜单将要被激活时发送此消息，它发生在用户菜单条中的某项或按下某个菜单键，它允许程序在显示前更改菜单
        /// </summary>
        WM_INITMENU                   = 0x0116, // 十进制：278 
        /// <summary>
        /// 当一个下拉菜单或子菜单将要被激活时发送此消息，它允许程序在它显示前更改菜单，而不要改变全部
        /// </summary>
        WM_INITMENUPOPUP              = 0x0117, // 十进制：279 
        /// <summary>
        /// 当用户选择一条菜单项时发送此消息给菜单的所有者（一般是窗口）
        /// </summary>
        WM_MENUSELECT                 = 0x011f, // 十进制：287 
        /// <summary>
        /// 当菜单已被激活用户按下了某个键（不同于加速键），发送此消息给菜单的所有者
        /// </summary>
        WM_MENUCHAR                   = 0x0120, // 十进制：288 
        /// <summary>
        /// 当一个模态对话框或菜单进入空载状态时发送此消息给它的所有者，一个模态对话框或菜单进入空载状态就是在处理完一条或几条先前的消息后没有消息它的列队中等待
        /// </summary>
        WM_ENTERIDLE                  = 0x0121, // 十进制：289 
        WM_MENURBUTTONUP              = 0x0122, // 十进制：290 
        WM_MENUDRAG                   = 0x0123, // 十进制：291 
        WM_MENUGETOBJECT              = 0x0124, // 十进制：292 
        WM_UNINITMENUPOPUP            = 0x0125, // 十进制：293 
        WM_MENUCOMMAND                = 0x0126, // 十进制：294 
        WM_CHANGEUISTATE              = 0x0127, // 十进制：295 
        WM_UPDATEUISTATE              = 0x0128, // 十进制：296 
        WM_QUERYUISTATE               = 0x0129, // 十进制：297 
        /// <summary>
        /// 在windows绘制消息框前发送此消息给消息框的所有者窗口，通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置消息框的文本和背景颜色
        /// </summary>
        WM_CTLCOLORMSGBOX             = 0x0132, // 十进制：306 
        /// <summary>
        /// 当一个编辑型控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置编辑框的文本和背景颜色
        /// </summary>
        WM_CTLCOLOREDIT               = 0x0133, // 十进制：307 
        /// <summary>
        /// 当一个列表框控件将要被绘制前发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置列表框的文本和背景颜色
        /// </summary>
        WM_CTLCOLORLISTBOX            = 0x0134, // 十进制：308 
        /// <summary>
        /// 当一个按钮控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置按纽的文本和背景颜色
        /// </summary>
        WM_CTLCOLORBTN                = 0x0135, // 十进制：309 
        /// <summary>
        /// 当一个对话框控件将要被绘制前发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置对话框的文本背景颜色
        /// </summary>
        WM_CTLCOLORDLG                = 0x0136, // 十进制：310 
        /// <summary>
        /// 当一个滚动条控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置滚动条的背景颜色
        /// </summary>
        WM_CTLCOLORSCROLLBAR          = 0x0137, // 十进制：311 
        /// <summary>
        /// 当一个静态控件将要被绘制时发送此消息给它的父窗口；通过响应这条消息，所有者窗口可以通过使用给定的相关显示设备的句柄来设置静态控件的文本和背景颜色
        /// </summary>
        WM_CTLCOLORSTATIC             = 0x0138, // 十进制：312 
        WM_MOUSEFIRST                 = 0x0200, // 十进制：512 
        /// <summary>
        /// 移动鼠标
        /// </summary>
        WM_MOUSEMOVE                  = 0x0200, // 十进制：512 
        /// <summary>
        /// 按下鼠标左键
        /// </summary>
        WM_LBUTTONDOWN                = 0x0201, // 十进制：513 
        /// <summary>
        /// 释放鼠标左键
        /// </summary>
        WM_LBUTTONUP                  = 0x0202, // 十进制：514 
        /// <summary>
        /// 双击鼠标左键
        /// </summary>
        WM_LBUTTONDBLCLK              = 0x0203, // 十进制：515 
        /// <summary>
        /// 按下鼠标右键
        /// </summary>
        WM_RBUTTONDOWN                = 0x0204, // 十进制：516 
        /// <summary>
        /// 释放鼠标右键
        /// </summary>
        WM_RBUTTONUP                  = 0x0205, // 十进制：517 
        /// <summary>
        /// 双击鼠标右键
        /// </summary>
        WM_RBUTTONDBLCLK              = 0x0206, // 十进制：518 
        /// <summary>
        /// 按下鼠标中键
        /// </summary>
        WM_MBUTTONDOWN                = 0x0207, // 十进制：519 
        /// <summary>
        /// 释放鼠标中键
        /// </summary>
        WM_MBUTTONUP                  = 0x0208, // 十进制：520 
        /// <summary>
        /// 双击鼠标中键
        /// </summary>
        WM_MBUTTONDBLCLK              = 0x0209, // 十进制：521 
        WM_MOUSELAST                  = 0x0209, // 十进制：521 
        /// <summary>
        /// 当鼠标轮子转动时发送此消息个当前有焦点的控件
        /// </summary>
        WM_MOUSEWHEEL                 = 0x020a, // 十进制：522 
        WM_XBUTTONDOWN                = 0x020b, // 十进制：523 
        WM_XBUTTONUP                  = 0x020c, // 十进制：524 
        WM_XBUTTONDBLCLK              = 0x020d, // 十进制：525 
        /// <summary>
        /// 当MDI子窗口被创建或被销毁，或用户按了一下鼠标键而光标在子窗口上时发送此消息给它的父窗口
        /// </summary>
        WM_PARENTNOTIFY               = 0x0210, // 十进制：528 
        /// <summary>
        /// 发送此消息通知应用程序的主窗口that已经进入了菜单循环模式
        /// </summary>
        WM_ENTERMENULOOP              = 0x0211, // 十进制：529 
        /// <summary>
        /// 发送此消息通知应用程序的主窗口that已退出了菜单循环模式
        /// </summary>
        WM_EXITMENULOOP               = 0x0212, // 十进制：530 
        WM_NEXTMENU                   = 0x0213, // 十进制：531 
        /// <summary>
        /// 当用户正在调整窗口大小时发送此消息给窗口；通过此消息应用程序可以监视窗口大小和位置也可以修改他们
        /// </summary>
        WM_SIZING                     = 0x0214, // 十进制：532 
        /// <summary>
        /// 发送此消息给窗口当它失去捕获的鼠标时
        /// </summary>
        WM_CAPTURECHANGED             = 0x0215, // 十进制：533 
        /// <summary>
        /// 当用户在移动窗口时发送此消息，通过此消息应用程序可以监视窗口大小和位置也可以修改他们
        /// </summary>
        WM_MOVING                     = 0x0216, // 十进制：534 
        /// <summary>
        /// 此消息发送给应用程序来通知它有关电源管理事件
        /// </summary>
        WM_POWERBROADCAST             = 0x0218, // 十进制：536 
        /// <summary>
        /// 当设备的硬件配置改变时发送此消息给应用程序或设备驱动程序
        /// </summary>
        WM_DEVICECHANGE               = 0x0219, // 十进制：537 
        /// <summary>
        /// 应用程序发送此消息给多文档的客户窗口来创建一个MDI 子窗口
        /// </summary>
        WM_MDICREATE                  = 0x0220, // 十进制：544 
        /// <summary>
        /// 应用程序发送此消息给多文档的客户窗口来关闭一个MDI 子窗口
        /// </summary>
        WM_MDIDESTROY                 = 0x0221, // 十进制：545 
        /// <summary>
        /// 应用程序发送此消息给多文档的客户窗口通知客户窗口激活另一个MDI子窗口，当客户窗口收到此消息后，它发出WM_MDIACTIVE消息给MDI子窗口（未激活）激活它
        /// </summary>
        WM_MDIACTIVATE                = 0x0222, // 十进制：546 
        /// <summary>
        /// 程序 发送此消息给MDI客户窗口让子窗口从最大最小化恢复到原来大小
        /// </summary>
        WM_MDIRESTORE                 = 0x0223, // 十进制：547 
        /// <summary>
        /// 程序发送此消息给MDI客户窗口激活下一个或前一个窗口
        /// </summary>
        WM_MDINEXT                    = 0x0224, // 十进制：548 
        /// <summary>
        /// 程序发送此消息给MDI客户窗口来最大化一个MDI子窗口
        /// </summary>
        WM_MDIMAXIMIZE                = 0x0225, // 十进制：549 
        /// <summary>
        /// 程序 发送此消息给MDI客户窗口以平铺方式重新排列所有MDI子窗口
        /// </summary>
        WM_MDITILE                    = 0x0226, // 十进制：550 
        /// <summary>
        /// 程序发送此消息给MDI客户窗口以层叠方式重新排列所有MDI子窗口
        /// </summary>
        WM_MDICASCADE                 = 0x0227, // 十进制：551 
        /// <summary>
        /// 程序发送此消息给MDI客户窗口重新排列所有最小化的MDI子窗口
        /// </summary>
        WM_MDIICONARRANGE             = 0x0228, // 十进制：552 
        /// <summary>
        /// 程序发送此消息给MDI客户窗口来找到激活的子窗口的句柄
        /// </summary>
        WM_MDIGETACTIVE               = 0x0229, // 十进制：553 
        /// <summary>
        /// 程序发送此消息给MDI客户窗口用MDI菜单代替子窗口的菜单
        /// </summary>
        WM_MDISETMENU                 = 0x0230, // 十进制：560 
        WM_ENTERSIZEMOVE              = 0x0231, // 十进制：561 
        WM_EXITSIZEMOVE               = 0x0232, // 十进制：562 
        WM_DROPFILES                  = 0x0233, // 十进制：563 
        WM_MDIREFRESHMENU             = 0x0234, // 十进制：564 
        WM_IME_REPORT                 = 0x0280, // 十进制：640 
        WM_IME_SETCONTEXT             = 0x0281, // 十进制：641 
        WM_IME_NOTIFY                 = 0x0282, // 十进制：642 
        WM_IME_CONTROL                = 0x0283, // 十进制：643 
        WM_IME_COMPOSITIONFULL        = 0x0284, // 十进制：644 
        WM_IME_SELECT                 = 0x0285, // 十进制：645 
        WM_IME_CHAR                   = 0x0286, // 十进制：646 
        WM_IME_REQUEST                = 0x0288, // 十进制：648 
        WM_IMEKEYDOWN                 = 0x0290, // 十进制：656 
        WM_IME_KEYDOWN                = 0x0290, // 十进制：656 
        WM_IMEKEYUP                   = 0x0291, // 十进制：657 
        WM_IME_KEYUP                  = 0x0291, // 十进制：657 
        WM_NCMOUSEHOVER               = 0x02a0, // 十进制：672 
        WM_MOUSEHOVER                 = 0x02a1, // 十进制：673 
        WM_NCMOUSELEAVE               = 0x02a2, // 十进制：674 
        WM_MOUSELEAVE                 = 0x02a3, // 十进制：675 
        /// <summary>
        /// 程序发送此消息给一个编辑框或combobox来删除当前选择的文本
        /// </summary>
        WM_CUT                        = 0x0300, // 十进制：768 
        /// <summary>
        /// 程序发送此消息给一个编辑框或combobox来复制当前选择的文本到剪贴板
        /// </summary>
        WM_COPY                       = 0x0301, // 十进制：769 
        /// <summary>
        /// 程序发送此消息给editcontrol或combobox从剪贴板中得到数据
        /// </summary>
        WM_PASTE                      = 0x0302, // 十进制：770 
        /// <summary>
        /// 程序发送此消息给editcontrol或combobox清除当前选择的内容
        /// </summary>
        WM_CLEAR                      = 0x0303, // 十进制：771 
        /// <summary>
        /// 程序发送此消息给editcontrol或combobox撤消最后一次操作
        /// </summary>
        WM_UNDO                       = 0x0304, // 十进制：772 
        WM_RENDERFORMAT               = 0x0305, // 十进制：773 
        WM_RENDERALLFORMATS           = 0x0306, // 十进制：774 
        /// <summary>
        /// 当调用ENPTYCLIPBOARD函数时 发送此消息给剪贴板的所有者
        /// </summary>
        WM_DESTROYCLIPBOARD           = 0x0307, // 十进制：775 
        /// <summary>
        /// 当剪贴板的内容变化时发送此消息给剪贴板观察链的第一个窗口；它允许用剪贴板观察窗口来显示剪贴板的新内容
        /// </summary>
        WM_DRAWCLIPBOARD              = 0x0308, // 十进制：776 
        /// <summary>
        /// 当剪贴板包含CF_OWNERDIPLAY格式的数据并且剪贴板观察窗口的客户区需要重画
        /// </summary>
        WM_PAINTCLIPBOARD             = 0x0309, // 十进制：777 
        WM_VSCROLLCLIPBOARD           = 0x030a, // 十进制：778 
        /// <summary>
        /// 当剪贴板包含CF_OWNERDIPLAY格式的数据并且剪贴板观察窗口的客户区域的大小已经改变是此消息通过剪贴板观察窗口发送给剪贴板的所有者
        /// </summary>
        WM_SIZECLIPBOARD              = 0x030b, // 十进制：779 
        /// <summary>
        /// 通过剪贴板观察窗口发送此消息给剪贴板的所有者来请求一个CF_OWNERDISPLAY格式的剪贴板的名字
        /// </summary>
        WM_ASKCBFORMATNAME            = 0x030c, // 十进制：780 
        /// <summary>
        /// 当一个窗口从剪贴板观察链中移去时发送此消息给剪贴板观察链的第一个窗口
        /// </summary>
        WM_CHANGECBCHAIN              = 0x030d, // 十进制：781 
        /// <summary>
        /// 此消息通过一个剪贴板观察窗口发送给剪贴板的所有者 ；它发生在当剪贴板包含CFOWNERDISPALY格式的数据并且有个事件在剪贴板观察窗的水平滚动条上；所有者应滚动剪贴板图象并更新滚动条的值
        /// </summary>
        WM_HSCROLLCLIPBOARD           = 0x030e, // 十进制：782 
        /// <summary>
        /// 此消息发送给将要收到焦点的窗口，此消息能使窗口在收到焦点时同时有机会实现他的逻辑调色板
        /// </summary>
        WM_QUERYNEWPALETTE            = 0x030f, // 十进制：783 
        /// <summary>
        /// 当一个应用程序正要实现它的逻辑调色板时发此消息通知所有的应用程序
        /// </summary>
        WM_PALETTEISCHANGING          = 0x0310, // 十进制：784 
        /// <summary>
        /// 此消息在一个拥有焦点的窗口实现它的逻辑调色板后发送此消息给所有顶级并重叠的窗口，以此来改变系统调色板
        /// </summary>
        WM_PALETTECHANGED             = 0x0311, // 十进制：785 
        /// <summary>
        /// 当用户按下由REGISTERHOTKEY函数注册的热键时提交此消息
        /// </summary>
        WM_HOTKEY                     = 0x0312, // 十进制：786 
        /// <summary>
        /// 应用程序发送此消息仅当WINDOWS或其它应用程序发出一个请求要求绘制一个应用程序的一部分
        /// </summary>
        WM_PRINT                      = 0x0317, // 十进制：791 
        WM_PRINTCLIENT                = 0x0318, // 十进制：792 
        WM_APPCOMMAND                 = 0x0319, // 十进制：793 
        WM_HANDHELDFIRST              = 0x0358, // 十进制：856 
        WM_HANDHELDLAST               = 0x035f, // 十进制：863 
        WM_AFXFIRST                   = 0x0360, // 十进制：864 
        WM_AFXLAST                    = 0x037f, // 十进制：895 
        WM_PENWINFIRST                = 0x0380, // 十进制：896 
        WM_RCRESULT                   = 0x0381, // 十进制：897 
        WM_HOOKRCRESULT               = 0x0382, // 十进制：898 
        WM_GLOBALRCCHANGE             = 0x0383, // 十进制：899 
        WM_PENMISCINFO                = 0x0383, // 十进制：899 
        WM_SKB                        = 0x0384, // 十进制：900 
        WM_HEDITCTL                   = 0x0385, // 十进制：901 
        WM_PENCTL                     = 0x0385, // 十进制：901 
        WM_PENMISC                    = 0x0386, // 十进制：902 
        WM_CTLINIT                    = 0x0387, // 十进制：903 
        WM_PENEVENT                   = 0x0388, // 十进制：904 
        WM_PENWINLAST                 = 0x038f, // 十进制：911 
        WM_DDE_FIRST                  = 0x03E0,
        /// <summary>
        /// 一个DDE客户程序提交此消息开始一个与服务器程序的会话来响应那个指定的程序和主题名
        /// </summary>
        WM_DDE_INITIATE               = WM_DDE_FIRST + 0,
        /// <summary>
        /// 一个DDE应用程序（无论是客户还是服务器）提交此消息来终止一个会话
        /// </summary>
        WM_DDE_TERMINATE              = WM_DDE_FIRST + 1,
        /// <summary>
        /// 一个DDE客户程序提交此消息给一个DDE服务程序来请求服务器每当数据项改变时更新它
        /// </summary>
        WM_DDE_ADVISE                 = WM_DDE_FIRST + 2,
        /// <summary>
        /// 一个DDE客户程序通过此消息通知一个DDE服务程序不更新指定的项或一个特殊的剪贴板格式的项
        /// </summary>
        WM_DDE_UNADVISE               = WM_DDE_FIRST + 3,
        /// <summary>
        /// 此消息通知一个DDE（动态数据交换）程序已收到并正在处理WM_DDE_POKE, WM_DDE_EXECUTE, WM_DDE_DATA, WM_DDE_ADVISE, WM_DDE_UNADVISE, or WM_DDE_INITIAT消息
        /// </summary>
        WM_DDE_ACK                    = WM_DDE_FIRST + 4,
        /// <summary>
        /// 一个DDE服务程序提交此消息给DDE客户程序来传递个一数据项给客户或通知客户的一条可用数据项
        /// </summary>
        WM_DDE_DATA                   = WM_DDE_FIRST + 5,
        /// <summary>
        /// 一个DDE客户程序提交此消息给一个DDE服务程序来请求一个数据项的值
        /// </summary>
        WM_DDE_REQUEST                = WM_DDE_FIRST + 6,
        /// <summary>
        /// 一个DDE客户程序提交此消息给一个DDE服务程序，客户使用此消息来请求服务器接收一个未经同意的数据项；服务器通过答复WM_DDE_ACK消息提示是否它接收这个数据项
        /// </summary>
        WM_DDE_POKE                   = WM_DDE_FIRST + 7,
        /// <summary>
        /// 一个DDE客户程序提交此消息给一个DDE服务程序来发送一个字符串给服务器让它象串行命令一样被处理，服务器通过提交WM_DDE_ACK消息来作回应
        /// </summary>
        WM_DDE_EXECUTE                = WM_DDE_FIRST + 8,
        WM_DDE_LAST                   = WM_DDE_FIRST + 8,
        DDM_SETFMT                    = 0x0400, // 十进制：1024 
        DM_GETDEFID                   = 0x0400, // 十进制：1024 
        NIN_SELECT                    = 0x0400, // 十进制：1024 
        TBM_GETPOS                    = 0x0400, // 十进制：1024 
        WM_PSD_PAGESETUPDLG           = 0x0400, // 十进制：1024 
        /// <summary>
        /// 私有窗口类使用整数消息。此消息能帮助应用程序自定义私有消息
        /// </summary>
        WM_USER                       = 0x0400, // 十进制：1024 
        CBEM_INSERTITEMA              = 0x0401, // 十进制：1025 
        DDM_DRAW                      = 0x0401, // 十进制：1025 
        DM_SETDEFID                   = 0x0401, // 十进制：1025 
        HKM_SETHOTKEY                 = 0x0401, // 十进制：1025 
        PBM_SETRANGE                  = 0x0401, // 十进制：1025 
        RB_INSERTBANDA                = 0x0401, // 十进制：1025 
        SB_SETTEXTA                   = 0x0401, // 十进制：1025 
        TB_ENABLEBUTTON               = 0x0401, // 十进制：1025 
        TBM_GETRANGEMIN               = 0x0401, // 十进制：1025 
        TTM_ACTIVATE                  = 0x0401, // 十进制：1025 
        WM_CHOOSEFONT_GETLOGFONT      = 0x0401, // 十进制：1025 
        WM_PSD_FULLPAGERECT           = 0x0401, // 十进制：1025 
        CBEM_SETIMAGELIST             = 0x0402, // 十进制：1026 
        DDM_CLOSE                     = 0x0402, // 十进制：1026 
        DM_REPOSITION                 = 0x0402, // 十进制：1026 
        HKM_GETHOTKEY                 = 0x0402, // 十进制：1026 
        PBM_SETPOS                    = 0x0402, // 十进制：1026 
        RB_DELETEBAND                 = 0x0402, // 十进制：1026 
        SB_GETTEXTA                   = 0x0402, // 十进制：1026 
        TB_CHECKBUTTON                = 0x0402, // 十进制：1026 
        TBM_GETRANGEMAX               = 0x0402, // 十进制：1026 
        WM_PSD_MINMARGINRECT          = 0x0402, // 十进制：1026 
        CBEM_GETIMAGELIST             = 0x0403, // 十进制：1027 
        DDM_BEGIN                     = 0x0403, // 十进制：1027 
        HKM_SETRULES                  = 0x0403, // 十进制：1027 
        PBM_DELTAPOS                  = 0x0403, // 十进制：1027 
        RB_GETBARINFO                 = 0x0403, // 十进制：1027 
        SB_GETTEXTLENGTHA             = 0x0403, // 十进制：1027 
        TBM_GETTIC                    = 0x0403, // 十进制：1027 
        TB_PRESSBUTTON                = 0x0403, // 十进制：1027 
        TTM_SETDELAYTIME              = 0x0403, // 十进制：1027 
        WM_PSD_MARGINRECT             = 0x0403, // 十进制：1027 
        CBEM_GETITEMA                 = 0x0404, // 十进制：1028 
        DDM_END                       = 0x0404, // 十进制：1028 
        PBM_SETSTEP                   = 0x0404, // 十进制：1028 
        RB_SETBARINFO                 = 0x0404, // 十进制：1028 
        SB_SETPARTS                   = 0x0404, // 十进制：1028 
        TB_HIDEBUTTON                 = 0x0404, // 十进制：1028 
        TBM_SETTIC                    = 0x0404, // 十进制：1028 
        TTM_ADDTOOLA                  = 0x0404, // 十进制：1028 
        WM_PSD_GREEKTEXTRECT          = 0x0404, // 十进制：1028 
        CBEM_SETITEMA                 = 0x0405, // 十进制：1029 
        PBM_STEPIT                    = 0x0405, // 十进制：1029 
        TB_INDETERMINATE              = 0x0405, // 十进制：1029 
        TBM_SETPOS                    = 0x0405, // 十进制：1029 
        TTM_DELTOOLA                  = 0x0405, // 十进制：1029 
        WM_PSD_ENVSTAMPRECT           = 0x0405, // 十进制：1029 
        CBEM_GETCOMBOCONTROL          = 0x0406, // 十进制：1030 
        PBM_SETRANGE32                = 0x0406, // 十进制：1030 
        RB_SETBANDINFOA               = 0x0406, // 十进制：1030 
        SB_GETPARTS                   = 0x0406, // 十进制：1030 
        TB_MARKBUTTON                 = 0x0406, // 十进制：1030 
        TBM_SETRANGE                  = 0x0406, // 十进制：1030 
        TTM_NEWTOOLRECTA              = 0x0406, // 十进制：1030 
        WM_PSD_YAFULLPAGERECT         = 0x0406, // 十进制：1030 
        CBEM_GETEDITCONTROL           = 0x0407, // 十进制：1031 
        PBM_GETRANGE                  = 0x0407, // 十进制：1031 
        RB_SETPARENT                  = 0x0407, // 十进制：1031 
        SB_GETBORDERS                 = 0x0407, // 十进制：1031 
        TBM_SETRANGEMIN               = 0x0407, // 十进制：1031 
        TTM_RELAYEVENT                = 0x0407, // 十进制：1031 
        CBEM_SETEXSTYLE               = 0x0408, // 十进制：1032 
        PBM_GETPOS                    = 0x0408, // 十进制：1032 
        RB_HITTEST                    = 0x0408, // 十进制：1032 
        SB_SETMINHEIGHT               = 0x0408, // 十进制：1032 
        TBM_SETRANGEMAX               = 0x0408, // 十进制：1032 
        TTM_GETTOOLINFOA              = 0x0408, // 十进制：1032 
        CBEM_GETEXSTYLE               = 0x0409, // 十进制：1033 
        CBEM_GETEXTENDEDSTYLE         = 0x0409, // 十进制：1033 
        PBM_SETBARCOLOR               = 0x0409, // 十进制：1033 
        RB_GETRECT                    = 0x0409, // 十进制：1033 
        SB_SIMPLE                     = 0x0409, // 十进制：1033 
        TB_ISBUTTONENABLED            = 0x0409, // 十进制：1033 
        TBM_CLEARTICS                 = 0x0409, // 十进制：1033 
        TTM_SETTOOLINFOA              = 0x0409, // 十进制：1033 
        CBEM_HASEDITCHANGED           = 0x040a, // 十进制：1034 
        RB_INSERTBANDW                = 0x040a, // 十进制：1034 
        SB_GETRECT                    = 0x040a, // 十进制：1034 
        TB_ISBUTTONCHECKED            = 0x040a, // 十进制：1034 
        TBM_SETSEL                    = 0x040a, // 十进制：1034 
        TTM_HITTESTA                  = 0x040a, // 十进制：1034 
        WIZ_QUERYNUMPAGES             = 0x040a, // 十进制：1034 
        CBEM_INSERTITEMW              = 0x040b, // 十进制：1035 
        RB_SETBANDINFOW               = 0x040b, // 十进制：1035 
        SB_SETTEXTW                   = 0x040b, // 十进制：1035 
        TB_ISBUTTONPRESSED            = 0x040b, // 十进制：1035 
        TBM_SETSELSTART               = 0x040b, // 十进制：1035 
        TTM_GETTEXTA                  = 0x040b, // 十进制：1035 
        WIZ_NEXT                      = 0x040b, // 十进制：1035 
        CBEM_SETITEMW                 = 0x040c, // 十进制：1036 
        RB_GETBANDCOUNT               = 0x040c, // 十进制：1036 
        SB_GETTEXTLENGTHW             = 0x040c, // 十进制：1036 
        TB_ISBUTTONHIDDEN             = 0x040c, // 十进制：1036 
        TBM_SETSELEND                 = 0x040c, // 十进制：1036 
        TTM_UPDATETIPTEXTA            = 0x040c, // 十进制：1036 
        WIZ_PREV                      = 0x040c, // 十进制：1036 
        CBEM_GETITEMW                 = 0x040d, // 十进制：1037 
        RB_GETROWCOUNT                = 0x040d, // 十进制：1037 
        SB_GETTEXTW                   = 0x040d, // 十进制：1037 
        TB_ISBUTTONINDETERMINATE      = 0x040d, // 十进制：1037 
        TTM_GETTOOLCOUNT              = 0x040d, // 十进制：1037 
        CBEM_SETEXTENDEDSTYLE         = 0x040e, // 十进制：1038 
        RB_GETROWHEIGHT               = 0x040e, // 十进制：1038 
        SB_ISSIMPLE                   = 0x040e, // 十进制：1038 
        TB_ISBUTTONHIGHLIGHTED        = 0x040e, // 十进制：1038 
        TBM_GETPTICS                  = 0x040e, // 十进制：1038 
        TTM_ENUMTOOLSA                = 0x040e, // 十进制：1038 
        SB_SETICON                    = 0x040f, // 十进制：1039 
        TBM_GETTICPOS                 = 0x040f, // 十进制：1039 
        TTM_GETCURRENTTOOLA           = 0x040f, // 十进制：1039 
        RB_IDTOINDEX                  = 0x0410, // 十进制：1040 
        SB_SETTIPTEXTA                = 0x0410, // 十进制：1040 
        TBM_GETNUMTICS                = 0x0410, // 十进制：1040 
        TTM_WINDOWFROMPOINT           = 0x0410, // 十进制：1040 
        RB_GETTOOLTIPS                = 0x0411, // 十进制：1041 
        SB_SETTIPTEXTW                = 0x0411, // 十进制：1041 
        TBM_GETSELSTART               = 0x0411, // 十进制：1041 
        TB_SETSTATE                   = 0x0411, // 十进制：1041 
        TTM_TRACKACTIVATE             = 0x0411, // 十进制：1041 
        RB_SETTOOLTIPS                = 0x0412, // 十进制：1042 
        SB_GETTIPTEXTA                = 0x0412, // 十进制：1042 
        TB_GETSTATE                   = 0x0412, // 十进制：1042 
        TBM_GETSELEND                 = 0x0412, // 十进制：1042 
        TTM_TRACKPOSITION             = 0x0412, // 十进制：1042 
        RB_SETBKCOLOR                 = 0x0413, // 十进制：1043 
        SB_GETTIPTEXTW                = 0x0413, // 十进制：1043 
        TB_ADDBITMAP                  = 0x0413, // 十进制：1043 
        TBM_CLEARSEL                  = 0x0413, // 十进制：1043 
        TTM_SETTIPBKCOLOR             = 0x0413, // 十进制：1043 
        RB_GETBKCOLOR                 = 0x0414, // 十进制：1044 
        SB_GETICON                    = 0x0414, // 十进制：1044 
        TB_ADDBUTTONSA                = 0x0414, // 十进制：1044 
        TBM_SETTICFREQ                = 0x0414, // 十进制：1044 
        TTM_SETTIPTEXTCOLOR           = 0x0414, // 十进制：1044 
        RB_SETTEXTCOLOR               = 0x0415, // 十进制：1045 
        TB_INSERTBUTTONA              = 0x0415, // 十进制：1045 
        TBM_SETPAGESIZE               = 0x0415, // 十进制：1045 
        TTM_GETDELAYTIME              = 0x0415, // 十进制：1045 
        RB_GETTEXTCOLOR               = 0x0416, // 十进制：1046 
        TB_DELETEBUTTON               = 0x0416, // 十进制：1046 
        TBM_GETPAGESIZE               = 0x0416, // 十进制：1046 
        TTM_GETTIPBKCOLOR             = 0x0416, // 十进制：1046 
        RB_SIZETORECT                 = 0x0417, // 十进制：1047 
        TB_GETBUTTON                  = 0x0417, // 十进制：1047 
        TBM_SETLINESIZE               = 0x0417, // 十进制：1047 
        TTM_GETTIPTEXTCOLOR           = 0x0417, // 十进制：1047 
        RB_BEGINDRAG                  = 0x0418, // 十进制：1048 
        TB_BUTTONCOUNT                = 0x0418, // 十进制：1048 
        TBM_GETLINESIZE               = 0x0418, // 十进制：1048 
        TTM_SETMAXTIPWIDTH            = 0x0418, // 十进制：1048 
        RB_ENDDRAG                    = 0x0419, // 十进制：1049 
        TB_COMMANDTOINDEX             = 0x0419, // 十进制：1049 
        TBM_GETTHUMBRECT              = 0x0419, // 十进制：1049 
        TTM_GETMAXTIPWIDTH            = 0x0419, // 十进制：1049 
        RB_DRAGMOVE                   = 0x041a, // 十进制：1050 
        TBM_GETCHANNELRECT            = 0x041a, // 十进制：1050 
        TB_SAVERESTOREA               = 0x041a, // 十进制：1050 
        TTM_SETMARGIN                 = 0x041a, // 十进制：1050 
        RB_GETBARHEIGHT               = 0x041b, // 十进制：1051 
        TB_CUSTOMIZE                  = 0x041b, // 十进制：1051 
        TBM_SETTHUMBLENGTH            = 0x041b, // 十进制：1051 
        TTM_GETMARGIN                 = 0x041b, // 十进制：1051 
        RB_GETBANDINFOW               = 0x041c, // 十进制：1052 
        TB_ADDSTRINGA                 = 0x041c, // 十进制：1052 
        TBM_GETTHUMBLENGTH            = 0x041c, // 十进制：1052 
        TTM_POP                       = 0x041c, // 十进制：1052 
        RB_GETBANDINFOA               = 0x041d, // 十进制：1053 
        TB_GETITEMRECT                = 0x041d, // 十进制：1053 
        TBM_SETTOOLTIPS               = 0x041d, // 十进制：1053 
        TTM_UPDATE                    = 0x041d, // 十进制：1053 
        RB_MINIMIZEBAND               = 0x041e, // 十进制：1054 
        TB_BUTTONSTRUCTSIZE           = 0x041e, // 十进制：1054 
        TBM_GETTOOLTIPS               = 0x041e, // 十进制：1054 
        TTM_GETBUBBLESIZE             = 0x041e, // 十进制：1054 
        RB_MAXIMIZEBAND               = 0x041f, // 十进制：1055 
        TBM_SETTIPSIDE                = 0x041f, // 十进制：1055 
        TB_SETBUTTONSIZE              = 0x041f, // 十进制：1055 
        TTM_ADJUSTRECT                = 0x041f, // 十进制：1055 
        TBM_SETBUDDY                  = 0x0420, // 十进制：1056 
        TB_SETBITMAPSIZE              = 0x0420, // 十进制：1056 
        TTM_SETTITLEA                 = 0x0420, // 十进制：1056 
        MSG_FTS_JUMP_VA               = 0x0421, // 十进制：1057 
        TB_AUTOSIZE                   = 0x0421, // 十进制：1057 
        TBM_GETBUDDY                  = 0x0421, // 十进制：1057 
        TTM_SETTITLEW                 = 0x0421, // 十进制：1057 
        RB_GETBANDBORDERS             = 0x0422, // 十进制：1058 
        MSG_FTS_JUMP_QWORD            = 0x0423, // 十进制：1059 
        RB_SHOWBAND                   = 0x0423, // 十进制：1059 
        TB_GETTOOLTIPS                = 0x0423, // 十进制：1059 
        MSG_REINDEX_REQUEST           = 0x0424, // 十进制：1060 
        TB_SETTOOLTIPS                = 0x0424, // 十进制：1060 
        MSG_FTS_WHERE_IS_IT           = 0x0425, // 十进制：1061 
        RB_SETPALETTE                 = 0x0425, // 十进制：1061 
        TB_SETPARENT                  = 0x0425, // 十进制：1061 
        RB_GETPALETTE                 = 0x0426, // 十进制：1062 
        RB_MOVEBAND                   = 0x0427, // 十进制：1063 
        TB_SETROWS                    = 0x0427, // 十进制：1063 
        TB_GETROWS                    = 0x0428, // 十进制：1064 
        TB_GETBITMAPFLAGS             = 0x0429, // 十进制：1065 
        TB_SETCMDID                   = 0x042a, // 十进制：1066 
        RB_PUSHCHEVRON                = 0x042b, // 十进制：1067 
        TB_CHANGEBITMAP               = 0x042b, // 十进制：1067 
        TB_GETBITMAP                  = 0x042c, // 十进制：1068 
        MSG_GET_DEFFONT               = 0x042d, // 十进制：1069 
        TB_GETBUTTONTEXTA             = 0x042d, // 十进制：1069 
        TB_REPLACEBITMAP              = 0x042e, // 十进制：1070 
        TB_SETINDENT                  = 0x042f, // 十进制：1071 
        TB_SETIMAGELIST               = 0x0430, // 十进制：1072 
        TB_GETIMAGELIST               = 0x0431, // 十进制：1073 
        TB_LOADIMAGES                 = 0x0432, // 十进制：1074 
        TTM_ADDTOOLW                  = 0x0432, // 十进制：1074 
        TB_GETRECT                    = 0x0433, // 十进制：1075 
        TTM_DELTOOLW                  = 0x0433, // 十进制：1075 
        TB_SETHOTIMAGELIST            = 0x0434, // 十进制：1076 
        TTM_NEWTOOLRECTW              = 0x0434, // 十进制：1076 
        TB_GETHOTIMAGELIST            = 0x0435, // 十进制：1077 
        TTM_GETTOOLINFOW              = 0x0435, // 十进制：1077 
        TB_SETDISABLEDIMAGELIST       = 0x0436, // 十进制：1078 
        TTM_SETTOOLINFOW              = 0x0436, // 十进制：1078 
        TB_GETDISABLEDIMAGELIST       = 0x0437, // 十进制：1079 
        TTM_HITTESTW                  = 0x0437, // 十进制：1079 
        TB_SETSTYLE                   = 0x0438, // 十进制：1080 
        TTM_GETTEXTW                  = 0x0438, // 十进制：1080 
        TB_GETSTYLE                   = 0x0439, // 十进制：1081 
        TTM_UPDATETIPTEXTW            = 0x0439, // 十进制：1081 
        TB_GETBUTTONSIZE              = 0x043a, // 十进制：1082 
        TTM_ENUMTOOLSW                = 0x043a, // 十进制：1082 
        TB_SETBUTTONWIDTH             = 0x043b, // 十进制：1083 
        TTM_GETCURRENTTOOLW           = 0x043b, // 十进制：1083 
        TB_SETMAXTEXTROWS             = 0x043c, // 十进制：1084 
        TB_GETTEXTROWS                = 0x043d, // 十进制：1085 
        TB_GETOBJECT                  = 0x043e, // 十进制：1086 
        TB_GETBUTTONINFOW             = 0x043f, // 十进制：1087 
        TB_SETBUTTONINFOW             = 0x0440, // 十进制：1088 
        TB_GETBUTTONINFOA             = 0x0441, // 十进制：1089 
        TB_SETBUTTONINFOA             = 0x0442, // 十进制：1090 
        TB_INSERTBUTTONW              = 0x0443, // 十进制：1091 
        TB_ADDBUTTONSW                = 0x0444, // 十进制：1092 
        TB_HITTEST                    = 0x0445, // 十进制：1093 
        TB_SETDRAWTEXTFLAGS           = 0x0446, // 十进制：1094 
        TB_GETHOTITEM                 = 0x0447, // 十进制：1095 
        TB_SETHOTITEM                 = 0x0448, // 十进制：1096 
        TB_SETANCHORHIGHLIGHT         = 0x0449, // 十进制：1097 
        TB_GETANCHORHIGHLIGHT         = 0x044a, // 十进制：1098 
        TB_GETBUTTONTEXTW             = 0x044b, // 十进制：1099 
        TB_SAVERESTOREW               = 0x044c, // 十进制：1100 
        TB_ADDSTRINGW                 = 0x044d, // 十进制：1101 
        TB_MAPACCELERATORA            = 0x044e, // 十进制：1102 
        TB_GETINSERTMARK              = 0x044f, // 十进制：1103 
        TB_SETINSERTMARK              = 0x0450, // 十进制：1104 
        TB_INSERTMARKHITTEST          = 0x0451, // 十进制：1105 
        TB_MOVEBUTTON                 = 0x0452, // 十进制：1106 
        TB_GETMAXSIZE                 = 0x0453, // 十进制：1107 
        TB_SETEXTENDEDSTYLE           = 0x0454, // 十进制：1108 
        TB_GETEXTENDEDSTYLE           = 0x0455, // 十进制：1109 
        TB_GETPADDING                 = 0x0456, // 十进制：1110 
        TB_SETPADDING                 = 0x0457, // 十进制：1111 
        TB_SETINSERTMARKCOLOR         = 0x0458, // 十进制：1112 
        TB_GETINSERTMARKCOLOR         = 0x0459, // 十进制：1113 
        TB_MAPACCELERATORW            = 0x045a, // 十进制：1114 
        TB_GETSTRINGW                 = 0x045b, // 十进制：1115 
        TB_GETSTRINGA                 = 0x045c, // 十进制：1116 
        TAPI_REPLY                    = 0x0463, // 十进制：1123 
        ACM_OPENA                     = 0x0464, // 十进制：1124 
        BFFM_SETSTATUSTEXTA           = 0x0464, // 十进制：1124 
        CDM_FIRST                     = 0x0464, // 十进制：1124 
        CDM_GETSPEC                   = 0x0464, // 十进制：1124 
        IPM_CLEARADDRESS              = 0x0464, // 十进制：1124 
        WM_CAP_UNICODE_START          = 0x0464, // 十进制：1124 
        ACM_PLAY                      = 0x0465, // 十进制：1125 
        BFFM_ENABLEOK                 = 0x0465, // 十进制：1125 
        CDM_GETFILEPATH               = 0x0465, // 十进制：1125 
        IPM_SETADDRESS                = 0x0465, // 十进制：1125 
        PSM_SETCURSEL                 = 0x0465, // 十进制：1125 
        UDM_SETRANGE                  = 0x0465, // 十进制：1125 
        WM_CHOOSEFONT_SETLOGFONT      = 0x0465, // 十进制：1125 
        ACM_STOP                      = 0x0466, // 十进制：1126 
        BFFM_SETSELECTIONA            = 0x0466, // 十进制：1126 
        CDM_GETFOLDERPATH             = 0x0466, // 十进制：1126 
        IPM_GETADDRESS                = 0x0466, // 十进制：1126 
        PSM_REMOVEPAGE                = 0x0466, // 十进制：1126 
        UDM_GETRANGE                  = 0x0466, // 十进制：1126 
        WM_CAP_SET_CALLBACK_ERRORW    = 0x0466, // 十进制：1126 
        WM_CHOOSEFONT_SETFLAGS        = 0x0466, // 十进制：1126 
        ACM_OPENW                     = 0x0467, // 十进制：1127 
        BFFM_SETSELECTIONW            = 0x0467, // 十进制：1127 
        CDM_GETFOLDERIDLIST           = 0x0467, // 十进制：1127 
        IPM_SETRANGE                  = 0x0467, // 十进制：1127 
        PSM_ADDPAGE                   = 0x0467, // 十进制：1127 
        UDM_SETPOS                    = 0x0467, // 十进制：1127 
        WM_CAP_SET_CALLBACK_STATUSW   = 0x0467, // 十进制：1127 
        BFFM_SETSTATUSTEXTW           = 0x0468, // 十进制：1128 
        CDM_SETCONTROLTEXT            = 0x0468, // 十进制：1128 
        IPM_SETFOCUS                  = 0x0468, // 十进制：1128 
        PSM_CHANGED                   = 0x0468, // 十进制：1128 
        UDM_GETPOS                    = 0x0468, // 十进制：1128 
        CDM_HIDECONTROL               = 0x0469, // 十进制：1129 
        IPM_ISBLANK                   = 0x0469, // 十进制：1129 
        PSM_RESTARTWINDOWS            = 0x0469, // 十进制：1129 
        UDM_SETBUDDY                  = 0x0469, // 十进制：1129 
        CDM_SETDEFEXT                 = 0x046a, // 十进制：1130 
        PSM_REBOOTSYSTEM              = 0x046a, // 十进制：1130 
        UDM_GETBUDDY                  = 0x046a, // 十进制：1130 
        PSM_CANCELTOCLOSE             = 0x046b, // 十进制：1131 
        UDM_SETACCEL                  = 0x046b, // 十进制：1131 
        EM_CONVPOSITION               = 0x046c, // 十进制：1132 
        PSM_QUERYSIBLINGS             = 0x046c, // 十进制：1132 
        UDM_GETACCEL                  = 0x046c, // 十进制：1132 
        MCIWNDM_GETZOOM               = 0x046d, // 十进制：1133 
        PSM_UNCHANGED                 = 0x046d, // 十进制：1133 
        UDM_SETBASE                   = 0x046d, // 十进制：1133 
        PSM_APPLY                     = 0x046e, // 十进制：1134 
        UDM_GETBASE                   = 0x046e, // 十进制：1134 
        PSM_SETTITLEA                 = 0x046f, // 十进制：1135 
        UDM_SETRANGE32                = 0x046f, // 十进制：1135 
        PSM_SETWIZBUTTONS             = 0x0470, // 十进制：1136 
        UDM_GETRANGE32                = 0x0470, // 十进制：1136 
        WM_CAP_DRIVER_GET_NAMEW       = 0x0470, // 十进制：1136 
        PSM_PRESSBUTTON               = 0x0471, // 十进制：1137 
        UDM_SETPOS32                  = 0x0471, // 十进制：1137 
        WM_CAP_DRIVER_GET_VERSIONW    = 0x0471, // 十进制：1137 
        PSM_SETCURSELID               = 0x0472, // 十进制：1138 
        UDM_GETPOS32                  = 0x0472, // 十进制：1138 
        PSM_SETFINISHTEXTA            = 0x0473, // 十进制：1139 
        PSM_GETTABCONTROL             = 0x0474, // 十进制：1140 
        PSM_ISDIALOGMESSAGE           = 0x0475, // 十进制：1141 
        MCIWNDM_REALIZE               = 0x0476, // 十进制：1142 
        PSM_GETCURRENTPAGEHWND        = 0x0476, // 十进制：1142 
        MCIWNDM_SETTIMEFORMATA        = 0x0477, // 十进制：1143 
        PSM_INSERTPAGE                = 0x0477, // 十进制：1143 
        MCIWNDM_GETTIMEFORMATA        = 0x0478, // 十进制：1144 
        PSM_SETTITLEW                 = 0x0478, // 十进制：1144 
        WM_CAP_FILE_SET_CAPTURE_FILEW = 0x0478, // 十进制：1144 
        MCIWNDM_VALIDATEMEDIA         = 0x0479, // 十进制：1145 
        PSM_SETFINISHTEXTW            = 0x0479, // 十进制：1145 
        WM_CAP_FILE_GET_CAPTURE_FILEW = 0x0479, // 十进制：1145 
        MCIWNDM_PLAYTO                = 0x047b, // 十进制：1147 
        WM_CAP_FILE_SAVEASW           = 0x047b, // 十进制：1147 
        MCIWNDM_GETFILENAMEA          = 0x047c, // 十进制：1148 
        MCIWNDM_GETDEVICEA            = 0x047d, // 十进制：1149 
        PSM_SETHEADERTITLEA           = 0x047d, // 十进制：1149 
        WM_CAP_FILE_SAVEDIBW          = 0x047d, // 十进制：1149 
        MCIWNDM_GETPALETTE            = 0x047e, // 十进制：1150 
        PSM_SETHEADERTITLEW           = 0x047e, // 十进制：1150 
        MCIWNDM_SETPALETTE            = 0x047f, // 十进制：1151 
        PSM_SETHEADERSUBTITLEA        = 0x047f, // 十进制：1151 
        MCIWNDM_GETERRORA             = 0x0480, // 十进制：1152 
        PSM_SETHEADERSUBTITLEW        = 0x0480, // 十进制：1152 
        PSM_HWNDTOINDEX               = 0x0481, // 十进制：1153 
        PSM_INDEXTOHWND               = 0x0482, // 十进制：1154 
        MCIWNDM_SETINACTIVETIMER      = 0x0483, // 十进制：1155 
        PSM_PAGETOINDEX               = 0x0483, // 十进制：1155 
        PSM_INDEXTOPAGE               = 0x0484, // 十进制：1156 
        DL_BEGINDRAG                  = 0x0485, // 十进制：1157 
        MCIWNDM_GETINACTIVETIMER      = 0x0485, // 十进制：1157 
        PSM_IDTOINDEX                 = 0x0485, // 十进制：1157 
        DL_DRAGGING                   = 0x0486, // 十进制：1158 
        PSM_INDEXTOID                 = 0x0486, // 十进制：1158 
        DL_DROPPED                    = 0x0487, // 十进制：1159 
        PSM_GETRESULT                 = 0x0487, // 十进制：1159 
        DL_CANCELDRAG                 = 0x0488, // 十进制：1160 
        PSM_RECALCPAGESIZES           = 0x0488, // 十进制：1160 
        MCIWNDM_GET_SOURCE            = 0x048c, // 十进制：1164 
        MCIWNDM_PUT_SOURCE            = 0x048d, // 十进制：1165 
        MCIWNDM_GET_DEST              = 0x048e, // 十进制：1166 
        MCIWNDM_PUT_DEST              = 0x048f, // 十进制：1167 
        MCIWNDM_CAN_PLAY              = 0x0490, // 十进制：1168 
        MCIWNDM_CAN_WINDOW            = 0x0491, // 十进制：1169 
        MCIWNDM_CAN_RECORD            = 0x0492, // 十进制：1170 
        MCIWNDM_CAN_SAVE              = 0x0493, // 十进制：1171 
        MCIWNDM_CAN_EJECT             = 0x0494, // 十进制：1172 
        MCIWNDM_CAN_CONFIG            = 0x0495, // 十进制：1173 
        IE_GETINK                     = 0x0496, // 十进制：1174 
        IE_MSGFIRST                   = 0x0496, // 十进制：1174 
        MCIWNDM_PALETTEKICK           = 0x0496, // 十进制：1174 
        IE_SETINK                     = 0x0497, // 十进制：1175 
        IE_GETPENTIP                  = 0x0498, // 十进制：1176 
        IE_SETPENTIP                  = 0x0499, // 十进制：1177 
        IE_GETERASERTIP               = 0x049a, // 十进制：1178 
        IE_SETERASERTIP               = 0x049b, // 十进制：1179 
        IE_GETBKGND                   = 0x049c, // 十进制：1180 
        IE_SETBKGND                   = 0x049d, // 十进制：1181 
        IE_GETGRIDORIGIN              = 0x049e, // 十进制：1182 
        IE_SETGRIDORIGIN              = 0x049f, // 十进制：1183 
        IE_GETGRIDPEN                 = 0x04a0, // 十进制：1184 
        IE_SETGRIDPEN                 = 0x04a1, // 十进制：1185 
        IE_GETGRIDSIZE                = 0x04a2, // 十进制：1186 
        IE_SETGRIDSIZE                = 0x04a3, // 十进制：1187 
        IE_GETMODE                    = 0x04a4, // 十进制：1188 
        IE_SETMODE                    = 0x04a5, // 十进制：1189 
        IE_GETINKRECT                 = 0x04a6, // 十进制：1190 
        WM_CAP_SET_MCI_DEVICEW        = 0x04a6, // 十进制：1190 
        WM_CAP_GET_MCI_DEVICEW        = 0x04a7, // 十进制：1191 
        WM_CAP_PAL_OPENW              = 0x04b4, // 十进制：1204 
        WM_CAP_PAL_SAVEW              = 0x04b5, // 十进制：1205 
        IE_GETAPPDATA                 = 0x04b8, // 十进制：1208 
        IE_SETAPPDATA                 = 0x04b9, // 十进制：1209 
        IE_GETDRAWOPTS                = 0x04ba, // 十进制：1210 
        IE_SETDRAWOPTS                = 0x04bb, // 十进制：1211 
        IE_GETFORMAT                  = 0x04bc, // 十进制：1212 
        IE_SETFORMAT                  = 0x04bd, // 十进制：1213 
        IE_GETINKINPUT                = 0x04be, // 十进制：1214 
        IE_SETINKINPUT                = 0x04bf, // 十进制：1215 
        IE_GETNOTIFY                  = 0x04c0, // 十进制：1216 
        IE_SETNOTIFY                  = 0x04c1, // 十进制：1217 
        IE_GETRECOG                   = 0x04c2, // 十进制：1218 
        IE_SETRECOG                   = 0x04c3, // 十进制：1219 
        IE_GETSECURITY                = 0x04c4, // 十进制：1220 
        IE_SETSECURITY                = 0x04c5, // 十进制：1221 
        IE_GETSEL                     = 0x04c6, // 十进制：1222 
        IE_SETSEL                     = 0x04c7, // 十进制：1223 
        CDM_LAST                      = 0x04c8, // 十进制：1224 
        IE_DOCOMMAND                  = 0x04c8, // 十进制：1224 
        MCIWNDM_NOTIFYMODE            = 0x04c8, // 十进制：1224 
        IE_GETCOMMAND                 = 0x04c9, // 十进制：1225 
        IE_GETCOUNT                   = 0x04ca, // 十进制：1226 
        IE_GETGESTURE                 = 0x04cb, // 十进制：1227 
        MCIWNDM_NOTIFYMEDIA           = 0x04cb, // 十进制：1227 
        IE_GETMENU                    = 0x04cc, // 十进制：1228 
        IE_GETPAINTDC                 = 0x04cd, // 十进制：1229 
        MCIWNDM_NOTIFYERROR           = 0x04cd, // 十进制：1229 
        IE_GETPDEVENT                 = 0x04ce, // 十进制：1230 
        IE_GETSELCOUNT                = 0x04cf, // 十进制：1231 
        IE_GETSELITEMS                = 0x04d0, // 十进制：1232 
        IE_GETSTYLE                   = 0x04d1, // 十进制：1233 
        MCIWNDM_SETTIMEFORMATW        = 0x04db, // 十进制：1243 
        EM_OUTLINE                    = 0x04dc, // 十进制：1244 
        MCIWNDM_GETTIMEFORMATW        = 0x04dc, // 十进制：1244 
        EM_GETSCROLLPOS               = 0x04dd, // 十进制：1245 
        EM_SETSCROLLPOS               = 0x04de, // 十进制：1246 
        EM_SETFONTSIZE                = 0x04df, // 十进制：1247 
        MCIWNDM_GETFILENAMEW          = 0x04e0, // 十进制：1248 
        MCIWNDM_GETDEVICEW            = 0x04e1, // 十进制：1249 
        MCIWNDM_GETERRORW             = 0x04e4, // 十进制：1252 
        FM_GETFOCUS                   = 0x0600, // 十进制：1536 
        FM_GETDRIVEINFOA              = 0x0601, // 十进制：1537 
        FM_GETSELCOUNT                = 0x0602, // 十进制：1538 
        FM_GETSELCOUNTLFN             = 0x0603, // 十进制：1539 
        FM_GETFILESELA                = 0x0604, // 十进制：1540 
        FM_GETFILESELLFNA             = 0x0605, // 十进制：1541 
        FM_REFRESH_WINDOWS            = 0x0606, // 十进制：1542 
        FM_RELOAD_EXTENSIONS          = 0x0607, // 十进制：1543 
        FM_GETDRIVEINFOW              = 0x0611, // 十进制：1553 
        FM_GETFILESELW                = 0x0614, // 十进制：1556 
        FM_GETFILESELLFNW             = 0x0615, // 十进制：1557 
        WLX_WM_SAS                    = 0x0659, // 十进制：1625 
        SM_GETSELCOUNT                = 0x07e8, // 十进制：2024 
        UM_GETSELCOUNT                = 0x07e8, // 十进制：2024 
        WM_CPL_LAUNCH                 = 0x07e8, // 十进制：2024 
        SM_GETSERVERSELA              = 0x07e9, // 十进制：2025 
        UM_GETUSERSELA                = 0x07e9, // 十进制：2025 
        WM_CPL_LAUNCHED               = 0x07e9, // 十进制：2025 
        SM_GETSERVERSELW              = 0x07ea, // 十进制：2026 
        UM_GETUSERSELW                = 0x07ea, // 十进制：2026 
        SM_GETCURFOCUSA               = 0x07eb, // 十进制：2027 
        UM_GETGROUPSELA               = 0x07eb, // 十进制：2027 
        SM_GETCURFOCUSW               = 0x07ec, // 十进制：2028 
        UM_GETGROUPSELW               = 0x07ec, // 十进制：2028 
        SM_GETOPTIONS                 = 0x07ed, // 十进制：2029 
        UM_GETCURFOCUSA               = 0x07ed, // 十进制：2029 
        UM_GETCURFOCUSW               = 0x07ee, // 十进制：2030 
        UM_GETOPTIONS                 = 0x07ef, // 十进制：2031 
        UM_GETOPTIONS2                = 0x07f0, // 十进制：2032 
        MEM_COMMIT                    = 0x1000,
        OCMBASE                       = 0x2000, // 十进制：8192 
        OCM_CTLCOLOR                  = 0x2019, // 十进制：8217 
        OCM_DRAWITEM                  = 0x202b, // 十进制：8235 
        OCM_MEASUREITEM               = 0x202c, // 十进制：8236 
        OCM_DELETEITEM                = 0x202d, // 十进制：8237 
        OCM_VKEYTOITEM                = 0x202e, // 十进制：8238 
        OCM_CHARTOITEM                = 0x202f, // 十进制：8239 
        OCM_COMPAREITEM               = 0x2039, // 十进制：8249 
        OCM_NOTIFY                    = 0x204e, // 十进制：8270 
        OCM_COMMAND                   = 0x2111, // 十进制：8465 
        OCM_HSCROLL                   = 0x2114, // 十进制：8468 
        OCM_VSCROLL                   = 0x2115, // 十进制：8469 
        OCM_CTLCOLORMSGBOX            = 0x2132, // 十进制：8498 
        OCM_CTLCOLOREDIT              = 0x2133, // 十进制：8499 
        OCM_CTLCOLORLISTBOX           = 0x2134, // 十进制：8500 
        OCM_CTLCOLORBTN               = 0x2135, // 十进制：8501 
        OCM_CTLCOLORDLG               = 0x2136, // 十进制：8502 
        OCM_CTLCOLORSCROLLBAR         = 0x2137, // 十进制：8503 
        OCM_CTLCOLORSTATIC            = 0x2138, // 十进制：8504 
        OCM_PARENTNOTIFY              = 0x2210, // 十进制：8720 
        /// <summary>
        /// 由应用程序使用的消息。
        /// </summary>
        WM_APP                        = 0x8000, // 十进制：32768 
        MEM_RELEASE                   = 0x8000,
        WM_RASDIALEVENT               = 0xcccd, // 十进制：52429 
    }

    /// <summary>
    /// 模拟键盘事件。
    /// </summary>
    public enum KEYEVENTF
    {
        /// <summary>
        /// 按下按键的常量
        /// </summary>
        KEYEVENTF_KEYDOWN = 0,
        /// <summary>
        /// 释放按键的常量
        /// </summary>
        KEYEVENTF_KEYUP = 2,
    }

    /// <summary>
    /// 模拟鼠标事件。
    /// </summary>
    public enum MOUSEEVENTF : int
    {
        /// <summary>
        /// 模拟鼠标移动事件。
        /// </summary>
        MOUSEEVENTF_MOVE       = 0x1,
        /// <summary>
        /// 模拟按下鼠标左键。
        /// </summary>
        MOUSEEVENTF_LEFTDOWN   = 0x2,
        /// <summary>
        /// 模拟放开鼠标左键。
        /// </summary>
        MOUSEEVENTF_LEFTUP     = 0x4,
        /// <summary>
        /// 模拟按下鼠标右键。
        /// </summary>
        MOUSEEVENTF_RIGHTDOWN  = 0x8,
        /// <summary>
        /// 模拟放开鼠标右键。
        /// </summary>
        MOUSEEVENTF_RIGHTUP    = 0x10,
        /// <summary>
        /// 模拟按下鼠标中键。
        /// </summary>
        MOUSEEVENTF_MIDDLEDOWN = 0x20,
        /// <summary>
        /// 模拟放开鼠标中键。
        /// </summary>
        MOUSEEVENTF_MIDDLEUP   = 0x40,
        /// <summary>
        /// 主要用于控制鼠标移动位置是否绝对位置。
        /// </summary>
        MOUSEEVENTF_ABSOLUTE   = 0x8000,
    }

    /// <summary>
    /// 进程访问权限标识。
    /// </summary>
    [System.Flags]
    public enum ProcessAccessFlags : uint
    {
        /// <summary>
        /// 所有能获得的权限
        /// </summary>
        PROCESS_ALL_ACCESS = 0x001F0FFF,
        /// <summary>
        /// 终止一个进程的权限，使用TerminateProcess
        /// </summary>
        Terminate = 0x00000001,
        /// <summary>
        /// 需要创建一个线程
        /// </summary>
        PROCESS_CREATE_THREAD = 0x00000002,
        /// <summary>
        /// 操作进程内存空间的权限(可用VirtualProtectEx和WriteProcessMemory)
        /// </summary>
        PROCESS_VM_OPERATION = 0x00000008,
        /// <summary>
        /// 读取进程内存空间的权限，可使用ReadProcessMemory
        /// </summary>
        PROCESS_VM_READ = 0x00000010,
        /// <summary>
        /// 读取进程内存空间的权限，可使用WriteProcessMemory
        /// </summary>
        PROCESS_VM_WRITE = 0x00000020,
        /// <summary>
        /// 重复使用DuplicateHandle句柄
        /// </summary>
        PROCESS_DUP_HANDLE = 0x00000040,
        /// <summary>
        /// 设置某些信息的权限，如进程优先级
        /// </summary>
        PROCESS_SET_INFORMATION = 0x00000200,
        /// <summary>
        /// 也拥有PROCESS_QUERY_LIMITED_INFORMATION权限
        /// </summary>
        PROCESS_QUERY_INFORMATION = 0x00000400,
        /// <summary>
        /// 等待进程终止
        /// </summary>
        Synchronize = 0x00100000
    }

    /// <summary>
    /// 定义文件属性标识
    /// </summary>
    public enum FileAttributeFlags : uint
    {
        /// <summary>
        /// 只读。
        /// </summary>
        FILE_ATTRIBUTE_READONLY = 0x00000001,
        /// <summary>
        /// 隐藏。
        /// </summary>
        FILE_ATTRIBUTE_HIDDEN = 0x00000002,
        /// <summary>
        /// 系统。
        /// </summary>
        FILE_ATTRIBUTE_SYSTEM = 0x00000004,
        /// <summary>
        /// 目录。
        /// </summary>
        FILE_ATTRIBUTE_DIRECTORY = 0x00000010,
        /// <summary>
        /// 存档。
        /// </summary>
        FILE_ATTRIBUTE_ARCHIVE = 0x00000020,
        /// <summary>
        /// 保留。
        /// </summary>
        FILE_ATTRIBUTE_DEVICE = 0x00000040,
        /// <summary>
        /// 正常
        /// </summary>
        FILE_ATTRIBUTE_NORMAL = 0x00000080,
        /// <summary>
        /// 临时
        /// </summary>
        FILE_ATTRIBUTE_TEMPORARY = 0x00000100,
        /// <summary>
        /// 稀疏文件
        /// </summary>
        FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200,
        /// <summary>
        /// 超链接或快捷方式
        /// </summary>
        FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400,
        /// <summary>
        /// 压缩。只存在于NTFS分区
        /// </summary>
        FILE_ATTRIBUTE_COMPRESSED = 0x00000800,
        /// <summary>
        /// 脱机。
        /// </summary>
        FILE_ATTRIBUTE_OFFLINE = 0x00001000,
        /// <summary>
        /// 索引。只存在于NTFS分区。
        /// </summary>
        FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000,
        /// <summary>
        /// 加密。只存在于NTFS分区。
        /// </summary>
        FILE_ATTRIBUTE_ENCRYPTED = 0x00004000,
        /// <summary>
        /// 虚拟
        /// </summary>
        FILE_ATTRIBUTE_VIRTUAL = 65536,
    }

    /// <summary>
    /// 定义获取资源标识
    /// </summary>
    public enum GetFileInfoFlags : uint
    {
        /// <summary>
        /// 获取图标
        /// </summary>
        SHGFI_ICON = 0x000000100,
        /// <summary>
        /// 获取显示名
        /// </summary>
        SHGFI_DISPLAYNAME = 0x000000200,
        /// <summary>
        /// 获取类型名
        /// </summary>
        SHGFI_TYPENAME = 0x000000400,
        /// <summary>
        /// 获取属性
        /// </summary>
        SHGFI_ATTRIBUTES = 0x000000800,
        /// <summary>
        /// get icon location
        /// </summary>
        SHGFI_ICONLOCATION = 0x000001000,
        /// <summary>
        /// return exe type
        /// </summary>
        SHGFI_EXETYPE = 0x000002000,
        /// <summary>
        /// get system icon index
        /// </summary>
        SHGFI_SYSICONINDEX = 0x000004000,
        /// <summary>
        /// put a link overlay on icon
        /// </summary>
        SHGFI_LINKOVERLAY = 0x000008000,
        /// <summary>
        /// show icon in selected state
        /// </summary>
        SHGFI_SELECTED = 0x000010000,
        /// <summary>
        /// get only specified attributes
        /// </summary>
        SHGFI_ATTR_SPECIFIED = 0x000020000,
        /// <summary>
        /// 获取大图标
        /// </summary>
        SHGFI_LARGEICON = 0x000000000,
        /// <summary>
        /// 获取小图标
        /// </summary>
        SHGFI_SMALLICON = 0x000000001,
        /// <summary>
        /// get open icon
        /// </summary>
        SHGFI_OPENICON = 0x000000002,
        /// <summary>
        /// get shell size icon
        /// </summary>
        SHGFI_SHELLICONSIZE = 0x000000004,
        /// <summary>
        /// pszPath是一个标识符（pszPath is a pidl）
        /// </summary>
        SHGFI_PIDL = 0x000000008,
        /// <summary>
        /// 不会访问第一个参数所指定的文件，使函数认为在pszPath参数中传递的文件是存在的，此时，它可以获得扩展名，并且搜索注册表来得到图标和类型名信息。（use passed dwFileAttribute）
        /// </summary>
        SHGFI_USEFILEATTRIBUTES = 0x000000010,
        /// <summary>
        /// apply the appropriate overlays
        /// </summary>
        SHGFI_ADDOVERLAYS = 0x000000020,
        /// <summary>
        /// Get the index of the overlay
        /// </summary>
        SHGFI_OVERLAYINDEX = 0x000000040
    }

    ///// <summary>
    ///// 通知消息(Notification message)是指这样一种消息，一个窗口内的子控件发生了一些事情，需要通知父窗口。通知消息只适用于标准的窗口控件如按钮、列表框、组合框、编辑框，以及Windows 95公共控件如树状视图、列表视图等。例如，单击或双击一个控件、在控件中选择部分文本、操作控件的滚动条都会产生通知消息。 
    ///// </summary>
    //public enum NotificationMessage
    //{
    //    /// <summary>
    //    /// 用户单击了按钮
    //    /// </summary>
    //    BN_CLICKED,
    //    /// <summary>
    //    /// 按钮被禁止
    //    /// </summary>
    //    BN_DISABLE,
    //    /// <summary>
    //    /// 用户双击了按钮
    //    /// </summary>
    //    BN_DOUBLECLICKED,
    //    /// <summary>
    //    /// 用户加亮了按钮
    //    /// </summary>
    //    BN_HILITE,
    //    /// <summary>
    //    /// 按钮应当重画
    //    /// </summary>
    //    BN_PAINT,
    //    /// <summary>
    //    /// 加亮应当去掉组合框
    //    /// </summary>
    //    BN_UNHILITE,
    //    /// <summary>
    //    /// 组合框的列表框被关闭
    //    /// </summary>
    //    CBN_CLOSEUP,
    //    /// <summary>
    //    /// 用户双击了一个字符串
    //    /// </summary>
    //    CBN_DBLCLK,
    //    /// <summary>
    //    /// 组合框的列表框被拉出
    //    /// </summary>
    //    CBN_DROPDOWN,
    //    /// <summary>
    //    /// 用户修改了编辑框中的文本
    //    /// </summary>
    //    CBN_EDITCHANGE,
    //    /// <summary>
    //    /// 编辑框内的文本即将更新
    //    /// </summary>
    //    CBN_EDITUPDATE,
    //    /// <summary>
    //    /// 组合框内存不足
    //    /// </summary>
    //    CBN_ERRSPACE,
    //    /// <summary>
    //    /// 组合框失去输入焦点
    //    /// </summary>
    //    CBN_KILLFOCUS,
    //    /// <summary>
    //    /// 在组合框中选择了一项
    //    /// </summary>
    //    CBN_SELCHANGE,
    //    /// <summary>
    //    /// 用户的选择应当被取消
    //    /// </summary>
    //    CBN_SELENDCANCEL,
    //    /// <summary>
    //    /// 用户的选择是合法的
    //    /// </summary>
    //    CBN_SELENDOK,
    //    /// <summary>
    //    /// 组合框获得输入焦点编辑框
    //    /// </summary>
    //    CBN_SETFOCUS,
    //    /// <summary>
    //    /// 编辑框中的文本己更新
    //    /// </summary>
    //    EN_CHANGE,
    //    /// <summary>
    //    /// 编辑框内存不足
    //    /// </summary>
    //    EN_ERRSPACE,
    //    /// <summary>
    //    /// 用户点击了水平滚动条
    //    /// </summary>
    //    EN_HSCROLL,
    //    /// <summary>
    //    /// 编辑框正在失去输入焦点
    //    /// </summary>
    //    EN_KILLFOCUS,
    //    /// <summary>
    //    /// 插入的内容被截断
    //    /// </summary>
    //    EN_MAXTEXT,
    //    /// <summary>
    //    /// 编辑框获得输入焦点
    //    /// </summary>
    //    EN_SETFOCUS,
    //    /// <summary>
    //    /// 编辑框中的文本将要更新
    //    /// </summary>
    //    EN_UPDATE,
    //    /// <summary>
    //    /// 用户点击了垂直滚动条消息含义列表框
    //    /// </summary>
    //    EN_VSCROLL,
    //    /// <summary>
    //    /// 用户双击了一项
    //    /// </summary>
    //    LBN_DBLCLK,
    //    /// <summary>
    //    /// 列表框内存不够
    //    /// </summary>
    //    LBN_ERRSPACE,
    //    /// <summary>
    //    /// 列表框正在失去输入焦点
    //    /// </summary>
    //    LBN_KILLFOCUS,
    //    /// <summary>
    //    /// 选择被取消
    //    /// </summary>
    //    LBN_SELCANCEL,
    //    /// <summary>
    //    /// 选择了另一项
    //    /// </summary>
    //    LBN_SELCHANGE,
    //    /// <summary>
    //    /// 列表框获得输入焦点
    //    /// </summary>
    //    LBN_SETFOCUS,
    //}

}
