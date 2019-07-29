namespace Thinksea.VerifyCode_WebFormDemo
{
    /// <summary>
    /// 生成一个验证码图片。
    /// </summary>
    public class VerifyCode : Thinksea.VerifyCode, System.Web.IHttpHandler, System.Web.SessionState.IRequiresSessionState
    {
        /// <summary>
        /// 获取验证码配置信息从配置文件。
        /// </summary>
        /// <param name="key">配置节名称。</param>
        /// <returns>配置节信息集合。</returns>
        private static System.Collections.Specialized.NameValueCollection GetSection(string key)
        {
            return (System.Collections.Specialized.NameValueCollection)System.Web.Configuration.WebConfigurationManager.GetSection(key);
        }

        /// <summary>
        /// 获取指定验证码控件持有的验证码。
        /// </summary>
        /// <param name="verifyCodeId">验证码控件 ID。</param>
        /// <returns>验证码的密文形式，找不到则返回空字符串“”。</returns>
        public static string GetVerifyCode(string verifyCodeId)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            object savedVerifyCode = context.Session["VerifyCode" + verifyCodeId];
            if (savedVerifyCode != null)
            {
                return System.Convert.ToString(savedVerifyCode);
            }
            return "";

        }

        /// <summary>
        /// 存储验证码。
        /// </summary>
        /// <param name="verifyCodeId">验证码 ID。</param>
        /// <param name="verifyCode">验证码内容。</param>
        private static void SaveVerifyCode(string verifyCodeId, string verifyCode)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            context.Session["VerifyCode" + verifyCodeId] = verifyCode;
        }

        /// <summary>
        /// 销毁验证码。
        /// </summary>
        /// <param name="verifyCodeId">验证码 ID。</param>
        private static void DestructionVerifyCode(string verifyCodeId)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            context.Session.Remove("VerifyCode" + verifyCodeId);
        }

        /// <summary>
        /// 一个静态构造方法。
        /// </summary>
        static VerifyCode()
        {
            var configSection = GetSection("VerifyCode");
            if (configSection != null)
            {
                if (!string.IsNullOrEmpty(configSection["VerifyCodeEnumerable"]))
                {
                    System.Collections.Generic.List<string> charVerifyCodeEnumerable = new System.Collections.Generic.List<string>(); //字符验证码枚举列表。
                    System.Collections.Generic.SortedList<string, string> keyValueVerifyCodeEnumerable = new System.Collections.Generic.SortedList<string, string>(); //键值对（问题和答案）验证码列表。
                    string[] keyValues = configSection["VerifyCodeEnumerable"].Split(',');
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
                    VerifyCode.VerifyCodeEnumerable = charVerifyCodeEnumerable.ToArray();
                    VerifyCode.KeyValueVerifyCodeEnumerable = keyValueVerifyCodeEnumerable;
                }

                if (!string.IsNullOrEmpty(configSection["Length"]))
                {
                    VerifyCode.Length = System.Convert.ToInt32(configSection["Length"]);
                }

                if (!string.IsNullOrEmpty(configSection["BendingAngle"]))
                {
                    VerifyCode.BendingAngle = System.Convert.ToInt32(configSection["BendingAngle"]);
                }

                if (!string.IsNullOrEmpty(configSection["FontSize"]))
                {
                    VerifyCode.FontSize = System.Convert.ToInt32(configSection["FontSize"]);
                }

                if (!string.IsNullOrEmpty(configSection["Padding"]))
                {
                    VerifyCode.Padding = System.Convert.ToInt32(configSection["Padding"]);
                }

                if (!string.IsNullOrEmpty(configSection["ForeColors"]))
                {
                    System.Collections.Generic.List<System.Drawing.Color> cs = new System.Collections.Generic.List<System.Drawing.Color>();
                    string[] vs = configSection["ForeColors"].Split(',');
                    foreach (var tmp in vs)
                    {
                        cs.Add(System.Drawing.ColorTranslator.FromHtml(tmp));
                    }
                    VerifyCode.ForeColors = cs.ToArray();
                }

                if (!string.IsNullOrEmpty(configSection["Fonts"]))
                {
                    string[] vs = configSection["Fonts"].Split(',');
                    VerifyCode.Fonts = vs;
                }
            }

        }

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
        public void ProcessRequest(System.Web.HttpContext context)
        {
            //在此写入您的处理程序实现。
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

            //string generateVerifyCode = VerifyCode.GenerateVerifyCodeString();
            string generateVerifyCodeQuestion, generateVerifyCodeAnswer;
            VerifyCode.GenerateVerifyCode(out generateVerifyCodeQuestion, out generateVerifyCodeAnswer);
            string _VerifyCode = generateVerifyCodeAnswer.ToLower(); //用于存储验证码的密码字符串。
            //if (this.IsTrackingViewState)
            {
                SaveVerifyCode(VerifyCodeID, _VerifyCode);
            }

            //context.Response.ContentType = "text/plain";
            context.Response.ContentType = "image/png";
            //context.Response.ContentType = "application/octet-stream";

            //context.Response.Write("Hello World");
            System.Drawing.Bitmap image = Thinksea.VerifyCode.GenerateVerifyCodeImage(generateVerifyCodeQuestion);
            image.Save(context.Response.OutputStream, System.Drawing.Imaging.ImageFormat.Png);
            image.Dispose();
        }

        #endregion

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
                    var configSection = GetSection("VerifyCode");
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
        /// <param name="verifyCode">用户输入的验证码。</param>
        /// <param name="verifyCodeId">验证码控件 ID。</param>
        /// <returns>如果输入正确返回 true，否则返回 false。</returns>
        public static bool IsVerify(string verifyCode, string verifyCodeId)
        {
            System.Web.HttpContext context = System.Web.HttpContext.Current;
            string savedVerifyCode = GetVerifyCode(verifyCodeId);
            if (savedVerifyCode != null)
            {
                DestructionVerifyCode(verifyCodeId);
                if ((string)savedVerifyCode == verifyCode.ToLower())
                {
                    return true;
                }
            }
            if (DebugMode)
            {
                bool r = true;
                foreach (var ch in verifyCode)
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