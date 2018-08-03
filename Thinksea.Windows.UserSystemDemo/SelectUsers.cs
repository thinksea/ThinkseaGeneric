using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.UserSystemDemo
{
    public partial class SelectUsers : Form
    {
        private Thinksea.Windows.UserSystem u = null;
        /// <summary>
        /// 获取选择的用户。
        /// </summary>
        public string[] SelectedUsers
        {
            get
            {
                System.Collections.Generic.List<string> l = new List<string>();
                foreach (var tmp in this.lbUsers.SelectedItems)
                {
                    l.Add(tmp.ToString());
                }
                return l.ToArray();
            }

        }

        public SelectUsers(Thinksea.Windows.UserSystem u)
        {
            InitializeComponent();

            this.u = u;
            this.lbUsers.Items.AddRange(this.u.GetUsers());

        }

        private void lbUsers_DoubleClick(object sender, EventArgs e)
        {
            if (this.lbUsers.SelectedIndex != -1)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }

        }
    }
}
