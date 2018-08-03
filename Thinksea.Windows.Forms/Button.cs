using System;
using System.Collections.Generic;
using System.Text;

namespace Thinksea.Windows.Forms
{
    /// <summary>
    /// 一个按钮控件。
    /// </summary>
    public class Button : System.Windows.Forms.Button
    {
        private eButtonDisplayState _DisplayState = eButtonDisplayState.Normal;
        /// <summary>
        /// 获取或设置按钮显示状态。
        /// </summary>
        private eButtonDisplayState DisplayState
        {
            get
            {
                return this._DisplayState;
            }
            set
            {
                this._DisplayState = value;
            }
        }

        private bool _ButtonState = false;
        /// <summary>
        /// 获取或设置按钮的按下状态。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Description("按钮的按下状态。"),
        System.ComponentModel.NotifyParentProperty(true)
        ]
        public bool ButtonState
        {
            get
            {
                return this._ButtonState;
            }
            set
            {
                if (this._ButtonState != value)
                {
                    this._ButtonState = value;
                    if (value)
                    {
                        this.DisplayState = eButtonDisplayState.Down;
                    }
                    else
                    {
                        this.DisplayState = eButtonDisplayState.Normal;
                    }
                    this.Refresh();
                }
            }
        }

        private eButtonType _ButtonType = eButtonType.Button;
        /// <summary>
        /// 获取或设置按钮类型。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Description("按钮类型。"),
        System.ComponentModel.NotifyParentProperty(true)
        ]
        public eButtonType ButtonType
        {
            get
            {
                return this._ButtonType;
            }
            set
            {
                this._ButtonType = value;
            }
        }

        private System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Button));

        private System.Drawing.Image _UpImage = null;
        /// <summary>
        /// 获取或设置鼠标抬起时显示的图片。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Description("鼠标抬起时显示的图片。"),
        System.ComponentModel.NotifyParentProperty(true)
        ]
        public System.Drawing.Image UpImage
        {
            get
            {
                if (_UpImage == null)
                {
                    return ((System.Drawing.Image)(resources.GetObject("button_up")));
                }
                return this._UpImage;
            }
            set
            {
                this._UpImage = value;
            }
        }

        private System.Drawing.Image _DownImage = null;
        /// <summary>
        /// 获取或设置按钮为按下状态时显示的图片。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Description("按钮为按下状态时显示的图片。"),
        System.ComponentModel.NotifyParentProperty(true)
        ]
        public System.Drawing.Image DownImage
        {
            get
            {
                if (this._DownImage == null)
                {
                    return ((System.Drawing.Image)(resources.GetObject("button_down")));
                    //return this.UpImage;
                }
                return this._DownImage;
            }
            set
            {
                this._DownImage = value;
            }
        }

        private System.Drawing.Image _MouseOverImage = null;
        /// <summary>
        /// 获取或设置鼠标进入控件区域时显示的图片。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Description("鼠标进入控件区域时显示的图片。"),
        System.ComponentModel.NotifyParentProperty(true)
        ]
        public System.Drawing.Image MouseOverImage
        {
            get
            {
                if (this._MouseOverImage == null)
                {
                    return ((System.Drawing.Image)(resources.GetObject("button_mouseover")));
                    //return this.UpImage;
                }
                return this._MouseOverImage;
            }
            set
            {
                this._MouseOverImage = value;
            }
        }

        private System.Drawing.Image _MouseDownImage = null;
        /// <summary>
        /// 获取或设置鼠标按下动作时显示的图片。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Description("鼠标按下动作时显示的图片。"),
        System.ComponentModel.NotifyParentProperty(true)
        ]
        public System.Drawing.Image MouseDownImage
        {
            get
            {
                if (this._MouseDownImage == null)
                {
                    return ((System.Drawing.Image)(resources.GetObject("button_mousedown")));
                    //return this.DownImage;
                }
                return this._MouseDownImage;
            }
            set
            {
                this._MouseDownImage = value;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public Button()
        {
        }

        /// <summary>
        /// 引发 Thinksea.Windows.Forms.Button.OnPaint(System.Windows.Forms.PaintEventArgs) 事件。
        /// </summary>
        /// <param name="pevent">包含事件数据的 System.Windows.Forms.PaintEventArgs。</param>
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs pevent)
        {
            if (this.ButtonType == eButtonType.Button)
            {
                base.OnPaint(pevent);
            }
            else
            {
                #region 显示按钮图片。
                switch (this.DisplayState)
                {
                    case eButtonDisplayState.Normal:
                        if (this.UpImage != null)
                        {
                            pevent.Graphics.DrawImage(this.UpImage, pevent.ClipRectangle);
                        }
                        break;
                    case eButtonDisplayState.MouseOver:
                        if (this.MouseOverImage != null)
                        {
                            pevent.Graphics.DrawImage(this.MouseOverImage, pevent.ClipRectangle);
                        }
                        break;
                    case eButtonDisplayState.MouseDown:
                        if (this.MouseDownImage != null)
                        {
                            pevent.Graphics.DrawImage(this.MouseDownImage, pevent.ClipRectangle);
                        }
                        break;
                    case eButtonDisplayState.Down:
                        if (this.DownImage != null)
                        {
                            pevent.Graphics.DrawImage(this.DownImage, pevent.ClipRectangle);
                        }
                        break;
                }
                #endregion

                #region 显示 Text 属性设置的文本内容。
                if (!string.IsNullOrEmpty(this.Text))
                {
                    //System.Windows.Forms.TextRenderer.MeasureText(pevent.Graphics, this.Text, this.Font, this.Size, System.Windows.Forms.TextFormatFlags.HorizontalCenter | System.Windows.Forms.TextFormatFlags.VerticalCenter);
                    System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(this.Enabled ? this.ForeColor : System.Drawing.SystemColors.GrayText);
                    System.Drawing.StringFormat fmt = new System.Drawing.StringFormat(System.Drawing.StringFormatFlags.NoWrap | System.Drawing.StringFormatFlags.MeasureTrailingSpaces);
                    fmt.LineAlignment = System.Drawing.StringAlignment.Center;
                    fmt.Alignment = System.Drawing.StringAlignment.Center;
                    pevent.Graphics.DrawString(this.Text, this.Font, brush, this.DisplayRectangle, fmt);
                }
                #endregion

                #region 画焦点虚线框。
                if (this.Focused)
                {
                    System.Drawing.Rectangle rec = this.DisplayRectangle;
                    System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Brushes.Gray, 1);
                    pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                    pevent.Graphics.DrawRectangle(pen, rec.X + 1, rec.Y + 1, rec.Width - 3, rec.Height - 3);
                }
                #endregion

            }
        }

        /// <summary>
        /// 引发 Thinksea.Windows.Forms.Button.OnMouseEnter(System.EventArgs) 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnMouseEnter(EventArgs e)
        {
            this.DisplayState = eButtonDisplayState.MouseOver;
            base.OnMouseEnter(e);
        }

        /// <summary>
        /// 引发 Thinksea.Windows.Forms.Button.OnMouseLeave(System.EventArgs) 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnMouseLeave(EventArgs e)
        {
            if (this.ButtonState)
            {
                this.DisplayState = eButtonDisplayState.Down;
            }
            else
            {
                this.DisplayState = eButtonDisplayState.Normal;
            }
            base.OnMouseLeave(e);
        }

        /// <summary>
        /// 引发 Thinksea.Windows.Forms.Button.OnMouseDown(System.Windows.Forms.MouseEventArgs) 事件。
        /// </summary>
        /// <param name="mevent">包含事件数据的 System.Windows.Forms.MouseEventArgs。</param>
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs mevent)
        {
            this.DisplayState = eButtonDisplayState.MouseDown;
            base.OnMouseDown(mevent);

        }

        /// <summary>
        /// 引发 Thinksea.Windows.Forms.Button.OnMouseUp(System.Windows.Forms.MouseEventArgs) 事件。
        /// </summary>
        /// <param name="mevent">包含事件数据的 System.Windows.Forms.MouseEventArgs。</param>
        protected override void OnMouseUp(System.Windows.Forms.MouseEventArgs mevent)
        {
            if (this.DisplayRectangle.Contains(mevent.X, mevent.Y))
            {
                if (this.ButtonType == eButtonType.KeepDownStateImageButton)
                {
                    this.ButtonState = !this.ButtonState;
                }
                else
                {
                    if (this.ButtonState != false)
                    {
                        this.ButtonState = false;
                    }
                }

            }

            base.OnMouseUp(mevent);
        }

        /// <summary>
        /// 引发 Thinksea.Windows.Forms.Button.OnKeyUp(System.Windows.Forms.KeyEventArgs) 事件。
        /// </summary>
        /// <param name="kevent">包含事件数据的 System.Windows.Forms.KeyEventArgs。</param>
        protected override void OnKeyUp(System.Windows.Forms.KeyEventArgs kevent)
        {
            if (kevent.KeyData == System.Windows.Forms.Keys.Enter || kevent.KeyData == System.Windows.Forms.Keys.Space)
            {
                if (this.ButtonType == eButtonType.KeepDownStateImageButton)
                {
                    this.ButtonState = !this.ButtonState;
                }
                else
                {
                    if (this.ButtonState != false)
                    {
                        this.ButtonState = false;
                    }
                }

            }
            base.OnKeyUp(kevent);
        }

    }

    /// <summary>
    /// 按钮状态。
    /// </summary>
    public enum eButtonDisplayState
    {
        /// <summary>
        /// 默认状态。
        /// </summary>
        Normal,
        /// <summary>
        /// 鼠标进入控件区域。
        /// </summary>
        MouseOver,
        /// <summary>
        /// 鼠标按下按键。
        /// </summary>
        MouseDown,
        /// <summary>
        /// 按钮控件为按下状态。
        /// </summary>
        Down,
    }

    /// <summary>
    /// 按钮类型。
    /// </summary>
    public enum eButtonType
    {
        /// <summary>
        /// 普通按钮。
        /// </summary>
        Button,
        /// <summary>
        /// 图片按钮。
        /// </summary>
        ImageButton,
        /// <summary>
        /// 可保持按下状态的图片按钮。
        /// </summary>
        KeepDownStateImageButton,
    }

}
