using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.Diagnostics;

namespace Thinksea.Windows.Forms.IPAddress
{
    /// <summary>
    /// IP 地址用户输入控件。
    /// </summary>
    [Designer(typeof(IPAddressDesigner))]
    public partial class IPAddress : UserControl
    {
        // Fields
        private bool _backColorChanged;
        private BorderStyle _borderStyle = BorderStyle.Fixed3D;
        private DotControl[] _dotControls = new DotControl[3];
        private FieldControl[] _fieldControls = new FieldControl[4];
        private bool _readOnly;
        private TextBox _referenceTextBox = new TextBox();
        private IContainer components = null;
        private Size Fixed3DOffset = new Size(3, 3);
        private Size FixedSingleOffset = new Size(2, 2);
        public const int NumberOfFields = 4;

        /// <summary>
        /// 字段已更改事件。
        /// </summary>
        public event EventHandler<FieldChangedEventArgs> FieldChangedEvent;

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public IPAddress()
        {
            this.BackColor = SystemColors.Window;
            this.ResetBackColorChanged();
            for (int i = 0; i < this._fieldControls.Length; i++)
            {
                this._fieldControls[i] = new FieldControl();
                this._fieldControls[i].CedeFocusEvent += new EventHandler<CedeFocusEventArgs>(this.OnCedeFocus);
                this._fieldControls[i].FieldId = i;
                this._fieldControls[i].Name = "FieldControl" + i.ToString(CultureInfo.InvariantCulture);
                this._fieldControls[i].Parent = this;
                this._fieldControls[i].SpecialKeyEvent += new EventHandler<SpecialKeyEventArgs>(this.OnSpecialKey);
                this._fieldControls[i].TextChangedEvent += new EventHandler<TextChangedEventArgs>(this.OnFieldTextChanged);
                base.Controls.Add(this._fieldControls[i]);
                if (i < 3)
                {
                    this._dotControls[i] = new DotControl();
                    this._dotControls[i].Name = "DotControl" + i.ToString(CultureInfo.InvariantCulture);
                    this._dotControls[i].Parent = this;
                    base.Controls.Add(this._dotControls[i]);
                }
            }
            this.InitializeComponent();
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            base.SetStyle(ControlStyles.ContainerControl, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.Selectable, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            this._referenceTextBox.AutoSize = true;
            base.Size = this.MinimumSize;
            this.AutoSize = true;
            base.DragEnter += new DragEventHandler(this.IPAddressControl_DragEnter);
            base.DragDrop += new DragEventHandler(this.IPAddressControl_DragDrop);
        }

        private void AdjustSize()
        {
            Size minimumSize = this.MinimumSize;
            if (base.Size.Width > minimumSize.Width)
            {
                minimumSize.Width = base.Size.Width;
            }
            if (base.Size.Height > minimumSize.Height)
            {
                minimumSize.Height = base.Size.Height;
            }
            if (this.AutoSize)
            {
                base.Size = new Size(this.MinimumSize.Width, this.MinimumSize.Height);
            }
            else
            {
                base.Size = minimumSize;
            }
            this.LayoutControls();
        }

        private Size CalculateMinimumSize()
        {
            Size size = new Size(0, 0);
            foreach (FieldControl control in this._fieldControls)
            {
                size.Width += control.Size.Width;
                size.Height = Math.Max(size.Height, control.Size.Height);
            }
            foreach (DotControl control2 in this._dotControls)
            {
                size.Width += control2.Size.Width;
                size.Height = Math.Max(size.Height, control2.Size.Height);
            }
            switch (this.BorderStyle)
            {
                case BorderStyle.FixedSingle:
                    size.Width += 4;
                    size.Height += this.GetSuggestedHeight() - size.Height;
                    return size;

                case BorderStyle.Fixed3D:
                    size.Width += 6;
                    size.Height += this.GetSuggestedHeight() - size.Height;
                    return size;
            }
            return size;
        }

        /// <summary>
        /// 清除控件中的所有文本。
        /// </summary>
        public void Clear()
        {
            foreach (FieldControl control in this._fieldControls)
            {
                control.Clear();
            }
        }

        /// <summary>
        /// 释放此控件占用的系统资源。
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        /// <summary>
        /// 获取 IP 地址字节数组。
        /// </summary>
        /// <returns></returns>
        public byte[] GetAddressBytes()
        {
            byte[] buffer = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                buffer[i] = this._fieldControls[i].Value;
            }
            return buffer;
        }

        private int GetSuggestedHeight()
        {
            this._referenceTextBox.BorderStyle = this.BorderStyle;
            this._referenceTextBox.Font = this.Font;
            return this._referenceTextBox.Height;
        }

        private static NativeMethods.TEXTMETRIC GetTextMetrics(IntPtr hwnd, Font font)
        {
            NativeMethods.TEXTMETRIC textmetric;
            IntPtr windowDC = NativeMethods.GetWindowDC(hwnd);
            IntPtr hgdiobj = font.ToHfont();
            try
            {
                IntPtr ptr3 = NativeMethods.SelectObject(windowDC, hgdiobj);
                NativeMethods.GetTextMetrics(windowDC, out textmetric);
                NativeMethods.SelectObject(windowDC, ptr3);
            }
            finally
            {
                NativeMethods.ReleaseDC(hwnd, windowDC);
                NativeMethods.DeleteObject(hgdiobj);
            }
            return textmetric;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
            base.AutoScaleMode = AutoScaleMode.Font;
        }

        private void IPAddressControl_DragDrop(object sender, DragEventArgs e)
        {
            this.Text = e.Data.GetData(DataFormats.Text).ToString();
        }

        private void IPAddressControl_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Text))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }
        }

        private void LayoutControls()
        {
            base.SuspendLayout();
            int num = base.Size.Width - this.MinimumSize.Width;
            Debug.Assert(num >= 0);
            int num2 = (this._fieldControls.Length + this._dotControls.Length) + 1;
            int num3 = num / num2;
            int num4 = num % num2;
            int[] numArray = new int[num2];
            for (int i = 0; i < num2; i++)
            {
                numArray[i] = num3;
                if (i < num4)
                {
                    numArray[i]++;
                }
            }
            int x = 0;
            int y = 0;
            switch (this.BorderStyle)
            {
                case BorderStyle.FixedSingle:
                    x = this.FixedSingleOffset.Width;
                    y = this.FixedSingleOffset.Height;
                    break;

                case BorderStyle.Fixed3D:
                    x = this.Fixed3DOffset.Width;
                    y = this.Fixed3DOffset.Height;
                    break;
            }
            int num8 = 0;
            x += numArray[num8++];
            for (int j = 0; j < this._fieldControls.Length; j++)
            {
                this._fieldControls[j].Location = new Point(x, y);
                x += this._fieldControls[j].Size.Width;
                if (j < this._dotControls.Length)
                {
                    x += numArray[num8++];
                    this._dotControls[j].Location = new Point(x, y);
                    x += this._dotControls[j].Size.Width;
                    x += numArray[num8++];
                }
            }
            base.ResumeLayout(false);
        }

        /// <summary>
        /// 引发背景色已经更改事件。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
            this._backColorChanged = true;
        }

        private void OnCedeFocus(object sender, CedeFocusEventArgs e)
        {
            if (((e.Direction != Direction.Reverse) || (e.FieldId != 0)) && ((e.Direction != Direction.Forward) || (e.FieldId != 3)))
            {
                int fieldId = e.FieldId;
                if (e.Direction == Direction.Forward)
                {
                    fieldId++;
                }
                else
                {
                    fieldId--;
                }
                this._fieldControls[fieldId].TakeFocus(e.Direction, e.Selection);
            }
        }

        private void OnFieldTextChanged(object sender, TextChangedEventArgs e)
        {
            if (null != this.FieldChangedEvent)
            {
                FieldChangedEventArgs args = new FieldChangedEventArgs();
                args.FieldId = e.FieldId;
                args.Text = e.Text;
                this.FieldChangedEvent(this, args);
            }
            this.OnTextChanged(EventArgs.Empty);
        }

        /// <summary>
        /// 引发字体已经更改事件。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            this.AdjustSize();
        }

        /// <summary>
        /// 引发 System.Windows.Forms.Control.GotFocus 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this._fieldControls[0].TakeFocus(Direction.Forward, Selection.All);
        }

        /// <summary>
        /// 引发鼠标进入事件。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            this.Cursor = Cursors.IBeam;
        }

        /// <summary>
        /// 引发 System.Window.Forms.Control.Paint 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.PaintEventArgs。</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color backColor = this.BackColor;
            if (!this._backColorChanged && !(base.Enabled && !this.ReadOnly))
            {
                backColor = SystemColors.Control;
            }
            e.Graphics.FillRectangle(new SolidBrush(backColor), base.ClientRectangle);
            Rectangle bounds = new Rectangle(base.ClientRectangle.Left, base.ClientRectangle.Top, base.ClientRectangle.Width - 1, base.ClientRectangle.Height - 1);
            switch (this.BorderStyle)
            {
                case BorderStyle.FixedSingle:
                    ControlPaint.DrawBorder(e.Graphics, base.ClientRectangle, SystemColors.WindowFrame, ButtonBorderStyle.Solid);
                    break;

                case BorderStyle.Fixed3D:
                    if (!Application.RenderWithVisualStyles)
                    {
                        ControlPaint.DrawBorder3D(e.Graphics, base.ClientRectangle, Border3DStyle.Sunken);
                        break;
                    }
                    ControlPaint.DrawVisualStyleBorder(e.Graphics, bounds);
                    break;
            }
        }

        /// <summary>
        /// 引发尺寸更改事件。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.AdjustSize();
        }

        private void OnSpecialKey(object sender, SpecialKeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.End:
                    this._fieldControls[3].TakeFocus(Direction.Reverse, Selection.None);
                    break;

                case Keys.Home:
                    this._fieldControls[0].TakeFocus(Direction.Forward, Selection.None);
                    break;

                case Keys.Back:
                    if (e.FieldId > 0)
                    {
                        this._fieldControls[e.FieldId - 1].HandleSpecialKey(Keys.Back);
                    }
                    break;
            }
        }

        private void Parse(string text)
        {
            this.Clear();
            if (null != text)
            {
                int startIndex = 0;
                int index = 0;
                index = 0;
                while (index < this._dotControls.Length)
                {
                    int num3 = text.IndexOf(this._dotControls[index].Text, startIndex);
                    if (num3 >= 0)
                    {
                        this._fieldControls[index].Text = text.Substring(startIndex, num3 - startIndex);
                        startIndex = num3 + this._dotControls[index].Text.Length;
                    }
                    else
                    {
                        break;
                    }
                    index++;
                }
                this._fieldControls[index].Text = text.Substring(startIndex);
            }
        }

        private void ResetBackColorChanged()
        {
            this._backColorChanged = false;
        }

        /// <summary>
        /// 使用指定的字节数组中的数据填充此控件文本。
        /// </summary>
        /// <param name="bytes"></param>
        public void SetAddressBytes(byte[] bytes)
        {
            this.Clear();
            int num = Math.Min(4, bytes.Length);
            for (int i = 0; i < num; i++)
            {
                this._fieldControls[i].Text = bytes[i].ToString(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// 将插入光标设置到指定的 IP 地址段输入区。
        /// </summary>
        /// <param name="fieldId"></param>
        public void SetFieldFocus(int fieldId)
        {
            if ((fieldId >= 0) && (fieldId < 4))
            {
                this._fieldControls[fieldId].TakeFocus(Direction.Forward, Selection.All);
            }
        }

        /// <summary>
        /// 设置 IP 地址段有效输入范围。
        /// </summary>
        /// <param name="fieldId">IP 地址段编号。</param>
        /// <param name="rangeLower">下限值。</param>
        /// <param name="rangeUpper">上限值。</param>
        public void SetFieldRange(int fieldId, byte rangeLower, byte rangeUpper)
        {
            if ((fieldId >= 0) && (fieldId < 4))
            {
                this._fieldControls[fieldId].RangeLower = rangeLower;
                this._fieldControls[fieldId].RangeUpper = rangeUpper;
            }
        }

        /// <summary>
        /// 获取此控件的 IP 地址的文本形式。
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < 4; i++)
            {
                builder.Append(this._fieldControls[i].ToString());
                if (i < this._dotControls.Length)
                {
                    builder.Append(this._dotControls[i].ToString());
                }
            }
            return builder.ToString();
        }

        /// <summary>
        /// 获取或设置一个值，用于指示是否自动调整控件大小。
        /// </summary>
        [DefaultValue(true)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
                if (this.AutoSize)
                {
                    this.AdjustSize();
                }
            }
        }

        public int Baseline
        {
            get
            {
                int num = GetTextMetrics(base.Handle, this.Font).tmAscent + 1;
                switch (this.BorderStyle)
                {
                    case BorderStyle.FixedSingle:
                        return (num + this.FixedSingleOffset.Height);

                    case BorderStyle.Fixed3D:
                        return (num + this.Fixed3DOffset.Height);
                }
                return num;
            }
        }

        /// <summary>
        /// 获取一个值，该值指示此 IP 地址控件的值是否为空。
        /// </summary>
        public bool Blank
        {
            get
            {
                foreach (FieldControl control in this._fieldControls)
                {
                    if (!control.Blank)
                    {
                        return false;
                    }
                }
                return true;
            }
        }

        /// <summary>
        /// 获取或设置边框样式。
        /// </summary>
        [DefaultValue(2)]
        public new BorderStyle BorderStyle
        {
            get
            {
                return this._borderStyle;
            }
            set
            {
                this._borderStyle = value;
                this.AdjustSize();
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取一个值，该值指示控件是否有输入焦点。
        /// </summary>
        public override bool Focused
        {
            get
            {
                foreach (FieldControl control in this._fieldControls)
                {
                    if (control.Focused)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public override Size MinimumSize
        {
            get
            {
                return this.CalculateMinimumSize();
            }
        }

        /// <summary>
        /// 获取或设置一个值，指示 IP 地址编辑控件是否处于禁止修改状态。
        /// </summary>
        [Description("指示 IP 地址编辑控件是否处于禁止修改状态。"), DefaultValue(false)]
        public bool ReadOnly
        {
            get
            {
                return this._readOnly;
            }
            set
            {
                this._readOnly = value;
                foreach (FieldControl control in this._fieldControls)
                {
                    control.ReadOnly = this._readOnly;
                }
                foreach (DotControl control2 in this._dotControls)
                {
                    control2.ReadOnly = this._readOnly;
                }
                base.Invalidate();
            }
        }

        /// <summary>
        /// 获取或设置一个 IP 地址。
        /// </summary>
        [Browsable(true)
        , Description("获取或设置一个 IP 地址。")]
        public override string Text
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < this._fieldControls.Length; i++)
                {
                    builder.Append(this._fieldControls[i].Text);
                    if (i < this._dotControls.Length)
                    {
                        builder.Append(this._dotControls[i].Text);
                    }
                }
                return builder.ToString();
            }
            set
            {
                this.Parse(value);
            }
        }

    }
}
