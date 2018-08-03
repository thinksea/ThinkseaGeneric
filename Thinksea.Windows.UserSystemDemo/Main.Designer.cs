namespace Thinksea.Windows.UserSystemDemo
{
    partial class Main
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.editPassword = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.editComputer = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.editUser = new System.Windows.Forms.TextBox();
            this.button14 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbGroups = new System.Windows.Forms.ListBox();
            this.contextMenuStrip2 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刷新FToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.新建组toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.删除组toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.属性RToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.lbUsers = new System.Windows.Forms.ListBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.刷新ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.创建用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.更改用户密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置用户密码ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.删除用户ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.属性ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.contextMenuStrip2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Location = new System.Drawing.Point(12, 85);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(119, 16);
            this.radioButton2.TabIndex = 29;
            this.radioButton2.Text = "连接到指定计算机";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(323, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 21;
            this.label5.Text = "密码";
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Checked = true;
            this.radioButton1.Location = new System.Drawing.Point(12, 63);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(107, 16);
            this.radioButton1.TabIndex = 28;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "管理这台计算机";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // editPassword
            // 
            this.editPassword.Location = new System.Drawing.Point(356, 9);
            this.editPassword.Name = "editPassword";
            this.editPassword.Size = new System.Drawing.Size(100, 21);
            this.editPassword.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.editComputer);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.editPassword);
            this.panel1.Controls.Add(this.editUser);
            this.panel1.Controls.Add(this.label5);
            this.panel1.Enabled = false;
            this.panel1.Location = new System.Drawing.Point(12, 107);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(464, 38);
            this.panel1.TabIndex = 30;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 12);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "计算机";
            // 
            // editComputer
            // 
            this.editComputer.Location = new System.Drawing.Point(46, 9);
            this.editComputer.Name = "editComputer";
            this.editComputer.Size = new System.Drawing.Size(100, 21);
            this.editComputer.TabIndex = 17;
            this.editComputer.Text = "192.168.0.1";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(150, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 18;
            this.label4.Text = "登录用户名";
            // 
            // editUser
            // 
            this.editUser.Location = new System.Drawing.Point(219, 9);
            this.editUser.Name = "editUser";
            this.editUser.Size = new System.Drawing.Size(100, 21);
            this.editUser.TabIndex = 19;
            this.editUser.Text = "administrator";
            // 
            // button14
            // 
            this.button14.Location = new System.Drawing.Point(482, 63);
            this.button14.Name = "button14";
            this.button14.Size = new System.Drawing.Size(78, 82);
            this.button14.TabIndex = 27;
            this.button14.Text = "创建连接";
            this.button14.UseVisualStyleBackColor = true;
            this.button14.Click += new System.EventHandler(this.button14_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.lbGroups);
            this.panel2.Controls.Add(this.lbUsers);
            this.panel2.Enabled = false;
            this.panel2.Location = new System.Drawing.Point(12, 151);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(548, 262);
            this.panel2.TabIndex = 31;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(283, 4);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(53, 12);
            this.label7.TabIndex = 35;
            this.label7.Text = "组列表：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(5, 4);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 34;
            this.label6.Text = "用户列表：";
            // 
            // lbGroups
            // 
            this.lbGroups.ContextMenuStrip = this.contextMenuStrip2;
            this.lbGroups.FormattingEnabled = true;
            this.lbGroups.ItemHeight = 12;
            this.lbGroups.Location = new System.Drawing.Point(285, 19);
            this.lbGroups.Name = "lbGroups";
            this.lbGroups.Size = new System.Drawing.Size(252, 232);
            this.lbGroups.TabIndex = 33;
            this.lbGroups.DoubleClick += new System.EventHandler(this.属性RToolStripMenuItem_Click);
            // 
            // contextMenuStrip2
            // 
            this.contextMenuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新FToolStripMenuItem,
            this.toolStripSeparator4,
            this.新建组toolStripMenuItem1,
            this.删除组toolStripMenuItem2,
            this.toolStripSeparator5,
            this.属性RToolStripMenuItem});
            this.contextMenuStrip2.Name = "contextMenuStrip2";
            this.contextMenuStrip2.Size = new System.Drawing.Size(125, 104);
            // 
            // 刷新FToolStripMenuItem
            // 
            this.刷新FToolStripMenuItem.Name = "刷新FToolStripMenuItem";
            this.刷新FToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.刷新FToolStripMenuItem.Text = "刷新(&F)";
            this.刷新FToolStripMenuItem.Click += new System.EventHandler(this.刷新FToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(121, 6);
            // 
            // 新建组toolStripMenuItem1
            // 
            this.新建组toolStripMenuItem1.Name = "新建组toolStripMenuItem1";
            this.新建组toolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.新建组toolStripMenuItem1.Text = "新建组(&N)";
            this.新建组toolStripMenuItem1.Click += new System.EventHandler(this.新建组toolStripMenuItem1_Click);
            // 
            // 删除组toolStripMenuItem2
            // 
            this.删除组toolStripMenuItem2.Name = "删除组toolStripMenuItem2";
            this.删除组toolStripMenuItem2.Size = new System.Drawing.Size(124, 22);
            this.删除组toolStripMenuItem2.Text = "删除组(&D)";
            this.删除组toolStripMenuItem2.Click += new System.EventHandler(this.删除组toolStripMenuItem2_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(121, 6);
            // 
            // 属性RToolStripMenuItem
            // 
            this.属性RToolStripMenuItem.Name = "属性RToolStripMenuItem";
            this.属性RToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.属性RToolStripMenuItem.Text = "属性(&R)";
            this.属性RToolStripMenuItem.Click += new System.EventHandler(this.属性RToolStripMenuItem_Click);
            // 
            // lbUsers
            // 
            this.lbUsers.ContextMenuStrip = this.contextMenuStrip1;
            this.lbUsers.FormattingEnabled = true;
            this.lbUsers.ItemHeight = 12;
            this.lbUsers.Location = new System.Drawing.Point(5, 19);
            this.lbUsers.Name = "lbUsers";
            this.lbUsers.Size = new System.Drawing.Size(252, 232);
            this.lbUsers.TabIndex = 32;
            this.lbUsers.DoubleClick += new System.EventHandler(this.属性ToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.刷新ToolStripMenuItem,
            this.toolStripSeparator1,
            this.创建用户ToolStripMenuItem,
            this.更改用户密码ToolStripMenuItem,
            this.设置用户密码ToolStripMenuItem,
            this.toolStripSeparator3,
            this.删除用户ToolStripMenuItem,
            this.toolStripSeparator2,
            this.属性ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(161, 154);
            // 
            // 刷新ToolStripMenuItem
            // 
            this.刷新ToolStripMenuItem.Name = "刷新ToolStripMenuItem";
            this.刷新ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.刷新ToolStripMenuItem.Text = "刷新(&F)";
            this.刷新ToolStripMenuItem.Click += new System.EventHandler(this.刷新ToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(157, 6);
            // 
            // 创建用户ToolStripMenuItem
            // 
            this.创建用户ToolStripMenuItem.Name = "创建用户ToolStripMenuItem";
            this.创建用户ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.创建用户ToolStripMenuItem.Text = "创建用户(&N)";
            this.创建用户ToolStripMenuItem.Click += new System.EventHandler(this.创建用户ToolStripMenuItem_Click);
            // 
            // 更改用户密码ToolStripMenuItem
            // 
            this.更改用户密码ToolStripMenuItem.Name = "更改用户密码ToolStripMenuItem";
            this.更改用户密码ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.更改用户密码ToolStripMenuItem.Text = "修改用户密码(&M)";
            this.更改用户密码ToolStripMenuItem.Click += new System.EventHandler(this.更改用户密码ToolStripMenuItem_Click);
            // 
            // 设置用户密码ToolStripMenuItem
            // 
            this.设置用户密码ToolStripMenuItem.Name = "设置用户密码ToolStripMenuItem";
            this.设置用户密码ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.设置用户密码ToolStripMenuItem.Text = "设置用户密码(&S)";
            this.设置用户密码ToolStripMenuItem.Click += new System.EventHandler(this.设置用户密码ToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(157, 6);
            // 
            // 删除用户ToolStripMenuItem
            // 
            this.删除用户ToolStripMenuItem.Name = "删除用户ToolStripMenuItem";
            this.删除用户ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.删除用户ToolStripMenuItem.Text = "删除用户(&D)";
            this.删除用户ToolStripMenuItem.Click += new System.EventHandler(this.删除用户ToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(157, 6);
            // 
            // 属性ToolStripMenuItem
            // 
            this.属性ToolStripMenuItem.Name = "属性ToolStripMenuItem";
            this.属性ToolStripMenuItem.Size = new System.Drawing.Size(160, 22);
            this.属性ToolStripMenuItem.Text = "属性(&R)";
            this.属性ToolStripMenuItem.Click += new System.EventHandler(this.属性ToolStripMenuItem_Click);
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.LinkArea = new System.Windows.Forms.LinkArea(21, 23);
            this.linkLabel1.Location = new System.Drawing.Point(39, 419);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(499, 19);
            this.linkLabel1.TabIndex = 33;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Copyright (c) 思海网络 ( http://www.thinksea.com ) 2008 年. All Rights Reserved.";
            this.linkLabel1.UseCompatibleTextRendering = true;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // label1
            // 
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Padding = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.label1.Size = new System.Drawing.Size(548, 49);
            this.label1.TabIndex = 34;
            this.label1.Text = "操作帮助：首先需要确定被管理的计算机，然后点击“创建连接”按钮开始管理操作。\r\n提示信息：1、执行操作时请注意观察 计算机管理 -> 本地用户和组 中的相应变化。" +
                "\r\n　　　　　2、在“用户列表”和“组列表”两个列表框上可以点击鼠标右键查看允许的更多操作。";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 441);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.button14);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.radioButton2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Main";
            this.Text = "管理 Windows 用户系统";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.contextMenuStrip2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.TextBox editPassword;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox editComputer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox editUser;
        private System.Windows.Forms.Button button14;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ListBox lbUsers;
        private System.Windows.Forms.ListBox lbGroups;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 刷新ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 创建用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置用户密码ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip2;
        private System.Windows.Forms.ToolStripMenuItem 更改用户密码ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除用户ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 属性ToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem 刷新FToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem 新建组toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem 属性RToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除组toolStripMenuItem2;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label1;
    }
}