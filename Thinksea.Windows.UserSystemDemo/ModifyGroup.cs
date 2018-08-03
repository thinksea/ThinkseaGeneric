using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.UserSystemDemo
{
    public partial class ModifyGroup : Form
    {
        private Thinksea.Windows.UserSystem u = null;
        private string GroupName = "";

        public ModifyGroup(Thinksea.Windows.UserSystem u, string GroupName)
        {
            InitializeComponent();

            this.u = u;
            this.GroupName = GroupName;
            GroupInfo g = this.u.GetGroup(GroupName);
            this.Text = g.GroupName + " 属性";
            this.lGroupName.Text = g.GroupName;
            this.editDescription.Text = g.Description;
            this.lbUsers.Items.AddRange(this.u.GetUsersByGroup(GroupName));

        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            SelectUsers suFrom = new SelectUsers(this.u);
            if (suFrom.ShowDialog() == DialogResult.OK)
            {
                foreach (var tmp in suFrom.SelectedUsers)
                {
                    if (!this.lbUsers.Items.Contains(tmp))
                    {
                        this.lbUsers.Items.Add(tmp);
                    }
                }
            }

        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (this.lbUsers.SelectedIndex != -1)
            {
                this.lbUsers.Items.Remove(this.lbUsers.SelectedItem);
            }

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                this.u.SetGroupDescription(this.GroupName, this.editDescription.Text);
                foreach (var tmp in this.lbUsers.Items)
                {
                    string UserName = tmp.ToString();
                    if (!this.u.IsUserInGroup(UserName, this.GroupName))
                    {
                        this.u.AddUserToGroup(UserName, this.GroupName);
                    }
                }
                #region 删除无用的关联
                string [] users = this.u.GetUsersByGroup(this.GroupName);
                foreach( var tmp2 in users)
                {
                    if (!this.lbUsers.Items.Contains(tmp2))
                    {
                        this.u.RemoveUserFromGroup(tmp2, this.GroupName);
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
