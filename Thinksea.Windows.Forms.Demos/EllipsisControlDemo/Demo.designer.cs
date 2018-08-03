namespace EllipsisControlDemo
{
	partial class Demo
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuEllipsisStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.button1 = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.checkBox_Word = new System.Windows.Forms.CheckBox();
            this.checkBox_Path = new System.Windows.Forms.CheckBox();
            this.radioButton_Right = new System.Windows.Forms.RadioButton();
            this.radioButton_Center = new System.Windows.Forms.RadioButton();
            this.radioButton_Left = new System.Windows.Forms.RadioButton();
            this.radioButton_None = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.labelEllipsis1 = new Thinksea.Windows.Forms.LabelEllipsis();
            this.textBoxEllipsis1 = new Thinksea.Windows.Forms.TextBoxEllipsis();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.menuStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuEllipsisStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(396, 25);
            this.menuStrip1.TabIndex = 1;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuEllipsisStripMenuItem
            // 
            this.menuEllipsisStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem1ToolStripMenuItem});
            this.menuEllipsisStripMenuItem.Name = "menuEllipsisStripMenuItem";
            this.menuEllipsisStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.menuEllipsisStripMenuItem.Text = "&Demo Menu";
            this.menuEllipsisStripMenuItem.DropDownOpening += new System.EventHandler(this.ellipsisMenuToolStripMenuItem_DropDownOpening);
            // 
            // menuItem1ToolStripMenuItem
            // 
            this.menuItem1ToolStripMenuItem.Name = "menuItem1ToolStripMenuItem";
            this.menuItem1ToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.menuItem1ToolStripMenuItem.Text = "menuItem1";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(182, 143);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 21);
            this.button1.TabIndex = 0;
            this.button1.Text = "&Browse...";
            this.toolTip1.SetToolTip(this.button1, "Select and display a filename in the LabelEllipsis and TextBoxEllipsis controls");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.AddExtension = false;
            this.openFileDialog1.CheckFileExists = false;
            this.openFileDialog1.CheckPathExists = false;
            this.openFileDialog1.DereferenceLinks = false;
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.ValidateNames = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.checkBox_Word);
            this.groupBox1.Controls.Add(this.checkBox_Path);
            this.groupBox1.Location = new System.Drawing.Point(295, 135);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(89, 63);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ellipsis options";
            // 
            // checkBox_Word
            // 
            this.checkBox_Word.AutoSize = true;
            this.checkBox_Word.Location = new System.Drawing.Point(15, 39);
            this.checkBox_Word.Name = "checkBox_Word";
            this.checkBox_Word.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Word.TabIndex = 1;
            this.checkBox_Word.Text = "&Word";
            this.toolTip1.SetToolTip(this.checkBox_Word, "Text is trimmed at a word boundary");
            this.checkBox_Word.UseVisualStyleBackColor = true;
            this.checkBox_Word.CheckedChanged += new System.EventHandler(this.EllipsisFormatChanged);
            // 
            // checkBox_Path
            // 
            this.checkBox_Path.AutoSize = true;
            this.checkBox_Path.Checked = true;
            this.checkBox_Path.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox_Path.Location = new System.Drawing.Point(15, 18);
            this.checkBox_Path.Name = "checkBox_Path";
            this.checkBox_Path.Size = new System.Drawing.Size(48, 16);
            this.checkBox_Path.TabIndex = 0;
            this.checkBox_Path.Text = "&Path";
            this.toolTip1.SetToolTip(this.checkBox_Path, "Preserve as much as possible of the drive and filename information");
            this.checkBox_Path.UseVisualStyleBackColor = true;
            this.checkBox_Path.CheckedChanged += new System.EventHandler(this.EllipsisFormatChanged);
            // 
            // radioButton_Right
            // 
            this.radioButton_Right.AutoSize = true;
            this.radioButton_Right.Location = new System.Drawing.Point(18, 60);
            this.radioButton_Right.Name = "radioButton_Right";
            this.radioButton_Right.Size = new System.Drawing.Size(41, 16);
            this.radioButton_Right.TabIndex = 4;
            this.radioButton_Right.Text = "&End";
            this.toolTip1.SetToolTip(this.radioButton_Right, "Text is trimmed at the end of the string");
            this.radioButton_Right.UseVisualStyleBackColor = true;
            this.radioButton_Right.CheckedChanged += new System.EventHandler(this.EllipsisFormatChanged);
            // 
            // radioButton_Center
            // 
            this.radioButton_Center.AutoSize = true;
            this.radioButton_Center.Location = new System.Drawing.Point(17, 81);
            this.radioButton_Center.Name = "radioButton_Center";
            this.radioButton_Center.Size = new System.Drawing.Size(59, 16);
            this.radioButton_Center.TabIndex = 5;
            this.radioButton_Center.Text = "&Middle";
            this.toolTip1.SetToolTip(this.radioButton_Center, "Text is trimmed in the middle of the string");
            this.radioButton_Center.UseVisualStyleBackColor = true;
            this.radioButton_Center.CheckedChanged += new System.EventHandler(this.EllipsisFormatChanged);
            // 
            // radioButton_Left
            // 
            this.radioButton_Left.AutoSize = true;
            this.radioButton_Left.Checked = true;
            this.radioButton_Left.Location = new System.Drawing.Point(18, 39);
            this.radioButton_Left.Name = "radioButton_Left";
            this.radioButton_Left.Size = new System.Drawing.Size(53, 16);
            this.radioButton_Left.TabIndex = 3;
            this.radioButton_Left.TabStop = true;
            this.radioButton_Left.Text = "&Start";
            this.toolTip1.SetToolTip(this.radioButton_Left, "Text is trimmed at the begining of the string");
            this.radioButton_Left.UseVisualStyleBackColor = true;
            this.radioButton_Left.CheckedChanged += new System.EventHandler(this.EllipsisFormatChanged);
            // 
            // radioButton_None
            // 
            this.radioButton_None.AutoSize = true;
            this.radioButton_None.Location = new System.Drawing.Point(18, 18);
            this.radioButton_None.Name = "radioButton_None";
            this.radioButton_None.Size = new System.Drawing.Size(47, 16);
            this.radioButton_None.TabIndex = 2;
            this.radioButton_None.Text = "&None";
            this.toolTip1.SetToolTip(this.radioButton_None, "Text is not modified");
            this.radioButton_None.UseVisualStyleBackColor = true;
            this.radioButton_None.CheckedChanged += new System.EventHandler(this.EllipsisFormatChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.radioButton_None);
            this.groupBox2.Controls.Add(this.radioButton_Left);
            this.groupBox2.Controls.Add(this.radioButton_Center);
            this.groupBox2.Controls.Add(this.radioButton_Right);
            this.groupBox2.Location = new System.Drawing.Point(295, 25);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(89, 104);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Ellipsis align";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.labelEllipsis1);
            this.groupBox3.Controls.Add(this.textBoxEllipsis1);
            this.groupBox3.Controls.Add(this.button1);
            this.groupBox3.Location = new System.Drawing.Point(12, 25);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(272, 173);
            this.groupBox3.TabIndex = 6;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Demo zone";
            // 
            // labelEllipsis1
            // 
            this.labelEllipsis1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.labelEllipsis1.AutoEllipsis = Thinksea.Windows.Forms.EllipsisFormat.Start;
            this.labelEllipsis1.Location = new System.Drawing.Point(9, 39);
            this.labelEllipsis1.Name = "labelEllipsis1";
            this.labelEllipsis1.Size = new System.Drawing.Size(248, 17);
            this.labelEllipsis1.TabIndex = 2;
            // 
            // textBoxEllipsis1
            // 
            this.textBoxEllipsis1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBoxEllipsis1.AutoEllipsis = ((Thinksea.Windows.Forms.EllipsisFormat)((Thinksea.Windows.Forms.EllipsisFormat.Start | Thinksea.Windows.Forms.EllipsisFormat.Path)));
            this.textBoxEllipsis1.FullText = "D:\\Documents and Settings\\×ÀÃæ\\´ý×ª»»¿Ø¼þ\\AutoEllipsis";
            this.textBoxEllipsis1.Location = new System.Drawing.Point(9, 99);
            this.textBoxEllipsis1.Name = "textBoxEllipsis1";
            this.textBoxEllipsis1.Size = new System.Drawing.Size(248, 21);
            this.textBoxEllipsis1.TabIndex = 3;
            // 
            // Demo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 205);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.MaximumSize = new System.Drawing.Size(2048, 243);
            this.MinimumSize = new System.Drawing.Size(16, 243);
            this.Name = "Demo";
            this.Text = "Auto Ellipsis";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private Thinksea.Windows.Forms.LabelEllipsis labelEllipsis1;
		private Thinksea.Windows.Forms.TextBoxEllipsis textBoxEllipsis1;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem menuEllipsisStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem menuItem1ToolStripMenuItem;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.OpenFileDialog openFileDialog1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.CheckBox checkBox_Path;
		private System.Windows.Forms.CheckBox checkBox_Word;
		private System.Windows.Forms.RadioButton radioButton_Right;
		private System.Windows.Forms.RadioButton radioButton_Center;
		private System.Windows.Forms.RadioButton radioButton_Left;
		private System.Windows.Forms.RadioButton radioButton_None;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolTip toolTip1;
	}
}

