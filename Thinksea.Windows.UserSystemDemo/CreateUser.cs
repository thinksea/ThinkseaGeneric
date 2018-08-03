using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.UserSystemDemo
{
    public partial class CreateUser : Form
    {
        private Thinksea.Windows.UserSystem u = null;

        public CreateUser(Thinksea.Windows.UserSystem u)
        {
            InitializeComponent();
            this.u = u;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                u.CreateUser(this.editUserName.Text, this.editFullName.Text, this.editPassword.Text, this.editDescription.Text);
                if (this.cbPasswordExpired.Checked)
                {
                    this.u.EnablePasswordExpired(this.editUserName.Text);
                }
                else
                {
                    this.u.DisablePasswordExpired(this.editUserName.Text);
                }

                if (this.cbChangePassword.Checked)
                {
                    this.u.EnableChangePassword(this.editUserName.Text);
                }
                else
                {
                    this.u.DisableChangePassword(this.editUserName.Text);
                }

                if (this.cbDontExpirePassword.Checked)
                {
                    this.u.EnableDontExpirePassword(this.editUserName.Text);
                }
                else
                {
                    this.u.DisableDontExpirePassword(this.editUserName.Text);
                }

                if (this.cbEnableUser.Checked)
                {
                    this.u.DisableUser(this.editUserName.Text);
                }
                else
                {
                    this.u.EnableUser(this.editUserName.Text);
                }

                this.editUserName.Text = "";
                this.editFullName.Text = "";
                this.editPassword.Text = "";
                this.editDescription.Text = "";
                this.cbChangePassword.Checked = false;
                this.cbDontExpirePassword.Checked = false;
                this.cbPasswordExpired.Checked = true;
                this.cbEnableUser.Checked = false;
            }
            catch(System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }

        private void cbPasswordExpired_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbPasswordExpired.Checked)
            {
                this.cbChangePassword.Enabled = false;
                this.cbChangePassword.Checked = false;
                this.cbDontExpirePassword.Enabled = false;
                this.cbDontExpirePassword.Checked = false;
            }
            else
            {
                this.cbChangePassword.Enabled = true;
                this.cbDontExpirePassword.Enabled = true;
            }

        }

        private void cbChangePassword_CheckedChanged(object sender, EventArgs e)
        {
            if (this.cbChangePassword.Checked || this.cbDontExpirePassword.Checked)
            {
                this.cbPasswordExpired.Enabled = false;
                this.cbPasswordExpired.Checked = false;
            }
            else
            {
                this.cbPasswordExpired.Enabled = true;
            }

        }
    }
}
