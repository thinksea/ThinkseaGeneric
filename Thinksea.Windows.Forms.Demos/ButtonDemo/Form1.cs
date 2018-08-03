using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ButtonDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("OK");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.button1.ButtonState = !this.button1.ButtonState;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
