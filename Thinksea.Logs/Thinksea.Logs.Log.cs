namespace Thinksea.Logs
{
    /// <summary>
    /// 日志输出位置。
    /// </summary>
    [System.Flags]
    public enum LogOutputType
    {
        /// <summary>
        /// 指示不输出日志。
        /// </summary>
        None = 0,
        /// <summary>
        /// 将日志输出到调试程序。
        /// </summary>
        Debugger = 1,
        /// <summary>
        /// 将日志输出到控制台。
        /// </summary>
        Console = 2,
        /// <summary>
        /// 将日志输出到日志文件。
        /// </summary>
        LogFile = 4,
        /// <summary>
        /// 将日志输出到日志事件。
        /// </summary>
        LogEvent = 8,
        /// <summary>
        /// 将日志输出到可以输出的任何地方（调试程序除外）。
        /// </summary>
        NotDebugger = Console | LogFile | LogEvent,
        /// <summary>
        /// 将日志输出到可以输出的任何地方。
        /// </summary>
        All = Debugger | Console | LogFile | LogEvent,
    }

    /// <summary>
    /// 日志类型。
    /// </summary>
    public enum LogFormatType
    {
        /// <summary>
        /// 具有纯文本格式的日志。
        /// </summary>
        Text = 0,
        /// <summary>
        /// 具有 XML 格式的日志，此设置要求开发人员书写日志内容时自己实现 XML 规则。
        /// </summary>
        XML = 1,
    }

    /// <summary>
    /// 定义日志级别类型。
    /// </summary>
    /// <remarks>
    /// 修改此类型定义时必须同步修改 <see cref="LogOutputLevel"/> 类型（详见其相关说明）。
    /// </remarks>
    public enum LogLevelType
    {
        /// <summary>
        /// 调试信息。
        /// </summary>
        Debug = 1,
        /// <summary>
        /// 信息
        /// </summary>
        Info = 2,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = 4,
        /// <summary>
        /// 错误。
        /// </summary>
        Error = 8,
        /// <summary>
        /// 致命错误。
        /// </summary>
        Fatal = 16,
    }

    /// <summary>
    /// 定义输出哪些日志。
    /// </summary>
    /// <remarks>
    /// 此类型定义必须完整包含类型 <see cref="LogLevelType"/> 的全部值。
    /// </remarks>
    [System.Flags]
    public enum LogOutputLevel
    {
        /// <summary>
        /// 指示不输出日志。(此值必须单独出现，不可与其他值搭配使用。)
        /// </summary>
        None = 0,
        /// <summary>
        /// 调试信息。
        /// </summary>
        Debug = LogLevelType.Debug,
        /// <summary>
        /// 信息
        /// </summary>
        Info = LogLevelType.Info,
        /// <summary>
        /// 警告
        /// </summary>
        Warning = LogLevelType.Warning,
        /// <summary>
        /// 错误。
        /// </summary>
        Error = LogLevelType.Error,
        /// <summary>
        /// 致命错误。
        /// </summary>
        Fatal = LogLevelType.Fatal,
        /// <summary>
        /// 任何日志信息都会被输出。(此值必须单独出现，不可与其他值搭配使用。)
        /// </summary>
        All = Debug | Info | Warning | Error | Fatal,
    }

    /// <summary>
    /// 定义一个日志实体信息类。
    /// </summary>
    public class LogEntity
    {
        private System.DateTime _DateTime = System.DateTime.Now;
        /// <summary>
        /// 获取或设置日志时间。
        /// </summary>
        public System.DateTime DateTime
        {
            get
            {
                return this._DateTime;
            }
            set
            {
                this._DateTime = value;
            }
        }

        /// <summary>
        /// 获取或设置日志内容。
        /// </summary>
        public string Message
        {
            get;
            set;
        }

        private LogLevelType _Level = LogLevelType.Info;
        /// <summary>
        /// 获取或设置日志级别。
        /// </summary>
        public LogLevelType Level
        {
            get
            {
                return this._Level;
            }
            set
            {
                this._Level = value;
            }
        }

        /// <summary>
        /// 获取或设置引发此事件的用户 ID。
        /// </summary>
        public string UserID
        {
            get;
            set;
        }

        private int _EventID = 0;
        /// <summary>
        /// 获取或设置错误号/事件 ID。
        /// </summary>
        public int EventID
        {
            get
            {
                return this._EventID;
            }
            set
            {
                this._EventID = value;
            }
        }

        /// <summary>
        /// 获取或设置事件来源。
        /// </summary>
        public string EventSourceName
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置导致事件的任务类别。
        /// </summary>
        public string TaskCategory
        {
            get;
            set;
        }

    }

    /// <summary>
    /// 封装了一个轻量级的日志功能。
    /// </summary>
    /// <remarks>
    /// 注意：日志文件虽然以日期命名，但是并不能精确保证记录写入时间与日志文件名所表示的日期相同。
    /// 原因主要有两点：
    /// 1、日志文件打开时间与写入时间有一定的微差，例如：位于零点的日期交割时间，此时日志文件打开时间位于零点之前，而日志写入时间却可能位于零点之后。
    /// 2、如果日志时间由用户指定，则无法保证与文件名所表示的日期相同。
    /// </remarks>
    [System.Obsolete("此组件从项目“Thinksea”中剥离，仅用于兼容旧项目的目的，如无必要将不再进行升级维护。推荐使用.NET项目自带的日志组件或 NLog 等同类产品替代。")]
    public class Log
    {
        /// <summary>
        /// 获取 XML 格式日志的开始标记。
        /// </summary>
        /// <returns></returns>
        private const string XMLDocBeginTag = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Logs>
";

        /// <summary>
        /// 获取 XML 格式日志的结束标记。
        /// </summary>
        /// <returns></returns>
        private const string XMLDocEndTag = @"</Logs>";

        private string _LogPath = "";
        /// <summary>
        /// 获取或设置日志文件存盘目录。
        /// </summary>
        public string LogPath
        {
            get
            {
                if (string.IsNullOrEmpty(this._LogPath))
                {
                    this._LogPath = System.IO.Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, @"Log\");
                }
                return this._LogPath;
            }
            set
            {
                this._LogPath = value;
            }
        }

        private int _MaxLogFileSize = 1024 * 10;
        /// <summary>
        /// 获取或设置单个日志文件最大尺寸。(单位：KB)
        /// </summary>
        public int MaxLogFileSize
        {
            get
            {
                return this._MaxLogFileSize;
            }
            set
            {
                this._MaxLogFileSize = value;
            }
        }

        private LogOutputType _OutputType = LogOutputType.All;
        /// <summary>
        /// 获取或设置日志输出位置。
        /// </summary>
        public LogOutputType OutputType
        {
            get
            {
                return this._OutputType;
            }
            set
            {
                this._OutputType = value;
            }
        }

        private LogOutputLevel _OutputLevel = LogOutputLevel.All;
        /// <summary>
        /// 获取或设置一个标识，指示输出哪些级别的日志。
        /// </summary>
        public LogOutputLevel OutputLevel
        {
            get
            {
                return this._OutputLevel;
            }
            set
            {
                this._OutputLevel = value;
            }
        }

        /// <summary>
        /// 将日志写入到线程的工作队列。
        /// </summary>
        private Thinksea.Collections.WaitQueue<LogEntity> wqLog;

        /// <summary>
        /// 用于将日志信息写入到输出设备的线程。
        /// </summary>
        private System.Threading.Thread thWriter;

        /// <summary>
        /// 获取或设置日志格式。
        /// </summary>
        public LogFormatType LogFormat
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public Log()
        {
            this.wqLog = new Thinksea.Collections.WaitQueue<LogEntity>();
            this.thWriter = new System.Threading.Thread(new System.Threading.ThreadStart(this.WriteMethod));
            this.thWriter.IsBackground = true;
            this.thWriter.Start();
        }

        /// <summary>
        /// 同步写入日志的锁功能实现。
        /// </summary>
        private object LockThis = new object();

        /// <summary>
        /// 获取指定日志的具有指定格式的数据。
        /// </summary>
        /// <param name="log">日志对象。</param>
        /// <param name="logFormat">日志格式。</param>
        /// <returns></returns>
        private string Log2String(LogEntity log, LogFormatType logFormat)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (logFormat == LogFormatType.Text)
            {
                sb.AppendLine("[" + log.DateTime.ToString("yyyy-MM-dd HH:mm:ss") + "] ");
                //switch (log.Level)
                //{
                //    case LogLevelType.Debug:
                //        sb.Append("调试信息");
                //        break;
                //    case LogLevelType.Info:
                //        sb.Append("信息");
                //        break;
                //    case LogLevelType.Warning:
                //        sb.Append("警告");
                //        break;
                //    case LogLevelType.Error:
                //        sb.Append("错误");
                //        break;
                //    case LogLevelType.Fatal:
                //        sb.Append("致命错误");
                //        break;
                //}
                sb.AppendLine("Level:" + log.Level.ToString());
                if (log.EventID != 0)
                {
                    sb.AppendLine("EventID:" + log.EventID.ToString());
                }
                if (!string.IsNullOrEmpty(log.EventSourceName))
                {
                    sb.AppendLine("EventSourceName:" + log.EventSourceName);
                }
                if (!string.IsNullOrEmpty(log.TaskCategory))
                {
                    sb.AppendLine("TaskCategory:" + log.TaskCategory);
                }
                if (!string.IsNullOrEmpty(log.UserID))
                {
                    sb.AppendLine("UserID:" + log.UserID);
                }
                sb.AppendLine(log.Message);

                sb.AppendLine();
            }
            else if (logFormat == LogFormatType.XML)
            {
                sb.Append("<Log>");
                sb.Append("<DateTime>" + log.DateTime.ToString("yyyy-MM-dd HH:mm:ss") + "</DateTime>");
                //switch (log.Level)
                //{
                //    case LogLevelType.Debug:
                //        sb.Append(Log.GetXMLElementString("Level", "调试信息"));
                //        break;
                //    case LogLevelType.Info:
                //        sb.Append(Log.GetXMLElementString("Level", "信息"));
                //        break;
                //    case LogLevelType.Warning:
                //        sb.Append(Log.GetXMLElementString("Level", "警告"));
                //        break;
                //    case LogLevelType.Error:
                //        sb.Append(Log.GetXMLElementString("Level", "错误"));
                //        break;
                //    case LogLevelType.Fatal:
                //        sb.Append(Log.GetXMLElementString("Level", "致命错误"));
                //        break;
                //}

                sb.Append(Log.GetXMLElementString("Level", log.Level.ToString()));

                if (log.EventID != 0)
                {
                    sb.Append("<EventID>" + log.EventID.ToString() + "</EventID>");
                }
                if (!string.IsNullOrEmpty(log.EventSourceName))
                {
                    sb.Append(Log.GetXMLElementString("EventSourceName", log.EventSourceName));
                }
                if (!string.IsNullOrEmpty(log.TaskCategory))
                {
                    sb.Append(Log.GetXMLElementString("TaskCategory", log.TaskCategory));
                }
                if (!string.IsNullOrEmpty(log.UserID))
                {
                    sb.Append(Log.GetXMLElementString("UserID", log.UserID));
                }
                sb.Append(Log.GetXMLElementString("Message", log.Message));
                sb.Append(@"</Log>
");
            }
            return sb.ToString();
        }

        /// <summary>
        /// 输出一条日志信息。
        /// </summary>
        /// <param name="log">日志实体。</param>
        private void WriteLog(LogEntity log)
        {
            if (log == null)
            {
                return;
            }

            if (this._OutLogEvent != null && (this.OutputType & LogOutputType.LogEvent) == LogOutputType.LogEvent)
            {
                this._OutLogEvent(log);
            }

            if ((this.OutputType & LogOutputType.Debugger) == LogOutputType.Debugger)
            {
                System.Diagnostics.Debug.Write(this.Log2String(log, this.LogFormat));
            }

            if ((this.OutputType & LogOutputType.Console) == LogOutputType.Console)
            {
                System.Console.Write(this.Log2String(log, this.LogFormat));
            }

            if ((this.OutputType & LogOutputType.LogFile) == LogOutputType.LogFile)
            {
                lock (this.LockThis)
                {
                    string f = this.GetCurrentLogFile();
                    System.IO.FileStream fs = new System.IO.FileStream(f, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.Read);
                    try
                    {
                        System.IO.StreamWriter sw = new System.IO.StreamWriter(fs, System.Text.Encoding.UTF8);
                        try
                        {
                            if (this.LogFormat == LogFormatType.XML)
                            {
                                if (sw.BaseStream.Length == 0) //如果是一个新的日志文件，则写入 XML 说明。
                                {
                                    sw.Write(XMLDocBeginTag);
                                }
                                else //如果是一个已经存在的日志文件，则将新日志数据写入位置定位到最后一条日志信息末尾。
                                {
                                    sw.BaseStream.Seek(0 - XMLDocEndTag.Length, System.IO.SeekOrigin.End);
                                }
                            }
                            else
                            {
                                sw.BaseStream.Seek(0, System.IO.SeekOrigin.End); //定位文件指针到文件末尾。
                            }

                            if (this.OutputType != LogOutputType.LogFile) //如果日志还输出到其他的位置（不仅限于日志文件）。
                            {
                                sw.Write(this.Log2String(log, this.LogFormat));
                                sw.Flush();
                            }
                            else
                            {
                                do
                                {
                                    sw.Write(this.Log2String(log, this.LogFormat));
                                    sw.Flush();
                                    log = null;
                                    #region 处理多条日志写入操作，避免重复打开文件耗时操作。
                                    if (this.wqLog.Count > 0 && fs.Length < this.MaxLogFileSize * 1024) //检查是否还有需要写入的日志，如果没有则关闭日志文件，否则继续写入下一条日志信息。
                                    {
                                        log = this.wqLog.Get(0);
                                    }
                                    #endregion
                                }
                                while (log != null);
                            }
                        }
                        finally
                        {
                            if (this.LogFormat == LogFormatType.XML)
                            {
                                sw.Write(XMLDocEndTag);
                            }
                            sw.Flush();
                            sw.Close();
                        }
                    }
                    finally
                    {
                        fs.Close();
                        fs.Dispose();
                    }
                }
            }

        }

        /// <summary>
        /// 用于描述异步输出日志线程当前是否正在输出日志。（注意：此对象只对异步输出日志操作有效）
        /// </summary>
        private volatile bool _IsBusy = false;
        /// <summary>
        /// 获取一个值，用于描述异步输出日志线程当前是否正在输出日志。（注意：此对象只对异步输出日志操作有效）
        /// </summary>
        public bool IsBusy
        {
            get
            {
                return this._IsBusy;
            }
        }

        /// <summary>
        /// 输出日志的线程方法。
        /// </summary>
        private void WriteMethod()
        {
            LogEntity log;
            while (true)
            {
                this._IsBusy = false;
                log = wqLog.Get();
                this._IsBusy = true;
                this.WriteLog(log);
            }

        }

        /// <summary>
        /// 获取当前可用的日志文件。
        /// </summary>
        /// <returns>日志文件名。</returns>
        private string GetCurrentLogFile()
        {
            if (!System.IO.Directory.Exists(this.LogPath))
            {
                System.IO.Directory.CreateDirectory(this.LogPath);
            }
            string logFile = System.IO.Path.Combine(this.LogPath, System.DateTime.Today.ToString("yyyyMMdd") + ".log");
            System.IO.FileInfo fileInfo = new System.IO.FileInfo(logFile);
            int i = 1;
            while (fileInfo.Exists && fileInfo.Length >= this.MaxLogFileSize * 1024)
            {
                logFile = System.IO.Path.Combine(this.LogPath, System.DateTime.Today.ToString("yyyyMMdd") + "_" + i.ToString("D3") + ".log");
                fileInfo = new System.IO.FileInfo(logFile);
                i++;
            }
            return logFile;

        }

        /// <summary>
        /// 输出一条日志信息（该信息将包含一个自动生成的时间元素。如果日志格式设置为“LogFormatType.XML”，则先转为 XML 格式，为日志信息增加“Message”标签对。）
        /// </summary>
        /// <param name="log">日志信息。</param>
        public void Write(string log)
        {
            if (this.OutputType != LogOutputType.None && this.OutputLevel != LogOutputLevel.None)
            {
                LogEntity l = new LogEntity() { Message = log };
                //                if (this.LogFormat == LogFormatType.XML)
                //                {
                //                    log = @"\t<Log>
                //\t\t<DateTime>" + log.DateTime.ToString("yyyy-MM-dd HH:mm:ss") + @"</DateTime>
                //\t\t" + Log.GetXMLElementString("Message", log.Message) + @"
                //\t</Log>
                //";
                //                }
                //                else
                //                {
                //                    log = "[" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] " + log + @"
                //";
                //                }
                this.Write(l);
            }
        }

        /// <summary>
        /// 输出一条日志信息（该信息将包含一个自动生成的时间元素。如果日志格式设置为“LogFormatType.XML”，则先转为 XML 格式，为日志信息增加“Message”标签对。）
        /// </summary>
        /// <param name="level">日志级别。</param>
        /// <param name="log">日志信息。</param>
        public void Write(LogLevelType level, string log)
        {
            if (this.OutputType != LogOutputType.None && this.OutputLevel != LogOutputLevel.None)
            {
                LogEntity l = new LogEntity() { Level = level, Message = log };
                this.Write(l);
            }
        }

        /// <summary>
        /// 输出一条日志信息（该信息将包含一个自动生成的时间元素。如果日志格式设置为“LogFormatType.XML”，则先转为 XML 格式，为日志信息增加“Message”标签对。）
        /// </summary>
        /// <param name="log">日志信息。</param>
        public void Write(LogEntity log)
        {
            if (this.OutputType != LogOutputType.None && this.OutputLevel != LogOutputLevel.None)
            {
                int outputLevel = (int)(this.OutputLevel), level = (int)(log.Level);
                if ((outputLevel & level) == level)
                {
                    if (this._AsyncWrite)
                    {
                        this.wqLog.Add(log);
                    }
                    else
                    {
                        this.WriteLog(log);
                    }
                }
            }

        }

        /// <summary>
        /// 输出一条日志信息（数据保持原样直接写入日志设备）。
        /// </summary>
        /// <param name="s">日志信息。</param>
        [System.Obsolete("此方法已过时，请使用替代方法“Write(LogEntity log)”。")]
        private void WriteBaseString(string s)
        {
            if (this.OutputType != LogOutputType.None && this.OutputLevel != LogOutputLevel.None)
            {
                if (this._AsyncWrite)
                {
                    this.wqLog.Add(new LogEntity() { Message = s });
                }
                else
                {
                    this.WriteLog(new LogEntity() { Message = s });
                }
            }

        }

        /// <summary>
        /// 输出一条具有 XML 格式的日志信息。
        /// </summary>
        /// <param name="xml">符合 XML 规则的文本形式数据。</param>
        [System.Obsolete("此方法已过时，请使用替代方法“Write(LogEntity log)”。")]
        private void WriteXML(string xml)
        {
            this.WriteBaseString(xml + @"
");
        }

        /// <summary>
        /// 输出一条具有 XML 格式的日志信息。
        /// </summary>
        /// <param name="xmlNode">XML 格式数据对象。</param>
        [System.Obsolete("此方法已过时，请使用替代方法“Write(LogEntity log)”。")]
        private void WriteXML(System.Xml.XmlNode xmlNode)
        {
            this.WriteBaseString(xmlNode.OuterXml + @"
");
        }

        /// <summary>
        /// 描述是否应该异步写入日志。
        /// </summary>
        private volatile bool _AsyncWrite = false;
        /// <summary>
        /// 获取或设置一个值，用于描述是否应该异步输出日志。
        /// </summary>
        public bool AsyncWrite
        {
            get
            {
                return this._AsyncWrite;
            }
            set
            {
                this._AsyncWrite = value;
            }
        }

        /// <summary>
        /// 启动异步写入日志功能。
        /// </summary>
        /// <remarks>
        /// 启动异步写入日志功能后，在写入日志时不会阻塞主线程，这能够提高执行效率，但是此方法不适合需要将日志立即写入的情况（例如：在进程退出前的日志书写操作）。
        /// </remarks>
        public void StartAsync()
        {
            this._AsyncWrite = true;
        }

        /// <summary>
        /// 禁止异步写入日志功能。调用此方法后将启用同步写入日志功能（这也是日志操作的默认状态）。
        /// </summary>
        public void StopAsync()
        {
            this._AsyncWrite = false;
        }

        /// <summary>
        /// 立即将所有待输出的日志数据输出到相应的输出设备（具体的输出位置由属性“<see cref="OutputType"/>”决定）。
        /// </summary>
        public void Flush()
        {
            while (this.wqLog.Count > 0 || this._IsBusy)
            {
                this.thWriter.Join(1);
            }

        }

        /// <summary>
        /// 输出日志事件代理。
        /// </summary>
        /// <param name="log"></param>
        public delegate void OutLogEventHandler(LogEntity log);

        private event OutLogEventHandler _OutLogEvent;
        /// <summary>
        /// 当需要输出日志时引发此事件。
        /// </summary>
        public event OutLogEventHandler OutLogEvent
        {
            add
            {
                this._OutLogEvent += value;
            }
            remove
            {
                this._OutLogEvent -= value;
            }
        }

        #region 封装了 XML 格式处理方法。
        private static System.Xml.XmlDocument _xDoc;
        /// <summary>
        /// 一个 XML 对象，用于处理 XML 数据。
        /// </summary>
        private static System.Xml.XmlDocument xDoc
        {
            get
            {
                if (Log._xDoc == null)
                {
                    Log._xDoc = new System.Xml.XmlDocument();
                }
                return Log._xDoc;
            }
        }

        /// <summary>
        /// 获取指定字符串的 XML 属性编码格式。
        /// </summary>
        /// <param name="name">属性名称。</param>
        /// <param name="value">属性取值。</param>
        /// <returns>符合 XML 规则的文本形式数据。</returns>
        internal static string GetXMLAttributeString(string name, string value)
        {
            System.Xml.XmlAttribute x = Log.xDoc.CreateAttribute(name);
            x.Value = value;
            return x.OuterXml;

        }

        /// <summary>
        /// 获取指定字符串的 XML 元素编码格式。
        /// </summary>
        /// <param name="name">元素名称。</param>
        /// <param name="text">元素取值。</param>
        /// <returns>符合 XML 规则的文本形式数据。</returns>
        internal static string GetXMLElementString(string name, string text)
        {
            System.Xml.XmlElement x = Log.xDoc.CreateElement(name);
            x.InnerText = text;
            return x.OuterXml;

        }

        #endregion

        #region 实现全局的公共日志方法。

        private static Log _GlobalLog;
        /// <summary>
        /// 获取一个全局的日志对象。
        /// </summary>
        public static Log GlobalLog
        {
            get
            {
                return Log._GlobalLog;
            }
        }

        /// <summary>
        /// 一个静态构造方法。
        /// </summary>
        static Log()
        {
            Log._GlobalLog = new Log();
        }

        #endregion

    }
}
