using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thinksea.Net.FileUploader_WinFormDemo
{
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
        /// 回调参数，上传结束后传递回指定位置。
        /// </summary>
        public string CallbackParams
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
        /// <param name="callbackParams">回调参数，上传结束后传递回指定位置。</param>
        public FileUploadResult(string savePath, long fileLength, bool isFastUpload, string callbackParams)
        {
            this.SavePath = savePath;
            this.FileLength = fileLength;
            this.IsFastUpload = isFastUpload;
            this.CallbackParams = callbackParams;
        }

        /// <summary>
        /// 从指定的对象还原为此类型的实例。
        /// </summary>
        /// <param name="obj">从其还原对象的数据。</param>
        /// <returns>一个 <see cref="FileUploadResult"/> 实例。</returns>
        /// <exception cref="System.NotSupportedException">当无法反序列化对象时引发的异常。</exception>
        public static FileUploadResult ConvertFrom(object obj)
        {
            Thinksea.Net.FileUploader_WinFormDemo.FileUploadResult fileUploadResult = obj as Thinksea.Net.FileUploader_WinFormDemo.FileUploadResult;
            if (fileUploadResult == null)
            {
                if (obj is Newtonsoft.Json.Linq.JObject)
                {
                    Newtonsoft.Json.Linq.JObject jobj = (Newtonsoft.Json.Linq.JObject)obj;
                    fileUploadResult = jobj.ToObject<Thinksea.Net.FileUploader_WinFormDemo.FileUploadResult>();
                }
                else
                {
                    throw new System.NotSupportedException("无法将指定的数据还原为“" + typeof(Thinksea.Net.FileUploader_WinFormDemo.FileUploadResult).FullName + "”类型或从其集成的子类型的对象实例。");
                }
            }
            return fileUploadResult;
        }

    }

}
