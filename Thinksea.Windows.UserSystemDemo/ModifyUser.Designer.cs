namespace Thinksea.Windows.UserSystemDemo
{
    partial class ModifyUser
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
            this.label1 = new System.Windows.Forms.Label();
            this.editFullName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editDescription = new System.Windows.Forms.TextBox();
            this.cbPasswordExpired = new System.Windows.Forms.CheckBox();
            this.cbChangePassword = new System.Windows.Forms.CheckBox();
            this.cbDontExpirePassword = new System.Windows.Forms.CheckBox();
            this.cbEnableUser = new System.Windows.Forms.CheckBox();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.btnSave = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.lUserName = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btnDeleteGroup = new System.Windows.Forms.Button();
            this.btnAddGroup = new System.Windows.Forms.Button();
            this.lbGroups = new System.Windows.Forms.ListBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(35, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "全名";
            // 
            // editFullName
            // 
            this.editFullName.Location = new System.Drawing.Point(70, 28);
            this.editFullName.Name = "editFullName";
            this.editFullName.Size = new System.Drawing.Size(100, 21);
            this.editFullName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(35, 57);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "描述";
            // 
            // editDescription
            // 
            this.editDescription.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.editDescription.Location = new System.Drawing.Point(70, 54);
            this.editDescription.MaxLength = 256;
            this.editDescription.Multiline = true;
            this.editDescription.Name = "editDescription";
            this.editDescription.Size = new System.Drawing.Size(378, 43);
            this.editDescription.TabIndex = 3;
            // 
            // cbPasswordExpired
            // 
            this.cbPasswordExpired.AutoSize = true;
            this.cbPasswordExpired.Location = new System.Drawing.Point(70, 103);
            this.cbPasswordExpired.Name = "cbPasswordExpired";
            this.cbPasswordExpired.Size = new System.Drawing.Size(168, 16);
            this.cbPasswordExpired.TabIndex = 4;
            this.cbPasswordExpired.Text = "用户下次登录时需更改密码";
            this.cbPasswordExpired.UseVisualStyleBackColor = true;
            // 
            // cbChangePassword
            // 
            this.cbChangePassword.AutoSize = true;
            this.cbChangePassword.Location = new System.Drawing.Point(70, 125);
            this.cbChangePassword.Name = "cbChangePassword";
            this.cbChangePassword.Size = new System.Drawing.Size(120, 16);
            this.cbChangePassword.TabIndex = 5;
            this.cbChangePassword.Text = "用户不能更改密码";
            this.cbChangePassword.UseVisualStyleBackColor = true;
            // 
            // cbDontExpirePassword
            // 
            this.cbDontExpirePassword.AutoSize = true;
            this.cbDontExpirePassword.Location = new System.Drawing.Point(70, 147);
            this.cbDontExpirePassword.Name = "cbDontExpirePassword";
            this.cbDontExpirePassword.Size = new System.Drawing.Size(120, 16);
            this.cbDontExpirePassword.TabIndex = 6;
            this.cbDontExpirePassword.Text = "用户密码永不过期";
            this.cbDontExpirePassword.UseVisualStyleBackColor = true;
            // 
            // cbEnableUser
            // 
            this.cbEnableUser.AutoSize = true;
            this.cbEnableUser.Location = new System.Drawing.Point(70, 169);
            this.cbEnableUser.Name = "cbEnableUser";
            this.cbEnableUser.Size = new System.Drawing.Size(84, 16);
            this.cbEnableUser.TabIndex = 7;
            this.cbEnableUser.Text = "帐户已禁用";
            this.cbEnableUser.UseVisualStyleBackColor = true;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Enabled = false;
            this.checkBox5.Location = new System.Drawing.Point(70, 191);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(84, 16);
            this.checkBox5.TabIndex = 8;
            this.checkBox5.Text = "帐户已锁定";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // btnSave
            // 
            this.btnSave.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnSave.Location = new System.Drawing.Point(107, 269);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(75, 23);
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "保存";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(257, 269);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(21, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(481, 240);
            this.tabControl1.TabIndex = 11;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.lUserName);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.editFullName);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.checkBox5);
            this.tabPage1.Controls.Add(this.editDescription);
            this.tabPage1.Controls.Add(this.cbEnableUser);
            this.tabPage1.Controls.Add(this.cbPasswordExpired);
            this.tabPage1.Controls.Add(this.cbDontExpirePassword);
            this.tabPage1.Controls.Add(this.cbChangePassword);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(473, 214);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "常规";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // lUserName
            // 
            this.lUserName.AutoSize = true;
            this.lUserName.Location = new System.Drawing.Point(68, 13);
            this.lUserName.Name = "lUserName";
            this.lUserName.Size = new System.Drawing.Size(0, 12);
            this.lUserName.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(21, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "用户名";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.btnDeleteGroup);
            this.tabPage2.Controls.Add(this.btnAddGroup);
            this.tabPage2.Controls.Add(this.lbGroups);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(473, 214);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "隶属于";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // btnDeleteGroup
            // 
            this.btnDeleteGroup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnDeleteGroup.Location = new System.Drawing.Point(199, 185);
            this.btnDeleteGroup.Name = "btnDeleteGroup";
            this.btnDeleteGroup.Size = new System.Drawing.Size(75, 23);
            this.btnDeleteGroup.TabIndex = 3;
            this.btnDeleteGroup.Text = "删除";
            this.btnDeleteGroup.UseVisualStyleBackColor = true;
            this.btnDeleteGroup.Click += new System.EventHandler(this.btnDeleteGroup_Click);
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btnAddGroup.Location = new System.Drawing.Point(65, 185);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(75, 23);
            this.btnAddGroup.TabIndex = 2;
            this.btnAddGroup.Text = "添加";
            this.btnAddGroup.UseVisualStyleBackColor = true;
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // lbGroups
            // 
            this.lbGroups.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbGroups.FormattingEnabled = true;
            this.lbGroups.ItemHeight = 12;
            this.lbGroups.Location = new System.Drawing.Point(18, 28);
            this.lbGroups.Name = "lbGroups";
            this.lbGroups.Size = new System.Drawing.Size(439, 136);
            this.lbGroups.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(41, 12);
            this.label4.TabIndex = 0;
            this.label4.Text = "隶属于";
            // 
            // ModifyUser
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(523, 301);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnSave);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ModifyUser";
            this.ShowInTaskbar = false;
            this.Text = "用户属性";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox editFullName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editDescription;
        private System.Windows.Forms.CheckBox cbPasswordExpired;
        private System.Windows.Forms.CheckBox cbChangePassword;
        private System.Windows.Forms.CheckBox cbDontExpirePassword;
        private System.Windows.Forms.CheckBox cbEnableUser;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label lUserName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListBox lbGroups;
        private System.Windows.Forms.Button btnDeleteGroup;
        private System.Windows.Forms.Button btnAddGroup;
    }
}