using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.UserSystemDemo
{
    public partial class ModifyUser : Form
    {
        private Thinksea.Windows.UserSystem u = null;
        private string UserName = "";

        public ModifyUser(Thinksea.Windows.UserSystem u, string UserName)
        {
            InitializeComponent();

            this.u = u;
            this.UserName = UserName;

            UserInfo ui = u.GetUser(UserName);
            this.Text = ui.UserName + " 属性";
            this.lUserName.Text = ui.UserName;
            this.editFullName.Text = ui.FullName;
            this.editDescription.Text = ui.Description;
            this.cbPasswordExpired.Checked = ui.PasswordExpired;
            this.cbChangePassword.Checked = ui.ChangePassword;
            this.cbDontExpirePassword.Checked = ui.DontExpirePassword;
            this.cbEnableUser.Checked = ui.EnableUser;

            this.lbGroups.Items.AddRange(u.GetGroupsByUser(UserName));

        }

        private void btnAddGroup_Click(object sender, EventArgs e)
        {
            SelectGroups sgFrom = new SelectGroups(this.u);
            if (sgFrom.ShowDialog() == DialogResult.OK)
            {
                foreach (var tmp in sgFrom.SelectedGroups)
                {
                    if (!this.lbGroups.Items.Contains(tmp))
                    {
                        this.lbGroups.Items.Add(tmp);
                    }
                }
            }

        }

        private void btnDeleteGroup_Click(object sender, EventArgs e)
        {
            if (this.lbGroups.SelectedIndex != -1)
            {
                this.lbGroups.Items.Remove(this.lbGroups.SelectedItem);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.cbPasswordExpired.Checked)
                {
                    this.u.EnablePasswordExpired(this.UserName);
                }
                else
                {
                    this.u.DisablePasswordExpired(this.UserName);
                }

                if (this.cbChangePassword.Checked)
                {
                    this.u.EnableChangePassword(this.UserName);
                }
                else
                {
                    this.u.DisableChangePassword(this.UserName);
                }

                if (this.cbDontExpirePassword.Checked)
                {
                    this.u.EnableDontExpirePassword(this.UserName);
                }
                else
                {
                    this.u.DisableDontExpirePassword(this.UserName);
                }

                if (this.cbEnableUser.Checked)
                {
                    this.u.DisableUser(this.UserName);
                }
                else
                {
                    this.u.EnableUser(this.UserName);
                }

                foreach (var tmp in this.lbGroups.Items)
                {
                    string GroupName = tmp.ToString();
                    if (!this.u.IsUserInGroup(this.UserName, GroupName))
                    {
                        this.u.AddUserToGroup(this.UserName, GroupName);
                    }
                }
                #region 删除无用的关联
                string[] groups = this.u.GetGroupsByUser(this.UserName);
                foreach (var tmp2 in groups)
                {
                    if (!this.lbGroups.Items.Contains(tmp2))
                    {
                        this.u.RemoveUserFromGroup(this.UserName, tmp2);
                    }
                }
                #endregion

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

    }
}
