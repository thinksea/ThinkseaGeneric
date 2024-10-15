using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.IO;
using Thinksea.WebControls.Menu2;

namespace Thinksea.WebControls.Menu2 {
	/// <summary>
	/// 为控件提供设计模式行为服务。
	/// </summary>
	public class Menu2Designer : ControlDesigner {
		/// <summary>
		/// 初始化此实例。
		/// </summary>
		public Menu2Designer() {}


		private Thinksea.WebControls.Menu2.Menu2 menu2 = null;

		/// <summary>
		/// 初始化设计器并加载指定的组件。
		/// </summary>
		/// <param name="component">正在设计的控件元素。</param>
		public override void Initialize(IComponent component)
		{
			this.menu2 = (Thinksea.WebControls.Menu2.Menu2)component;
			base.Initialize(component);

		}

		/// <summary>
		/// 获取一个值，该值指示指定的路径字符串是包含绝对路径信息还是包含相对路径信息。
		/// </summary>
		/// <param name="Path">要测试的路径。</param>
		/// <returns>包含绝对路径信息返回 true；否则返回 false。</returns>
		private bool IsPathRooted( string Path )
		{
			if( System.IO.Path.IsPathRooted(Path) ) return true;
			string [] sta = new string[]{
											"http://"
											, "https://"
											, "ftp://"
										};
			string tmpPath = Path.ToLowerInvariant();
			foreach( string tmp in sta )
			{
				if( tmpPath.StartsWith( tmp ) ) return true;
			}
			return false;

		}

		/// <summary>
		/// 获取设计时用于表示控件的 HTML。
		/// </summary>
		/// <returns></returns>
		public override string GetDesignTimeHtml()
		{
			StringWriter sw = new StringWriter();
			HtmlTextWriter htw = new HtmlTextWriter(sw);

            string menu2_MenuXML = this.menu2.MenuXML;
            if (this.IsPathRooted(menu2_MenuXML) == false)
            {
                menu2_MenuXML = System.IO.Path.Combine(new Thinksea.VisualStudio.IDE().GetActiveDocumentDirectory(), menu2_MenuXML);
            }
            if (System.IO.File.Exists(menu2_MenuXML))
            {
                this.menu2.RenderControl(htw, menu2_MenuXML);
            }
            else
            {
                htw.Write("设计时：" + this.menu2.MenuXML);
            }

			return sw.ToString();

		}
	}

}
