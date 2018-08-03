using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.LogTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Thinksea.Logs.Log.GlobalLog.OutputLevel = Thinksea.Logs.LogOutputLevel.Info | Thinksea.Logs.LogOutputLevel.Warning | Thinksea.Logs.LogOutputLevel.Error | Thinksea.Logs.LogOutputLevel.Fatal;
            Thinksea.Logs.Log.GlobalLog.Write(new Thinksea.Logs.LogEntity() { TaskCategory = "任务分类", Level = Thinksea.Logs.LogLevelType.Info, EventID = 1001, UserID = "一个测试用户ID", EventSourceName = "日志组件测试功能", Message = "测试日志功能是否好用" + System.DateTime.Now.ToString() });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thinksea.Logs.Log.GlobalLog.StartAsync();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thinksea.Logs.Log.GlobalLog.StopAsync();

        }
    }
}
