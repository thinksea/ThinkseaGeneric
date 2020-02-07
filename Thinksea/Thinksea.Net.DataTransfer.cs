namespace Thinksea.Net
{
    /// <summary>
    /// 封装了数据传输方法。
    /// </summary>
    public static class DataTransfer
    {
        #region 封装了基础数据传输方法。
        /// <summary>
        /// 数据传输尺寸已更改事件参数。
        /// </summary>
        public class TransferPositionChangedEventArgs
        {
            /// <summary>
            /// 获取下次传输数据位置。
            /// </summary>
            public System.Int64 TransferPosition
            {
                get;
                private set;
            }

            private bool _AbortTransfer = false;
            /// <summary>
            /// 获取或设置一个值，用于指示是否应该终止发送数据任务。
            /// </summary>
            public bool AbortTransfer
            {
                get
                {
                    return this._AbortTransfer;
                }
                set
                {
                    this._AbortTransfer = value;
                }
            }

            /// <summary>
            /// 用指定的数据初始化此实例。
            /// </summary>
            /// <param name="transferSize">已传输数据大小。</param>
            public TransferPositionChangedEventArgs(System.Int64 transferSize)
            {
                this.TransferPosition = transferSize;
            }

        }

        /// <summary>
        /// 当已传输的数据大小更改后引发此事件。
        /// </summary>
        /// <param name="e">事件参数。</param>
        public delegate void TransferPositionChangedHandler(TransferPositionChangedEventArgs e);

        /// <summary>
        /// 以指定的发送规则发送数据到指定的网络流，数据来自输入流“inputStream”。
        /// </summary>
        /// <param name="networkStream">与客户端连接的网络流。</param>
        /// <param name="inputStream">数据输入流。</param>
        /// <param name="sleep">发送数据间隔时间。</param>
        /// <param name="dataSize">每次发送数据包大小。</param>
        /// <param name="firstDataSize">首次发送数据包大小。</param>
        /// <param name="transferSizeChanged">已经传输的数据大小更改时引发此事件。设置为 null 则忽略此参数。</param>
        /// <returns>实际发送的数据大小。</returns>
        public static long WriteDataToNetwork(System.Net.Sockets.NetworkStream networkStream, System.IO.Stream inputStream, System.TimeSpan sleep, int dataSize, int firstDataSize, TransferPositionChangedHandler transferSizeChanged)
        {
            //if (!NetworkStream.CanWrite)
            //{
            //    throw new System.Exception("网络文件流不支持写入操作。");
            //}
            //this.OutputLog("[IP=" + RemoteClient.RemoteIP.ToString() + ":" + RemoteClient.RemotePort.ToString() + "] 开始传送文件：" + FileName);
            //if (!networkStream.CanWrite)
            //{
            //    return;
            //}
            if (inputStream.Length < firstDataSize)
            {
                firstDataSize = System.Convert.ToInt32(inputStream.Length);
            }
            if (inputStream.Length < dataSize)
            {
                dataSize = System.Convert.ToInt32(inputStream.Length);
            }
            int bufferLength = dataSize > firstDataSize ? dataSize : firstDataSize;
            byte[] buffer = new byte[bufferLength];
            //if (!networkStream.CanWrite)
            //{
            //    return;
            //}
            //try
            //{
            #region 开始写入数据。
            long tLen = 0;
            //long currentSize = inputStream.Position;
            int len = inputStream.Read(buffer, 0, firstDataSize);
            while (len != 0)
            {
                //if (!networkStream.CanWrite)
                //{
                //    break;
                //}
                networkStream.Write(buffer, 0, len);
                tLen += len;
                //currentSize += len;
                if (transferSizeChanged != null)
                {
                    TransferPositionChangedEventArgs e = new TransferPositionChangedEventArgs(inputStream.Position);
                    transferSizeChanged(e);
                    if (e.AbortTransfer)
                    {
                        break;
                    }
                }
                if (sleep > System.TimeSpan.Zero)
                {
                    //if (!networkStream.CanWrite)
                    //{
                    //    break;
                    //}
                    System.Threading.Thread.Sleep(sleep);//控制每隔多长时间发送一次数据。
                }
                //if (!networkStream.CanWrite)
                //{
                //    break;
                //}
                len = inputStream.Read(buffer, 0, bufferLength);
            }
            #endregion

            //}
            //catch (System.Threading.ThreadAbortException taex)
            //{
            //    //this.OutputLog(taex.ToString());
            //}
            //catch (System.IO.IOException ioex)
            //{
            //    /*此处 catch 用于截获下面的因属于正常的下载文件完成 IE 客户端先断开连接引发的异常：
            //    System.IO.IOException: 无法将数据写入传输连接: 远程主机强迫关闭了一个现有的连接。。 ---> System.Net.Sockets.SocketException: 远程主机强迫关闭了一个现有的连接。
            //       在 System.Net.Sockets.Socket.Send(Byte[] buffer, Int32 offset, Int32 size, SocketFlags socketFlags)
            //       在 System.Net.Sockets.NetworkStream.Write(Byte[] buffer, Int32 offset, Int32 size)
            //       --- 内部异常堆栈跟踪的结尾 ---
            //       在 System.Net.Sockets.NetworkStream.Write(Byte[] buffer, Int32 offset, Int32 size)
            //       在 IEFileServer.Form1.SendFileToNetwork(String FileName, NetworkStream NetworkStream) 位置 D:\Documents and Settings\桌面\IEFileServer\IEFileServer\Form1.cs:行号 157
            //     */
            //    //this.OutputLog(ioex.ToString());
            //}
            //catch (System.ObjectDisposedException odex)
            //{
            //    //this.OutputLog(odex.ToString());
            //}

            return tLen;
        }

        /// <summary>
        /// 发送数据到指定的网络流，数据来自输入流“inputStream”。
        /// </summary>
        /// <param name="networkStream">与客户端连接的网络流。</param>
        /// <param name="inputStream">数据输入流。</param>
        /// <param name="bufferSize">设置允许使用的缓冲区最大大小。</param>
        /// <param name="transferSizeChanged">已经传输的数据大小更改时引发此事件。设置为 null 则忽略此参数。</param>
        /// <returns>实际发送的数据大小。</returns>
        public static long WriteDataToNetwork(System.Net.Sockets.NetworkStream networkStream, System.IO.Stream inputStream, int bufferSize, TransferPositionChangedHandler transferSizeChanged)
        {
            return WriteDataToNetwork(networkStream, inputStream, System.TimeSpan.Zero, bufferSize, bufferSize, transferSizeChanged);

        }

        /// <summary>
        /// 发送文件到指定的网络流。
        /// </summary>
        /// <param name="networkStream">与客户端连接的网络流。</param>
        /// <param name="fileName">文件名。</param>
        /// <param name="bufferSize">设置允许使用的缓冲区最大大小。</param>
        /// <param name="transferSizeChanged">已经传输的数据大小更改时引发此事件。设置为 null 则忽略此参数。</param>
        /// <returns>实际发送的数据大小。</returns>
        public static long WriteFileToNetwork(System.Net.Sockets.NetworkStream networkStream, string fileName, int bufferSize, TransferPositionChangedHandler transferSizeChanged)
        {
            System.IO.FileStream fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            try
            {
                return WriteDataToNetwork(networkStream, fileStream, System.TimeSpan.Zero, bufferSize, bufferSize, transferSizeChanged);
            }
            //catch (System.Exception ioex)
            //{
            //    //this.OutputLog("[IP=" + RemoteClient.RemoteIP.ToString() + ":" + RemoteClient.RemotePort.ToString() + "] " + ioex.ToString());
            //}
            finally
            {
                fileStream.Close();
            }
            //this.OutputLog("[IP=" + RemoteClient.RemoteIP.ToString() + ":" + RemoteClient.RemotePort.ToString() + "] 传送文件完成：" + FileName);
        }

        /// <summary>
        /// 从指定的网络流中获取数据并写入到输出流中。
        /// </summary>
        /// <param name="networkStream">与客户端连接的网络流。</param>
        /// <param name="dataSize">指定要读取的数据最大大小。</param>
        /// <param name="outputStream">数据输出流。</param>
        /// <param name="bufferSize">设置允许使用的缓冲区最大大小。</param>
        /// <param name="transferSizeChanged">已经传输的数据大小更改时引发此事件。设置为 null 则忽略此参数。</param>
        /// <returns>实际接收到的数据大小。</returns>
        public static long ReadDataFromNetwork(System.Net.Sockets.NetworkStream networkStream, long dataSize, System.IO.Stream outputStream, int bufferSize, TransferPositionChangedHandler transferSizeChanged)
        {
            long tLen = 0;
            byte[] buff = new byte[dataSize < bufferSize ? dataSize : bufferSize];
            int len;
            int tRead;
            long rightSize; //剩余待传输的数据大小。
            while (tLen < dataSize) // && networkStream.CanRead
            {
                rightSize = dataSize - tLen;
                tRead = System.Convert.ToInt32(rightSize > buff.Length ? buff.Length : rightSize);
                len = networkStream.Read(buff, 0, tRead);
                if (len == 0)
                {
                    break;
                }
                outputStream.Write(buff, 0, len);
                tLen += len;
                if (transferSizeChanged != null)
                {
                    TransferPositionChangedEventArgs e = new TransferPositionChangedEventArgs(outputStream.Position);
                    transferSizeChanged(e);
                    if (e.AbortTransfer)
                    {
                        break;
                    }
                }
            }
            return tLen;

        }

        /// <summary>
        /// 以指定的文件名保存文件。
        /// </summary>
        /// <param name="networkStream">与客户端连接的网络流。</param>
        /// <param name="dataSize">指定要读取的数据大小。</param>
        /// <param name="fileName">存盘文件名。</param>
        /// <param name="bufferSize">设置允许使用的缓冲区最大大小。</param>
        /// <param name="transferSizeChanged">已经传输的数据大小更改时引发此事件。设置为 null 则忽略此参数。</param>
        /// <returns>实际接收到的数据大小。</returns>
        public static long ReadFileFromNetwork(System.Net.Sockets.NetworkStream networkStream, long dataSize, string fileName, int bufferSize, TransferPositionChangedHandler transferSizeChanged)
        {
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Create, System.IO.FileAccess.Write, System.IO.FileShare.None);
            try
            {
                return ReadDataFromNetwork(networkStream, dataSize, fs, bufferSize, transferSizeChanged);
            }
            finally
            {
                fs.Close();
            }
        }

        #endregion

        #region 封装了断点续传数据方法。

        /// <summary>
        /// 定义断点传输数据记录描述信息。
        /// </summary>
        private class BreakPoint
        {
            private System.Version _Version = new System.Version(1, 0, 0, 0);
            /// <summary>
            /// 序列化数据版本号。用于解决版本兼容性问题。
            /// </summary>
            private System.Version Version
            {
                get
                {
                    return this._Version;
                }
                set
                {
                    this._Version = value;
                }
            }

            /// <summary>
            /// 最后传输数据位置。
            /// </summary>
            public System.Int64 LastTransferPosition
            {
                get;
                set;
            }

            /// <summary>
            /// 总数据大小。
            /// </summary>
            public System.Int64 TotalSize
            {
                get;
                set;
            }

            /// <summary>
            /// 数据的 SHA1 码。
            /// </summary>
            public System.Byte[] SHA1
            {
                get;
                set;
            }

            /// <summary>
            /// 获取此实例中的数据转为 byte 数组。
            /// </summary>
            /// <returns>此实例的 byte 数组形式。</returns>
            public byte[] ToBytes()
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                {
                    System.IO.BinaryWriter writer = new System.IO.BinaryWriter(ms, System.Text.Encoding.UTF8);
                    writer.Write(this.Version.Major);
                    writer.Write(this.Version.Minor);
                    writer.Write(this.Version.Build);
                    writer.Write(this.Version.Revision);

                    writer.Write(this.TotalSize);
                    writer.Write(this.LastTransferPosition);
                    writer.Write(this.SHA1.Length);
                    writer.Write(this.SHA1);
                    return ms.ToArray();
                }
            }

            /// <summary>
            /// 从 byte 数组还原数据到此实例。
            /// </summary>
            /// <param name="datas">从其中还原数据的数据区。</param>
            public void FromBytes(byte[] datas)
            {
                using (System.IO.MemoryStream ms = new System.IO.MemoryStream(datas))
                {
                    System.IO.BinaryReader reader = new System.IO.BinaryReader(ms, System.Text.Encoding.UTF8);
                    System.Version version = new System.Version(reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32(), reader.ReadInt32());
                    if (version != this.Version)
                    {
                        throw new System.Exception("无效或不兼容的断点续传报文版本。");
                    }
                    this.TotalSize = reader.ReadInt64();
                    this.LastTransferPosition = reader.ReadInt64();
                    System.Int32 sha1Length = reader.ReadInt32();
                    this.SHA1 = reader.ReadBytes(sha1Length);
                }
            }

        }

        /// <summary>
        /// 发送数据到指定的网络流，数据来自输入流“inputStream”。支持断点续传。
        /// </summary>
        /// <param name="networkStream">与客户端连接的网络流。</param>
        /// <param name="inputStream">数据输入流。</param>
        /// <param name="bufferSize">设置允许使用的缓冲区最大大小。</param>
        /// <param name="transferSizeChanged">已经传输的数据大小更改时引发此事件。设置为 null 则忽略此参数。</param>
        /// <returns>指示数据传输是否成功。</returns>
        public static bool WriteDataToNetworkBreakPoint(System.Net.Sockets.NetworkStream networkStream, System.IO.Stream inputStream, int bufferSize, TransferPositionChangedHandler transferSizeChanged)
        {
            if (inputStream.CanRead == false || inputStream.CanSeek == false)// || inputStream.CanWrite == false
            {
                throw new System.IO.IOException("无效的数据输入流参数“inputStream”，必须允许读取数据和支持查找功能。");//，写入数据
            }
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(networkStream, System.Text.Encoding.UTF8);
            System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(networkStream, System.Text.Encoding.UTF8);
            byte[] sha1Datas = Thinksea.General.GetSHA1(inputStream, 0); //获取SHA1码。
            binaryWriter.Write(inputStream.Length); //写入待上传数据总长度。
            binaryWriter.Write(sha1Datas.Length); //写入SHA1码的长度。
            binaryWriter.Write(sha1Datas); //写入SHA1码。

            //System.Int64 BreakCheckPosition = binaryReader.ReadInt64(); //读取客户端请求的断点识别数据块起始位置。
            //System.Int32 BreakCheckLength = binaryReader.ReadInt32(); //读取客户端请求的断点识别数据块大小。
            //if (BreakCheckLength > 0) //服务端请求上传断点识别数据块。
            //{
            //    inputStream.Seek(BreakCheckPosition, System.IO.SeekOrigin.Begin);
            //    byte[] BreakCheckDatas = new byte[BreakCheckLength]; //断点识别数据块。
            //    inputStream.Read(BreakCheckDatas, 0, BreakCheckDatas.Length); //读取待校验的断点数据。

            //    binaryWriter.Write(BreakCheckDatas.Length); //写入断点识别数据块大小。
            //    binaryWriter.Write(BreakCheckDatas); //写入断点识别数据块。
            //}

            System.Int64 firstPosition = binaryReader.ReadInt64(); //获取开始上传的数据区起始位置。
            if (firstPosition != inputStream.Length)
            {
                inputStream.Seek(firstPosition, System.IO.SeekOrigin.Begin); //设置读取数据位置。
                Thinksea.Net.DataTransfer.WriteDataToNetwork(networkStream, inputStream, System.TimeSpan.Zero, bufferSize, bufferSize, transferSizeChanged); //发送数据。
            }

            bool success = binaryReader.ReadBoolean(); //获取一个标识，指示文件上传是否成功。
            return success;
        }

        /// <summary>
        /// 发送文件到指定的网络流。支持断点续传。
        /// </summary>
        /// <param name="networkStream">与客户端连接的网络流。</param>
        /// <param name="fileName">文件名。</param>
        /// <param name="bufferSize">设置允许使用的缓冲区最大大小。</param>
        /// <param name="transferSizeChanged">已经传输的数据大小更改时引发此事件。设置为 null 则忽略此参数。</param>
        /// <returns>指示数据传输是否成功。</returns>
        public static bool WriteFileToNetworkBreakPoint(System.Net.Sockets.NetworkStream networkStream, string fileName, int bufferSize, TransferPositionChangedHandler transferSizeChanged)
        {
            System.IO.FileStream fileStream = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            try
            {
                return WriteDataToNetworkBreakPoint(networkStream, fileStream, bufferSize, transferSizeChanged);
            }
            //catch (System.Exception ioex)
            //{
            //    //this.OutputLog("[IP=" + RemoteClient.RemoteIP.ToString() + ":" + RemoteClient.RemotePort.ToString() + "] " + ioex.ToString());
            //}
            finally
            {
                fileStream.Close();
            }
            //this.OutputLog("[IP=" + RemoteClient.RemoteIP.ToString() + ":" + RemoteClient.RemotePort.ToString() + "] 传送文件完成：" + FileName);
        }

        /// <summary>
        /// 保存断点数据到输出流。
        /// </summary>
        /// <param name="outputStream"></param>
        /// <param name="breakPoint"></param>
        private static void SaveBreakPoint(System.IO.Stream outputStream, BreakPoint breakPoint)
        {
            long oldPosition = outputStream.Position; //记录数据操作指针原始位置。

            System.Int32 Int32Size = 4;
            System.Int32 Int64Size = 8;
            byte[] breakPointDatas = breakPoint.ToBytes();
            System.Int64 fileSize = breakPoint.TotalSize + Int32Size + breakPointDatas.Length + Int64Size;

            if (outputStream.Length != fileSize)
            {
                outputStream.SetLength(fileSize);
            }
            outputStream.Seek(breakPoint.TotalSize, System.IO.SeekOrigin.Begin);
            System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(outputStream, System.Text.Encoding.UTF8);
            binaryWriter.Write(breakPointDatas.Length); //写入断点报文的长度。
            binaryWriter.Write(breakPointDatas); //写入断点报文。
            binaryWriter.Write(breakPoint.TotalSize); //写入断点报文数据区起始位置。

            outputStream.Position = oldPosition; //还原数据操作指针原始位置。
        }

        /// <summary>
        /// 读取断点报文从数据流。
        /// </summary>
        /// <param name="outputStream"></param>
        /// <returns>无法加载断点报文则返回 null。</returns>
        private static BreakPoint LoadBreakPoint(System.IO.Stream outputStream)
        {
            if (outputStream.Length == 0)
            {
                return null;
            }
            BreakPoint breakPoint = new BreakPoint();
            long oldPosition = outputStream.Position; //记录数据操作指针原始位置。
            try
            {
                System.Int32 Int64Size = 8;
                outputStream.Seek(-Int64Size, System.IO.SeekOrigin.End);
                System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(outputStream, System.Text.Encoding.UTF8);
                System.Int64 breakPointPosition = binaryReader.ReadInt64(); //读取断点报文数据区起始位置。
                outputStream.Seek(breakPointPosition, System.IO.SeekOrigin.Begin); //定位到断点报文数据区起始位置。
                System.Int32 breakPointLength = binaryReader.ReadInt32(); //读取断点报文的长度。
                byte[] breakPointDatas = binaryReader.ReadBytes(breakPointLength); //读取断点报文。

                breakPoint.FromBytes(breakPointDatas);
            }
            catch
            {
                return null;
            }
            finally
            {
                outputStream.Position = oldPosition; //还原数据操作指针原始位置。
            }

            return breakPoint;
        }

        /// <summary>
        /// 从指定的网络流中获取数据并写入到输出流中。支持断点续传。
        /// </summary>
        /// <param name="networkStream">与客户端连接的网络流。</param>
        /// <param name="outputStream">数据输出流。</param>
        /// <param name="bufferSize">设置允许使用的缓冲区最大大小。</param>
        /// <param name="transferSizeChanged">已经传输的数据大小更改时引发此事件。设置为 null 则忽略此参数。</param>
        /// <returns>指示数据传输是否成功。</returns>
        /// <remarks>
        /// 断点续传的前提条件为数据输出流参数 <paramref name="outputStream"/> 提供读写支持。
        /// </remarks>
        public static bool ReadDataFromNetworkBreakPoint(System.Net.Sockets.NetworkStream networkStream, System.IO.Stream outputStream, int bufferSize, TransferPositionChangedHandler transferSizeChanged)
        {
            if (outputStream.CanRead == false || outputStream.CanWrite == false || outputStream.CanSeek == false)
            {
                throw new System.IO.IOException("无效的数据输出流参数“outputStream”，必须允许读取数据，写入数据和支持查找功能。");
            }
            //System.Int32 EnableBreakLength = 1024 * 500; //指示断点上传启用条件。当待上传的数据总大小大于或等于此值时启用断点上传功能。断点检测是需要耗费时间的，所以此值不应该设置过小。
            //System.Int32 BreakCheckLength = 1024 * 32; //指示断点识别数据块大小,为 0 时表示忽略断点检测。默认设置为32Kbyte。如果设置的值太小将造成识别错误，太大又将影响效率首次传输效率。
            System.IO.BinaryReader binaryReader = new System.IO.BinaryReader(networkStream, System.Text.Encoding.UTF8);
            System.IO.BinaryWriter binaryWriter = new System.IO.BinaryWriter(networkStream, System.Text.Encoding.UTF8);

            System.Int64 dataLength = binaryReader.ReadInt64(); //读取待上传数据总长度。
            System.Int32 sha1Length = binaryReader.ReadInt32(); //读取SHA1码的长度。
            byte[] clientSha1Datas = binaryReader.ReadBytes(sha1Length); //读取SHA1码。

            #region 加载断点数据或生成新的断点数据。
            if (dataLength == outputStream.Length) //如果存盘数据大小与待上传数据大小相同，则先确认文件上传是否已经完成。
            {
                byte[] sha1 = Thinksea.General.GetSHA1(outputStream, 0); //获取SHA1码。
                if (Thinksea.General.CompareArray(sha1, clientSha1Datas)) //如果 SHA1 码相同，则认为文件已经传输完成。
                {
                    binaryWriter.Write(dataLength); //写入发送数据的起始位置。
                    binaryWriter.Write(true); //告诉客户端文件传输已经完成。
                    return true;
                }
            }
            BreakPoint breakPoint = LoadBreakPoint(outputStream);
            if (!(breakPoint != null && dataLength == breakPoint.TotalSize && Thinksea.General.CompareArray(breakPoint.SHA1, clientSha1Datas))) //只有文件大小相同，并且校验码相同，才启用断点续传。
            {
                breakPoint = new BreakPoint();
                breakPoint.TotalSize = dataLength;
                breakPoint.SHA1 = clientSha1Datas;
                breakPoint.LastTransferPosition = 0;
                SaveBreakPoint(outputStream, breakPoint);
            }
            #endregion

            //System.Int64 BreakCheckPosition = 0; //断点识别数据块起始位置。
            //if (dataLength >= outputStream.Length && dataLength > EnableBreakLength && outputStream.Length > 0) //如果需要断点续传。
            //{
            //    if (outputStream.Length < BreakCheckLength) BreakCheckLength = System.Convert.ToInt32(outputStream.Length); //根据已经传送的数据的长度调整校验数据块的大小。
            //    BreakCheckPosition = outputStream.Seek(0 - BreakCheckLength, System.IO.SeekOrigin.End); //定位断点检测起始位置。
            //    byte[] BreakCheckDatas = new byte[BreakCheckLength]; //断点识别数据块。
            //    outputStream.Read(BreakCheckDatas, 0, BreakCheckDatas.Length); //读取待校验的断点数据。

            //    binaryWriter.Write(BreakCheckPosition); //写入断点识别数据块起始位置。
            //    binaryWriter.Write(BreakCheckLength); //写入断点识别数据块大小。
            //    System.Int32 clientBreakDatasLength = binaryReader.ReadInt32(); //获取客户端将上传的断点识别数据块大小。
            //    byte[] clientBreakDatas = binaryReader.ReadBytes(clientBreakDatasLength); //获取客户端上传的断点识别数据块。
            //    if (Thinksea.General.CompareArray(BreakCheckDatas, clientBreakDatas)) //如果校验码相同，则启用断点续传。
            //    {
            //        outputStream.Seek(0, System.IO.SeekOrigin.End);
            //    }
            //    else //如果校验码不同，则重新传送数据。
            //    {
            //        outputStream.SetLength(0); //截断文件长度。
            //        outputStream.Seek(0, System.IO.SeekOrigin.Begin);
            //    }
            //}
            //else
            //{
            //    BreakCheckPosition = 0;
            //    BreakCheckLength = 0;
            //    binaryWriter.Write(BreakCheckPosition); //写入断点识别数据块起始位置。
            //    binaryWriter.Write(BreakCheckLength); //写入断点识别数据块大小。

            //    outputStream.SetLength(0); //截断文件长度。
            //    outputStream.Seek(0, System.IO.SeekOrigin.Begin);
            //}
            binaryWriter.Write(breakPoint.LastTransferPosition); //通知客户端上传数据的起始位置。
            if (breakPoint.LastTransferPosition < breakPoint.TotalSize)
            {
                outputStream.Seek(breakPoint.LastTransferPosition, System.IO.SeekOrigin.Begin); //设置写入数据起始位置。
                int saveBreakPointSize = 1024 * 500; //保存断点信息的条件。
                //Thinksea.FileTransfer.Lib.FileSystem.ReadDataFromNetwork(networkStream, (dataLength - outputStream.Position), outputStream, bufferSize, transferSizeChanged); //读取数据。
                Thinksea.Net.DataTransfer.ReadDataFromNetwork(networkStream, (dataLength - outputStream.Position), outputStream, bufferSize, delegate (TransferPositionChangedEventArgs e)
                {
                    if ((e.TransferPosition - breakPoint.LastTransferPosition) > saveBreakPointSize) //记录断点传输信息。
                    {
                        breakPoint.LastTransferPosition = e.TransferPosition;
                        SaveBreakPoint(outputStream, breakPoint);
                    }
                    if (transferSizeChanged != null)
                    {
                        transferSizeChanged(e);
                    }
                }); //读取数据。
            }
            bool success = false;
            if (outputStream.Position == breakPoint.TotalSize)
            {
                outputStream.SetLength(breakPoint.TotalSize); //清除断点数据。

                #region 校验文件SHA1码是否相同。
                byte[] serverSha1Datas = Thinksea.General.GetSHA1(outputStream, 0); //获取SHA1码。
                if (Thinksea.General.CompareArray(clientSha1Datas, serverSha1Datas))
                {
                    success = true;
                }
                else
                {
                    success = false;
                }
                #endregion
            }
            binaryWriter.Write(success);
            return success;
        }

        /// <summary>
        /// 以指定的文件名保存文件。支持断点续传。
        /// </summary>
        /// <param name="networkStream">与客户端连接的网络流。</param>
        /// <param name="fileName">存盘文件名。</param>
        /// <param name="bufferSize">设置允许使用的缓冲区最大大小。</param>
        /// <param name="transferSizeChanged">已经传输的数据大小更改时引发此事件。设置为 null 则忽略此参数。</param>
        /// <returns>指示数据传输是否成功。</returns>
        public static bool ReadFileFromNetworkBreakPoint(System.Net.Sockets.NetworkStream networkStream, string fileName, int bufferSize, TransferPositionChangedHandler transferSizeChanged)
        {
            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.None);
            try
            {
                return ReadDataFromNetworkBreakPoint(networkStream, fs, bufferSize, transferSizeChanged);
            }
            finally
            {
                fs.Close();
            }
        }

        #endregion

    }
}
