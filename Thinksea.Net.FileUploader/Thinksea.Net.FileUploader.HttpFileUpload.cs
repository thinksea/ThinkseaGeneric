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
        private event BeforeUploadEventHandler _BeforeUpload;
        /// <summary>
        /// 当计算文件校验码完成之后开始上传文件之前引发此事件。
        /// </summary>
        public event BeforeUploadEventHandler BeforeUpload
        {
            add
            {
                this._BeforeUpload += value;
            }
            remove
            {
                this._BeforeUpload -= value;
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
        /// 开始上传指定文件。
        /// </summary>
        /// <param name="FileStream">文件数据流。</param>
        /// <param name="FileName">文件名。</param>
        /// <param name="CustomParameter">自定义参数。</param>
        public void StartUpload(System.IO.Stream FileStream, string FileName, string CustomParameter)
        {
            this.StartUpload(FileStream, FileName, CustomParameter, this.BufferSize, this.ChunkSize);
        }

        /// <summary>
        /// 开始上传指定文件。
        /// </summary>
        /// <param name="FileStream">文件数据流。</param>
        /// <param name="FileName">文件名。</param>
        /// <param name="CustomParameter">自定义参数。</param>
        /// <param name="BufferSize">缓冲区大小。</param>
        /// <param name="ChunkSize">每次与上传服务器建立连接后允许发送的最大数据量。</param>
        public void StartUpload(System.IO.Stream FileStream, string FileName, string CustomParameter, int BufferSize, int ChunkSize)
        {
            if (FileStream == null)
            {
                throw new System.ArgumentNullException("FileStream", "参数“FileStream”不能为 null。");
                //return;
            }
            if (this.Finished || this.Cancelling) //如果已经完成上传，或者取消上传过程还未结束，则无法继续上传，直接结束流程。
            {
                return;
            }
            this.FileStream = FileStream;
            this.FileName = FileName;
            this.FileSize = FileStream.Length;
            this.CustomParameter = CustomParameter;
            this.BufferSize = BufferSize;
            this.ChunkSize = ChunkSize;
            this.Cancelling = false;

            //System.Threading.Thread thread = null;
            //if (thread == null)
            //{
            //    thread = new System.Threading.Thread(new System.Threading.ThreadStart(this.StartBreakpointUpload));
            //    thread.Start();
            //}
            #region 获取文件完整性校验码，例如 SHA1 或 MD5 等。
            if (this._FileStream == null || this.Cancelling)
            {
                this.Cancelling = false;
                return;
            }
            if (this.CheckCode == null) //解决避免重复计算文件校验码，当重复调用方法“StartUpload”（例如重新启动这个文件上传任务）时。
            {
                if (this.Cancelling)
                {
                    this.Cancelling = false;
                    return;
                }
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

            if (this._BeforeUpload != null)
            {
                this._BeforeUpload(this, new BeforeUploadEventArgs(this.CheckCode));
            }
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
        /// 开始上传文件，支持秒传。
        /// </summary>
        private void StartFastUpload()
        {
            if (this.Cancelling)
            {
                this.Cancelling = false;
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
                webRequest.BeginGetResponse(new System.AsyncCallback(FastUpload_ReadHttpResponseCallback), webRequest);
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
        /// 开始上传文件，支持断点续传。
        /// </summary>
        private void StartBreakpointUpload()
        {
            if (this.Cancelling)
            {
                this.Cancelling = false;
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
                            BreakpointUploadEventArgs p = new BreakpointUploadEventArgs(breakpoint);
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
        /// 开始上传文件内容。
        /// </summary>
        private void StartUploadContent()
        {
            if (this.Cancelling)
            {
                this.Cancelling = false;
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
        /// 通知上传进度已经更改。
        /// </summary>
        /// <param name="resultData">需要返回到客户端的数据。</param>
        private void OnUploadProgressChanged(object resultData)
        {
            if (this._UploadProgressChanged != null)
            {
                this._UploadProgressChanged(this, new UploadProgressChangedEventArgs(this.FileSize, this.BytesUploaded, resultData));
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
                this._ErrorOccurred(this, new UploadErrorEventArgs(errorMessage + (exception == null ? "" : exception.ToString()), exception));
            }
        }

        /// <summary>
        /// 释放此对象占用的资源。
        /// </summary>
        public void Dispose()
        {
            this._FileStream = null;
            this.CheckCode = null;
            this._UploadProgressChanged = null;
            this._ErrorOccurred = null;
        }
        #endregion

    }

    #region 定义代理和事件参数。
    /// <summary>
    /// 上传前事件参数。
    /// </summary>
    public class BeforeUploadEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取文件完整性校验码。
        /// </summary>
        public byte[] CheckCode
        {
            get;
            private set;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="CheckCode">文件完整性校验码。</param>
        /// <param name="DataSize">数据总大小。</param>
        public BeforeUploadEventArgs(byte[] CheckCode)
        {
            this.CheckCode = CheckCode;
        }
    }

    /// <summary>
    /// 上传前事件代理。
    /// </summary>
    /// <param name="sender">事件引发对象。</param>
    /// <param name="e">事件参数。</param>
    public delegate void BeforeUploadEventHandler(object sender, BeforeUploadEventArgs e);

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
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="errorMessage">错误信息。</param>
        /// <param name="exception">引发此错误的异常信息，或者null。</param>
        public UploadErrorEventArgs(string errorMessage, System.Exception exception)
        {
            this.Message = ((string.IsNullOrEmpty(errorMessage) && exception != null)? exception.Message: errorMessage);
            this.Exception = exception;
        }
    }

    /// <summary>
    /// 上传出错事件代理。
    /// </summary>
    /// <param name="sender">事件引发对象。</param>
    /// <param name="e">事件参数。</param>
    public delegate void UploadErrorEventHandler(object sender, UploadErrorEventArgs e);

    /// <summary>
    /// 上传进度更改事件参数。
    /// </summary>
    public class UploadProgressChangedEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取数据总大小。
        /// </summary>
        public long DataSize
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
        /// 获取需要返回到客户端的数据。
        /// </summary>
        public object ResultData
        {
            get;
            set;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="DataSize">数据总大小。</param>
        /// <param name="FinishedSize">已成功上传的数据大小。</param>
        /// <param name="resultData">需要返回到客户端的数据。</param>
        public UploadProgressChangedEventArgs(long DataSize, long FinishedSize, object resultData)
        {
            this.DataSize = DataSize;
            this.FinishedSize = FinishedSize;
            this.ResultData = resultData;
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
        /// 一个构造方法。
        /// </summary>
        /// <param name="breakpoint">断点上传起始位置。</param>
        public BreakpointUploadEventArgs(long breakpoint)
        {
            this.Breakpoint = breakpoint;
        }
    }

    /// <summary>
    /// 断点上传信息事件代理。
    /// </summary>
    /// <param name="sender">事件引发对象。</param>
    /// <param name="e">事件参数。</param>
    public delegate void BreakpointUploadEventHandler(object sender, BreakpointUploadEventArgs e);

    #endregion

}
