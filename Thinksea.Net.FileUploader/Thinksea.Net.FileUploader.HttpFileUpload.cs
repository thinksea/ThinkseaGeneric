using System.Net.Http;

namespace Thinksea.Net.FileUploader
{
    /// <summary>
    /// 封装了基于 HTTP 协议的文件上传客户端功能。
    /// </summary>
    public class HttpFileUpload : System.IDisposable, Thinksea.Net.FileUploader.IFileUpload
    {
        #region 变量/属性。
        /// <summary>
        /// 声明一个 HTTP 通信对象实例。
        /// </summary>
        /// <remarks>
        /// 注意：依据微软官方文档说明，同一应用程序范围内仅允许初始化一次。否则在高用户并发下将耗尽网络接口资源引发异常。
        /// <see href="https://docs.microsoft.com/zh-cn/dotnet/api/system.net.http.httpclient?view=net-6.0"/>
        /// </remarks>
        private static readonly System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();

        /// <summary>
        /// 缓冲区大小。
        /// </summary>
        private int BufferSize;

        private System.IO.Stream _FileStream = null;
        /// <summary>
        /// 获取或设置文件数据流。
        /// </summary>
        private System.IO.Stream FileStream
        {
            get
            {
                return this._FileStream;
            }
            set
            {
                if (this._FileStream != value)
                {
                    this._FileStream = value;
                    this.CheckCode = null;
                }
            }
        }

        /// <summary>
        /// 获取或设置文件名。
        /// </summary>
        private string FileName = "";

        /// <summary>
        /// 获取或设置文件大小。
        /// </summary>
        private long FileSize = 0;

        /// <summary>
        /// 获取或设置已经成功上传的数据大小。
        /// </summary>
        private long BytesUploaded = 0;

        /// <summary>
        /// 记录与服务器建立一次通信过程中成功上传的数据大小。
        /// </summary>
        private long TempUploadDataSize = 0;

        /// <summary>
        /// 获取文件完整性校验码。
        /// </summary>
        private byte[] CheckCode = null;

        /// <summary>
        /// 获取或设置自定义参数。
        /// </summary>
        private string CustomParameter;

        /// <summary>
        /// 获取或设置每次与上传服务器建立连接后允许发送的最大数据量。
        /// </summary>
        private int ChunkSize;

        private string _UploadServiceUrl;
        /// <summary>
        /// 获取或设置文件上传服务地址。
        /// </summary>
        public string UploadServiceUrl
        {
            get
            {
                return this._UploadServiceUrl;
            }
            set
            {
                this._UploadServiceUrl = value;
            }
        }

        /// <summary>
        /// 获取或设置一个标记，指示是否取消上传任务。
        /// </summary>
        private bool Cancelling = false;

        /// <summary>
        /// 获取或设置一个标记，指示是否已经完成上传。
        /// </summary>
        private bool Finished = false;

        /// <summary>
        /// 创建一个取消令牌源
        /// </summary>
        System.Threading.CancellationTokenSource cancellationTokenSource = null;
        //// 设置取消令牌的超时时间（例如：5秒）
        //cancellationTokenSource.CancelAfter(TimeSpan.FromSeconds(5));
        #endregion

        #region 事件。
        private event BeginUploadEventHandler _BeginUpload;
        /// <summary>
        /// 当开始上传文件时引发此事件。
        /// </summary>
        [System.Obsolete]
        public event BeginUploadEventHandler BeginUpload
        {
            add
            {
                this._BeginUpload += value;
            }
            remove
            {
                this._BeginUpload -= value;
            }
        }

        private event BreakpointUploadEventHandler _FindBreakpoint;
        /// <summary>
        /// 当发现可用的断点上传信息时引发此事件。
        /// </summary>
        public event BreakpointUploadEventHandler FindBreakpoint
        {
            add
            {
                this._FindBreakpoint += value;
            }
            remove
            {
                this._FindBreakpoint -= value;
            }
        }

        private event UploadProgressChangedEventHandler _UploadProgressChanged;
        /// <summary>
        /// 当上传进度更改时引发此事件。
        /// </summary>
        public event UploadProgressChangedEventHandler UploadProgressChanged
        {
            add
            {
                this._UploadProgressChanged += value;
            }
            remove
            {
                this._UploadProgressChanged -= value;
            }
        }

        private event UploadErrorEventHandler _ErrorOccurred;
        /// <summary>
        /// 当出现错误时引发此事件。
        /// </summary>
        public event UploadErrorEventHandler ErrorOccurred
        {
            add
            {
                this._ErrorOccurred += value;
            }
            remove
            {
                this._ErrorOccurred -= value;
            }
        }

        private event AbortEventHandler _OnAbort;
        /// <summary>
        /// 当上传中止时引发此事件。
        /// </summary>
        public event AbortEventHandler OnAbort
        {
            add
            {
                this._OnAbort += value;
            }
            remove
            {
                this._OnAbort -= value;
            }
        }

        #endregion

        #region 方法。
        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public HttpFileUpload()
        {
            this.BufferSize = 8192;
            this.ChunkSize = 256 * this.BufferSize;
        }

        /// <summary>
        /// 开始上传指定文件。
        /// </summary>
        /// <param name="fileStream">文件数据流。</param>
        /// <param name="fileName">文件名。</param>
        /// <param name="customParameter">自定义参数。</param>
        public async System.Threading.Tasks.Task StartUploadAsync(System.IO.Stream fileStream, string fileName, string customParameter)
        {
            await this.StartUploadAsync(fileStream, fileName, customParameter, this.BufferSize, this.ChunkSize);
        }

        /// <summary>
        /// 开始上传指定文件。
        /// </summary>
        /// <param name="fileStream">文件数据流。</param>
        /// <param name="fileName">文件名。</param>
        /// <param name="customParameter">自定义参数。</param>
        /// <param name="bufferSize">缓冲区大小。</param>
        /// <param name="chunkSize">每次与上传服务器建立连接后允许发送的最大数据量。</param>
        public async System.Threading.Tasks.Task StartUploadAsync(System.IO.Stream fileStream, string fileName, string customParameter, int bufferSize, int chunkSize)
        {
            if (fileStream == null)
            {
                throw new System.ArgumentNullException(nameof(fileStream), "参数“" + nameof(fileStream) + "”不能为 null。");
                //return;
            }
            if (this.cancellationTokenSource == null)
            {
                this.cancellationTokenSource = new System.Threading.CancellationTokenSource();
            }
            if (this.Finished || this.Cancelling) //如果已经完成上传，或者取消上传过程还未结束，则无法继续上传，直接结束流程。
            {
                return;
            }
            this.FileStream = fileStream;
            this.FileName = fileName;
            this.FileSize = fileStream.Length;
            this.CustomParameter = customParameter;
            this.BufferSize = bufferSize;
            this.ChunkSize = chunkSize;
            this.Cancelling = false;

            //System.Threading.Thread thread = null;
            //if (thread == null)
            //{
            //    thread = new System.Threading.Thread(new System.Threading.ThreadStart(this.StartBreakpointUpload));
            //    thread.Start();
            //}
            #region 获取文件完整性校验码，例如 SHA1 或 MD5 等。
            if (this._FileStream == null)
            {
                this.Cancelling = false;
                return;
            }
            if (this.Cancelling)
            {
                this.Cancelling = false;
                if (this._OnAbort != null)
                {
                    this._OnAbort(this, new AbortEventArgs(this.CustomParameter));
                }
                return;
            }
            if (this.CheckCode == null) //解决避免重复计算文件校验码，当重复调用方法“StartUpload”（例如重新启动这个文件上传任务）时。
            {
                long p = this._FileStream.Position;
                try
                {
                    try
                    {
                        this._FileStream.Position = 0;
                        this.CheckCode = await this.GetCheckCode(this._FileStream);
                    }
                    //catch (System.Threading.Tasks.TaskCanceledException tcex)
                    //{
                    //    if (tcex.CancellationToken.IsCancellationRequested || this.Cancelling)
                    //    {
                    //        this.cancellationTokenSource.Dispose();
                    //        this.cancellationTokenSource = null;

                    //        this.Cancelling = false;
                    //        if (this._OnAbort != null)
                    //        {
                    //            this._OnAbort(this, new AbortEventArgs(this.CustomParameter));
                    //        }
                    //        return;
                    //    }
                    //    throw;
                    //}
                    catch (System.OperationCanceledException ocex)
                    {
						if (ocex.CancellationToken.IsCancellationRequested || this.Cancelling)
						{
							this.cancellationTokenSource.Dispose();
							this.cancellationTokenSource = null;

							this.Cancelling = false;
							if (this._OnAbort != null)
							{
								this._OnAbort(this, new AbortEventArgs(this.CustomParameter));
							}
							return;
						}
						throw;
					}
					catch
                    {
                        if (this.cancellationTokenSource.IsCancellationRequested || this.Cancelling)
                        {
                            this.cancellationTokenSource.Dispose();
                            this.cancellationTokenSource = null;

                            this.Cancelling = false;
                            if (this._OnAbort != null)
                            {
                                this._OnAbort(this, new AbortEventArgs(this.CustomParameter));
                            }
                            return;
                        }
                        throw;
                    }
                }
                finally
                {
                    this._FileStream.Position = p;
                }
            }
            #endregion

            //if (this._BeforeUpload != null)
            //{
            //    this._BeforeUpload(this, new BeforeUploadEventArgs(this.CheckCode));
            //}
            await this.StartFastUploadAsync();
        }

        /// <summary>
        /// 异步获取文件完整性校验码，例如 SHA1 或 MD5 等。
        /// </summary>
        /// <param name="stream">文件流。</param>
        /// <returns>文件完整性校验码。</returns>
        public virtual async System.Threading.Tasks.Task<byte[]> GetCheckCode(System.IO.Stream stream)
        {
            //return await Thinksea.General.GetSHA1Async(stream); //获取SHA1码。

            byte[] b;
            using (System.Security.Cryptography.SHA1 sha1 = System.Security.Cryptography.SHA1.Create())
            {
                //try
                //{
#if NETCOREAPP
                    b = await sha1.ComputeHashAsync(stream, this.cancellationTokenSource.Token);
#else //#elif NETFRAMEWORK
                b = await System.Threading.Tasks.Task.Run(() =>
                {
                    return sha1.ComputeHash(stream);
                }, this.cancellationTokenSource.Token);
#endif
                //}
                //finally
                //            {
                //                sha1.Clear(); // 清除哈希算法内部状态，这样可以确保下一次计算哈希值时是从初始状态开始的。
                //}
            }
            return b;
        }

        #region 秒传方法。
        /// <summary>
        /// 创建一个 <see cref="System.Net.Http.HttpRequestMessage"/> 对象实例，并用指定的数据填充。
        /// </summary>
        /// <param name="endpoint">一个 URI 资源地址。</param>
        /// <param name="data">提交的数据。</param>
        /// <returns>一个 <see cref="System.Net.Http.HttpRequestMessage"/> 对象实例。</returns>
        private static System.Net.Http.HttpRequestMessage CreateHttpRequestMessage(System.Uri endpoint, string data)
        {
            System.Net.Http.HttpRequestMessage requestMessage = new System.Net.Http.HttpRequestMessage();
            requestMessage.RequestUri = endpoint;
            //post请求
            requestMessage.Method = System.Net.Http.HttpMethod.Post;
            //****************************************************//requestMessage.Content.Headers.ContentLength = buf.Length;
            //requestMessage.Timeout = 5000;
            //wc.Encoding = System.Text.Encoding.UTF8;
            System.Net.Http.HttpContent content = new System.Net.Http.StringContent(data, System.Text.Encoding.UTF8);
            //content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded; charset=utf-8");
            content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/x-www-form-urlencoded")
            {
                CharSet = "utf-8"
            };
            requestMessage.Content = content;

            requestMessage.Headers.Add("Pragma", "no-cache");
            requestMessage.Headers.Add("Cache-Control", "no-cache");
            //requestMessage.MaximumAutomaticRedirections = 1;
            //requestMessage.AllowAutoRedirect = true;

            return requestMessage;
        }

        /// <summary>
        /// 开始上传文件，支持秒传。
        /// </summary>
        private async System.Threading.Tasks.Task StartFastUploadAsync()
        {
            if (this.Cancelling)
            {
                this.Cancelling = false;
                if (this._OnAbort != null)
                {
                    this._OnAbort(this, new AbortEventArgs(this.CustomParameter));
                }
                return;
            }
            {
                System.UriBuilder httpHandlerUrlBuilder = new System.UriBuilder(UploadServiceUrl);
                httpHandlerUrlBuilder.Query = string.Format("{0}cmd=fastupload&filename={1}&filesize={2}&checkcode={3}&param={4}"
                    , string.IsNullOrEmpty(httpHandlerUrlBuilder.Query) ? "" : httpHandlerUrlBuilder.Query.Remove(0, 1) + "&"
                    , System.Uri.EscapeDataString(this.FileName)
                    , this.FileSize
                    , System.Uri.EscapeDataString(Thinksea.General.Bytes2HexString(this.CheckCode))
                    , System.Uri.EscapeDataString(this.CustomParameter)
                );

                try
                {
                    using (var requestMessage = CreateHttpRequestMessage(httpHandlerUrlBuilder.Uri, ""))
                    {
                        var responseMessage = await httpClient.SendAsync(requestMessage);

                        responseMessage.EnsureSuccessStatusCode();
                        var statusCode = responseMessage.StatusCode;
                        var content = await responseMessage.Content.ReadAsStringAsync(); //获得接口返回值

                        if (statusCode == System.Net.HttpStatusCode.OK) //如果执行成功。注意：这里假定服务器端成功响应时返回 HTTP 200 错误码，否则请使用“if(responseMessage.IsSuccessStatusCode)”判定是否成功执行。
                        {
                            Thinksea.Net.FileUploader.Result result = System.Text.Json.JsonSerializer.Deserialize<Thinksea.Net.FileUploader.Result>(content);
                            if (result.ErrorCode != 0)
                            {
                                this.OnError("上传出错。详细信息：" + result.Message, null);
                                return;
                            }

                            if (result.Data != null) //断点续传成功
                            {
                                this.BytesUploaded = this.FileSize;
                                this.Finished = true;
                                this.OnUploadProgressChanged(result.Data);
                            }
                            else //无法秒传，开始尝试断点续传。
                            {
                                await this.StartBreakpointUploadAsync();
                            }
                        }
                        else
                        {

                            //if (statusCode == System.Net.HttpStatusCode.NotFound)
                            //{
                            //    throw new Thinksea.OAuth2.Client.ProtocolResponseException(content, statusCode, "NotFound", "HTTP Error 404. The requested resource is not found.", "");
                            //    //throw new System.Net.WebException("", System.Net.WebExceptionStatus.ProtocolError);
                            //}

                            throw new System.Net.ProtocolViolationException("HTTP Error " + ((int)statusCode).ToString() + ".");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    this.Cancelling = false;
                    this.OnError(string.Format("文件“{0}”上传出错。", this.FileName), ex);
                }

                //System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(httpHandlerUrlBuilder.Uri);
                //webRequest.Method = "POST";
                //webRequest.ContentType = "text/xml";
                //webRequest.ContentLength = 0;
                ////获取服务器端返回的信息。
                //webRequest.BeginGetResponse(new System.AsyncCallback(this.FastUpload_ReadHttpResponseCallback), webRequest);
            }
        }

        #endregion

        #region 断点续传方法。
        /// <summary>
        /// 开始上传文件，支持断点续传。
        /// </summary>
        private async System.Threading.Tasks.Task StartBreakpointUploadAsync()
        {
            if (this.Cancelling)
            {
                this.Cancelling = false;
                if (this._OnAbort != null)
                {
                    this._OnAbort(this, new AbortEventArgs(this.CustomParameter));
                }
                return;
            }
            {
                System.UriBuilder httpHandlerUrlBuilder = new System.UriBuilder(UploadServiceUrl);
                httpHandlerUrlBuilder.Query = string.Format("{0}cmd=getoffset&filename={1}&filesize={2}&checkcode={3}&param={4}"
                    , string.IsNullOrEmpty(httpHandlerUrlBuilder.Query) ? "" : httpHandlerUrlBuilder.Query.Remove(0, 1) + "&"
                    , System.Uri.EscapeDataString(this.FileName)
                    , this.FileSize
                    , System.Uri.EscapeDataString(Thinksea.General.Bytes2HexString(this.CheckCode))
                    , System.Uri.EscapeDataString(this.CustomParameter)
                    );

                try
                {
                    using (var requestMessage = CreateHttpRequestMessage(httpHandlerUrlBuilder.Uri, ""))
                    {
                        var responseMessage = await httpClient.SendAsync(requestMessage);

                        responseMessage.EnsureSuccessStatusCode();
                        var statusCode = responseMessage.StatusCode;
                        var content = await responseMessage.Content.ReadAsStringAsync(); //获得接口返回值

                        if (statusCode == System.Net.HttpStatusCode.OK) //如果执行成功。注意：这里假定服务器端成功响应时返回 HTTP 200 错误码，否则请使用“if(responseMessage.IsSuccessStatusCode)”判定是否成功执行。
                        {
                            Thinksea.Net.FileUploader.Result result = System.Text.Json.JsonSerializer.Deserialize<Thinksea.Net.FileUploader.Result>(content);
                            long breakpoint;
                            //long breakpoint = System.Convert.ToInt64(content);
                            if (result.Data is System.Text.Json.JsonElement element)
                            {
                                breakpoint = element.GetInt64();
                            }
                            else
                            {
                                breakpoint = System.Convert.ToInt64(result.Data);
                            }
                            if (this._FindBreakpoint != null && breakpoint != 0)
                            {
                                BreakpointUploadEventArgs p = new BreakpointUploadEventArgs(breakpoint, this.CustomParameter);
                                this._FindBreakpoint(this, p);
                                this.BytesUploaded = p.Breakpoint;
                            }
                            else
                            {
                                this.BytesUploaded = breakpoint;
                            }

                            this.OnBeginUpload();

                            await this.StartUploadContentAsync();//开始上传数据。
                        }
                        else
                        {

                            //if (statusCode == System.Net.HttpStatusCode.NotFound)
                            //{
                            //    throw new Thinksea.OAuth2.Client.ProtocolResponseException(content, statusCode, "NotFound", "HTTP Error 404. The requested resource is not found.", "");
                            //    //throw new System.Net.WebException("", System.Net.WebExceptionStatus.ProtocolError);
                            //}

                            throw new System.Net.ProtocolViolationException("HTTP Error " + ((int)statusCode).ToString() + ".");
                        }
                    }
                }
                catch (System.Exception ex)
                {
                    //this.Canceled = true;
                    this.OnError(string.Format("文件“{0}”上传出错。", this.FileName), ex);
                }

                //System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(httpHandlerUrlBuilder.Uri);
                //webRequest.Method = "POST";
                //webRequest.ContentType = "text/xml";
                //webRequest.ContentLength = 0;
                ////获取服务器端返回的信息。
                //webRequest.BeginGetResponse(new System.AsyncCallback(BreakpointUpload_ReadHttpResponseCallback), webRequest);
            }
        }

        #endregion

        /// <summary>
        /// 开始上传文件内容。
        /// </summary>
        private async System.Threading.Tasks.Task StartUploadContentAsync()
        {
        uploadNextBlock:
            if (this.Cancelling)
            {
                this.Cancelling = false;
                if (this._OnAbort != null)
                {
                    this._OnAbort(this, new AbortEventArgs(this.CustomParameter));
                }
                return;
            }

            System.UriBuilder httpHandlerUrlBuilder = new System.UriBuilder(UploadServiceUrl);
            httpHandlerUrlBuilder.Query = string.Format("{0}cmd=upload&filename={1}&filesize={2}&offset={3}&checkcode={4}&param={5}"
                , string.IsNullOrEmpty(httpHandlerUrlBuilder.Query) ? "" : httpHandlerUrlBuilder.Query.Remove(0, 1) + "&"
                , System.Uri.EscapeDataString(this.FileName)
                , this.FileSize
                , this.BytesUploaded
                , System.Uri.EscapeDataString(Thinksea.General.Bytes2HexString(this.CheckCode))
                , System.Uri.EscapeDataString(this.CustomParameter)
                );

            try
            {
                //if (this._FileStream == null || this.Cancelling)
                //{
                //    this.Cancelling = false;
                //    return;
                //}

                #region 组织文件表单。
                // 实例化HttpRequestMessage对象
                //System.Net.Http.HttpRequestMessage requestMessage = new System.Net.Http.HttpRequestMessage();
                //// 设定请求地址
                //requestMessage.RequestUri = httpHandlerUrlBuilder.Uri;
                ////post请求
                //requestMessage.Method = System.Net.Http.HttpMethod.Post;

                //webRequest.ContentType = "multipart/form-data;charset=utf-8;boundary=----WebKitFormBoundary" + System.Guid.NewGuid().ToString("N");
                //System.Net.Http.MultipartFormDataContent mulContent = new System.Net.Http.MultipartFormDataContent();
                //mulContent.Add(null, null, null);
                string boundary = "--WebKitFormBoundary" + System.DateTime.Now.Ticks.ToString("X"); // 随机分隔线

                byte[] itemBoundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "\r\n");
                byte[] endBoundaryBytes = System.Text.Encoding.UTF8.GetBytes("\r\n--" + boundary + "--\r\n");

                string fileName = this.FileName;

                //请求头部信息 
                System.Text.StringBuilder sbHeader = new System.Text.StringBuilder(string.Format("Content-Disposition:form-data;name=\"file\";filename=\"{0}\"\r\nContent-Type:application/octet-stream\r\n\r\n", Thinksea.Web.ConvertToJavaScriptString(fileName)));
                byte[] postHeaderBytes = System.Text.Encoding.UTF8.GetBytes(sbHeader.ToString());
                #endregion

                string responsestring;
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    ms.Write(itemBoundaryBytes, 0, itemBoundaryBytes.Length);
                    ms.Write(postHeaderBytes, 0, postHeaderBytes.Length);

                    #region 写入文件内容。
                    byte[] buffer = new byte[this.BufferSize];
                    int bytesRead = 0;
                    this.TempUploadDataSize = 0;

                    //设置文件流读取位置。
                    this._FileStream.Position = this.BytesUploaded;

                    //读取数据
                    while ((bytesRead = this._FileStream.Read(buffer, 0, buffer.Length)) != 0
                        && this.TempUploadDataSize + bytesRead <= this.ChunkSize
                        && !this.Cancelling)
                    {
                        ms.Write(buffer, 0, bytesRead);
                        ms.Flush();

                        this.TempUploadDataSize += bytesRead;
                    }
                    #endregion

                    ms.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
                    ms.Seek(0, System.IO.SeekOrigin.Begin); //将指针设置到流的开头，否则后面代码访问不到完整的文件数据。

                    // 实例化multipart表单模型
                    using (var formData = new MultipartFormDataContent(boundary))
                    {

                        formData.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("multipart/form-data;charset=utf-8;boundary=" + boundary);
                        //formData.Headers.Remove("Content-Type");
                        //formData.Headers.Add("ContentType", "multipart/form-data;charset=utf-8;boundary=" + boundary);
                        formData.Add(new StreamContent(ms));
                        //requestMessage.Content = formData;

                        //var response = await httpClient.SendAsync(requestMessage);
                        var response = await httpClient.PostAsync(httpHandlerUrlBuilder.Uri, formData);
                        responsestring = await response.Content.ReadAsStringAsync();
                    }
                }
                if (this.Cancelling)
                {
                    this.Cancelling = false;
                    if (this._OnAbort != null)
                    {
                        this._OnAbort(this, new AbortEventArgs(this.CustomParameter));
                    }
                    return;
                }

                #region 获取服务器端返回的信息。检查上传是否成功。
                Thinksea.Net.FileUploader.Result result = System.Text.Json.JsonSerializer.Deserialize<Thinksea.Net.FileUploader.Result>(responsestring);
                if (result.ErrorCode != 0)
                {
                    this.OnError("上传出错。详细信息：" + result.Message, null);
                    return;
                }

                this.BytesUploaded += this.TempUploadDataSize;

                if (this.BytesUploaded >= this.FileSize) //文件上传完成。
                {
                    this.Finished = true;
                }

                this.OnUploadProgressChanged(result.Data);

                if (this.BytesUploaded < this.FileSize)
                {
                    //if (this.Cancelling)
                    //{
                    //    this.Cancelling = false;
                    //    return;
                    //}
                    //await this.StartUploadContentAsync(); //继续上传下一块文件数据。
                    goto uploadNextBlock; //继续上传下一块文件数据。
                }
                #endregion
            }
            catch (System.Exception ex)
            {
                this.Cancelling = false;
                this.OnError("上传出错。", ex);
            }

            //System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(httpHandlerUrlBuilder.Uri);
            //webRequest.Method = "POST";
            //webRequest.BeginGetRequestStream(new System.AsyncCallback(WriteToStreamCallbackAsync), webRequest);
        }

        /// <summary>
        /// 放弃上传文件。
        /// </summary>
        public void CancelUpload()
        {
            if (this.cancellationTokenSource != null)
            {
                this.cancellationTokenSource.Cancel();
            }

            this.Cancelling = true;
        }

        /// <summary>
        /// 通知准备上传文件。
        /// </summary>
        private void OnBeginUpload()
        {
            if (this._BeginUpload != null)
            {
                this._BeginUpload(this, new Thinksea.Net.FileUploader.BeginUploadEventArgs(this.FileSize, this.BytesUploaded, this.CustomParameter));
            }

        }

        /// <summary>
        /// 通知上传进度已经更改。
        /// </summary>
        /// <param name="resultData">需要返回到客户端的数据。</param>
        private void OnUploadProgressChanged(object resultData)
        {
            if (this._UploadProgressChanged != null)
            {
                this._UploadProgressChanged(this, new UploadProgressChangedEventArgs(this.FileSize, this.BytesUploaded, resultData, this.CustomParameter));
            }
        }

        /// <summary>
        /// 通知上传过程已经出现错误。
        /// </summary>
        /// <param name="errorMessage">错误信息。</param>
        /// <param name="exception">引发此错误的更加详细的异常信息实例。默认设置为 null。</param>
        private void OnError(string errorMessage, System.Exception exception)
        {
            if (this._ErrorOccurred != null)
            {
                this._ErrorOccurred(this, new UploadErrorEventArgs(errorMessage + (exception == null ? "" : exception.ToString()), exception, this.CustomParameter));
            }
        }
        #endregion

        #region 实现 IDisposable 接口。
        /// <summary>
        /// 要检测冗余调用
        /// </summary>
        private bool disposedValue = false;

        /// <summary>
        /// 释放占用的资源。
        /// </summary>
        /// <param name="disposing">是否需要释放那些实现 IDisposable 接口的托管对象</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: 释放托管状态(托管对象)。
                    this._FileStream = null;
                    this.CheckCode = null;
                    this._UploadProgressChanged = null;
                    this._ErrorOccurred = null;
                    if (this.cancellationTokenSource != null)
                    {
                        var cts = this.cancellationTokenSource;
                        this.cancellationTokenSource = null;
                        cts.Dispose();
                    }
                }

                // TODO: 释放未托管的资源(未托管的对象)并在以下内容中替代终结器。
                // TODO: 将大型字段设置为 null。

                disposedValue = true;
            }
        }

        /// <summary>
        /// 释放占用的资源。
        /// </summary>
        public void Dispose()
        {
            // 请勿更改此代码。将清理代码放入以上 Dispose(bool disposing) 中。
            Dispose(true);
            // TODO: 如果在以上内容中替代了终结器，则取消注释以下行。
            // GC.SuppressFinalize(this);
        }
        #endregion

    }

    #region 定义代理和事件参数。
    /// <summary>
    /// 定义开始上传文件事件参数。
    /// </summary>
    public class BeginUploadEventArgs : System.EventArgs
    {
        /// <summary>
        /// 文件大小（单位：字节）
        /// </summary>
        public long FileLength
        {
            get;
            private set;
        }

        /// <summary>
        /// 上传起始位置。
        /// </summary>
        public long StartPosition
        {
            get;
            private set;
        }

        /// <summary>
        /// 自定义参数。
        /// </summary>
        public string CustomParameter
        {
            get;
            private set;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="fileLength">文件大小（单位：字节）</param>
        /// <param name="startPosition">上传起始位置。</param>
        /// <param name="customParameter">自定义参数。</param>
        public BeginUploadEventArgs(long fileLength, long startPosition, string customParameter)
        {
            this.FileLength = fileLength;
            this.StartPosition = startPosition;
            this.CustomParameter = customParameter;
        }

    };

    /// <summary>
    /// 上传前事件代理。
    /// </summary>
    /// <param name="sender">事件引发对象。</param>
    /// <param name="e">事件参数。</param>
    public delegate void BeginUploadEventHandler(object sender, BeginUploadEventArgs e);

    /// <summary>
    /// 上传进度更改事件参数。
    /// </summary>
    public class UploadProgressChangedEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取文件大小（单位：字节）
        /// </summary>
        public long FileLength
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取已成功上传的数据大小。
        /// </summary>
        public long FinishedSize
        {
            get;
            private set;
        }

        /// <summary>
        /// 自定义参数。
        /// </summary>
        public string CustomParameter
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取需要返回到客户端的数据。
        /// </summary>
        public object ResultData
        {
            get;
            private set;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="fileLength">数据总大小。</param>
        /// <param name="finishedSize">已成功上传的数据大小。</param>
        /// <param name="resultData">需要返回到客户端的数据。</param>
        /// <param name="customParameter">自定义参数。</param>
        public UploadProgressChangedEventArgs(long fileLength, long finishedSize, object resultData, string customParameter)
        {
            this.FileLength = fileLength;
            this.FinishedSize = finishedSize;
            this.ResultData = resultData;
            this.CustomParameter = customParameter;
        }
    }

    /// <summary>
    /// 上传进度更改事件代理。
    /// </summary>
    /// <param name="sender">事件引发对象。</param>
    /// <param name="e">事件参数。</param>
    public delegate void UploadProgressChangedEventHandler(object sender, UploadProgressChangedEventArgs e);

    /// <summary>
    /// 断点上传信息事件参数。
    /// </summary>
    public class BreakpointUploadEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取或设置断点上传起始位置，设置为0时表示重新上传。
        /// </summary>
        public long Breakpoint
        {
            get;
            set;
        }

        /// <summary>
        /// 自定义参数。
        /// </summary>
        public string CustomParameter
        {
            get;
            private set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="breakpoint">断点上传起始位置。</param>
        /// <param name="customParameter">自定义参数。</param>
        public BreakpointUploadEventArgs(long breakpoint, string customParameter)
        {
            this.Breakpoint = breakpoint;
            this.CustomParameter = customParameter;
        }
    }

    /// <summary>
    /// 断点上传信息事件代理。
    /// </summary>
    /// <param name="sender">事件引发对象。</param>
    /// <param name="e">事件参数。</param>
    public delegate void BreakpointUploadEventHandler(object sender, BreakpointUploadEventArgs e);

    /// <summary>
    /// 上传出错事件参数。
    /// </summary>
    public class UploadErrorEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取错误信息。
        /// </summary>
        public string Message
        {
            get;
            private set;
        }
        /// <summary>
        /// 引发此错误的异常信息如果有，否则返回 null。
        /// </summary>
        public System.Exception Exception
        {
            get;
            private set;
        }

        /// <summary>
        /// 自定义参数。
        /// </summary>
        public string CustomParameter
        {
            get;
            private set;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="errorMessage">错误信息。</param>
        /// <param name="exception">引发此错误的异常信息，或者null。</param>
        /// <param name="customParameter">自定义参数。</param>
        public UploadErrorEventArgs(string errorMessage, System.Exception exception, string customParameter)
        {
            this.Message = ((string.IsNullOrEmpty(errorMessage) && exception != null) ? exception.Message : errorMessage);
            this.Exception = exception;
            this.CustomParameter = customParameter;
        }
    }

    /// <summary>
    /// 上传出错事件代理。
    /// </summary>
    /// <param name="sender">事件引发对象。</param>
    /// <param name="e">事件参数。</param>
    public delegate void UploadErrorEventHandler(object sender, UploadErrorEventArgs e);

    /// <summary>
    /// 定义上传中止事件参数。
    /// </summary>
    public class AbortEventArgs : System.EventArgs
    {
        /// <summary>
        /// 自定义参数。
        /// </summary>
        public string CustomParameter
        {
            get;
            private set;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="customParameter">自定义参数。</param>
        public AbortEventArgs(string customParameter)
        {
            this.CustomParameter = customParameter;
        }
    }

    /// <summary>
    /// 上传出错事件代理。
    /// </summary>
    /// <param name="sender">事件引发对象。</param>
    /// <param name="e">事件参数。</param>
    public delegate void AbortEventHandler(object sender, AbortEventArgs e);

    #endregion

}
