/*一个调用例子
//添加新网站
Thinksea.IIS.IISServer.IISManagement m = new Thinksea.IIS.IISServer.IISManagement();
if (!m.ExistsSite("我的网站"))
{
    Thinksea.IIS.IISServer.Site iws = new Thinksea.IIS.IISServer.Site();
    iws.Name = "我的网站";
    iws.Port = 81;
    iws.Path = @"c:\test";
    m.CreateSite(iws);
}
else
{
    Thinksea.IIS.IISServer.Site iws = m.GetSite("abcd");
    iws.Path = @"c:\test2";
    m.ModifySite(iws);
}

//添加应用程序
if (!m.ExistsApplication("我的网站", "新的应用程序名称"))
{
    Thinksea.IIS.IISServer.Application twvd = new Thinksea.IIS.IISServer.Application("新的应用程序名称");
    twvd.Path = @"c:\test\a";
    m.CreateApplication("我的网站", twvd, false);
}
else
{
    Thinksea.IIS.IISServer.Application twvd = m.GetApplication("我的网站", "新的应用程序名称");
    twvd.Path = @"c:\test2\a";
    m.ModifyApplication("我的网站", twvd);
}

foreach (var tmp in m.GetSite())
{
    Console.WriteLine(tmp.Name + " | " + tmp.Path);
    foreach (var tmp2 in m.GetApplication(tmp.Name))
    {
        Console.WriteLine("    " + tmp2.Name + " | " + tmp2.Path);
        
    }
}
*/

namespace Thinksea.IIS.IISServer
{
    /// <summary>
    /// 由于 IIS 版本限制，不能执行指定的操作时引发的异常。
    /// </summary>
    public class IISVersionException : System.ApplicationException
    {
        /// <summary>
        /// 初始化 IISVersionException 类的新实例。
        /// </summary>
        public IISVersionException()
        {
        }
        /// <summary>
        /// 使用指定错误信息初始化 IISVersionException 类的新实例。
        /// </summary>
        /// <param name="message">异常错误信息</param>
        public IISVersionException(string message)
            : base(message)
        {
        }
        /// <summary>
        /// 使用指定错误信息和对导致此异常的内部异常的引用来初始化 IISVersionException 类的新实例。
        /// </summary>
        /// <param name="message">异常错误信息</param>
        /// <param name="inner">作为当前异常的原因的异常。</param>
        public IISVersionException(string message, System.Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// 网站的状态。
    /// </summary>
    public enum SiteState
    {
        /// <summary>
        /// 正在启动。
        /// </summary>
        Starting = 1,
        /// <summary>
        /// 已经启动。
        /// </summary>
        Started = 2,
        /// <summary>
        /// 正在停止。
        /// </summary>
        Stopping = 3,
        /// <summary>
        /// 已经停止。
        /// </summary>
        Stopped = 4,
        /// <summary>
        /// 正在暂停。
        /// </summary>
        Pausing = 5,
        /// <summary>
        /// 已经暂停。
        /// </summary>
        Paused = 6,
        /// <summary>
        /// 
        /// </summary>
        Continuing = 7,

    }
    /// <summary>
    /// ASP.NET 版本。
    /// </summary>
    public enum AspNetVersion
    {
        /// <summary>
        /// 默认设置。
        /// </summary>
        Default,
        /// <summary>
        /// ASP.NET 1.1
        /// </summary>
        ASP_NET_1_1,
        /// <summary>
        /// ASP.NET 2.0
        /// </summary>
        ASP_NET_2_0,
        /// <summary>
        /// ASP.NET 4.0
        /// </summary>
        ASP_NET_4_0,
    }

    /// <summary>
    /// IIS 管理类。支持 IIS 6
    /// </summary>
    public class IISManagement
    {
        #region ASP.NET 脚本影射。
        /// <summary>
        /// ASP.NET 1.1 脚本影射。
        /// </summary>
        private static readonly string[] ScriptMaps_1_1 = new string[] {
        @".asp,C:\WINDOWS\system32\inetsrv\asp.dll,5,GET,HEAD,POST,TRACE"
        , @".cer,C:\WINDOWS\system32\inetsrv\asp.dll,5,GET,HEAD,POST,TRACE"
        , @".cdx,C:\WINDOWS\system32\inetsrv\asp.dll,5,GET,HEAD,POST,TRACE"
        , @".asa,C:\WINDOWS\system32\inetsrv\asp.dll,5,GET,HEAD,POST,TRACE"
        , @".idc,C:\WINDOWS\system32\inetsrv\httpodbc.dll,5,GET,POST"
        , @".shtm,C:\WINDOWS\system32\inetsrv\ssinc.dll,5,GET,POST"
        , @".shtml,C:\WINDOWS\system32\inetsrv\ssinc.dll,5,GET,POST"
        , @".stm,C:\WINDOWS\system32\inetsrv\ssinc.dll,5,GET,POST"
        , @".asax,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".ascx,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".ashx,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".asmx,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".aspx,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".axd,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".vsdisco,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".rem,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".soap,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".config,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".cs,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".csproj,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".vb,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".vbproj,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".webinfo,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".licx,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".resx,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".resources,C:\WINDOWS\Microsoft.NET\Framework\v1.1.4322\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        };
        /// <summary>
        /// ASP.NET 2.0 脚本影射。
        /// </summary>
        private static readonly string[] ScriptMaps_2_0 = new string[] {
        @".asp,C:\WINDOWS\system32\inetsrv\asp.dll,5,GET,HEAD,POST,TRACE"
        , @".cer,C:\WINDOWS\system32\inetsrv\asp.dll,5,GET,HEAD,POST,TRACE"
        , @".cdx,C:\WINDOWS\system32\inetsrv\asp.dll,5,GET,HEAD,POST,TRACE"
        , @".asa,C:\WINDOWS\system32\inetsrv\asp.dll,5,GET,HEAD,POST,TRACE"
        , @".idc,C:\WINDOWS\system32\inetsrv\httpodbc.dll,5,GET,POST"
        , @".shtm,C:\WINDOWS\system32\inetsrv\ssinc.dll,5,GET,POST"
        , @".shtml,C:\WINDOWS\system32\inetsrv\ssinc.dll,5,GET,POST"
        , @".stm,C:\WINDOWS\system32\inetsrv\ssinc.dll,5,GET,POST"
        , @".asax,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".ascx,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".ashx,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".asmx,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".aspx,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".axd,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".vsdisco,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".rem,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".soap,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".config,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".cs,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".csproj,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".vb,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".vbproj,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".webinfo,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".licx,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".resx,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".resources,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".master,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".skin,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".compiled,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".browser,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".mdb,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".jsl,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".vjsproj,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".sitemap,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".msgx,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".ad,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".dd,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".ldd,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".sd,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".cd,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".adprototype,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".lddprototype,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".sdm,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".sdmDocument,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".ldb,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".svc,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".mdf,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".ldf,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".java,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".exclude,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".refresh,c:\windows\microsoft.net\framework\v2.0.50727\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        };

        /// <summary>
        /// ASP.NET 4.0 脚本影射。
        /// </summary>
        private static readonly string[] ScriptMaps_4_0 = new string[] {
        @".asp,C:\WINDOWS\system32\inetsrv\asp.dll,5,GET,HEAD,POST,TRACE"
        , @".cer,C:\WINDOWS\system32\inetsrv\asp.dll,5,GET,HEAD,POST,TRACE"
        , @".cdx,C:\WINDOWS\system32\inetsrv\asp.dll,5,GET,HEAD,POST,TRACE"
        , @".asa,C:\WINDOWS\system32\inetsrv\asp.dll,5,GET,HEAD,POST,TRACE"
        , @".idc,C:\WINDOWS\system32\inetsrv\httpodbc.dll,5,GET,POST"
        , @".shtm,C:\WINDOWS\system32\inetsrv\ssinc.dll,5,GET,POST"
        , @".shtml,C:\WINDOWS\system32\inetsrv\ssinc.dll,5,GET,POST"
        , @".stm,C:\WINDOWS\system32\inetsrv\ssinc.dll,5,GET,POST"
        , @".asax,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".ascx,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".ashx,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".asmx,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".aspx,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".axd,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".vsdisco,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".rem,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".soap,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".config,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".cs,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".csproj,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".vb,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".vbproj,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".webinfo,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".licx,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".resx,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".resources,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".master,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".skin,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".compiled,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".browser,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".mdb,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".jsl,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".vjsproj,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".sitemap,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".msgx,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".ad,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".dd,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".ldd,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".sd,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".cd,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".adprototype,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".lddprototype,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".sdm,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".sdmDocument,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".ldb,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".svc,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".mdf,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".ldf,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".java,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".exclude,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".refresh,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"

#region  ASP.NET 4.0 版本新增内容
        , @".xamlx,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,1,GET,HEAD,POST,DEBUG"
        , @".aspq,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".cshtm,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".cshtml,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".vbhtm,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".vbhtml,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5,GET,HEAD,POST,DEBUG"
        , @".xoml,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,1"
        , @".rules,c:\windows\microsoft.net\framework\v4.0.30319\aspnet_isapi.dll,5"
#endregion
        };

        #endregion

        private string _Machinename = @"localhost";
        /// <summary>
        /// 获取机器名,默认值为 localhost
        /// </summary>
        public string Machinename
        {
            get
            {
                return this._Machinename;
            }
        }

        private System.DirectoryServices.DirectoryEntry _Service = null;
        /// <summary>
        /// 获取 IIS 服务对象。
        /// </summary>
        private System.DirectoryServices.DirectoryEntry Service
        {
            get
            {
                if (this._Service == null)
                {
                    this._Service = new System.DirectoryServices.DirectoryEntry("IIS://" + this.Machinename + "/W3SVC");
                }
                return this._Service;
            }

        }

        private System.DirectoryServices.DirectoryEntry _ApplicationPoolService = null;
        /// <summary>
        /// 获取 IIS 应用程序池服务对象。
        /// </summary>
        private System.DirectoryServices.DirectoryEntry ApplicationPoolService
        {
            get
            {
                if (this._ApplicationPoolService == null)
                {
                    this._ApplicationPoolService = new System.DirectoryServices.DirectoryEntry("IIS://" + this.Machinename + "/W3SVC/AppPools");
                }
                return this._ApplicationPoolService;
            }

        }

        private System.DirectoryServices.DirectoryEntry _IIS51Service = null;
        /// <summary>
        /// 获取 IIS 5.1 服务对象。
        /// </summary>
        private System.DirectoryServices.DirectoryEntry IIS51Service
        {
            get
            {
                if (this._IIS51Service == null)
                {
                    this._IIS51Service = new System.DirectoryServices.DirectoryEntry("IIS://" + this.Machinename + "/W3SVC/1");
                }
                return this._IIS51Service;
            }

        }

        /// <summary>
        /// 获取默认 IIS 匿名访问用户名。
        /// </summary>
        private string DefaultAnonymousUserName
        {
            get
            {
                return (string)this.Service.Properties["AnonymousUserName"][0];
            }
        }

        /// <summary>
        /// 获取默认 IIS 匿名访问用户密码。
        /// </summary>
        private string DefaultAnonymousUserPass
        {
            get
            {
                return (string)this.Service.Properties["AnonymousUserPass"][0];
            }
        }

        /// <summary>
        /// 初始化此实例。
        /// </summary>
        public IISManagement()
        {

        }

        /// <summary>
        /// 用指定的机器名初始化此实例。
        /// </summary>
        /// <param name="MachineName">机器名,默认值为localhost</param>
        private IISManagement(string MachineName)
        {
            if (MachineName.ToString() != "")
            {
                this._Machinename = MachineName;
            }

        }
        /// <summary>
        /// 一个析构方法，用于释放占用的资源。
        /// </summary>
        ~IISManagement()
        {
            if (this._Service != null)
            {
                this._Service.Close();
                this._Service = null;
            }

        }


        /// <summary>
        /// 获取指定注册表键的值。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="hive"></param>
        /// <param name="machineName">指定计算机名称。取值为 string.Empty 表示获取本机信息。</param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="kind"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool GetRegistryValue<T>(Microsoft.Win32.RegistryHive hive, string machineName, string key, string value, Microsoft.Win32.RegistryValueKind kind, out T data)
        {
            bool success = false;
            data = default(T);

            using (Microsoft.Win32.RegistryKey baseKey = Microsoft.Win32.RegistryKey.OpenRemoteBaseKey(hive, machineName))
            {
                if (baseKey != null)
                {
                    using (Microsoft.Win32.RegistryKey registryKey = baseKey.OpenSubKey(key, Microsoft.Win32.RegistryKeyPermissionCheck.ReadSubTree))
                    {
                        if (registryKey != null)
                        {
                            try
                            {
                                // If the key was opened, try to retrieve the value.
                                Microsoft.Win32.RegistryValueKind kindFound = registryKey.GetValueKind(value);
                                if (kindFound == kind)
                                {
                                    object regValue = registryKey.GetValue(value, null);
                                    if (regValue != null)
                                    {
                                        data = (T)System.Convert.ChangeType(regValue, typeof(T), System.Globalization.CultureInfo.InvariantCulture);
                                        success = true;
                                    }
                                }
                            }
                            catch (System.IO.IOException)
                            {
                                throw;
                                // The registry value doesn't exist. Since the
                                // value doesn't exist we have to assume that
                                // the component isn't installed and return
                                // false and leave the data param as the
                                // default value.
                            }
                        }
                    }
                }
            }
            return success;
        }

        /// <summary>
        /// 获取服务器IIS主版本号。
        /// </summary>
        /// <returns>IIS 服务器主版本号，如果无法识别则返回 -1。</returns>
        public int GetIISMajorVersion()
        {
            string IISRegKeyName = "Software\\Microsoft\\InetStp";
            string IISRegKeyValue = "MajorVersion";
            //string IISRegKeyMinorVersionValue = "MinorVersion";
            string machineName = (this.Machinename == "localhost") ? string.Empty : this.Machinename;

            int regValue = 0;

            if (GetRegistryValue(Microsoft.Win32.RegistryHive.LocalMachine, machineName, IISRegKeyName, IISRegKeyValue, Microsoft.Win32.RegistryValueKind.DWord, out regValue))
            {
                return regValue;
            }

            return -1;

            //try
            //{
            //    string path = "IIS://" + this.Machinename + "/W3SVC/INFO";
            //    System.DirectoryServices.DirectoryEntry entry = new System.DirectoryServices.DirectoryEntry(path);
            //    int num = 5;
            //    num = (int)entry.Properties["MajorIISVersionNumber"].Value;
            //    switch (num)
            //    {
            //        case 4:
            //            return IISVersion.IIS4;
            //        case 5:
            //            return IISVersion.IIS5;
            //        case 6:
            //            return IISVersion.IIS6;
            //        case 7:
            //            return IISVersion.IIS7;
            //        default:
            //            return IISVersion.Unknown;
            //    }
            //}
            //catch (System.Runtime.InteropServices.COMException comex)
            //{
            //    #region 最大可能兼容性。如果常规方式无法获取 IIS 版本号，则尝试通过注册表方式获取。
            //    if ((ulong)(comex.ErrorCode) == 0xFFFFFFFF80005000)//System.Runtime.InteropServices.COMException (0x80005000): 未知错误(0x80005000)
            //    {
            //    }
            //    #endregion

            //    throw;
            //}
        }

        /// <summary>
        /// 获取服务器IIS次要版本号。
        /// </summary>
        /// <returns>IIS 服务器次要版本号，如果无法识别则返回 -1。</returns>
        public int GetIISMinorVersion()
        {
            string IISRegKeyName = "Software\\Microsoft\\InetStp";
            string IISRegKeyMinorVersionValue = "MinorVersion";
            string machineName = (this.Machinename == "localhost") ? string.Empty : this.Machinename;

            int minorVersion = 0;
            if (GetRegistryValue(Microsoft.Win32.RegistryHive.LocalMachine, machineName, IISRegKeyName, IISRegKeyMinorVersionValue, Microsoft.Win32.RegistryValueKind.DWord, out minorVersion))
            {
                return minorVersion;
            }
            return -1;
        }

        /// <summary>
        /// 返回指定的网站。
        /// </summary>
        /// <param name="index">用基础目录服务命名的对象的名称。</param>
        /// <returns>找不到返回 null。</returns>
        private System.DirectoryServices.DirectoryEntry returnSite(long index)
        {
            int MajorVersion = this.GetIISMajorVersion();
            int MinorVersion = this.GetIISMinorVersion();
            if (MajorVersion == 5 && MinorVersion == 1) //IIS 5.1
            {
                if (this.IIS51Service != null && long.Parse(this.IIS51Service.Name) == index)
                {
                    return this.IIS51Service;
                }
            }
            else if (MajorVersion == 6) //IIS 6
            {
                System.Collections.IEnumerator ie = this.Service.Children.GetEnumerator();

                while (ie.MoveNext())
                {
                    System.DirectoryServices.DirectoryEntry Server = (System.DirectoryServices.DirectoryEntry)ie.Current;
                    if (Server.SchemaClassName == "IIsWebServer")
                    {
                        if (int.Parse(Server.Name) == index)
                        {
                            return Server;
                        }
                    }
                }

                //System.DirectoryServices.DirectoryEntry Server = new System.DirectoryServices.DirectoryEntry("IIS://" + IISManagement.Machinename + "/W3SVC/" + index);
                //try
                //{
                //    System.Collections.IEnumerator ie = Server.Children.GetEnumerator();
                //    return Server;
                //}
                //catch
                //{
                //    return null;
                //}
            }
            else
            {
                throw new IISVersionException("未知的 IIS 版本。");
            }
            return null;
        }
        /// <summary>
        /// 返回指定的网站。
        /// </summary>
        /// <param name="Name">网站名称</param>
        /// <returns>找不到返回 null。</returns>
        private System.DirectoryServices.DirectoryEntry returnSite(string Name)
        {
            Name = Name.ToLowerInvariant().Trim();
            int MajorVersion = this.GetIISMajorVersion();
            int MinorVersion = this.GetIISMinorVersion();
            if (MajorVersion == 5 && MinorVersion == 1) //IIS 5.1
            {
                if (this.IIS51Service != null && this.IIS51Service.Properties["ServerComment"][0].ToString().ToLowerInvariant().Trim() == Name)
                {
                    return this.IIS51Service;
                }
            }
            else if (MajorVersion == 6) //IIS 6
            {
                System.Collections.IEnumerator ie = this.Service.Children.GetEnumerator();

                while (ie.MoveNext())
                {
                    System.DirectoryServices.DirectoryEntry Server = (System.DirectoryServices.DirectoryEntry)ie.Current;
                    if (Server.SchemaClassName == "IIsWebServer")
                    {
                        if (Server.Properties["ServerComment"][0].ToString().ToLowerInvariant().Trim() == Name)
                        {
                            return Server;
                        }
                    }
                }
            }
            else
            {
                throw new IISVersionException("未知的 IIS 版本。");
            }
            return null;
        }

        /// <summary>
        /// 获取指定 DirectoryEntry 的根。
        /// </summary>
        /// <param name="Server">一个 System.DirectoryServices.DirectoryEntry 实例。</param>
        /// <returns>找不到返回 null。</returns>
        private System.DirectoryServices.DirectoryEntry getRoot(System.DirectoryServices.DirectoryEntry Server)
        {
            foreach (System.DirectoryServices.DirectoryEntry child in Server.Children)
            {
                string name = child.Name.ToLowerInvariant();
                if (name == "iiswebvirtualdir" || name == "root")
                {
                    return child;
                }
            }
            return null;

        }
        /// <summary>
        /// 尝试将 DirectoryEntry 转换成 IISWebServer 对象。
        /// </summary>
        /// <param name="Server">一个 DirectoryEntry 实例。</param>
        /// <returns>成功转换的 IISWebServer 对象。</returns>
        private static Site ConvertToSite(System.DirectoryServices.DirectoryEntry Server)
        {
            if (Server.SchemaClassName != "IIsWebServer")
            {
                throw new System.Exception("指定的 DirectoryEntry 不能转换为 IISWebServer 对象。");
            }
            Site item = new Site();
            System.DirectoryServices.DirectoryEntry Root = null;
            #region 填充网站信息。
            item._Index = System.Convert.ToInt32(Server.Name);
            item.Name = (string)Server.Properties["ServerComment"][0];
            item.AccessRead = (bool)Server.Properties["AccessRead"][0];
            item.AccessScript = (bool)Server.Properties["AccessScript"][0];
            item.DefaultDoc.AddRange(((string)Server.Properties["DefaultDoc"][0]).Split(','));
            item.EnableDefaultDoc = (bool)Server.Properties["EnableDefaultDoc"][0];
            item.EnableDirBrowsing = (bool)Server.Properties["EnableDirBrowsing"][0];
            item.AccessWrite = (bool)Server.Properties["AccessWrite"][0];
            item.AccessExecute = (bool)Server.Properties["AccessExecute"][0];
            item.AuthAnonymous = (bool)Server.Properties["AuthAnonymous"][0];
            item.AuthBasic = (bool)Server.Properties["AuthBasic"][0];
            item.AuthNTLM = (bool)Server.Properties["AuthNTLM"][0];
            item.DontLog = (bool)Server.Properties["DontLog"][0];
            item.ContentIndexed = (bool)Server.Properties["ContentIndexed"][0];
            item.AspEnableParentPaths = (bool)Server.Properties["AspEnableParentPaths"][0];
            item.AnonymousUserName = (string)Server.Properties["AnonymousUserName"][0];
            item.AnonymousUserPass = (string)Server.Properties["AnonymousUserPass"][0];
            #region 解析网站标识。
            //string Serverbindings = (string)Server.Properties["Serverbindings"][0];
            //item.Port = System.Convert.ToInt32(Serverbindings.Substring(1, Serverbindings.Length - 2));
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"(?<IP>[^:]*):(?<Port>[^:]*):(?<HostHeader>[^:]*)", System.Text.RegularExpressions.RegexOptions.Compiled);
            System.DirectoryServices.PropertyValueCollection pvc = Server.Properties["Serverbindings"];
            for (int i = 0; i < pvc.Count; i++)
            {
                if (i == 0)
                {
                    System.Text.RegularExpressions.Match mc = reg.Match((string)pvc[i]);
                    if (mc.Success)
                    {
                        item.IP = mc.Groups["IP"].Value;
                        item.Port = int.Parse(mc.Groups["Port"].Value);
                        item.HostHeader = mc.Groups["HostHeader"].Value;
                    }
                }
                else
                {
                    System.Text.RegularExpressions.Match mc = reg.Match((string)pvc[i]);
                    if (mc.Success)
                    {
                        item.Marks.Add(new IISServer.Mark(mc.Groups["IP"].Value, int.Parse(mc.Groups["Port"].Value), mc.Groups["HostHeader"].Value));
                    }
                }
            }
            //item.IP = System.Text.RegularExpressions.Regex.Replace(Serverbindings, @"(?<IP>[^:]*):(?<Port>[^:]*):(?<HostHeader>[^:]*)", "${IP}");
            //item.Port = System.Convert.ToInt32(System.Text.RegularExpressions.Regex.Replace(Serverbindings, @"(?<IP>[^:]*):(?<Port>[^:]*):(?<HostHeader>[^:]*)", "${Port}"));
            //item.HostHeader = System.Text.RegularExpressions.Regex.Replace(Serverbindings, @"(?<IP>[^:]*):(?<Port>[^:]*):(?<HostHeader>[^:]*)", "${HostHeader}");
            #endregion

            System.Collections.IEnumerator ieRoot = Server.Children.GetEnumerator();
            while (ieRoot.MoveNext())
            {
                Root = (System.DirectoryServices.DirectoryEntry)ieRoot.Current;
                if (Root.SchemaClassName == "IIsWebVirtualDir")
                {
                    item.Path = Root.Properties["path"][0].ToString();
                    item.AppAspNetVersion = AspNetVersion.Default;
                    foreach (string tmp in Root.Properties["ScriptMaps"])
                    {
                        if (tmp.ToLowerInvariant().Contains(@"microsoft.net\framework\v1.1.4322"))//Microsoft.NET\Framework\v1.1.4322
                        {
                            item.AppAspNetVersion = AspNetVersion.ASP_NET_1_1;
                            break;
                        }
                        else if (tmp.ToLowerInvariant().Contains(@"microsoft.net\framework\v2.0.50727"))//microsoft.net\framework\v2.0.50727
                        {
                            item.AppAspNetVersion = AspNetVersion.ASP_NET_2_0;
                            break;
                        }
                        else if (tmp.ToLowerInvariant().Contains(@"microsoft.net\framework\v4.0.30319"))//microsoft.net\framework\v4.0.30319
                        {
                            item.AppAspNetVersion = AspNetVersion.ASP_NET_4_0;
                            break;
                        }
                    }
                    item.AppFriendlyName = (string)Root.Properties["AppFriendlyName"][0];
                    //item.AppCreate = (bool)Root.Properties["AppCreate"][0];
                    break;
                }
            }

            #region 解析网站状态。
            item._ServerState = (SiteState)int.Parse(Server.Properties["ServerState"][0].ToString());
            //switch (Server.Properties["ServerState"][0].ToString())
            //{
            //    case "2":
            //        item._ServerState = IISServerState.Started;
            //        break;
            //    case "4":
            //        item._ServerState = IISServerState.Stopped;
            //        break;
            //    case "6":
            //        item._ServerState = IISServerState.Paused;
            //        break;
            //}
            #endregion

            #endregion

            return item;

        }
        /// <summary>
        /// 尝试将 Microsoft.Web.Administration.Site 转换成 IISWebServer 对象。
        /// </summary>
        /// <param name="Server">一个 Microsoft.Web.Administration.Site 实例。</param>
        /// <returns>成功转换的 IISWebServer 对象。</returns>
        private static Site ConvertToSite(Microsoft.Web.Administration.Site Server)
        {
            Site item = new Site();
            #region 填充网站信息。
            item._Index = System.Convert.ToInt64(Server.Id);
            item.Name = Server.Name;
            //item.AccessRead = (bool)Server.Properties["AccessRead"][0];
            //item.AccessScript = (bool)Server.Properties["AccessScript"][0];
            //item.DefaultDoc.AddRange(((string)Server.Properties["DefaultDoc"][0]).Split(','));
            //item.EnableDefaultDoc = (bool)Server.Properties["EnableDefaultDoc"][0];
            //item.EnableDirBrowsing = (bool)Server.Properties["EnableDirBrowsing"][0];
            //item.AccessWrite = (bool)Server.Properties["AccessWrite"][0];
            //item.AccessExecute = (bool)Server.Properties["AccessExecute"][0];
            //item.AuthAnonymous = (bool)Server.Properties["AuthAnonymous"][0];
            //item.AuthBasic = (bool)Server.Properties["AuthBasic"][0];
            //item.AuthNTLM = (bool)Server.Properties["AuthNTLM"][0];
            //item.DontLog = (bool)Server.Properties["DontLog"][0];
            //item.ContentIndexed = (bool)Server.Properties["ContentIndexed"][0];
            //item.AspEnableParentPaths = (bool)Server.Properties["AspEnableParentPaths"][0];
            item.AnonymousUserName = Server.Applications["/"].VirtualDirectories["/"].UserName;
            item.AnonymousUserPass = Server.Applications["/"].VirtualDirectories["/"].Password;
            #region 解析网站标识。
            for (int i = 0; i < Server.Bindings.Count; i++)
            {
                Microsoft.Web.Administration.Binding bind = Server.Bindings[i];
                if (bind.IsIPPortHostBinding)
                {
                    if (i == 0)
                    {
                        item.IP = bind.EndPoint.Address.ToString();
                        item.Port = bind.EndPoint.Port;
                        item.HostHeader = bind.Host;
                    }
                    else
                    {
                        item.Marks.Add(new IISServer.Mark(bind.EndPoint.Address.ToString(), bind.EndPoint.Port, bind.Host));
                    }
                }
            }
            #endregion

            item.Path = Server.Applications["/"].VirtualDirectories["/"].PhysicalPath;
            item.AppAspNetVersion = AspNetVersion.Default;
            Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
            switch (sm.ApplicationPools[Server.Applications["/"].ApplicationPoolName].ManagedRuntimeVersion)
            {
                case "v4.0":
                    item.AppAspNetVersion = AspNetVersion.ASP_NET_4_0;
                    break;
                case "v2.0":
                    item.AppAspNetVersion = AspNetVersion.ASP_NET_2_0;
                    break;
                case "v1.1":
                    item.AppAspNetVersion = AspNetVersion.ASP_NET_1_1;
                    break;
                default:
                    item.AppAspNetVersion = AspNetVersion.Default;
                    break;
            }
            //item.AppFriendlyName = (string)Root.Properties["AppFriendlyName"][0];

            #region 解析网站状态。
            switch (Server.State)
            {
                case Microsoft.Web.Administration.ObjectState.Started:
                    item._ServerState = SiteState.Started;
                    break;
                case Microsoft.Web.Administration.ObjectState.Starting:
                    item._ServerState = SiteState.Starting;
                    break;
                case Microsoft.Web.Administration.ObjectState.Stopped:
                    item._ServerState = SiteState.Stopped;
                    break;
                case Microsoft.Web.Administration.ObjectState.Stopping:
                    item._ServerState = SiteState.Stopping;
                    break;
                case Microsoft.Web.Administration.ObjectState.Unknown:
                    item._ServerState = SiteState.Continuing;
                    break;
            }
            #endregion

            #endregion

            return item;

        }
        /// <summary>
        /// 检查是否存在指定网站。
        /// </summary>
        /// <param name="Name">网站名称</param>
        /// <returns>存在返回 true；否则返回 false。</returns>
        public bool ExistsSite(string Name)
        {
            int MajorVersion = this.GetIISMajorVersion();
            int MinorVersion = this.GetIISMinorVersion();
            if (MajorVersion == 5 && MinorVersion == 1) //IIS 5.1
            {
                if (this.IIS51Service == null)
                {
                    return false;
                }
                return ConvertToSite(this.IIS51Service).Name.ToLowerInvariant().Trim() == Name.ToLowerInvariant().Trim();
            }
            else if (MajorVersion == 6) //IIS 6
            {
                System.DirectoryServices.DirectoryEntry de = returnSite(Name);
                return de != null;
            }
            else
            {
                Microsoft.Web.Administration.ServerManager iisManager = new Microsoft.Web.Administration.ServerManager();
                return iisManager.Sites[Name] != null;
            }
        }
        /// <summary>
        /// 获取具有指定名称的网站。
        /// </summary>
        /// <param name="Name">网站名称</param>
        /// <returns>找不到返回 null；否则返回找到的网站信息。</returns>
        public Site GetSite(string Name)
        {
            int MajorVersion = this.GetIISMajorVersion();
            int MinorVersion = this.GetIISMinorVersion();
            if (MajorVersion == 5 && MinorVersion == 1) //IIS 5.1
            {
                if (this.IIS51Service == null)
                {
                    return null;
                }
                Site ws = ConvertToSite(this.IIS51Service);
                if (ws.Name.ToLowerInvariant().Trim() == Name.ToLowerInvariant().Trim())
                {
                    return ws;
                }
                return null;
            }
            else if (MajorVersion == 6) //IIS 6
            {
                System.DirectoryServices.DirectoryEntry Server = returnSite(Name);
                if (Server == null)
                {
                    return null;
                }
                return ConvertToSite(Server);
            }
            else
            {
                Microsoft.Web.Administration.ServerManager iisManager = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.Site s = iisManager.Sites[Name];
                if (s == null)
                {
                    return null;
                }
                return ConvertToSite(s);
            }
        }

        /// <summary>
        /// 获取所有网站。
        /// </summary>
        /// <returns>网站集合。</returns>
        public IISServer.Site[] GetSite()
        {
            System.Collections.Generic.List<IISServer.Site> WebServers = new System.Collections.Generic.List<Site>();
            int MajorVersion = this.GetIISMajorVersion();
            int MinorVersion = this.GetIISMinorVersion();
            if (MajorVersion == 5 && MinorVersion == 1) //IIS 5.1
            {
                if (this.IIS51Service != null)
                {
                    WebServers.Add(ConvertToSite(this.IIS51Service));
                }
            }
            else if (MajorVersion == 6) //IIS 6
            {
                System.Collections.IEnumerator ie = this.Service.Children.GetEnumerator();
                while (ie.MoveNext())
                {
                    System.DirectoryServices.DirectoryEntry Server = (System.DirectoryServices.DirectoryEntry)ie.Current;
                    if (Server.SchemaClassName == "IIsWebServer")
                    {
                        Site item = ConvertToSite(Server);
                        WebServers.Add(item);
                    }
                }
            }
            else
            {
                Microsoft.Web.Administration.ServerManager iisManager = new Microsoft.Web.Administration.ServerManager();
                foreach (var tmp in iisManager.Sites)
                {
                    WebServers.Add(ConvertToSite(tmp));
                }
            }
            return WebServers.ToArray();

        }
        /// <summary>
        /// 获取所有网站的网站名称
        /// </summary>
        /// <returns>网站名称集合。</returns>
        public string[] GetSiteName()
        {
            System.Collections.Generic.List<string> list = new System.Collections.Generic.List<string>();

            int MajorVersion = this.GetIISMajorVersion();
            int MinorVersion = this.GetIISMinorVersion();
            if (MajorVersion == 5 && MinorVersion == 1) //IIS 5.1
            {
                if (this.IIS51Service != null)
                {
                    list.Add(ConvertToSite(this.IIS51Service).Name);
                }
            }
            else if (MajorVersion == 6) //IIS 6
            {
                System.Collections.IEnumerator ie = this.Service.Children.GetEnumerator();

                while (ie.MoveNext())
                {
                    System.DirectoryServices.DirectoryEntry Server = (System.DirectoryServices.DirectoryEntry)ie.Current;
                    if (Server.SchemaClassName == "IIsWebServer")
                    {
                        list.Add(Server.Properties["ServerComment"][0].ToString());
                    }
                }
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                foreach (var tmp in sm.Sites)
                {
                    list.Add(tmp.Name);
                }
            }

            return list.ToArray();
        }

        /// <summary>
        /// 创建网站
        /// </summary>
        /// <param name="iisServer">一个 IISWebServer 实例。</param>
        public void CreateSite(IISServer.Site iisServer)
        {
            if (iisServer.Name.ToString() == "")
            {
                throw new System.Exception("IISWebServer 的 Name 不能为空!");
            }

            int MajorVersion = this.GetIISMajorVersion();
            if (MajorVersion == 6) //IIS 6
            {
                int index = 0;
                #region 计算新建网站应该使用的 用基础目录服务命名的对象的名称。
                {
                    System.Collections.IEnumerator ie = this.Service.Children.GetEnumerator();
                    while (ie.MoveNext())
                    {
                        System.DirectoryServices.DirectoryEntry Server = (System.DirectoryServices.DirectoryEntry)ie.Current;
                        if (Server.SchemaClassName == "IIsWebServer")
                        {
                            if (System.Convert.ToInt32(Server.Name) > index)
                            {
                                index = System.Convert.ToInt32(Server.Name);
                            }
                            //                    if( Server.Properties["Serverbindings"][0].ToString() == ":" + iisServer.Port + ":" )    
                            //                    {
                            //                        Server.Invoke("stop",new object[0]);
                            //                    }
                        }
                    }

                    index++;
                }
                #endregion

                {
                    iisServer._Index = index;
                    System.DirectoryServices.DirectoryEntry Server = this.Service.Children.Add(iisServer._Index.ToString(), "IIsWebServer");
                    Server.Properties["ServerComment"][0] = iisServer.Name;
                    #region 重新设置网站标识。
                    Server.Properties["Serverbindings"].Clear();
                    for (int pi = 0; pi < iisServer.Marks.Count; pi++)
                    {
                        IISServer.Mark tmp = iisServer.Marks[pi];
                        Server.Properties["Serverbindings"].Add(tmp.IP + ":" + tmp.Port + ":" + tmp.HostHeader);
                    }
                    #endregion
                    Server.Properties["AccessScript"][0] = iisServer.AccessScript;
                    Server.Properties["AccessRead"][0] = iisServer.AccessRead;
                    Server.Properties["EnableDirBrowsing"][0] = iisServer.EnableDirBrowsing;
                    Server.Properties["DefaultDoc"][0] = string.Join(",", iisServer.DefaultDoc.ToArray());
                    Server.Properties["EnableDefaultDoc"][0] = iisServer.EnableDefaultDoc;
                    Server.Properties["AccessWrite"][0] = iisServer.AccessWrite;
                    Server.Properties["AccessExecute"][0] = iisServer.AccessExecute;
                    Server.Properties["AuthAnonymous"][0] = iisServer.AuthAnonymous;
                    Server.Properties["AuthBasic"][0] = iisServer.AuthBasic;
                    Server.Properties["AuthNTLM"][0] = iisServer.AuthNTLM;
                    Server.Properties["DontLog"][0] = iisServer.DontLog;
                    Server.Properties["ContentIndexed"][0] = iisServer.ContentIndexed;
                    Server.Properties["AspEnableParentPaths"][0] = iisServer.AspEnableParentPaths;
                    #region 设置匿名访问用户。
                    if (!string.IsNullOrEmpty(iisServer.AnonymousUserName))
                    {
                        Server.Properties["AnonymousUserName"][0] = iisServer.AnonymousUserName;
                        Server.Properties["AnonymousUserPass"][0] = iisServer.AnonymousUserPass;
                    }
                    else
                    {
                        Server.Properties["AnonymousUserName"][0] = DefaultAnonymousUserName;
                        if (string.IsNullOrEmpty(iisServer.AnonymousUserPass))
                        {
                            Server.Properties["AnonymousUserPass"][0] = DefaultAnonymousUserPass;
                        }
                        else
                        {
                            Server.Properties["AnonymousUserPass"][0] = iisServer.AnonymousUserPass;
                        }
                    }
                    #endregion

                    System.DirectoryServices.DirectoryEntry root = Server.Children.Add("Root", "IIsWebVirtualDir");
                    root.Properties["path"][0] = iisServer.Path;
                    switch (iisServer.AppAspNetVersion)
                    {
                        case AspNetVersion.Default:
                            break;
                        case AspNetVersion.ASP_NET_1_1:
                            root.Properties["ScriptMaps"].Value = IISManagement.ScriptMaps_1_1;
                            break;
                        case AspNetVersion.ASP_NET_2_0:
                            root.Properties["ScriptMaps"].Value = IISManagement.ScriptMaps_2_0;
                            break;
                        case AspNetVersion.ASP_NET_4_0:
                            root.Properties["ScriptMaps"].Value = IISManagement.ScriptMaps_4_0;
                            break;
                    }
                    root.Properties["AppFriendlyName"][0] = iisServer.AppFriendlyName;
                    root.Properties["AppIsolated"][0] = 2;//应用程序保护。这是一个二进制值，它指定当前创建的应用程序是在进程内运行 (0)、进程外运行 (1)，还是在进程池中运行 (2)。如果应用程序已存在并且正在运行，则更改此标志的值将会导致删除应用程序定义，并创建运行于指定进程空间的新应用程序。
                    root.Invoke("AppCreate", true);//root.Invoke("AppCreate2", new object[1] { 2 });

                    this.Service.CommitChanges();
                    Server.CommitChanges();
                    root.CommitChanges();
                    //Server.Invoke("start",new object[0]);
                }
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.Site s = sm.Sites.Add(iisServer.Name, iisServer.Path, iisServer.Port);
                if (!string.IsNullOrEmpty(iisServer.AnonymousUserName))
                {
                    s.Applications["/"].VirtualDirectories["/"].UserName = iisServer.AnonymousUserName;
                    s.Applications["/"].VirtualDirectories["/"].Password = iisServer.AnonymousUserPass;
                    //s.Applications["/"].VirtualDirectoryDefaults.UserName = iisServer.AnonymousUserName;
                    //s.Applications["/"].VirtualDirectoryDefaults.Password = iisServer.AnonymousUserPass;
                }
                s.Bindings.Clear();
                foreach (var tmp in iisServer.Marks)
                {
                    //Microsoft.Web.Administration.Binding b = s.Bindings.CreateElement();
                    //b.EndPoint.Address = System.Net.IPAddress.Parse();
                    //b.EndPoint.Port = tmp.Port;
                    //b.Host = tmp.HostHeader;
                    s.Bindings.Add(tmp.IP + ":" + tmp.Port + ":" + tmp.HostHeader, "http");
                }
                sm.CommitChanges();
            }
        }

        /// <summary>
        /// 修改与给定的网站具有相同名称(用基础目录服务命名的对象的名称)的网站配置。
        /// </summary>
        /// <param name="iisServer">给定的网站。</param>
        public void ModifySite(IISServer.Site iisServer)
        {
            if (iisServer._Index == -1)
            {
                throw new System.Exception("找不到给定的网站!");
            }
            System.DirectoryServices.DirectoryEntry Server = null;
            int MajorVersion = this.GetIISMajorVersion();
            if (MajorVersion < 7)
            {
                Server = returnSite(iisServer._Index);
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.Site s = sm.Sites[iisServer.Name];
                if (s == null)
                {
                    throw new System.Exception("找不到给定的网站!");
                }
                s.Name = iisServer.Name;
                s.Applications["/"].VirtualDirectories["/"].PhysicalPath = iisServer.Path;
                //s.Bindings[0].EndPoint.Port = iisServer.Port;
                s.Bindings.Clear();
                foreach (var tmp in iisServer.Marks)
                {
                    //Microsoft.Web.Administration.Binding b = s.Bindings.CreateElement();
                    //b.EndPoint.Address = System.Net.IPAddress.Parse();
                    //b.EndPoint.Port = tmp.Port;
                    //b.Host = tmp.HostHeader;
                    s.Bindings.Add(tmp.IP + ":" + tmp.Port + ":" + tmp.HostHeader, "http");
                }

                #region 设置匿名访问用户。
                s.Applications["/"].VirtualDirectories["/"].UserName = iisServer.AnonymousUserName;
                s.Applications["/"].VirtualDirectories["/"].Password = iisServer.AnonymousUserPass;
                //s.Applications["/"].VirtualDirectoryDefaults.UserName = iisServer.AnonymousUserName;
                //s.Applications["/"].VirtualDirectoryDefaults.Password = iisServer.AnonymousUserPass;
                #endregion

                sm.CommitChanges();
            }

            if (MajorVersion < 7)
            {
                if (Server == null)
                {
                    throw new System.Exception("找不到给定的网站!");
                }
                //Server.Invoke("stop", new object[0]);
                Server.Properties["ServerComment"][0] = iisServer.Name;
                #region 重新设置网站标识。
                Server.Properties["Serverbindings"].Clear();
                for (int pi = 0; pi < iisServer.Marks.Count; pi++)
                {
                    IISServer.Mark tmp = iisServer.Marks[pi];
                    Server.Properties["Serverbindings"].Add(tmp.IP + ":" + tmp.Port + ":" + tmp.HostHeader);
                }
                #endregion
                Server.Properties["AccessScript"][0] = iisServer.AccessScript;
                Server.Properties["AccessRead"][0] = iisServer.AccessRead;
                Server.Properties["EnableDirBrowsing"][0] = iisServer.EnableDirBrowsing;
                Server.Properties["DefaultDoc"][0] = string.Join(",", iisServer.DefaultDoc.ToArray());
                Server.Properties["EnableDefaultDoc"][0] = iisServer.EnableDefaultDoc;
                Server.Properties["AccessWrite"][0] = iisServer.AccessWrite;
                Server.Properties["AccessExecute"][0] = iisServer.AccessExecute;
                Server.Properties["AuthAnonymous"][0] = iisServer.AuthAnonymous;
                Server.Properties["AuthBasic"][0] = iisServer.AuthBasic;
                Server.Properties["AuthNTLM"][0] = iisServer.AuthNTLM;
                Server.Properties["DontLog"][0] = iisServer.DontLog;
                Server.Properties["ContentIndexed"][0] = iisServer.ContentIndexed;
                Server.Properties["AspEnableParentPaths"][0] = iisServer.AspEnableParentPaths;
                #region 设置匿名访问用户。
                if (!string.IsNullOrEmpty(iisServer.AnonymousUserName))
                {
                    Server.Properties["AnonymousUserName"][0] = iisServer.AnonymousUserName;
                    Server.Properties["AnonymousUserPass"][0] = iisServer.AnonymousUserPass;
                }
                else
                {
                    if (MajorVersion == 6) //IIS 6
                    {
                        Server.Properties["AnonymousUserName"][0] = DefaultAnonymousUserName;
                    }
                    if (string.IsNullOrEmpty(iisServer.AnonymousUserPass))
                    {
                        if (MajorVersion == 6) //IIS 6
                        {
                            Server.Properties["AnonymousUserPass"][0] = DefaultAnonymousUserPass;
                        }
                    }
                    else
                    {
                        Server.Properties["AnonymousUserPass"][0] = iisServer.AnonymousUserPass;
                    }
                }
                #endregion

                System.DirectoryServices.DirectoryEntry root = getRoot(Server);

                Server.CommitChanges();
                if (root != null)
                {
                    root.Properties["path"][0] = iisServer.Path;
                    switch (iisServer.AppAspNetVersion)
                    {
                        case AspNetVersion.Default:
                            break;
                        case AspNetVersion.ASP_NET_1_1:
                            root.Properties["ScriptMaps"].Value = IISManagement.ScriptMaps_1_1;
                            break;
                        case AspNetVersion.ASP_NET_2_0:
                            root.Properties["ScriptMaps"].Value = IISManagement.ScriptMaps_2_0;
                            break;
                        case AspNetVersion.ASP_NET_4_0:
                            root.Properties["ScriptMaps"].Value = IISManagement.ScriptMaps_4_0;
                            break;
                    }
                    root.Properties["AppFriendlyName"][0] = iisServer.AppFriendlyName;
                    //root.Properties["AppIsolated"][0] = 2;//应用程序保护。这是一个二进制值，它指定当前创建的应用程序是在进程内运行 (0)、进程外运行 (1)，还是在进程池中运行 (2)。如果应用程序已存在并且正在运行，则更改此标志的值将会导致删除应用程序定义，并创建运行于指定进程空间的新应用程序。
                    root.Invoke("AppCreate", true);//root.Invoke("AppCreate2", new object[1] { 2 });
                    root.CommitChanges();
                }

                //Server.Invoke("start", new object[0]);
            }

        }

        /// <summary>
        /// 删除网站。
        /// </summary>
        /// <param name="Name">网站名称</param>
        public void RemoveSite(string Name)
        {
            int MajorVersion = this.GetIISMajorVersion();
            if (MajorVersion == 6) //IIS 6
            {
                System.DirectoryServices.DirectoryEntry de = returnSite(Name);
                if (de == null)
                {
                    return;
                }
                de.DeleteTree();
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.Site s = sm.Sites[Name];
                if (s == null)
                {
                    throw new System.Exception("找不到给定的网站!");
                }
                sm.Sites.Remove(s);
                sm.CommitChanges();
            }
        }

        /// <summary>
        /// 启动网站。
        /// </summary>
        /// <param name="Name">网站名称</param>
        public void StartSite(string Name)
        {
            if (this.GetIISMajorVersion() < 7)
            {
                System.DirectoryServices.DirectoryEntry Server = returnSite(Name);
                if (Server == null)
                {
                    throw new System.Exception("在 IIS 找不到此网站!");
                }

                Server.Invoke("start", new object[0]);
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.Site s = sm.Sites[Name];
                if (s == null)
                {
                    throw new System.Exception("找不到给定的网站!");
                }
                s.Start();
                sm.CommitChanges();
            }

        }

        /// <summary>
        /// 停止网站。
        /// </summary>
        /// <param name="Name">网站名称</param>
        public void StopSite(string Name)
        {
            if (this.GetIISMajorVersion() < 7)
            {
                System.DirectoryServices.DirectoryEntry Server = this.returnSite(Name);
                if (Server == null)
                {
                    throw new System.Exception("在 IIS 找不到此网站!");
                }

                Server.Invoke("stop", new object[0]);
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.Site s = sm.Sites[Name];
                if (s == null)
                {
                    throw new System.Exception("找不到给定的网站!");
                }
                s.Stop();
                sm.CommitChanges();
            }

        }

        /// <summary>
        /// 返回指定的应用程序。
        /// </summary>
        /// <param name="Server">用于表示网站的 DirectoryEntry 对象。</param>
        /// <param name="ApplicationName">应用程序名称。</param>
        /// <returns>找不到返回 null。</returns>
        private System.DirectoryServices.DirectoryEntry returnApplication(System.DirectoryServices.DirectoryEntry Server, string ApplicationName)
        {
            if (Server.SchemaClassName != "IIsWebServer")
            {
                throw new System.Exception("指定的 Server 不是网站。");
            }
            ApplicationName = ApplicationName.ToLowerInvariant().Trim();

            System.DirectoryServices.DirectoryEntry Server2 = null;
            System.Collections.IEnumerator ie = Server.Children.GetEnumerator();
            while (ie.MoveNext())
            {
                Server2 = (System.DirectoryServices.DirectoryEntry)ie.Current;
                if (Server2.SchemaClassName == "IIsWebVirtualDir")
                {
                    break;
                }
            }

            ie = Server2.Children.GetEnumerator();
            while (ie.MoveNext())
            {
                System.DirectoryServices.DirectoryEntry Server3 = (System.DirectoryServices.DirectoryEntry)ie.Current;
                if (Server3.SchemaClassName == "IIsWebVirtualDir" || Server3.SchemaClassName == "IIsWebDirectory")
                {
                    if (Server3.Name.ToLowerInvariant() == ApplicationName)
                    {
                        return Server3;
                    }
                }
            }
            return null;
        }
        /// <summary>
        /// 尝试将 DirectoryEntry 转换成 IISWebVirtualDir 对象。
        /// </summary>
        /// <param name="AppDir">一个 DirectoryEntry 实例。</param>
        /// <returns>成功转换的 IISWebVirtualDir 对象。</returns>
        private static Application ConvertToApplication(System.DirectoryServices.DirectoryEntry AppDir)
        {
            if (AppDir.SchemaClassName != "IIsWebVirtualDir" && AppDir.SchemaClassName != "IIsWebDirectory")
            {
                throw new System.Exception("指定的 DirectoryEntry 不能转换为 IISWebVirtualDir 对象。");
            }
            #region 获取应用程序或 IIS Web 目录
            Application item_virdir = new Application(AppDir.Name);
            item_virdir.AccessRead = (bool)AppDir.Properties["AccessRead"][0];
            item_virdir.AccessScript = (bool)AppDir.Properties["AccessScript"][0];
            item_virdir.DefaultDoc.AddRange(((string)AppDir.Properties["DefaultDoc"][0]).Split(','));
            item_virdir.EnableDefaultDoc = (bool)AppDir.Properties["EnableDefaultDoc"][0];
            item_virdir.AccessWrite = (bool)AppDir.Properties["AccessWrite"][0];
            item_virdir.AccessExecute = (bool)AppDir.Properties["AccessExecute"][0];
            item_virdir.AuthAnonymous = (bool)AppDir.Properties["AuthAnonymous"][0];
            item_virdir.AuthBasic = (bool)AppDir.Properties["AuthBasic"][0];
            item_virdir.AuthNTLM = (bool)AppDir.Properties["AuthNTLM"][0];
            item_virdir.DontLog = (bool)AppDir.Properties["DontLog"][0];
            item_virdir.ContentIndexed = (bool)AppDir.Properties["ContentIndexed"][0];
            item_virdir.AspEnableParentPaths = (bool)AppDir.Properties["AspEnableParentPaths"][0];
            item_virdir.AnonymousUserName = (string)AppDir.Properties["AnonymousUserName"][0];
            item_virdir.AnonymousUserPass = (string)AppDir.Properties["AnonymousUserPass"][0];
            item_virdir.AppFriendlyName = (string)AppDir.Properties["AppFriendlyName"][0];
            item_virdir.AppAspNetVersion = AspNetVersion.Default;
            foreach (string tmp in AppDir.Properties["ScriptMaps"])
            {
                if (tmp.ToLowerInvariant().Contains(@"microsoft.net\framework\v1.1.4322"))//Microsoft.NET\Framework\v1.1.4322
                {
                    item_virdir.AppAspNetVersion = AspNetVersion.ASP_NET_1_1;
                    break;
                }
                else if (tmp.ToLowerInvariant().Contains(@"microsoft.net\framework\v2.0.50727"))//microsoft.net\framework\v2.0.50727
                {
                    item_virdir.AppAspNetVersion = AspNetVersion.ASP_NET_2_0;
                    break;
                }
                else if (tmp.ToLowerInvariant().Contains(@"microsoft.net\framework\v4.0.30319"))//microsoft.net\framework\v4.0.30319
                {
                    item_virdir.AppAspNetVersion = AspNetVersion.ASP_NET_4_0;
                    break;
                }
            }
            if (AppDir.SchemaClassName == "IIsWebVirtualDir")
            {
                item_virdir.Path = (string)AppDir.Properties["Path"][0];
                //item_virdir.AppCreate = (bool)VirDir.Properties["AppCreate"][0];
            }
            else if (AppDir.SchemaClassName == "IIsWebDirectory")
            {
                System.DirectoryServices.DirectoryEntry Root = AppDir.Parent;
                item_virdir.Path = Root.Properties["Path"][0] + @"\" + AppDir.Name;
                //item_virdir.AppCreate = (bool)Root.Properties["AppCreate"][0];
            }
            return item_virdir;
            #endregion

        }
        /// <summary>
        /// 尝试将 Microsoft.Web.Administration.VirtualDirectory 转换成 IISWebVirtualDir 对象。
        /// </summary>
        /// <param name="AppDir">一个 DirectoryEntry 实例。</param>
        /// <returns>成功转换的 IISWebVirtualDir 对象。</returns>
        private static Application ConvertToApplication(Microsoft.Web.Administration.Application AppDir)
        {
            #region 获取应用程序或 IIS Web 目录
            Application item_virdir = new Application(AppDir.Path);
            //item_virdir.AccessRead = (bool)VirDir.Properties["AccessRead"][0];
            //item_virdir.AccessScript = (bool)VirDir.Properties["AccessScript"][0];
            //item_virdir.DefaultDoc.AddRange(((string)VirDir.Properties["DefaultDoc"][0]).Split(','));
            //item_virdir.EnableDefaultDoc = (bool)VirDir.Properties["EnableDefaultDoc"][0];
            //item_virdir.AccessWrite = (bool)VirDir.Properties["AccessWrite"][0];
            //item_virdir.AccessExecute = (bool)VirDir.Properties["AccessExecute"][0];
            //item_virdir.AuthAnonymous = (bool)VirDir.Properties["AuthAnonymous"][0];
            //item_virdir.AuthBasic = (bool)VirDir.Properties["AuthBasic"][0];
            //item_virdir.AuthNTLM = (bool)VirDir.Properties["AuthNTLM"][0];
            //item_virdir.DontLog = (bool)VirDir.Properties["DontLog"][0];
            //item_virdir.ContentIndexed = (bool)VirDir.Properties["ContentIndexed"][0];
            //item_virdir.AspEnableParentPaths = (bool)VirDir.Properties["AspEnableParentPaths"][0];
            item_virdir.AnonymousUserName = AppDir.VirtualDirectories["/"].UserName;
            item_virdir.AnonymousUserPass = AppDir.VirtualDirectories["/"].Password;
            //item_virdir.AppFriendlyName = (string)VirDir.Properties["AppFriendlyName"][0];
            //item_virdir.AppAspNetVersion = AspNetVersion.Default;
            //foreach (string tmp in VirDir.Properties["ScriptMaps"])
            //{
            //    if (tmp.ToLowerInvariant().Contains(@"microsoft.net\framework\v1.1.4322"))//Microsoft.NET\Framework\v1.1.4322
            //    {
            //        item_virdir.AppAspNetVersion = AspNetVersion.ASP_NET_1_1;
            //        break;
            //    }
            //    else if (tmp.ToLowerInvariant().Contains(@"microsoft.net\framework\v2.0.50727"))//microsoft.net\framework\v2.0.50727
            //    {
            //        item_virdir.AppAspNetVersion = AspNetVersion.ASP_NET_2_0;
            //        break;
            //    }
            //}
            item_virdir.Path = AppDir.VirtualDirectories["/"].PhysicalPath;
            //if (VirDir.SchemaClassName == "IIsWebVirtualDir")
            //{
            //    item_virdir.Path = (string)VirDir.Properties["Path"][0];
            //    //item_virdir.AppCreate = (bool)VirDir.Properties["AppCreate"][0];
            //}
            //else if (VirDir.SchemaClassName == "IIsWebDirectory")
            //{
            //    System.DirectoryServices.DirectoryEntry Root = VirDir.Parent;
            //    item_virdir.Path = Root.Properties["Path"][0] + @"\" + VirDir.Name;
            //    //item_virdir.AppCreate = (bool)Root.Properties["AppCreate"][0];
            //}
            return item_virdir;
            #endregion

        }
        /// <summary>
        /// 检查是否存在指定的应用程序。
        /// </summary>
        /// <param name="SiteName">网站名称</param>
        /// <param name="ApplicationName">应用程序名称。</param>
        /// <returns>存在返回 true；否则返回 false。</returns>
        public bool ExistsApplication(string SiteName, string ApplicationName)
        {
            if (this.GetIISMajorVersion() < 7)
            {
                System.DirectoryServices.DirectoryEntry Server = this.returnSite(SiteName);
                if (Server == null)
                {
                    return false;
                }
                System.DirectoryServices.DirectoryEntry vir = returnApplication(Server, ApplicationName);
                if (vir == null)
                {
                    return false;
                }
                return true;
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.Site s = sm.Sites[SiteName];
                if (s == null)
                {
                    return false;
                }
                if (s.Applications[ApplicationName.StartsWith("/") ? ApplicationName : "/" + ApplicationName] == null)
                {
                    return false;
                }
                return true;
            }
            //return false;

        }
        /// <summary>
        /// 获取指定的应用程序。
        /// </summary>
        /// <param name="SiteName">网站名称。</param>
        /// <param name="ApplicationName">应用程序名称。</param>
        /// <returns>找不到返回 null。</returns>
        public Application GetApplication(string SiteName, string ApplicationName)
        {
            if (this.GetIISMajorVersion() < 7)
            {
                System.DirectoryServices.DirectoryEntry Server = this.returnSite(SiteName);
                if (Server == null)
                {
                    return null;
                }
                System.DirectoryServices.DirectoryEntry vir = returnApplication(Server, ApplicationName);
                if (vir == null)
                {
                    return null;
                }
                return ConvertToApplication(vir);
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.Site s = sm.Sites[SiteName];
                if (s == null)
                {
                    return null;
                }
                Microsoft.Web.Administration.Application vir = s.Applications[ApplicationName.StartsWith("/") ? ApplicationName : "/" + ApplicationName];
                if (vir == null)
                {
                    return null;
                }
                return ConvertToApplication(vir);
            }

        }

        /// <summary>
        /// 获取指定的网站下的所有应用程序。
        /// </summary>
        /// <param name="SiteName">网站名称。</param>
        /// <returns>找不到返回 null。</returns>
        public Application[] GetApplication(string SiteName)
        {
            System.Collections.Generic.List<Application> result = new System.Collections.Generic.List<Application>();
            if (this.GetIISMajorVersion() < 7)
            {
                System.DirectoryServices.DirectoryEntry Server = this.returnSite(SiteName);
                if (Server == null)
                {
                    return result.ToArray();
                }
                System.DirectoryServices.DirectoryEntry Root = null;
                System.Collections.IEnumerator ieRoot = Server.Children.GetEnumerator();
                while (ieRoot.MoveNext())
                {
                    Root = (System.DirectoryServices.DirectoryEntry)ieRoot.Current;
                    if (Root.SchemaClassName == "IIsWebVirtualDir")
                    {
                        break;
                    }
                }

                #region 获取应用程序或 IIS Web 目录
                if (Root != null)
                {
                    ieRoot = Root.Children.GetEnumerator();
                    while (ieRoot.MoveNext())
                    {
                        System.DirectoryServices.DirectoryEntry VirDir = (System.DirectoryServices.DirectoryEntry)ieRoot.Current;
                        if (VirDir.SchemaClassName != "IIsWebVirtualDir" && VirDir.SchemaClassName != "IIsWebDirectory")
                        {
                            continue;
                        }
                        Application item_virdir = new Application(VirDir.Name);
                        item_virdir.AccessRead = (bool)VirDir.Properties["AccessRead"][0];
                        item_virdir.AccessScript = (bool)VirDir.Properties["AccessScript"][0];
                        item_virdir.DefaultDoc.AddRange(((string)VirDir.Properties["DefaultDoc"][0]).Split(','));
                        item_virdir.EnableDefaultDoc = (bool)VirDir.Properties["EnableDefaultDoc"][0];
                        item_virdir.AccessWrite = (bool)VirDir.Properties["AccessWrite"][0];
                        item_virdir.AccessExecute = (bool)VirDir.Properties["AccessExecute"][0];
                        item_virdir.AuthAnonymous = (bool)VirDir.Properties["AuthAnonymous"][0];
                        item_virdir.AuthBasic = (bool)VirDir.Properties["AuthBasic"][0];
                        item_virdir.AuthNTLM = (bool)VirDir.Properties["AuthNTLM"][0];
                        item_virdir.DontLog = (bool)VirDir.Properties["DontLog"][0];
                        item_virdir.ContentIndexed = (bool)VirDir.Properties["ContentIndexed"][0];
                        item_virdir.AspEnableParentPaths = (bool)VirDir.Properties["AspEnableParentPaths"][0];
                        item_virdir.AnonymousUserName = (string)VirDir.Properties["AnonymousUserName"][0];
                        item_virdir.AnonymousUserPass = (string)VirDir.Properties["AnonymousUserPass"][0];
                        item_virdir.AppFriendlyName = (string)VirDir.Properties["AppFriendlyName"][0];
                        item_virdir.AppAspNetVersion = AspNetVersion.Default;
                        foreach (string tmp in VirDir.Properties["ScriptMaps"])
                        {
                            if (tmp.ToLowerInvariant().Contains(@"microsoft.net\framework\v1.1.4322"))//Microsoft.NET\Framework\v1.1.4322
                            {
                                item_virdir.AppAspNetVersion = AspNetVersion.ASP_NET_1_1;
                                break;
                            }
                            else if (tmp.ToLowerInvariant().Contains(@"microsoft.net\framework\v2.0.50727"))//microsoft.net\framework\v2.0.50727
                            {
                                item_virdir.AppAspNetVersion = AspNetVersion.ASP_NET_2_0;
                                break;
                            }
                            else if (tmp.ToLowerInvariant().Contains(@"microsoft.net\framework\v4.0.30319"))//microsoft.net\framework\v4.0.30319
                            {
                                item_virdir.AppAspNetVersion = AspNetVersion.ASP_NET_4_0;
                                break;
                            }
                        }
                        if (VirDir.SchemaClassName == "IIsWebVirtualDir")
                        {
                            item_virdir.Path = (string)VirDir.Properties["Path"][0];
                            //item_virdir.AppCreate = (bool)VirDir.Properties["AppCreate"][0];
                        }
                        else if (VirDir.SchemaClassName == "IIsWebDirectory")
                        {
                            item_virdir.Path = Root.Properties["Path"][0] + @"\" + VirDir.Name;
                            //item_virdir.AppCreate = (bool)Root.Properties["AppCreate"][0];
                        }
                        result.Add(item_virdir);
                    }
                }
                #endregion
                //return this.ConvertToIISWebVirtualDir(vir);
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.Site s = sm.Sites[SiteName];
                if (s == null)
                {
                    return result.ToArray();
                }
                foreach (var tmp in s.Applications)
                {
                    result.Add(ConvertToApplication(tmp));
                }
            }
            return result.ToArray();

        }

        /// <summary>
        /// 创建应用程序
        /// </summary>
        /// <param name="SiteName">网站名称。</param>
        /// <param name="iisApp">应用程序。</param>
        /// <param name="deleteIfExist">指示如果目标存在是否删除。</param>
        public void CreateApplication(string SiteName, IISServer.Application iisApp, bool deleteIfExist)
        {
            if (this.GetIISMajorVersion() < 7)
            {
                //System.DirectoryServices.DirectoryEntry Service = this.GetService();
                System.DirectoryServices.DirectoryEntry Server = returnSite(SiteName);

                if (Server == null)
                {
                    throw new System.Exception("找不到给定的网站!");
                }

                Server = getRoot(Server);
                if (deleteIfExist)
                {
                    foreach (System.DirectoryServices.DirectoryEntry VirDir in Server.Children)
                    {
                        if (VirDir.Name.ToLowerInvariant() == iisApp.Name.ToLowerInvariant())
                        {
                            Server.Children.Remove(VirDir);
                            Server.CommitChanges();
                            break;
                        }
                    }
                }

                System.DirectoryServices.DirectoryEntry vir = Server.Children.Add(iisApp.Name, "IIsWebVirtualDir");
                vir.Properties["Path"][0] = iisApp.Path;
                vir.Properties["DefaultDoc"][0] = string.Join(",", iisApp.DefaultDoc.ToArray());
                vir.Properties["EnableDefaultDoc"][0] = iisApp.EnableDefaultDoc;
                vir.Properties["AccessScript"][0] = iisApp.AccessScript;
                vir.Properties["AccessRead"][0] = iisApp.AccessRead;
                vir.Properties["AccessWrite"][0] = iisApp.AccessWrite;
                vir.Properties["AccessExecute"][0] = iisApp.AccessExecute;
                vir.Properties["AuthAnonymous"][0] = iisApp.AuthAnonymous;
                vir.Properties["AuthBasic"][0] = iisApp.AuthBasic;
                vir.Properties["AuthNTLM"][0] = iisApp.AuthNTLM;
                vir.Properties["DontLog"][0] = iisApp.DontLog;
                vir.Properties["ContentIndexed"][0] = iisApp.ContentIndexed;
                vir.Properties["AspEnableParentPaths"][0] = iisApp.AspEnableParentPaths;
                #region 设置匿名访问用户。
                if (!string.IsNullOrEmpty(iisApp.AnonymousUserName))
                {
                    vir.Properties["AnonymousUserName"][0] = iisApp.AnonymousUserName;
                    vir.Properties["AnonymousUserPass"][0] = iisApp.AnonymousUserPass;
                }
                else
                {
                    vir.Properties["AnonymousUserName"][0] = Server.Properties["AnonymousUserName"][0];
                    if (string.IsNullOrEmpty(iisApp.AnonymousUserPass))
                    {
                        vir.Properties["AnonymousUserPass"][0] = Server.Properties["AnonymousUserPass"][0];
                    }
                    else
                    {
                        vir.Properties["AnonymousUserPass"][0] = iisApp.AnonymousUserPass;
                    }
                }
                #endregion

                vir.Properties["AppFriendlyName"][0] = iisApp.AppFriendlyName;
                //vir.Properties["AppIsolated"][0] = 2;//应用程序保护。这是一个二进制值，它指定当前创建的应用程序是在进程内运行 (0)、进程外运行 (1)，还是在进程池中运行 (2)。如果应用程序已存在并且正在运行，则更改此标志的值将会导致删除应用程序定义，并创建运行于指定进程空间的新应用程序。
                switch (iisApp.AppAspNetVersion)
                {
                    case AspNetVersion.Default:
                        break;
                    case AspNetVersion.ASP_NET_1_1:
                        vir.Properties["ScriptMaps"].Value = IISManagement.ScriptMaps_1_1;
                        break;
                    case AspNetVersion.ASP_NET_2_0:
                        vir.Properties["ScriptMaps"].Value = IISManagement.ScriptMaps_2_0;
                        break;
                    case AspNetVersion.ASP_NET_4_0:
                        vir.Properties["ScriptMaps"].Value = IISManagement.ScriptMaps_4_0;
                        break;
                }
                vir.Invoke("AppCreate", true);//vir.Invoke("AppCreate2", new object[1] { 2 });

                Server.CommitChanges();
                vir.CommitChanges();
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.Site s = sm.Sites[SiteName];
                if (s == null)
                {
                    throw new System.Exception("找不到给定的网站!");
                }
                if (deleteIfExist)
                {
                    var m = s.Applications[iisApp.Name.StartsWith("/") ? iisApp.Name : "/" + iisApp.Name];
                    if (m != null)
                    {
                        s.Applications.Remove(m);
                    }
                }
                s.Applications.Add(iisApp.Name.StartsWith("/") ? iisApp.Name : "/" + iisApp.Name, iisApp.Path);
                sm.CommitChanges();
            }

        }

        /// <summary>
        /// 修改应用程序
        /// </summary>
        /// <param name="SiteName">网站名称。</param>
        /// <param name="iisApp">应用程序。</param>
        public void ModifyApplication(string SiteName, IISServer.Application iisApp)
        {
            if (this.GetIISMajorVersion() < 7)
            {
                System.DirectoryServices.DirectoryEntry Server = returnSite(SiteName);
                if (Server == null)
                {
                    throw new System.Exception("没有找到指定的网站!");
                }

                System.DirectoryServices.DirectoryEntry vir = returnApplication(Server, iisApp.Name);
                if (vir.SchemaClassName == "IIsWebVirtualDir")//如果是应用程序则允许更改路径。
                {
                    vir.Properties["Path"][0] = iisApp.Path;
                }
                vir.Properties["DefaultDoc"][0] = string.Join(",", iisApp.DefaultDoc.ToArray());
                vir.Properties["EnableDefaultDoc"][0] = iisApp.EnableDefaultDoc;
                vir.Properties["AccessScript"][0] = iisApp.AccessScript;
                vir.Properties["AccessRead"][0] = iisApp.AccessRead;
                vir.Properties["AccessWrite"][0] = iisApp.AccessWrite;
                vir.Properties["AccessExecute"][0] = iisApp.AccessExecute;
                vir.Properties["AuthAnonymous"][0] = iisApp.AuthAnonymous;
                vir.Properties["AuthBasic"][0] = iisApp.AuthBasic;
                vir.Properties["AuthNTLM"][0] = iisApp.AuthNTLM;
                vir.Properties["DontLog"][0] = iisApp.DontLog;
                vir.Properties["ContentIndexed"][0] = iisApp.ContentIndexed;
                vir.Properties["AspEnableParentPaths"][0] = iisApp.AspEnableParentPaths;
                #region 设置匿名访问用户。
                if (!string.IsNullOrEmpty(iisApp.AnonymousUserName))
                {
                    vir.Properties["AnonymousUserName"][0] = iisApp.AnonymousUserName;
                    vir.Properties["AnonymousUserPass"][0] = iisApp.AnonymousUserPass;
                }
                else
                {
                    vir.Properties["AnonymousUserName"][0] = Server.Properties["AnonymousUserName"][0]; //默认自动从网站继承匿名访问用户名
                    if (string.IsNullOrEmpty(iisApp.AnonymousUserPass))
                    {
                        vir.Properties["AnonymousUserPass"][0] = Server.Properties["AnonymousUserPass"][0];//默认自动从网站继承匿名访问用户密码
                    }
                    else
                    {
                        vir.Properties["AnonymousUserPass"][0] = iisApp.AnonymousUserPass;
                    }
                }
                #endregion

                vir.Properties["AppFriendlyName"][0] = iisApp.AppFriendlyName;
                //vir.Properties["AppIsolated"][0] = 2;//应用程序保护。这是一个二进制值，它指定当前创建的应用程序是在进程内运行 (0)、进程外运行 (1)，还是在进程池中运行 (2)。如果应用程序已存在并且正在运行，则更改此标志的值将会导致删除应用程序定义，并创建运行于指定进程空间的新应用程序。
                switch (iisApp.AppAspNetVersion)
                {
                    case AspNetVersion.Default:
                        break;
                    case AspNetVersion.ASP_NET_1_1:
                        vir.Properties["ScriptMaps"].Value = IISManagement.ScriptMaps_1_1;
                        break;
                    case AspNetVersion.ASP_NET_2_0:
                        vir.Properties["ScriptMaps"].Value = IISManagement.ScriptMaps_2_0;
                        break;
                    case AspNetVersion.ASP_NET_4_0:
                        vir.Properties["ScriptMaps"].Value = IISManagement.ScriptMaps_4_0;
                        break;
                }
                vir.Invoke("AppCreate", true);//vir.Invoke("AppCreate2", new object[1] { 2 });

                Server.CommitChanges();
                vir.CommitChanges();
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.Site s = sm.Sites[SiteName];
                if (s == null)
                {
                    throw new System.Exception("找不到给定的网站!");
                }
                Microsoft.Web.Administration.Application m = s.Applications[iisApp.Name.StartsWith("/") ? iisApp.Name : "/" + iisApp.Name];
                if (m == null)
                {
                    throw new System.Exception("找不到给定的应用程序!");
                }
                m.Path = iisApp.Name;
                m.VirtualDirectories["/"].PhysicalPath = iisApp.Path;
                sm.CommitChanges();
            }

        }

        /// <summary>
        /// 删除应用程序
        /// </summary>
        /// <param name="SiteName">网站名称</param>
        /// <param name="ApplicationName">应用程序名称</param>
        public void RemoveApplication(string SiteName, string ApplicationName)
        {
            if (this.GetIISMajorVersion() < 7)
            {
                ApplicationName = ApplicationName.ToLowerInvariant();
                //System.DirectoryServices.DirectoryEntry Service = this.GetService();
                System.DirectoryServices.DirectoryEntry Server = returnSite(SiteName);

                if (Server == null)
                {
                    throw new System.Exception("找不到给定的网站!");
                }

                Server = getRoot(Server);
                foreach (System.DirectoryServices.DirectoryEntry VirDir in Server.Children)
                {
                    if (VirDir.Name.ToLowerInvariant().Trim() == ApplicationName)
                    {
                        VirDir.DeleteTree();
                        //Server.Children.Remove(VirDir);
                        //Server.CommitChanges();
                        return;
                    }
                }

                //throw (new System.Exception("找不到给定的应用程序!"));
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.Site s = sm.Sites[SiteName];
                if (s == null)
                {
                    throw new System.Exception("找不到给定的网站!");
                }
                var m = s.Applications[ApplicationName.StartsWith("/") ? ApplicationName : "/" + ApplicationName];
                if (m != null)
                {
                    s.Applications.Remove(m);
                }
                sm.CommitChanges();
            }

        }

        /// <summary>
        /// 判断是否存在指定的应用程序池。
        /// </summary>
        /// <param name="Name">应用程序池名称。</param>
        /// <returns></returns>
        public bool ExistsApplicationPool(string Name)
        {
            int MajorVersion = this.GetIISMajorVersion();
            if (MajorVersion == 6) //IIS 6
            {
                Name = Name.ToLowerInvariant();
                System.DirectoryServices.DirectoryEntry apppools = this.ApplicationPoolService;
                foreach (System.DirectoryServices.DirectoryEntry a in apppools.Children)
                {
                    if (a.Name.ToLowerInvariant() == Name)
                    {
                        return true;
                    }
                }
                return false;
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                return sm.ApplicationPools[Name] != null;
            }
        }

        /// <summary>
        /// 创建应用程序池。
        /// </summary>
        /// <param name="Name">应用程序池名称。</param>
        /// <param name="UserName">用户名。</param>
        /// <param name="Password">密码。</param>
        public void CreateApplicationPool(string Name, string UserName, string Password)
        {
            int MajorVersion = this.GetIISMajorVersion();
            if (MajorVersion == 6) //IIS 6
            {
                System.DirectoryServices.DirectoryEntry newpool;
                System.DirectoryServices.DirectoryEntry apppools = this.ApplicationPoolService;
                newpool = apppools.Children.Add(Name, "IIsApplicationPool");
                newpool.Properties["WAMUserName"][0] = UserName;
                newpool.Properties["WAMUserPass"][0] = Password;
                newpool.Properties["AppPoolIdentityType"][0] = "3";
                newpool.CommitChanges();
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.ApplicationPool apool = sm.ApplicationPools.Add(Name);
                //apool.ProcessModel.IdentityType = Microsoft.Web.Administration.ProcessModelIdentityType.SpecificUser;
                apool.ProcessModel.UserName = UserName;
                apool.ProcessModel.Password = Password;
                sm.CommitChanges();
            }

        }

        /// <summary>
        /// 创建应用程序池。
        /// </summary>
        /// <param name="Name">应用程序池名称。</param>
        /// <param name="UserName">用户名。</param>
        /// <param name="Password">密码。</param>
        /// <param name="ManagedRuntimeVersion">指派应用程序池的 .NET Framework 版本。（IIS 7 以上版本有效）</param>
        /// <param name="ManagedPipelineMode">设置托管管道模式。（IIS 7 以上版本有效）</param>
        public void CreateApplicationPool(string Name, string UserName, string Password, AspNetVersion ManagedRuntimeVersion, Microsoft.Web.Administration.ManagedPipelineMode ManagedPipelineMode)
        {
            int MajorVersion = this.GetIISMajorVersion();
            if (MajorVersion == 6) //IIS 6
            {
                System.DirectoryServices.DirectoryEntry newpool;
                System.DirectoryServices.DirectoryEntry apppools = this.ApplicationPoolService;
                newpool = apppools.Children.Add(Name, "IIsApplicationPool");
                newpool.Properties["WAMUserName"][0] = UserName;
                newpool.Properties["WAMUserPass"][0] = Password;
                newpool.Properties["AppPoolIdentityType"][0] = "3";
                newpool.CommitChanges();
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.ApplicationPool apool = sm.ApplicationPools.Add(Name);
                //apool.ProcessModel.IdentityType = Microsoft.Web.Administration.ProcessModelIdentityType.SpecificUser;
                apool.ProcessModel.UserName = UserName;
                apool.ProcessModel.Password = Password;
                switch (ManagedRuntimeVersion)
                {
                    case AspNetVersion.Default:
                        break;
                    case AspNetVersion.ASP_NET_1_1:
                        break;
                    case AspNetVersion.ASP_NET_2_0:
                        apool.ManagedRuntimeVersion = "v2.0";
                        break;
                    case AspNetVersion.ASP_NET_4_0:
                        apool.ManagedRuntimeVersion = "v4.0";
                        break;
                }
                apool.ManagedPipelineMode = ManagedPipelineMode;
                sm.CommitChanges();
            }

        }

        /// <summary>
        /// 创建应用程序池。
        /// </summary>
        /// <param name="Name">应用程序池名称。</param>
        public void CreateApplicationPool(string Name)
        {
            int MajorVersion = this.GetIISMajorVersion();
            if (MajorVersion == 6) //IIS 6
            {
                System.DirectoryServices.DirectoryEntry newpool;
                System.DirectoryServices.DirectoryEntry apppools = this.ApplicationPoolService;
                newpool = apppools.Children.Add(Name, "IIsApplicationPool");
                newpool.CommitChanges();
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.ApplicationPool apool = sm.ApplicationPools.Add(Name);
                //apool.ProcessModel.IdentityType = Microsoft.Web.Administration.ProcessModelIdentityType.NetworkService;
                sm.CommitChanges();
            }

        }

        /// <summary>
        /// 创建应用程序池。
        /// </summary>
        /// <param name="Name">应用程序池名称。</param>
        /// <param name="ManagedRuntimeVersion">指派应用程序池的 .NET Framework 版本。（IIS 7 以上版本有效）</param>
        /// <param name="ManagedPipelineMode">设置托管管道模式。（IIS 7 以上版本有效）</param>
        public void CreateApplicationPool(string Name, AspNetVersion ManagedRuntimeVersion, Microsoft.Web.Administration.ManagedPipelineMode ManagedPipelineMode)
        {
            int MajorVersion = this.GetIISMajorVersion();
            if (MajorVersion == 6) //IIS 6
            {
                System.DirectoryServices.DirectoryEntry newpool;
                System.DirectoryServices.DirectoryEntry apppools = this.ApplicationPoolService;
                newpool = apppools.Children.Add(Name, "IIsApplicationPool");
                newpool.CommitChanges();
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.ApplicationPool apool = sm.ApplicationPools.Add(Name);
                //apool.ProcessModel.IdentityType = Microsoft.Web.Administration.ProcessModelIdentityType.NetworkService;
                switch (ManagedRuntimeVersion)
                {
                    case AspNetVersion.Default:
                        break;
                    case AspNetVersion.ASP_NET_1_1:
                        break;
                    case AspNetVersion.ASP_NET_2_0:
                        apool.ManagedRuntimeVersion = "v2.0";
                        break;
                    case AspNetVersion.ASP_NET_4_0:
                        apool.ManagedRuntimeVersion = "v4.0";
                        break;
                }
                apool.ManagedPipelineMode = ManagedPipelineMode;
                sm.CommitChanges();
            }

        }

        /// <summary>
        /// 删除应用程序池。
        /// </summary>
        /// <param name="Name">应用程序池名称。</param>
        public void DeleteApplicationPool(string Name)
        {
            int MajorVersion = this.GetIISMajorVersion();
            if (MajorVersion == 6) //IIS 6
            {
                Name = Name.ToLowerInvariant();
                System.DirectoryServices.DirectoryEntry apppools = this.ApplicationPoolService;
                System.DirectoryServices.DirectoryEntry has = null;
                foreach (System.DirectoryServices.DirectoryEntry a in apppools.Children)
                {
                    if (a.Name.ToLowerInvariant() == Name)
                    {
                        has = a;
                        break;
                    }
                }
                if (has != null)
                {
                    has.DeleteTree();
                    apppools.CommitChanges();
                }
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                Microsoft.Web.Administration.ApplicationPool ap = sm.ApplicationPools[Name];
                if (ap != null)
                {
                    sm.ApplicationPools.Remove(ap);
                    sm.CommitChanges();
                }
            }

        }

        /// <summary>
        /// 设置指定的网站应用指定的应用程序池。
        /// </summary>
        /// <param name="SiteName">网站名称</param>
        /// <param name="AppPoolName">应用程序池名称。</param>
        public void AssignApplicationPool(string SiteName, string AppPoolName)
        {
            int MajorVersion = this.GetIISMajorVersion();
            if (MajorVersion == 6) //IIS 6
            {
                //DirectoryEntry  Server  =  new  DirectoryEntry("IIS://localhost/W3SVC/3/Root"); 
                //Server.Properties["AppPoolId"].Value = "AppPoolTest"; 
                System.DirectoryServices.DirectoryEntry vDir = returnSite(SiteName);
                vDir = getRoot(vDir);
                //string className = vDir.SchemaClassName.ToString();
                //if (className.EndsWith("VirtualDir"))
                //{
                object[] param = { 0, AppPoolName, true };
                vDir.Invoke("AppCreate3", param);
                vDir.CommitChanges();
                //vDir.Properties["AppIsolated"][0] = "2";
                //}
            }
            else
            {
                Microsoft.Web.Administration.ServerManager sm = new Microsoft.Web.Administration.ServerManager();
                sm.Sites[SiteName].Applications["/"].ApplicationPoolName = AppPoolName;
                //sm.Sites[WebServerComment].ApplicationDefaults.ApplicationPoolName = AppPoolName;
                sm.CommitChanges();
            }

        }

    }

    /// <summary>
    /// 网站。
    /// </summary>
    public class Site
    {
        internal long _Index = -1;
        /// <summary>
        /// 获取索引。
        /// </summary>
        public long Index
        {
            get
            {
                return this._Index;
            }
        }
        private string _Name = "";
        /// <summary>
        /// 网站名称
        /// </summary>
        public string Name
        {
            get
            {
                return this._Name;
            }
            set
            {
                this._Name = value;
            }
        }
        private bool _AccessScript = false;
        /// <summary>
        /// 执行权限—纯脚本
        /// </summary>
        public bool AccessScript
        {
            get
            {
                return this._AccessScript;
            }
            set
            {
                this._AccessScript = value;
            }
        }
        private bool _AccessRead = true;
        /// <summary>
        /// 指示是否支持读取
        /// </summary>
        public bool AccessRead
        {
            get
            {
                return this._AccessRead;
            }
            set
            {
                this._AccessRead = value;
            }
        }
        private string _Path = @"";
        /// <summary>
        /// 本地路径
        /// </summary>
        public string Path
        {
            get
            {
                return this._Path;
            }
            set
            {
                this._Path = value;
            }
        }
        /// <summary>
        /// 网站标识集合。
        /// </summary>
        public MarkCollection Marks = new MarkCollection();
        /// <summary>
        /// IP 地址。
        /// </summary>
        public string IP
        {
            get
            {
                return this.Marks[0].IP;
            }
            set
            {
                this.Marks[0].IP = value;
            }
        }
        /// <summary>
        /// 端口，默认值为 80 端口。
        /// </summary>
        public int Port
        {
            get
            {
                return this.Marks[0].Port;
            }
            set
            {
                this.Marks[0].Port = value;
            }
        }
        /// <summary>
        /// 主机头。
        /// </summary>
        public string HostHeader
        {
            get
            {
                return this.Marks[0].HostHeader;
            }
            set
            {
                this.Marks[0].HostHeader = value;
            }
        }
        private bool _EnableDirBrowsing = false;
        /// <summary>
        /// 指示是否允许支持目录浏览
        /// </summary>
        public bool EnableDirBrowsing
        {
            get
            {
                return this._EnableDirBrowsing;
            }
            set
            {
                this._EnableDirBrowsing = value;
            }
        }
        //private string _DefaultDoc = "index.htm,default.htm,default.aspx,index.aspx,default.asp";
        private System.Collections.Generic.List<string> _DefaultDoc = new System.Collections.Generic.List<string>();
        /// <summary>
        /// 获取默认文档。
        /// </summary>
        public System.Collections.Generic.List<string> DefaultDoc
        {
            get
            {
                return this._DefaultDoc;
            }
        }
        private bool _EnableDefaultDoc = true;
        /// <summary>
        /// 指示是否使用默认文档
        /// </summary>
        public bool EnableDefaultDoc
        {
            get
            {
                return this._EnableDefaultDoc;
            }
            set
            {
                this._EnableDefaultDoc = value;
            }
        }
        private bool _AccessWrite = false;
        /// <summary>
        /// 指示是否支持写入
        /// </summary>
        public bool AccessWrite
        {
            get
            {
                return this._AccessWrite;
            }
            set
            {
                this._AccessWrite = value;
            }
        }
        private bool _AccessExecute = false;
        /// <summary>
        /// 执行权限—脚本和可执行文件
        /// </summary>
        public bool AccessExecute
        {
            get
            {
                return this._AccessExecute;
            }
            set
            {
                this._AccessExecute = value;
            }
        }
        private bool _AuthAnonymous = true;
        /// <summary>
        /// 允许匿名访问
        /// </summary>
        public bool AuthAnonymous
        {
            get
            {
                return this._AuthAnonymous;
            }
            set
            {
                this._AuthAnonymous = value;
            }
        }
        private bool _AuthBasic = false;
        /// <summary>
        /// 允许基本验证
        /// </summary>
        public bool AuthBasic
        {
            get
            {
                return this._AuthBasic;
            }
            set
            {
                this._AuthBasic = value;
            }
        }
        private bool _AuthNTLM = false;
        /// <summary>
        /// 允许 Windows 集成验证
        /// </summary>
        public bool AuthNTLM
        {
            get
            {
                return this._AuthNTLM;
            }
            set
            {
                this._AuthNTLM = value;
            }
        }
        private bool _DontLog = false;
        /// <summary>
        /// 指示是否不记录访问
        /// </summary>
        public bool DontLog
        {
            get
            {
                return this._DontLog;
            }
            set
            {
                this._DontLog = value;
            }
        }
        private bool _ContentIndexed = true;
        /// <summary>
        /// 索引此资源
        /// </summary>
        public bool ContentIndexed
        {
            get
            {
                return this._ContentIndexed;
            }
            set
            {
                this._ContentIndexed = value;
            }
        }
        private bool _AspEnableParentPaths = false;
        /// <summary>
        /// 启用父路径
        /// </summary>
        public bool AspEnableParentPaths
        {
            get
            {
                return this._AspEnableParentPaths;
            }
            set
            {
                this._AspEnableParentPaths = value;
            }
        }
        private string _AnonymousUserName = "";
        /// <summary>
        /// 匿名用户
        /// </summary>
        public string AnonymousUserName
        {
            get
            {
                return this._AnonymousUserName;
            }
            set
            {
                this._AnonymousUserName = value;
            }
        }
        private string _AnonymousUserPass = "";
        /// <summary>
        /// 匿名用户密码
        /// </summary>
        public string AnonymousUserPass
        {
            get
            {
                return this._AnonymousUserPass;
            }
            set
            {
                this._AnonymousUserPass = value;
            }
        }
        private string _AppFriendlyName = "";
        /// <summary>
        /// 应用程序友好的显示名称
        /// </summary>
        public string AppFriendlyName
        {
            get
            {
                return this._AppFriendlyName;
            }
            set
            {
                this._AppFriendlyName = value;
            }
        }
        private AspNetVersion _AppAspNetVersion = AspNetVersion.Default;
        /// <summary>
        /// 获取或设置应用程序 ASP.NET 版本。
        /// </summary>
        public AspNetVersion AppAspNetVersion
        {
            get
            {
                return this._AppAspNetVersion;
            }
            set
            {
                this._AppAspNetVersion = value;
            }
        }

        internal IISServer.SiteState _ServerState = SiteState.Stopped;
        /// <summary>
        /// 获取网站的状态。
        /// </summary>
        public IISServer.SiteState ServerState
        {
            get
            {
                return this._ServerState;
            }

        }

        /// <summary>
        /// 初始化此实例。
        /// </summary>
        public Site()
        {
            this.DefaultDoc.AddRange(new string[] { "index.html", "index.htm", "default.html", "default.htm", "default.aspx", "index.aspx", "default.asp" });
        }


    }

    /// <summary>
    /// 应用程序。
    /// </summary>
    public class Application
    {
        private string _Name = "";
        /// <summary>
        /// 获取应用程序名称。
        /// </summary>
        public string Name
        {
            get
            {
                return this._Name;
            }
        }
        private bool _AccessRead = true;
        /// <summary>
        /// 指示是否支持读取
        /// </summary>
        public bool AccessRead
        {
            get
            {
                return this._AccessRead;
            }
            set
            {
                this._AccessRead = value;
            }
        }
        private bool _AccessScript = false;
        /// <summary>
        /// 执行权限—纯脚本
        /// </summary>
        public bool AccessScript
        {
            get
            {
                return this._AccessScript;
            }
            set
            {
                this._AccessScript = value;
            }
        }
        private string _Path = "";
        /// <summary>
        /// 获取或设置本地路径
        /// </summary>
        public string Path
        {
            get
            {
                return this._Path;
            }
            set
            {
                this._Path = value;
            }
        }
        //private string _DefaultDoc = "index.htm,default.htm,default.aspx,index.aspx,default.asp";
        private System.Collections.Generic.List<string> _DefaultDoc = new System.Collections.Generic.List<string>();
        /// <summary>
        /// 获取默认文档。
        /// </summary>
        public System.Collections.Generic.List<string> DefaultDoc
        {
            get
            {
                return this._DefaultDoc;
            }
        }
        private bool _EnableDefaultDoc = true;
        /// <summary>
        /// 指示是否使用默认文档
        /// </summary>
        public bool EnableDefaultDoc
        {
            get
            {
                return this._EnableDefaultDoc;
            }
            set
            {
                this._EnableDefaultDoc = value;
            }
        }
        private bool _AccessWrite = false;
        /// <summary>
        /// 指示是否支持写入
        /// </summary>
        public bool AccessWrite
        {
            get
            {
                return this._AccessWrite;
            }
            set
            {
                this._AccessWrite = value;
            }
        }
        private bool _AccessExecute = false;
        /// <summary>
        /// 执行权限—脚本和可执行文件
        /// </summary>
        public bool AccessExecute
        {
            get
            {
                return this._AccessExecute;
            }
            set
            {
                this._AccessExecute = value;
            }
        }
        private bool _AuthAnonymous = true;
        /// <summary>
        /// 允许匿名访问
        /// </summary>
        public bool AuthAnonymous
        {
            get
            {
                return this._AuthAnonymous;
            }
            set
            {
                this._AuthAnonymous = value;
            }
        }
        private bool _AuthBasic = false;
        /// <summary>
        /// 允许基本验证
        /// </summary>
        public bool AuthBasic
        {
            get
            {
                return this._AuthBasic;
            }
            set
            {
                this._AuthBasic = value;
            }
        }
        private bool _AuthNTLM = false;
        /// <summary>
        /// 允许 Windows 集成验证
        /// </summary>
        public bool AuthNTLM
        {
            get
            {
                return this._AuthNTLM;
            }
            set
            {
                this._AuthNTLM = value;
            }
        }
        private bool _DontLog = false;
        /// <summary>
        /// 指示是否不记录访问
        /// </summary>
        public bool DontLog
        {
            get
            {
                return this._DontLog;
            }
            set
            {
                this._DontLog = value;
            }
        }
        private bool _ContentIndexed = true;
        /// <summary>
        /// 索引此资源
        /// </summary>
        public bool ContentIndexed
        {
            get
            {
                return this._ContentIndexed;
            }
            set
            {
                this._ContentIndexed = value;
            }
        }
        private bool _AspEnableParentPaths = false;
        /// <summary>
        /// 启用父路径
        /// </summary>
        public bool AspEnableParentPaths
        {
            get
            {
                return this._AspEnableParentPaths;
            }
            set
            {
                this._AspEnableParentPaths = value;
            }
        }
        private string _AnonymousUserName = "";
        /// <summary>
        /// 获取或设置匿名用户
        /// </summary>
        public string AnonymousUserName
        {
            get
            {
                return this._AnonymousUserName;
            }
            set
            {
                this._AnonymousUserName = value;
            }
        }
        private string _AnonymousUserPass = "";
        /// <summary>
        /// 获取或设置匿名用户密码
        /// </summary>
        public string AnonymousUserPass
        {
            get
            {
                return this._AnonymousUserPass;
            }
            set
            {
                this._AnonymousUserPass = value;
            }
        }
        private string _AppFriendlyName = "";
        /// <summary>
        /// 友好的显示名称
        /// </summary>
        public string AppFriendlyName
        {
            get
            {
                return this._AppFriendlyName;
            }
            set
            {
                this._AppFriendlyName = value;
            }
        }
        private AspNetVersion _AppAspNetVersion = AspNetVersion.Default;
        /// <summary>
        /// 获取或设置应用程序 ASP.NET 版本。
        /// </summary>
        public AspNetVersion AppAspNetVersion
        {
            get
            {
                return this._AppAspNetVersion;
            }
            set
            {
                this._AppAspNetVersion = value;
            }
        }

        /// <summary>
        /// 初始化此实例。
        /// </summary>
        /// <param name="Name">应用程序名称。</param>
        public Application(string Name)
        {
            this.DefaultDoc.AddRange(new string[] { "index.html", "index.htm", "default.html", "default.htm", "default.aspx", "index.aspx", "default.asp" });
            this._Name = Name;

        }

    }

    /// <summary>
    /// 网站标识集合。
    /// </summary>
    public class MarkCollection : System.Collections.Generic.ICollection<IISServer.Mark>
    {
        /// <summary>
        /// 网站标识集合。
        /// </summary>
        private System.Collections.Generic.List<IISServer.Mark> markCollection = new System.Collections.Generic.List<Mark>();

        /// <summary>
        /// 索引。
        /// </summary>
        public IISServer.Mark this[int Index]
        {
            get
            {
                return this.markCollection[Index];
            }
        }

        #region ICollection<Mark> 成员
        /// <summary>
        /// 将网站标识添加到集合的结尾处。
        /// </summary>
        /// <param name="item">网站标识</param>
        public void Add(Mark item)
        {
            this.markCollection.Add(item);

        }
        /// <summary>
        /// 从网站标识集合中移除所有元素。（此方法会保留唯一的默认网站标识。）
        /// </summary>
        public void Clear()
        {
            for (int i = 1; i < this.markCollection.Count; i++)
            {
                this.markCollection.RemoveAt(i);
            }

        }
        /// <summary>
        /// 确定网站标识是否在集合中。
        /// </summary>
        /// <param name="item">要在集合中定位的网站标识。</param>
        /// <returns>存在返回 true；否则返回 false。</returns>
        public bool Contains(Mark item)
        {
            return this.markCollection.Contains(item);

        }
        /// <summary>
        /// 复制整个网站标识集合。
        /// </summary>
        /// <param name="array">将数据复制到这个网站标识数组中。</param>
        /// <param name="arrayIndex">array 中从零开始的索引，在此处开始复制。</param>
        public void CopyTo(Mark[] array, int arrayIndex)
        {
            this.markCollection.CopyTo(array, arrayIndex);

        }
        /// <summary>
        /// 网站标识数量。
        /// </summary>
        public int Count
        {
            get { return this.markCollection.Count; }
        }
        /// <summary>
        /// 指示集合是否只读。
        /// </summary>
        public bool IsReadOnly
        {
            get { return false; }
        }
        /// <summary>
        /// 从集合中移除特定对象的第一个匹配项。
        /// </summary>
        /// <param name="item">要移除的网站标识对象。</param>
        /// <returns>成功移除返回 true；否则返回 false。</returns>
        public bool Remove(Mark item)
        {
            if (this.markCollection.Count <= 1)
            {
                throw new System.Exception("禁止移除唯一的网站标识。");
            }
            return this.markCollection.Remove(item);

        }

        #endregion

        #region IEnumerable 成员

        /// <summary>
        /// 返回循环访问集合的枚举数。
        /// </summary>
        /// <returns>用于遍历集合的 IEnumerator 对象。</returns>
        public System.Collections.Generic.IEnumerator<Mark> GetEnumerator()
        {
            return this.markCollection.GetEnumerator();

        }

        #endregion

        #region IEnumerable 成员

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.markCollection.GetEnumerator();
        }

        #endregion

        /// <summary>
        /// 用于初始化对象。
        /// </summary>
        public MarkCollection()
        {
            this.markCollection.Add(new Mark("", 80, ""));//网站标识集合中需要至少存在一个默认的标识。

        }

    }

    /// <summary>
    /// 网站标识。
    /// </summary>
    public class Mark
    {
        private string _IP = "";
        /// <summary>
        /// 获取或设置IP 地址。
        /// </summary>
        public string IP
        {
            get
            {
                return this._IP;
            }
            set
            {
                this._IP = value;
            }
        }
        private int _Port = 80;
        /// <summary>
        /// TCP 端口。
        /// </summary>
        public int Port
        {
            get
            {
                return this._Port;
            }
            set
            {
                this._Port = value;
            }
        }
        private string _HostHeader = "";
        /// <summary>
        /// 主机头。
        /// </summary>
        public string HostHeader
        {
            get
            {
                return this._HostHeader;
            }
            set
            {
                this._HostHeader = value;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public Mark()
        {
        }

        /// <summary>
        /// 用指定的参数实例化对象。
        /// </summary>
        /// <param name="IP">IP 地址。</param>
        /// <param name="Port">TCP 端口。</param>
        /// <param name="HostHeader">主机头。</param>
        public Mark(string IP, int Port, string HostHeader)
        {
            this.IP = IP;
            this.Port = Port;
            this.HostHeader = HostHeader;

        }

    }

}
