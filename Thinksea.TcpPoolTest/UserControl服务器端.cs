using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.TcpPoolTest
{
    public partial class UserControl服务器端 : UserControl
    {
        public UserControl服务器端()
        {
            InitializeComponent();
        }

        Thinksea.Net.TcpServerPool tcpServerPool;

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
                this.label当前线程数.Text = this.tcpServerPool.Count.ToString();
            }
        }

        private void button启动_Click(object sender, EventArgs e)
        {
            if (this.button启动.Tag == null)
            {
                this.textBox侦听地址.Enabled = false;
                this.numericUpDown端口号.Enabled = false;
                this.button启动.Tag = true;
                this.button启动.Text = "停止";
                this.启动时间 = System.DateTime.Now;
                this.timer1.Start();

                if (this.tcpServerPool == null)
                {
                    System.Net.IPAddress ip = System.Net.IPAddress.Parse(this.textBox侦听地址.Text);
                    int port = System.Convert.ToInt32(this.numericUpDown端口号.Value);
                    this.tcpServerPool = new Net.TcpServerPool(ip, port);
                    this.tcpServerPool.TcpClientConnectionEvent += tcpServerPool_TcpClientConnectionEvent;
                }
                this.tcpServerPool.Start();
            }
            else
            {
                this.tcpServerPool.Stop();
                this.tcpServerPool = null;

                this.textBox侦听地址.Enabled = true;
                this.numericUpDown端口号.Enabled = true;
                this.button启动.Text = "启动";
                this.timer1.Stop();
                this.button启动.Tag = null;
            }
        }

        void tcpServerPool_TcpClientConnectionEvent(System.Net.Sockets.TcpClient client, System.Net.Sockets.NetworkStream networkStream)
        {
            try
            {
                while (true)
                {
                    //networkStream.WriteByte(1);
                    //int r = networkStream.ReadByte();
                    //if (r != 1)
                    //{
                    //    throw new System.Exception("错误！！！");
                    //}
                    System.IO.BinaryReader br = new System.IO.BinaryReader(networkStream);
                    System.IO.BinaryWriter bw = new System.IO.BinaryWriter(networkStream);
                    bw.Write(true);
                    bool r = br.ReadBoolean();
                    if (!r)
                    {
                        throw new System.Exception("错误！！！");
                    }
                }
            }
            catch
            {
            }
        }
    }
}
