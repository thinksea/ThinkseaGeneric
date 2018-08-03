using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Thinksea.Windows.Forms
{
    /// <summary>
    /// 一个允许使用省略号替代剪辑文本的文本输入控件。
    /// </summary>
	public class TextBoxEllipsis : TextBox
	{
        /// <summary>
        /// 
        /// </summary>
        [
        Browsable(false),
        ReadOnly(true),
        EditorBrowsable( EditorBrowsableState.Never),
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

			if (!Focused) // doesn't apply if textbox has the focus
			{
				this.FullText = FullText;
			}
		}

        /// <summary>
        /// 引发 System.Windows.Forms.Control.GotFocus 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
		protected override void OnGotFocus(EventArgs e)
		{
			base.Text = FullText;
			base.OnGotFocus(e);
		}

        /// <summary>
        /// 引发 System.Windows.Forms.Control.LostFocus 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.FullText = base.Text;
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
                base.Text = Focused ? longText : shortText;
            }
        }

		private EllipsisFormat _ellipsis;

        /// <summary>
        /// 是否使用省略模式剪辑文本。
        /// </summary>
		[Category("Behavior")]
        [Description("是否使用省略模式剪辑文本。")]
		public virtual EllipsisFormat AutoEllipsis
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

		#region P/Invoke for context menu

        //// credit is due to zsh
        //// http://www.codeproject.com/KB/edit/cmenuedit.aspx?msg=2774005#xx2774005xx

        //const uint MSGF_MENU = 2;
        //const uint OBJID_CLIENT = 0xFFFFFFFC;

        //const uint MF_SEPARATOR = 0x800;
        //const uint MF_BYCOMMAND = 0;
        //const uint MF_POPUP = 16;
        //const uint MF_UNCHECKED = 0;
        //const uint MF_CHECKED = 8;

        //const int WM_ENTERIDLE = 0x121;
        //const int WM_APP = 0x8000;

        //// user-defined windows messages
        //const int WM_NONE = WM_APP + 1;
        //const int WM_LEFT = WM_APP + 2;
        //const int WM_RIGHT = WM_APP + 3;
        //const int WM_CENTER = WM_APP + 4;
        //const int WM_PATH = WM_APP + 5;
        //const int WM_WORD = WM_APP + 6;

        //[StructLayout(LayoutKind.Sequential)]
        //struct RECT
        //{
        //    int Left;
        //    int Top;
        //    int Right;
        //    int Bottom;
        //}

        //[StructLayout(LayoutKind.Sequential)]
        //struct MENUBARINFO
        //{
        //    public int cbSize;
        //    public RECT rcBar;
        //    public IntPtr hMenu;
        //    public IntPtr hwndMenu;
        //    public ushort fBarFocused_fFocused;
        //}

        //[DllImport("user32.dll")]
        //static extern bool GetMenuBarInfo(IntPtr hwnd, uint idObject, uint idItem, ref MENUBARINFO pmbi);

        //[DllImport("user32.dll")]
        //static extern int GetMenuState(IntPtr hMenu, uint uId, uint uFlags);

        //[DllImport("user32.dll")]
        //static extern uint AppendMenu(IntPtr hMenu, uint uFlags, uint uIDNewItem, string lpNewItem);

        //[DllImport("user32.dll")]
        //static extern IntPtr CreatePopupMenu();

        //protected override void WndProc(ref Message m)
        //{
        //    switch (m.Msg)
        //    {
        //        case WM_ENTERIDLE:
        //            base.WndProc(ref m);

        //            if (MSGF_MENU == (int)m.WParam)
        //            {
        //                MENUBARINFO mbi = new MENUBARINFO();
        //                mbi.cbSize = Marshal.SizeOf(mbi);

        //                GetMenuBarInfo(m.LParam, OBJID_CLIENT, 0, ref mbi);

        //                if (GetMenuState(mbi.hMenu, WM_APP + 1, MF_BYCOMMAND) == -1)
        //                {
        //                    IntPtr hSubMenu = CreatePopupMenu();

        //                    if (hSubMenu != IntPtr.Zero)
        //                    {
        //                        AppendMenu(hSubMenu, isChecked(EllipsisFormat.None), WM_NONE, "None");
        //                        AppendMenu(hSubMenu, isChecked(EllipsisFormat.Start), WM_LEFT, "Left");
        //                        AppendMenu(hSubMenu, isChecked(EllipsisFormat.End), WM_RIGHT, "Right");
        //                        AppendMenu(hSubMenu, isChecked(EllipsisFormat.Middle), WM_CENTER, "Center");
        //                        AppendMenu(hSubMenu, MF_SEPARATOR, 0, null);
        //                        AppendMenu(hSubMenu, isChecked(EllipsisFormat.Path), WM_PATH, "Path Ellipsis");
        //                        AppendMenu(hSubMenu, isChecked(EllipsisFormat.Word), WM_WORD, "Word Ellipsis");

        //                        AppendMenu(mbi.hMenu, MF_SEPARATOR, 0, null);
        //                        AppendMenu(mbi.hMenu, MF_POPUP, (uint)hSubMenu, "Auto Ellipsis");
        //                    }
        //                }
        //            }
        //            break;

        //        case WM_NONE:
        //            AutoEllipsis = EllipsisFormat.None;
        //            break;

        //        case WM_LEFT:
        //            AutoEllipsis = AutoEllipsis & ~EllipsisFormat.Middle | EllipsisFormat.Start;
        //            break;

        //        case WM_RIGHT:
        //            AutoEllipsis = AutoEllipsis & ~EllipsisFormat.Middle | EllipsisFormat.End;
        //            break;

        //        case WM_CENTER:
        //            AutoEllipsis |= EllipsisFormat.Middle;
        //            break;

        //        case WM_PATH:
        //            if ((AutoEllipsis & EllipsisFormat.Path) == 0)
        //            {
        //                AutoEllipsis |= EllipsisFormat.Path;
        //            }
        //            else
        //            {
        //                AutoEllipsis &= ~EllipsisFormat.Path;
        //            }
        //            break;

        //        case WM_WORD:
        //            if ((AutoEllipsis & EllipsisFormat.Word) == 0)
        //            {
        //                AutoEllipsis |= EllipsisFormat.Word;
        //            }
        //            else
        //            {
        //                AutoEllipsis &= ~EllipsisFormat.Word;
        //            }
        //            break;

        //        default:
        //            base.WndProc(ref m);
        //            break;
        //    }
        //}

        //uint isChecked(EllipsisFormat fmt)
        //{
        //    EllipsisFormat mask = fmt;

        //    switch (fmt)
        //    {
        //        case EllipsisFormat.None:
        //        case EllipsisFormat.Start:
        //        case EllipsisFormat.End:
        //            mask = EllipsisFormat.Middle;
        //            break;
        //    }
        //    return ((AutoEllipsis & mask) == fmt) ? MF_CHECKED : MF_UNCHECKED;
        //}
	
		#endregion

		#region Tooltip

		ToolTip tooltip = new ToolTip();

		#endregion
	}
}
