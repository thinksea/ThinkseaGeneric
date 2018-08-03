namespace Thinksea.Windows.Forms
{
    /// <summary>
    ///双缓冲 ListView ，解决添加项时的闪烁问题。
    /// </summary>
    public class DoubleBufferListView : System.Windows.Forms.ListView
    {
        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public DoubleBufferListView()
        {
            // 开启双缓冲
            this.SetStyle(System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);

            // Enable the OnNotifyMessage event so we get a chance to filter out 
            // Windows messages before they get to the form's WndProc
            this.SetStyle(System.Windows.Forms.ControlStyles.EnableNotifyMessage, true);
        }

        /// <summary>
        /// 向控件通知 Windows 消息。
        /// </summary>
        /// <param name="m">一个 System.Windows.Forms.Message，它表示 Windows 消息。</param>
        protected override void OnNotifyMessage(System.Windows.Forms.Message m)
        {
            //Filter out the WM_ERASEBKGND message
            if (m.Msg != 0x14)
            {
                base.OnNotifyMessage(m);
            }

        }

        //public DoubleBufferListView()
        //{
        //    SetStyle(System.Windows.Forms.ControlStyles.DoubleBuffer | System.Windows.Forms.ControlStyles.OptimizedDoubleBuffer | System.Windows.Forms.ControlStyles.AllPaintingInWmPaint, true);
        //    UpdateStyles();
        //}

    }
}
