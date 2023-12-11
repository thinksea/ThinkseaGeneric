namespace Thinksea.Net.FileUploader_WinFormDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要修改
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent()
		{
			btnUploadRaw = new System.Windows.Forms.Button();
			label5 = new System.Windows.Forms.Label();
			label17 = new System.Windows.Forms.Label();
			lFileSize_Raw = new System.Windows.Forms.Label();
			lFormats = new System.Windows.Forms.Label();
			label6 = new System.Windows.Forms.Label();
			openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
			label3 = new System.Windows.Forms.Label();
			tbFileTitle = new System.Windows.Forms.TextBox();
			uploadFileRaw = new UploadFile();
			label1 = new System.Windows.Forms.Label();
			SuspendLayout();
			// 
			// btnUploadRaw
			// 
			btnUploadRaw.Location = new System.Drawing.Point(150, 174);
			btnUploadRaw.Margin = new System.Windows.Forms.Padding(4);
			btnUploadRaw.Name = "btnUploadRaw";
			btnUploadRaw.Size = new System.Drawing.Size(88, 33);
			btnUploadRaw.TabIndex = 8;
			btnUploadRaw.Text = "上传(&U)";
			btnUploadRaw.UseVisualStyleBackColor = true;
			btnUploadRaw.Click += btnUploadRaw_Click;
			// 
			// label5
			// 
			label5.AutoSize = true;
			label5.Location = new System.Drawing.Point(110, 181);
			label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label5.Name = "label5";
			label5.Size = new System.Drawing.Size(32, 17);
			label5.TabIndex = 9;
			label5.Text = "文件";
			// 
			// label17
			// 
			label17.AutoSize = true;
			label17.Location = new System.Drawing.Point(327, 237);
			label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label17.Name = "label17";
			label17.Size = new System.Drawing.Size(32, 17);
			label17.TabIndex = 15;
			label17.Text = "格式";
			// 
			// lFileSize_Raw
			// 
			lFileSize_Raw.AutoSize = true;
			lFileSize_Raw.Location = new System.Drawing.Point(173, 237);
			lFileSize_Raw.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lFileSize_Raw.Name = "lFileSize_Raw";
			lFileSize_Raw.Size = new System.Drawing.Size(73, 17);
			lFileSize_Raw.TabIndex = 14;
			lFileSize_Raw.Text = "0000.00 KB";
			// 
			// lFormats
			// 
			lFormats.AutoSize = true;
			lFormats.Location = new System.Drawing.Point(368, 237);
			lFormats.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			lFormats.Name = "lFormats";
			lFormats.Size = new System.Drawing.Size(28, 17);
			lFormats.TabIndex = 16;
			lFormats.Text = ".zip";
			// 
			// label6
			// 
			label6.AutoSize = true;
			label6.Location = new System.Drawing.Point(103, 237);
			label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label6.Name = "label6";
			label6.Size = new System.Drawing.Size(56, 17);
			label6.TabIndex = 13;
			label6.Text = "文件大小";
			// 
			// label3
			// 
			label3.AutoSize = true;
			label3.Location = new System.Drawing.Point(61, 132);
			label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label3.Name = "label3";
			label3.Size = new System.Drawing.Size(74, 17);
			label3.TabIndex = 17;
			label3.Text = "文件名称(&N)";
			// 
			// tbFileTitle
			// 
			tbFileTitle.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
			tbFileTitle.Location = new System.Drawing.Point(150, 126);
			tbFileTitle.Margin = new System.Windows.Forms.Padding(4);
			tbFileTitle.MaxLength = 100;
			tbFileTitle.Name = "tbFileTitle";
			tbFileTitle.Size = new System.Drawing.Size(366, 23);
			tbFileTitle.TabIndex = 18;
			// 
			// uploadFileRaw
			// 
			uploadFileRaw.File = null;
			uploadFileRaw.Location = new System.Drawing.Point(150, 174);
			uploadFileRaw.Margin = new System.Windows.Forms.Padding(5, 6, 5, 6);
			uploadFileRaw.Name = "uploadFileRaw";
			uploadFileRaw.ShowFileName = false;
			uploadFileRaw.Size = new System.Drawing.Size(366, 33);
			uploadFileRaw.TabIndex = 0;
			uploadFileRaw.UploadCanceled += uploadFileRaw_UploadCanceled;
			uploadFileRaw.UploadCompleted += uploadFileRaw_UploadCompleted;
			// 
			// label1
			// 
			label1.Location = new System.Drawing.Point(44, 27);
			label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
			label1.Name = "label1";
			label1.Size = new System.Drawing.Size(582, 75);
			label1.TabIndex = 19;
			label1.Text = "此 Demo 使用项目“Thinksea.Net.FileUploader_AspNetCoreDemo”做为文件上传服务端。\r\n\r\n***需要先启动此项目。";
			// 
			// Form1
			// 
			AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
			AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			ClientSize = new System.Drawing.Size(653, 307);
			Controls.Add(label1);
			Controls.Add(label3);
			Controls.Add(tbFileTitle);
			Controls.Add(label17);
			Controls.Add(lFileSize_Raw);
			Controls.Add(lFormats);
			Controls.Add(label6);
			Controls.Add(label5);
			Controls.Add(btnUploadRaw);
			Controls.Add(uploadFileRaw);
			Margin = new System.Windows.Forms.Padding(4);
			Name = "Form1";
			Text = "WinForm 上传大文件示例 - Thinksea.Net.FileUploader_WinFormDemo";
			FormClosing += Form1_FormClosing;
			ResumeLayout(false);
			PerformLayout();
		}

		#endregion

		private UploadFile uploadFileRaw;
        private System.Windows.Forms.Button btnUploadRaw;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label lFileSize_Raw;
        private System.Windows.Forms.Label lFormats;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbFileTitle;
        private System.Windows.Forms.Label label1;
    }
}

