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
        /// <param name="html">具有 HTML 格式的代码文本。</param>
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
        public static string RemoveScriptLable(string html)
        {
            System.Text.RegularExpressions.RegexOptions regexOptions = System.Text.RegularExpressions.RegexOptions.ExplicitCapture | System.Text.RegularExpressions.RegexOptions.Singleline | System.Text.RegularExpressions.RegexOptions.IgnoreCase;

            string reSCRIPT = @"<SCRIPT(?:[^>]*)>(?:.*?)</SCRIPT>";

            html = System.Text.RegularExpressions.Regex.Replace(html, reSCRIPT, "", regexOptions);

            return html;

        }

        /// <summary>
        /// 从指定的 HTML 文本中清除 HTML 标签和脚本标签“SCRIPT”等不可显示代码，只保留可显示文本。
        /// </summary>
        /// <param name="html">具有 HTML 格式的代码文本。</param>
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
        public static string RemoveHtmlLable(string html)
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

            html = System.Text.RegularExpressions.Regex.Replace(html, reHTML, "", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reContain, "", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reComment, "", regexOptions);
            //html = System.Text.RegularExpressions.Regex.Replace( html, reBODY, "", regexOptions );
            html = System.Text.RegularExpressions.Regex.Replace(html, reLable, "", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reSpace, "", regexOptions);
            //html = System.Text.RegularExpressions.Regex.Replace( html, reOther, "", regexOptions );

            return html;

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
        /// <param name="text">一个 string 对象。可能具有格式编排的文本。</param>
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
        public static string TextToHtml(string text)
        {
            System.Text.RegularExpressions.RegexOptions regexOptions = System.Text.RegularExpressions.RegexOptions.ExplicitCapture | System.Text.RegularExpressions.RegexOptions.Singleline | System.Text.RegularExpressions.RegexOptions.IgnoreCase;
            string r = System.Web.HttpUtility.HtmlEncode(text).Replace("\t", "    ");
            r = System.Text.RegularExpressions.Regex.Replace(r, @"^ (?<notspace>[^ ]|$)",
                delegate (System.Text.RegularExpressions.Match m)
                {
                    return "&nbsp;" + m.Groups["notspace"].Value;
                }
                , regexOptions);
            r = System.Text.RegularExpressions.Regex.Replace(r, @"(?<space> +) (?<notspace>[^ ]|$)",
                delegate (System.Text.RegularExpressions.Match m)
                {
                    return m.Groups["space"].Value.Replace(" ", "&nbsp;") + " " + m.Groups["notspace"].Value;
                }
                , regexOptions);
            r = System.Text.RegularExpressions.Regex.Replace(r, @"\n\r|\r\n|\r|\n", "<br />", regexOptions);
            return r;
            ////System.Text.RegularExpressions.RegexOptions regexOptions = System.Text.RegularExpressions.RegexOptions.ExplicitCapture | System.Text.RegularExpressions.RegexOptions.Singleline | System.Text.RegularExpressions.RegexOptions.IgnoreCase;
            ////return System.Text.RegularExpressions.Regex.Replace(System.Web.HttpUtility.HtmlEncode(text).Replace(" ", "&nbsp;").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;"), @"\n\r|\r\n|\r|\n", "<br />", regexOptions);
            //if(text==null) return null;
            //return text.replace(/&/gi, "&amp;").replace(/\"/gi, "&quot;").replace(/</gi, "&lt;").replace(/>/gi, "&gt;").replace(/ /gi, "&nbsp;").replace(/\t/gi, "&nbsp;&nbsp;&nbsp;&nbsp;").replace(/\n\r|\r\n|\r|\n/gi, "<br />");
            //return System.Web.HttpUtility.HtmlEncode(text).Replace(" ", "&nbsp;").Replace("\t", "&nbsp;&nbsp;&nbsp;&nbsp;").Replace("\n\r", "<br />").Replace("\r\n", "<br />").Replace("\r", "<br />").Replace("\n", "<br />");

        }

        /// <summary>
        /// 将 HTML 代码片段转换成具有相似格式编排的纯文本形式。
        /// </summary>
        /// <param name="html">一段 HTML 片段。</param>
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
        public static string HtmlToText(string html)
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

            html = System.Text.RegularExpressions.Regex.Replace(html, reSpace, "", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reHTML, "", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reContain, "", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reComment, "", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reTD, "${Block}\t", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reBlock, "\r\n${Block}", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reParagraph, "\r\n${Paragraph}\r\n", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reBR, "\r\n", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reLable, "", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reNBSP4, "\t", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reNBSP, " ", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reQUOT, "\"", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reLT, "<", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reGT, ">", regexOptions);
            html = System.Text.RegularExpressions.Regex.Replace(html, reAMP, "&", regexOptions);

            return html;
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
        public class UriBuilder
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
                /// <param name="key">参数名</param>
                /// <param name="value">参数值</param>
                public QueryItem(string key, string value)
                {
                    this.Key = key;
                    this.Value = value;
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
            /// <param name="uri">一个 uri 字符串。</param>
            public UriBuilder(string uri)
            {
                int queryIndex = uri.IndexOf('?');
                int sharpIndex;
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
                    this.path = uri.Substring(0, queryIndex);
                }
                else if (sharpIndex > -1)
                {
                    this.path = uri.Substring(0, sharpIndex);
                }
                else
                {
                    this.path = uri;
                }

                if (sharpIndex > -1)
                {
                    this.mark = uri.Substring(sharpIndex);
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
                        this.query = new System.Collections.Generic.List<QueryItem>();
                        string[] queryList = queryString.Split('&', '?');
                        foreach (string item in queryList)
                        {
                            int enqIndex = item.IndexOf('=');
                            if (enqIndex > -1)
                            {
                                this.query.Add(new QueryItem(item.Substring(0, enqIndex), item.Substring(enqIndex + 1)));
                            }
                            else
                            {
                                this.query.Add(new QueryItem(item, null));
                            }
                        }
                    }
                }

            }

			/// <summary>
			/// 对参数按照参数名升序排序。
			/// </summary>
			public void SortQuery() {
                if (this.query != null)
                {
                    this.query.Sort((a, b) => { return string.Compare(a.Key, b.Key, System.StringComparison.OrdinalIgnoreCase); });
                }
            }

            /// <summary>
            /// 删除值为 null 或者空字符串的参数。
            /// </summary>
            public void RemoveNullOrEmpty()
            {
                if (this.query != null)
                {
                    for (int i = this.query.Count - 1; i >= 0; i--)
                    {
                        var item = this.query[i];
                        if (string.IsNullOrEmpty(item.Value))
                        {
                            this.query.RemoveAt(i);
                        }
                    }
                }
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
            /// <param name="name">参数名。</param>
            /// <returns>指定参数的值，如果找不到这个参数则返回 null。</returns>
            public string GetUriParameter(string name)
            {
                if (this.query != null)
                {
                    foreach (var item in this.query)
                    {
                        if (item.Key.ToLowerInvariant() == name.ToLowerInvariant())
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
            /// <param name="name">参数名。</param>
            /// <param name="value">新的参数值。</param>
            public void SetUriParameter(string name, string value)
            {
                if (this.query != null)
                {
                    for (int i = 0; i < this.query.Count; i++)
                    {
                        var item = this.query[i];
                        if (item.Key.ToLowerInvariant() == name.ToLowerInvariant())
                        {
                            this.query[i] = new QueryItem(name, (value == null ? null : System.Uri.EscapeDataString(value)));
                            return;
                        }
                    }
                }
                if (this.query == null)
                {
                    this.query = new System.Collections.Generic.List<QueryItem>();
                }
                this.query.Add(new QueryItem(name, (value == null ? null : System.Uri.EscapeDataString(value))));
            }

            /// <summary>
            /// 从指定的 URI 删除参数。
            /// </summary>
            /// <param name="name">参数名。</param>
            public void RemoveUriParameter(string name)
            {
                if (this.query != null)
                {
                    for (int i = 0; i < this.query.Count; i++)
                    {
                        var item = this.query[i];
                        if (item.Key.ToLowerInvariant() == name.ToLowerInvariant())
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
        /// <param name="name">参数名。</param>
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
        public static string GetUriParameter(string uri, string name)
        {
            var r = new UriBuilder(uri);
            return r.GetUriParameter(name);
        }

        /// <summary>
        /// 为指定的 URI 设置参数。
        /// </summary>
        /// <param name="uri">一个可能包含参数的 uri 字符串。</param>
        /// <param name="name">参数名。</param>
        /// <param name="value">新的参数值。</param>
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
        public static string SetUriParameter(string uri, string name, string value)
        {
            var r = new UriBuilder(uri);
            r.SetUriParameter(name, value);
            return r.ToString();
        }

        /// <summary>
        /// 从指定的 URI 删除参数。
        /// </summary>
        /// <param name="uri">一个可能包含参数的 uri 字符串。</param>
        /// <param name="name">参数名。</param>
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
        public static string RemoveUriParameter(string uri, string name)
        {
            var r = new UriBuilder(uri);
            r.RemoveUriParameter(name);
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
            var r = new UriBuilder(uri);
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
            var r = new UriBuilder(uri);
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
            return str.Replace("\\", "\\\\").Replace("'", "\\'").Replace("\"", "\\\"").Replace("\r", "\\r").Replace("\n", "\\n")
                .Replace("/", "\\/").Replace("<", "\\<").Replace(">", "\\>"); //避免异常终止 <script> 标签，阻止 XSS 攻击。

        }

        #region 设备类型判断。
#if NETFRAMEWORK
        /// <summary>
        /// 判断用户端访问设备是否手机。
        /// </summary>
        /// <returns>如果是手机则返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 基于站点（http://detectmobilebrowsers.com/）提供的内容修改。
        /// </remarks>
        [System.Obsolete()]
        public static bool IsMobile()
        {
            System.Web.HttpContext page = System.Web.HttpContext.Current;
            string userAgent = page.Request.ServerVariables["HTTP_USER_AGENT"];
            return Thinksea.Web.IsMobile(userAgent);
        }
#endif

        /// <summary>
        /// 判断用户端访问设备是否手机。
        /// </summary>
        /// <param name="userAgent">用户代理字符串。</param>
        /// <returns>如果是手机则返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 基于站点（http://detectmobilebrowsers.com/）提供的内容修改。
        /// </remarks>
        public static bool IsMobile(string userAgent)
        {
            System.Text.RegularExpressions.Regex b = new System.Text.RegularExpressions.Regex(@"(android|bb\d+|meego).+mobile|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
            System.Text.RegularExpressions.Regex v = new System.Text.RegularExpressions.Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
            System.Text.RegularExpressions.Regex androidPAD = new System.Text.RegularExpressions.Regex(@"MI PAD", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline); //小米PAD
            if (b.IsMatch(userAgent) || v.IsMatch(userAgent.Substring(0, 4)))
            {
                if (!androidPAD.IsMatch(userAgent))
                {
                    return true;
                }
            }
            return false;
        }

#if NETFRAMEWORK
        /// <summary>
        /// 判断用户端访问设备是否手机或平板。
        /// </summary>
        /// <returns>如果是则返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 基于站点（http://detectmobilebrowsers.com/）提供的内容修改。
        /// 注意：存在一个已知的BUG，无法得知如何识别微软的 surface 平板设备。
        /// </remarks>
        [System.Obsolete()]
        public static bool IsMobileOrPad()
        {
            System.Web.HttpContext page = System.Web.HttpContext.Current;
            string userAgent = page.Request.ServerVariables["HTTP_USER_AGENT"];
            return Thinksea.Web.IsMobileOrPad(userAgent);
        }
#endif

        /// <summary>
        /// 判断用户端访问设备是否手机或平板。
        /// </summary>
        /// <param name="userAgent">用户代理字符串。</param>
        /// <returns>如果是则返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 基于站点（http://detectmobilebrowsers.com/）提供的内容修改。
        /// 注意：存在一个已知的BUG，无法得知如何识别微软的 surface 平板设备。
        /// </remarks>
        public static bool IsMobileOrPad(string userAgent)
        {
            System.Text.RegularExpressions.Regex b = new System.Text.RegularExpressions.Regex(@"(android|bb\d+|meego)|avantgo|bada\/|blackberry|blazer|compal|elaine|fennec|hiptop|iemobile|ip(hone|od|ad)|iris|kindle|lge |maemo|midp|mmp|mobile.+firefox|netfront|opera m(ob|in)i|palm( os)?|phone|p(ixi|re)\/|plucker|pocket|psp|series(4|6)0|symbian|treo|up\.(browser|link)|vodafone|wap|windows ce|xda|xiino", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
            System.Text.RegularExpressions.Regex v = new System.Text.RegularExpressions.Regex(@"1207|6310|6590|3gso|4thp|50[1-6]i|770s|802s|a wa|abac|ac(er|oo|s\-)|ai(ko|rn)|al(av|ca|co)|amoi|an(ex|ny|yw)|aptu|ar(ch|go)|as(te|us)|attw|au(di|\-m|r |s )|avan|be(ck|ll|nq)|bi(lb|rd)|bl(ac|az)|br(e|v)w|bumb|bw\-(n|u)|c55\/|capi|ccwa|cdm\-|cell|chtm|cldc|cmd\-|co(mp|nd)|craw|da(it|ll|ng)|dbte|dc\-s|devi|dica|dmob|do(c|p)o|ds(12|\-d)|el(49|ai)|em(l2|ul)|er(ic|k0)|esl8|ez([4-7]0|os|wa|ze)|fetc|fly(\-|_)|g1 u|g560|gene|gf\-5|g\-mo|go(\.w|od)|gr(ad|un)|haie|hcit|hd\-(m|p|t)|hei\-|hi(pt|ta)|hp( i|ip)|hs\-c|ht(c(\-| |_|a|g|p|s|t)|tp)|hu(aw|tc)|i\-(20|go|ma)|i230|iac( |\-|\/)|ibro|idea|ig01|ikom|im1k|inno|ipaq|iris|ja(t|v)a|jbro|jemu|jigs|kddi|keji|kgt( |\/)|klon|kpt |kwc\-|kyo(c|k)|le(no|xi)|lg( g|\/(k|l|u)|50|54|\-[a-w])|libw|lynx|m1\-w|m3ga|m50\/|ma(te|ui|xo)|mc(01|21|ca)|m\-cr|me(rc|ri)|mi(o8|oa|ts)|mmef|mo(01|02|bi|de|do|t(\-| |o|v)|zz)|mt(50|p1|v )|mwbp|mywa|n10[0-2]|n20[2-3]|n30(0|2)|n50(0|2|5)|n7(0(0|1)|10)|ne((c|m)\-|on|tf|wf|wg|wt)|nok(6|i)|nzph|o2im|op(ti|wv)|oran|owg1|p800|pan(a|d|t)|pdxg|pg(13|\-([1-8]|c))|phil|pire|pl(ay|uc)|pn\-2|po(ck|rt|se)|prox|psio|pt\-g|qa\-a|qc(07|12|21|32|60|\-[2-7]|i\-)|qtek|r380|r600|raks|rim9|ro(ve|zo)|s55\/|sa(ge|ma|mm|ms|ny|va)|sc(01|h\-|oo|p\-)|sdk\/|se(c(\-|0|1)|47|mc|nd|ri)|sgh\-|shar|sie(\-|m)|sk\-0|sl(45|id)|sm(al|ar|b3|it|t5)|so(ft|ny)|sp(01|h\-|v\-|v )|sy(01|mb)|t2(18|50)|t6(00|10|18)|ta(gt|lk)|tcl\-|tdg\-|tel(i|m)|tim\-|t\-mo|to(pl|sh)|ts(70|m\-|m3|m5)|tx\-9|up(\.b|g1|si)|utst|v400|v750|veri|vi(rg|te)|vk(40|5[0-3]|\-v)|vm40|voda|vulc|vx(52|53|60|61|70|80|81|83|85|98)|w3c(\-| )|webc|whit|wi(g |nc|nw)|wmlb|wonu|x700|yas\-|your|zeto|zte\-", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline);
            if (b.IsMatch(userAgent) || v.IsMatch(userAgent.Substring(0, 4)))
            {
                return true;
            }
            return false;
        }

#if NETFRAMEWORK
        /// <summary>
        /// 判断是否在微信浏览器内访问网页。
        /// </summary>
        /// <returns>返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 支持微信客户端 PC 版。
        /// </remarks>
        [System.Obsolete()]
        public static bool IsWeixinBrowser()
        {
            System.Web.HttpContext page = System.Web.HttpContext.Current;
            string userAgent = page.Request.ServerVariables["HTTP_USER_AGENT"];
            return Thinksea.Web.IsWeixinBrowser(userAgent);
        }
#endif

        /// <summary>
        /// 判断是否在微信浏览器内访问网页。
        /// </summary>
        /// <param name="userAgent">用户代理字符串。</param>
        /// <returns>返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 支持微信客户端 PC 版。
        /// </remarks>
        public static bool IsWeixinBrowser(string userAgent)
        {
            if (userAgent.IndexOf("MicroMessenger") != -1)
            {  //strpos() 函数查找字符串在另一字符串中第一次出现的位置。
                return true;
            }
            return false;
        }

        #endregion

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
