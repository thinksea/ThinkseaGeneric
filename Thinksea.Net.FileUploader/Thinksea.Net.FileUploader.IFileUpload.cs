namespace Thinksea.Net.FileUploader
{
    /// <summary>
    /// 文件上传客户端必须实现的接口。
    /// </summary>
    public interface IFileUpload : System.IDisposable
    {
        /// <summary>
        /// 开始上传指定文件数据。
        /// </summary>
        /// <param name="FileStream">文件数据流。</param>
        /// <param name="FileName">文件名。</param>
        /// <param name="CustomParameter">自定义参数。</param>
        void StartUpload(System.IO.Stream FileStream, string FileName, string CustomParameter);
        /// <summary>
        /// 开始上传指定文件数据。
        /// </summary>
        /// <param name="FileStream">文件数据流。</param>
        /// <param name="FileName">文件名。</param>
        /// <param name="CustomParameter">自定义参数。</param>
        /// <param name="BufferSize">缓冲区大小。</param>
        /// <param name="ChunkSize">每次与上传服务器建立连接后允许发送的最大数据量。</param>
        void StartUpload(System.IO.Stream FileStream, string FileName, string CustomParameter, int BufferSize, int ChunkSize);
        /// <summary>
        /// 放弃上传文件。
        /// </summary>
        void CancelUpload();
        /// <summary>
        /// 当上传进度更改时引发此事件。
        /// </summary>
        event UploadProgressChangedEventHandler UploadProgressChanged;
        /// <summary>
        /// 当出现错误时引发此事件。
        /// </summary>
        event UploadErrorEventHandler ErrorOccurred;
        /// <summary>
        /// 当发现可用的断点上传信息时引发此事件。
        /// </summary>
        event BreakpointUploadEventHandler FindBreakpoint;
    }
}
