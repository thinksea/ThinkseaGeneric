using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Thinksea.Windows.Forms;

namespace EllipsisControlDemo
{
	public partial class Demo : Form
	{
		public Demo()
		{
			InitializeComponent();

			CurrentText = Application.ExecutablePath;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				CurrentText = openFileDialog1.FileName;
			}
		}

		private void ellipsisMenuToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
		{
			// the menu item re-uses the label's text and width 
			menuItem1ToolStripMenuItem.Text = Thinksea.Windows.Forms.Ellipsis.Compact(labelEllipsis1.FullText, labelEllipsis1, CurrentEllipsisFormat);
		}

		private void EllipsisFormatChanged(object sender, EventArgs e)
		{
			textBoxEllipsis1.AutoEllipsis = labelEllipsis1.AutoEllipsis = CurrentEllipsisFormat;
		}

		string CurrentText
		{
			set { textBoxEllipsis1.FullText = labelEllipsis1.FullText = value; }
		}

		Thinksea.Windows.Forms.EllipsisFormat CurrentEllipsisFormat
		{
			get
			{
				EllipsisFormat fmt = EllipsisFormat.None;

				if (radioButton_Left.Checked) { fmt |= EllipsisFormat.Start; }
				if (radioButton_Right.Checked) { fmt |= EllipsisFormat.End; }
				if (radioButton_Center.Checked) { fmt |= EllipsisFormat.Middle; }
				if (checkBox_Path.Checked) { fmt |= EllipsisFormat.Path; }
				if (checkBox_Word.Checked) { fmt |= EllipsisFormat.Word; }

				return fmt;
			}
		}
	}
}