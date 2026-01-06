using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Thinksea.Windows.Forms.IPAddress.Properties;

namespace Thinksea.Windows.Forms.IPAddress
{
    internal class DotControl : Control
    {
        // Fields
        private bool _backColorChanged;
        private bool _readOnly;

        // Methods
        public DotControl()
        {
            base.SetStyle(ControlStyles.ResizeRedraw, true);
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.BackColor = SystemColors.Window;
            base.Size = this.MinimumSize;
            base.TabStop = false;
            this.Text = Resources.FieldSeparator;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);
            base.Size = this.MinimumSize;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Color backColor = this.BackColor;
            if (!this._backColorChanged && !(base.Enabled && !this.ReadOnly))
            {
                backColor = SystemColors.Control;
            }
            Color foreColor = this.ForeColor;
            if (!base.Enabled)
            {
                foreColor = SystemColors.GrayText;
            }
            else if (this.ReadOnly && !this._backColorChanged)
            {
                foreColor = SystemColors.WindowText;
            }
            e.Graphics.FillRectangle(new SolidBrush(backColor), base.ClientRectangle);
            StringFormat format = new StringFormat();
            format.Alignment = StringAlignment.Center;
            e.Graphics.DrawString(this.Text, this.Font, new SolidBrush(foreColor), base.ClientRectangle, format);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            this.BackColor = base.Parent.BackColor;
            this._backColorChanged = true;
        }

        protected override void OnParentForeColorChanged(EventArgs e)
        {
            base.OnParentForeColorChanged(e);
            this.ForeColor = base.Parent.ForeColor;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            base.Size = this.MinimumSize;
        }

        public override string ToString()
        {
            return this.Text;
        }

        // Properties
        public override Size MinimumSize
        {
            get
            {
                int count = 10;
                string text = new string(Resources.FieldSeparator[0], count);
                Size size = TextRenderer.MeasureText(text, this.Font);
                size.Width /= count;
                return size;
            }
        }

		[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public bool ReadOnly
        {
            get
            {
                return this._readOnly;
            }
            set
            {
                this._readOnly = value;
                base.Invalidate();
            }
        }
    }

}
