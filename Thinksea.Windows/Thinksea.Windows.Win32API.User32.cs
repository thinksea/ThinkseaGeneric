namespace Thinksea.Windows.Win32API
{
    /// <summary>
    /// 封装了对 Windows 库“User32.dll”的调用接口。
    /// </summary>
    public static class User32
    {
        /// <summary>
        /// 该函数捕获并跟踪鼠标的移动直到用户松开左键、按下Esc键或者将鼠标移动到围绕指定点的“拖动矩形”之外。拖动矩形的宽和高由函数GetSystemMetrics返回的SM_CXDRAG或SM_CYDRAG确定。
        /// </summary>
        /// <param name="hWnd">接受鼠标输入的窗口的句柄。</param>
        /// <param name="pt">鼠标在屏幕坐标下的初始位置，此函数根据这个点来确定拖动矩形的坐标。</param>
        /// <returns>如果用户在按着鼠标左键时将鼠标移出了拖动矩形之外，则返回非零值；如果用户按着鼠标左键在拖动内移动鼠标，则返回值是零。</returns>
        /// <remarks>
        /// 拖动矩形的系统度量是可构造的，允许更大或更小的拖动矩形。
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool DragDetect(System.IntPtr hWnd, System.Drawing.Point pt);

        /// <summary>
        /// 功能确定当前焦点位于哪个控件上。
        /// </summary>
        /// <returns>GraphicObject。函数执行成功时返回当前得到焦点控件的引用，发生错误时返回无效引用。用法应用程序利用IsValid()函数可以检测GetFocus()是否返回有效的控件引用。同时，使用TypeOf()函数可以确定控件的类型。</returns>
        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern System.IntPtr GetFocus();

        /// <summary>
        /// 该函数对指定的窗口设置键盘焦点。该窗口必须与调用线程的消息队列相关。
        /// </summary>
        /// <param name="hWnd">接收键盘输入的窗口指针。若该参数为NULL，则击键被忽略。</param>
        /// <returns>若函数调用成功，则返回原先拥有键盘焦点的窗口句柄。若hWnd参数无效或窗口未与调用线程的消息队列相关，则返回值为NULL。若想要获得更多错误信息，可以调用GetLastError函数。</returns>
        /// <remarks>
        /// SetFocus函数发送WM_KILLFOCUS消息到失去键盘焦点的窗口，并且发送WM_SETFOCUS消息到接受键盘焦点的窗口。它也激活接受键盘焦点的窗口或接受键盘焦点的窗口的父窗口。
        /// 若一个窗口是活动的，但没有键盘焦点，则任何按键将会产生WM_SYSCHAR,WM_SYSKEYDOWN或WM_SYSKEYUP消息。若VK_MENU键也被按下，则消息的IParam参数将设置第30位。否则，所产生的消息将不设置此位。
        /// 使用AttachThreadInput函数，一个线程可将输入处理连接到其他线程。这使得线程可以调用SetFocus函数为一个与其他线程的消息队列相关的窗口设置键盘焦点。
        /// Windows CE：不使用SetFocus函数为一个与其他线程的消息队列相关的窗口设置键盘焦点。但有一个例外。若一个线程的窗口是另一线程的子窗口，或这些窗口是具有同一父窗口的兄弟窗口，则与一个线程关联的窗口可以为其他窗口设置焦点，尽管该窗口属于一个不同的线程。在这种情况下，就不必先调用AttachThreadlnpUt函数。
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern System.IntPtr SetFocus(System.IntPtr hWnd);

        /// <summary>
        /// 该函数将一个消息放入（寄送）到与指定窗口创建的线程相联系消息队列里，不等待线程处理消息就返回，是异步消息模式。消息队列里的消息通过调用GetMessage和PeekMessage取得。
        /// </summary>
        /// <param name="hWnd">
        /// 其窗口程序接收消息的窗口的句柄。可取有特定含义的两个值：
        /// HWND_BROADCAST：消息被寄送到系统的所有顶层窗口，包括无效或不可见的非自身拥有的窗口、 被覆盖的窗口和弹出式窗口。消息不被寄送到子窗口。
        /// NULL：此函数的操作和调用参数dwThread设置为当前线程的标识符PostThreadMessage函数一样。
        /// </param>
        /// <param name="Msg">指定被寄送的消息。</param>
        /// <param name="wParam">指定附加的消息特定的信息。</param>
        /// <param name="lParam">指定附加的消息特定的信息。</param>
        /// <returns>如果函数调用成功，返回非零值：如果函数调用失败，返回值是零。若想获得更多的错误信息，请调用GetLastError函数。</returns>
        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool PostMessage(System.IntPtr hWnd, int Msg, uint wParam, uint lParam);

        /// <summary>
        /// 该函数将指定的消息发送到一个或多个窗口。此函数为指定的窗口调用窗口程序，直到窗口程序处理完消息再返回。而和函数PostMessage不同，PostMessage是将一个消息寄送到一个线程的消息队列后就立即返回。
        /// </summary>
        /// <param name="hWnd">其窗口程序将接收消息的窗口的句柄。如果此参数为HWND_BROADCAST，则消息将被发送到系统中所有顶层窗口，包括无效或不可见的非自身拥有的窗口、被覆盖的窗口和弹出式窗口，但消息不被发送到子窗口。</param>
        /// <param name="Msg">指定被发送的消息。</param>
        /// <param name="wParam">指定附加的消息特定信息。</param>
        /// <param name="lParam">指定附加的消息特定信息。</param>
        /// <returns>返回值指定消息处理的结果，依赖于所发送的消息。</returns>
        /// <remarks>
        /// 需要用HWND_BROADCAST通信的应用程序应当使用函数RegisterWindowMessage来为应用程序间的通信取得一个唯一的消息。
        /// 如果指定的窗口是由正在调用的线程创建的，则窗口程序立即作为子程序调用。如果指定的窗口是由不同线程创建的，则系统切换到该线程并调用恰当的窗口程序。线程间的消息只有在线程执行消息检索代码时才被处理。发送线程被阻塞直到接收线程处理完消息为止。
        /// Windows CE：Windows CE不支持Windows桌面平台支持的所有消息。使用SendMesssge之前，要检查发送的消息是否被支持。
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern uint SendMessage(System.IntPtr hWnd, int Msg, uint wParam, uint lParam);

        /// <summary>
        /// 该函数将指定的消息发送到一个或多个窗口。此函数为指定的窗口调用窗口程序，直到窗口程序处理完消息再返回。而和函数PostMessage不同，PostMessage是将一个消息寄送到一个线程的消息队列后就立即返回。
        /// </summary>
        /// <param name="hWnd">其窗口程序将接收消息的窗口的句柄。如果此参数为HWND_BROADCAST，则消息将被发送到系统中所有顶层窗口，包括无效或不可见的非自身拥有的窗口、被覆盖的窗口和弹出式窗口，但消息不被发送到子窗口。</param>
        /// <param name="Msg">指定被发送的消息。</param>
        /// <param name="wParam">指定附加的消息特定信息。</param>
        /// <param name="lParam">指定附加的消息特定信息。</param>
        /// <returns>返回值指定消息处理的结果，依赖于所发送的消息。</returns>
        /// <remarks>
        /// 需要用HWND_BROADCAST通信的应用程序应当使用函数RegisterWindowMessage来为应用程序间的通信取得一个唯一的消息。
        /// 如果指定的窗口是由正在调用的线程创建的，则窗口程序立即作为子程序调用。如果指定的窗口是由不同线程创建的，则系统切换到该线程并调用恰当的窗口程序。线程间的消息只有在线程执行消息检索代码时才被处理。发送线程被阻塞直到接收线程处理完消息为止。
        /// Windows CE：Windows CE不支持Windows桌面平台支持的所有消息。使用SendMesssge之前，要检查发送的消息是否被支持。
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, SetLastError = false)]
        public static extern int SendMessage(System.IntPtr hWnd, int Msg, int wParam, int lParam);

        /// <summary>
        /// 向窗口发送ANSI格式的消息
        /// </summary>
        /// <param name="hwnd">其窗口程序将接收消息的窗口的句柄。</param>
        /// <param name="wMsg">指定被发送的消息。</param>
        /// <param name="wParam">指定附加的消息特定信息。</param>
        /// <param name="lParam">指定附加的消息特定信息。</param>
        /// <returns>返回值指定消息处理的结果，依赖于所发送的消息。</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessageA(System.IntPtr hwnd, int wMsg, System.IntPtr wParam, string lParam);

        /// <summary>
        /// 向窗口发送ANSI格式的消息
        /// </summary>
        /// <param name="hwnd">其窗口程序将接收消息的窗口的句柄。</param>
        /// <param name="wMsg">指定被发送的消息。</param>
        /// <param name="wParam">指定附加的消息特定信息。</param>
        /// <param name="lParam">指定附加的消息特定信息。</param>
        /// <returns>返回值指定消息处理的结果，依赖于所发送的消息。</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "SendMessageA")]
        public static extern int SendMessageA(System.IntPtr hwnd, int wMsg, System.IntPtr wParam, ref System.Drawing.Rectangle lParam);

        /// <summary>
        /// 该函数改变一个子窗口，弹出式窗口式顶层窗口的尺寸，位置和Z序。子窗口，弹出式窗口，及顶层窗口根据它们在屏幕上出现的顺序排序、顶层窗口设置的级别最高，并且被设置为Z序的第一个窗口。
        /// </summary>
        /// <param name="hWnd">窗口句柄。</param>
        /// <param name="hWndlnsertAfter">在z序中的位于被置位的窗口前的窗口句柄。</param>
        /// <param name="X">以客户坐标指定窗口新位置的左边界。</param>
        /// <param name="Y">以客户坐标指定窗口新位置的顶边界。</param>
        /// <param name="Width">以像素指定窗口的新的宽度。</param>
        /// <param name="Height">以像素指定窗口的新的高度。</param>
        /// <param name="flags">窗口尺寸和定位的标志。</param>
        /// <returns>如果函数成功，返回值为非零；如果函数失败，返回值为零。若想获得更多错误消息，请调用GetLastError函数。</returns>
        /// <remarks>
        /// 窗口成为最顶级窗口后，它下属的所有窗口也会进入最顶级。一旦将其设为非最顶级，则它的所有下属和物主窗口也会转为非最顶级。Z序列用垂直于屏幕的一根假想Z轴量化这种从顶部到底部排列的窗口顺序。
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int SetWindowPos(System.IntPtr hWnd, System.IntPtr hWndlnsertAfter, int X, int Y, int Width, int Height, Thinksea.Windows.Win32API.FlagsSetWindowPos flags);

        /// <summary>
        /// GetWindowLong是一个Windows API函数。该函数获得有关指定窗口的信息，函数也获得在额外窗口内存中指定偏移位地址的32位度整型值。
        /// </summary>
        /// <param name="hWnd">窗口句柄及间接给出的窗口所属的窗口类。</param>
        /// <param name="nlndex">指定要获得值的大于等于0的值的偏移量。有效值的范围从0到额外窗口内存空间的字节数一4例如，若指定了12位或多于12位的额外类存储空间，则应设为第三个32位整数的索引位8。</param>
        /// <returns>如果函数成功，返回值是所需的32位值；如果函数失败，返回值是0。若想获得更多错误信息请调用 GetLastError函数。</returns>
        /// <remarks>
        /// 通过使用函数RegisterClassEx将结构WNDCLASSEX中的cbWndExtra单元指定为一个非0值来保留额外类的存储空间。
        /// Windows CE：nlndex参数指定的字节偏移量必须为 4的倍数。不支持 unaligmned access。
        /// Windows CE：不支持在参数nlndex中设定的GWL_HINSTANCE和GWL_HWNDPARENT。
        /// Windows CE1.0也不支持在 nlndex参数中的 DWL_DLGPROC和 GWL_USERDATA。
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int GetWindowLong(System.IntPtr hWnd, int nlndex);

        /// <summary>
        /// SetWindowLong是一个Windows API函数。该函数用来改变指定窗口的属性．函数也将指定的一个32位值设置在窗口的额外存储空间的指定偏移位置。
        /// </summary>
        /// <param name="hWnd">窗口句柄及间接给出的窗口所属的类。</param>
        /// <param name="nlndex">指定将设定的大于等于0的偏移值。有效值的范围从0到额外类的存储空间的字节数减4：例如若指定了12或多于12个字节的额外窗口存储空间，则应设索引位8来访问第三个4字节，同样设置0访问第一个4字节，4访问第二个4字节。</param>
        /// <param name="dwNewLong">指定的替换值。</param>
        /// <returns>
        /// 如果函数成功，返回值是指定的32位整数的原来的值。如果函数失败，返回值为0。若想获得更多错误信息，请调用GetLastError函数。
        /// 如果指定32位整数的原来的值为0，并且函数成功，则返回值为0，但是函数并不清除最后的错误信息，这就很难判断函数是否成功。这时，就应在调用SetWindowLong之前调用callingSetLastError（0）函数来清除最后的错误信息。这样，如果函数失败就会返回0，并且GetLastError。也返回一个非零值。
        /// </returns>
        /// <remarks>
        /// 如果由hWnd参数指定的窗口与调用线程不属于同一进程，将导致SetWindowLong函数修改窗口过程失败。
        /// 指定的窗口数据是在缓存中保存的，因此在调用SetWindowLong之后再调用SetWindowPos函数才能使SetWindowLong函数所作的改变生效。
        /// 如果使用带GWL_WNDPROC索引值的SetWindowLong函数替换窗口过程，则该窗口过程必须与WindowProccallback函数说明部分指定的指导行一致。
        /// 如果使用带DWL_MSGRESULT索引值的SetWindowLong函数来设置由一个对话框过程处理的消息的返回值，应在此后立即返回TRUE。否则，如果又调用了其他函数而使对话框过程接收到一个窗口消息，则嵌套的窗口消息可能改写使用DWL_MSGRESULT设定的返回值。
        /// 可以使用带GWL_WNDPROC索引值的SetWindowLong函数创建一个窗口类的子类，该窗口类是用于创建该窗口的类。一个应用程序可以以一个系统类为子类，但是不能以一个其他进程产生的窗口类为子类，SetwindowLong函数通过改变与一个特殊的窗口类相联系的窗口过程来创建窗口子类，从而使系统调用新的窗口过程而不是以前定义的窗口过程。应用程序必须通过调用CallWindowProc函数向前窗口传递未被新窗口处理的消息，这样作允许应用程序创建一个窗口过程链。
        /// 通过使用函数RegisterClassEx将结构WNDCLASSEX中的cbWndExtra单元指定为一个非0值来保留新外窗口内存。
        /// 不能通过调用带GWL_HWNDPARENT索引值的SetWindowLong的函数来改变子窗口的父窗口，应使用SetParent函数。
        /// Windows CE：nlndex参数必须是4个字节的倍数不支持unaligned access。
        /// 不支持下列nlndex参数值
        /// GWL_HINSTANCE；GWL_HWNDPARENTGWL;GWL_USERDATA
        /// Windows CE 2.0版支持在nlndex参数中的DWL_DLGPROC值，但是WindowsCE1.0不支持。
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int SetWindowLong(System.IntPtr hWnd, int nlndex, int dwNewLong);

        /// <summary>
        /// 显示或隐藏滚动条。
        /// </summary>
        /// <param name="hWnd">根据参数nBar值，处理滚动条控制或带有标准滚动条窗体。</param>
        /// <param name="nBar">指定滚动条是否是窗口的非工作区的控件或部件。 如果是非工作区的一部分，nBar 还指示滚动条是水平，垂直定位或两个。</param>
        /// <param name="bShow">指定滚动条是被显示还是隐藏。此参数为TRUE，滚动条将被显示，否则被隐藏。</param>
        /// <returns>如果函数运行成功，返回值为非零；如果函数运行失败，返回值为零。若想获得更多的错误信息，请调用GetLastError函数。</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern int ShowScrollBar(System.IntPtr hWnd, int nBar, int bShow);

        /// <summary>
        /// 该函数获得包含指定点的窗口的句柄。
        /// </summary>
        /// <param name="point">指定一个被检测的点的POINT结构。</param>
        /// <returns>返回值为包含该点的窗口的句柄。如果包含指定点的窗口不存在，返回值为NULL。如果该点在静态文本控件之上，返回值是在该静态文本控件的下面的窗口的句柄。</returns>
        /// <remarks>
        /// WindowFromPoint函数不获取隐藏或禁止的窗口句柄，即使点在该窗口内。应用程序应该使用ChildWindowFromPoint函数进行无限制查询，这样就可以获得静态文本控件的句柄。
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //*********************************
        // FxCop bug, suppress the message
        //*********************************
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Portability", "CA1901:PInvokeDeclarationsShouldBePortable", MessageId = "0")]
        public static extern System.IntPtr WindowFromPoint(System.Drawing.Point point);

        public delegate System.IntPtr HookProc(int code, System.IntPtr wParam, System.IntPtr lParam);

        /// <summary>
        /// 设置钩子
        /// </summary>
        /// <param name="code">钩子类型</param>
        /// <param name="func">函数指针</param>
        /// <param name="hInstance">包含钩子函数的模块(EXE、DLL)句柄; 一般是 HInstance; 如果是当前线程这里可以是 0</param>
        /// <param name="threadID">关联的线程; 可用 GetCurrentThreadId 获取当前线程; 0 表示是系统级钩子</param>
        /// <returns>返回钩子的句柄; 0 表示失败</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.IntPtr SetWindowsHookEx(Thinksea.Windows.Win32API.HookType code, HookProc func, System.IntPtr hInstance, int threadID);

        /// <summary>
        /// 抽掉钩子
        /// </summary>
        /// <param name="hhook">钩子的句柄</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(System.IntPtr hhook);

        /// <summary>
        /// 调用下一个钩子
        /// </summary>
        /// <param name="hhook">钩子的句柄</param>
        /// <param name="code"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.IntPtr CallNextHookEx(System.IntPtr hhook, int code, System.IntPtr wParam, System.IntPtr lParam);

        ///// <summary>
        ///// 发送键盘事件。
        ///// </summary>
        ///// <param name="bVk">虚拟键值</param>
        ///// <param name="bScan">硬件扫描码。一般为0</param>
        ///// <param name="dwFlags">动作标识。  0 为按下，2为释放</param>
        ///// <param name="dwExtraInfo">键盘动作关联的辅加信息。一般情况下设成为 0</param>
        //[System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "keybd_event")]
        //public static extern void KeyBoardEvent(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        /// <summary>
        /// 发送键盘事件。
        /// </summary>
        /// <param name="bVk">虚拟键值</param>
        /// <param name="bScan">硬件扫描码。一般为0</param>
        /// <param name="dwFlags">动作标识。  0 为按下，2为释放</param>
        /// <param name="dwExtraInfo">键盘动作关联的辅加信息。一般情况下设成为 0</param>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "keybd_event")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);

        /// <summary>
        /// 模拟鼠标动作
        /// </summary>
        /// <param name="dwFlags">鼠标动作标识。 </param>
        /// <param name="dx">鼠标水平方向位置。 </param>
        /// <param name="dy">鼠标垂直方向位置。 </param>
        /// <param name="dwData">鼠标轮子转动的数量。</param>
        /// <param name="dwExtraInfo">一个关联鼠标动作辅加信息。 </param>
        /// <remarks>
        /// mouse_event MOUSEEVENTF_LEFTDOWN Or MOUSEEVENTF_LEFTUP, 0, 0, 0, 0
        /// mouse_event MOUSEEVENTF_LEFTDOWN Or MOUSEEVENTF_LEFTUP, 0, 0, 0, 0 '两句合起来是双击
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "mouse_event")]
        public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        /// <summary>
        /// 获取鼠标指针的当前位置。
        /// </summary>
        /// <param name="lpPoint">返回鼠标的当前位置。</param>
        /// <returns>非零表示成功，零表示失败。会设置GetLastError</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int GetCursorPos(ref System.Drawing.Point lpPoint);

        /// <summary>
        /// 设置鼠标位置。
        /// </summary>
        /// <param name="X">鼠标的水平方向位置。 </param>
        /// <param name="Y">鼠标的垂直方向位置。 </param>
        /// <returns>0表示假；1表示真。</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", EntryPoint = "mouse_event")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        //public static extern int SetCursorPos(int X, int Y);
        public static extern bool SetCursorPos(int X, int Y);

        /// <summary>
        /// 在窗口列表中寻找与指定条件相符的第一个子窗口
        /// </summary>
        /// <param name="hwndParent">在其中查找子的父窗口。如设为System.IntPtr.Zero，表示使用桌面窗口（通常说的顶级窗口都被认为是桌面的子窗口，所以也会对它们进行查找）</param>
        /// <param name="hwndChildAfter">从这个窗口后开始查找。这样便可利用对FindWindowEx的多次调用找到符合条件的所有子窗口。如设为System.IntPtr.Zero，表示从第一个子窗口开始搜索</param>
        /// <param name="lpszClass">欲搜索的类名。null表示忽略</param>
        /// <param name="lpszWindow">欲搜索的类名。null表示忽略</param>
        /// <returns>找到的窗口的句柄。如未找到相符窗口，则返回System.IntPtr.Zero。会设置GetLastError</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.IntPtr FindWindowEx(System.IntPtr hwndParent, System.IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        /// <summary>
        /// 获取与指定窗口关联在一起的一个线程和进程标识符
        /// </summary>
        /// <param name="hWnd">指定窗口句柄</param>
        /// <param name="ProcessId">指定一个变量，用于装载拥有那个窗口的一个进程的标识符</param>
        /// <returns>拥有窗口的线程的标识符</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.IntPtr GetWindowThreadProcessId(System.IntPtr hWnd, ref int ProcessId);

        /// <summary>
        /// 设置钩子
        /// </summary>
        /// <param name="idHook">钩子类型</param>
        /// <param name="lpfn">函数指针</param>
        /// <param name="hInstance">包含钩子函数的模块(EXE、DLL)句柄; 一般是 HInstance; 如果是当前线程这里可以是 0</param>
        /// <param name="threadId">关联的线程; 可用 GetCurrentThreadId 获取当前线程; 0 表示是系统级钩子</param>
        /// <returns>返回钩子的句柄; 0 表示失败</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, System.IntPtr hInstance, int threadId);

        /// <summary>
        /// 抽掉钩子
        /// </summary>
        /// <param name="idHook">钩子的句柄。</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool UnhookWindowsHookEx(int idHook);

        /// <summary>
        /// 调用下一个钩子
        /// </summary>
        /// <param name="idHook">钩子的句柄。</param>
        /// <param name="nCode"></param>
        /// <param name="wParam"></param>
        /// <param name="lParam"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, System.IntPtr wParam, System.IntPtr lParam);

        /// <summary>
        /// 取得模块句柄
        /// </summary>
        /// <param name="lpModuleName">模块名称。</param>
        /// <returns>如执行成功成功，则返回模块句柄。System.IntPtr.Zero表示失败。会设置GetLastError</returns>
        [System.Runtime.InteropServices.DllImport("kernel32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto, CallingConvention = System.Runtime.InteropServices.CallingConvention.StdCall)]
        private static extern System.IntPtr GetModuleHandle(string lpModuleName);

        /// <summary>
        /// 这个函数是查找窗口句柄的, 参数sClassName,是类名（默认设置为 System.IntPtr.Zero）, 参数sWindowTitle是窗口名
        /// </summary>
        /// <param name="sClassName">类名（默认设置为 System.IntPtr.Zero）</param>
        /// <param name="sWindowTitle">窗口名</param>
        /// <returns>找到窗口的句柄。如未找到相符窗口，则返回System.IntPtr.Zero。会设置GetLastError</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern System.IntPtr FindWindow(System.IntPtr sClassName, string sWindowTitle);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lpClassName">指向包含了窗口类名的空中止（C语言）字串的指针；或设为null表示接收任何类</param>
        /// <param name="lpWindowName">指向包含了窗口文本（或标签）的空中止（C语言）字串的指针；或设为null表示接收任何窗口标题</param>
        /// <returns>找到窗口的句柄。如未找到相符窗口，则返回System.IntPtr.Zero。会设置GetLastError</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        public static extern System.IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 这个函数用来置顶显示,参数hwnd为窗口句柄
        /// </summary>
        /// <param name="hwnd">窗口句柄。</param>
        [System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        public static extern void SetForegroundWindow(System.IntPtr hwnd);

        ///// <summary>
        ///// 这个函数用来置顶显示,参数hwnd为窗口句柄
        ///// </summary>
        ///// <param name="hwnd">窗口句柄。</param>
        //[System.Runtime.InteropServices.DllImport("user32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //public static extern void SetForegroundWindow(int hwnd);

        //[System.Runtime.InteropServices.DllImport("User32.dll", CharSet = System.Runtime.InteropServices.CharSet.Auto)]
        //public static extern int ShowWindow(System.IntPtr hWnd, short cmdShow);

        /// <summary>
        /// 这个函数用来显示窗口,参数hwnd为窗口句柄,nCmdShow是显示类型的枚举
        /// </summary>
        /// <param name="hWnd">窗口句柄。</param>
        /// <param name="nCmdShow">操作类型。</param>
        /// <returns>如窗口之前是可见的，则返回TRUE，否则返回FALSE。</returns>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        public static extern bool ShowWindow(System.IntPtr hWnd, ShowWindowStyles nCmdShow);

        /// <summary>
        /// 获取文件信息。
        /// </summary>
        /// <param name="pszPath">文件路径。</param>
        /// <param name="dwFileAttributes">文件属性。</param>
        /// <param name="psfi">文件信息输出。</param>
        /// <param name="cbFileInfo">文件信息存储结构大小。</param>
        /// <param name="uFlags">获取信息标志位。</param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("shell32.dll", EntryPoint = "SHGetFileInfo")]
        public static extern System.IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbFileInfo, uint uFlags);

        /// <summary>
        /// 获取指定窗口（包括边框、滚动条、标题栏、菜单等）的设备场景。（用完后一定要用ReleaseDC函数释放场景）
        /// </summary>
        /// <param name="hWnd">将获取其设备场景的窗口</param>
        /// <returns>执行成功为窗口设备场景，失败则为0。</returns>
        /// <remarks>
        /// 获得的设备环境覆盖了整个窗口（包括非客户区），例如标题栏、菜单、滚动条，以及边框。这使得程序能够在非客户区域实现自定义图形，例如自定义标题或者边框。当不再需要该设备环境时，需要调用ReleaseDC函数释放设备环境。注意，该函数只获得通用设备环境，该设备环境的任何属性改变都不会反映到窗口的私有或者类设备环境中（如果窗口有的话）!
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern System.IntPtr GetWindowDC(System.IntPtr hWnd);

        /// <summary>
        /// 函数释放设备上下文环境（DC）供其他应用程序使用。函数的效果与设备上下文环境类型有关。它只释放公用的和设备上下文环境，对于类或私有的则无效。
        /// </summary>
        /// <param name="hWnd">指向要释放的设备上下文环境所在的窗口的句柄。</param>
        /// <param name="hDC">指向要释放的设备上下文环境的句柄。</param>
        /// <returns>返回值说明了设备上下文环境是否释放；如果释放成功，则返回值为1；如果没有释放成功，则返回值为0。</returns>
        /// <remarks>
        /// 每次调用GetWindowDC和GetDC函数检索公用设备上下文环境之后，应用程序必须调用ReleaseDC函数来释放设备上下文环境。
        /// 应用程序不能调用ReleaseDC函数来释放由CreateDC函数创建的设备上下文环境，只能使用DeleteDC函数。
        /// </remarks>
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        public static extern int ReleaseDC(System.IntPtr hWnd, System.IntPtr hDC);

        /// <summary>
        /// 清除图标
        /// </summary>
        /// <param name="handle">图标句柄</param>
        /// <returns>非零表示成功，零表示失败。会设置GetLastError</returns>
        /// <remarks>
        /// 不要用这个函数清除随同LoadIcon函数载入的系统固有图标
        /// </remarks>
        [System.Runtime.InteropServices.DllImportAttribute("user32.dll")]
        [return: System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.Bool)]
        private static extern bool DestroyIcon(System.IntPtr handle);

    }
}
