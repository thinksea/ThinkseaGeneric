namespace Thinksea
{
    /// <summary>
    /// 网站编程基本处理类。封装了网站编程过程中常用的基本功能。（不能继承此类）
    /// </summary>
    /// <remarks>
    /// <note>不要尝试从此类继承或对其进行实例化。</note>
    /// </remarks>
    public static class Web
    {
        /// <summary>
        /// 从指定的 HTML 文本中清除脚本标签“SCRIPT”。
        /// </summary>
        /// <param name="Html">具有 HTML 格式的代码文本。</param>
        /// <returns>清除“SCRIPT”标签后的返回值。</returns>
        /// <example>
        /// <para lang="C#">
        /// 下面的代码演示了如何使用这个方法：
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[this.Response.Write(RemoveScriptLable(@"
        /// <script language=javascript>
        /// alert('ok');
        /// </script>
        /// abc"));
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>abc
        /// </para>
        /// </example>
        public static string RemoveScriptLable(string Html)
        {
            System.Text.RegularExpressions.RegexOptions regexOptions = System.Text.RegularExpressions.RegexOptions.ExplicitCapture | System.Text.RegularExpressions.RegexOptions.Singleline | System.Text.RegularExpressions.RegexOptions.IgnoreCase;

            string reSCRIPT = @"<SCRIPT(?:[^>]*)>(?:.*?)</SCRIPT>";

            Html = System.Text.RegularExpressions.Regex.Replace(Html, reSCRIPT, "", regexOptions);

            return Html;

        }

        /// <summary>
        /// 从指定的 HTML 文本中清除 HTML 标签和脚本标签“SCRIPT”等不可显示代码，只保留可显示文本。
        /// </summary>
        /// <param name="Html">具有 HTML 格式的代码文本。</param>
        /// <returns>清除不可显示代码后的返回值。</returns>
        /// <example>
        /// <para lang="C#">
        /// 下面的代码演示了如何使用这个方法：
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[this.Response.Write(RemoveHtmlLable(@"
        /// <table id=Table1 align=center border=0>
        /// <tr><TD>用户名</TD></tr>
        /// <TR><TD>密&nbsp;码</TD></TR>
        /// <TR><TD>验证码</TD></TR>
        /// </table>"));
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>用户名密 码验证码
        /// </para>
        /// </example>
        public static string RemoveHtmlLable(string Html)
        {
            System.Text.RegularExpressions.RegexOptions regexOptions = System.Text.RegularExpressions.RegexOptions.ExplicitCapture | System.Text.RegularExpressions.RegexOptions.Singleline | System.Text.RegularExpressions.RegexOptions.IgnoreCase;

            string reHTML = @"<HTML([^>]*)>|</HTML>";
            string reContain = @"<HEAD(?:[^>]*)>(?:.*?)</HEAD>|<STYLE(?:[^>]*)>(?:.*?)</STYLE>|<SCRIPT(?:[^>]*)>(?:.*?)</SCRIPT>";
            string reComment = @"<!--(.*?)-->";
            //string reBODY = @"<BODY(?:[^>]*)>|</BODY><TBODY(?:[^>]*)>|</TBODY>";
            //string reLable = @"<A(?:[^>]*)>|</A>|<SPAN(?:[^>]*)>|</SPAN>|<DIV(?:[^>]*)>|</DIV>|<FONT(?:[^>]*)>|</FONT>|<IMG(?:[^>]*)>|</IMG>|<TABLE(?:[^>]*)>|</TABLE>|<TR(?:[^>]*)>|</TR>|<TD(?:[^>]*)>|</TD>|<TBODY(?:[^>]*)>|</TBODY>";
            string reLable = @"<([^>]*)>";
            string reSpace = @"\s+";
            //string reOther = @"<(?:[^>]*)>|</(?:[^>]*)>";

            Html = System.Text.RegularExpressions.Regex.Replace(Html, reHTML, "", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reContain, "", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reComment, "", regexOptions);
            //Html = System.Text.RegularExpressions.Regex.Replace( Html, reBODY, "", regexOptions );
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reLable, "", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reSpace, "", regexOptions);
            //Html = System.Text.RegularExpressions.Regex.Replace( Html, reOther, "", regexOptions );

            return Html;

        }

        /// <summary>
        /// <![CDATA[
        /// 从 HTML 格式文本中截取子字符串。截取前先清除 HTML 标签和脚本标签“SCRIPT”等不可显示代码，同时清除可以解释为空白字符的代码（如：HTML代码中的空格标记“&nbsp;”），只保留可显示文本。
        /// ]]>
        /// </summary>
        /// <param name="input">输入具有 HTML 格式的代码文本。</param>
        /// <param name="maxLength">截取的文本最大长度。</param>
        /// <returns>截取后的子字符串（不包含不可见代码元素）</returns>
        /// <remarks>
        /// <![CDATA[
        /// 截取前先清除 HTML 标签和脚本标签“SCRIPT”等不可显示代码，同时清除可以解释为空白字符的代码（如：HTML代码中的空格标记“&nbsp;”），只保留可显示文本。
        /// ]]>
        /// </remarks>
        /// <example>
        /// <para lang="C#">
        /// 下面的代码演示了如何使用这个方法：
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[this.Response.Write(RemoveHtmlLable(@"
        /// <table id=Table1 align=center border=0>
        /// <tr><TD>用户名</TD></tr>
        /// <TR><TD>密&nbsp;码</TD></TR>
        /// <TR><TD>验证码</TD></TR>
        /// </table>", 8));
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>用户名密
        /// </para>
        /// </example>
        public static string HtmlSubstring(string input, int maxLength)
        {
            input = Thinksea.Web.HtmlToText(input);
            System.Text.RegularExpressions.RegexOptions regexOptions = System.Text.RegularExpressions.RegexOptions.ExplicitCapture | System.Text.RegularExpressions.RegexOptions.IgnoreCase;
            input = System.Text.RegularExpressions.Regex.Replace(input, @"^\s+", "", regexOptions);//删除开头的空白符。
            input = System.Text.RegularExpressions.Regex.Replace(input, @"(\r|\n|\s)+", " ", regexOptions);//回车换行符和空格全部做为一个空格处理。（既要保持文字紧凑，又要兼顾英文单词中间需有一空格。）
            input = Thinksea.Text.Chinese.Substring(input, maxLength);
            return Thinksea.Web.TextToHtml(input);
            ////此项功能中的代码需要注意执行顺序（例如在从Html代码中清除部分内容后的剩余代码可能重新组合成新的HTML标记），否则可能会引起不宜被发现的错误。
            //System.Text.RegularExpressions.RegexOptions regexOptions = System.Text.RegularExpressions.RegexOptions.ExplicitCapture | System.Text.RegularExpressions.RegexOptions.IgnoreCase;

            //string result = System.Text.RegularExpressions.Regex.Replace( input, "&nbsp;", "", regexOptions );//清除HTML代码中的空格标记“&nbsp;”
            //result = Thinksea.Web.RemoveHtmlLable(result);
            //result = System.Text.RegularExpressions.Regex.Replace( result, @"\s", "", regexOptions );//清除可以解释为空白字符的代码
            //return Thinksea.General.Substring( result, maxLength );

        }

        /// <summary>
        /// 将纯文本转换成具有相似格式编排的 HTML 代码文本。
        /// </summary>
        /// <param name="Text">一个 string 对象。可能具有格式编排的文本。</param>
        /// <returns>具有 HTML 格式的文本对象。</returns>
        /// <remarks>
        /// 为了保证转换后的内容尽可能保持之前的文本编排格式，将换行符和空格符号等内容进行相应的转换。
        /// </remarks>
        /// <example>
        /// <para lang="C#">
        /// 下面的代码演示了如何使用这个方法：
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[string str = @"
        /// 欢迎使用 Thinksea 产品：
        ///     更多的内容请从 http://www.thinksea.com/ 站点获取。
        /// ";
        /// this.Response.Write(Thinksea.Web.TextToHtml(str));
        /// ]]>
        /// </code>
        /// </example>
        public static string TextToHtml(string Text)
        {
            System.Text.RegularExpressions.RegexOptions regexOptions = System.Text.RegularExpressions.RegexOptions.ExplicitCapture | System.Text.RegularExpressions.RegexOptions.Singleline | System.Text.RegularExpressions.RegexOptions.IgnoreCase;
            string r = System.Web.HttpUtility.HtmlEncode(Text).Replace("\t", "    ");
            r = System.Text.RegularExpressions.Regex.Replace(r, @"^ (?<notspace>[^ ]|$)",
                delegate(System.Text.RegularExpressions.Match m)
                {
                    return "&nbsp;" + m.Groups["notspace"].Value;
                }
                , regexOptions);
            r = System.Text.RegularExpressions.Regex.Replace(r, @"(?<space> +) (?<notspace>[^ ]|$)",
                delegate(System.Text.RegularExpressions.Match m)
                {
                    return m.Groups["space"].Value.Replace(" ", "&nbsp;") + " " + m.Groups["notspace"].Value;
                }
                , regexOptions);
            r = System.Text.RegularExpressions.Regex.Replace(r, @"\n\r|\r\n|\r|\n", "<br />", regexOptions);
            return r;
            ////System.Text.RegularExpressions.RegexOptions regexOptions = System.Text.RegularExpressions.RegexOptions.ExplicitCapture | System.Text.RegularExpressions.RegexOptions.Singleline | System.Text.RegularExpressions.RegexOptions.IgnoreCase;
            ////return System.Text.RegularExpressions.Regex.Replace(System.Web.HttpUtility.HtmlEncode(Text).Replace(" ", "&nbsp;").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;"), @"\n\r|\r\n|\r|\n", "<br />", regexOptions);
            //if(Text==null) return null;
            //return Text.replace(/&/gi, "&amp;").replace(/\"/gi, "&quot;").replace(/</gi, "&lt;").replace(/>/gi, "&gt;").replace(/ /gi, "&nbsp;").replace(/\t/gi, "&nbsp;&nbsp;&nbsp;&nbsp;").replace(/\n\r|\r\n|\r|\n/gi, "<br />");
            //return System.Web.HttpUtility.HtmlEncode(Text).Replace(" ", "&nbsp;").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;").Replace("\n\r", "<br />").Replace("\r\n", "<br />").Replace("\r", "<br />").Replace("\n", "<br />");

        }

        /// <summary>
        /// 将 HTML 代码片段转换成具有相似格式编排的纯文本形式。
        /// </summary>
        /// <param name="Html">一段 HTML 片段。</param>
        /// <returns>具有 HTML 格式的文本对象。</returns>
        /// <remarks>
        /// <![CDATA[为了保证转换后的内容尽可能保持之前的文本编排格式，将换行“<br />”和空格符号“&nbsp;”等内容进行相应的转换。]]>
        /// </remarks>
        /// <example>
        /// <para lang="C#">
        /// 下面的代码演示了如何使用这个方法：
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[string str = @"
        /// 欢迎使用 Thinksea 产品：<br />
        /// &nbsp;&nbsp;&nbsp;&nbsp;更多的内容请从 http://www.thinksea.com/ 站点获取。
        /// ";
        /// this.Response.Write(Thinksea.Web.HtmlToText(str));
        /// ]]>
        /// </code>
        /// </example>
        public static string HtmlToText(string Html)
        {
            System.Text.RegularExpressions.RegexOptions regexOptions = System.Text.RegularExpressions.RegexOptions.ExplicitCapture | System.Text.RegularExpressions.RegexOptions.Singleline | System.Text.RegularExpressions.RegexOptions.IgnoreCase;

            string reSpace = @"(\s*)\n(\s*)";//去掉空白字符。
            string reHTML = @"<HTML([^>]*)>|</HTML>";
            string reContain = @"<HEAD(?:[^>]*)>(?:.*?)</HEAD>|<STYLE(?:[^>]*)>(?:.*?)</STYLE>|<SCRIPT(?:[^>]*)>(?:.*?)</SCRIPT>";
            string reComment = @"<!--(.*?)-->";
            string reTD = @"<TD(?:[^>]*)>(?<Block>.*?)</TD>";
            string reBlock = @"<DIV(?:[^>]*)>(?<Block>.*?)</DIV>|<TR(?:[^>]*)>(?<Block>.*?)</TR>";
            string reParagraph = @"<P(?:[^>]*)>(?<Paragraph>.*?)</P>";
            string reBR = @"<br(\s*)>|<br(\s*)/>";
            string reLable = @"<([^>]*)>";
            string reNBSP4 = @"&nbsp;&nbsp;&nbsp;&nbsp;";
            string reNBSP = @"&nbsp;";
            string reQUOT = @"&quot;";
            string reLT = @"&lt;";
            string reGT = @"&gt;";
            string reAMP = @"&amp;";

            Html = System.Text.RegularExpressions.Regex.Replace(Html, reSpace, "", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reHTML, "", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reContain, "", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reComment, "", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reTD, "${Block}\t", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reBlock, "\r\n${Block}", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reParagraph, "\r\n${Paragraph}\r\n", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reBR, "\r\n", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reLable, "", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reNBSP4, "\t", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reNBSP, " ", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reQUOT, "\"", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reLT, "<", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reGT, ">", regexOptions);
            Html = System.Text.RegularExpressions.Regex.Replace(Html, reAMP, "&", regexOptions);

            return Html;
            /*
                        <?php
                        // $document 应包含一个 HTML 文档。
                        // 本例将去掉 HTML 标记，javascript 代码
                        // 和空白字符。还会将一些通用的
                        // HTML 实体转换成相应的文本。 




                        $search = array (“‘<script[^>]*?>.*?</script>’si”,  // 去掉 javascript
                        “‘<[\/\!]*?[^<>]*?>’si”,           // 去掉 HTML 标记
                        “‘([\r\n])[\s]+’”,                 // 去掉空白字符
                        “‘&(quot|#34);’i”,                 // 替换 HTML 实体
                        “‘&(amp|#38);’i”,
                        “‘&(lt|#60);’i”,
                        “‘&(gt|#62);’i”,
                        “‘&(nbsp|#160);’i”,
                        “‘&(iexcl|#161);’i”,
                        “‘&(cent|#162);’i”,
                        “‘&(pound|#163);’i”,
                        “‘&(copy|#169);’i”,
                        “‘&#(\d+);’e”);                    // 作为 PHP 代码运行$replace = array (“”,
                        “”,
                        “\\1″,
                        “\”",
                        “&”,
                        “<”,
                        “>”,
                        “ ”,
                        chr(161),
                        chr(162),
                        chr(163),
                        chr(169),
                        “chr(\\1)”);$text = preg_replace ($search, $replace, $document);
                        ?> 
            */
        }


        #region URI 参数处理。
        /// <summary>
        /// 封装了 URI 扩展处理功能。
        /// </summary>
        private class UriExtTool
        {
            /// <summary>
            /// 定义 URI 的基础参数数据结构。
            /// </summary>
            private class QueryItem
            {
                /// <summary>
                /// 参数名。
                /// </summary>
                public string Key;
                /// <summary>
                /// 参数值。
                /// </summary>
                public string Value = null;
                /// <summary>
                /// 一个构造方法。
                /// </summary>
                public QueryItem()
                {

                }
                /// <summary>
                /// 用指定的数据初始化此实例。
                /// </summary>
                /// <param name="Key">参数名</param>
                /// <param name="Value">参数值</param>
                public QueryItem(string Key, string Value)
                {
                    this.Key = Key;
                    this.Value = Value;
                }

                /// <summary>
                /// 返回此实例的字符串表示形式。
                /// </summary>
                /// <returns></returns>
                public override string ToString()
                {
                    if (this.Value == null)
                    {
                        return this.Key;
                    }
                    else
                    {
                        return this.Key + "=" + this.Value;
                    }
                }
            }

            /// <summary>
            /// URI 基本路径信息。
            /// </summary>
            private string path = null;
            /// <summary>
            /// URI 的参数。
            /// </summary>
            private System.Collections.Generic.List<QueryItem> query = null;
            /// <summary>
            /// URI 的页面内部定位标记
            /// </summary>
            private string mark = null;

            /// <summary>
            /// 用指定的 URI 创建此实例。
            /// </summary>
            /// <param name="uri">一个可能包含参数的 uri 字符串。</param>
            /// <returns>URI 解析实例。</returns>
            public static UriExtTool Create(string uri)
            {
                UriExtTool result = new UriExtTool();

                int queryIndex = uri.IndexOf('?');
                int sharpIndex = -1;
                if (queryIndex > -1)
                {
                    sharpIndex = uri.IndexOf('#', queryIndex + 1);
                }
                else
                {
                    sharpIndex = uri.IndexOf('#');
                }

                if (queryIndex > -1)
                {
                    result.path = uri.Substring(0, queryIndex);
                }
                else if (sharpIndex > -1)
                {
                    result.path = uri.Substring(0, sharpIndex);
                }
                else
                {
                    result.path = uri;
                }

                if (sharpIndex > -1)
                {
                    result.mark = uri.Substring(sharpIndex);
                }

                if (queryIndex > -1)
                {
                    string queryString;
                    if (sharpIndex > -1)
                    {
                        queryString = uri.Substring(queryIndex + 1, sharpIndex - queryIndex - 1);
                    }
                    else
                    {
                        queryString = uri.Substring(queryIndex + 1);
                    }
                    if (queryString.Length > 0)
                    {
                        result.query = new System.Collections.Generic.List<QueryItem>();
                        string[] queryList = queryString.Split('&', '?');
                        foreach (string item in queryList)
                        {
                            int enqIndex = item.IndexOf('=');
                            if (enqIndex > -1)
                            {
                                result.query.Add(new QueryItem(item.Substring(0, enqIndex), item.Substring(enqIndex + 1)));
                            }
                            else
                            {
                                result.query.Add(new QueryItem(item, null));
                            }
                        }
                    }
                }

                return result;
            }

            /// <summary>
            /// 返回此实例的字符串表示形式。
            /// </summary>
            /// <returns>返回一个 URI，此实例到字符串表示形式。</returns>
            public override string ToString()
            {
                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                if (path != null)
                {
                    sb.Append(path);
                }
                if (this.query != null && this.query.Count > 0)
                {
                    sb.Append('?');
                    for (int i = 0; i < this.query.Count; i++)
                    {
                        var item = this.query[i];
                        if (i > 0)
                        {
                            sb.Append('&');
                        }
                        sb.Append(item.ToString());
                    }
                }
                if (mark != null)
                {
                    sb.Append(mark);
                }
                return sb.ToString();
            }

            /// <summary>
            /// 从指定的 URI 中获取指定的参数的值。
            /// </summary>
            /// <param name="Name">参数名。</param>
            /// <returns>指定参数的值，如果找不到这个参数则返回 null。</returns>
            public string GetUriParameter(string Name)
            {
                if (this.query != null)
                {
                    foreach (var item in this.query)
                    {
                        if (item.Key.ToLower() == Name.ToLower())
                        {
                            if (item.Value == null)
                            {
                                return null;
                            }
                            else
                            {
                                return System.Uri.UnescapeDataString(item.Value);
                            }
                        }
                    }
                }
                //如果没有找到匹配项。
                return null;
            }

            /// <summary>
            /// 为指定的 URI 设置参数。
            /// </summary>
            /// <param name="Name">参数名。</param>
            /// <param name="Value">新的参数值。</param>
            public void SetUriParameter(string Name, string Value)
            {
                if (this.query != null)
                {
                    for (int i = 0; i < this.query.Count; i++)
                    {
                        var item = this.query[i];
                        if (item.Key.ToLower() == Name.ToLower())
                        {
                            this.query[i] = new QueryItem(Name, (Value == null ? null : System.Uri.EscapeDataString(Value)));
                            return;
                        }
                    }
                }
                if (this.query == null)
                {
                    this.query = new System.Collections.Generic.List<QueryItem>();
                }
                this.query.Add(new QueryItem(Name, (Value == null ? null : System.Uri.EscapeDataString(Value))));
            }

            /// <summary>
            /// 从指定的 URI 删除参数。
            /// </summary>
            /// <param name="Name">参数名。</param>
            public void RemoveUriParameter(string Name)
            {
                if (this.query != null)
                {
                    for (int i = 0; i < this.query.Count; i++)
                    {
                        var item = this.query[i];
                        if (item.Key.ToLower() == Name.ToLower())
                        {
                            this.query.Remove(item);
                        }
                    }
                }
            }

            /// <summary>
            /// 从指定的 URI 删除所有参数，只保留问号“?”之前的部分或者按照参数选择是否保留页面内部定位标记。
            /// </summary>
            /// <param name="retainSharp">指示是否应保留页面内部定位标记（井号后的内容）。</param>
            /// <returns>已经去除参数的 uri 字符串。</returns>
            public void ClearUriParameter(bool retainSharp)
            {
                this.query = null;
                if (!retainSharp)
                {
                    this.mark = null;
                }
            }

        }

        /// <summary>
        /// 从指定的 URI 中获取指定的参数的值。
        /// </summary>
        /// <param name="uri">一个可能包含参数的 uri 字符串。</param>
        /// <param name="Name">参数名。</param>
        /// <returns>指定参数的值，如果找不到这个参数则返回 null。</returns>
        /// <example>
        /// <para lang="C#">
        /// <![CDATA[
        /// 下面的代码演示了如何使用这个方法从 URL“http://www.thinksea.com/default.aspx?id=1&name=thinksea#mark1”中获取参数“id”的值“1”：
        /// ]]>
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[this.Response.Write("Par:" + Thinksea.Web.GetUriParameter("http://www.thinksea.com/default.aspx?id=1&name=thinksea#mark1", "id"));
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>Par:1
        /// </para>
        /// </example>
        public static string GetUriParameter(string uri, string Name)
        {
            var r = UriExtTool.Create(uri);
            return r.GetUriParameter(Name);
        }

        /// <summary>
        /// 为指定的 URI 设置参数。
        /// </summary>
        /// <param name="uri">一个可能包含参数的 uri 字符串。</param>
        /// <param name="Name">参数名。</param>
        /// <param name="Value">新的参数值。</param>
        /// <returns>已经设置了指定参数名和参数值的 uri 字符串。</returns>
        /// <remarks>
        /// 如果指定的参数存在，则更改参数值为指定的新的参数值，否则，添加一个具有指定参数名和新的参数值的参数。
        /// </remarks>
        /// <example>
        /// <para lang="C#">
        /// <![CDATA[
        /// 下面的代码演示了如何使用这个方法将 URL“http://www.thinksea.com/default.aspx?id=1&name=thinksea”的参数“id=1”更改为“id=2”：
        /// ]]>
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[this.Response.Write(Thinksea.Web.SetUriParameter("http://www.thinksea.com/default.aspx?id=1&name=thinksea", "id", "2"));
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/><![CDATA[http://www.thinksea.com/default.aspx?id=2&name=thinksea]]>
        /// </para>
        /// </example>
        public static string SetUriParameter(string uri, string Name, string Value)
        {
            var r = UriExtTool.Create(uri);
            r.SetUriParameter(Name, Value);
            return r.ToString();
        }

        /// <summary>
        /// 从指定的 URI 删除参数。
        /// </summary>
        /// <param name="uri">一个可能包含参数的 uri 字符串。</param>
        /// <param name="Name">参数名。</param>
        /// <returns>已经移除了指定参数的 uri 字符串。</returns>
        /// <example>
        /// <para lang="C#">
        /// <![CDATA[
        /// 下面的代码演示了如何使用这个方法清除 URL“http://www.thinksea.com/default.aspx?id=1&name=thinksea”的参数“id=1”：
        /// ]]>
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[this.Response.Write(Thinksea.Web.RemoveUriParameter("http://www.thinksea.com/default.aspx?id=1&name=thinksea", "id"));
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>http://www.thinksea.com/default.aspx?name=thinksea
        /// </para>
        /// </example>
        public static string RemoveUriParameter(string uri, string Name)
        {
            var r = UriExtTool.Create(uri);
            r.RemoveUriParameter(Name);
            return r.ToString();
        }

        /// <summary>
        /// 从指定的 URI 删除所有参数，只保留问号“?”之前的部分或者按照参数选择是否保留页面内部定位标记。
        /// </summary>
        /// <param name="uri">一个可能包含参数的 uri 字符串。</param>
        /// <param name="retainSharp">指示是否应保留页面内部标记（井号后的内容）。</param>
        /// <returns>已经去除参数的 uri 字符串。</returns>
        /// <example>
        /// <para lang="C#">
        /// <![CDATA[
        /// 下面的代码演示了如何使用这个方法清除 URL“http://www.thinksea.com/default.aspx?id=1&name=thinksea”的参数部分：
        /// ]]>
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[this.Response.Write(Thinksea.Web.ClearUriParameter("http://www.thinksea.com/default.aspx?id=1&name=thinksea", false));
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>http://www.thinksea.com/default.aspx
        /// </para>
        /// </example>
        public static string ClearUriParameter(string uri, bool retainSharp)
        {
            var r = UriExtTool.Create(uri);
            r.ClearUriParameter(retainSharp);
            return r.ToString();
        }

        /// <summary>
        /// 从指定的 URI 删除所有参数，只保留问号“?”之前的部分或者按照参数选择是否保留页面内部定位标记。
        /// </summary>
        /// <param name="uri">一个可能包含参数的 uri 字符串。</param>
        /// <returns>已经去除参数的 uri 字符串。</returns>
        /// <example>
        /// <para lang="C#">
        /// <![CDATA[
        /// 下面的代码演示了如何使用这个方法清除 URL“http://www.thinksea.com/default.aspx?id=1&name=thinksea”的参数部分：
        /// ]]>
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[this.Response.Write(Thinksea.Web.ClearUriParameter("http://www.thinksea.com/default.aspx?id=1&name=thinksea"));
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>http://www.thinksea.com/default.aspx
        /// </para>
        /// </example>
        public static string ClearUriParameter(string uri)
        {
            var r = UriExtTool.Create(uri);
            r.ClearUriParameter(false);
            return r.ToString();
        }
        #endregion

        /// <summary>
        /// 将指定的文本转换为 JavaScript 字符串。
        /// </summary>
        /// <param name="str">待转换字符串。</param>
        /// <returns>符合 JavaScript 规则的字符串。此返回结果可以直接与双引号或单引号串联构成标准的 JavaScript 字符串。</returns>
        /// <example>
        /// <para lang="C#">
        /// <![CDATA[
        /// 下面的代码演示了如何使用这个方法转换“<a'b"c>”：
        /// ]]>
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[this.Response.Write(Thinksea.Web.ConvertToJavaScriptString("<a'b\"c>"));
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>
        /// <![CDATA[
        /// <a\'b\"c>
        /// ]]>
        /// </para>
        /// </example>
        public static string ConvertToJavaScriptString(string str)
        {
            return str.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"").Replace("\r", "\\r").Replace("\n", "\\n");

        }


        /// <summary>
        /// 构造方法。
        /// </summary>
        static Web()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

    }

}
