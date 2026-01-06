namespace Thinksea.SQL
{
    /// <summary>
    /// 此类封装了备份/还原数据的方法。
    /// </summary>
    public class BackupOrRestoreDataBase
    {
        /// <summary>
        /// 文档版本。主要用来实现文档版本识别和控制，一般备份数据时写入文档生成器版本号，还原数据时通过版本号判断能否正确导入数据支持。
        /// </summary>
        public System.Version Version
        {
            get;
            set;
        }

        /// <summary>
        /// 文档生成器描述。
        /// </summary>
        public string Generator
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public BackupOrRestoreDataBase()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
            this.Version = new System.Version();
            this.Generator = "";

        }

        /// <summary>
        /// 获取全部的数据表名称集合。
        /// </summary>
        /// <param name="Connection">已经打开的数据库连接。</param>
        /// <returns>数据表名称集合。</returns>
        private string[] GetTables(Microsoft.Data.SqlClient.SqlConnection Connection)
        {
            System.Collections.Generic.List<string> tables = new System.Collections.Generic.List<string>();

			Microsoft.Data.SqlClient.SqlDataReader sdr = null;
			Microsoft.Data.SqlClient.SqlCommand comm = new Microsoft.Data.SqlClient.SqlCommand();
            comm.Connection = Connection;
            comm.CommandText = @"SELECT name FROM sysobjects where xtype='U' and name<>'dtproperties' order by name asc";
            sdr = comm.ExecuteReader();
            try
            {
                while (sdr.Read())
                {
                    tables.Add(sdr["name"].ToString());
                }
            }
            finally
            {
                sdr.Close();
            }
            return tables.ToArray();

        }

        /// <summary>
        /// 写入备份注释和版本信息。
        /// </summary>
        /// <param name="xtw">XML 书写器。</param>
        /// <param name="Version">文档版本。主要用来实现文档版本识别和控制，一般备份数据时写入文档生成器版本号，还原数据时通过版本号判断能否正确导入数据支持。</param>
        /// <param name="Description">文档备注。</param>
        /// <param name="Generator">文档生成器描述。</param>
        private void WriteVersion(System.Xml.XmlTextWriter xtw, System.Version Version, string Description, string Generator)
        {
            xtw.WriteStartElement("Version");
            xtw.WriteElementString("Version", Version.ToString());
            xtw.WriteElementString("DataTime", System.DateTime.Now.ToString());
            xtw.WriteElementString("Description", Description);
            xtw.WriteElementString("Generator", Generator);

            xtw.WriteEndElement();
        }

        /// <summary>
        /// 备份指定的数据表。
        /// </summary>
        /// <param name="Connection">已经打开的数据库连接。</param>
        /// <param name="xtw">XML 书写器。</param>
        /// <param name="TableName">数据表名称。</param>
        private void BackupTable(Microsoft.Data.SqlClient.SqlConnection Connection, System.Xml.XmlTextWriter xtw, string TableName)
        {
            xtw.WriteStartElement(TableName);
			Microsoft.Data.SqlClient.SqlDataReader sdr = null;
			Microsoft.Data.SqlClient.SqlCommand comm = new Microsoft.Data.SqlClient.SqlCommand();
            comm.Connection = Connection;

            comm.CommandText = @"SELECT * FROM [" + TableName + "]";
            sdr = comm.ExecuteReader();

            try
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                {
                    xtw.WriteAttributeString(sdr.GetName(i), sdr.GetDataTypeName(i));
                }
                while (sdr.Read())
                {
                    xtw.WriteStartElement("Row");
                    for (int i = 0; i < sdr.FieldCount; i++)
                    {
                        switch ((System.Data.SqlDbType)System.Enum.Parse(typeof(System.Data.SqlDbType), sdr.GetDataTypeName(i), true))
                        {
                            case System.Data.SqlDbType.BigInt:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.Bit:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.Date:
                            case System.Data.SqlDbType.DateTime:
                            case System.Data.SqlDbType.DateTime2:
                            case System.Data.SqlDbType.SmallDateTime:
                            case System.Data.SqlDbType.Time:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.DateTimeOffset:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.Decimal:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.Float:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.Int:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.Money:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.Char:
                            case System.Data.SqlDbType.Text:
                            case System.Data.SqlDbType.VarChar:
                            case System.Data.SqlDbType.NChar:
                            case System.Data.SqlDbType.NText:
                            case System.Data.SqlDbType.NVarChar:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.Real:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.SmallInt:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.SmallMoney:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.TinyInt:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.UniqueIdentifier:
                                xtw.WriteElementString(sdr.GetName(i), sdr[i].ToString());
                                break;
                            case System.Data.SqlDbType.Binary:
                                xtw.WriteElementString(sdr.GetName(i), System.Text.Encoding.UTF8.GetString(sdr.GetSqlBytes(i).Value));
                                break;
                            case System.Data.SqlDbType.VarBinary:
                                xtw.WriteElementString(sdr.GetName(i), System.Text.Encoding.UTF8.GetString(sdr.GetSqlBytes(i).Value));
                                break;
                            case System.Data.SqlDbType.Timestamp:
                                xtw.WriteElementString(sdr.GetName(i), System.Text.Encoding.UTF8.GetString(sdr.GetSqlBytes(i).Value));
                                break;
                            case System.Data.SqlDbType.Image:
                                xtw.WriteElementString(sdr.GetName(i), System.Text.Encoding.UTF8.GetString(sdr.GetSqlBytes(i).Value));
                                break;
                            case System.Data.SqlDbType.Structured://此类型操作待处理
                                break;
                            case System.Data.SqlDbType.Udt://此类型操作待处理
                                break;
                            case System.Data.SqlDbType.Variant://此类型操作待处理
                                break;
                            case System.Data.SqlDbType.Xml://此类型操作待处理
                                break;
                        }
                    }
                    xtw.WriteEndElement();
                }
            }
            finally
            {
                sdr.Close();
            }
            xtw.WriteEndElement();

        }

        /// <summary>
        /// 备份全部的数据表到 XML 文件。
        /// </summary>
        /// <param name="Connection">已经打开的数据库连接。</param>
        /// <param name="XMLFile">XML 文件名。</param>
        /// <param name="Description">文档备注。</param>
        public void BackupAllTables(Microsoft.Data.SqlClient.SqlConnection Connection, string XMLFile, string Description)
        {
            System.Xml.XmlTextWriter xtw = new System.Xml.XmlTextWriter(XMLFile, System.Text.Encoding.UTF8);
            try
            {
                xtw.Formatting = System.Xml.Formatting.Indented;
                xtw.WriteStartDocument();
                xtw.WriteStartElement("Root");
                this.WriteVersion(xtw, Version, Description, Generator);

                string[] tables = this.GetTables(Connection);
                foreach (string tmp in tables)
                {
                    this.BackupTable(Connection, xtw, tmp);
                }
                xtw.WriteEndElement();
                xtw.WriteEndDocument();
            }
            finally
            {
                xtw.Close();
            }
        }

        /// <summary>
        /// 读取备份注释和版本信息。
        /// </summary>
        /// <param name="XmlDoc">XML 文档。</param>
        private VersionInfo ReadVersion(System.Xml.XmlDocument XmlDoc)
        {
            System.Xml.XmlNodeList XmlNodeList = XmlDoc.SelectNodes(@"/Root/Version");
            if (XmlNodeList.Count == 0)
            {
                return null;
            }

            System.Version Version = new System.Version("0.0.0.0");
            System.DateTime DataTime = System.DateTime.MinValue;
            string Description = "";
            string Generator = "";

            foreach (System.Xml.XmlNode tmp2 in XmlNodeList)
            {
                System.Xml.XmlNode xnVersion = tmp2.SelectSingleNode("Version");
                if (xnVersion != null)
                {
                    Version = new System.Version(xnVersion.InnerText);
                }

                System.Xml.XmlNode xnDataTime = tmp2.SelectSingleNode("DataTime");
                if (xnDataTime != null)
                {
                    DataTime = System.DateTime.Parse(xnDataTime.InnerText);
                }

                System.Xml.XmlNode xnDescription = tmp2.SelectSingleNode("Description");
                if (xnDescription != null)
                {
                    Description = xnDescription.InnerText;
                }

                System.Xml.XmlNode xnGenerator = tmp2.SelectSingleNode("Generator");
                if (xnGenerator != null)
                {
                    Generator = xnGenerator.InnerText;
                }

            }

            return new VersionInfo(Version, DataTime, Description, Generator);

        }

        /// <summary>
        /// 读取备份注释和版本信息。
        /// </summary>
        /// <param name="XMLFile">XML 文件名。</param>
        public VersionInfo ReadVersion(string XMLFile)
        {
            System.Xml.XmlDocument Doc = new System.Xml.XmlDocument();
            try
            {
                Doc.Load(XMLFile);
            }
            catch
            {
                throw new System.Exception("文档不是有效的数据备份文件或文件数据已经损坏");
            }

            return this.ReadVersion(Doc);

        }

        /// <summary>
        /// 还原指定的数据表。
        /// </summary>
        /// <param name="Connection">已经打开的数据库连接。</param>
        /// <param name="XmlDoc">XML 文档。</param>
        /// <param name="TableName">数据表名称。</param>
        private void RestoreTable(Microsoft.Data.SqlClient.SqlConnection Connection, System.Xml.XmlDocument XmlDoc, string TableName)
        {
            System.Xml.XmlNodeList XmlNodeList = XmlDoc.SelectNodes(@"/Root/" + TableName);
            if (XmlNodeList.Count == 0)
            {
                return;
            }

			Microsoft.Data.SqlClient.SqlCommand comm = new Microsoft.Data.SqlClient.SqlCommand();
            comm.Connection = Connection;

            bool HasIdentity = false;//表是否有标识列。
			#region 判断指定的表是否有标识列。
			Microsoft.Data.SqlClient.SqlDataReader sdr = null;
            comm.CommandText = @"select OBJECTPROPERTY(OBJECT_ID(N'" + Thinksea.General.FixSQLCommandText(TableName) + @"'),'TableHasIdentity')";//判断指定的表是否有标识列。
            sdr = comm.ExecuteReader();
            try
            {
                if (sdr.Read())
                {
                    if (sdr.GetInt32(0) == 1)
                    {
                        HasIdentity = true;
                    }
                }
            }
            finally
            {
                sdr.Close();
            }
            #endregion

            comm.CommandText = @"DELETE FROM [" + Thinksea.General.FixSQLFieldName(TableName) + "]";
            comm.ExecuteNonQuery();

            if (HasIdentity)
            {
                comm.CommandText = @"SET IDENTITY_INSERT [" + Thinksea.General.FixSQLFieldName(TableName) + "] ON";
                comm.ExecuteNonQuery();
            }

            foreach (System.Xml.XmlNode tmp2 in XmlNodeList)
            {
                #region 生成命令。
                if (tmp2.Attributes.Count > 0)
                {
                    comm.Parameters.Clear();
                    string sql = "";
                    string sql2 = "";
                    foreach (System.Xml.XmlAttribute attr in tmp2.Attributes)
                    {
                        if (sql == "")
                        {
                            sql = "[" + attr.Name + "]";
                            sql2 = "@" + attr.Name;
                        }
                        else
                        {
                            sql += ", [" + attr.Name + "]";
                            sql2 += ", @" + attr.Name;
                        }
						Microsoft.Data.SqlClient.SqlParameter sp = new Microsoft.Data.SqlClient.SqlParameter("@" + attr.Name, (System.Data.SqlDbType)System.Enum.Parse(typeof(System.Data.SqlDbType), attr.Value, true));
                        sp.SourceColumn = attr.Name;
                        comm.Parameters.Add(sp);
                    }
                    comm.CommandText = "INSERT INTO [" + TableName + "] (" + sql + ") VALUES (" + sql2 + ")";
                }
                #endregion

                #region 导入数据。
                foreach (System.Xml.XmlNode row in tmp2.ChildNodes)
                {
                    foreach (System.Xml.XmlNode column in row.ChildNodes)
                    {
						Microsoft.Data.SqlClient.SqlParameter sp = comm.Parameters["@" + column.Name];
                        switch (sp.SqlDbType)
                        {
                            case System.Data.SqlDbType.BigInt:
                                sp.Value = System.Convert.ToInt64(column.InnerText);
                                break;
                            case System.Data.SqlDbType.Bit:
                                sp.Value = System.Convert.ToBoolean(column.InnerText);
                                //sp.Value = bool.Parse(column.InnerText);
                                break;
                            case System.Data.SqlDbType.Date:
                            case System.Data.SqlDbType.DateTime:
                            case System.Data.SqlDbType.DateTime2:
                            case System.Data.SqlDbType.SmallDateTime:
                            case System.Data.SqlDbType.Time:
                                sp.Value = System.DateTime.Parse(column.InnerText);
                                break;
                            case System.Data.SqlDbType.DateTimeOffset:
                                sp.Value = System.DateTimeOffset.Parse(column.InnerText);
                                break;
                            case System.Data.SqlDbType.Decimal:
                                sp.Value = System.Convert.ToDecimal(column.InnerText);
                                break;
                            case System.Data.SqlDbType.Float:
                                sp.Value = System.Convert.ToDouble(column.InnerText);
                                break;
                            case System.Data.SqlDbType.Int:
                                sp.Value = System.Convert.ToInt32(column.InnerText);
                                break;
                            case System.Data.SqlDbType.Money:
                                sp.Value = System.Convert.ToDecimal(column.InnerText);
                                break;
                            case System.Data.SqlDbType.Char:
                            case System.Data.SqlDbType.Text:
                            case System.Data.SqlDbType.VarChar:
                            case System.Data.SqlDbType.NChar:
                            case System.Data.SqlDbType.NText:
                            case System.Data.SqlDbType.NVarChar:
                                sp.Value = column.InnerText;
                                break;
                            case System.Data.SqlDbType.Real:
                                sp.Value = System.Convert.ToSingle(column.InnerText);
                                break;
                            case System.Data.SqlDbType.SmallInt:
                                sp.Value = System.Convert.ToInt16(column.InnerText);
                                break;
                            case System.Data.SqlDbType.SmallMoney:
                                sp.Value = System.Convert.ToDecimal(column.InnerText);
                                break;
                            case System.Data.SqlDbType.TinyInt:
                                sp.Value = System.Convert.ToByte(column.InnerText);
                                break;
                            case System.Data.SqlDbType.UniqueIdentifier:
                                sp.Value = new System.Guid(column.InnerText);
                                break;
                            case System.Data.SqlDbType.Binary:
                                sp.Value = System.Text.Encoding.UTF8.GetBytes(column.InnerText);
                                break;
                            case System.Data.SqlDbType.VarBinary:
                                sp.Value = System.Text.Encoding.UTF8.GetBytes(column.InnerText);
                                break;
                            case System.Data.SqlDbType.Timestamp:
                                sp.Value = System.Text.Encoding.UTF8.GetBytes(column.InnerText);
                                break;
                            case System.Data.SqlDbType.Image:
                                sp.Value = System.Text.Encoding.UTF8.GetBytes(column.InnerText);
                                break;
                            case System.Data.SqlDbType.Structured://此类型操作待处理
                                break;
                            case System.Data.SqlDbType.Udt://此类型操作待处理
                                break;
                            case System.Data.SqlDbType.Variant://此类型操作待处理
                                break;
                            case System.Data.SqlDbType.Xml://此类型操作待处理
                                break;
                        }
                    }
                    comm.ExecuteNonQuery();

                }
                #endregion
            }

            if (HasIdentity)
            {
                comm.Parameters.Clear();
                comm.CommandText = @"SET IDENTITY_INSERT [" + Thinksea.General.FixSQLFieldName(TableName) + "] OFF";
                comm.ExecuteNonQuery();
            }

        }

        /// <summary>
        /// 还原全部的数据表从 XML 文件。
        /// </summary>
        /// <param name="Connection">已经打开的数据库连接。</param>
        /// <param name="XMLFile">XML 文件名。</param>
        public void RestoreAllTables(Microsoft.Data.SqlClient.SqlConnection Connection, string XMLFile)
        {
            System.Xml.XmlDocument Doc = new System.Xml.XmlDocument();
            try
            {
                Doc.Load(XMLFile);
            }
            catch
            {
                throw new System.Exception("文档不是 " + Generator + " 的数据备份文件或文件数据已经损坏");
            }

            if (Doc.HasChildNodes)
            {
                try
                {
                    #region 数据文件检查。
                    VersionInfo vi;
                    try
                    {
                        vi = this.ReadVersion(Doc);
                    }
                    catch
                    {
                        throw new System.Exception("文档不是 " + Generator + " 的数据备份文件或文件数据已经损坏");
                    }
                    if (vi == null)
                    {
                        throw new System.Exception("文档不是 " + Generator + " 的数据备份文件或文件数据已经损坏");
                    }
                    if (vi.Version > Version)
                    {
                        throw new System.Exception("无法将高版本数据导入到低版本系统中，数据文件的版本号“" + vi.Version.ToString() + "”高于指定的版本号 “" + Version.ToString() + "”。");
                    }
                    #endregion

                    string[] tables = this.GetTables(Connection);
                    foreach (string tmp in tables)
                    {
                        this.RestoreTable(Connection, Doc, tmp);
                    }

                }
                finally
                {
                    Connection.Close();
                }
            }
            else
            {
                throw new System.Exception("文档不是有效的数据备份文件或文件数据已经损坏");
            }
        }
    }

    /// <summary>
    /// 文档版本信息结构。用于描述数据备份文档的特定信息。
    /// </summary>
    public class VersionInfo
    {
        private System.Version _Version;
        /// <summary>
        /// 获取文档版本。
        /// </summary>
        public System.Version Version
        {
            get
            {
                return this._Version;
            }
        }

        private System.DateTime _DataTime;
        /// <summary>
        /// 获取文档生成时间。
        /// </summary>
        public System.DateTime DataTime
        {
            get
            {
                return this._DataTime;
            }
        }
        private string _Description;
        /// <summary>
        /// 获取文档备注。
        /// </summary>
        public string Description
        {
            get
            {
                return this._Description;
            }
        }
        private string _Generator;
        /// <summary>
        /// 获取文档生成器说明。
        /// </summary>
        public string Generator
        {
            get
            {
                return this._Generator;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="Version">文档版本。</param>
        /// <param name="DataTime">文档生成时间。</param>
        /// <param name="Description">文档备注。</param>
        /// <param name="Generator">文档生成器说明。</param>
        public VersionInfo(System.Version Version, System.DateTime DataTime, string Description, string Generator)
        {
            this._Version = Version;
            this._DataTime = DataTime;
            this._Description = Description;
            this._Generator = Generator;

        }

    }

}
