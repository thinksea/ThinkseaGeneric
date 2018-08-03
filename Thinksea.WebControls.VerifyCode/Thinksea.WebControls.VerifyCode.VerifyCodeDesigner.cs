using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.Design;
using System.Web.UI.HtmlControls;
using System.ComponentModel;
using System.IO;
using Thinksea.WebControls.VerifyCode;

namespace Thinksea.WebControls.VerifyCode
{
	/// <summary>
	/// 为控件的设计模式行为提供设计器支持。
	/// </summary>
	public class VerifyCodeDesigner : ControlDesigner {

//		private Thinksea.WebControls.VerifyCode.VerifyCode VerifyCode1;
		/// <summary>
		/// 初始化此实例。
		/// </summary>
		public VerifyCodeDesigner() { }


		/// <summary>
		/// 初始化设计器并加载指定的组件。
		/// </summary>
		/// <param name="component">正在设计的控件元素。</param>
		public override void Initialize(IComponent component)
		{
			//			this.VerifyCode1 = (Thinksea.WebControls.VerifyCode.VerifyCode)component;
			base.Initialize(component);

		}

		/// <summary>
		/// 获取设计时用于表示控件的 HTML。
		/// </summary>
		/// <returns></returns>
		public override string GetDesignTimeHtml()
		{
			StringWriter sw = new StringWriter();
			HtmlTextWriter htw = new HtmlTextWriter(sw);
			//			this.VerifyCode1.RenderControl( htw );
			htw.Write("<STRONG>VerifyCode Control</STRONG>");
			return sw.ToString();

		}
	}

}
