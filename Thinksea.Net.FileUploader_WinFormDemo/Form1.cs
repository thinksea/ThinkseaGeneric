using System;
using System.Windows.Forms;

namespace Thinksea.Net.FileUploader_WinFormDemo
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		private void btnUploadRaw_Click(object sender, EventArgs e)
		{
			this.openFileDialog1.Filter = "全部文件|*.*";
			if (this.openFileDialog1.ShowDialog(this) == DialogResult.OK)
			{
				string file = this.openFileDialog1.FileName;

				this.uploadFileRaw.File = file;
				this.uploadFileRaw.Show();
				this.btnUploadRaw.Hide();
				if (!this.tbFileTitle.Modified)
				{
					this.tbFileTitle.Text = System.IO.Path.GetFileNameWithoutExtension(file);
				}
				this.uploadFileRaw.BeginUploadFile("");
			}
		}

		private void uploadFileRaw_UploadCompleted(object sender, UploadCompletedEventArgs e)
		{
			this.uploadFileRaw.Hide();
			this.btnUploadRaw.Show();
			string file = this.openFileDialog1.FileName;
			string ext = System.IO.Path.GetExtension(file).ToLower();
			Thinksea.Net.FileUploader_WinFormDemo.FileUploadResult data = Thinksea.Net.FileUploader_WinFormDemo.FileUploadResult.ConvertFrom(e.ResultData);
			string fileSavePath = data.SavePath;
			this.lFileSize_Raw.Text = Thinksea.General.ConvertToFileSize(data.FileLength);
			this.lFormats.Text = ext;
			if (data.IsFastUpload)
			{
				MessageBox.Show(this, "秒传完成！", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void uploadFileRaw_UploadCanceled(object sender, System.EventArgs e)
		{
			this.uploadFileRaw.Hide();
			this.btnUploadRaw.Show();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.uploadFileRaw.CancelUploadFile();
		}

	}
}
