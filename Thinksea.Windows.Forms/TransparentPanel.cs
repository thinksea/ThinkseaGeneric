using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.Forms
{
    /// <summary>
    /// 支持背景透明度设置的 Panel 控件。
    /// </summary>
    public class TransparentPanel : Panel
    {
        /// <summary>
        /// 指定了用这个风格创建的窗口是透明的。这意味着，在这个窗口下面的任何窗口都不会被这个窗口挡住。用这个风格创建的窗口只有当它下面的窗口都更新过以后才接收WM_PAINT消息。 
        /// </summary>
		private const int WS_EX_TRANSPARENT = 0x00000020;
        private int _Opacity = 125;

        #region Property
        /// <summary>
        /// 背景的透明度。有效值0-255
        /// </summary>
        [
        DefaultValue(125),
        Category("Appearance"),
        Bindable(true),
        Description("背景的透明度. 有效值0-255"),
        ]
        public int Opacity
        {
            get { return this._Opacity; }
            set
            {
                if (value > 255) value = 255;
                else if (value < 0) value = 0;
                this._Opacity = value;
                this.Invalidate();
            }
        }
        #endregion

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public TransparentPanel()
        {

        }

        /// <summary>
        /// 当重绘控件背景时调用此方法。
        /// </summary>
        /// <param name="e">用于重绘控件的参数。</param>
        protected override void OnPaintBackground(PaintEventArgs e)
        {
            //do not allow the background to be painted
        }

        /// <summary>
        /// 设置控件创建时的样式
        /// </summary>
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= WS_EX_TRANSPARENT; //WS_EX_TRANSPARENT //窗口透明度。
                return cp;
            }
        }

        /// <summary>
        /// 当需要重绘控件时调用此方法。
        /// </summary>
        /// <param name="e">用于重绘控件的参数。</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            if (this._Opacity > 0)
            {
                e.Graphics.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.FromArgb(this._Opacity, this.BackColor)), this.ClientRectangle);
            }
            //if (this._borderWidth > 0)
            //{
            //    Pen pen = new Pen(this._borderColor, this._borderWidth);
            //    pen.DashStyle = this.BorderStyle;
            //    e.Graphics.DrawRectangle(pen, e.ClipRectangle.Left, e.ClipRectangle.Top, this.Width - 1, this.Height - 1);
            //    pen.Dispose();
            //}
        }

    }
}
