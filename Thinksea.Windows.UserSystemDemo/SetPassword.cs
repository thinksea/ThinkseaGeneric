using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.UserSystemDemo
{
    public partial class SetPassword : Form
    {
        private Thinksea.Windows.UserSystem u = null;
        private string UserName = "";

        public SetPassword(Thinksea.Windows.UserSystem u, string UserName)
        {
            InitializeComponent();
            this.u = u;
            this.UserName = UserName;
            this.Text = "为 " + UserName + " 设置密码";

        }

        private void btnAccept_Click(object sender, EventArgs e)
        {
            try
            {
                u.SetPassword(this.UserName, this.editPassword.Text);
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
