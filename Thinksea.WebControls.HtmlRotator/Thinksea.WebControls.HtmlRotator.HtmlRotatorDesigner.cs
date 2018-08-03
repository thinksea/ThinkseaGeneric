using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.IO;

namespace Thinksea.WebControls.HtmlRotator {
	/// <summary>
	/// HtmlRotator 控件设计时支持类。
	/// </summary>
	public class HtmlRotatorDesigner : System.Web.UI.Design.ControlDesigner {

		/// <summary>
		/// 一个构造方法。
		/// </summary>
		public HtmlRotatorDesigner() {}


		private Thinksea.WebControls.HtmlRotator.HtmlRotator htmlRotator = null;

		/// <summary>
		/// 初始化设计器并加载指定的组件。
		/// </summary>
		/// <param name="component">正在设计的控件元素。</param>
		public override void Initialize(IComponent component)
		{
			this.htmlRotator = (Thinksea.WebControls.HtmlRotator.HtmlRotator)component;
			base.Initialize(component);

		}

		/// <summary>
		/// 获取设计时用于表示控件的 HTML。
		/// </summary>
		/// <returns>设计时用于表示控件的 HTML。</returns>
		public override string GetDesignTimeHtml()
		{
			StringWriter sw = new StringWriter();
			HtmlTextWriter htw = new HtmlTextWriter(sw);

			this.htmlRotator.Style.Add("OVERFLOW", "hidden");
			this.htmlRotator.RenderBeginTag( htw );

			if( this.htmlRotator.Htmls.Count > 0 )
			{
				htw.WriteLine(this.htmlRotator.Htmls[0]);
			}

			this.htmlRotator.RenderEndTag( htw );
			return sw.ToString();

		}
	}

}
