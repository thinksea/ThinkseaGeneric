/*
 * 实现文件系统管理功能封装。提供共享文件和文件系统访问权限控制能力。
 */
namespace Thinksea.Windows
{
    /// <summary>
    /// 共享资源类型。
    /// </summary>
    public enum ShareResourceType : uint
    {
        /// <summary>
        /// 磁盘驱动器
        /// </summary>
        DiskDrive = 0x0,
        /// <summary>
        /// 打印队列。
        /// </summary>
        PrintQueue = 0x1,
        /// <summary>
        /// 设备
        /// </summary>
        Device = 0x2,
        /// <summary>
        /// IPC
        /// </summary>
        IPC = 0x3,
        /// <summary>
        /// 为管理目的设置的磁盘驱动器共享。
        /// </summary>
        DiskDriveAdmin = 0x80000000,
        /// <summary>
        /// 为管理目的设置的打印队列共享。
        /// </summary>
        PrintQueueAdmin = 0x80000001,
        /// <summary>
        /// 为管理目的设置的设备共享。
        /// </summary>
        DeviceAdmin = 0x80000002,
        /// <summary>
        /// 为管理目的设置的 IPC 共享。
        /// </summary>
        IPCAdmin = 0x80000003
    }

    /// <summary>
    /// 共享配置节。
    /// </summary>
    public class Share
    {
        /// <summary>
        /// 共享的目录路径。
        /// </summary>
        public string Path
        {
            get;
            set;
        }
        /// <summary>
        /// 共享名。
        /// </summary>
        public string Name
        {
            get;
            set;
        }
        private string _Description = null;
        /// <summary>
        /// 注释。
        /// </summary>
        public string Description
        {
            get
            {
                return this._Description;
            }
            set
            {
                this._Description = value;
            }
        }
        private ShareResourceType _Type = ShareResourceType.DiskDrive;
        /// <summary>
        /// 共享类型。
        /// </summary>
        public ShareResourceType Type
        {
            get
            {
                return this._Type;
            }
            set
            {
                this._Type = value;
            }
        }
        private int _MaximumAllowed = 0;
        /// <summary>
        /// 允许连接的最大用户数量。取值为 0 表示允许最多用户连接；否则设置为正整数表示允许连接的用户数量限制。
        /// </summary>
        public int MaximumAllowed
        {
            get
            {
                return this._MaximumAllowed;
            }
            set
            {
                this._MaximumAllowed = value;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="Path">共享的目录路径。</param>
        /// <param name="Name">共享名。</param>
        public Share(string Path, string Name)
        {
            this.Path = Path;
            this.Name = Name;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="Path">共享的目录路径。</param>
        public Share(string Path)
        {
            this.Path = Path;
            this.Name = Path;
        }

    }

    /// <summary>
    /// 文件系统共享管理类。
    /// </summary>
    /// <example>
    /// <code>
    ///string path = @"c:\testabc";
    ///ShareManagement fss = new ShareManagement();
    /// //如果存在指定共享则删除
    ///if (fss.IsExistsShare(path))
    ///{
    ///    fss.DeleteShare(path);
    ///}
    ///
    /// //创建共享
    ///Share sre = new Share(path);
    ///sre.MaximumAllowed = 0;
    ///fss.CreateShare(sre);
    ///
    /// //获取全部的共享
    ///foreach (Share tmp in fss.GetShares())
    ///{
    ///    System.Console.WriteLine(tmp.Name + ":" + tmp.Type.ToString());
    ///}
    /// </code>
    /// </example>
    public class ShareManagement
    {
        /// <summary>
        /// 获取指定名称的共享。
        /// </summary>
        /// <param name="ShareName">共享名称。</param>
        /// <returns>共享信息。找不到返回 null。</returns>
        public Share GetShare(string ShareName)
        {
            ShareName = ShareName.ToLower();
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_share");
            foreach (System.Management.ManagementObject share in searcher.Get())
            {
                if (((string)share.GetPropertyValue("Name")).ToLower() == ShareName)
                {
                    Share s = new Share((string)share.GetPropertyValue("Path"), (string)share.GetPropertyValue("Name"));
                    s.Description = (string)share.GetPropertyValue("Description");
                    s.Type = (ShareResourceType)share.GetPropertyValue("Type");
                    if ((bool)share.GetPropertyValue("AllowMaximum"))
                    {
                        s.MaximumAllowed = 0;
                    }
                    else
                    {
                        s.MaximumAllowed = System.Convert.ToInt32(share.GetPropertyValue("MaximumAllowed"));
                    }
                    return s;
                }
            }
            return null;

        }

        /// <summary>
        /// 获取全部的共享信息。
        /// </summary>
        /// <returns>共享信息集合。</returns>
        public Share[] GetShares()
        {
            System.Collections.Generic.List<Share> l = new System.Collections.Generic.List<Share>();
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_share");
            foreach (System.Management.ManagementObject share in searcher.Get())
            {
                Share s = new Share((string)share.GetPropertyValue("Path"), (string)share.GetPropertyValue("Name"));
                s.Description = (string)share.GetPropertyValue("Description");
                s.Type = (ShareResourceType)share.GetPropertyValue("Type");
                if ((bool)share.GetPropertyValue("AllowMaximum"))
                {
                    s.MaximumAllowed = 0;
                }
                else
                {
                    s.MaximumAllowed = System.Convert.ToInt32(share.GetPropertyValue("MaximumAllowed"));
                }
                l.Add(s);
                //l.Add(share.GetText(System.Management.TextFormat.Mof));
            }
            return l.ToArray();
        }

        /// <summary>
        /// 判断指定的共享是否存在。
        /// </summary>
        /// <param name="ShareName">共享名称。</param>
        /// <returns>存在返回 true；否则返回 false。</returns>
        public bool IsExistsShare(string ShareName)
        {
            ShareName = ShareName.ToLower();
            System.Management.ManagementObjectSearcher searcher = new System.Management.ManagementObjectSearcher("SELECT * FROM Win32_share");
            foreach (System.Management.ManagementObject share in searcher.Get())
            {
                if (((string)share.GetPropertyValue("Name")).ToLower() == ShareName)
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// 为指定的目录开启共享设置。
        /// </summary>
        /// <param name="sre">共享配置节</param>
        public void CreateShare(Share sre)
        {
            System.Management.ManagementClass mc = new System.Management.ManagementClass("win32_share");
            System.Management.ManagementBaseObject inParams = mc.GetMethodParameters("Create");
            inParams["Path"] = sre.Path;
            inParams["Name"] = sre.Name;
            inParams["Type"] = sre.Type;
            if (sre.MaximumAllowed == 0)
            {
                inParams["MaximumAllowed"] = null;      //null = 用户数连接无限制,否则指定一个正整数
            }
            else
            {
                inParams["MaximumAllowed"] = sre.MaximumAllowed;      //null = 用户数连接无限制,否则指定一个正整数
            }
            inParams["Description"] = sre.Description;
            inParams["Password"] = null;
            inParams["Access"] = null; //null = 使Everyone拥有完全控制权限

            System.Management.ManagementBaseObject outParams = mc.InvokeMethod("Create", inParams, null);
            switch ((uint)outParams.Properties["ReturnValue"].Value)
            {
                case 0: //Success
                    break;
                case 2: //Access denied 
                    throw new System.Exception("无权访问");
                case 8: //Unknown failure 
                    throw new System.Exception("未知失败");
                case 9: //Invalid name 
                    throw new System.Exception("非法的共享名");
                case 10: //Invalid level 
                    throw new System.Exception("非法的层次");
                case 21: //Invalid parameter 
                    throw new System.Exception("非法的参数");
                case 22: //Duplicate share
                    throw new System.Exception("重复共享");
                case 23: //Redirected path 
                    throw new System.Exception("重定向路径");
                case 24: //Unknown device or directory 
                    throw new System.Exception("未知的目录");
                case 25: //Net name not found 
                    throw new System.Exception("网络名不存在");
                default:
                    throw new System.Exception("未知异常信息");
            }

        }
        /// <summary>
        /// 取消共享指定目录。
        /// </summary>
        /// <param name="ShareName">共享名称。</param>
        public void DeleteShare(string ShareName)
        {
            System.Management.ManagementObject classInstance = new System.Management.ManagementObject(@"root\CIMV2", "Win32_Share.Name='" + ShareName + @"'", null);
            System.Management.ManagementBaseObject outParams = classInstance.InvokeMethod("Delete", null, null);
            switch ((uint)outParams.Properties["ReturnValue"].Value)
            {
                case 0: //Success
                    break;
                case 2: //Access denied 
                    throw new System.Exception("无权访问");
                case 8: //Unknown failure 
                    throw new System.Exception("未知失败");
                case 9: //Invalid name 
                    throw new System.Exception("非法的共享名");
                case 10: //Invalid level 
                    throw new System.Exception("非法的层次");
                case 21: //Invalid parameter 
                    throw new System.Exception("非法的参数");
                case 22: //Duplicate share
                    throw new System.Exception("重复共享");
                case 23: //Redirected path 
                    throw new System.Exception("重定向路径");
                case 24: //Unknown device or directory 
                    throw new System.Exception("未知的目录");
                case 25: //Net name not found 
                    throw new System.Exception("网络名不存在");
                default:
                    throw new System.Exception("未知异常信息");
            }

        }

    }

}
