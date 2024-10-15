using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Thinksea.Net.FileUploader_WebFormDemo
{
    /// <summary>
    /// 上传文件服务。
    /// </summary>
    public class HttpUploadHandler : IHttpHandler
    {
        private CustomHttpUploadServer customHttpUploadServer = new CustomHttpUploadServer();

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            customHttpUploadServer.ProcessRequest(context);
        }

        public bool IsReusable
        {
            get
            {
                return true;
            }
        }
    }

    /// <summary>
    /// 实现文件上传组件并添加自定义功能。
    /// </summary>
    internal class CustomHttpUploadServer : Thinksea.Net.FileUploader.HttpUploadServer
    {
        public new void ProcessRequest(System.Web.HttpContext context)
        {
            var response = context.Response;

            context.Response.AppendHeader("Access-Control-Allow-Origin", "*"); //解决 AJAX 跨域访问本接口。
            //response.Headers[Microsoft.Net.Http.Headers.HeaderNames.AccessControlAllowOrigin] = "*"; //解决 AJAX 跨域访问本接口。

            base.ProcessRequest(context);
        }

        /// <summary>
        /// 将指定的参数字符串（多个参数之间以分号“;”分隔；键与值以等号“=”分隔）转换为键值对形式。
        /// </summary>
        /// <param name="param">参数字符串。</param>
        /// <returns>参数集合。</returns>
        private System.Collections.Generic.Dictionary<string, string> ConvertParamFromString(string param)
        {
            System.Collections.Generic.Dictionary<string, string> parms = new System.Collections.Generic.Dictionary<string, string>();
            #region 解析参数。
            if (param != null && param.Length > 0)
            {
                string[] parmsList = param.Split(new char[] { ';' }, System.StringSplitOptions.RemoveEmptyEntries);
                if (parmsList.Length > 0)
                {
                    foreach (string tmp in parmsList)
                    {
                        string[] kv = tmp.Split(new char[] { '=' }, System.StringSplitOptions.RemoveEmptyEntries);
                        if (kv.Length > 1)
                        {
                            parms.Add(kv[0], kv[1]);
                        }
                        else if (kv.Length == 1)
                        {
                            parms.Add(kv[0], "");
                        }
                    }
                }
            }
            #endregion
            return parms;
        }

        /// <summary>
        /// 从指定的参数集合中获取参数。
        /// </summary>
        /// <param name="parms">参数集合。</param>
        /// <param name="key">参数的名称。</param>
        /// <returns>参数的值，找不到返回 null。</returns>
        private string GetParam(System.Collections.Generic.Dictionary<string, string> parms, string key)
        {
            if (parms.ContainsKey(key))
            {
                return parms[key];
            }
            return null;
        }

        private string _FileUploadTempDirectory = null;
        public override string FileUploadTempDirectory
        {
            get
            {
                if (this._FileUploadTempDirectory == null)
                {
                    this._FileUploadTempDirectory = Define.MapFileUploadPath("/upload/temp");
                }
                return this._FileUploadTempDirectory;
            }
        }

        /// <summary>
        /// 当客户端请求尝试秒传文件时调用此方法。
        /// </summary>
        /// <param name="e">秒传需要的数据。</param>
        public override void FastUpload(Thinksea.Net.FileUploader.FastUploadEventArgs e)
        {
            #region 秒传代码逻辑。如果不支持秒传，则不应设置 e.ResultData。
            #region 文件校验码、文件类型以及自定义参数的处理。
            string sha1 = ""; //文件唯一码（例如通过 sha1 算法计算文件内容的散列值），一般情况，可通过判断待上传文件的 sha1 码是否与位于服务端的文件 sha1 码相同实现秒传。
            {
                sha1 = System.BitConverter.ToString(e.CheckCode).Replace("-", "");
            }

            System.Collections.Generic.Dictionary<string, string> parms = this.ConvertParamFromString(e.Parameters);

            string ClientFileExtension = System.IO.Path.GetExtension(e.ClientFileName).ToLowerInvariant();
            #endregion

            //e.ResultData = new FileUploadResult
            //(
            //    "", //文件路径。
            //    0, //文件大小。
            //    true
            //);
            #endregion
        }

        /// <summary>
        /// 每次与客户端建立连接准备上传文件时调用此方法。注意：一个文件的上传过程可能会多次调用此方法。
        /// </summary>
        public override void BeginUpload(Thinksea.Net.FileUploader.FileUploadEventArgs e)
        {
            if (e.StartPosition == 0) //当上传文件的第一个数据块时校验权限。（为了提高上传效率，只对首次上传和上传完成两个时间点做权限校验）
            {
                #region 文件类型以及自定义参数的处理。
                System.Collections.Generic.Dictionary<string, string> parms = this.ConvertParamFromString(e.Parameters);

                string ClientFileExtension = System.IO.Path.GetExtension(e.ClientFileName).ToLowerInvariant();
                #endregion

                #region 校验是否满足文件上传条件。
                //throw new System.Exception("禁止上传的文件类型“" + ClientFileExtension + "”！");
                #endregion

            }
        }

        /// <summary>
        /// 当文件上传完成时调用此方法。
        /// </summary>
        public override void UploadFinished(Thinksea.Net.FileUploader.UploadFinishedEventArgs e)
        {
            //base.FinishedFileUpload(e);
            //string targetFile = this.GetTargetFile(e.ClientFileName);
            //if (e.ServerFile != targetFile)
            //{
            //    if (System.IO.File.Exists(targetFile))
            //    {
            //        System.IO.File.Delete(targetFile);
            //    }
            //    System.IO.File.Move(e.ServerFile, targetFile);//将临时文件名更改为目的文件名。
            //}

            #region 文件类型以及自定义参数的处理。
            System.Collections.Generic.Dictionary<string, string> parms = this.ConvertParamFromString(e.Parameters);

            string ClientFileExtension = System.IO.Path.GetExtension(e.ClientFileName).ToLowerInvariant();
            #endregion

            #region
            string middleDir = System.DateTime.Now.ToString("yyyy-MM-dd");
            string OutFileNameWithoutExtension = System.Guid.NewGuid().ToString("N");
            string saveDirectory = Define.MapFileUploadPath(System.IO.Path.Combine(Define.FileUploadDirectory, middleDir));
            if (!System.IO.Directory.Exists(saveDirectory))
            {
                System.IO.Directory.CreateDirectory(saveDirectory);
            }
            string outputFile = System.IO.Path.Combine(saveDirectory, OutFileNameWithoutExtension + ClientFileExtension);
            while (System.IO.File.Exists(outputFile))
            {
                OutFileNameWithoutExtension = System.Guid.NewGuid().ToString("N");
                outputFile = System.IO.Path.Combine(saveDirectory, OutFileNameWithoutExtension + ClientFileExtension);
            }
            System.IO.File.Move(e.ServerFile, outputFile);
            string fileSavePath = System.IO.Path.Combine(System.IO.Path.Combine(Define.FileUploadDirectory, middleDir), OutFileNameWithoutExtension + ClientFileExtension).Replace(System.IO.Path.DirectorySeparatorChar, System.IO.Path.AltDirectorySeparatorChar);
            System.IO.FileInfo fi = new System.IO.FileInfo(outputFile);
            long fileLength = fi.Length;

            e.ResultData = new FileUploadResult
            (
                fileSavePath,
                fileLength,
                false
            );
            #endregion

        }

    }

    /// <summary>
    /// 文件上传返回结果。
    /// </summary>
    public class FileUploadResult
    {
        /// <summary>
        /// 文件存储路径。
        /// </summary>
        public string SavePath
        {
            get;
            private set;
        }
        /// <summary>
        /// 文件大小（单位：字节）
        /// </summary>
        public long FileLength
        {
            get;
            private set;
        }

        /// <summary>
        /// 指示是否秒传。
        /// </summary>
        public bool IsFastUpload
        {
            get;
            private set;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="savePath">文件存储路径。</param>
        /// <param name="fileLength">文件大小（单位：字节）</param>
        /// <param name="isFastUpload">指示是否秒传。</param>
        /// <param name="customParameter">自定义参数。</param>
        public FileUploadResult(string savePath, long fileLength, bool isFastUpload)
        {
            this.SavePath = savePath;
            this.FileLength = fileLength;
            this.IsFastUpload = isFastUpload;
        }

    }

}