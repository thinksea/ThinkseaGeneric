namespace Thinksea.Windows
{

    /// <summary>
    /// Windows 用户和用户组操作类。
    /// </summary>
    public class UserSystem
    {
        /// <summary>
        /// 用户属性定义标志
        /// </summary>
        [System.Flags]
        private enum ADS_USER_FLAG_ENUM
        {
            /// <summary>
            /// 登录脚本标志。如果通过 ADSI LDAP 进行读或写操作时，该标志失效。如果通过 ADSI WINNT，该标志为只读。
            /// </summary>
            SCRIPT = 0X0001,
            /// <summary>
            /// 用户帐号禁用标志
            /// </summary>
            ACCOUNTDISABLE = 0X0002,
            /// <summary>
            /// 主文件夹标志
            /// </summary>
            HOMEDIR_REQUIRED = 0X0008,
            /// <summary>
            /// 过期标志
            /// </summary>
            LOCKOUT = 0X0010,
            /// <summary>
            /// 用户密码不是必须的
            /// </summary>
            PASSWD_NOTREQD = 0X0020,
            /// <summary>
            /// 密码不能更改标志
            /// </summary>
            PASSWD_CANT_CHANGE = 0X0040,
            /// <summary>
            /// 使用可逆的加密保存密码
            /// </summary>
            ENCRYPTED_TEXT_PASSWORD_ALLOWED = 0X0080,
            /// <summary>
            /// 本地帐号标志
            /// </summary>
            TEMP_DUPLICATE_ACCOUNT = 0X0100,
            /// <summary>
            /// 普通用户的默认帐号类型
            /// </summary>
            NORMAL_ACCOUNT = 0X0200,
            /// <summary>
            /// 跨域的信任帐号标志
            /// </summary>
            INTERDOMAIN_TRUST_ACCOUNT = 0X0800,
            /// <summary>
            /// 工作站信任帐号标志
            /// </summary>
            WORKSTATION_TRUST_ACCOUNT = 0x1000,
            /// <summary>
            /// 服务器信任帐号标志
            /// </summary>
            SERVER_TRUST_ACCOUNT = 0X2000,
            /// <summary>
            /// 密码永不过期标志
            /// </summary>
            DONT_EXPIRE_PASSWD = 0X10000,
            /// <summary>
            /// MNS 帐号标志
            /// </summary>
            MNS_LOGON_ACCOUNT = 0X20000,
            /// <summary>
            /// 交互式登录必须使用智能卡
            /// </summary>
            SMARTCARD_REQUIRED = 0X40000,
            /// <summary>
            /// 当设置该标志时，服务帐号（用户或计算机帐号）将通过 Kerberos 委托信任
            /// </summary>
            TRUSTED_FOR_DELEGATION = 0X80000,
            /// <summary>
            /// 当设置该标志时，即使服务帐号是通过 Kerberos 委托信任的，敏感帐号不能被委托
            /// </summary>
            NOT_DELEGATED = 0X100000,
            /// <summary>
            /// 此帐号需要 DES 加密类型
            /// </summary>
            USE_DES_KEY_ONLY = 0X200000,
            /// <summary>
            /// 不要进行 Kerberos 预身份验证
            /// </summary>
            DONT_REQUIRE_PREAUTH = 0X4000000,
            /// <summary>
            /// 用户密码过期标志
            /// </summary>
            PASSWORD_EXPIRED = 0X800000,
            /// <summary>
            /// 用户帐号可委托标志
            /// </summary>
            TRUSTED_TO_AUTHENTICATE_FOR_DELEGATION = 0X1000000

        }

        /// <summary>
        /// 域服务对象。
        /// </summary>
        private System.DirectoryServices.DirectoryEntry AD = null;
        
        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public UserSystem()
        {
            this.AD = new System.DirectoryServices.DirectoryEntry("WinNT://" + System.Environment.MachineName + ",computer");

        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="LoginName">登录用户名(如：Administrator)</param>
        /// <param name="LoginPassword">登录密码</param>
        public UserSystem(string LoginName, string LoginPassword)
        {
            this.AD = new System.DirectoryServices.DirectoryEntry("WinNT://" + System.Environment.MachineName + ",computer", ".\\" + LoginName, LoginPassword);
        }

        /// <summary>
        /// 初始化方法
        /// </summary>
        /// <param name="MachineName">机器名</param>
        /// <param name="LoginName">登录用户名(如：Administrator)</param>
        /// <param name="LoginPassword">登录密码</param>
        public UserSystem(string MachineName, string LoginName, string LoginPassword)
        {
            this.AD = new System.DirectoryServices.DirectoryEntry("WinNT://" + MachineName + ",computer", ".\\" + LoginName, LoginPassword);
        }

        /// <summary>
        /// 析构函数。
        /// </summary>
        ~UserSystem()
        {
            if (this.AD != null)
            {
                this.AD.Close();
                this.AD = null;
            }

        }

        #region 用户处理方法。
        /// <summary>
        /// 获取用户信息。
        /// </summary>
        /// <param name="commonName">用户名</param>
        /// <returns>用户信息。</returns>
        public UserInfo GetUser(string commonName)
        {
            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "user");
            try
            {
                string FullName = (string)obUser.Invoke("Get", "FullName");
                string Description = (string)obUser.Invoke("Get", "Description");
                string HomeDirectory = (string)obUser.Invoke("Get", "HomeDirectory");
                bool PasswordExpired = (int)obUser.Invoke("Get", "PasswordExpired")==1? true: false;
                ADS_USER_FLAG_ENUM UserFlags = (ADS_USER_FLAG_ENUM)obUser.Invoke("Get", "UserFlags");
                bool DontExpirePassword = ((UserFlags & ADS_USER_FLAG_ENUM.DONT_EXPIRE_PASSWD) == ADS_USER_FLAG_ENUM.DONT_EXPIRE_PASSWD);
                bool ChangePassword = ((UserFlags & ADS_USER_FLAG_ENUM.PASSWD_CANT_CHANGE) == ADS_USER_FLAG_ENUM.PASSWD_CANT_CHANGE);
                bool EnableUser = ((UserFlags & ADS_USER_FLAG_ENUM.ACCOUNTDISABLE) == ADS_USER_FLAG_ENUM.ACCOUNTDISABLE);
                return new UserInfo(commonName, FullName, Description, HomeDirectory, PasswordExpired, DontExpirePassword, ChangePassword, EnableUser);
            }
            finally
            {
                obUser.Close();
            }
        }
        /// <summary>
        /// 获取用户列表。
        /// </summary>
        /// <returns>用户名数组。</returns>
        public string[] GetUsers()
        {
            System.Collections.Generic.List<string> l = new System.Collections.Generic.List<string>();
            foreach (System.DirectoryServices.DirectoryEntry tmp in this.AD.Children)
            {
                if (tmp.SchemaClassName == "User")
                {
                    l.Add(tmp.Name);
                }
            }
            return l.ToArray();
        }

        /// <summary>
        /// 判断指定名称的用户是否存在
        /// </summary>
        /// <param name="commonName">用户名</param>
        /// <returns>如果存在，返回 true；否则返回 false</returns>
        public bool IsUserExists(string commonName)
        {
            try
            {
                System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "user");
                obUser.Close();
                return true;
            }
            catch (System.Runtime.InteropServices.COMException e)
            {//如果用户不存在则正常返回，否则抛出异常。
                if (System.Convert.ToInt64(string.Format("0x{0:X}", e.ErrorCode), 16) == 0x800708AD)//找不到用户名。 (异常来自 HRESULT:0x800708AD)
                {
                    return false;
                }
                throw;
            }

        }
        /// <summary>
        /// 创建用户
        /// </summary>
        /// <param name="commonName">用户名</param>
        /// <param name="FullName">全名</param>
        /// <param name="Password">密码</param>
        /// <param name="Description">描述</param>
        public void CreateUser(string commonName, string FullName, string Password, string Description)
        {
            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Add(commonName, "User");
            try
            {
                obUser.Invoke("Put", "FullName", FullName);
                obUser.Invoke("Put", "Description", Description);
                obUser.Invoke("SetPassword", Password);
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }
            //this.SetPassword(commonName, Password);

        }
        /// <summary>
        /// 设置用户全名。
        /// </summary>
        /// <param name="commonName">用户名</param>
        /// <param name="FullName">全名</param>
        public void SetFullName(string commonName, string FullName)
        {
            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                obUser.Invoke("Put", "FullName", FullName);
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }
        /// <summary>
        /// 设置用户描述。
        /// </summary>
        /// <param name="commonName">用户名</param>
        /// <param name="Description">描述</param>
        public void SetDescription(string commonName, string Description)
        {
            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                obUser.Invoke("Put", "Description", Description);
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }
        /// <summary>
        /// 设置用户主文件夹路径。
        /// </summary>
        /// <param name="commonName">用户名</param>
        /// <param name="Path">主文件夹路径</param>
        public void SetHomeDirectory(string commonName, string Path)
        {
            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                obUser.Invoke("Put", "HomeDirectory", Path); //主文件夹路径
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }

        /// <summary>
        /// 删除用户。
        /// </summary>
        /// <param name="commonName">用户名</param>
        public void DeleteUser(string commonName)
        {
            System.DirectoryServices.DirectoryEntry obUser = null;
            try
            {
                obUser = this.AD.Children.Find(commonName, "User");//找得用户
            }
            catch (System.Runtime.InteropServices.COMException e)
            {//如果用户不存在则正常返回，否则抛出异常。
                if (System.Convert.ToInt64(string.Format("0x{0:X}", e.ErrorCode), 16) == 0x800708AD)//找不到用户名。 (异常来自 HRESULT:0x800708AD)
                {
                    return;
                }
                throw;
            }
            try
            {
                this.AD.Children.Remove(obUser);//删除用户
            }
            finally
            {
                obUser.Close();
            }

        }
        /// <summary>
        /// 设置用户密码。
        /// </summary>
        /// <param name="commonName">用户名</param>
        /// <param name="newPassword">用户新密码</param>
        public void SetPassword(string commonName, string newPassword)
        {
            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                obUser.Invoke("SetPassword", newPassword);
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }
        /// <summary>
        /// 修改用户密码
        /// </summary>
        /// <param name="commonName">用户名</param>
        /// <param name="oldPassword">旧密码</param>
        /// <param name="newPassword">新密码</param>
        public void ChangeUserPassword(string commonName, string oldPassword, string newPassword)
        {
            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                obUser.Invoke("ChangePassword", new object[] { oldPassword, newPassword });
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }
        /// <summary>
        /// 验证用户密码。
        /// </summary>
        /// <param name="commonName">用户名</param>
        /// <param name="Password">密码</param>
        /// <returns>验证成功返回 true；否则返回 false。</returns>
        public bool CheckPassword(string commonName, string Password)
        {//此方法当前采用捕获异常的方式判断密码是否正确，这种方法是不可取的，以后需要改善。
            try
            {
                this.ChangeUserPassword(commonName, Password, Password);
                return true;
            }
            catch (System.Reflection.TargetInvocationException)
            {
                return false;
            }

        }

        /// <summary>
        /// 设置用户下次登录时需更改密码。
        /// </summary>
        /// <param name="commonName">用户名</param>
        public void EnablePasswordExpired(string commonName)
        {
            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                this.DisableDontExpirePassword(commonName);
                this.DisableChangePassword(commonName);
                obUser.Invoke("Put", "PasswordExpired", 1);
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }
        /// <summary>
        /// 取消设置用户下次登录时需更改密码。
        /// </summary>
        /// <param name="commonName">用户名</param>
        public void DisablePasswordExpired(string commonName)
        {
            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                obUser.Invoke("Put", "PasswordExpired", 0);
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }
        /// <summary>
        /// 设置用户密码永不过期。
        /// </summary>
        /// <param name="commonName">用户名</param>
        public void EnableDontExpirePassword(string commonName)
        {

            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                object UserFlags = obUser.Invoke("Get", "UserFlags");
                ADS_USER_FLAG_ENUM aUserFlags = (ADS_USER_FLAG_ENUM)UserFlags;
                aUserFlags = aUserFlags | ADS_USER_FLAG_ENUM.DONT_EXPIRE_PASSWD;
                obUser.Invoke("Put", "UserFlags", aUserFlags);
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }
        /// <summary>
        /// 取消设置用户密码永不过期。
        /// </summary>
        /// <param name="commonName">用户名</param>
        public void DisableDontExpirePassword(string commonName)
        {

            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                object UserFlags = obUser.Invoke("Get", "UserFlags");
                ADS_USER_FLAG_ENUM aUserFlags = (ADS_USER_FLAG_ENUM)UserFlags;
                aUserFlags = aUserFlags & (~ADS_USER_FLAG_ENUM.DONT_EXPIRE_PASSWD);
                obUser.Invoke("Put", "UserFlags", aUserFlags);
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }
        /// <summary>
        /// 设置用户不能更改密码。
        /// </summary>
        /// <param name="commonName">用户名</param>
        public void EnableChangePassword(string commonName)
        {

            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                object UserFlags = obUser.Invoke("Get", "UserFlags");
                ADS_USER_FLAG_ENUM aUserFlags = (ADS_USER_FLAG_ENUM)UserFlags;
                aUserFlags = aUserFlags | ADS_USER_FLAG_ENUM.PASSWD_CANT_CHANGE;
                obUser.Invoke("Put", "UserFlags", aUserFlags);
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }
        /// <summary>
        /// 取消设置用户不能更改密码。
        /// </summary>
        /// <param name="commonName">用户名</param>
        public void DisableChangePassword(string commonName)
        {

            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                object UserFlags = obUser.Invoke("Get", "UserFlags");
                ADS_USER_FLAG_ENUM aUserFlags = (ADS_USER_FLAG_ENUM)UserFlags;
                aUserFlags = aUserFlags & (~ADS_USER_FLAG_ENUM.PASSWD_CANT_CHANGE);
                obUser.Invoke("Put", "UserFlags", aUserFlags);
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }
        /// <summary>
        /// 启用指定的用户。
        /// </summary>
        /// <param name="commonName">用户名</param>
        public void EnableUser(string commonName)
        {
            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                object UserFlags = obUser.Invoke("Get", "UserFlags");
                ADS_USER_FLAG_ENUM aUserFlags = (ADS_USER_FLAG_ENUM)UserFlags;
                aUserFlags = aUserFlags & (~ADS_USER_FLAG_ENUM.ACCOUNTDISABLE);
                obUser.Invoke("Put", "UserFlags", aUserFlags);
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }
        /// <summary>
        /// 禁用指定的用户。
        /// </summary>
        /// <param name="commonName">用户名</param>
        public void DisableUser(string commonName)
        {
            System.DirectoryServices.DirectoryEntry obUser = this.AD.Children.Find(commonName, "User");
            try
            {
                object UserFlags = obUser.Invoke("Get", "UserFlags");
                ADS_USER_FLAG_ENUM aUserFlags = (ADS_USER_FLAG_ENUM)UserFlags;
                aUserFlags = aUserFlags | ADS_USER_FLAG_ENUM.ACCOUNTDISABLE;
                obUser.Invoke("Put", "UserFlags", aUserFlags);
                obUser.CommitChanges();
            }
            finally
            {
                obUser.Close();
            }

        }
        #endregion

        #region 用户组处理方法。
        /// <summary>
        /// 获取用户组信息。
        /// </summary>
        /// <param name="groupCommonName">用户组名称</param>
        /// <returns>用户组信息。</returns>
        public GroupInfo GetGroup(string groupCommonName)
        {
            System.DirectoryServices.DirectoryEntry o = this.AD.Children.Find(groupCommonName, "Group");
            try
            {
                string Description = (string)o.Invoke("Get", "Description");
                return new GroupInfo(groupCommonName, Description);
            }
            finally
            {
                o.Close();
            }
        }
        /// <summary>
        /// 获取用户组列表。
        /// </summary>
        /// <returns>用户组名称数组。</returns>
        public string[] GetGroups()
        {
            System.Collections.Generic.List<string> l = new System.Collections.Generic.List<string>();
            foreach (System.DirectoryServices.DirectoryEntry tmp in this.AD.Children)
            {
                if (tmp.SchemaClassName == "Group")
                {
                    l.Add(tmp.Name);
                }
            }
            return l.ToArray();
        }

        /// <summary>
        /// 判断指定用户组是否存在
        /// </summary>
        /// <param name="groupCommonName">用户组名称</param>
        /// <returns>如果存在，返回 true；否则返回 false</returns>
        public bool IsGroupExists(string groupCommonName)
        {
            try
            {
                System.DirectoryServices.DirectoryEntry de = this.AD.Children.Find(groupCommonName, "group");
                de.Close();
                return true;
            }
            catch (System.Runtime.InteropServices.COMException e)
            {//如果组不存在则正常返回，否则抛出异常。
                if (System.Convert.ToInt64(string.Format("0x{0:X}", e.ErrorCode), 16) == 0x800708AC)//找不到组名。 (异常来自 HRESULT:0x800708AC)
                {
                    return false;
                }
                else if (System.Convert.ToInt64(string.Format("0x{0:X}", e.ErrorCode), 16) == 0x80070560)//指定的本地组不存在。 (异常来自 HRESULT:0x80070560)
                {
                    return false;
                }
                throw;
            }

        }
        /// <summary>
        /// 添加用户组
        /// </summary>
        /// <param name="groupCommonName">组名</param>
        /// <param name="Description">描述</param>
        public void CreateGroup(string groupCommonName, string Description)
        {
            System.DirectoryServices.DirectoryEntry Group = this.AD.Children.Add(groupCommonName, "group");
            try
            {
                Group.Invoke("Put", "description", Description);
                Group.CommitChanges();
            }
            finally
            {
                Group.Close();
            }

        }
        /// <summary>
        /// 设置用户组描述
        /// </summary>
        /// <param name="groupCommonName">组名</param>
        /// <param name="Description">描述</param>
        public void SetGroupDescription(string groupCommonName, string Description)
        {
            System.DirectoryServices.DirectoryEntry Group = this.AD.Children.Find(groupCommonName, "group");
            try
            {
                if (Group.Name != null)
                {
                    Group.Invoke("Put", "description", Description);
                    Group.CommitChanges();
                }
            }
            finally
            {
                Group.Close();
            }

        }
        /// <summary>
        /// 删除用户组
        /// </summary>
        /// <param name="groupCommonName">组名</param>
        public void DeleteGroup(string groupCommonName)
        {
            System.DirectoryServices.DirectoryEntry Group = null;
            try
            {
                Group = this.AD.Children.Find(groupCommonName, "group");
            }
            catch (System.Runtime.InteropServices.COMException e)
            {//如果组不存在则正常返回，否则抛出异常。
                if (System.Convert.ToInt64(string.Format("0x{0:X}", e.ErrorCode), 16) == 0x800708AC)//找不到组名。 (异常来自 HRESULT:0x800708AC)
                {
                    return;
                }
                throw;
            }
            try
            {
                if (Group.Name != null)
                {
                    this.AD.Children.Remove(Group);
                }
            }
            finally
            {
                Group.Close();
            }

        }
        #endregion

        #region 用户和用户组关系处理方法。
        /// <summary>
        /// 获取隶属于指定组的所有用户。
        /// </summary>
        /// <param name="groupCommonName">组名</param>
        /// <returns>用户列表。</returns>
        public string[] GetUsersByGroup(string groupCommonName)
        {
            System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string>();
            System.DirectoryServices.DirectoryEntry oGroup = this.AD.Children.Find(groupCommonName, "group");

            try
            {
                object members = oGroup.Invoke("Members", null);
                foreach (object member in (System.Collections.IEnumerable)members)
                {
                    //获取该组的每个成员
                    System.DirectoryServices.DirectoryEntry x = new System.DirectoryServices.DirectoryEntry(member);

                    result.Add(x.Name);
                }
            }
            finally
            {
                oGroup.Close();
            }
            return result.ToArray();

        }
        /// <summary>
        /// 获取指定用户隶属于哪些用户组。
        /// </summary>
        /// <param name="commonName">用户名</param>
        /// <returns>用户组列表。</returns>
        public string[] GetGroupsByUser(string commonName)
        {
            commonName = commonName.ToLowerInvariant();
            System.Collections.Generic.List<string> result = new System.Collections.Generic.List<string>();
            string [] gs = this.GetGroups();
            foreach (string tmp in gs)//此处采用了笨拙的反查方法，有待优化。
            {
                string [] us = this.GetUsersByGroup(tmp);
                foreach(string tmp2 in us)
                {
                    if (tmp2.ToLowerInvariant() == commonName)
                    {
                        result.Add(tmp);
                        break;
                    }
                }
            }
            return result.ToArray();

        }
        /// <summary>
        /// 判断用户是否存在于指定的用户组中
        /// </summary>
        /// <param name="userCommonName">用户名</param>
        /// <param name="groupCommonName">组名</param>
        /// <returns>存在返回 true；否则返回 false。</returns>
        public bool IsUserInGroup(string userCommonName, string groupCommonName)
        {
            System.DirectoryServices.DirectoryEntry oGroup = this.AD.Children.Find(groupCommonName, "group");

            try
            {
                object members = oGroup.Invoke("Members", null);
                foreach (object member in (System.Collections.IEnumerable)members)
                {
                    //获取该组的每个成员
                    System.DirectoryServices.DirectoryEntry x = new System.DirectoryServices.DirectoryEntry(member);

                    if (userCommonName == x.Name) //如果用户存在。
                    {
                        return true;
                    }
                }
            }
            finally
            {
                oGroup.Close();
            }
            return false;

        }
        /// <summary>
        /// 将指定的用户添加到指定的组中。默认为 Users 下的组和用户。
        /// </summary>
        /// <param name="userCommonName">用户名</param>
        /// <param name="groupCommonName">组名</param>
        public void AddUserToGroup(string userCommonName, string groupCommonName)
        {
            System.DirectoryServices.DirectoryEntry oUser = this.AD.Children.Find(userCommonName, "User");
            System.DirectoryServices.DirectoryEntry oGroup = this.AD.Children.Find(groupCommonName, "group");
            try
            {
                if (oGroup.Name != null && oUser.Name != null)
                {
                    oGroup.Invoke("Add", new object[] { oUser.Path });
                }
            }
            finally
            {
                oGroup.Close();
                oUser.Close();
            }
        }
        /// <summary>
        /// 将用户从指定组中移除。默认为 Users 下的组和用户。
        /// </summary>
        /// <param name="userCommonName">用户名</param>
        /// <param name="groupCommonName">组名</param>
        public void RemoveUserFromGroup(string userCommonName, string groupCommonName)
        {
            System.DirectoryServices.DirectoryEntry oGroup = this.AD.Children.Find(groupCommonName, "group");

            try
            {
                object members = oGroup.Invoke("Members", null);
                foreach (object member in (System.Collections.IEnumerable)members)
                {
                    //获取该组的每个成员
                    System.DirectoryServices.DirectoryEntry x = new System.DirectoryServices.DirectoryEntry(member);

                    if (userCommonName == x.Name) //要移除的用户存在的话，则从该组中移除。
                    {
                        System.DirectoryServices.DirectoryEntry User = this.AD.Children.Find(userCommonName, "user");//找到该用户
                        oGroup.Invoke("Remove", new object[] { User.Path });
                        User.Close();
                    }
                }
            }
            finally
            {
                oGroup.Close();
            }

        }
        #endregion

    }

    /// <summary>
    /// 用户信息结构描述。
    /// </summary>
    public struct UserInfo
    {
        private string _UserName;
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName
        {
            get
            {
                return this._UserName;
            }
        }
        private string _FullName;
        /// <summary>
        /// 用户全名。
        /// </summary>
        public string FullName
        {
            get
            {
                return this._FullName;
            }
        }
        private string _Description;
        /// <summary>
        /// 用户描述。
        /// </summary>
        public string Description
        {
            get
            {
                return this._Description;
            }
        }
        private string _HomeDirectory;
        /// <summary>
        /// 用户主文件夹路径。
        /// </summary>
        public string HomeDirectory
        {
            get
            {
                return this._HomeDirectory;
            }
        }
        private bool _PasswordExpired;
        /// <summary>
        /// 用户下次登录时需更改密码
        /// </summary>
        public bool PasswordExpired
        {
            get
            {
                return this._PasswordExpired;
            }
        }
        private bool _DontExpirePassword;
        /// <summary>
        /// 设置用户密码永不过期
        /// </summary>
        public bool DontExpirePassword
        {
            get
            {
                return this._DontExpirePassword;
            }
        }
        private bool _ChangePassword;
        /// <summary>
        /// 用户不能更改密码
        /// </summary>
        public bool ChangePassword
        {
            get
            {
                return this._ChangePassword;
            }
        }
        private bool _EnableUser;
        /// <summary>
        /// 启用指定的用户
        /// </summary>
        public bool EnableUser
        {
            get
            {
                return this._EnableUser;
            }
        }
        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="FullName">用户全名。</param>
        /// <param name="Description">用户描述。</param>
        /// <param name="HomeDirectory">用户主文件夹路径。</param>
        /// <param name="PasswordExpired">用户下次登录时需更改密码</param>
        /// <param name="DontExpirePassword">设置用户密码永不过期</param>
        /// <param name="ChangePassword">用户不能更改密码</param>
        /// <param name="EnableUser">启用指定的用户</param>
        public UserInfo(string UserName, string FullName, string Description, string HomeDirectory, bool PasswordExpired, bool DontExpirePassword, bool ChangePassword, bool EnableUser)
        {
            this._UserName = UserName;
            this._FullName = FullName;
            this._Description = Description;
            this._HomeDirectory = HomeDirectory;
            this._PasswordExpired = PasswordExpired;
            this._DontExpirePassword = DontExpirePassword;
            this._ChangePassword = ChangePassword;
            this._EnableUser = EnableUser;

        }

    }

    /// <summary>
    /// 用户组信息结构描述。
    /// </summary>
    public struct GroupInfo
    {
        private string _GroupName;
        /// <summary>
        /// 用户组名称
        /// </summary>
        public string GroupName
        {
            get
            {
                return this._GroupName;
            }
        }
        private string _Description;
        /// <summary>
        /// 描述。
        /// </summary>
        public string Description
        {
            get
            {
                return this._Description;
            }
        }
        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="GroupName">用户名</param>
        /// <param name="Description">描述。</param>
        public GroupInfo(string GroupName, string Description)
        {
            this._GroupName = GroupName;
            this._Description = Description;

        }
    }


}
