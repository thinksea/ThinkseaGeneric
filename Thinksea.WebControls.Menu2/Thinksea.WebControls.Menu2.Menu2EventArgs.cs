using System;

namespace Thinksea.WebControls.Menu2
{
	/// <summary>
	/// 此类用于处理菜单项事件。
	/// </summary>
	public class ItemEventArgs: System.EventArgs
	{
		/// <summary>
		/// 用于显示菜单项的控件。
		/// </summary>
		System.Web.UI.WebControls.HyperLink _ItemControl = null;
		/// <summary>
		/// 获取用于显示菜单项的控件。
		/// </summary>
		public System.Web.UI.WebControls.HyperLink ItemControl
		{
			get
			{
				return this._ItemControl;
			}
		}

		/// <summary>
		/// 正在处理的菜单项。
		/// </summary>
		private Thinksea.WebControls.Menu2.MenuItem _Item = null;
		/// <summary>
		/// 正在处理的菜单项。
		/// </summary>
		public Thinksea.WebControls.Menu2.MenuItem Item
		{
			get
			{
				return this._Item;
			}
			set
			{
				this._Item = value;
			}
		}

		/// <summary>
		/// 初始化此实例。
		/// </summary>
		public ItemEventArgs()
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
		}

		/// <summary>
		/// 初始化此实例。
		/// </summary>
		/// <param name="ItemControl">用于显示菜单项的控件。</param>
		/// <param name="Item">正在处理的菜单项。</param>
		public ItemEventArgs( System.Web.UI.WebControls.HyperLink ItemControl, Thinksea.WebControls.Menu2.MenuItem Item )
		{
			//
			// TODO: 在此处添加构造函数逻辑
			//
			this._ItemControl = ItemControl;
			this._Item = Item;

		}

	}
}
