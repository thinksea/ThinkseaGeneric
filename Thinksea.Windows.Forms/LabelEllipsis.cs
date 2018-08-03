using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Thinksea.Windows.Forms
{
    /// <summary>
    /// 一个允许使用省略号替代剪辑文本的文本显示控件。
    /// </summary>
	public class LabelEllipsis : Label
	{
        /// <summary>
        /// 
        /// </summary>
        [
        Browsable(false),
        ReadOnly(true),
        EditorBrowsable(EditorBrowsableState.Never),
        System.ComponentModel.DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden),
        ]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

		private string longText;
		private string shortText;

        /// <summary>
        /// 引发 System.Windows.Forms.Control.Resize 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
            this.FullText = FullText;
		}

		#region AutoEllipsis property

        /// <summary>
        /// 获取或设置文本。
        /// </summary>
        [DefaultValue(null)]
        [Description("在控件上显示的文本")]
        public virtual string FullText
		{
            get
            {
                return longText;
            }
            set
            {
                longText = value;
                shortText = Ellipsis.Compact(longText, this, AutoEllipsis);

                tooltip.SetToolTip(this, longText);
                base.Text = shortText;
            }
        }

		private EllipsisFormat _ellipsis;

        /// <summary>
        /// 是否使用省略模式剪辑文本。
        /// </summary>
		[Category("Behavior")]
		[Description("是否使用省略模式剪辑文本。")]
		public new EllipsisFormat AutoEllipsis
		{
			get { return _ellipsis; }
			set
			{
				if (_ellipsis != value)
				{
					_ellipsis = value;
					// ellipsis type changed, recalculate ellipsis text
					this.FullText = FullText;
					OnAutoEllipsisChanged(EventArgs.Empty);
				}
			}
		}

        /// <summary>
        /// 当更改属性“AutoEllipsis”之后引发此事件。
        /// </summary>
        [Category("Property Changed")]
        [Description("当更改属性“AutoEllipsis”之后引发此事件。")]
		public event EventHandler AutoEllipsisChanged;

        /// <summary>
        /// 引发 AutoEllipsisChanged 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected void OnAutoEllipsisChanged(EventArgs e)
		{
			if (AutoEllipsisChanged != null)
			{
				AutoEllipsisChanged(this, e);
			}
		}

		#endregion

		#region Tooltip

		ToolTip tooltip = new ToolTip();

		#endregion
	}
}
