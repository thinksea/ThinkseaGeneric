using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Thinksea.Net.FileUploader_WebFormDemo
{
    public class Define
    {
        /// <summary>
        /// 返回与指定虚拟路径相对应的文件上传物理路径。
        /// </summary>
        /// <param name="path">虚拟路径。</param>
        /// <returns>与 path 相对应的文件上传物理路径。</returns>
        public static string MapFileUploadPath(string path)
        {
            return System.IO.Path.Combine(System.AppContext.BaseDirectory, path.Replace(System.IO.Path.AltDirectorySeparatorChar, System.IO.Path.DirectorySeparatorChar).TrimStart(System.IO.Path.DirectorySeparatorChar));
        }

        /// <summary>
        /// 图片文件上传目录。（以左下划线“/”为后缀。）
        /// </summary>
        public const string FileUploadDirectory = "/upload/files/";
    }
}
