using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Net.FileUploader_WinFormDemo
{
    public class TextProgressBar : System.Windows.Forms.ProgressBar
    {
        [System.ComponentModel.Browsable(true), System.ComponentModel.Category("Appearance"), System.ComponentModel.Description("最大尺寸")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                if (base.Text != value)
                {
                    base.Text = value;
                    this.Invalidate();
                }
            }
        }

        protected void DrawText()
        {
            StringFormat fmt = new StringFormat();
            fmt.Alignment = StringAlignment.Center;
            fmt.LineAlignment = StringAlignment.Center;
            fmt.Trimming |= StringTrimming.EllipsisPath;
            SizeF size = TextRenderer.MeasureText(this.Text, Font);
            using (var g = this.CreateGraphics())
            {
                g.DrawString(this.Text, this.Font, System.Drawing.Brushes.Black, this.ClientRectangle, fmt);
            }
        }

        private const int WM_PAINT = 0xf;

        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            base.WndProc(ref m);

            switch (m.Msg)
            {
                case WM_PAINT:
                    DrawText();
                    break;
            }
        }
    }
}
