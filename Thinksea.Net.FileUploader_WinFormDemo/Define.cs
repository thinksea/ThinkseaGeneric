using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thinksea.Net.FileUploader_WinFormDemo
{
    public class Define
    {
        /// <summary>
        /// 文件上传服务地址。
        /// 这里使用项目“Thinksea.Net.FileUploader_AspNetCoreDemo”做为文件上传服务端。
        /// </summary>
        public const string HttpUploadHandlerServiceURL = "https://localhost:44390/HttpUploadHandler";
        //public const string HttpUploadHandlerServiceURL = "https://localhost:44307/HttpUploadHandler.ashx";
    }
}
