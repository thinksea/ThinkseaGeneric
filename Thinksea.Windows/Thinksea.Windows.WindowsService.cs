namespace Thinksea.Windows
{
    /// <summary>
    /// 封装了对 Windows Service 的管理功能。
    /// </summary>
    public class WindowsService
    {
        /// <summary>
        /// 安装 Windows 服务
        /// </summary>
        /// <param name="filepath">服务程序文件路径。</param>
        /// <param name="serviceName">服务名称。</param>
        /// <param name="options">在为程序集安装创建新的 System.Configuration.Install.InstallContext 对象时要使用的命令行。设置为 null 表示忽略此项。</param>
        public static void InstallService(string filepath, string serviceName, string[] options)
        {
            //try
            //{
                if (!IsServiceExisted(serviceName))
                {
                    System.Collections.IDictionary mySavedState = new System.Collections.Hashtable();
                    using (System.Configuration.Install.AssemblyInstaller myAssemblyInstaller = new System.Configuration.Install.AssemblyInstaller())
                    {
                        myAssemblyInstaller.UseNewContext = true;
                        myAssemblyInstaller.Path = filepath;
                        if (options != null)
                        {
                            myAssemblyInstaller.CommandLine = options;
                        }
                        try
                        {
                            myAssemblyInstaller.Install(mySavedState);
                            myAssemblyInstaller.Commit(mySavedState);
                        }
                        catch
                        {
                            myAssemblyInstaller.Rollback(mySavedState);
                            throw;
                        }
                        //myAssemblyInstaller.Dispose();
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Install Service Error\n" + ex.Message);
            //}
        }

        /// <summary>
        /// 卸载 Windows 服务
        /// </summary>
        /// <param name="filepath">服务程序文件路径。</param>
        /// <param name="serviceName">服务名称。</param>
        /// <param name="options">在为程序集安装创建新的 System.Configuration.Install.InstallContext 对象时要使用的命令行。设置为 null 表示忽略此项。</param>
        public static void UnInstallService(string filepath, string serviceName, string[] options)
        {
            //try
            //{
                if (IsServiceExisted(serviceName))
                {
                    using (System.Configuration.Install.AssemblyInstaller myAssemblyInstaller = new System.Configuration.Install.AssemblyInstaller())
                    {
                        myAssemblyInstaller.UseNewContext = true;
                        myAssemblyInstaller.Path = filepath;
                        if (options != null)
                        {
                            myAssemblyInstaller.CommandLine = options;
                        }
                        myAssemblyInstaller.Uninstall(null);
                        //myAssemblyInstaller.Dispose();
                    }
                }
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("UnInstall Service Error\n" + ex.Message);
            //}
        }

        /// <summary>
        /// 判断 Windows 服务是否存在
        /// </summary>
        /// <param name="serviceName">服务名称。</param>
        /// <returns>存在返回 true；否则返回 false。</returns>
        public static bool IsServiceExisted(string serviceName)
        {
            System.ServiceProcess.ServiceController[] services = System.ServiceProcess.ServiceController.GetServices();
            foreach (System.ServiceProcess.ServiceController s in services)
            {
                if (s.ServiceName == serviceName)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 启动 Windows 服务
        /// </summary>
        /// <param name="serviceName">服务名称。</param>
        public static void StartService(string serviceName)
        {
            if (IsServiceExisted(serviceName))
            {
                System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController(serviceName);
                if (service.Status != System.ServiceProcess.ServiceControllerStatus.Running && service.Status != System.ServiceProcess.ServiceControllerStatus.StartPending)
                {
                    service.Start();
                }
                service.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Running, System.TimeSpan.FromSeconds(60));
            }
        }

        /// <summary>
        /// 停止 Windows 服务
        /// </summary>
        /// <param name="serviceName">服务名称。</param>
        public static void StopService(string serviceName)
        {
            if (IsServiceExisted(serviceName))
            {
                System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController(serviceName);
                if (service.Status != System.ServiceProcess.ServiceControllerStatus.StopPending && service.Status != System.ServiceProcess.ServiceControllerStatus.Stopped)
                {
                    service.Stop();
                }
                service.WaitForStatus(System.ServiceProcess.ServiceControllerStatus.Stopped, System.TimeSpan.FromSeconds(60));
            }
        }

        /// <summary>
        /// 获取 Windows 服务的状态。
        /// </summary>
        /// <param name="serviceName">服务名称。</param>
        /// <returns>服务的状态。</returns>
        public static System.ServiceProcess.ServiceControllerStatus GetServiceState(string serviceName)
        {
            System.ServiceProcess.ServiceController service = new System.ServiceProcess.ServiceController(serviceName);
            return service.Status;
        }

    }
}
