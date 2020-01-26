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
            this.btnUploadRaw = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.lFileSize_Raw = new System.Windows.Forms.Label();
            this.lFormats = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.tbFileTitle = new System.Windows.Forms.TextBox();
            this.uploadFileRaw = new Thinksea.Net.FileUploader_WinFormDemo.UploadFile();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnUploadRaw
            // 
            this.btnUploadRaw.Location = new System.Drawing.Point(129, 123);
            this.btnUploadRaw.Name = "btnUploadRaw";
            this.btnUploadRaw.Size = new System.Drawing.Size(75, 23);
            this.btnUploadRaw.TabIndex = 8;
            this.btnUploadRaw.Text = "上传(&U)";
            this.btnUploadRaw.UseVisualStyleBackColor = true;
            this.btnUploadRaw.Click += new System.EventHandler(this.btnUploadRaw_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(94, 128);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "文件";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(280, 167);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(29, 12);
            this.label17.TabIndex = 15;
            this.label17.Text = "格式";
            // 
            // lFileSize_Raw
            // 
            this.lFileSize_Raw.AutoSize = true;
            this.lFileSize_Raw.Location = new System.Drawing.Point(148, 167);
            this.lFileSize_Raw.Name = "lFileSize_Raw";
            this.lFileSize_Raw.Size = new System.Drawing.Size(65, 12);
            this.lFileSize_Raw.TabIndex = 14;
            this.lFileSize_Raw.Text = "0000.00 KB";
            // 
            // lFormats
            // 
            this.lFormats.AutoSize = true;
            this.lFormats.Location = new System.Drawing.Point(315, 167);
            this.lFormats.Name = "lFormats";
            this.lFormats.Size = new System.Drawing.Size(29, 12);
            this.lFormats.TabIndex = 16;
            this.lFormats.Text = ".zip";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(88, 167);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "文件大小";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 93);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(71, 12);
            this.label3.TabIndex = 17;
            this.label3.Text = "文件名称(&N)";
            // 
            // tbFileTitle
            // 
            this.tbFileTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbFileTitle.Location = new System.Drawing.Point(129, 89);
            this.tbFileTitle.MaxLength = 100;
            this.tbFileTitle.Name = "tbFileTitle";
            this.tbFileTitle.Size = new System.Drawing.Size(314, 21);
            this.tbFileTitle.TabIndex = 18;
            // 
            // uploadFileRaw
            // 
            this.uploadFileRaw.File = null;
            this.uploadFileRaw.Location = new System.Drawing.Point(129, 123);
            this.uploadFileRaw.Name = "uploadFileRaw";
            this.uploadFileRaw.ShowFileName = false;
            this.uploadFileRaw.Size = new System.Drawing.Size(314, 23);
            this.uploadFileRaw.TabIndex = 0;
            this.uploadFileRaw.UploadCanceled += new Thinksea.Net.FileUploader_WinFormDemo.UploadFile.UploadCanceledHandler(this.uploadFileRaw_UploadCanceled);
            this.uploadFileRaw.UploadCompleted += new Thinksea.Net.FileUploader_WinFormDemo.UploadFile.UploadCompletedHandler(this.uploadFileRaw_UploadCompleted);
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(38, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(499, 53);
            this.label1.TabIndex = 19;
            this.label1.Text = "此 Demo 使用项目“Thinksea.Net.FileUploader_AspNetCoreDemo”做为文件上传服务端。\r\n\r\n***需要先启动此项目。";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(560, 217);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.tbFileTitle);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.lFileSize_Raw);
            this.Controls.Add(this.lFormats);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnUploadRaw);
            this.Controls.Add(this.uploadFileRaw);
            this.Name = "Form1";
            this.Text = "WinForm 上传大文件示例 - Thinksea.Net.FileUploader_WinFormDemo";
            this.ResumeLayout(false);
            this.PerformLayout();

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

