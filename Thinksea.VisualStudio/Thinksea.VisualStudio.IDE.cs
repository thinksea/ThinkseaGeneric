/*
 * 封装了对 VisualStudio 的访问接口。
 */
namespace Thinksea.VisualStudio
{
    /// <summary>
    /// 封装了对 VisualStudio 设计器的访问接口。
    /// </summary>
    /// <remarks>
    /// 相关 EnvDTE 编程知识可以从这里开始“http://technet.microsoft.com/zh-cn/envdte.dte”。
    /// </remarks>
    public class IDE
    {
        //		public override void OnSetComponentDefaults()
        //		{
        //
        //			//Moniker string definition: "!VisualStudio.DTE.7.1:"
        //
        //			string strMoniker = "!VisualStudio.DTE.7.1:" +
        //
        //				System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
        //
        //			////MessageBox.Show(strMoniker);
        //
        // 
        //
        //			EnvDTE._DTE dte =(EnvDTE._DTE)GetMSDEVFromGIT(strMoniker);
        //
        //			////this.Control.Text = dte.Solution.FullName;
        //			
        //
        //		}
        //

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reserved"></param>
        /// <param name="prot"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("ole32.dll")]
        public static extern int GetRunningObjectTable(int reserved, out System.Runtime.InteropServices.ComTypes.IRunningObjectTable prot);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="reserved"></param>
        /// <param name="ppbc"></param>
        /// <returns></returns>
        [System.Runtime.InteropServices.DllImport("ole32.dll")]
        public static extern int CreateBindCtx(int reserved, out System.Runtime.InteropServices.ComTypes.IBindCtx ppbc);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="strProgID"></param>
        /// <param name="type">查询数据类型。</param>
        /// <returns></returns>
        /// <example>
        /// string strMoniker = "!VisualStudio.DTE.7.1:" + System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
        /// EnvDTE.DTE dte = (EnvDTE.DTE)this.GetMSDEVFromGIT(strMoniker);
        /// </example>
        public static object GetMSDEVFromGIT(string strProgID)
        {
            System.Runtime.InteropServices.ComTypes.IRunningObjectTable prot;
            System.Runtime.InteropServices.ComTypes.IEnumMoniker pMonkEnum;

            GetRunningObjectTable(0, out prot);
            prot.EnumRunning(out pMonkEnum);
            pMonkEnum.Reset();

            System.IntPtr fetched = new System.IntPtr();

            System.Runtime.InteropServices.ComTypes.IMoniker[] pmon = new System.Runtime.InteropServices.ComTypes.IMoniker[1];
            while (pMonkEnum.Next(1, pmon, fetched) == 0)
            {

                System.Runtime.InteropServices.ComTypes.IBindCtx pCtx;

                CreateBindCtx(0, out pCtx);

                string str;

                pmon[0].GetDisplayName(pCtx, null, out str);
                if (str == strProgID)
                {

                    object objReturnObject;

                    prot.GetObject(pmon[0], out objReturnObject);

                    object ide = (object)objReturnObject;
                    return ide;
                }

            }
            return null;

        }

        /// <summary>
        /// 获取 VisualStudio 设计器实例。
        /// </summary>
        /// <returns>返回找到的设计器实例；否则返回 null。</returns>
        /// <remarks>
        /// 此方法由方法“GetMSDEVFromGIT”改写。
        /// </remarks>
        public static EnvDTE.DTE GetVisualStudioIDE()
        {
            //string strProgID = "!VisualStudio.DTE.7.1:" + System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
            //string strProgID = "!VisualStudio.DTE.8.0:" + System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
            //string strProgID = "!VisualStudio.DTE.9.0:" + System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
            //string strProgID = "!VisualStudio.DTE.11.0:" + System.Diagnostics.Process.GetCurrentProcess().Id.ToString();
            string strProgID = @"!VisualStudio\.DTE\.\d+\.\d+\:" + System.Diagnostics.Process.GetCurrentProcess().Id.ToString();

            System.Runtime.InteropServices.ComTypes.IRunningObjectTable prot;
            System.Runtime.InteropServices.ComTypes.IEnumMoniker pMonkEnum;

            GetRunningObjectTable(0, out prot);
            prot.EnumRunning(out pMonkEnum);
            pMonkEnum.Reset();

            System.IntPtr fetched = new System.IntPtr();

            System.Runtime.InteropServices.ComTypes.IMoniker[] pmon = new System.Runtime.InteropServices.ComTypes.IMoniker[1];
            while (pMonkEnum.Next(1, pmon, fetched) == 0)
            {
                System.Runtime.InteropServices.ComTypes.IBindCtx pCtx;
                CreateBindCtx(0, out pCtx);

                string str;
                pmon[0].GetDisplayName(pCtx, null, out str);
                if (System.Text.RegularExpressions.Regex.IsMatch(str, strProgID, System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.ExplicitCapture))
                {

                    object objReturnObject;
                    prot.GetObject(pmon[0], out objReturnObject);

                    EnvDTE.DTE ide = objReturnObject as EnvDTE.DTE;
                    if (ide != null)
                    {
                        return ide;
                    }
                }

            }
            return null;

        }

        /// <summary>
        /// 获取解决方案文件的完整文件名。
        /// </summary>
        /// <returns></returns>
        public string GetSolutionFileName()
        {
            EnvDTE.DTE dte = GetVisualStudioIDE();

            return dte.Solution.FullName;
        }

        /// <summary>
        /// 获取解决方案文件的目录。
        /// </summary>
        /// <returns></returns>
        public string GetSolutionDirectory()
        {
            EnvDTE.DTE dte = GetVisualStudioIDE();

            return dte.Solution.FullName;
        }

        /// <summary>
        /// 获取正在设计的当前活动文档隶属的工程(项目)文件的完整文件名。
        /// </summary>
        /// <returns></returns>
        public string GetActiveProjectFileName()
        {
            EnvDTE.DTE dte = GetVisualStudioIDE();

            return dte.ActiveDocument.ProjectItem.ContainingProject.FileName;
        }

        /// <summary>
        /// 获取正在设计的当前活动文档隶属的项目目录。
        /// </summary>
        /// <returns></returns>
        public string GetActiveProjectDirectory()
        {
            string projectFile = GetActiveProjectFileName();
            return System.IO.Path.GetDirectoryName(projectFile);
        }

        /// <summary>
        /// 获取正在设计的当前活动文档的完整文件名。
        /// </summary>
        /// <returns></returns>
        public string GetActiveDocumentFileName()
        {
            EnvDTE.DTE dte = GetVisualStudioIDE();

            return dte.ActiveDocument.FullName;
        }

        /// <summary>
        /// 获取正在设计的当前活动文档的目录。
        /// </summary>
        /// <returns></returns>
        public string GetActiveDocumentDirectory()
        {
            string documentFile = GetActiveDocumentFileName();
            return System.IO.Path.GetDirectoryName(documentFile);
        }

    }

}
