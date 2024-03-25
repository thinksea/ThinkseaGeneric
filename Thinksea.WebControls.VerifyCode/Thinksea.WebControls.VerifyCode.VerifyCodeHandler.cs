﻿using System.Web;

namespace Thinksea.WebControls.VerifyCode
{
	/// <summary>
	/// 生成一个验证码图片。
	/// </summary>
	/// <remarks>
	/// 您将需要在您网站的 web.config 文件中配置此处理程序，
	/// 并向 IIS 注册此处理程序，然后才能进行使用。有关详细信息，
	/// 请参见下面的链接: http://go.microsoft.com/?linkid=8101007
	/// </remarks>
	public class VerifyCodeHandler : Thinksea.VerifyCode, IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        /// <summary>
        /// 一个静态构造方法。
        /// </summary>
        static VerifyCodeHandler()
        {
            System.Collections.Specialized.NameValueCollection configSection = (System.Collections.Specialized.NameValueCollection)System.Web.Configuration.WebConfigurationManager.GetSection("VerifyCode");
            if (configSection != null)
            {
                if (!string.IsNullOrEmpty(configSection["VerifyCodeEnumerable"]))
                {
                    System.Collections.Generic.List<string> charVerifyCodeEnumerable = new System.Collections.Generic.List<string>(); //字符验证码枚举列表。
                    System.Collections.Generic.SortedList<string, string> keyValueVerifyCodeEnumerable = new System.Collections.Generic.SortedList<string, string>(); //键值对（问题和答案）验证码列表。
                    string [] keyValues = configSection["VerifyCodeEnumerable"].Split(',');
                    foreach (var tmp in keyValues)
                    {
                        int eqIndex = tmp.IndexOf('=');
                        if (eqIndex > 0) //如果是键值对验证码。
                        {
                            string key = tmp.Substring(0, eqIndex);
                            string value = tmp.Substring(eqIndex + 1);
                            if (!keyValueVerifyCodeEnumerable.ContainsKey(key))
                            {
                                keyValueVerifyCodeEnumerable.Add(key, value);
                            }
                        }
                        else if (!charVerifyCodeEnumerable.Contains(tmp)) //如果是字符型验证码。
                        {
                            charVerifyCodeEnumerable.Add(tmp);
                        }
                    }
                    VerifyCodeHandler.VerifyCodeEnumerable = charVerifyCodeEnumerable.ToArray();
                    VerifyCodeHandler.KeyValueVerifyCodeEnumerable = keyValueVerifyCodeEnumerable;
                }

                if (!string.IsNullOrEmpty(configSection["Length"]))
                {
                    VerifyCodeHandler.Length = System.Convert.ToInt32(configSection["Length"]);
                }

                if (!string.IsNullOrEmpty(configSection["BendingAngle"]))
                {
                    VerifyCodeHandler.BendingAngle = System.Convert.ToInt32(configSection["BendingAngle"]);
                }

                if (!string.IsNullOrEmpty(configSection["FontSize"]))
                {
                    VerifyCodeHandler.FontSize = System.Convert.ToInt32(configSection["FontSize"]);
                }

                if (!string.IsNullOrEmpty(configSection["Padding"]))
                {
                    VerifyCodeHandler.Padding = System.Convert.ToInt32(configSection["Padding"]);
                }

                if (!string.IsNullOrEmpty(configSection["ForeColors"]))
                {
                    System.Collections.Generic.List<System.Drawing.Color> cs = new System.Collections.Generic.List<System.Drawing.Color>();
                    string[] vs = configSection["ForeColors"].Split(',');
                    foreach (var tmp in vs)
                    {
                        cs.Add(System.Drawing.ColorTranslator.FromHtml(tmp));
                    }
                    VerifyCodeHandler.ForeColors = cs.ToArray();
                }

                if (!string.IsNullOrEmpty(configSection["Fonts"]))
                {
                    string[] vs = configSection["Fonts"].Split(',');
                    VerifyCodeHandler.Fonts = vs;
                }
            }

        }

		HttpContext context = null;
		#region IHttpHandler Members

		/// <summary>
		/// 获取一个值，该值指示其他请求是否可以使用 System.Web.IHttpHandler 实例。
		/// </summary>
		/// <value>如果 System.Web.IHttpHandler 实例可再次使用，则为 true；否则为 false。</value>
		public bool IsReusable
		{
			// 如果无法为其他请求重用托管处理程序，则返回 false。
			// 如果按请求保留某些状态信息，则通常这将为 false。
			get { return true; }
		}

		/// <summary>
		/// 通过实现 System.Web.IHttpHandler 接口的自定义 HttpHandler 启用 HTTP Web 请求的处理。
		/// </summary>
		/// <param name="context">System.Web.HttpContext 对象，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session 和 Server）的引用。</param>
		public void ProcessRequest(HttpContext context)
		{
			//在此写入您的处理程序实现。
			this.context = context;
            string VerifyCodeID = context.Request["VerifyCodeID"];
//            if (string.IsNullOrEmpty(VerifyCodeID))
//            {
//                //context.Response.ContentType = "text/plain";
//                context.Response.ContentType = "text/html";
//                context.Response.Write(@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body>
//" + Thinksea.Web.TextToHtml(@"功能：生成一个验证码图片。
//参数列表：
//VerifyCodeID：验证码对应的唯一 ID。（*必选参数）
//") + @"
//</body></html>");
//                return;
//            }

            //string generateVerifyCode = VerifyCodeHandler.GenerateVerifyCodeString();
            string generateVerifyCodeQuestion, generateVerifyCodeAnswer;
            VerifyCodeHandler.GenerateVerifyCode(out generateVerifyCodeQuestion, out generateVerifyCodeAnswer);
            string _VerifyCode = ""; //用于存储验证码的密码字符串。
            _VerifyCode = Thinksea.WebControls.VerifyCode.VerifyCodeHandler.EncryptPassword(generateVerifyCodeAnswer.ToLower());
			//if (this.IsTrackingViewState)
			{
                context.Session["VerifyCode" + VerifyCodeID] = _VerifyCode;
			}

			//context.Response.ContentType = "text/plain";
            //context.Response.ContentType = "image/Jpeg";
            context.Response.ContentType = "application/octet-stream";

			//context.Response.Write("Hello World");
            System.Drawing.Bitmap image = GenerateVerifyCodeImage(generateVerifyCodeQuestion);
            image.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg);
			image.Dispose();
		}

		#endregion

        /// <summary>
        /// 加密密码字符串。
        /// </summary>
        /// <param name="Password"></param>
        /// <returns></returns>
        internal static string EncryptPassword(string Password)
        {
            return Password;
            //return System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile( Password + "Thinksea_VerifyCode", "sha1" );
        }

        /// <summary>
        /// 获取指定验证码控件持有的验证码。
        /// </summary>
        /// <param name="VerifyCodeControlID">验证码控件 ID。</param>
        /// <returns>验证码的密文形式，找不到则返回空字符串“”。</returns>
        public static string GetVerifyCode(string VerifyCodeControlID)
        {
            string sName = "VerifyCode" + VerifyCodeControlID;
            object savedVerifyCode = System.Web.HttpContext.Current.Session[sName];
            if (savedVerifyCode != null)
            {
                return System.Convert.ToString(savedVerifyCode);
            }
            return "";

        }

        private static bool? _DebugMode = null;
        /// <summary>
        /// 获取一个值，指示是否使用调试模式。
        /// </summary>
        /// <remarks>
        /// 主要是为了自动化测试工具开启通用验证码。
        /// </remarks>
        private static bool DebugMode
        {
            get
            {
                if (_DebugMode == null)
                {
                    _DebugMode = false;
                    System.Collections.Specialized.NameValueCollection configSection = (System.Collections.Specialized.NameValueCollection)System.Web.Configuration.WebConfigurationManager.GetSection("VerifyCode");
                    if (configSection != null)
                    {
                        string debugMode = configSection["DebugMode"];
                        if (!string.IsNullOrEmpty(debugMode))
                        {
                            _DebugMode = System.Convert.ToBoolean(debugMode);
                        }
                    }
                }
                return _DebugMode.Value;
            }
        }

        /// <summary>
        /// 验证指定的验证码是否与指定验证码控件所表示的验证码相同。
        /// </summary>
        /// <param name="VerifyCode">用户输入的验证码。</param>
        /// <param name="VerifyCodeControlID">验证码控件 ID。</param>
        /// <returns>如果输入正确返回 true，否则返回 false。</returns>
        public static bool IsVerify(string VerifyCode, string VerifyCodeControlID)
        {
            string sName = "VerifyCode" + VerifyCodeControlID;
            object savedVerifyCode = System.Web.HttpContext.Current.Session[sName];
            if (savedVerifyCode != null)
            {
                System.Web.HttpContext.Current.Session.Remove(sName);
                if ((string)savedVerifyCode == Thinksea.WebControls.VerifyCode.VerifyCodeHandler.EncryptPassword(VerifyCode.ToLower()))
                {
                    return true;
                }
            }
            if (DebugMode)
            {
                bool r = true;
                foreach (var ch in VerifyCode)
                {
                    if (ch != '0')
                    {
                        r = false;
                        break;
                    }
                }
                return r;
            }
            return false;

        }

	}
}
