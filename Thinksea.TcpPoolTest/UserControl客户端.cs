using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.TcpPoolTest
{
    public partial class UserControl客户端 : UserControl
    {
        public UserControl客户端()
        {
            InitializeComponent();

            if (this.thread1 == null)
            {
                this.thread1 = new System.Threading.Thread(this.thread1Run);
                this.thread1.IsBackground = true;
                this.thread1.Start();
            }
        }

        System.Threading.Thread thread1;
        System.Threading.AutoResetEvent autoResetEventThread1 = new System.Threading.AutoResetEvent(false);
        private void thread1Run()
        {
            while (true)
            {
                lock (threadCollLock)
                {
                    while (this.threadColl.Count < this.threadCount)
                    {
                        System.Threading.Thread thread = new System.Threading.Thread(this.subThreadRun);
                        thread.IsBackground = true;
                        this.threadColl.Add(thread);
                        thread.Start(new object[] { thread });
                    }
                }
                autoResetEventThread1.WaitOne(1000);
            }
        }

        Thinksea.Net.TcpClientPool tcpClientPool;

        System.DateTime _启动时间 = System.DateTime.MinValue;
        System.DateTime 启动时间
        {
            get
            {
                return this._启动时间;
            }
            set
            {
                this._启动时间 = value;
                this.label启动时间.Text = value.ToString();
                this.label测试时长.Text = "";
                this.label当前线程数.Text = "0";
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_启动时间 != System.DateTime.MinValue)
            {
                this.label测试时长.Text = (System.DateTime.Now - this.启动时间).ToString();
                this.label当前线程数.Text = this.threadColl.Count.ToString();
            }
        }

        private void button启动_Click(object sender, EventArgs e)
        {
            if (this.button启动.Tag == null)
            {
                this.textBox服务器地址.Enabled = false;
                this.numericUpDown端口号.Enabled = false;
                this.numericUpDown并发线程数.Enabled = false;
                this.numericUpDown最大延迟时间.Enabled = false;
                this.button启动.Tag = true;
                this.button启动.Text = "停止";
                this.启动时间 = System.DateTime.Now;
                this.timer1.Start();

                if (this.tcpClientPool == null)
                {
                    ip = System.Net.IPAddress.Parse(this.textBox服务器地址.Text);
                    port = System.Convert.ToInt32(this.numericUpDown端口号.Value);
                    threadCount = System.Convert.ToInt32(this.numericUpDown并发线程数.Value);
                    delay = System.Convert.ToInt32(this.numericUpDown最大延迟时间.Value);
                    this.tcpClientPool = new Net.TcpClientPool();
                }
            }
            else
            {
                this.tcpClientPool.CloseAll();
                this.tcpClientPool = null;

                this.textBox服务器地址.Enabled = true;
                this.numericUpDown端口号.Enabled = true;
                this.numericUpDown并发线程数.Enabled = true;
                this.numericUpDown最大延迟时间.Enabled = true;
                this.button启动.Text = "启动";
                this.timer1.Stop();
                this.button启动.Tag = null;
            }
        }

        System.Net.IPAddress ip;
        int port;
        int threadCount;
        int delay;
        System.Collections.Generic.List<System.Threading.Thread> threadColl = new List<System.Threading.Thread>();
        object threadCollLock = new object();

        private void subThreadRun(object o)
        {
            object[] os = o as object[];
            System.Net.Sockets.NetworkStream ns = this.tcpClientPool.GetConnection(ip, port);
            System.Threading.Thread thread = os[0] as System.Threading.Thread;
            //int r = ns.ReadByte();
            //if (r != 1)
            //{
            //    throw new System.Exception("错误！！！");
            //}
            System.IO.BinaryReader br = new System.IO.BinaryReader(ns);
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ns);
            bool r = br.ReadBoolean();
            if (!r)
            {
                throw new System.Exception("错误！！！");
            }
            System.Threading.Thread.Sleep(delay);
            bw.Write(true);
            //ns.WriteByte(1);
            ns.Flush();
            //this.tcpClientPool.Free(ns);
            this.tcpClientPool.Close(ns);
            lock (threadCollLock)
            {
                this.threadColl.Remove(thread);
            }
            autoResetEventThread1.Set();
        }

    }
}
