namespace Thinksea.SQL
{
    /// <summary>
    /// SQL Server 连接类型。
    /// </summary>
    public enum SQLServerConnectionType
    {
        /// <summary>
        /// Windows 身份验证
        /// </summary>
        WindowsConnection,
        /// <summary>
        /// SQL Server 身份验证
        /// </summary>
        SQLServerConnection
    }

    /// <summary>
    /// 微软 SQL Server 管理类。
    /// </summary>
    public class MSSQLServer
    {
        private string _DataSource = "(local)";
        /// <summary>
        /// 获取或设置数据源。默认值为“(local)”。
        /// </summary>
        public string DataSource
        {
            get
            {
                return this._DataSource;
            }
            set
            {
                this._DataSource = value;
            }
        }
        private SQLServerConnectionType _ConnectionType = SQLServerConnectionType.SQLServerConnection;
        /// <summary>
        /// SQL Server 连接类型。当设置为 SQL Server 身份验证“SQLServerConnectionType.SQLServerConnection”时将使用属性“UserID”和“Password”连接到数据库执行 SQL 操作。
        /// </summary>
        public SQLServerConnectionType ConnectionType
        {
            get
            {
                return this._ConnectionType;
            }
            set
            {
                this._ConnectionType = value;
            }
        }
        private string _UserID;
        /// <summary>
        /// 获取或设置登录用户名。当属性“ConnectionType”设置为 SQL Server 身份验证“SQLServerConnectionType.SQLServerConnection”时使用此属性进行连接。
        /// </summary>
        public string UserID
        {
            get
            {
                return this._UserID;
            }
            set
            {
                this._UserID = value;
            }
        }
        private string _Password;
        /// <summary>
        /// 获取或设置登录密码。当属性“ConnectionType”设置为 SQL Server 身份验证“SQLServerConnectionType.SQLServerConnection”时使用此属性进行连接。
        /// </summary>
        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
            }
        }

        /// <summary>
        /// 初始化此实例。
        /// </summary>
        public MSSQLServer()
        {
        }
        /// <summary>
        /// 初始化此实例。
        /// </summary>
        /// <param name="DataSource">数据源。默认值为“(local)”。</param>
        public MSSQLServer(string DataSource)
        {
            this.DataSource = DataSource;

        }
        /// <summary>
        /// 初始化此实例。
        /// </summary>
        /// <param name="DataSource">数据源。默认值为“(local)”。</param>
        /// <param name="UserID">登录用户名。</param>
        /// <param name="Password">登录密码。</param>
        public MSSQLServer(string DataSource, string UserID, string Password)
        {
            this.DataSource = DataSource;
            this.UserID = UserID;
            this.Password = Password;

        }

        /// <summary>
        /// 使用当前设置创建一个数据库连接，初始连接到指定的数据库。
        /// </summary>
        /// <param name="DataBase">数据库名称。</param>
        /// <returns>一个数据库连接。</returns>
        private System.Data.SqlClient.SqlConnection CreateConnection(string DataBase)
        {
            if (this.ConnectionType == SQLServerConnectionType.WindowsConnection)
            {
                return new System.Data.SqlClient.SqlConnection("server='" + this.DataSource.Replace("'", "''") + "'; database='" + DataBase.Replace("'", "''") + "'; Trusted_Connection=Yes;");
            }
            else if (this.ConnectionType == SQLServerConnectionType.SQLServerConnection)
            {
                return new System.Data.SqlClient.SqlConnection("Data Source='" + this.DataSource.Replace("'", "''") + "'; Initial Catalog='" + DataBase.Replace("'", "''") + "'; User ID='" + this.UserID.Replace("'", "''") + "'; Password='" + this.Password.Replace("'", "''") + "';");
            }
            return new System.Data.SqlClient.SqlConnection("server='" + this.DataSource.Replace("'", "''") + "'; database='" + DataBase.Replace("'", "''") + "'; Trusted_Connection=Yes;");

        }

        /// <summary>
        /// 检测当前机器是否安装 SQL2000 方法。
        /// 注意：当前版本，Web 应用程序访问此方法需要服务器为 Web 应用程序授权（通过“System.ServiceProcess.dll”）使其允许访问 Windows 服务。
        /// </summary>
        /// <returns></returns>
        public static bool IsInstallSqlServer()
        {
            bool ExistFlag = false;
            System.ServiceProcess.ServiceController[] service = System.ServiceProcess.ServiceController.GetServices();
            for (int i = 0; i < service.Length; i++)
            {
                if (service[i].ServiceName == "MSSQLSERVER")
                {
                    ExistFlag = true;
                }
            }
            return ExistFlag;
        }


        /// <summary>
        /// 测试以当前提供的资料能否成功连接到数据库服务器上的指定数据库。
        /// </summary>
        /// <param name="DataBase">数据库名称。</param>
        /// <returns>连接成功返回 True；否则返回 False。</returns>
        public bool TestSQLConnection(string DataBase)
        {
            System.Data.SqlClient.SqlConnection Connection = this.CreateConnection(DataBase);
            try
            {
                Connection.Open();
                Connection.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                Connection.Dispose();
            }

        }
        /// <summary>
        /// 测试以当前提供的资料能否成功连接到数据库服务器。
        /// </summary>
        /// <returns>连接成功返回 True；否则返回 False。</returns>
        public bool TestSQLConnection()
        {
            return TestSQLConnection("master");

        }

        /// <summary>
        /// 判断指定的 SQL 数据库是否已经存在。
        /// </summary>
        /// <param name="DataBase">数据库名称。</param>
        /// <returns>存在返回 true；否则返回 false。</returns>
        public bool ExistsSQLDataBase(string DataBase)
        {
            System.Data.SqlClient.SqlConnection Connection = this.CreateConnection("master");// new System.Data.SqlClient.SqlConnection("Data Source=" + this.DataSource + "; Initial Catalog = master; User ID=" + UserID + "; Password=" + Password + ";");
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlDataReader sdr = null;
            sqlCommand.Connection = Connection;
            sqlCommand.CommandText = "select [Name] from master.dbo.sysdatabases where [Name]=N'" + DataBase + "'";

            Connection.Open();
            try
            {
                sdr = sqlCommand.ExecuteReader();
                try
                {
                    if (sdr.Read())
                    {
                        return true;
                    }
                }
                finally
                {
                    sdr.Close();
                }
            }
            finally
            {
                Connection.Close();
            }
            return false;

        }
        /// <summary>
        /// 获取 SQL 数据库列表。
        /// </summary>
        /// <returns>数据库列表。</returns>
        public string[] GetSQLDataBases()
        {
            System.Collections.Generic.List<string> l = new System.Collections.Generic.List<string>();
            System.Data.SqlClient.SqlConnection Connection = this.CreateConnection("master");
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlDataReader sdr = null;
            sqlCommand.Connection = Connection;
            sqlCommand.CommandText = "select [Name] from master.dbo.sysdatabases where [dbid]>6";

            Connection.Open();
            try
            {
                sdr = sqlCommand.ExecuteReader();
                try
                {
                    while (sdr.Read())
                    {
                        l.Add(sdr["Name"].ToString());
                    }
                }
                finally
                {
                    sdr.Close();
                }
            }
            finally
            {
                Connection.Close();
            }
            return l.ToArray();

        }

        /// <summary>
        /// 判断指定的登录用户是否已经存在。
        /// </summary>
        /// <param name="UserID">登录用户名。</param>
        /// <returns>存在返回 true；否则返回 false。</returns>
        public bool ExistsSQLUser(string UserID)
        {
            System.Data.SqlClient.SqlConnection Connection = this.CreateConnection("master");
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlDataReader sdr = null;
            sqlCommand.Connection = Connection;
            sqlCommand.CommandText = @"select [Name] from master.dbo.syslogins where [Name]=N'" + Thinksea.General.FixSQLCommandText(UserID) + @"'";

            Connection.Open();
            try
            {
                sdr = sqlCommand.ExecuteReader();
                try
                {
                    if (sdr.Read())
                    {
                        return true;
                    }
                }
                finally
                {
                    sdr.Close();
                }
            }
            finally
            {
                Connection.Close();
            }
            return false;

        }
        /// <summary>
        /// 获取 SQL 登录用户列表。
        /// </summary>
        /// <returns>用户列表。</returns>
        public string[] GetSQLUsers()
        {
            System.Collections.Generic.List<string> l = new System.Collections.Generic.List<string>();
            System.Data.SqlClient.SqlConnection Connection = this.CreateConnection("master");
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
            System.Data.SqlClient.SqlDataReader sdr = null;
            sqlCommand.Connection = Connection;
            sqlCommand.CommandText = "select [Name] from master.dbo.syslogins";

            Connection.Open();
            try
            {
                sdr = sqlCommand.ExecuteReader();
                try
                {
                    while (sdr.Read())
                    {
                        l.Add(sdr["Name"].ToString());
                    }
                }
                finally
                {
                    sdr.Close();
                }
            }
            finally
            {
                Connection.Close();
            }
            return l.ToArray();

        }

        /// <summary>
        /// 创建 SQL 数据库。
        /// </summary>
        /// <param name="DataBaseName">数据库名称。</param>
        /// <param name="Path">数据库文件存放目录。</param>
        public void CreateSQLDataBase(string DataBaseName, string Path)
        {
            DataBaseName = Thinksea.General.FixSQLCommandText(DataBaseName);
            Path = Thinksea.General.FixSQLCommandText(Path);

            Path = System.IO.Path.Combine(Path, DataBaseName);
            string SQL = @"CREATE DATABASE [" + DataBaseName + @"]
ON
PRIMARY ( NAME = [" + DataBaseName + @"_Data],
      FILENAME = N'" + Path + @"_Data.mdf')
LOG ON 
( NAME = [" + DataBaseName + @"_Log],
   FILENAME = N'" + Path + @"_Log.ldf')
";
            System.Data.SqlClient.SqlConnection conn = this.CreateConnection("master");// new System.Data.SqlClient.SqlConnection("Data Source=" + this.DataSource + "; Initial Catalog = master; User ID=" + SAUserID + "; Password=" + SAPassword + ";");
            conn.Open();
            try
            {
                Thinksea.General.ExecuteSQL(conn, SQL);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }

        }
        /// <summary>
        /// 创建 SQL 数据库。
        /// </summary>
        /// <param name="DataBaseName">数据库名称。</param>
        public void CreateSQLDataBase(string DataBaseName)
        {
            string SQL = @"CREATE DATABASE [" + Thinksea.General.FixSQLCommandText(DataBaseName) + @"]";
            System.Data.SqlClient.SqlConnection conn = this.CreateConnection("master");// new System.Data.SqlClient.SqlConnection("server=" + this.DataSource + "; database=master; Trusted_Connection=Yes;");
            conn.Open();
            try
            {
                Thinksea.General.ExecuteSQL(conn, SQL);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }

        }
        /// <summary>
        /// 创建 SQL 数据库。
        /// </summary>
        /// <param name="DataBaseName">数据库名称。</param>
        /// <param name="Path">数据库文件存放目录。</param>
        /// <param name="UserID">用于管理此数据库的登录用户名。</param>
        /// <param name="Password">用于管理此数据库的登录密码。</param>
        public void CreateSQLDataBase(string DataBaseName, string Path, string UserID, string Password)
        {
            DataBaseName = Thinksea.General.FixSQLCommandText(DataBaseName);
            Path = Thinksea.General.FixSQLCommandText(Path);
            UserID = Thinksea.General.FixSQLCommandText(UserID);
            Password = Thinksea.General.FixSQLCommandText(Password);

            Path = System.IO.Path.Combine(Path, DataBaseName);
            string SQL = @"CREATE DATABASE [" + DataBaseName + @"]
ON
PRIMARY ( NAME = [" + DataBaseName + @"_Data],
      FILENAME = N'" + Path + @"_Data.mdf')
LOG ON 
( NAME = [" + DataBaseName + @"_Log],
   FILENAME = N'" + Path + @"_Log.ldf')
GO

use [" + DataBaseName + @"]
GO
EXEC sp_addlogin N'" + UserID + @"', N'" + Password + @"', N'" + DataBaseName + @"'
EXEC sp_grantdbaccess N'" + UserID + @"'
EXEC sp_addrolemember 'db_owner', N'" + UserID + @"'
";
            System.Data.SqlClient.SqlConnection conn = this.CreateConnection("master");// new System.Data.SqlClient.SqlConnection("Data Source=" + this.DataSource + "; Initial Catalog = master; User ID=" + SAUserID + "; Password=" + SAPassword + ";");
            conn.Open();
            try
            {
                Thinksea.General.ExecuteSQL(conn, SQL);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }

        }
        /// <summary>
        /// 创建 SQL 数据库。
        /// </summary>
        /// <param name="DataBaseName">数据库名称。</param>
        /// <param name="UserID">用于管理此数据库的登录用户名。</param>
        /// <param name="Password">用于管理此数据库的登录密码。</param>
        public void CreateSQLDataBase(string DataBaseName, string UserID, string Password)
        {
            DataBaseName = Thinksea.General.FixSQLCommandText(DataBaseName);
            UserID = Thinksea.General.FixSQLCommandText(UserID);
            Password = Thinksea.General.FixSQLCommandText(Password);

            string SQL = @"CREATE DATABASE [" + DataBaseName + @"]
GO

use [" + DataBaseName + @"]
GO
EXEC sp_addlogin N'" + UserID + @"', N'" + Password + @"', N'" + DataBaseName + @"'
EXEC sp_grantdbaccess N'" + UserID + @"'
EXEC sp_addrolemember 'db_owner', N'" + UserID + @"'
";
            System.Data.SqlClient.SqlConnection conn = this.CreateConnection("master");// new System.Data.SqlClient.SqlConnection("server=" + this.DataSource + "; database=master; Trusted_Connection=Yes;");
            conn.Open();
            try
            {
                Thinksea.General.ExecuteSQL(conn, SQL);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }

        }

        /// <summary>
        /// 创建 SQL 登录用户。
        /// </summary>
        /// <param name="UserID">登录用户名。</param>
        /// <param name="Password">登录密码。</param>
        public void CreateSQLUser(string UserID, string Password)
        {
            string SQL = @"EXEC sp_addlogin N'" + Thinksea.General.FixSQLCommandText(UserID) + @"', N'" + Thinksea.General.FixSQLCommandText(Password) + @"'";
            System.Data.SqlClient.SqlConnection conn = this.CreateConnection("master");
            conn.Open();
            try
            {
                Thinksea.General.ExecuteSQL(conn, SQL);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }

        }
        /// <summary>
        /// 创建 SQL 登录用户。
        /// </summary>
        /// <param name="UserID">登录用户名。</param>
        /// <param name="Password">登录密码。</param>
        /// <param name="DefaultDataBase">登录的默认数据库（登录后登录所连接到的数据库）。</param>
        public void CreateSQLUser(string UserID, string Password, string DefaultDataBase)
        {
            string SQL = @"EXEC sp_addlogin N'" + Thinksea.General.FixSQLCommandText(UserID) + @"', N'" + Thinksea.General.FixSQLCommandText(Password) + @"', N'" + DefaultDataBase + @"'";
            System.Data.SqlClient.SqlConnection conn = this.CreateConnection("master");
            conn.Open();
            try
            {
                Thinksea.General.ExecuteSQL(conn, SQL);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }

        }
        /// <summary>
        /// 在指定的数据库中添加一个已经存在的用户，使其拥有访问此数据库的权限。
        /// </summary>
        /// <param name="DataBaseName">数据库名称。</param>
        /// <param name="UserID">用于管理此数据库的登录用户名。</param>
        public void GrantDBAccess(string DataBaseName, string UserID)
        {
            UserID = Thinksea.General.FixSQLCommandText(UserID);

            string SQL = @"EXEC sp_grantdbaccess N'" + UserID + @"'
EXEC sp_addrolemember 'db_owner', N'" + UserID + @"'
";
            System.Data.SqlClient.SqlConnection conn = this.CreateConnection(DataBaseName);
            conn.Open();
            try
            {
                Thinksea.General.ExecuteSQL(conn, SQL);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }

        }
        /// <summary>
        /// 从指定的数据库中删除安全帐户，禁止此用户访问指定的数据库。
        /// </summary>
        /// <param name="DataBaseName">数据库名称。</param>
        /// <param name="UserID">用于管理此数据库的登录用户名。</param>
        public void RevokeDBAccess(string DataBaseName, string UserID)
        {
            UserID = Thinksea.General.FixSQLCommandText(UserID);

            string SQL = @"EXEC sp_revokedbaccess '" + UserID + @"'";
            System.Data.SqlClient.SqlConnection conn = this.CreateConnection(DataBaseName);
            conn.Open();
            try
            {
                Thinksea.General.ExecuteSQL(conn, SQL);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }

        }

        /// <summary>
        /// 删除数据库。
        /// </summary>
        /// <param name="DataBaseName">数据库名称。</param>
        public void DeleteSQLDataBase(string DataBaseName)
        {
            string SQL = @"drop DATABASE [" + Thinksea.General.FixSQLCommandText(DataBaseName) + @"]";
            System.Data.SqlClient.SqlConnection conn = this.CreateConnection("master");
            conn.Open();
            try
            {
                Thinksea.General.ExecuteSQL(conn, SQL);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }

        }
        /// <summary>
        /// 删除登录用户。
        /// </summary>
        /// <param name="UserID">登录用户名。</param>
        public void DeleteSQLUser(string UserID)
        {
            string SQL = @"EXEC sp_droplogin N'" + Thinksea.General.FixSQLCommandText(UserID) + @"'";
            System.Data.SqlClient.SqlConnection conn = this.CreateConnection("master");
            conn.Open();
            try
            {
                Thinksea.General.ExecuteSQL(conn, SQL);
            }
            finally
            {
                conn.Close();
                //conn.Dispose();
            }

        }

        /// <summary>
        /// 批量执行 SQL 代码。（支持 GO 语句）
        /// </summary>
        /// <param name="DataBaseName">数据库名称。</param>
        /// <param name="SQLString">SQL 代码。</param>
        public void ExecuteSQL(string DataBaseName, string SQLString)
        {
            System.Data.SqlClient.SqlConnection conn = this.CreateConnection(DataBaseName);
            conn.Open();
            try
            {
                Thinksea.General.ExecuteSQL(conn, SQLString);
            }
            finally
            {
                conn.Close();
            }

        }

    }
}
