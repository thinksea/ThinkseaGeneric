using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Diagnostics;

namespace Thinksea.WebControls.VerifyCode
{
    /// <summary>
    /// 验证码控件。
    /// </summary>
    /// <remarks>
    /// <note>要求应用本控件的网络页面必须启动 Session 会话。</note>
    /// </remarks>
    /// <example>
    /// <para lang="C#">
    /// 两种使用方法：
    /// <br/>1、对于“用户登录页”和“用户登录信息输入验证页”为同一页面的情况，请使用方法“IsVerify”进行验证。示例代码如下：
    /// </para>
    /// <code lang="C#">
    /// if( ! this.VerifyCode1.IsVerify(this.VerifyCode.Text) )
    /// {
    ///		this.Response.Write("您输入的验证码错误");
    ///	}
    ///	</code>
    /// <para lang="C#">
    ///	2、对于“用户登录页”和“用户登录信息输入验证页”为不同页面的情况，请使用静态方法“IsVerify”进行验证。示例代码如下：
    /// </para>
    ///	<code lang="C#">
    ///	if( ! Thinksea.WebControls.VerifyCode.VerifyCode.IsVerify(this.VerifyCode.Text, "VerifyCode1") )
    ///	{
    ///		this.Response.Write("您输入的验证码错误");
    ///	}
    ///	</code>
    /// </example>
    [ToolboxData("<{0}:VerifyCode runat=server></{0}:VerifyCode>"),
    DefaultProperty("Length"),
        //	System.ComponentModel.DefaultEvent("PageSelectedCommand"),
        //	ValidationPropertyAttribute("Text"),
    Designer(typeof(Thinksea.WebControls.VerifyCode.VerifyCodeDesigner))
    ]
    public class VerifyCode : System.Web.UI.Control, INamingContainer//, IPostBackEventHandler//, IStateManager
    {
        private bool _ChangeOnClick = true;
        /// <summary>
        /// 获取或设置一个值，用于指示当在图片上点击鼠标左键时是否更换一个新的验证码。
        /// </summary>
        /// <remarks>
        /// 这为用户提供方便，因为人眼可能难于识别生成的一些验证码。
        /// </remarks>
        [
        System.ComponentModel.DefaultValue(true),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("获取或设置一个值，用于指示当在图片上点击鼠标左键时是否更换一个新的验证码。"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool ChangeOnClick
        {
            get
            {
                return this._ChangeOnClick;
            }
            set
            {
                this._ChangeOnClick = value;
            }
        }

        private string _ImageTitle = "点击更换验证码";
        /// <summary>
        /// 获取或设置当鼠标停止在验证码图片上时显示的文本提示信息。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("点击更换验证码"),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("获取或设置当鼠标停止在验证码图片上时显示的文本提示信息。"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public string ImageTitle
        {
            get
            {
                return this._ImageTitle;
            }
            set
            {
                this._ImageTitle = value;
            }
        }

        /// <summary>
        /// 获取验证码的密码字符串。
        /// </summary>
        /// <returns>验证码的密文形式。</returns>
        public string GetVerifyCode()
        {
            return Thinksea.WebControls.VerifyCode.VerifyCodeHandler.GetVerifyCode(this.ClientID);
        }

        /// <summary>
        /// 验证指定的验证码是否与此实例所表示的验证码相同。
        /// </summary>
        /// <param name="VerifyCode">用户输入的验证码。</param>
        /// <returns>如果输入正确返回 true，否则返回 false。</returns>
        public bool IsVerify(string VerifyCode)
        {
            return Thinksea.WebControls.VerifyCode.VerifyCodeHandler.IsVerify(VerifyCode, this.ClientID);
        }

        /// <summary>
        /// 验证指定的验证码是否与指定验证码控件所表示的验证码相同。
        /// </summary>
        /// <param name="VerifyCode">用户输入的验证码。</param>
        /// <param name="VerifyCodeControlID">验证码控件 ID。</param>
        /// <returns>如果输入正确返回 true，否则返回 false。</returns>
        public static bool IsVerify(string VerifyCode, string VerifyCodeControlID)
        {
            return Thinksea.WebControls.VerifyCode.VerifyCodeHandler.IsVerify(VerifyCode, VerifyCodeControlID);
        }

        /// <summary>
        /// 初始化此实例。
        /// </summary>
        public VerifyCode()
        {

        }

        /// <summary>
        /// 引发 System.Web.UI.Control.PreRender 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs 对象。</param>
        protected override void OnPreRender(EventArgs e)
        {
            if (this.ChangeOnClick)
            {
                if (!this.Page.ClientScript.IsClientScriptBlockRegistered("Thinksea.WebControls.VerifyCode"))
                {
                    this.Page.ClientScript.RegisterClientScriptBlock(this.GetType()
                        , "Thinksea.WebControls.VerifyCode"
                        , @"
function VerifyCode_ChangeVerifyCode(imageID)
{
    var Name=""rt"";
    var Value=Math.random()
    var uri=document.getElementById(imageID).src;
    uri=uri.replace(/(\s|\?)*$/g, """");
    if(uri.indexOf(""?"")==-1)
    {
        uri = uri + ""?"" + Name + ""="" + encodeURIComponent( Value );
    }
	else
	{
	    var reg=new RegExp(""(\\?|&)"" + Name.replace(/\$/gi, ""\\$"") + ""=([^&]*)"",""gi"");
		if( reg.test(uri) )
		{
		    uri = uri.replace(reg, ""$1"" + Name.replace(/\$/gi, ""$$$$"") + ""="" + encodeURIComponent( Value ));
		}
		else
		{
			uri = uri + ""&"" + Name + ""="" + encodeURIComponent( Value );
		}
	}
    document.getElementById(imageID).src=uri;
}
", true);
                }
            }

        }

        /// <summary>
        /// 将服务器控件内容发送到提供的 <see cref="System.Web.UI.HtmlTextWriter"/> 对象，此对象编写将在客户端呈现的内容。
        /// </summary>
        /// <param name="writer">接收服务器控件内容的 <see cref="System.Web.UI.HtmlTextWriter"/> 对象。</param>
        protected override void Render(HtmlTextWriter writer)
        {
            string VerifyCodeURL = Thinksea.Web.SetUriParameter("VerifyCode.ashx", "VerifyCodeID", this.ClientID);
            VerifyCodeURL = Thinksea.Web.SetUriParameter(VerifyCodeURL, "rt", System.DateTime.Now.Ticks.ToString());

            System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
            image.ID = "Image";
            image.BorderWidth = 0;
            image.ImageUrl = VerifyCodeURL;

            if (this.ChangeOnClick)
            {
                this.Controls.Add(image);

                writer.WriteBeginTag("a");
                writer.WriteAttribute("id", this.ClientID);
                writer.WriteAttribute("href", "javascript:void(0);");
                if (!string.IsNullOrEmpty(this.ImageTitle))
                {
                    writer.WriteAttribute("title", this.ImageTitle, true);
                }
                writer.WriteAttribute("onclick", "VerifyCode_ChangeVerifyCode('" + image.ClientID + "'); return false;");
                writer.Write(HtmlTextWriter.TagRightChar);

                this.RenderChildren(writer);

                writer.WriteEndTag("a");
            }
            else
            {
                image.ID = this.ClientID;
                image.RenderControl(writer);
            }
            //base.Render(writer);
        }

    }
}
