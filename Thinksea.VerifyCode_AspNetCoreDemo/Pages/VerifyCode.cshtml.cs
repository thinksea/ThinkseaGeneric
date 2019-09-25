using Microsoft.AspNetCore.Http;

namespace Thinksea.VerifyCode_AspNetCoreDemo.Pages
{
    /// <summary>
    /// 生成一个验证码图片。
    /// </summary>
    public class VerifyCodeModel : Microsoft.AspNetCore.Mvc.RazorPages.PageModel
    {
        /// <summary>
        /// 获取验证码配置信息从配置文件。
        /// </summary>
        /// <param name="key">配置节名称。</param>
        /// <returns>配置节信息集合。</returns>
        private static Microsoft.Extensions.Configuration.IConfigurationSection GetSection(string key)
        {
            return Thinksea.VerifyCode_AspNetCoreDemo.Startup.Configuration.GetSection(key);
        }

        /// <summary>
        /// 获取指定验证码控件持有的验证码。
        /// </summary>
        /// <param name="context">上下文对象。</param>
        /// <param name="verifyCodeId">验证码控件 ID。</param>
        /// <returns>验证码的密文形式，找不到则返回空字符串“”。</returns>
        public static string GetVerifyCode(Microsoft.AspNetCore.Http.HttpContext context, string verifyCodeId)
        {
            //object savedVerifyCode = Microsoft.AspNetCore.Http.SessionExtensions.GetString(context.Session, VerifyCodeControlID);
            string savedVerifyCode = context.Session.GetString(verifyCodeId);
            if (savedVerifyCode != null)
            {
                return savedVerifyCode;
            }
            return "";
        }

        /// <summary>
        /// 存储验证码。
        /// </summary>
        /// <param name="context">上下文对象。</param>
        /// <param name="verifyCodeId">验证码 ID。</param>
        /// <param name="verifyCode">验证码内容。</param>
        private static void SaveVerifyCode(Microsoft.AspNetCore.Http.HttpContext context, string verifyCodeId, string verifyCode)
        {
            //Microsoft.AspNetCore.Http.SessionExtensions.SetString(context.Session, VerifyCodeID, _VerifyCode);
            context.Session.SetString(verifyCodeId, verifyCode);
        }

        /// <summary>
        /// 销毁验证码。
        /// </summary>
        /// <param name="context">上下文对象。</param>
        /// <param name="verifyCodeId">验证码 ID。</param>
        private static void DestructionVerifyCode(Microsoft.AspNetCore.Http.HttpContext context, string verifyCodeId)
        {
            context.Session.Remove(verifyCodeId);
        }

        /// <summary>
        /// 一个静态构造方法。
        /// </summary>
        static VerifyCodeModel()
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

        /// <summary>
        /// 通过实现 System.Web.IHttpHandler 接口的自定义 HttpHandler 启用 HTTP Web 请求的处理。
        /// </summary>
        /// <param name="context">Microsoft.AspNetCore.Http.HttpContext 对象，它提供对用于为 HTTP 请求提供服务的内部服务器对象（如 Request、Response、Session 和 Server）的引用。</param>
        public async void ProcessRequest(Microsoft.AspNetCore.Http.HttpContext context)
        {
            var request = context.Request;
            var response = context.Response;

            //在此写入您的处理程序实现。
            string VerifyCodeID = request.Query["VerifyCodeID"];
            //            if (string.IsNullOrEmpty(VerifyCodeID))
            //            {
            //                //response.ContentType = "text/plain";
            //                response.ContentType = "text/html";
            //                response.Write(@"<html><head><meta http-equiv=""Content-Type"" content=""text/html; charset=utf-8"" /></head><body>
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
                SaveVerifyCode(context, VerifyCodeID, _VerifyCode);
            }

            //response.ContentType = "text/plain";
            response.ContentType = "image/png";
            //response.ContentType = "application/octet-stream";

            //response.Write("Hello World");
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                using (System.Drawing.Bitmap image = Thinksea.VerifyCode.GenerateVerifyCodeImage(generateVerifyCodeQuestion))
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    //image.Dispose();
                }
                ms.Seek(0, System.IO.SeekOrigin.Begin);
                await ms.CopyToAsync(response.Body);
            }
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
        public static bool IsVerify(Microsoft.AspNetCore.Http.HttpContext context, string verifyCode, string verifyCodeId)
        {
            string savedVerifyCode = GetVerifyCode(context, verifyCodeId);
            if (savedVerifyCode != null)
            {
                DestructionVerifyCode(context, verifyCodeId);
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

        public void OnGet()
        {
            this.ProcessRequest(this.HttpContext);
        }
    }
}