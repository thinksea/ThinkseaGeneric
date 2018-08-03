using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.UserSystemDemo
{
    public partial class Main : Form
    {
        public Thinksea.Windows.UserSystem u = null;
        public Thinksea.Windows.AccessManagement am = new Thinksea.Windows.AccessManagement();

        public Main()
        {
            InitializeComponent();
        }
        private void RefreshUserList()
        {
            this.lbUsers.Items.Clear();
            this.lbUsers.Items.AddRange(u.GetUsers());
        }

        private void RefreshGroupList()
        {
            this.lbGroups.Items.Clear();
            this.lbGroups.Items.AddRange(u.GetGroups());
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Enabled = !this.radioButton1.Checked;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            this.panel1.Enabled = this.radioButton2.Checked;

        }

        private void button14_Click(object sender, EventArgs e)
        {
            this.panel2.Enabled = false;
            if (this.radioButton1.Checked)
            {
                this.u = new Thinksea.Windows.UserSystem();

            }
            else
            {
                this.u = new Thinksea.Windows.UserSystem(this.editComputer.Text, this.editUser.Text, this.editPassword.Text);
            }
            try
            {
                this.u.IsUserExists(this.editUser.Text);
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return;
            }
            MessageBox.Show("连接成功!");
            this.panel2.Enabled = true;
            this.RefreshUserList();
            this.RefreshGroupList();

        }

        private void 刷新ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RefreshUserList();

        }

        private void 创建用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new CreateUser(this.u).ShowDialog();
            this.RefreshUserList();

        }

        private void 设置用户密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbUsers.SelectedIndex >= 0)
            {
                string UserName = this.lbUsers.SelectedItem.ToString();
                new SetPassword(this.u, UserName).ShowDialog();
            }

        }

        private void 更改用户密码ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbUsers.SelectedIndex >= 0)
            {
                string UserName = this.lbUsers.SelectedItem.ToString();
                new ChangePassword(this.u, UserName).ShowDialog();
            }

        }

        private void 删除用户ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbUsers.SelectedIndex >= 0)
            {
                string UserName = this.lbUsers.SelectedItem.ToString();
                if (this.u.IsUserExists(UserName))
                {
                    if (MessageBox.Show("确定要删除用户 " + UserName + "　吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        this.u.DeleteUser(UserName);
                    }
                }
                else
                {
                    MessageBox.Show("指定的用户不存在");
                }
                this.RefreshUserList();
            }

        }

        private void 属性ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbUsers.SelectedIndex >= 0)
            {
                ModifyUser userForm = new ModifyUser(this.u, this.lbUsers.SelectedItem.ToString());
                if (userForm.ShowDialog() == DialogResult.OK)
                {
                    this.RefreshUserList();
                }
            }

        }

        private void 刷新FToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.RefreshGroupList();

        }

        private void 新建组toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new CreateGroup(this.u).ShowDialog();
            this.RefreshGroupList();

        }

        private void 删除组toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if (this.lbGroups.SelectedIndex >= 0)
            {
                string GroupName = this.lbGroups.SelectedItem.ToString();
                if (this.u.IsGroupExists(GroupName))
                {
                    if (MessageBox.Show("确定要删除组 " + GroupName + "　吗？", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.OK)
                    {
                        this.u.DeleteGroup(GroupName);
                    }
                }
                else
                {
                    MessageBox.Show("指定的组不存在");
                }
                this.RefreshGroupList();
            }

        }

        private void 属性RToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.lbGroups.SelectedIndex >= 0)
            {
                ModifyGroup groupForm = new ModifyGroup(this.u, this.lbGroups.SelectedItem.ToString());
                if (groupForm.ShowDialog() == DialogResult.OK)
                {
                    this.RefreshGroupList();
                }
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("IEXPLORE.EXE", "http://www.thinksea.com");

        }


    }
}
