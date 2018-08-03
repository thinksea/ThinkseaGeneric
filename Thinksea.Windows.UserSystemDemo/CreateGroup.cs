using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.UserSystemDemo
{
    public partial class CreateGroup : Form
    {
        private Thinksea.Windows.UserSystem u = null;

        public CreateGroup(Thinksea.Windows.UserSystem u)
        {
            InitializeComponent();
            this.u = u;
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {
                this.u.CreateGroup(this.editGroupName.Text, this.editDescription.Text);
                foreach (var tmp in this.lbUsers.Items)
                {
                    this.u.AddUserToGroup(tmp.ToString(), this.editGroupName.Text);
                }

                this.editGroupName.Text = "";
                this.editDescription.Text = "";
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
}
