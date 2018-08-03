namespace Thinksea.Windows
{
    /// <summary>
    /// 封装了对文件系统的操作功能。
    /// </summary>
    public static class FileSystem
    {
        #region 复制。
        /// <summary>
        /// 将现有目录及其内容复制到新目录，并提供指定新目录名的选项。
        /// </summary>
        /// <param name="sourceDirectory">要移动的目录的名称。</param>
        /// <param name="destDirectory">目录的新路径。</param>
        /// <param name="overwrite">当目标文件已经存在时是否覆盖。</param>
        /// <param name="emptyDirectory">是否复制空目录。</param>
        public static void CopyDirectory(string sourceDirectory, string destDirectory, bool overwrite, bool emptyDirectory)
        {
            if (System.IO.Directory.Exists(sourceDirectory))
            {
                foreach (string item in System.IO.Directory.GetDirectories(sourceDirectory))
                {
                    CopyDirectory(item, System.IO.Path.Combine(destDirectory, System.IO.Path.GetFileName(item)), overwrite, emptyDirectory);
                }
                string[] files = System.IO.Directory.GetFiles(sourceDirectory);
                if (files.Length == 0)
                {
                    if (emptyDirectory && !System.IO.Directory.Exists(destDirectory))
                    {
                        System.IO.Directory.CreateDirectory(destDirectory);
                    }
                }
                else
                {
                    if (!System.IO.Directory.Exists(destDirectory))
                    {
                        System.IO.Directory.CreateDirectory(destDirectory);
                    }
                    foreach (string item in files)
                    {
                        string dFile = System.IO.Path.Combine(destDirectory, System.IO.Path.GetFileName(item));
                        if (overwrite)
                        {
                            if (System.IO.File.Exists(dFile))
                            {
                                System.IO.File.SetAttributes(dFile, System.IO.FileAttributes.Normal);
                            }
                            System.IO.File.Copy(item, dFile, overwrite);
                        }
                        else
                        {
                            if (!System.IO.File.Exists(dFile))
                            {
                                System.IO.File.Copy(item, dFile, overwrite);
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 将文件或目录（包含子目录和文件）复制到新位置，并提供指定新名称的选项。
        /// </summary>
        /// <param name="source">要移动的文件或目录的名称。</param>
        /// <param name="dest">文件或目录的新路径。</param>
        /// <param name="overwrite">当目标文件已经存在时是否覆盖。</param>
        /// <param name="emptyDirectory">是否复制空目录，当指定的 source 为目录时。</param>
        public static void Copy(string source, string dest, bool overwrite, bool emptyDirectory)
        {
            if (System.IO.File.Exists(source))
            {
                if (overwrite)
                {
                    System.IO.File.Copy(source, dest, overwrite);
                }
                else
                {
                    if (!System.IO.File.Exists(dest))
                    {
                        System.IO.File.Copy(source, dest, overwrite);
                    }
                }
            }
            else if (System.IO.Directory.Exists(source))
            {
                CopyDirectory(source, dest, overwrite, emptyDirectory);
            }
        }

        /// <summary>
        /// 将文件或目录（包含子目录和文件）复制到新位置，并提供指定新名称的选项。复制空目录。
        /// </summary>
        /// <param name="source">要移动的文件或目录的名称。</param>
        /// <param name="dest">文件或目录的新路径。</param>
        /// <param name="overwrite">当目标文件已经存在时是否覆盖。</param>
        public static void Copy(string source, string dest, bool overwrite)
        {
            Copy(source, dest, overwrite, true);
        }

        /// <summary>
        /// 将文件或目录（包含子目录和文件）复制到新位置，并提供指定新名称的选项。当目标文件已经存在时不覆盖。复制空目录。
        /// </summary>
        /// <param name="source">要移动的文件或目录的名称。</param>
        /// <param name="dest">文件或目录的新路径。</param>
        public static void Copy(string source, string dest)
        {
            Copy(source, dest, false, true);
        }
        #endregion

        #region 移动。
        /// <summary>
        /// 将指定目录移到新位置，并提供指定新目录名的选项。主要用于跨卷移动文件。
        /// </summary>
        /// <param name="sourceDirectory">原目录。</param>
        /// <param name="destDirectory">目的目录。</param>
        /// <param name="overwrite">当目标文件已经存在时是否覆盖。</param>
        private static void MoveDirectoryBase(string sourceDirectory, string destDirectory, bool overwrite)
        {
            if (System.IO.Directory.Exists(sourceDirectory))
            {
                foreach (string item in System.IO.Directory.GetDirectories(sourceDirectory))
                {
                    MoveDirectoryBase(item, System.IO.Path.Combine(destDirectory, System.IO.Path.GetFileName(item)), overwrite);
                }
                string[] files = System.IO.Directory.GetFiles(sourceDirectory);
                if (!System.IO.Directory.Exists(destDirectory))
                {
                    System.IO.Directory.CreateDirectory(destDirectory);
                }
                foreach (string item in files)
                {
                    string dFile = System.IO.Path.Combine(destDirectory, System.IO.Path.GetFileName(item));
                    if (!System.IO.File.Exists(dFile))
                    {
                        System.IO.File.Move(item, dFile);
                    }
                    else
                    {
                        if (overwrite)
                        {
                            System.IO.File.Delete(dFile);
                            System.IO.File.Move(item, dFile);
                        }
                    }
                }
                if (overwrite)
                {
                    System.IO.Directory.Delete(sourceDirectory, true);
                }
                else
                {
                    if (System.IO.Directory.GetFiles(sourceDirectory).Length == 0 && System.IO.Directory.GetDirectories(sourceDirectory).Length == 0)
                    {
                        System.IO.Directory.Delete(sourceDirectory, true);
                    }
                }
            }

        }

        /// <summary>
        /// 将指定目录移到新位置，并提供指定新目录名的选项。
        /// </summary>
        /// <param name="sourceDirectory">原目录。</param>
        /// <param name="destDirectory">目的目录。</param>
        /// <param name="overwrite">当目标文件已经存在时是否覆盖。</param>
        /// <remarks>
        /// 因为 .net 提供的自带的移动目录方法不能实现不同卷之间的移动，所以此方法自动识别，对于同一卷之间的目录移动使用 .net 自带方法，不同卷则采用创建目录和移动文件的方法来实现目录移动。
        /// </remarks>
        public static void MoveDirectory(string sourceDirectory, string destDirectory, bool overwrite)
        {
            if (System.IO.Directory.Exists(sourceDirectory))
            {
                System.IO.DirectoryInfo diSource = new System.IO.DirectoryInfo(sourceDirectory);
                System.IO.DirectoryInfo diDest = new System.IO.DirectoryInfo(destDirectory);
                if (diSource.FullName.ToLower() == diDest.FullName.ToLower()) return;
                if (System.IO.Path.GetPathRoot(diSource.FullName) == System.IO.Path.GetPathRoot(diDest.FullName) && diDest.Exists == false)
                {
                    if (!diDest.Parent.Exists)
                    {
                        diDest.Parent.Create();
                    }
                    System.IO.Directory.Move(sourceDirectory, destDirectory);
                }
                else
                {
                    MoveDirectoryBase(sourceDirectory, destDirectory, overwrite);
                }

            }
        }

        /// <summary>
        /// 将文件或目录（包含子目录和文件）移到新位置，并指定新的名称。
        /// </summary>
        /// <param name="source">原文件或目录。</param>
        /// <param name="dest">目的文件或目录。</param>
        /// <param name="overwrite">指示是否覆盖重名文件。</param>
        public static void Move(string source, string dest, bool overwrite)
        {
            if (System.IO.File.Exists(source))//如果是文件。
            {
                if (!System.IO.File.Exists(dest))//如果目的文件不存在。
                {
                    System.IO.File.Move(source, dest);
                }
                else
                {
                    if (overwrite)//覆盖重名文件。
                    {
                        if (System.IO.File.Exists(dest))
                        {
                            System.IO.File.SetAttributes(dest, System.IO.FileAttributes.Normal);
                            System.IO.File.Delete(dest);
                        }
                        System.IO.File.Move(source, dest);
                    }
                }
            }
            else if (System.IO.Directory.Exists(source))//如果是目录。
            {
                MoveDirectory(source, dest, overwrite);
            }

        }

        /// <summary>
        /// 将文件或目录（包含子目录和文件）移到新位置，并指定新的名称。不覆盖重名文件。
        /// </summary>
        /// <param name="source">原文件或目录。</param>
        /// <param name="dest">目的文件或目录。</param>
        public static void Move(string source, string dest)
        {
            Move(source, dest, false);
        }
        #endregion

        #region 删除。
        /// <summary>
        /// 删除文件或目录。
        /// </summary>
        /// <param name="path">要删除的文件或目录。</param>
        /// <param name="recursive">如果包含 Path 中的目录、子目录和文件，则为 true；否则为 false。</param>
        private static void Delete(string path, bool recursive)
        {
            if (System.IO.File.Exists(path))
            {
                System.IO.File.SetAttributes(path, System.IO.FileAttributes.Normal);
                System.IO.File.Delete(path);
            }
            else
            {
                if (System.IO.Directory.Exists(path))
                {
                    #region 增加此段代码为了解决无法删除只读文件的问题。
                    string[] files = System.IO.Directory.GetFileSystemEntries(path);
                    foreach (var tmp in files)
                    {
                        Delete(tmp, recursive);
                    }
                    #endregion
                    System.IO.Directory.Delete(path, recursive);
                }
            }

        }

        /// <summary>
        /// 删除文件或目录。
        /// </summary>
        /// <param name="path">要删除的文件或目录。</param>
        public static void Delete(string path)
        {
            FileSystem.Delete(path, true);

        }
        #endregion

        /// <summary>
        /// 判断指定的文件或目录是否存在。
        /// </summary>
        /// <param name="path">文件或目录路径。</param>
        /// <returns>存在返回 true；否则返回 false。</returns>
        public static bool IsExists(string path)
        {
            if (System.IO.File.Exists(path))
            {
                return true;
            }
            else
            {
                if (System.IO.Directory.Exists(path))
                {
                    return true;
                }
            }
            return false;

        }

        /// <summary>
        /// 比较两个文件内容是否相同。注意：两个文件都不存在视为相同。
        /// </summary>
        /// <param name="sourceFileName">原始文件。</param>
        /// <param name="destFileName">待比较文件。</param>
        /// <returns></returns>
        private static bool CompareFile(string sourceFileName, string destFileName)
        {
            System.IO.FileInfo sourceFileInfo = new System.IO.FileInfo(sourceFileName);
            System.IO.FileInfo destFileInfo = new System.IO.FileInfo(destFileName);
            if (!sourceFileInfo.Exists && !destFileInfo.Exists)
            {
                return true;
            }
            if (sourceFileInfo.Exists != destFileInfo.Exists)
            {
                return false;
            }
            if (sourceFileInfo.Length != destFileInfo.Length)//根据文件长度判断两文件是否相同。
            {
                return false;
            }

            System.IO.FileStream sourceFileStream = null;
            System.IO.FileStream destFileStream = null;
            try
            {
                sourceFileStream = sourceFileInfo.OpenRead();
                destFileStream = destFileInfo.OpenRead();
                if (sourceFileStream.Length != destFileStream.Length)//根据文件长度判断两文件是否相同。
                {
                    return false;
                }

                #region 循环比较文件内容是否相同。
                byte[] sourceBuffer = new byte[sourceFileStream.Length > 1024 * 8 ? 1024 * 8 : sourceFileStream.Length];
                byte[] destBuffer = new byte[destFileStream.Length > 1024 * 8 ? 1024 * 8 : destFileStream.Length];
                int sourceCount = 0;
                int destCount = 0;
                while (sourceFileStream.Position < sourceFileStream.Length)
                {
                    sourceCount = sourceFileStream.Read(sourceBuffer, 0, sourceBuffer.Length);
                    destCount = destFileStream.Read(destBuffer, 0, destBuffer.Length);
                    if (sourceCount != destCount)
                    {
                        return false;
                    }
                    for (int i = 0; i < sourceCount; i++)
                    {
                        if (sourceBuffer[i] != destBuffer[i])
                        {
                            return false;
                        }
                    }
                }
                #endregion

                return true;

            }
            finally
            {
                if (sourceFileStream != null)
                {
                    sourceFileStream.Close();
                    sourceFileStream = null;
                }
                if (destFileStream != null)
                {
                    destFileStream.Close();
                    destFileStream = null;
                }
            }

        }

        #region 文件系统操作。

        /// <summary>
        /// 根据文件扩展名得到系统扩展名的图标
        /// </summary>
        /// <param name="fileName">文件名(如：win.rar;setup.exe;temp.txt)</param>
        /// <param name="largeIcon">图标的大小</param>
        /// <returns></returns>
        public static System.Drawing.Icon GetFileIcon(string fileName, bool largeIcon)
        {
            Thinksea.Windows.Win32API.SHFILEINFO _SHFILEINFO = new Thinksea.Windows.Win32API.SHFILEINFO();
            Thinksea.Windows.Win32API.GetFileInfoFlags flags;
            if (largeIcon)
                flags = Thinksea.Windows.Win32API.GetFileInfoFlags.SHGFI_ICON | Thinksea.Windows.Win32API.GetFileInfoFlags.SHGFI_LARGEICON | Thinksea.Windows.Win32API.GetFileInfoFlags.SHGFI_USEFILEATTRIBUTES;
            else
                flags = Thinksea.Windows.Win32API.GetFileInfoFlags.SHGFI_ICON | Thinksea.Windows.Win32API.GetFileInfoFlags.SHGFI_SMALLICON | Thinksea.Windows.Win32API.GetFileInfoFlags.SHGFI_USEFILEATTRIBUTES;
            System.IntPtr IconIntPtr = Thinksea.Windows.Win32API.User32.SHGetFileInfo(fileName, (uint)Thinksea.Windows.Win32API.FileAttributeFlags.FILE_ATTRIBUTE_TEMPORARY, ref _SHFILEINFO, (uint)System.Runtime.InteropServices.Marshal.SizeOf(_SHFILEINFO), (uint)flags);
            if (IconIntPtr.Equals(System.IntPtr.Zero))
                return null;
            return System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
        }

        /// <summary>  
        /// 获取目录图标
        /// </summary>
        /// <param name="largeIcon">指示获取大图标还是小图标。</param>
        /// <returns>图标</returns>  
        public static System.Drawing.Icon GetDirectoryIcon(bool largeIcon)
        {
            Thinksea.Windows.Win32API.SHFILEINFO _SHFILEINFO = new Thinksea.Windows.Win32API.SHFILEINFO();
            Thinksea.Windows.Win32API.GetFileInfoFlags flags;
            if (largeIcon)
                flags = Thinksea.Windows.Win32API.GetFileInfoFlags.SHGFI_ICON | Thinksea.Windows.Win32API.GetFileInfoFlags.SHGFI_LARGEICON;
            else
                flags = Thinksea.Windows.Win32API.GetFileInfoFlags.SHGFI_ICON | Thinksea.Windows.Win32API.GetFileInfoFlags.SHGFI_SMALLICON;

            System.IntPtr IconIntPtr = Thinksea.Windows.Win32API.User32.SHGetFileInfo(@"", (uint)0, ref _SHFILEINFO, (uint)System.Runtime.InteropServices.Marshal.SizeOf(_SHFILEINFO), (uint)flags);
            if (IconIntPtr.Equals(System.IntPtr.Zero))
                return null;
            System.Drawing.Icon _Icon = System.Drawing.Icon.FromHandle(_SHFILEINFO.hIcon);
            return _Icon;
        }

        /// <summary>
        /// 获取文件类型。
        /// </summary>
        /// <param name="fileName">文件名(如：win.rar;setup.exe;temp.txt)</param>
        /// <returns>类型命名。</returns>
        public static string GetTypeName(string fileName)
        {
            Thinksea.Windows.Win32API.SHFILEINFO fileInfo = new Thinksea.Windows.Win32API.SHFILEINFO();  //初始化FileInfomation结构

            //调用GetFileInfo函数，最后一个参数说明获取的是文件类型(SHGFI_TYPENAME)
            System.IntPtr res = Thinksea.Windows.Win32API.User32.SHGetFileInfo(fileName, (uint)Thinksea.Windows.Win32API.FileAttributeFlags.FILE_ATTRIBUTE_NORMAL,
                ref fileInfo, (uint)System.Runtime.InteropServices.Marshal.SizeOf(fileInfo), (uint)Thinksea.Windows.Win32API.GetFileInfoFlags.SHGFI_TYPENAME);

            return fileInfo.szTypeName;
        }

        /// <summary>
        /// 获取显示名称。
        /// </summary>
        /// <param name="fileName">文件名(如：win.rar;setup.exe;temp.txt)</param>
        /// <returns>显示名称。</returns>
        public static string GetDisplayName(string fileName)
        {
            Thinksea.Windows.Win32API.SHFILEINFO fileInfo = new Thinksea.Windows.Win32API.SHFILEINFO();  //初始化FileInfomation结构

            //调用GetFileInfo函数，最后一个参数说明获取的是文件类型(SHGFI_TYPENAME)
            System.IntPtr res = Thinksea.Windows.Win32API.User32.SHGetFileInfo(fileName, (uint)Thinksea.Windows.Win32API.FileAttributeFlags.FILE_ATTRIBUTE_NORMAL,
                ref fileInfo, (uint)System.Runtime.InteropServices.Marshal.SizeOf(fileInfo), (uint)Thinksea.Windows.Win32API.GetFileInfoFlags.SHGFI_DISPLAYNAME);

            return fileInfo.szTypeName;
        }

        #endregion

    }
}
