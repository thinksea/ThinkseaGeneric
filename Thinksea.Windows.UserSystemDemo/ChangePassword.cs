using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.UserSystemDemo
{
    public partial class ChangePassword : Form
    {
        private Thinksea.Windows.UserSystem u = null;
        private string UserName = "";

        public ChangePassword(Thinksea.Windows.UserSystem u, string UserName)
        {
            InitializeComponent();
            this.u = u;
            this.UserName = UserName;
            this.Text = "为 " + UserName + " 修改密码";
        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                u.ChangeUserPassword(this.UserName, this.editOldPassword.Text, this.editNewPassword.Text);
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
