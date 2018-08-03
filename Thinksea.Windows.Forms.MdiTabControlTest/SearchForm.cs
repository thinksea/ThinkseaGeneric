using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Collections;
using Microsoft.VisualBasic;
using System.Collections.Generic;
using System;


namespace MdiTabControlTest
{
    public partial class SearchForm : System.Windows.Forms.Form
    {
        public SearchForm()
        {
            InitializeComponent();
        }

        public void NewToolStripButton_Click(System.Object sender, System.EventArgs e)
        {
            Detail d = new Detail();
            d.Owner = this;
            d.TopLevel = true;
            d.Show();
        }

        public void SearchForm_Activated(object sender, System.EventArgs e)
        {
        }

        public void SearchForm_Enter(object sender, System.EventArgs e)
        {
            txtSearch.Focus();
        }

        public void SearchForm_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
        {
            e.Cancel = MessageBox.Show(this, "Close this form?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No;
        }

    }

}
