using System;

namespace Thinksea.WebControls.PageNavigate
{
	/// <summary>
	/// 此类用于处理分页事件。
	/// </summary>
	public class PageNumberEventArgs: System.EventArgs
	{
		/// <summary>
		/// 用于显示页码的控件。
		/// </summary>
		System.Web.UI.WebControls.HyperLink _PageNumberControl = null;
		/// <summary>
		/// 获取用于显示页码的控件。
		/// </summary>
		public System.Web.UI.WebControls.HyperLink PageNumberControl
		{
			get
			{
				return this._PageNumberControl;
			}
		}

		/// <summary>
		/// 正在处理的页码。
		/// </summary>
        private int _PageNumber = 0;
		/// <summary>
		/// 获取或设置正在处理的页码。
		/// </summary>
        public int PageNumber
		{
			get
			{
				return this._PageNumber;
			}
			set
			{
				this._PageNumber = value;
			}
		}

		/// <summary>
		/// 初始化此实例。
		/// </summary>
		public PageNumberEventArgs()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 初始化此实例。
		/// </summary>
		/// <param name="PageNumberControl">用于显示页码的控件。</param>
		/// <param name="PageNumber">页码。</param>
        public PageNumberEventArgs(System.Web.UI.WebControls.HyperLink PageNumberControl, int PageNumber)
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this._PageNumberControl = PageNumberControl;
			this.PageNumber = PageNumber;

		}

	}
}
