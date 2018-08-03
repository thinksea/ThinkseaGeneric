using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DataGridViewPageDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridViewPage1_OnRequestNewPage(object sender, Thinksea.Windows.Forms.RequestNewPageEventArgs e)
        {
            System.Random rand = new System.Random();

            System.Data.DataTable dt = e.Data;
            dt.Columns.Add("Name");
            for (int i = 0; i < e.MaxCount + 1; i++)
            {
                //dt.Rows.Add(rand.Next().ToString());
                dt.Rows.Add((e.StartIndex + i).ToString());
            }
        }

        private void dataGridViewPage1_SaveChanged(object sender, Thinksea.Windows.Forms.SaveChangedEventArgs e)
        {
        }

        private void dataGridViewPage1_PageIndexChange(object sender, Thinksea.Windows.Forms.PageIndexChangeEventArgs e)
        {
        }
    }
}
