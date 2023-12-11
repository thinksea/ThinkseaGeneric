namespace Thinksea.Net.FileUploader
{
    /// <summary>
    /// 文件上传客户端必须实现的接口。
    /// </summary>
    public interface IFileUpload : System.IDisposable
    {
		/// <summary>
		/// 开始上传指定文件。
		/// </summary>
		/// <param name="fileStream">文件数据流。</param>
		/// <param name="fileName">文件名。</param>
		/// <param name="customParameter">自定义参数。</param>
		System.Threading.Tasks.Task StartUploadAsync(System.IO.Stream fileStream, string fileName, string customParameter);
		/// <summary>
		/// 开始上传指定文件。
		/// </summary>
		/// <param name="fileStream">文件数据流。</param>
		/// <param name="fileName">文件名。</param>
		/// <param name="customParameter">自定义参数。</param>
		/// <param name="bufferSize">缓冲区大小。</param>
		/// <param name="chunkSize">每次与上传服务器建立连接后允许发送的最大数据量。</param>
		System.Threading.Tasks.Task StartUploadAsync(System.IO.Stream fileStream, string fileName, string customParameter, int bufferSize, int chunkSize);
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
