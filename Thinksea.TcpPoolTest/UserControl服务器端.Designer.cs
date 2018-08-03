namespace Thinksea.TcpPoolTest
{
    partial class UserControl服务器端
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label测试时长 = new System.Windows.Forms.Label();
            this.label启动时间 = new System.Windows.Forms.Label();
            this.button启动 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown端口号 = new System.Windows.Forms.NumericUpDown();
            this.textBox侦听地址 = new System.Windows.Forms.TextBox();
            this.label当前线程数 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown端口号)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label当前线程数);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.textBox侦听地址);
            this.groupBox1.Controls.Add(this.numericUpDown端口号);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label测试时长);
            this.groupBox1.Controls.Add(this.label启动时间);
            this.groupBox1.Controls.Add(this.button启动);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(349, 232);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "服务器端";
            // 
            // label测试时长
            // 
            this.label测试时长.AutoSize = true;
            this.label测试时长.Location = new System.Drawing.Point(104, 130);
            this.label测试时长.Name = "label测试时长";
            this.label测试时长.Size = new System.Drawing.Size(41, 12);
            this.label测试时长.TabIndex = 3;
            this.label测试时长.Text = "label7";
            // 
            // label启动时间
            // 
            this.label启动时间.AutoSize = true;
            this.label启动时间.Location = new System.Drawing.Point(104, 98);
            this.label启动时间.Name = "label启动时间";
            this.label启动时间.Size = new System.Drawing.Size(41, 12);
            this.label启动时间.TabIndex = 2;
            this.label启动时间.Text = "label1";
            // 
            // button启动
            // 
            this.button启动.Location = new System.Drawing.Point(243, 119);
            this.button启动.Name = "button启动";
            this.button启动.Size = new System.Drawing.Size(75, 23);
            this.button启动.TabIndex = 1;
            this.button启动.Text = "启动";
            this.button启动.UseVisualStyleBackColor = true;
            this.button启动.Click += new System.EventHandler(this.button启动_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(38, 130);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "测试时长：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(38, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "启动时间：";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(38, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "侦听地址：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(38, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "端口号：";
            // 
            // numericUpDown端口号
            // 
            this.numericUpDown端口号.Location = new System.Drawing.Point(104, 59);
            this.numericUpDown端口号.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDown端口号.Name = "numericUpDown端口号";
            this.numericUpDown端口号.Size = new System.Drawing.Size(120, 21);
            this.numericUpDown端口号.TabIndex = 5;
            this.numericUpDown端口号.Value = new decimal(new int[] {
            1001,
            0,
            0,
            0});
            // 
            // textBox侦听地址
            // 
            this.textBox侦听地址.Location = new System.Drawing.Point(104, 27);
            this.textBox侦听地址.Name = "textBox侦听地址";
            this.textBox侦听地址.Size = new System.Drawing.Size(120, 21);
            this.textBox侦听地址.TabIndex = 6;
            this.textBox侦听地址.Text = "127.0.0.1";
            // 
            // label当前线程数
            // 
            this.label当前线程数.AutoSize = true;
            this.label当前线程数.Location = new System.Drawing.Point(121, 157);
            this.label当前线程数.Name = "label当前线程数";
            this.label当前线程数.Size = new System.Drawing.Size(41, 12);
            this.label当前线程数.TabIndex = 8;
            this.label当前线程数.Text = "label7";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(38, 157);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(77, 12);
            this.label8.TabIndex = 7;
            this.label8.Text = "当前连接数：";
            // 
            // UserControl服务器端
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Name = "UserControl服务器端";
            this.Size = new System.Drawing.Size(349, 232);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown端口号)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label测试时长;
        private System.Windows.Forms.Label label启动时间;
        private System.Windows.Forms.Button button启动;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown端口号;
        private System.Windows.Forms.TextBox textBox侦听地址;
        private System.Windows.Forms.Label label当前线程数;
        private System.Windows.Forms.Label label8;
    }
}
