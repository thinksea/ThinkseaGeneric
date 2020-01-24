namespace Thinksea.Net
{
    /// <summary>
    /// TCP 服务器端。用于提供 TcpClient 多连接解决方案。
    /// 注意：对跨线程是安全的。
    /// </summary>
    public class TcpServerPool : System.IDisposable
    {
        /// <summary>
        /// 描述一个客户端连接信息。
        /// </summary>
        private class TcpServerPoolEntity : System.IDisposable
        {
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
                    this.TcpClient.Close();
                    this.TcpClient = null;
                }
            }

            #region 实现 IDisposable 接口。
            /// <summary>
            /// Track whether Dispose has been called.
            /// </summary>
            private bool disposed = false;

            /// <summary>
            /// 释放占用的资源。
            /// </summary>
            /// <param name="disposing">是否需要释放那些实现IDisposable接口的托管对象</param>
            protected virtual void Dispose(bool disposing)
            {
                // Check to see if Dispose has already been called.
                if (!this.disposed)
                {
                    // If disposing equals true, dispose all managed
                    // and unmanaged resources.
                    if (disposing)
                    {
                        // Dispose managed resources.
                        this.Close();
                    }

                    // Note disposing has been done.
                    disposed = true;
                }
            }

            /// <summary>
            /// 释放占用的资源。
            /// </summary>
            public void Dispose()
            {
                Dispose(true);
                // This object will be cleaned up by the Dispose method.
                // Therefore, you should call GC.SupressFinalize to
                // take this object off the finalization queue
                // and prevent finalization code for this object
                // from executing a second time.
                System.GC.SuppressFinalize(this);
            }
            #endregion

        }

        /// <summary>
        /// 当与客户端建立连接时引发此事件代理。
        /// </summary>
        /// <param name="client">客户端连接信息。</param>
        /// <param name="networkStream">网络数据通道。</param>
        public delegate void TcpClientConnectionEventHandler(System.Net.Sockets.TcpClient client, System.Net.Sockets.NetworkStream networkStream);

        /// <summary>
        /// 侦听服务。
        /// </summary>
        private System.Net.Sockets.TcpListener tcpListener = null;
        /// <summary>
        /// 已经建立的客户端连接集合。
        /// </summary>
        private System.Collections.Generic.List<TcpServerPoolEntity> TcpClients = new System.Collections.Generic.List<TcpServerPoolEntity>();
        /// <summary>
        /// 客户端连接集合锁。
        /// </summary>
        private object TcpClientsLock = new object();

        /// <summary>
        /// 获取已经建立的连接数。
        /// </summary>
        public int Count
        {
            get
            {
                return this.TcpClients.Count;
            }
        }

        private event TcpClientConnectionEventHandler _TcpClientConnectionEvent = null;
        /// <summary>
        /// 当与客户端建立连接时引发此事件。
        /// </summary>
        public event TcpClientConnectionEventHandler TcpClientConnectionEvent
        {
            add
            {
                this._TcpClientConnectionEvent += value;
            }
            remove
            {
                this._TcpClientConnectionEvent -= value;
            }
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="listenIP">侦听服务 IP 地址。</param>
        /// <param name="listenPort">侦听服务端口。</param>
        public TcpServerPool(System.Net.IPAddress listenIP, int listenPort)
        {
            if (this.tcpListener == null)
            {
                this.tcpListener = new System.Net.Sockets.TcpListener(listenIP, listenPort);
            }
        }

        /// <summary>
        /// 启动服务。
        /// </summary>
        public void Start()
        {
            this.CallByStop = false;
            this.tcpListener.Start();
            this.tcpListener.BeginAcceptTcpClient(new System.AsyncCallback(this.ClientConnection), null);

        }

        /// <summary>
        /// 启动服务。
        /// </summary>
        /// <param name="backlog">挂起连接队列的最大长度。</param>
        public void Start(int backlog)
        {
            this.CallByStop = false;
            this.tcpListener.Start(backlog);
            this.tcpListener.BeginAcceptTcpClient(new System.AsyncCallback(this.ClientConnection), null);

        }

        private bool CallByStop = false;
        /// <summary>
        /// 停止服务。
        /// </summary>
        public void Stop()
        {
            //try
            //{
            this.CallByStop = true;
            this.tcpListener.Stop();
            //}
            //catch { }

            lock (this.TcpClientsLock)
            {
                foreach (var tmp in this.TcpClients)
                {
                    tmp.Close();
                    //try
                    //{
                    //    tmp.Value.Close();
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

        /// <summary>
        /// 侦听服务方法。
        /// </summary>
        /// <param name="ar"></param>
        private void ClientConnection(System.IAsyncResult ar)
        {
            //TcpListener listener = (TcpListener)ar.AsyncState;
            System.Net.Sockets.TcpListener listener = this.tcpListener;

            System.Net.Sockets.TcpClient client;
            try
            {
                client = listener.EndAcceptTcpClient(ar);
            }
            catch (System.ObjectDisposedException)
            {
                return;
            }
            catch (System.Exception)
            {
                return;
            }
            finally
            {
                if (!this.CallByStop)
                {
                    listener.BeginAcceptTcpClient(this.ClientConnection, null);
                }
            }

            TcpServerPoolEntity tcpServerPoolEntity = null;
            try
            {
                if (client.Connected)
                {
                    System.Net.Sockets.NetworkStream ns = client.GetStream();
                    try
                    {
                        //int linkMark = ns.ReadByte();
                        //if (linkMark != 1)
                        //{
                        //    return;
                        //}
                        //ns.WriteByte(1);
                        tcpServerPoolEntity = new TcpServerPoolEntity() { TcpClient = client, NetworkStream = ns };
                        lock (this.TcpClientsLock)
                        {
                            this.TcpClients.Add(tcpServerPoolEntity);
                        }

                        if (this._TcpClientConnectionEvent != null)
                        {
                            this._TcpClientConnectionEvent(client, ns);
                        }
                    }
                    finally
                    {
                        ns.Flush();
                        ns.Close();
                    }
                }
            }
            finally
            {
                try
                {
                    client.Close();
                }
                finally
                {
                    if (tcpServerPoolEntity != null)
                    {
                        lock (this.TcpClientsLock)
                        {
                            this.TcpClients.Remove(tcpServerPoolEntity);
                        }
                    }
                }
            }

        }

        #region 实现 IDisposable 接口。
        /// <summary>
        /// Track whether Dispose has been called.
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// 释放占用的资源。
        /// </summary>
        /// <param name="disposing">是否需要释放那些实现IDisposable接口的托管对象</param>
        protected virtual void Dispose(bool disposing)
        {
            // Check to see if Dispose has already been called.
            if (!this.disposed)
            {
                // If disposing equals true, dispose all managed
                // and unmanaged resources.
                if (disposing)
                {
                    // Dispose managed resources.
                    this.Stop();
                }

                // Note disposing has been done.
                disposed = true;
            }
        }

        /// <summary>
        /// 释放占用的资源。
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SupressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            System.GC.SuppressFinalize(this);
        }
        #endregion

    }

}
