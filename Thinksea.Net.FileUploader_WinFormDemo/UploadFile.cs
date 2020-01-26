using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Net.FileUploader_WinFormDemo
{
    public partial class UploadFile : UserControl
    {
        private Thinksea.Net.FileUploader.HttpFileUpload httpFileUpload = null;
        private bool _ShowFileName = false;
        public bool ShowFileName
        {
            get
            {
                return this._ShowFileName;
            }
            set
            {
                this._ShowFileName = value;
            }
        }
        /// <summary>
        /// 获取或设置待上传的文件。
        /// </summary>
        public string File
        {
            get;
            set;
        }

        private System.IO.FileStream fileStream;
        /// <summary>
        /// 取消上传文件事件代理。
        /// </summary>
        public delegate void UploadCanceledHandler(object sender, System.EventArgs e);
        private event UploadCanceledHandler _UploadCanceled;
        /// <summary>
        /// 当取消上传文件后引发此事件。
        /// </summary>
        [Description("当取消上传文件后引发此事件。")]
        public event UploadCanceledHandler UploadCanceled
        {
            add
            {
                this._UploadCanceled += value;
            }
            remove
            {
                this._UploadCanceled -= value;
            }
        }

        /// <summary>
        /// 上传文件完成事件代理。
        /// </summary>
        public delegate void UploadCompletedHandler(object sender, UploadCompletedEventArgs e);
        private event UploadCompletedHandler _UploadCompleted;
        /// <summary>
        /// 当上传文件完成后引发此事件。
        /// </summary>
        [Description("当上传文件完成后引发此事件。")]
        public event UploadCompletedHandler UploadCompleted
        {
            add
            {
                this._UploadCompleted += value;
            }
            remove
            {
                this._UploadCompleted -= value;
            }
        }

        /// <summary>
        /// 上传起始时间。
        /// </summary>
        private System.DateTime uploadStartTime;
        /// <summary>
        /// 指示上传是否已完成。
        /// </summary>
        public bool IsUploadComplete
        {
            get;
            private set;
        }

        /// <summary>
        /// 开始上传文件。
        /// </summary>
        public void BeginUploadFile(string CustomParameter)
        {
            if (string.IsNullOrEmpty(this.File))
            {
                throw new System.Exception("必须设置属性“File”。");
            }
            this.IsUploadComplete = false;
            string fileName = System.IO.Path.GetFileName(this.File);
            this.httpFileUpload = new Thinksea.Net.FileUploader.HttpFileUpload();
            this.httpFileUpload.UploadServiceUrl = Define.HttpUploadHandlerServiceURL;
            this.httpFileUpload.UploadProgressChanged += HttpFileUpload_UploadProgressChanged;
            this.httpFileUpload.ErrorOccurred += HttpFileUpload_ErrorOccurred;
            this.httpFileUpload.FindBreakpoint += HttpFileUpload_FindBreakpoint;
            this.textProgressBar1.Value = 0;
            this.textProgressBar1.Text = "正在计算……";
            this.fileStream = new System.IO.FileStream(this.File, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            this.httpFileUpload.StartUpload(fileStream, fileName, CustomParameter);
            this.uploadStartTime = System.DateTime.Now;
            this.btnCancel.Show();
        }

        private void HttpFileUpload_FindBreakpoint(object sender, Thinksea.Net.FileUploader.BreakpointUploadEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Thinksea.Net.FileUploader.BreakpointUploadEventHandler(this.HttpFileUpload_FindBreakpoint), sender, e);
            }
            else
            {
                if (e.Breakpoint > 0)
                {
                    if (MessageBox.Show(this, "我们发现这个文件曾上传过部分内容，请确认是否续传或重传。", "", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) != DialogResult.OK)
                    {
                        e.Breakpoint = 0;
                    }
                }
            }
        }

        private void HttpFileUpload_ErrorOccurred(object sender, Thinksea.Net.FileUploader.UploadErrorEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Thinksea.Net.FileUploader.UploadErrorEventHandler(this.HttpFileUpload_ErrorOccurred), sender, e);
            }
            else
            {
                MessageBox.Show(this, e.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void HttpFileUpload_UploadProgressChanged(object sender, Thinksea.Net.FileUploader.UploadProgressChangedEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new Thinksea.Net.FileUploader.UploadProgressChangedEventHandler(this.HttpFileUpload_UploadProgressChanged), sender, e);
            }
            else
            {
                #region 处理上传进度已更改事件。
                int v = System.Convert.ToInt32(e.FinishedSize * 1.0 / e.FileLength * this.textProgressBar1.Maximum);
                this.textProgressBar1.Value = v;
                System.TimeSpan dsp = (System.DateTime.Now - this.uploadStartTime);
                if (dsp.TotalSeconds > 0)
                {
                    double speed = e.FinishedSize / dsp.TotalSeconds; //平均传输速度（单位：字节/秒）。
                    double ds = (e.FileLength - e.FinishedSize) / speed; //大概剩余时间（单位：秒）。
                    System.TimeSpan tsds = System.TimeSpan.FromSeconds(ds);
                    string text = Thinksea.General.ConvertToFileSize(System.Convert.ToInt64(speed)) + "/秒";

                    if (tsds.TotalHours > 1)
                    {
                        text += "，剩余时间 " + tsds.ToString();
                    }
                    else if (tsds.TotalMinutes > 1)
                    {
                        text += "，剩余时间 " + string.Format("{0}:{1}", tsds.Minutes, tsds.Seconds);
                    }
                    else
                    {
                        text += "，剩余时间少于1分钟";
                    }
                    if (this.ShowFileName)
                    {
                        text += "，" + System.IO.Path.GetFileName(this.File);
                    }
                    this.textProgressBar1.Text = text;
                }
                #endregion

                if (e.FinishedSize == e.FileLength) //上传完成
                {
                    if (this.fileStream != null)
                    {
                        this.fileStream.Close();
                    }
                    this.SetCompleteState();
                    if (this._UploadCompleted != null)
                    {
#if DEBUG
                        System.Diagnostics.Debug.WriteLine("上传文件完成！返回值：" + e.ResultData);
#endif
                        this._UploadCompleted(this, new UploadCompletedEventArgs(e.ResultData));
                    }
                }
            }
        }

        /// <summary>
        /// 取消上传文件。
        /// </summary>
        public void CancelUploadFile()
        {
            if (this.httpFileUpload != null)
            {
                this.httpFileUpload.CancelUpload();
            }
            if (this._UploadCanceled != null)
            {
                this._UploadCanceled(this, System.EventArgs.Empty);
            }
        }

        public UploadFile()
        {
            InitializeComponent();
        }

        private void UploadFile_Load(object sender, EventArgs e)
        {
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.CancelUploadFile();
        }

        /// <summary>
        /// 将视图设置为上传完成状态。
        /// </summary>
        public void SetCompleteState()
        {
            this.IsUploadComplete = true;
            this.btnCancel.Hide();

            string text2 = "上传完成";
            if (this.ShowFileName)
            {
                text2 += "，" + System.IO.Path.GetFileName(this.File);
            }

            this.textProgressBar1.Value = this.textProgressBar1.Maximum;
            this.textProgressBar1.Text = text2;
        }

    }

    /// <summary>
    /// 上传完成事件数据。
    /// </summary>
    public class UploadCompletedEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取需要返回到客户端的数据。
        /// </summary>
        public object ResultData
        {
            get;
            set;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="resultData">需要返回到客户端的数据。</param>
        public UploadCompletedEventArgs(object resultData)
        {
            this.ResultData = resultData;
        }
    }

}
