namespace Thinksea.Net
{
    /// <summary>
    /// TCP 客户端链接池。用于提供 TcpClient 多连接解决方案。
    /// 注意：对跨线程是安全的。
    /// </summary>
    public class TcpClientPool : System.IDisposable
    {
        /// <summary>
        /// 描述一个客户端连接信息。
        /// </summary>
        private class TcpClientPoolEntity : System.IDisposable
        {
            /// <summary>
            /// 服务器 IP。
            /// </summary>
            public System.Net.IPAddress ServerIP
            {
                get;
                set;
            }

            /// <summary>
            /// 服务器通信端口。
            /// </summary>
            public int ServerPort
            {
                get;
                set;
            }

            /// <summary>
            /// 一个可用的数据通道。
            /// </summary>
            public System.Net.Sockets.NetworkStream NetworkStream
            {
                get;
                set;
            }

            /// <summary>
            /// 一个 TCP 客户端连接。
            /// </summary>
            public System.Net.Sockets.TcpClient TcpClient
            {
                get;
                set;
            }

            private bool _IsFree = true;
            /// <summary>
            /// 获取一个值，指示是否空闲。
            /// </summary>
            public bool IsFree
            {
                get
                {
                    return this._IsFree;
                }
                set
                {
                    this._IsFree = value;
                }
            }

            /// <summary>
            /// 关闭连接和流，并释放此对象占用的所有资源。
            /// </summary>
            public void Close()
            {
                if (this.NetworkStream != null)
                {
                    this.NetworkStream.Close();
                    this.NetworkStream = null;
                }
                if (this.TcpClient != null)
                {
                    this.TcpClient.Client.Close();
                    this.TcpClient.Close();
                    this.TcpClient = null;
                }
                this.IsFree = true;
            }

            public void Dispose()
            {
                this.Close();
            }

        }

        /// <summary>
        /// 已经建立的客户端连接集合。
        /// </summary>
        private System.Collections.Generic.List<TcpClientPoolEntity> TcpClients = new System.Collections.Generic.List<TcpClientPoolEntity>();

        /// <summary>
        /// TcpClient 集合操作锁。
        /// </summary>
        private object _TcpClientsLock = new object();

        /// <summary>
        /// 获取已经分配的 Tcp 连接池中的链接对象数量。
        /// </summary>
        public int Count
        {
            get
            {
                return this.TcpClients.Count;
            }
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        public TcpClientPool()
        {
        }

        /// <summary>
        /// 获取一个可用的数据通道。（重要的：应该在不使用数据通道后调用“<see cref="Free"/>”方法将其归还给连接池或调用“<see cref="Close"/>”方法关闭对象并释放其占用的资源。）
        /// </summary>
        /// <param name="serverIP">服务器 IP。</param>
        /// <param name="serverPort">服务器通信端口。</param>
        /// <returns>一个可用的数据通道。</returns>
        public System.Net.Sockets.NetworkStream GetConnection(System.Net.IPAddress serverIP, int serverPort)
        {
            lock (this._TcpClientsLock)
            {
                foreach (var tmp in this.TcpClients) //尝试获取可重复使用的连接信息。
                {
                    if (tmp.IsFree && tmp.ServerIP.Equals(serverIP) && tmp.ServerPort == serverPort)
                    {
                        tmp.IsFree = false;
                        return tmp.NetworkStream;
                    }
                }

                //如果找不到一个可以重用的连接信息则创建一个新的实例。
                System.Net.Sockets.TcpClient tcp = new System.Net.Sockets.TcpClient();
                tcp.Connect(serverIP, serverPort);
                System.Net.Sockets.NetworkStream ns = tcp.GetStream();
                //ns.WriteByte(1);
                //int linkMark = ns.ReadByte();
                //if (linkMark != 1)
                //{
                //}
                this.TcpClients.Add(new TcpClientPoolEntity() { ServerIP = serverIP, ServerPort = serverPort, TcpClient = tcp, NetworkStream = ns, IsFree = false });
                return ns;
            }
        }

        /// <summary>
        /// 将 TCP 连接还给连接池，使其可以被其他调用者重用。
        /// </summary>
        /// <param name="ns">一个数据通道。</param>
        public void Free(System.Net.Sockets.NetworkStream ns)
        {
            lock (this._TcpClientsLock)
            {
                foreach (var tmp in this.TcpClients)
                {
                    if (tmp.NetworkStream == ns)
                    {
                        tmp.IsFree = true;
                        break;
                    }
                }
                //TcpClientPoolEntity tc = null;
                //if (this.TcpClients.Contains(TryGetValue(ns, out tc))
                //{
                //    tc.IsFree = true;
                //    //try
                //    //{
                //    //    ns.Close();
                //    //}
                //    //catch { }

                //    //try
                //    //{
                //    //    tc.Close();
                //    //}
                //    //catch { }

                //    //this.TcpClients.Remove(ns);
                //}
            }
        }

        /// <summary>
        /// 关闭指定的流、连接并释放与之关联的所有资源。
        /// </summary>
        /// <param name="ns">一个数据通道。</param>
        public void Close(System.Net.Sockets.NetworkStream ns)
        {
            lock (this._TcpClientsLock)
            {
                for (int i = 0; i < this.TcpClients.Count; i++)
                {
                    var tmp = this.TcpClients[i];
                    if (tmp.NetworkStream == ns)
                    {
                        tmp.Close();
                        this.TcpClients.RemoveAt(i);
                        break;
                    }
                }
                //TcpClientPoolEntity tc = null;
                //if (this.TcpClients.TryGetValue(ns, out tc))
                //{
                //    tc.Close();
                //    //try
                //    //{
                //    //    ns.Close();
                //    //}
                //    //catch { }

                //    //try
                //    //{
                //    //    tc.Close();
                //    //}
                //    //catch { }

                //    this.TcpClients.Remove(ns);
                //}
            }
        }

        /// <summary>
        /// 获取一个值，指示是否已连接到远程主机。
        /// </summary>
        /// <param name="ns"></param>
        /// <returns></returns>
        public bool IsConnected(System.Net.Sockets.NetworkStream ns)
        {
            lock (this._TcpClientsLock)
            {
                foreach (var tmp in this.TcpClients)
                {
                    if (tmp.NetworkStream == ns)
                    {
                        return tmp.TcpClient.Connected;
                    }
                }
                //TcpClientPoolEntity tc = null;
                //if (this.TcpClients.TryGetValue(ns, out tc))
                //{
                //    return tc.TcpClient.Connected;
                //}
            }
            return false;
        }

        /// <summary>
        /// 关闭连接池中的所有链接，并释放占用的全部资源。
        /// </summary>
        public void CloseAll()
        {
            lock (this._TcpClientsLock)
            {
                foreach (var tmp in this.TcpClients)
                {
                    tmp.Close();
                    //try
                    //{
                    //    tmp.Value.TcpClient.Close();
                    //}
                    //catch { }

                    //try
                    //{
                    //    tmp.Key.Close();
                    //}
                    //catch { }
                }
                this.TcpClients.Clear();
            }
        }

        #region IDisposable 成员

        /// <summary>
        /// 释放此对象占用的全部资源。
        /// </summary>
        public void Dispose()
        {
            this.CloseAll();
        }

        #endregion
    }

}
