namespace Thinksea.Windows
{
    /// <summary>
    /// 文件系统权限管理类。
    /// </summary>
    /// <example>
    /// <code>
    /// //获取指定目录的安全性设置
    /// string path = @"c:\test";
    /// Thinksea.Windows.FileSystem.AccessManagement.RemovePermission(path, "123");
    /// </code>
    /// </example>
    public class AccessManagement
    {
        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public AccessManagement()
        {
        }

        /// <summary>
        /// 判断指定的文件或目录是否拥有与 identity 相关联的权限设置。
        /// </summary>
        /// <param name="path">文件或目录路径。</param>
        /// <param name="identity">Windows 用户或组名称。</param>
        /// <returns>存在返回 true；否则返回 false。</returns>
        public static bool IsExistsPermission(string path, string identity)
        {
            identity = identity.ToLowerInvariant();
            bool hasDomain = (identity.IndexOf('\\') != -1);

            if (System.IO.Directory.Exists(path))
            {
                System.Security.AccessControl.DirectorySecurity ds = System.IO.Directory.GetAccessControl(path);
                System.Security.AccessControl.AuthorizationRuleCollection arc = ds.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                foreach (System.Security.AccessControl.FileSystemAccessRule tmp in arc)
                {
                    string ident = tmp.IdentityReference.Value.ToLowerInvariant();
                    if (hasDomain)
                    {
                        if (ident == identity)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (ident == identity || ident.EndsWith(@"\" + identity))
                        {
                            return true;
                        }
                    }
                }
            }
            else if (System.IO.File.Exists(path))
            {
                System.Security.AccessControl.FileSecurity ds = System.IO.File.GetAccessControl(path);
                System.Security.AccessControl.AuthorizationRuleCollection arc = ds.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                foreach (System.Security.AccessControl.FileSystemAccessRule tmp in arc)
                {
                    string ident = tmp.IdentityReference.Value.ToLowerInvariant();
                    if (hasDomain)
                    {
                        if (ident == identity)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (ident == identity || ident.EndsWith(@"\" + identity))
                        {
                            return true;
                        }
                    }
                }
            }
            return false;

        }

        /// <summary>
        /// 获取权限集合。
        /// </summary>
        /// <param name="path">文件或目录路径。</param>
        /// <returns>权限集合。</returns>
        public static string[] GetPermissions(string path)
        {
            System.Collections.Generic.List<string> l = new System.Collections.Generic.List<string>();

            if (System.IO.Directory.Exists(path))
            {
                System.Security.AccessControl.DirectorySecurity ds = System.IO.Directory.GetAccessControl(path);
                System.Security.AccessControl.AuthorizationRuleCollection arc = ds.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                foreach (System.Security.AccessControl.FileSystemAccessRule tmp in arc)
                {
                    l.Add(tmp.IdentityReference.Value);
                }
            }
            else if (System.IO.File.Exists(path))
            {
                System.Security.AccessControl.FileSecurity ds = System.IO.File.GetAccessControl(path);
                System.Security.AccessControl.AuthorizationRuleCollection arc = ds.GetAccessRules(true, true, typeof(System.Security.Principal.NTAccount));
                foreach (System.Security.AccessControl.FileSystemAccessRule tmp in arc)
                {
                    l.Add(tmp.IdentityReference.Value);
                }
            }
            return l.ToArray();

        }

        /// <summary>
        /// 向指定的文件或目录追加访问权限。
        /// </summary>
        /// <param name="path">文件或目录路径。</param>
        /// <param name="identity">Windows 用户或组名称。</param>
        /// <param name="fileSystemRights">访问权限。</param>
        /// <param name="accessControlType">指定允许还是拒绝该操作。</param>
        public static void AppendPermission(string path, string identity, System.Security.AccessControl.FileSystemRights fileSystemRights, System.Security.AccessControl.AccessControlType accessControlType)
        {
            System.Security.AccessControl.FileSystemAccessRule ar1 = new System.Security.AccessControl.FileSystemAccessRule(identity, fileSystemRights, accessControlType);

            if (System.IO.Directory.Exists(path))
            {
                System.Security.AccessControl.FileSystemAccessRule ar2 = new System.Security.AccessControl.FileSystemAccessRule(identity, fileSystemRights, System.Security.AccessControl.InheritanceFlags.ContainerInherit | System.Security.AccessControl.InheritanceFlags.ObjectInherit, System.Security.AccessControl.PropagationFlags.InheritOnly, accessControlType);

                System.Security.AccessControl.DirectorySecurity ds = System.IO.Directory.GetAccessControl(path);
                ds.AddAccessRule(ar1);
                ds.AddAccessRule(ar2);
                System.IO.Directory.SetAccessControl(path, ds);
            }
            else if (System.IO.File.Exists(path))
            {
                System.Security.AccessControl.FileSecurity ds = System.IO.File.GetAccessControl(path);
                ds.AddAccessRule(ar1);
                System.IO.File.SetAccessControl(path, ds);
            }
        }

        /// <summary>
        /// 从指定的文件或目录移除访问权限。
        /// </summary>
        /// <param name="path">文件或目录路径。</param>
        /// <param name="identity">Windows 用户或组名称。</param>
        /// <param name="fileSystemRights"></param>
        /// <param name="accessControlType">指定允许还是拒绝该操作。</param>
        public static void RemovePermission(string path, string identity, System.Security.AccessControl.FileSystemRights fileSystemRights, System.Security.AccessControl.AccessControlType accessControlType)
        {
            System.Security.AccessControl.FileSystemAccessRule ar1 = new System.Security.AccessControl.FileSystemAccessRule(identity, fileSystemRights, accessControlType);

            if (System.IO.Directory.Exists(path))
            {
                System.Security.AccessControl.DirectorySecurity ds = System.IO.Directory.GetAccessControl(path);
                ds.RemoveAccessRuleAll(ar1);
                System.IO.Directory.SetAccessControl(path, ds);
            }
            else if (System.IO.File.Exists(path))
            {
                System.Security.AccessControl.FileSecurity ds = System.IO.File.GetAccessControl(path);
                ds.RemoveAccessRuleAll(ar1);
                System.IO.File.SetAccessControl(path, ds);
            }
        }

        /// <summary>
        /// 从指定的文件或目录移除访问权限。
        /// </summary>
        /// <param name="path">文件或目录路径。</param>
        /// <param name="identity">Windows 用户或组名称。</param>
        public static void RemovePermission(string path, string identity)
        {
            if (System.IO.Directory.Exists(path))
            {
                System.Security.AccessControl.DirectorySecurity ds = System.IO.Directory.GetAccessControl(path);
                ds.PurgeAccessRules(new System.Security.Principal.NTAccount(identity));
                System.IO.Directory.SetAccessControl(path, ds);
            }
            else if (System.IO.File.Exists(path))
            {
                System.Security.AccessControl.FileSecurity ds = System.IO.File.GetAccessControl(path);
                ds.PurgeAccessRules(new System.Security.Principal.NTAccount(identity));
                System.IO.File.SetAccessControl(path, ds);
            }
        }

        /// <summary>
        /// 对指定的文件或目录的设置新的访问权限。移除所有现有的权限。
        /// </summary>
        /// <param name="path">文件或目录路径。</param>
        /// <param name="identity">Windows 用户或组名称。</param>
        /// <param name="fileSystemRights">访问权限。</param>
        /// <param name="accessControlType">指定允许还是拒绝该操作。</param>
        public static void SetPermission(string path, string identity, System.Security.AccessControl.FileSystemRights fileSystemRights, System.Security.AccessControl.AccessControlType accessControlType)
        {
            System.Security.AccessControl.FileSystemAccessRule ar1 = new System.Security.AccessControl.FileSystemAccessRule(identity, fileSystemRights, accessControlType);
            if (System.IO.Directory.Exists(path))
            {
                System.Security.AccessControl.FileSystemAccessRule ar2 = new System.Security.AccessControl.FileSystemAccessRule(identity, fileSystemRights, System.Security.AccessControl.InheritanceFlags.ContainerInherit | System.Security.AccessControl.InheritanceFlags.ObjectInherit, System.Security.AccessControl.PropagationFlags.InheritOnly, accessControlType);

                System.Security.AccessControl.DirectorySecurity ds = new System.Security.AccessControl.DirectorySecurity();
                ds.AddAccessRule(ar1);
                ds.AddAccessRule(ar2);
                ds.SetAccessRuleProtection(true, false);
                System.IO.Directory.SetAccessControl(path, ds);
            }
            else if (System.IO.File.Exists(path))
            {
                System.Security.AccessControl.FileSecurity ds = new System.Security.AccessControl.FileSecurity();
                ds.AddAccessRule(ar1);
                ds.SetAccessRuleProtection(true, false);
                System.IO.File.SetAccessControl(path, ds);
            }
        }

        /// <summary>
        /// 清除指定的文件或目录拥有的全部访问权限。
        /// </summary>
        /// <param name="path">文件或目录路径。</param>
        public static void ClearPermission(string path)
        {
            if (System.IO.Directory.Exists(path))
            {
                System.Security.AccessControl.DirectorySecurity ds = new System.Security.AccessControl.DirectorySecurity();
                ds.SetAccessRuleProtection(true, false);
                System.IO.Directory.SetAccessControl(path, ds);
            }
            else if (System.IO.File.Exists(path))
            {
                System.Security.AccessControl.FileSecurity ds = new System.Security.AccessControl.FileSecurity();
                ds.SetAccessRuleProtection(true, false);
                System.IO.File.SetAccessControl(path, ds);
            }
        }

    }

}
