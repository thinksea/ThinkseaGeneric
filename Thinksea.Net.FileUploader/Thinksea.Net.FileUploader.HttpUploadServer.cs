namespace Thinksea.Net.FileUploader
{
    /// <summary>
    /// 封装了基于 HTTP 协议的文件上传服务器端功能。
    /// </summary>
    /// <remarks>
    /// 支持的 URL 参数及其示意：
    /// cmd：调用服务器端指令名称。可用指令名称如下：
    ///     fastupload (filename,filesize,checkcode,param)：尝试秒传指定的文件。
    ///     upload (filename,filesize,checkcode,param)：上传文件。（默认指令，当未指定cmd参数时执行此指令。）
    ///     getoffset (filename,filesize,checkcode,param,offset)：查询续传起始地址。
    /// filename：客户端文件名
    /// filesize：文件大小
    /// offset：上传数据起始偏移地址。
    /// checkcode：文件完整性校验码，如果设置了此项参数，则在文件上传完成时执行文件完整性校验。
    /// param：用户自定义参数
    /// </remarks>
    public abstract class HttpUploadServer
    {
        private string _TempExtension = ".config";
        /// <summary>
        /// 获取或设置临时文件扩展名。
        /// </summary>
        public string TempExtension
        {
            get
            {
                return this._TempExtension;
            }
            set
            {
                this._TempExtension = value;
            }
        }

        private string _FileUploadTempDirectory = null;
        /// <summary>
        /// 获取文件上传临时存储目录
        /// </summary>
        public virtual string FileUploadTempDirectory
        {
            get
            {
                if (this._FileUploadTempDirectory == null)
                {
#if NETFRAMEWORK
                    string baseDirectory = System.Web.Hosting.HostingEnvironment.ApplicationPhysicalPath;
#elif NETCOREAPP
                    string baseDirectory = System.AppContext.BaseDirectory;
#endif
                    this._FileUploadTempDirectory = System.IO.Path.Combine(baseDirectory, @"UploadTemp");
                }
                return this._FileUploadTempDirectory;
            }
        }

        /// <summary>
        /// 从指定的数据流读取数据并保存到文件流。
        /// </summary>
        /// <param name="stream">数据源。</param>
        /// <param name="fs">文件数据流。</param>
        /// <returns>写入的数据大小。</returns>
        private long SaveFile(System.IO.Stream stream, System.IO.FileStream fs)
        {
            long r = 0;
            byte[] buffer = new byte[4096];
            int bytesRead;
            while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) != 0)
            {
                fs.Write(buffer, 0, bytesRead);
                r += bytesRead;
            }
            return r;
        }

        /// <summary>
        /// 分配一个临时上传文件名。
        /// </summary>
        /// <param name="checkCode">文件完整性校验码。如果设置为 null，则分配一个随机的名称。</param>
        /// <returns>临时文件名。</returns>
        public virtual string GetTempFile(byte[] checkCode)
        {
            #region 保证文件上传物理根目录存在。
            if (!System.IO.Directory.Exists(this.FileUploadTempDirectory))
            {
                try
                {
                    System.IO.Directory.CreateDirectory(this.FileUploadTempDirectory);
                }
                catch (System.IO.IOException)
                {
                    throw new System.Exception("创建目录失败！原因是由 path 指定的目录是只读的或不为空。这将导致您无法上传文件。");
                }
                catch (System.UnauthorizedAccessException)
                {
                    throw new System.Exception("创建目录失败！原因是没有足够的权限。这将导致您无法上传文件。");
                }
                catch
                {
                    throw new System.Exception("创建目录失败！这将导致您无法上传文件。");
                }
            }
            #endregion

            string tempFile;
            if (checkCode == null)
            {
                tempFile = System.Guid.NewGuid().ToString("N"); //上传临时存盘文件名。
            }
            else
            {
                tempFile = Thinksea.General.Bytes2HexString(checkCode); //上传临时存盘文件名。
            }
            return System.IO.Path.Combine(this.FileUploadTempDirectory, tempFile + TempExtension);
        }

        /// <summary>
        /// 获取文件完整性校验码，例如 SHA1 或 MD5 等。
        /// </summary>
        /// <param name="stream">文件流。</param>
        /// <returns>文件完整性校验码。</returns>
        public virtual byte[] GetCheckCode(System.IO.Stream stream)
        {
            return Thinksea.General.GetSHA1(stream); //获取SHA1码。
        }

        /// <summary>
        /// 获取指定文件的续传位置。
        /// </summary>
        /// <param name="taskFile">任务文件。</param>
        /// <returns>返回找到的续传位置，否则返回 0 表示重头开始上传；</returns>
        public virtual long GetContinueUploadPosition(string taskFile)
        {
            if (!System.IO.File.Exists(taskFile))
            {
                return 0;
            }
            System.IO.FileInfo fi = new System.IO.FileInfo(taskFile);
            return fi.Length;
            //TaskFile tf = new TaskFile();
            //if (!tf.LoadFromFile(taskFile)) //加载数据失败则需要重新上传文件。
            //{
            //    return 0;
            //}
            //return tf.BytesUploaded;
        }

#if NETFRAMEWORK
        /// <summary>
        /// 从 URL 获取参数。
        /// </summary>
        /// <param name="request">当前上下文的 <see cref="System.Web.HttpRequest"/> 对象。</param>
        /// <param name="key">参数名。</param>
        /// <returns>参数值。</returns>
        private string GetUrlParameter(System.Web.HttpRequest request, string key)
        {
            return request.QueryString[key];
        }
#elif NETCOREAPP
        /// <summary>
        /// 从 URL 获取参数。
        /// </summary>
        /// <param name="request">当前上下文的 <see cref="Microsoft.AspNetCore.Http.HttpRequest"/> 对象。</param>
        /// <param name="key">参数名。</param>
        /// <returns>参数值。</returns>
        private string GetUrlParameter(Microsoft.AspNetCore.Http.HttpRequest request, string key)
        {
            return request.Query[key];
        }
#endif

        /// <summary>
        /// 通过实现 System.Web.IHttpHandler 接口的自定义 HttpHandler 启用 HTTP Web 请求的处理。
        /// </summary>
        /// <param name="context">System.Web.HttpContext 对象，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session 和 Server）的引用。</param>
#if NETFRAMEWORK
        public void ProcessRequest(System.Web.HttpContext context)
#elif NETCOREAPP
        public async void ProcessRequest(Microsoft.AspNetCore.Http.HttpContext context)
#endif
        {
            var request = context.Request;
            var response = context.Response;

            string result;
            try
            {
                result = this.DoCommand(context);
            }
            catch (System.Exception ex)
            {
                result = System.Text.Json.JsonSerializer.Serialize(new Thinksea.Net.FileUploader.Result()
                {
                    ErrorCode = 1,
                    Message = ex.Message
                });
#if DEBUG
                System.Diagnostics.Debug.WriteLine(ex.ToString());
#endif
            }

#if NETFRAMEWORK
            response.ContentEncoding = System.Text.Encoding.UTF8;
            response.ContentType = "application/json";
            response.AddHeader("Pragma", "no-cache");
            response.CacheControl = "no-cache";
            response.Write(result);
#elif NETCOREAPP
            {
                if (!response.Headers.ContainsKey(Microsoft.Net.Http.Headers.HeaderNames.Pragma))
                {
                    response.Headers.Add(Microsoft.Net.Http.Headers.HeaderNames.Pragma, "no-cache");
                }
                //response.CacheControl = "no-store";
                if (!response.Headers.ContainsKey(Microsoft.Net.Http.Headers.HeaderNames.CacheControl))
                {
                    response.Headers.Add(Microsoft.Net.Http.Headers.HeaderNames.CacheControl, "no-cache");
                }

                System.Net.Http.Headers.MediaTypeHeaderValue mediaType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
                mediaType.CharSet = "utf-8";
                response.ContentType = mediaType.ToString();
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    using (System.IO.StreamWriter sw = new System.IO.StreamWriter(ms, System.Text.Encoding.UTF8))
                    {
                        sw.Write(result);
                        sw.Flush();
                        ms.Seek(0, System.IO.SeekOrigin.Begin);
                        await ms.CopyToAsync(response.Body);
                    }
                }
                return;
            }
#endif

        }

#if NETFRAMEWORK
        private string DoCommand(System.Web.HttpContext context)
#elif NETCOREAPP
        private string DoCommand(Microsoft.AspNetCore.Http.HttpContext context)
#endif
        {
            var request = context.Request;
            var response = context.Response;
            switch (this.GetUrlParameter(request, "cmd"))
            {
                case "fastupload": //尝试秒传指定的文件。
                    {
                        string clientFileName = this.GetUrlParameter(request, "filename"); //客户端文件名。
                        long fileSize = long.Parse(this.GetUrlParameter(request, "filesize")); //文件大小。
                        string sCheckCode = this.GetUrlParameter(request, "checkcode"); //文件完整性校验码，如果设置了此项参数，则在文件上传完成时执行文件完整性校验。
                        byte[] clientCheckCode = Thinksea.General.HexString2Bytes(sCheckCode);
                        string customParameter = this.GetUrlParameter(request, "param"); //用户自定义参数。
                        FastUploadEventArgs e = new FastUploadEventArgs(clientFileName, clientCheckCode, customParameter);
                        this.FastUpload(e);
                        string JSON = System.Text.Json.JsonSerializer.Serialize(new Thinksea.Net.FileUploader.Result()
                        {
                            ErrorCode = 0,
                            Data = e.ResultData,//将需要返回到客户端的数据写回客户端。
                        });
                        return JSON;
                    }
                case "getoffset": //客户端获取断点续传起始位置。
                    {
                        string clientFileName = this.GetUrlParameter(request, "filename"); //客户端文件名。
                        long fileSize = long.Parse(this.GetUrlParameter(request, "filesize")); //文件大小。
                        string sCheckCode = this.GetUrlParameter(request, "checkcode"); //文件完整性校验码，如果设置了此项参数，则在文件上传完成时执行文件完整性校验。
                        byte[] clientCheckCode = Thinksea.General.HexString2Bytes(sCheckCode);
                        string customParameter = this.GetUrlParameter(request, "param"); //用户自定义参数。
                        string tempFile = this.GetTempFile(clientCheckCode); //上传临时存盘文件名。
                        long p = this.GetContinueUploadPosition(tempFile);
                        string JSON = System.Text.Json.JsonSerializer.Serialize(new Thinksea.Net.FileUploader.Result()
                        {
                            ErrorCode = 0,
                            Data = p,
                        });
                        //response.Write(p);
                        //_httpContext.Response.Clear();
                        //_httpContext.Response.End();
                        return JSON;
                    }
                default: //上传文件内容。
                    {
                        #region 解析查询参数。
                        string clientFileName = this.GetUrlParameter(request, "filename"); //客户端文件名。
                        string sFileSize = this.GetUrlParameter(request, "filesize"); //文件大小。
                        long fileSize = string.IsNullOrEmpty(sFileSize) ? 0 : long.Parse(sFileSize);
                        string sCheckCode = this.GetUrlParameter(request, "checkcode"); //文件完整性校验码，如果设置了此项参数，则在文件上传完成时执行文件完整性校验。
                        byte[] clientCheckCode = string.IsNullOrEmpty(sCheckCode) ? null : Thinksea.General.HexString2Bytes(sCheckCode);
                        string customParameter = this.GetUrlParameter(request, "param"); //用户自定义参数。
                        string sOffset = this.GetUrlParameter(request, "offset"); //上传数据起始偏移地址。
                        long startByte = string.IsNullOrEmpty(sOffset) ? 0 : long.Parse(sOffset);
                        #endregion

#if NETFRAMEWORK
                        var uploadFiles = request.Files;
#elif NETCOREAPP
                        if (!request.HasFormContentType)
                        {
                            return System.Text.Json.JsonSerializer.Serialize(new Thinksea.Net.FileUploader.Result()
                            {
                                ErrorCode = 1,
                                Message = "未上传文件内容。"
                            });
                        }
                        var uploadFiles = request.Form.Files;
#endif
                        if (uploadFiles.Count > 0) //如果采用标准的网页表单格式上传文件数据。
                        {
                            var httpPostedFile = uploadFiles[0];
                            if (string.IsNullOrEmpty(clientFileName))
                            {
                                clientFileName = httpPostedFile.FileName;
                            }
                            if (string.IsNullOrEmpty(sFileSize))
                            {
#if NETFRAMEWORK
                                fileSize = httpPostedFile.ContentLength;
#elif NETCOREAPP
                                fileSize = httpPostedFile.Length;
#endif
                            }
                        }
                        else
                        {
                            return System.Text.Json.JsonSerializer.Serialize(new Thinksea.Net.FileUploader.Result()
                            {
                                ErrorCode = 1,
                                Message = "未上传文件内容。"
                            });
                        }

                        string tempFile = this.GetTempFile(clientCheckCode); //上传临时存盘文件名。

                        this.BeginUpload(new FileUploadEventArgs(tempFile, clientFileName, customParameter, startByte));

                        //如果是第一次上传数据则删除同名文件。
                        if (startByte == 0)
                        {
#if DEBUG
                            System.Diagnostics.Debug.WriteLine("准备写入第一个数据块。");
#endif

                            //删除临时文件。
                            if (System.IO.File.Exists(tempFile))
                            {
                                System.IO.File.Delete(tempFile);
                            }

                        }

#if DEBUG
                        System.Diagnostics.Debug.WriteLine(string.Format("写数据到磁盘目录: {0}", this.FileUploadTempDirectory));
#endif

                        bool updateFinish = false;
                        byte[] serverCheckCode = null;
                        long writeDataSize = 0;
                        System.IO.FileStream outputStream = System.IO.File.Open(tempFile, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None);//System.IO.IOException
                        try
                        {
                            outputStream.Position = startByte;

#if NETFRAMEWORK
                            System.IO.Stream inputStream;
                            if (uploadFiles.Count > 0) //如果采用标准的网页表单格式上传文件数据。
                            {
                                var httpPostedFile = uploadFiles[0];
                                inputStream = httpPostedFile.InputStream;
                            }
                            else //如果非表单（即所有POST发送的内容原文既是文件数据）
                            {
                                inputStream = request.InputStream;
                            }
                            writeDataSize = this.SaveFile(inputStream, outputStream);
#elif NETCOREAPP
                            System.IO.Stream inputStream;
                            if (uploadFiles.Count > 0) //如果采用标准的网页表单格式上传文件数据。
                            {
                                var httpPostedFile = uploadFiles[0];
                                using (inputStream = httpPostedFile.OpenReadStream())
                                {
                                    writeDataSize = this.SaveFile(inputStream, outputStream);
                                }
                            }
                            else //如果非表单（即所有POST发送的内容原文既是文件数据）
                            {
                                using (inputStream = request.Body)
                                {
                                    writeDataSize = this.SaveFile(inputStream, outputStream);
                                }
                            }
#endif

#if DEBUG
                            System.Diagnostics.Debug.WriteLine("写数据到磁盘成功。");
#endif

                            if (outputStream.Position == fileSize) //如果上传完成。
                            {
#if DEBUG
                                System.Diagnostics.Debug.WriteLine("写入最后一个数据块完成。");
#endif

                                #region 对比文件完整性校验码是否相同。
                                long p = outputStream.Position;
                                try
                                {
                                    outputStream.Position = 0;
                                    serverCheckCode = this.GetCheckCode(outputStream);
                                }
                                finally
                                {
                                    outputStream.Position = p;
                                }
                                if (clientCheckCode != null && !Thinksea.General.CompareArray(clientCheckCode, serverCheckCode))
                                {
                                    outputStream.Close();
                                    outputStream.Dispose();
                                    outputStream = null;
                                    if (System.IO.File.Exists(tempFile))
                                    {
                                        System.IO.File.Delete(tempFile);
                                    }
                                    throw new System.Exception("上传失败，未通过最后的文件校验过程。");
                                }
                                #endregion
                                updateFinish = true;
                            }
                            else
                            {
                                updateFinish = false;
                            }
                        }
                        finally
                        {
                            if (outputStream != null)
                            {
                                outputStream.Close();
                                outputStream.Dispose();
                                outputStream = null;
                            }
                        }

                        string JSON;
                        if (updateFinish)
                        {
                            UploadFinishedEventArgs finishedFileUploadEventArgs = new UploadFinishedEventArgs(tempFile, clientFileName, serverCheckCode, customParameter);
                            this.UploadFinished(finishedFileUploadEventArgs);
                            //string targetFile = this.GetTargetFile(finishedFileUploadEventArgs.ClientFileName);
                            //if (finishedFileUploadEventArgs.ServerFile != targetFile)
                            //{
                            //    if (System.IO.File.Exists(targetFile))
                            //    {
                            //        System.IO.File.Delete(targetFile);
                            //    }
                            //    System.IO.File.Move(finishedFileUploadEventArgs.ServerFile, targetFile);//将临时文件名更改为目的文件名。
                            //}

                            //return finishedFileUploadEventArgs.ResultData;
                            JSON = System.Text.Json.JsonSerializer.Serialize(new Thinksea.Net.FileUploader.Result()
                            {
                                ErrorCode = 0,
                                Data = finishedFileUploadEventArgs.ResultData,//将需要返回到客户端的数据写回客户端。
                            });
                        }
                        else
                        {
                            JSON = System.Text.Json.JsonSerializer.Serialize(new Thinksea.Net.FileUploader.Result()
                            {
                                ErrorCode = 0,
                            });
                        }
                        return JSON;
                    }
            }
        }

        /// <summary>
        /// 当客户端请求尝试秒传文件时调用此方法。
        /// </summary>
        /// <param name="e">秒传需要的数据。</param>
        public virtual void FastUpload(FastUploadEventArgs e)
        {

        }

        /// <summary>
        /// 每次与客户端建立连接准备上传文件时调用此方法。注意：一个文件的上传过程可能会多次调用此方法。
        /// </summary>
        public virtual void BeginUpload(FileUploadEventArgs e)
        {
        }

        /// <summary>
        /// 当文件上传完成时调用此方法。
        /// </summary>
        public abstract void UploadFinished(UploadFinishedEventArgs e);

    }

    /// <summary>
    /// 文件秒传事件参数。
    /// </summary>
    public class FastUploadEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取客户端文件名。
        /// </summary>
        public string ClientFileName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取客户端提交的自定义参数。
        /// </summary>
        public string Parameters
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取文件完整性校验码。
        /// </summary>
        public byte[] CheckCode
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取或设置需要返回到客户端的数据。
        /// </summary>
        public object ResultData
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="clientFileName">客户端文件名。</param>
        /// <param name="checkCode">文件完整性校验码。</param>
        /// <param name="parameters">客户端提交的自定义参数。</param>
        public FastUploadEventArgs(string clientFileName, byte[] checkCode, string parameters)
        {
            this.ClientFileName = clientFileName;
            this.CheckCode = checkCode;
            this.Parameters = parameters;
        }

    }

    /// <summary>
    /// 文件上传事件参数。
    /// </summary>
    public class FileUploadEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取上传到服务器的文件全名。
        /// </summary>
        public string ServerFile
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取客户端文件名。
        /// </summary>
        public string ClientFileName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取客户端提交的自定义参数。
        /// </summary>
        public string Parameters
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取传输数据起始位置。
        /// </summary>
        public long StartPosition
        {
            get;
            private set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="serverFile">上传到服务器的文件全名。</param>
        /// <param name="clientFileName">客户端文件名。</param>
        /// <param name="parameters">客户端提交的自定义参数。</param>
        /// <param name="startPosition">传输数据起始位置。</param>
        public FileUploadEventArgs(string serverFile, string clientFileName, string parameters, long startPosition)
        {
            this.StartPosition = startPosition;
            this.ServerFile = serverFile;
            this.ClientFileName = clientFileName;
            this.Parameters = parameters;
        }

    }

    /// <summary>
    /// 文件上传完成事件参数。
    /// </summary>
    public class UploadFinishedEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取上传到服务器的文件全名。
        /// </summary>
        public string ServerFile
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取客户端文件名。
        /// </summary>
        public string ClientFileName
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取客户端提交的自定义参数。
        /// </summary>
        public string Parameters
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取文件完整性校验码。
        /// </summary>
        public byte[] CheckCode
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取或设置需要返回到客户端的数据。
        /// </summary>
        [System.Text.Json.Serialization.JsonInclude()]
        public object ResultData
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="serverFile">上传到服务器的文件全名。</param>
        /// <param name="clientFileName">客户端文件名。</param>
        /// <param name="checkCode">文件完整性校验码。</param>
        /// <param name="parameters">客户端提交的自定义参数。</param>
        public UploadFinishedEventArgs(string serverFile, string clientFileName, byte[] checkCode, string parameters)
        {
            this.ServerFile = serverFile;
            this.ClientFileName = clientFileName;
            this.CheckCode = checkCode;
            this.Parameters = parameters;
        }

    }
}
