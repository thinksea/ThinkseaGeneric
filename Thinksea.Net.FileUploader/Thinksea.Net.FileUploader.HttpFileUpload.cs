namespace Thinksea.Net.FileUploader
{
    /// <summary>
    /// 封装了基于 HTTP 协议的文件上传客户端功能。
    /// </summary>
    public class HttpFileUpload : System.IDisposable, Thinksea.Net.FileUploader.IFileUpload
    {
        #region 变量/属性。
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
            this.ChunkSize = 512 * this.BufferSize;
        }

        /// <summary>
        /// 开始上传指定文件。（线程同步）
        /// </summary>
        /// <param name="fileStream">文件数据流。</param>
        /// <param name="fileName">文件名。</param>
        /// <param name="customParameter">自定义参数。</param>
        public void StartUploadSync(System.IO.Stream fileStream, string fileName, string customParameter)
        {
            this.StartUploadSync(fileStream, fileName, customParameter, this.BufferSize, this.ChunkSize);
        }

        /// <summary>
        /// 开始上传指定文件。（线程同步）
        /// </summary>
        /// <param name="fileStream">文件数据流。</param>
        /// <param name="fileName">文件名。</param>
        /// <param name="customParameter">自定义参数。</param>
        /// <param name="bufferSize">缓冲区大小。</param>
        /// <param name="chunkSize">每次与上传服务器建立连接后允许发送的最大数据量。</param>
        public void StartUploadSync(System.IO.Stream fileStream, string fileName, string customParameter, int bufferSize, int chunkSize)
        {
            if (fileStream == null)
            {
                throw new System.ArgumentNullException("FileStream", "参数“FileStream”不能为 null。");
                //return;
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
                        this.CheckCode = this.GetCheckCode(this._FileStream);
                    }
                    catch
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
            this.StartFastUploadSync();
        }

        /// <summary>
        /// 开始上传指定文件。
        /// </summary>
        /// <param name="fileStream">文件数据流。</param>
        /// <param name="fileName">文件名。</param>
        /// <param name="customParameter">自定义参数。</param>
        public void StartUpload(System.IO.Stream fileStream, string fileName, string customParameter)
        {
            this.StartUpload(fileStream, fileName, customParameter, this.BufferSize, this.ChunkSize);
        }

        /// <summary>
        /// 开始上传指定文件。
        /// </summary>
        /// <param name="fileStream">文件数据流。</param>
        /// <param name="fileName">文件名。</param>
        /// <param name="customParameter">自定义参数。</param>
        /// <param name="bufferSize">缓冲区大小。</param>
        /// <param name="chunkSize">每次与上传服务器建立连接后允许发送的最大数据量。</param>
        public void StartUpload(System.IO.Stream fileStream, string fileName, string customParameter, int bufferSize, int chunkSize)
        {
            if (fileStream == null)
            {
                throw new System.ArgumentNullException("FileStream", "参数“FileStream”不能为 null。");
                //return;
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
                        this.CheckCode = this.GetCheckCode(this._FileStream);
                    }
                    catch
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
            this.StartFastUpload();
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

        #region 秒传方法。
        /// <summary>
        /// 开始上传文件，支持秒传。（线程同步）
        /// </summary>
        private void StartFastUploadSync()
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
                    , System.Web.HttpUtility.UrlEncode(this.FileName)
                    , this.FileSize
                    , System.Web.HttpUtility.UrlEncode(Thinksea.General.Bytes2HexString(this.CheckCode))
                    , this.CustomParameter
                    );

                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(httpHandlerUrlBuilder.Uri);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml";
                webRequest.ContentLength = 0;
                //获取服务器端返回的信息。
                this.FastUpload_ReadHttpResponseCallbackSync(webRequest);
                webRequest.Abort();
            }
        }

        /// <summary>
        /// 开始上传文件，支持秒传。
        /// </summary>
        private void StartFastUpload()
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
                    , System.Web.HttpUtility.UrlEncode(this.FileName)
                    , this.FileSize
                    , System.Web.HttpUtility.UrlEncode(Thinksea.General.Bytes2HexString(this.CheckCode))
                    , this.CustomParameter
                    );

                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(httpHandlerUrlBuilder.Uri);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml";
                webRequest.ContentLength = 0;
                //获取服务器端返回的信息。
                webRequest.BeginGetResponse(new System.AsyncCallback(this.FastUpload_ReadHttpResponseCallback), webRequest);
            }
        }

        /// <summary>
        /// 接收服务器回执。
        /// </summary>
        /// <param name="webRequest"></param>
        private void FastUpload_ReadHttpResponseCallbackSync(System.Net.HttpWebRequest webRequest)
        {
            try
            {
                string responsestring = "";
                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
                try
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
                    try
                    {
                        responsestring = reader.ReadToEnd();
                    }
                    finally
                    {
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }
                }
                finally
                {
                    webResponse.Close();
                    //webResponse.Dispose();
                    webResponse = null;
                    //webRequest.Abort();
                    //webRequest = null;
                }

                Thinksea.Net.FileUploader.Result result = Newtonsoft.Json.JsonConvert.DeserializeObject<Thinksea.Net.FileUploader.Result>(responsestring);
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
                    this.StartBreakpointUploadSync();
                }
            }
            catch (System.Exception ex)
            {
                this.Cancelling = false;
                this.OnError(string.Format("文件“{0}”上传出错。", this.FileName), ex);
            }

        }

        /// <summary>
        /// 接收服务器回执。
        /// </summary>
        /// <param name="asynchronousResult"></param>
        private void FastUpload_ReadHttpResponseCallback(System.IAsyncResult asynchronousResult)
        {
            try
            {
                string responsestring = "";
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)asynchronousResult.AsyncState;
                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);
                try
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
                    try
                    {
                        responsestring = reader.ReadToEnd();
                    }
                    finally
                    {
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }
                }
                finally
                {
                    webResponse.Close();
                    //webResponse.Dispose();
                    webResponse = null;
                    webRequest.Abort();
                    webRequest = null;
                }

                Thinksea.Net.FileUploader.Result result = Newtonsoft.Json.JsonConvert.DeserializeObject<Thinksea.Net.FileUploader.Result>(responsestring);
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
                    this.StartBreakpointUpload();
                }
            }
            catch (System.Exception ex)
            {
                this.Cancelling = false;
                this.OnError(string.Format("文件“{0}”上传出错。", this.FileName), ex);
            }

        }

        #endregion

        #region 断点续传方法。
        /// <summary>
        /// 开始上传文件，支持断点续传。（线程同步）
        /// </summary>
        private void StartBreakpointUploadSync()
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
                    , System.Web.HttpUtility.UrlEncode(this.FileName)
                    , this.FileSize
                    , System.Web.HttpUtility.UrlEncode(Thinksea.General.Bytes2HexString(this.CheckCode))
                    , this.CustomParameter
                    );

                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(httpHandlerUrlBuilder.Uri);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml";
                webRequest.ContentLength = 0;
                //获取服务器端返回的信息。
                this.BreakpointUpload_ReadHttpResponseCallbackSync(webRequest);
                webRequest.Abort();
            }
        }

        /// <summary>
        /// 开始上传文件，支持断点续传。
        /// </summary>
        private void StartBreakpointUpload()
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
                    , System.Web.HttpUtility.UrlEncode(this.FileName)
                    , this.FileSize
                    , System.Web.HttpUtility.UrlEncode(Thinksea.General.Bytes2HexString(this.CheckCode))
                    , this.CustomParameter
                    );

                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(httpHandlerUrlBuilder.Uri);
                webRequest.Method = "POST";
                webRequest.ContentType = "text/xml";
                webRequest.ContentLength = 0;
                //获取服务器端返回的信息。
                webRequest.BeginGetResponse(new System.AsyncCallback(BreakpointUpload_ReadHttpResponseCallback), webRequest);
            }
        }

        /// <summary>
        /// 接收服务器回执。
        /// </summary>
        /// <param name="webRequest"></param>
        private void BreakpointUpload_ReadHttpResponseCallbackSync(System.Net.HttpWebRequest webRequest)
        {
            try
            {
                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
                try
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
                    try
                    {
                        string responsestring = reader.ReadToEnd();
                        Thinksea.Net.FileUploader.Result result = Newtonsoft.Json.JsonConvert.DeserializeObject<Thinksea.Net.FileUploader.Result>(responsestring);
                        long breakpoint = System.Convert.ToInt64(result.Data);
                        //long breakpoint = System.Convert.ToInt64(responsestring);
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
                    }
                    finally
                    {
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }
                }
                finally
                {
                    webResponse.Close();
                    //webResponse.Dispose();
                    webResponse = null;
                    //webRequest.Abort();
                    //webRequest = null;
                }

                this.OnBeginUpload();

                this.StartUploadContentSync();//开始上传数据。

            }
            catch (System.Exception ex)
            {
                //this.Canceled = true;
                this.OnError(string.Format("文件“{0}”上传出错。", this.FileName), ex);
            }

        }

        /// <summary>
        /// 接收服务器回执。
        /// </summary>
        /// <param name="asynchronousResult"></param>
        private void BreakpointUpload_ReadHttpResponseCallback(System.IAsyncResult asynchronousResult)
        {
            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)asynchronousResult.AsyncState;
                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);
                try
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
                    try
                    {
                        string responsestring = reader.ReadToEnd();
                        Thinksea.Net.FileUploader.Result result = Newtonsoft.Json.JsonConvert.DeserializeObject<Thinksea.Net.FileUploader.Result>(responsestring);
                        long breakpoint = System.Convert.ToInt64(result.Data);
                        //long breakpoint = System.Convert.ToInt64(responsestring);
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
                    }
                    finally
                    {
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }
                }
                finally
                {
                    webResponse.Close();
                    //webResponse.Dispose();
                    webResponse = null;
                    webRequest.Abort();
                    webRequest = null;
                }

                this.OnBeginUpload();

                this.StartUploadContent();//开始上传数据。

            }
            catch (System.Exception ex)
            {
                //this.Canceled = true;
                this.OnError(string.Format("文件“{0}”上传出错。", this.FileName), ex);
            }

        }

        #endregion

        /// <summary>
        /// 开始上传文件内容。（线程同步）
        /// </summary>
        private void StartUploadContentSync()
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
            System.UriBuilder httpHandlerUrlBuilder = new System.UriBuilder(UploadServiceUrl);
            httpHandlerUrlBuilder.Query = string.Format("{0}filename={1}&filesize={2}&offset={3}&checkcode={4}&param={5}"
                , string.IsNullOrEmpty(httpHandlerUrlBuilder.Query) ? "" : httpHandlerUrlBuilder.Query.Remove(0, 1) + "&"
                , System.Web.HttpUtility.UrlEncode(this.FileName)
                , this.FileSize
                , this.BytesUploaded
                , System.Web.HttpUtility.UrlEncode(Thinksea.General.Bytes2HexString(this.CheckCode))
                , this.CustomParameter
                );

            System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(httpHandlerUrlBuilder.Uri);
            webRequest.Method = "POST";
            this.WriteToStreamCallbackSync(webRequest);
        }

        /// <summary>
        /// 开始上传文件内容。
        /// </summary>
        private void StartUploadContent()
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
            System.UriBuilder httpHandlerUrlBuilder = new System.UriBuilder(UploadServiceUrl);
            httpHandlerUrlBuilder.Query = string.Format("{0}filename={1}&filesize={2}&offset={3}&checkcode={4}&param={5}"
                , string.IsNullOrEmpty(httpHandlerUrlBuilder.Query) ? "" : httpHandlerUrlBuilder.Query.Remove(0, 1) + "&"
                , System.Web.HttpUtility.UrlEncode(this.FileName)
                , this.FileSize
                , this.BytesUploaded
                , System.Web.HttpUtility.UrlEncode(Thinksea.General.Bytes2HexString(this.CheckCode))
                , this.CustomParameter
                );

            System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(httpHandlerUrlBuilder.Uri);
            webRequest.Method = "POST";
            webRequest.BeginGetRequestStream(new System.AsyncCallback(WriteToStreamCallback), webRequest);
        }

        /// <summary>
        /// 放弃上传文件。
        /// </summary>
        public void CancelUpload()
        {
            this.Cancelling = true;
        }

        /// <summary>
        /// 发送文件数据到服务器。
        /// </summary>
        /// <param name="webRequest"></param>
        private void WriteToStreamCallbackSync(System.Net.HttpWebRequest webRequest)
        {
            try
            {
                //if (this._FileStream == null || this.Cancelling)
                //{
                //    this.Cancelling = false;
                //    return;
                //}
                System.IO.Stream requestStream = webRequest.GetRequestStream();
                try
                {
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
                        requestStream.Write(buffer, 0, bytesRead);
                        requestStream.Flush();

                        this.TempUploadDataSize += bytesRead;
                    }

                }
                finally
                {
                    requestStream.Close();
                    requestStream.Dispose();
                    requestStream = null;
                }
                if (this.Cancelling)
                {
                    //webRequest.Abort();
                    //webRequest = null;
                    this.Cancelling = false;
                    if (this._OnAbort != null)
                    {
                        this._OnAbort(this, new AbortEventArgs(this.CustomParameter));
                    }
                    return;
                }
                //获取服务器端返回的信息。
                this.ReadHttpResponseCallbackSync(webRequest);
            }
            catch (System.Exception ex)
            {
                this.Cancelling = false;
                this.OnError("上传出错。", ex);
            }
        }

        /// <summary>
        /// 发送文件数据到服务器。
        /// </summary>
        /// <param name="asynchronousResult"></param>
        private void WriteToStreamCallback(System.IAsyncResult asynchronousResult)
        {
            try
            {
                //if (this._FileStream == null || this.Cancelling)
                //{
                //    this.Cancelling = false;
                //    return;
                //}
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)asynchronousResult.AsyncState;
                System.IO.Stream requestStream = webRequest.EndGetRequestStream(asynchronousResult);
                try
                {
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
                        requestStream.Write(buffer, 0, bytesRead);
                        requestStream.Flush();

                        this.TempUploadDataSize += bytesRead;
                    }

                }
                finally
                {
                    requestStream.Close();
                    requestStream.Dispose();
                    requestStream = null;
                }
                if (this.Cancelling)
                {
                    webRequest.Abort();
                    webRequest = null;
                    this.Cancelling = false;
                    if (this._OnAbort != null)
                    {
                        this._OnAbort(this, new AbortEventArgs(this.CustomParameter));
                    }
                    return;
                }
                //获取服务器端返回的信息。
                webRequest.BeginGetResponse(new System.AsyncCallback(ReadHttpResponseCallback), webRequest);
            }
            catch (System.Exception ex)
            {
                this.Cancelling = false;
                this.OnError("上传出错。", ex);
            }
        }

        /// <summary>
        /// 接收服务器回执。
        /// </summary>
        /// <param name="webRequest"></param>
        private void ReadHttpResponseCallbackSync(System.Net.HttpWebRequest webRequest)
        {
            //检查上传是否成功。
            try
            {
                string responsestring = "";
                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.GetResponse();
                try
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
                    try
                    {
                        responsestring = reader.ReadToEnd();
                    }
                    finally
                    {
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }
                }
                finally
                {
                    webResponse.Close();
                    //webResponse.Dispose();
                    webResponse = null;
                    //webRequest.Abort();
                    //webRequest = null;
                }

                Thinksea.Net.FileUploader.Result result = Newtonsoft.Json.JsonConvert.DeserializeObject<Thinksea.Net.FileUploader.Result>(responsestring);
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
                    this.StartUploadContentSync();//继续上传下一块文件数据。
                }
            }
            catch (System.Exception ex)
            {
                this.Cancelling = false;
                this.OnError("上传出错。", ex);
            }

        }

        /// <summary>
        /// 接收服务器回执。
        /// </summary>
        /// <param name="asynchronousResult"></param>
        private void ReadHttpResponseCallback(System.IAsyncResult asynchronousResult)
        {
            //检查上传是否成功。
            try
            {
                string responsestring = "";
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)asynchronousResult.AsyncState;
                System.Net.HttpWebResponse webResponse = (System.Net.HttpWebResponse)webRequest.EndGetResponse(asynchronousResult);
                try
                {
                    System.IO.StreamReader reader = new System.IO.StreamReader(webResponse.GetResponseStream());
                    try
                    {
                        responsestring = reader.ReadToEnd();
                    }
                    finally
                    {
                        reader.Close();
                        reader.Dispose();
                        reader = null;
                    }
                }
                finally
                {
                    webResponse.Close();
                    //webResponse.Dispose();
                    webResponse = null;
                    webRequest.Abort();
                    webRequest = null;
                }

                Thinksea.Net.FileUploader.Result result = Newtonsoft.Json.JsonConvert.DeserializeObject<Thinksea.Net.FileUploader.Result>(responsestring);
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
                    this.StartUploadContent();//继续上传下一块文件数据。
                }
            }
            catch (System.Exception ex)
            {
                this.Cancelling = false;
                this.OnError("上传出错。", ex);
            }

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

        /// <summary>
        /// Track whether Dispose has been called.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// 释放占用的资源。
        /// </summary>
        /// <param name="disposing">是否需要释放那些实现IDisposable接口的托管对象</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    this._FileStream = null;
                    this.CheckCode = null;
                    this._UploadProgressChanged = null;
                    this._ErrorOccurred = null;
                }

                // Note disposing has been done.
                disposed = true;
            }
        }

        /// <summary>
        /// 释放占用的资源。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            System.GC.SuppressFinalize(this);
        }

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
