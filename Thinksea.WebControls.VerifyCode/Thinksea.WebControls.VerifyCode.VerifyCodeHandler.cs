using System;
using System.Web;

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
	public class VerifyCodeHandler : IHttpHandler, System.Web.SessionState.IRequiresSessionState
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
                    VerifyCodeHandler.verifyCodeEnumerable = charVerifyCodeEnumerable.ToArray();
                    VerifyCodeHandler.keyValueVerifyCodeEnumerable = keyValueVerifyCodeEnumerable;
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

        #region 验证码元数据序列。
        /// <summary>
        /// 键值对验证码元数据序列。
        /// </summary>
        private static System.Collections.Generic.SortedList<string, string> keyValueVerifyCodeEnumerable = new System.Collections.Generic.SortedList<string, string>();
        /// <summary>
        /// 获取键值对验证码元数据序列。
        /// </summary>
        public static System.Collections.Generic.SortedList<string, string> KeyValueVerifyCodeEnumerable
        {
            get
            {
                return VerifyCodeHandler.keyValueVerifyCodeEnumerable;
            }
        }
        /// <summary>
        /// 字符型验证码元数据序列。
        /// </summary>
        private static string[] verifyCodeEnumerable = new string[] {
                        "3","4","5","6","7"
                        ,"a","b","c","d","e","f","g","h","j","k","m","n","p","r","s","t","u","v","w","x","y"
                        ,"A","C","D","E","F","G","H","I","J","K","M","N","P","Q","R","S","T","U","V","W","X","Y"
        };

        /// <summary>
        /// 获取或设置字符型验证码元数据序列。
        /// </summary>
        [
        //System.ComponentModel.DefaultValue("3,4,5,6,7,a,b,c,d,e,f,g,h,j,k,m,n,p,r,s,t,u,v,w,x,y,A,C,D,E,F,G,H,I,J,K,M,N,P,Q,R,S,T,U,V,W,X,Y"),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("产生的验证码允许出现的字符列表"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public static string[] VerifyCodeEnumerable
        {
            get
            {
                return VerifyCodeHandler.verifyCodeEnumerable;
            }
            set
            {
                VerifyCodeHandler.verifyCodeEnumerable = value;
            }
        }
        #endregion

        #region 字符型验证码长度
        /// <summary>
		/// 字符型验证码长度
		/// </summary>
		private static int _Length = 6;
		/// <summary>
		/// 设置或获取字符型验证码长度
		/// </summary>
		[
		System.ComponentModel.DefaultValue(6),
		System.ComponentModel.Category("Data"),
		System.ComponentModel.Description("字符型验证码长度"),
		System.ComponentModel.NotifyParentProperty(true),
		]
		public static int Length
		{
			get
			{
                return VerifyCodeHandler._Length;
			}
			set
			{
				if (value <= 0)
				{
					throw new System.ArgumentOutOfRangeException("value", value, "指定的参数已超出有效取值的范围，该参数取值必须大于 0。");
				}
                VerifyCodeHandler._Length = value;
			}
		}
        #endregion

        #region 图片扭曲程度
        private static int bendingAngle = 8;
        /// <summary>
        /// 获取或设置图片扭曲程度，默认值为8
        /// </summary>
        [
        System.ComponentModel.DefaultValue(8),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("图片扭曲程度，默认值为8"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public static int BendingAngle
        {
            get
            {
                return VerifyCodeHandler.bendingAngle;
            }
            set
            {
                VerifyCodeHandler.bendingAngle = value;
            }
        }
        #endregion

        #region 验证码字体大小
        /// <summary>
		/// 字号
		/// </summary>
		private static int _FontSize = 22;
		/// <summary>
        /// 获取或设置验证码字体大小。设置太小的字体可能无法显示扭曲效果。
		/// </summary>
		[
		System.ComponentModel.DefaultValue(22),
		System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("验证码字体大小。设置太小的字体可能无法显示扭曲效果。"),
		System.ComponentModel.NotifyParentProperty(true),
		]
		public static int FontSize
		{
			get
			{
                return VerifyCodeHandler._FontSize;
			}
			set
			{
				if (value <= 0)
				{
					throw new System.ArgumentOutOfRangeException("value", value, "指定的参数已超出有效取值的范围，该参数取值必须大于 0。");
				}
                VerifyCodeHandler._FontSize = value;
			}
		}
        #endregion

        #region 内容和图片边缘之间保留的空白大小
        private static int padding = 2;
        /// <summary>
        /// 获取或设置内容和图片边缘之间保留的空白大小(默认2像素)
        /// </summary>
        [
        System.ComponentModel.DefaultValue(2),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("内容和图片边缘之间保留的空白大小(默认2像素)"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public static int Padding
        {
            get
            {
                return VerifyCodeHandler.padding;
            }
            set
            {
                VerifyCodeHandler.padding = value;
            }
        }
        #endregion

        #region 验证码文本随机颜色序列
        private static System.Drawing.Color[] foreColors = { System.Drawing.Color.Black, System.Drawing.Color.Red, System.Drawing.Color.DarkBlue, System.Drawing.Color.Green, System.Drawing.Color.Orange, System.Drawing.Color.Brown, System.Drawing.Color.DarkCyan, System.Drawing.Color.Purple };
        /// <summary>
        /// 获取或设置验证码文本随机颜色数组。
        /// </summary>
        [
            //System.ComponentModel.DefaultValue(System.Drawing.Color.White),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("验证码文本随机颜色数组"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public static System.Drawing.Color[] ForeColors
        {
            get
            {
                return VerifyCodeHandler.foreColors;
            }
            set
            {
                VerifyCodeHandler.foreColors = value;
            }
        }
        #endregion

        #region 背景填充颜色
        private static System.Drawing.Color _BackColor = System.Drawing.Color.White;
		/// <summary>
		/// 设置或获取背景填充颜色
		/// </summary>
		[
			//System.ComponentModel.DefaultValue(System.Drawing.Color.White),
		System.ComponentModel.Category("Data"),
		System.ComponentModel.Description("背景填充颜色"),
		System.ComponentModel.NotifyParentProperty(true),
		]
		public static System.Drawing.Color BackColor
		{
			get
			{
                return VerifyCodeHandler._BackColor;
			}
			set
			{
                VerifyCodeHandler._BackColor = value;
			}
		}
        #endregion

        #region 字体数组
        private static string[] fonts = { "Arial", "Georgia" };
        /// <summary>
        /// 获取或设置验证码文本随机字体数组。
        /// </summary>
        [
            //System.ComponentModel.DefaultValue(System.Drawing.Color.White),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("验证码文本随机字体数组"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public static string[] Fonts
        {
            get
            {
                return VerifyCodeHandler.fonts;
            }
            set
            {
                VerifyCodeHandler.fonts = value;
            }
        }
        #endregion

        #region 产生波形滤镜效果
        private const double PI = 3.1415926535897932384626433832795;
        private const double PI2 = 6.283185307179586476925286766559;

        /// <summary>
        /// 正弦曲线Wave扭曲图片
        /// </summary>
        /// <param name="srcBmp">图片路径</param>
        /// <param name="backColor">背景颜色。</param>
        /// <param name="bXDir">扭曲方向，水平方向扭曲为 True；垂直方向为 False。</param>
        /// <param name="dMultValue">波形的幅度倍数，越大扭曲的程度越高，一般为3</param>
        /// <param name="dPhase">波形的起始相位，取值区间[0-2*PI)</param>
        /// <returns></returns>
        private static System.Drawing.Bitmap TwistImage(System.Drawing.Bitmap srcBmp, System.Drawing.Color backColor, bool bXDir, double dMultValue, double dPhase)
        {
            System.Drawing.Bitmap destBmp = new System.Drawing.Bitmap(srcBmp.Width, srcBmp.Height);

            // 将位图背景填充为白色
            System.Drawing.Graphics graph = System.Drawing.Graphics.FromImage(destBmp);
            //graph.FillRectangle(new System.Drawing.SolidBrush(System.Drawing.Color.White), 0, 0, destBmp.Width, destBmp.Height);
            graph.Clear(backColor);
            graph.Dispose();

            double dBaseAxisLen = bXDir ? (double)destBmp.Height : (double)destBmp.Width;

            for (int i = 0; i < destBmp.Width; i++)
            {
                for (int j = 0; j < destBmp.Height; j++)
                {
                    double dx = 0;
                    dx = bXDir ? (PI2 * (double)j) / dBaseAxisLen : (PI2 * (double)i) / dBaseAxisLen;
                    dx += dPhase;
                    double dy = System.Math.Sin(dx);

                    // 取得当前点的颜色
                    int nOldX = 0, nOldY = 0;
                    nOldX = bXDir ? i + (int)(dy * dMultValue) : i;
                    nOldY = bXDir ? j : j + (int)(dy * dMultValue);

                    System.Drawing.Color color = srcBmp.GetPixel(i, j);
                    if (nOldX >= 0 && nOldX < destBmp.Width
                     && nOldY >= 0 && nOldY < destBmp.Height)
                    {
                        destBmp.SetPixel(nOldX, nOldY, color);
                    }
                }
            }

            return destBmp;
        }

        #endregion

        #region 画一条由两条连在一起构成的随机正弦函数曲线作干扰线
        /// <summary>
        /// 画一条由两条连在一起构成的随机正弦函数曲线作干扰线
        /// </summary>
        /// <param name="image"></param>
        /// <param name="color"></param>
        private static void DrawCurve(System.Drawing.Bitmap image, System.Drawing.Color color)
        {
            int fontSize = image.Height / 3;

            int imageH = image.Height;
            int imageL = image.Width;
            System.Random rand = new System.Random();
            double A = rand.Next(1, imageH / 2);                  // 振幅  
            double b = rand.Next(-imageH / 4, imageH / 4);   // Y轴方向偏移量  
            double f = rand.Next(-imageH / 4, imageH / 4);   // X轴方向偏移量  
            double T = rand.Next(System.Convert.ToInt32(imageH * 1.5), imageL * 2);  // 周期  
            double w = (2 * System.Math.PI) / T;
            double py = 0;

            int px1 = 0;  // 曲线横坐标起始位置  
            int px2 = rand.Next(System.Convert.ToInt32(imageL / 2), System.Convert.ToInt32(imageL * 0.667));  // 曲线横坐标结束位置             
            double px;
            for (px = px1; px <= px2; px = px + 0.9)
            {
                if (w != 0)
                {
                    py = A * System.Math.Sin(w * px + f) + b + imageH / 2;  // y = Asin(ωx+φ) + b  
                    int i = (int)((fontSize - 6) / 4);
                    while (i > 0)
                    {
                        int w2 = System.Convert.ToInt32(px + i);
                        if (w2 < 0)
                        {
                            w2 = 0;
                        }
                        if (w2 >= image.Width)
                        {
                            w2 = image.Width - 1;
                        }
                        int h2 = System.Convert.ToInt32(py + i);
                        if (h2 < 0)
                        {
                            h2 = 0;
                        }
                        if (h2 >= image.Height)
                        {
                            h2 = image.Height - 1;
                        }
                        image.SetPixel(w2, h2, color);  // 这里画像素点比imagettftext和imagestring性能要好很多                    
                        i--;
                    }
                }
            }

            A = rand.Next(1, imageH / 2);                  // 振幅          
            f = rand.Next(-imageH / 4, imageH / 4);   // X轴方向偏移量  
            T = rand.Next(System.Convert.ToInt32(imageH * 1.5), imageL * 2);  // 周期  
            w = (2 * System.Math.PI) / T;
            b = py - A * System.Math.Sin(w * px + f) - imageH / 2;
            px1 = px2;
            px2 = imageL;
            for (px = px1; px <= px2; px = px + 0.9)
            {
                if (w != 0)
                {
                    py = A * System.Math.Sin(w * px + f) + b + imageH / 2;  // y = Asin(ωx+φ) + b  
                    int i = (int)((fontSize - 8) / 4);
                    while (i > 0)
                    {
                        int w2 = System.Convert.ToInt32(px + i);
                        if (w2 < 0)
                        {
                            w2 = 0;
                        }
                        if (w2 >= image.Width)
                        {
                            w2 = image.Width - 1;
                        }
                        int h2 = System.Convert.ToInt32(py + i);
                        if (h2 < 0)
                        {
                            h2 = 0;
                        }
                        if (h2 >= image.Height)
                        {
                            h2 = image.Height - 1;
                        }
                        image.SetPixel(w2, h2, color);  // 这里(while)循环画像素点比imagettftext和imagestring用字体大小一次画出（不用这while循环）性能要好很多
                        i--;
                    }
                }
            }
        }
        #endregion

        #region 生成随机验证码
        /// <summary>
        /// 随机生成一个新的字符型验证码。
        /// </summary>
        /// <param name="codeLen">验证码长度。</param>
        /// <returns></returns>
        [Obsolete("此方法在未来的版本中可能将被移除，请使用新的方法“GenerateVerifyCode”。")]
        public static string GenerateVerifyCodeString(int codeLen)
        {
            if (codeLen == 0)
            {
                codeLen = VerifyCodeHandler.Length;
            }

            string[] arr = VerifyCodeHandler.VerifyCodeEnumerable;

            string code = "";

            int randValue = -1;

            System.Random rand = new System.Random();

            for (int i = 0; i < codeLen; i++)
            {
                randValue = rand.Next(0, arr.Length - 1);

                code += arr[randValue];
            }

            return code;
        }
        /// <summary>
        /// 随机生成一个新的验证码。
        /// </summary>
        /// <returns></returns>
        [Obsolete("此方法在未来的版本中可能将被移除，请使用新的方法“GenerateVerifyCode”。")]
        public static string GenerateVerifyCodeString()
        {
            return GenerateVerifyCodeString(VerifyCodeHandler.Length);
        }

        /// <summary>
        /// 随机生成一个新的验证码。
        /// </summary>
        /// <param name="codeLen">字符型验证码长度。</param>
        /// <param name="question">生成的验证码问题。</param>
        /// <param name="answer">生成的验证码答案。</param>
        /// <returns></returns>
        public static void GenerateVerifyCode(int codeLen, out string question, out string answer)
        {
            System.Random rand = new System.Random();

            #region 用于确定生成验证码类型。
            System.Collections.Generic.List<int> verifyType = new System.Collections.Generic.List<int>();

            if (VerifyCodeHandler.VerifyCodeEnumerable.Length > 0)
            {
                verifyType.Add(1);
            }
            if (VerifyCodeHandler.keyValueVerifyCodeEnumerable.Count > 0)
            {
                verifyType.Add(2);
            }

            if (verifyType.Count == 0)
            {
                throw new System.Exception("未设置可用的验证码元数据。");
            }
            #endregion

            switch (verifyType[rand.Next(0, verifyType.Count)])
            {
                case 1: //生成字符型验证码。
                    {
                        if (codeLen == 0)
                        {
                            codeLen = VerifyCodeHandler.Length;
                        }

                        string[] arr = VerifyCodeHandler.VerifyCodeEnumerable;

                        string code = "";

                        int randValue = -1;

                        for (int i = 0; i < codeLen; i++)
                        {
                            //randValue = rand.Next(0, arr.Length - 1);
                            randValue = rand.Next(0, arr.Length);

                            code += arr[randValue];
                        }

                        answer = question = code;
                    }
                    break;
                case 2: //生成键值对验证码。
                    {
                        int index = rand.Next(0, VerifyCodeHandler.KeyValueVerifyCodeEnumerable.Count);
                        string key = VerifyCodeHandler.KeyValueVerifyCodeEnumerable.Keys[index];
                        string value = VerifyCodeHandler.KeyValueVerifyCodeEnumerable[key];
                        question = key;
                        answer = value;
                    }
                    break;
                default:
                    throw new System.Exception("未知的验证码类型。");
            }

        }

        /// <summary>
        /// 随机生成一个新的验证码。
        /// </summary>
        /// <param name="question">生成的验证码问题。</param>
        /// <param name="answer">生成的验证码答案。</param>
        /// <returns></returns>
        public static void GenerateVerifyCode(out string question, out string answer)
        {
            GenerateVerifyCode(VerifyCodeHandler.Length, out question, out answer);
        }
        #endregion

        #region 生成校验码图片
        /// <summary>
        /// 生成校验码图片。
        /// </summary>
        /// <param name="code">验证码文本。</param>
        /// <returns></returns>
        public System.Drawing.Bitmap GenerateVerifyCodeImage(string code)
        {
            int fSize = VerifyCodeHandler.FontSize;
            int iPadding = VerifyCodeHandler.Padding;

            int fWidth = fSize + iPadding;
            int imageWidth = code.Length * fWidth + iPadding * 2 + fWidth;
            int imageHeight = fSize * 2 + iPadding;

            System.Drawing.Bitmap image = new System.Drawing.Bitmap(imageWidth, imageHeight);

            System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(image);

            g.Clear(VerifyCodeHandler.BackColor);

            System.Random rand = new System.Random();

            int left = 0, top = 0, top1 = 1, top2 = 1;

            int n1 = (imageHeight - fSize - iPadding * 2);
            int n2 = n1 / 4;
            top1 = n2;
            top2 = n2 * 2;

            System.Drawing.Font f;
            System.Drawing.Brush b;

            int cindex, findex;

            #region 随机字体和颜色的验证码字符
            cindex = rand.Next(ForeColors.Length);
            for (int i = 0; i < code.Length; i++)
            {
                findex = rand.Next(Fonts.Length);

                f = new System.Drawing.Font(Fonts[findex], fSize, System.Drawing.FontStyle.Bold);
                b = new System.Drawing.SolidBrush(ForeColors[cindex]);

                if (i % 2 == 1)
                {
                    top = top2;
                }
                else
                {
                    top = top1;
                }

                left = i * fWidth;

                g.DrawString(code.Substring(i, 1), f, b, left, top);
            }
            #endregion

            //#region 给背景添加随机生成的燥点
            //if (VerifyCodeHandler.HasPinto && VerifyCodeHandler.Pinto > 0)
            //{

            //    //System.Drawing.Pen pen = new System.Drawing.Pen(System.Drawing.Color.LightGray, 0);
            //    System.Drawing.Pen pen = new System.Drawing.Pen(VerifyCodeHandler.BackColor, 0);
            //    int c = System.Convert.ToInt32(image.Width * image.Height * VerifyCodeHandler.Pinto);

            //    for (int i = 0; i < c; i++)
            //    {
            //        int x = rand.Next(image.Width);
            //        int y = rand.Next(image.Height);

            //        g.DrawRectangle(pen, x, y, 1, 1);
            //    }
            //}
            //#endregion

            //画一个边框 边框颜色为Color.Gainsboro
            //g.DrawRectangle(new System.Drawing.Pen(System.Drawing.Color.Gainsboro, 0), 0, 0, image.Width - 1, image.Height - 1);
            g.Dispose();

            //产生波形
            if (VerifyCodeHandler.BendingAngle != 0)
            {
                image = VerifyCodeHandler.TwistImage(image, VerifyCodeHandler.BackColor, true, VerifyCodeHandler.BendingAngle, 4);
            }
            DrawCurve(image, VerifyCodeHandler.ForeColors[rand.Next(0, VerifyCodeHandler.ForeColors.Length)]);
            return image;
        }
        #endregion

        ///// <summary>
        ///// 生成包含指定文本的斑点图像。
        ///// </summary>
        ///// <param name="VerifyCodeText">验证码文本</param>
        ///// <param name="FontSize">字号</param>
        ///// <param name="ForeColor">文本颜色。</param>
        ///// <param name="BackColor">背景颜色。</param>
        ///// <param name="IsColorText">指示用彩色显示或者用单色显示文字。</param>
        ///// <param name="Pinto">杂色填充深度</param>
        ///// <returns></returns>
        //internal static System.Drawing.Bitmap GenerateVerifyCodeImage(string VerifyCodeText, int FontSize, System.Drawing.Color ForeColor, System.Drawing.Color BackColor, bool IsColorText, float Pinto)
        //{
        //    System.Drawing.Size size;//输出图像的尺寸
        //    System.Drawing.StringFormat stringFormat = new System.Drawing.StringFormat(System.Drawing.StringFormatFlags.NoClip | System.Drawing.StringFormatFlags.NoWrap | System.Drawing.StringFormatFlags.MeasureTrailingSpaces | System.Drawing.StringFormatFlags.FitBlackBox);
        //    System.Drawing.Font font = new System.Drawing.Font("Arial", FontSize, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Pixel);
        //    System.Drawing.Pen LinePen = new System.Drawing.Pen(new System.Drawing.SolidBrush(ForeColor));
        //    System.Collections.Generic.List<System.Drawing.Color> LineColors = new System.Collections.Generic.List<System.Drawing.Color>();

        //    System.Random rand = new System.Random();

        //    #region 计算图像尺寸。
        //    using (System.Drawing.Bitmap b = new System.Drawing.Bitmap(1, 1, System.Drawing.Imaging.PixelFormat.Format32bppArgb))
        //    {
        //        System.Drawing.Graphics g1 = System.Drawing.Graphics.FromImage(b);
        //        g1.PageUnit = font.Unit;
        //        System.Drawing.SizeF sizeF = g1.MeasureString(VerifyCodeText, font, new System.Drawing.PointF(0, 0), stringFormat);
        //        size = new System.Drawing.Size(System.Convert.ToInt32(System.Math.Ceiling(sizeF.Width)), System.Convert.ToInt32(System.Math.Ceiling(font.Size)));
        //    }
        //    #endregion

        //    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(size.Width, size.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
        //    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
        //    g.Clear(BackColor);

        //    #region 绘制验证码文本。
        //    {
        //        if (!IsColorText)
        //        {
        //            g.DrawString(VerifyCodeText, font, new System.Drawing.SolidBrush(ForeColor), 0, 0, stringFormat);
        //        }
        //        else
        //        {
        //            #region 画彩色验证码。
        //            //System.Drawing.Region[] rgs = g.MeasureCharacterRanges(VerifyCodeText, font, new System.Drawing.RectangleF(0, 0, size.Width, size.Height), stringFormat);
        //            int minR, maxR, minG, maxG, minB, maxB;

        //            minR = ForeColor.R - 160;
        //            if (minR < 0) minR = 0;
        //            maxR = minR + 160;
        //            if (maxR > 255) maxR = 255;

        //            minG = ForeColor.G - 160;
        //            if (minG < 0) minG = 0;
        //            maxG = minG + 160;
        //            if (maxG > 255) maxG = 255;

        //            minB = ForeColor.B - 160;
        //            if (minB < 0) minB = 0;
        //            maxB = minB + 160;
        //            if (maxB > 255) maxB = 255;

        //            float x = 0;
        //            for (int i = 0; i < VerifyCodeText.Length; i++)
        //            {
        //                char ch = VerifyCodeText[i];
        //                int cr, cg, cb;
        //                cr = rand.Next(minR, maxR);
        //                cg = rand.Next(minG, maxG);
        //                cb = rand.Next(minB, maxB);
        //                //int l;
        //                //do
        //                //{
        //                //    cr = rand.Next(0, 255);
        //                //    cg = rand.Next(0, 255);
        //                //    cb = rand.Next(0, 255);
        //                //    l = (cr - this.BackColor.R) ^ 2 + (cg - this.BackColor.G) ^ 2 + (cb - this.BackColor.B) ^ 2;
        //                //}
        //                //while (l > 100 || l < -100);
        //                System.Drawing.Color color = System.Drawing.Color.FromArgb(cr, cg, cb);
        //                LineColors.Add(color);
        //                System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(color);

        //                //System.Drawing.SolidBrush brush = new System.Drawing.SolidBrush(this.ForeColor);

        //                g.DrawString(ch.ToString(), font, brush, x, 0, stringFormat);



        //                //stringFormat.Alignment = System.Drawing.StringAlignment.Center;
        //                //stringFormat.LineAlignment = System.Drawing.StringAlignment.Center;
        //                //System.Drawing.Point dot = new System.Drawing.Point(16,16);
        //                //float angle = rand.Next(-45, 45);
        //                //g.TranslateTransform(dot.X, dot.Y);//移动光标到指定位置
        //                //g.RotateTransform(angle);
        //                //g.DrawString(ch.ToString(), font, brush, 1, 1, stringFormat);
        //                //g.RotateTransform(-angle);//转回去
        //                //g.TranslateTransform(2, -dot.Y);//移动光标到指定位置

        //                if (ch >= 0 && ch <= 255)
        //                {
        //                    x += font.Size / 2 + 2;
        //                }
        //                else
        //                {
        //                    x += font.Size + 2;
        //                }
        //            }
        //            #endregion
        //        }
        //        //g.ResetTransform(); ///////
        //        g.Flush();
        //    }
        //    #endregion

        //    #region 填充杂色。
        //    if (!IsColorText)
        //    {
        //        float p1 = size.Width * size.Height * Pinto;
        //        for (int i = 0; i < p1; i++)
        //        {
        //            int x = rand.Next(size.Width);
        //            int y = rand.Next(size.Height);
        //            bitmap.SetPixel(x, y, LinePen.Color);
        //        }
        //    }
        //    float p2 = 10 * Pinto;
        //    for (int i = 0; i < p2; i++)
        //    {
        //        int x1 = rand.Next(size.Width / 3);
        //        int y1 = rand.Next(size.Height);
        //        int x2 = rand.Next(size.Width * 2 / 3, size.Width);
        //        int y2 = rand.Next(size.Height);
        //        LinePen.Width = System.Convert.ToSingle(System.Math.Ceiling(FontSize / 10F));
        //        //LinePen.Width = System.Convert.ToSingle(System.Math.Ceiling(this.FontSize / 10F)) / 4;
        //        if (IsColorText)
        //        {
        //            System.Drawing.Color color = LineColors[rand.Next(0, LineColors.Count)];
        //            LinePen.Color = color;
        //        }
        //        g.DrawLine(LinePen, x1, y1, x2, y2);

        //    }
        //    #endregion

        //    return bitmap;

        //}

	}
}
