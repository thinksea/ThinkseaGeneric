using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.UserSystemDemo
{
    public partial class SelectGroups : Form
    {
        private Thinksea.Windows.UserSystem u = null;
        /// <summary>
        /// 获取选择的组。
        /// </summary>
        public string[] SelectedGroups
        {
            get
            {
                System.Collections.Generic.List<string> l = new List<string>();
                foreach( var tmp in this.lbGroups.SelectedItems)
                {
                    l.Add(tmp.ToString());
                }
                return l.ToArray();
            }

        }

        public SelectGroups(Thinksea.Windows.UserSystem u)
        {
            InitializeComponent();

            this.u = u;
            this.lbGroups.Items.AddRange(this.u.GetGroups());

        }

        private void lbGroups_DoubleClick(object sender, EventArgs e)
        {
            if (this.lbGroups.SelectedIndex != -1)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
            }


        }

    }
}
