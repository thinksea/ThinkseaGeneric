namespace Thinksea.Windows.UserSystemDemo
{
    partial class CreateUser
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
            this.editUserName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.editFullName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.editPassword = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.editDescription = new System.Windows.Forms.TextBox();
            this.btnAccept = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cbEnableUser = new System.Windows.Forms.CheckBox();
            this.cbDontExpirePassword = new System.Windows.Forms.CheckBox();
            this.cbChangePassword = new System.Windows.Forms.CheckBox();
            this.cbPasswordExpired = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "用户名";
            // 
            // editUserName
            // 
            this.editUserName.Location = new System.Drawing.Point(97, 10);
            this.editUserName.Name = "editUserName";
            this.editUserName.Size = new System.Drawing.Size(233, 21);
            this.editUserName.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 41);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "全名";
            // 
            // editFullName
            // 
            this.editFullName.Location = new System.Drawing.Point(97, 38);
            this.editFullName.Name = "editFullName";
            this.editFullName.Size = new System.Drawing.Size(233, 21);
            this.editFullName.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 95);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "密码";
            // 
            // editPassword
            // 
            this.editPassword.Location = new System.Drawing.Point(97, 92);
            this.editPassword.Name = "editPassword";
            this.editPassword.Size = new System.Drawing.Size(233, 21);
            this.editPassword.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(13, 68);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 12);
            this.label5.TabIndex = 8;
            this.label5.Text = "用户详细描述";
            // 
            // editDescription
            // 
            this.editDescription.Location = new System.Drawing.Point(97, 65);
            this.editDescription.MaxLength = 256;
            this.editDescription.Name = "editDescription";
            this.editDescription.Size = new System.Drawing.Size(233, 21);
            this.editDescription.TabIndex = 9;
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(97, 256);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(75, 23);
            this.btnAccept.TabIndex = 10;
            this.btnAccept.Text = "创建";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(225, 256);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 11;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // cbEnableUser
            // 
            this.cbEnableUser.AutoSize = true;
            this.cbEnableUser.Location = new System.Drawing.Point(97, 196);
            this.cbEnableUser.Name = "cbEnableUser";
            this.cbEnableUser.Size = new System.Drawing.Size(84, 16);
            this.cbEnableUser.TabIndex = 15;
            this.cbEnableUser.Text = "帐户已禁用";
            this.cbEnableUser.UseVisualStyleBackColor = true;
            // 
            // cbDontExpirePassword
            // 
            this.cbDontExpirePassword.AutoSize = true;
            this.cbDontExpirePassword.Enabled = false;
            this.cbDontExpirePassword.Location = new System.Drawing.Point(97, 174);
            this.cbDontExpirePassword.Name = "cbDontExpirePassword";
            this.cbDontExpirePassword.Size = new System.Drawing.Size(120, 16);
            this.cbDontExpirePassword.TabIndex = 14;
            this.cbDontExpirePassword.Text = "用户密码永不过期";
            this.cbDontExpirePassword.UseVisualStyleBackColor = true;
            this.cbDontExpirePassword.CheckedChanged += new System.EventHandler(this.cbChangePassword_CheckedChanged);
            // 
            // cbChangePassword
            // 
            this.cbChangePassword.AutoSize = true;
            this.cbChangePassword.Enabled = false;
            this.cbChangePassword.Location = new System.Drawing.Point(97, 152);
            this.cbChangePassword.Name = "cbChangePassword";
            this.cbChangePassword.Size = new System.Drawing.Size(120, 16);
            this.cbChangePassword.TabIndex = 13;
            this.cbChangePassword.Text = "用户不能更改密码";
            this.cbChangePassword.UseVisualStyleBackColor = true;
            this.cbChangePassword.CheckedChanged += new System.EventHandler(this.cbChangePassword_CheckedChanged);
            // 
            // cbPasswordExpired
            // 
            this.cbPasswordExpired.AutoSize = true;
            this.cbPasswordExpired.Checked = true;
            this.cbPasswordExpired.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbPasswordExpired.Location = new System.Drawing.Point(97, 130);
            this.cbPasswordExpired.Name = "cbPasswordExpired";
            this.cbPasswordExpired.Size = new System.Drawing.Size(168, 16);
            this.cbPasswordExpired.TabIndex = 12;
            this.cbPasswordExpired.Text = "用户下次登录时需更改密码";
            this.cbPasswordExpired.UseVisualStyleBackColor = true;
            this.cbPasswordExpired.CheckedChanged += new System.EventHandler(this.cbPasswordExpired_CheckedChanged);
            // 
            // CreateUser
            // 
            this.AcceptButton = this.btnAccept;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(395, 312);
            this.Controls.Add(this.cbEnableUser);
            this.Controls.Add(this.cbDontExpirePassword);
            this.Controls.Add(this.cbChangePassword);
            this.Controls.Add(this.cbPasswordExpired);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.editDescription);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.editPassword);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.editFullName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.editUserName);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreateUser";
            this.ShowInTaskbar = false;
            this.Text = "创建用户";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox editUserName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox editFullName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox editPassword;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox editDescription;
        private System.Windows.Forms.Button btnAccept;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.CheckBox cbEnableUser;
        private System.Windows.Forms.CheckBox cbDontExpirePassword;
        private System.Windows.Forms.CheckBox cbChangePassword;
        private System.Windows.Forms.CheckBox cbPasswordExpired;
    }
}