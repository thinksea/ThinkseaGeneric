using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Thinksea.Windows.Forms.IPAddress.Properties;

namespace Thinksea.Windows.Forms.IPAddress
{
    internal class FieldControl : TextBox
    {
        // Fields
        private int _fieldId = -1;
        private bool _invalidKeyDown;
        private byte _rangeLower;
        private byte _rangeUpper = 0xff;
        public const byte MaximumValue = 0xff;
        public const byte MinimumValue = 0;

        // Events
        public event EventHandler<CedeFocusEventArgs> CedeFocusEvent;

        public event EventHandler<SpecialKeyEventArgs> SpecialKeyEvent;

        public event EventHandler<TextChangedEventArgs> TextChangedEvent;

        // Methods
        public FieldControl()
        {
            base.BorderStyle = BorderStyle.None;
            this.MaxLength = 3;
            base.Size = this.MinimumSize;
            base.TabStop = false;
            base.TextAlign = HorizontalAlignment.Center;
        }

        private void HandleBackKey(KeyEventArgs e)
        {
            if ((this.TextLength == 0) || ((base.SelectionStart == 0) && (this.SelectionLength == 0)))
            {
                this.SendSpecialKeyEvent(Keys.Back);
                e.SuppressKeyPress = true;
            }
            else
            {
                int selectionStart;
                if (this.SelectionLength > 0)
                {
                    selectionStart = base.SelectionStart;
                    this.Text = this.Text.Remove(base.SelectionStart, this.SelectionLength);
                    base.SelectionStart = selectionStart;
                    e.SuppressKeyPress = true;
                }
                else if (base.SelectionStart > 0)
                {
                    selectionStart = --base.SelectionStart;
                    this.Text = this.Text.Remove(base.SelectionStart, 1);
                    base.SelectionStart = selectionStart;
                    e.SuppressKeyPress = true;
                }
            }
        }

        public void HandleSpecialKey(Keys keyCode)
        {
            if (keyCode == Keys.Back)
            {
                base.Focus();
                if (this.TextLength > 0)
                {
                    int length = this.TextLength - 1;
                    this.Text = this.Text.Substring(0, length);
                }
                base.SelectionStart = this.TextLength;
            }
        }

        private bool IsCedeFocusKey(Keys keyCode)
        {
            return ((((keyCode == Keys.OemPeriod) || (keyCode == Keys.Decimal)) || (keyCode == Keys.Space)) && (((this.TextLength != 0) && (this.SelectionLength == 0)) && (base.SelectionStart != 0)));
        }

        private static bool NumericKeyDown(KeyEventArgs e)
        {
            if (((e.KeyCode < Keys.NumPad0) || (e.KeyCode > Keys.NumPad9)) && ((e.KeyCode < Keys.D0) || (e.KeyCode > Keys.D9)))
            {
                return false;
            }
            return true;
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if ((e.KeyCode == Keys.Home) || (e.KeyCode == Keys.End))
            {
                this.SendSpecialKeyEvent(e.KeyCode);
            }
            else
            {
                this._invalidKeyDown = false;
                if (!NumericKeyDown(e) && !ValidKeyDown(e))
                {
                    this._invalidKeyDown = true;
                }
                if (this.IsCedeFocusKey(e.KeyCode))
                {
                    this.SendCedeFocusEvent(Direction.Forward, Selection.All);
                }
                if ((e.KeyCode == Keys.Left) || (e.KeyCode == Keys.Up))
                {
                    if (e.Modifiers == Keys.Control)
                    {
                        this.SendCedeFocusEvent(Direction.Reverse, Selection.All);
                    }
                    else if ((this.SelectionLength == 0) && (base.SelectionStart == 0))
                    {
                        this.SendCedeFocusEvent(Direction.Reverse, Selection.None);
                    }
                }
                if (e.KeyCode == Keys.Back)
                {
                    this.HandleBackKey(e);
                }
                if ((e.KeyCode == Keys.Delete) && ((base.SelectionStart < this.TextLength) && (this.TextLength > 0)))
                {
                    int selectionStart = base.SelectionStart;
                    this.Text = this.Text.Remove(base.SelectionStart, (this.SelectionLength > 0) ? this.SelectionLength : 1);
                    base.SelectionStart = selectionStart;
                    e.SuppressKeyPress = true;
                }
                if ((e.KeyCode == Keys.Right) || (e.KeyCode == Keys.Down))
                {
                    if (e.Modifiers == Keys.Control)
                    {
                        this.SendCedeFocusEvent(Direction.Forward, Selection.All);
                    }
                    else if ((this.SelectionLength == 0) && (base.SelectionStart == this.Text.Length))
                    {
                        this.SendCedeFocusEvent(Direction.Forward, Selection.None);
                    }
                }
            }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (this._invalidKeyDown)
            {
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        protected override void OnParentBackColorChanged(EventArgs e)
        {
            base.OnParentBackColorChanged(e);
            this.BackColor = base.Parent.BackColor;
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

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (!this.Blank)
            {
                int num;
                if (!int.TryParse(this.Text, out num))
                {
                    this.Text = string.Empty;
                }
                else if (num > this.RangeUpper)
                {
                    this.Text = this.RangeUpper.ToString(CultureInfo.InvariantCulture);
                }
                else
                {
                    this.Text = num.ToString(CultureInfo.InvariantCulture);
                }
            }
            if (null != this.TextChangedEvent)
            {
                TextChangedEventArgs args = new TextChangedEventArgs();
                args.FieldId = this.FieldId;
                args.Text = this.Text;
                this.TextChangedEvent(this, args);
            }
            if ((this.Text.Length == this.MaxLength) && this.Focused)
            {
                this.SendCedeFocusEvent(Direction.Forward, Selection.All);
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            base.OnValidating(e);
            if (!this.Blank && (this.Value < this.RangeLower))
            {
                this.Text = this.RangeLower.ToString(CultureInfo.InvariantCulture);
            }
        }

        private void SendCedeFocusEvent(Direction direction, Selection selection)
        {
            if (null != this.CedeFocusEvent)
            {
                CedeFocusEventArgs e = new CedeFocusEventArgs();
                e.FieldId = this.FieldId;
                e.Direction = direction;
                e.Selection = selection;
                this.CedeFocusEvent(this, e);
            }
        }

        private void SendSpecialKeyEvent(Keys keyCode)
        {
            if (null != this.SpecialKeyEvent)
            {
                SpecialKeyEventArgs e = new SpecialKeyEventArgs();
                e.FieldId = this.FieldId;
                e.KeyCode = keyCode;
                this.SpecialKeyEvent(this, e);
            }
        }

        public void TakeFocus(Direction direction, Selection selection)
        {
            base.Focus();
            if (selection == Selection.All)
            {
                base.SelectionStart = 0;
                this.SelectionLength = this.TextLength;
            }
            else if (direction == Direction.Forward)
            {
                base.SelectionStart = 0;
            }
            else
            {
                base.SelectionStart = this.TextLength;
            }
        }

        public override string ToString()
        {
            return this.Value.ToString(CultureInfo.InvariantCulture);
        }

        private static bool ValidKeyDown(KeyEventArgs e)
        {
            return (((e.KeyCode == Keys.Back) || (e.KeyCode == Keys.Delete)) || ((e.Modifiers == Keys.Control) && (((e.KeyCode == Keys.C) || (e.KeyCode == Keys.V)) || (e.KeyCode == Keys.X))));
        }

        /// <summary>
        /// 获取一个值，该值指示文本内容是否为空。
        /// </summary>
        public bool Blank
        {
            get
            {
                return (this.Text.Length == 0);
            }
        }

		[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int FieldId
        {
            get
            {
                return this._fieldId;
            }
            set
            {
                this._fieldId = value;
            }
        }

        public override Size MinimumSize
        {
            get
            {
                return TextRenderer.MeasureText(Resources.FieldMeasureText, this.Font);
            }
        }

		[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public byte RangeLower
        {
            get
            {
                return this._rangeLower;
            }
            set
            {
                if (value < 0)
                {
                    this._rangeLower = 0;
                }
                else if (value > this._rangeUpper)
                {
                    this._rangeLower = this._rangeUpper;
                }
                else
                {
                    this._rangeLower = value;
                }
                if (this.Value < this._rangeLower)
                {
                    this.Text = this._rangeLower.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

		[System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public byte RangeUpper
        {
            get
            {
                return this._rangeUpper;
            }
            set
            {
                if (value < this._rangeLower)
                {
                    this._rangeUpper = this._rangeLower;
                }
                else if (value > 0xff)
                {
                    this._rangeUpper = 0xff;
                }
                else
                {
                    this._rangeUpper = value;
                }
                if (this.Value > this._rangeUpper)
                {
                    this.Text = this._rangeUpper.ToString(CultureInfo.InvariantCulture);
                }
            }
        }

        public byte Value
        {
            get
            {
                byte rangeLower;
                if (!byte.TryParse(this.Text, out rangeLower))
                {
                    rangeLower = this.RangeLower;
                }
                return rangeLower;
            }
        }
    }

}
