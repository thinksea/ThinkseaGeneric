namespace Thinksea.Net.FileUploader_WinFormDemo
{
    partial class UploadFile
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.textProgressBar1 = new Thinksea.Net.FileUploader_WinFormDemo.TextProgressBar();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.BackgroundImage = global::Thinksea.Net.FileUploader_WinFormDemo.Properties.Resources.remove;
            this.btnCancel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCancel.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnCancel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnCancel.Location = new System.Drawing.Point(284, 0);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(30, 23);
            this.btnCancel.TabIndex = 1;
            this.toolTip1.SetToolTip(this.btnCancel, "取消上传");
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Visible = false;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // textProgressBar1
            // 
            this.textProgressBar1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textProgressBar1.Location = new System.Drawing.Point(0, 0);
            this.textProgressBar1.Name = "textProgressBar1";
            this.textProgressBar1.Size = new System.Drawing.Size(284, 23);
            this.textProgressBar1.TabIndex = 2;
            // 
            // UploadFile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.textProgressBar1);
            this.Controls.Add(this.btnCancel);
            this.Name = "UploadFile";
            this.Size = new System.Drawing.Size(314, 23);
            this.Load += new System.EventHandler(this.UploadFile_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.ToolTip toolTip1;
        private Thinksea.Net.FileUploader_WinFormDemo.TextProgressBar textProgressBar1;
    }
}
