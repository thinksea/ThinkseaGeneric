namespace Thinksea.Windows
{
    /// <summary>
    /// 封装了对系统托盘图标的管理功能。
    /// </summary>
    public class SysTrayWnd
    {
        /// <summary>
        /// 描述图标信息。
        /// </summary>
        public struct TrayItemData
        {
            /// <summary>
            /// 进程ID。
            /// </summary>
            public int dwProcessID;
            public byte fsState;
            public byte fsStyle;
            /// <summary>
            /// 托盘图标句柄。
            /// </summary>
            public System.IntPtr hIcon;
            /// <summary>
            /// 进程句柄。
            /// </summary>
            public System.IntPtr hProcess;
            /// <summary>
            /// 窗口句柄。
            /// </summary>
            public System.IntPtr hWnd;
            public int idBitmap;
            public int idCommand;
            /// <summary>
            /// 进程映象路径。
            /// </summary>
            public string lpProcImagePath;
            /// <summary>
            /// 托盘图标提示内容。
            /// </summary>
            public string lpTrayToolTip;
            /// <summary>
            /// 获取图标。
            /// </summary>
            /// <returns></returns>
            public System.Drawing.Icon GetIcon()
            {
                System.Drawing.Icon icon = System.Drawing.Icon.FromHandle(this.hIcon);
                if (icon.Width == 0 && icon.Height == 0)
                {
                    return null;
                }
                return icon;
            }
        }

        /// <summary>
        /// 获取对托盘的引用。
        /// </summary>
        /// <returns></returns>
        public static System.IntPtr GetTrayWnd()
        {
            System.IntPtr handle = Thinksea.Windows.Win32API.User32.FindWindow("Shell_TrayWnd", null);
            handle = Thinksea.Windows.Win32API.User32.FindWindowEx(handle, System.IntPtr.Zero, "TrayNotifyWnd", null);
            handle = Thinksea.Windows.Win32API.User32.FindWindowEx(handle, System.IntPtr.Zero, "SysPager", null);
            handle = Thinksea.Windows.Win32API.User32.FindWindowEx(handle, System.IntPtr.Zero, "ToolbarWindow32", null);
            return handle;
        }

        /// <summary>
        /// 获取托盘图标详细信息。
        /// </summary>
        /// <returns></returns>
        public static TrayItemData[] GetTrayWndDetail()
        {
            System.Collections.Generic.SortedList<string, TrayItemData> stlTrayItems = new System.Collections.Generic.SortedList<string, TrayItemData>();
            try
            {
                Thinksea.Windows.Win32API.TBBUTTON tbButtonInfo = new Thinksea.Windows.Win32API.TBBUTTON();
                System.IntPtr hTrayWnd = System.IntPtr.Zero;
                System.IntPtr hTrayProcess = System.IntPtr.Zero;
                int iTrayProcessID = 0;
                int iAllocBaseAddress = 0;
                int iRet = 0;
                int iTrayItemCount = 0;

                hTrayWnd = GetTrayWnd();
                Thinksea.Windows.Win32API.User32.GetWindowThreadProcessId(hTrayWnd, ref iTrayProcessID);
                //hTrayProcess = Thinksea.Windows.Win32API.Kernel32.OpenProcess(
                //(uint)Thinksea.Windows.Win32API.ProcessAccessFlags.PROCESS_ALL_ACCESS |
                //(uint)Thinksea.Windows.Win32API.ProcessAccessFlags.PROCESS_VM_OPERATION |
                //(uint)Thinksea.Windows.Win32API.ProcessAccessFlags.PROCESS_VM_READ |
                //(uint)Thinksea.Windows.Win32API.ProcessAccessFlags.PROCESS_VM_WRITE, 0, (uint)iTrayProcessID);
                hTrayProcess = Thinksea.Windows.Win32API.Kernel32.OpenProcess(
                Thinksea.Windows.Win32API.ProcessAccessFlags.PROCESS_ALL_ACCESS |
                Thinksea.Windows.Win32API.ProcessAccessFlags.PROCESS_VM_OPERATION |
                Thinksea.Windows.Win32API.ProcessAccessFlags.PROCESS_VM_READ |
                Thinksea.Windows.Win32API.ProcessAccessFlags.PROCESS_VM_WRITE, false, (uint)iTrayProcessID);


                iAllocBaseAddress = Thinksea.Windows.Win32API.Kernel32.VirtualAllocEx(hTrayProcess, 0, System.Runtime.InteropServices.Marshal.SizeOf(tbButtonInfo), (int)Thinksea.Windows.Win32API.WindowsNumber.MEM_COMMIT, (int)Thinksea.Windows.Win32API.WindowsNumber.PAGE_READWRITE);
                iTrayItemCount = Thinksea.Windows.Win32API.User32.SendMessage(hTrayWnd, (int)Thinksea.Windows.Win32API.WindowsNumber.TB_BUTTONCOUNT, 0, 0);

                for (int i = 0; i < iTrayItemCount; i++)
                {
                    try
                    {
                        TrayItemData trayItem = new TrayItemData();
                        Thinksea.Windows.Win32API.TRAYDATA trayData = new Thinksea.Windows.Win32API.TRAYDATA();
                        int iOut = 0;
                        int dwProcessID = 0;
                        System.IntPtr hRelProcess = System.IntPtr.Zero;
                        string strTrayToolTip = string.Empty;

                        iRet = Thinksea.Windows.Win32API.User32.SendMessage(hTrayWnd, (int)Thinksea.Windows.Win32API.WindowsNumber.TB_GETBUTTON, i, iAllocBaseAddress);
                        System.IntPtr hButtonInfo = System.Runtime.InteropServices.Marshal.AllocHGlobal(System.Runtime.InteropServices.Marshal.SizeOf(tbButtonInfo));
                        System.IntPtr hTrayData = System.Runtime.InteropServices.Marshal.AllocHGlobal(System.Runtime.InteropServices.Marshal.SizeOf(trayData));

                        iRet = Thinksea.Windows.Win32API.Kernel32.ReadProcessMemory(hTrayProcess, iAllocBaseAddress, hButtonInfo, System.Runtime.InteropServices.Marshal.SizeOf(tbButtonInfo), out iOut);
                        System.Runtime.InteropServices.Marshal.PtrToStructure(hButtonInfo, tbButtonInfo);

                        iRet = Thinksea.Windows.Win32API.Kernel32.ReadProcessMemory(hTrayProcess, tbButtonInfo.dwData, hTrayData, System.Runtime.InteropServices.Marshal.SizeOf(trayData), out iOut);
                        System.Runtime.InteropServices.Marshal.PtrToStructure(hTrayData, trayData);

                        byte[] bytTextData = new byte[1024];
                        iRet = Thinksea.Windows.Win32API.Kernel32.ReadProcessMemory(hTrayProcess, tbButtonInfo.iString, bytTextData, 1024, out iOut);
                        strTrayToolTip = System.Text.Encoding.Unicode.GetString(bytTextData);
                        if (!string.IsNullOrEmpty(strTrayToolTip))
                        {
                            int iNullIndex = strTrayToolTip.IndexOf('\0');
                            strTrayToolTip = strTrayToolTip.Substring(0, iNullIndex);
                        }

                        Thinksea.Windows.Win32API.User32.GetWindowThreadProcessId(trayData.hwnd, ref dwProcessID);
                        //hRelProcess = Thinksea.Windows.Win32API.Kernel32.OpenProcess((uint)Thinksea.Windows.Win32API.ProcessAccessFlags.PROCESS_QUERY_INFORMATION, 0, (uint)dwProcessID);
                        hRelProcess = Thinksea.Windows.Win32API.Kernel32.OpenProcess(Thinksea.Windows.Win32API.ProcessAccessFlags.PROCESS_QUERY_INFORMATION, false, (uint)dwProcessID);
                        System.Text.StringBuilder sbProcImagePath = new System.Text.StringBuilder(256);
                        if (hRelProcess != System.IntPtr.Zero)
                        {
                            Thinksea.Windows.Win32API.Kernel32.GetProcessImageFileName(hRelProcess, sbProcImagePath, sbProcImagePath.Capacity);
                        }

                        string strImageFilePath = string.Empty;
                        if (sbProcImagePath.Length > 0)
                        {
                            int iDeviceIndex = sbProcImagePath.ToString().IndexOf("\\", "\\Device\\HarddiskVolume".Length);
                            string strDevicePath = sbProcImagePath.ToString().Substring(0, iDeviceIndex);
                            int iStartDisk = (int)'A';
                            while (iStartDisk <= (int)'Z')
                            {
                                System.Text.StringBuilder sbWindowImagePath = new System.Text.StringBuilder(256);
                                iRet = Thinksea.Windows.Win32API.Kernel32.QueryDosDevice(((char)iStartDisk).ToString() + ":", sbWindowImagePath, sbWindowImagePath.Capacity);
                                if (iRet != 0)
                                {
                                    if (sbWindowImagePath.ToString() == strDevicePath)
                                    {
                                        strImageFilePath = ((char)iStartDisk).ToString() + ":" + sbProcImagePath.ToString().Replace(strDevicePath, "");
                                        break;
                                    }
                                }
                                iStartDisk++;
                            }
                        }

                        trayItem.dwProcessID = dwProcessID;
                        trayItem.fsState = tbButtonInfo.fsState;
                        trayItem.fsStyle = tbButtonInfo.fsStyle;
                        trayItem.hIcon = trayData.hIcon;
                        trayItem.hProcess = hRelProcess;
                        trayItem.hWnd = trayData.hwnd;
                        trayItem.idBitmap = tbButtonInfo.iBitmap;
                        trayItem.idCommand = tbButtonInfo.idCommand;
                        trayItem.lpProcImagePath = strImageFilePath;
                        trayItem.lpTrayToolTip = strTrayToolTip;
                        stlTrayItems[string.Format("{0:d8}", tbButtonInfo.idCommand)] = trayItem;
                    }
                    catch { continue; }
                }

                Thinksea.Windows.Win32API.Kernel32.VirtualFreeEx(hTrayProcess, iAllocBaseAddress, System.Runtime.InteropServices.Marshal.SizeOf(tbButtonInfo), (int)Thinksea.Windows.Win32API.WindowsNumber.MEM_RELEASE);
                Thinksea.Windows.Win32API.Kernel32.CloseHandle(hTrayProcess);

                TrayItemData[] trayItems = new TrayItemData[stlTrayItems.Count];
                stlTrayItems.Values.CopyTo(trayItems, 0);
                return trayItems;
            }
            catch (System.Runtime.InteropServices.SEHException ex)
            {
                throw ex;
            }
            catch (System.Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 刷新托盘。
        /// </summary>
        public static void RefreshTrayWnd()
        {
            TrayItemData[] trayItems = GetTrayWndDetail();
            System.IntPtr hTrayWnd = GetTrayWnd();
            for (int i = trayItems.Length - 1; i >= 0; i--)
            {
                int iProcessExitCode = 0;
                Thinksea.Windows.Win32API.Kernel32.GetExitCodeProcess(trayItems[i].hProcess, ref iProcessExitCode);
                if (iProcessExitCode != (int)Thinksea.Windows.Win32API.WindowsNumber.STILL_ACTIVE)
                {
                    //通过隐藏图标来达到刷新的动作
                    int iRet = Thinksea.Windows.Win32API.User32.SendMessage(hTrayWnd, (int)Thinksea.Windows.Win32API.WindowsNumber.TB_HIDEBUTTON, trayItems[i].idCommand, 0);
                }
            }
        }

        /// <summary>
        /// 获取与指定进程关联的托盘图标。
        /// </summary>
        /// <param name="hwnd">进程 Handler</param>
        /// <returns></returns>
        public static TrayItemData[] GetTrayWndDetail(System.IntPtr hwnd)
        {
            System.Collections.Generic.List<TrayItemData> r = new System.Collections.Generic.List<TrayItemData>();
            var icons = GetTrayWndDetail();
            foreach (var tmp in icons)
            {
                if (tmp.hProcess == hwnd)
                {
                    r.Add(tmp);
                }
            }
            return r.ToArray();
        }

        /// <summary>
        /// 获取无关联进程的托盘图标。
        /// </summary>
        /// <param name="hwnd">进程 Handler</param>
        /// <returns></returns>
        public static TrayItemData[] GetFreeTrayWndDetail()
        {
            System.Collections.Generic.List<TrayItemData> r = new System.Collections.Generic.List<TrayItemData>();
            var icons = GetTrayWndDetail();
            foreach (var tmp in icons)
            {
                if (tmp.hProcess == System.IntPtr.Zero)
                {
                    r.Add(tmp);
                }
            }
            return r.ToArray();
        }

        /// <summary>
        /// 清除所有的无关联图标。
        /// </summary>
        public static void KillFreeTrayWnd()
        {
            System.IntPtr hTrayWnd = SysTrayWnd.GetTrayWnd();
            TrayItemData[] dd = GetTrayWndDetail();
            for (int i = dd.Length - 1; i >= 0; i--)
            {
                if (dd[i].hProcess == System.IntPtr.Zero)
                {
                    Thinksea.Windows.Win32API.User32.SendMessage(hTrayWnd, (int)Thinksea.Windows.Win32API.WindowsNumber.TB_DELETEBUTTON, i, 0);
                }
            }
        }

    }
}