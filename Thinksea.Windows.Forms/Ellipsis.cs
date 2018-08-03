using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Thinksea.Windows.Forms
{
	/// <summary>
	/// 定义如何用省略号替换剪辑文本的模式。
	/// </summary>
	[Flags]
	public enum EllipsisFormat
	{
		/// <summary>
		/// 完整的字符串，无剪辑操作。（只能单独使用）
		/// </summary>
		None = 0,
		/// <summary>
        /// 剪辑并在文本末尾添加省略号。（和 Path 或 Word 组合使用）
		/// </summary>
		End = 1,
		/// <summary>
        /// 剪辑并在文本起始位置添加省略号。（和 Path 或 Word 组合使用）
		/// </summary>
		Start = 2,
		/// <summary>
        /// 剪辑并在文本中间位置添加省略号。（和 Path 或 Word 组合使用）
		/// </summary>
		Middle = 3,
		/// <summary>
        /// 剪辑并尽可能保持文本的最后一个路径分隔符后面的文本的完整。（和 Start、 Middle 或 End 组合使用）
		/// </summary>
		Path = 4,
		/// <summary>
        /// 剪辑并尽可能保持单词完整。（和 Start、 Middle 或 End 组合使用）
		/// </summary>
		Word = 8
	}


    /// <summary>
    /// 封装了剪辑文本的功能。
    /// </summary>
	public class Ellipsis
	{
		/// <summary>
		/// 定义一个串，用于替换省略掉的文本。
		/// </summary>
		public static readonly string EllipsisChars = "...";

		private static Regex prevWord = new Regex(@"\W*\w*$");
		private static Regex nextWord = new Regex(@"\w*\W*");

		/// <summary>
        /// 转换文本使其长度能够在指定控件的宽度范围内显示，使用省略号替换剪辑掉的部分。
		/// </summary>
		/// <param name="text">将要被转换的文本。</param>
		/// <param name="ctrl">剪辑后的文本即将在此控件内显示。</param>
		/// <param name="options">指示如何处理文本。</param>
		/// <returns>当参数“text”为 null 或 string.Empty 返回 text 的值，否则返回转换后的内容。</returns>
        public static string Compact(string text, Control ctrl, EllipsisFormat options)
        {
			if (ctrl == null)
				throw new ArgumentNullException("ctrl");

            using (Graphics dc = ctrl.CreateGraphics())
            {
                return Compact(dc, text, ctrl.Width, ctrl.Font, options);
            }

        }

        /// <summary>
        /// 转换文本使其长度能够在指定控件的宽度范围内显示，使用省略号替换剪辑掉的部分。
		/// </summary>
        /// <param name="dc">剪辑文本所使用的设备上下文。</param>
        /// <param name="text">将要被转换的文本。</param>
        /// <param name="width">显示区域的宽度。</param>
        /// <param name="font">要应用于已剪辑文本的 System.Drawing.Font。</param>
		/// <param name="options">指示如何处理文本。</param>
		/// <returns>当参数“text”为 null 或 string.Empty 返回 text 的值，否则返回转换后的内容。</returns>
        public static string Compact(IDeviceContext dc, string text, int width, System.Drawing.Font font, EllipsisFormat options)
        {
            if (string.IsNullOrEmpty(text))
                return text;

            // no aligment information
            if ((EllipsisFormat.Middle & options) == 0)
                return text;

            Size s = TextRenderer.MeasureText(dc, text, font);

            // control is large enough to display the whole text
            if (s.Width <= width)
                return text;

            string pre = "";
            string mid = text;
            string post = "";

            bool isPath = (EllipsisFormat.Path & options) != 0;

            // split path string into <drive><directory><filename>
            if (isPath)
            {
                pre = Path.GetPathRoot(text);
                mid = Path.GetDirectoryName(text).Substring(pre.Length);
                post = Path.GetFileName(text);
            }

            int len = 0;
            int seg = mid.Length;
            string fit = "";

            // find the longest string that fits into 
            // the control boundaries using bisection method
            while (seg > 1)
            {
                seg -= seg / 2;

                int left = len + seg;
                int right = mid.Length;

                if (left > right)
                    continue;

                if ((EllipsisFormat.Middle & options) == EllipsisFormat.Middle)
                {
                    right -= left / 2;
                    left -= left / 2;
                }
                else if ((EllipsisFormat.Start & options) != 0)
                {
                    right -= left;
                    left = 0;
                }

                // trim at a word boundary using regular expressions
                if ((EllipsisFormat.Word & options) != 0)
                {
                    if ((EllipsisFormat.End & options) != 0)
                    {
                        left -= prevWord.Match(mid, 0, left).Length;
                    }
                    if ((EllipsisFormat.Start & options) != 0)
                    {
                        right += nextWord.Match(mid, right).Length;
                    }
                }

                // build and measure a candidate string with ellipsis
                string tst = mid.Substring(0, left) + EllipsisChars + mid.Substring(right);

                // restore path with <drive> and <filename>
                if (isPath)
                {
                    tst = Path.Combine(Path.Combine(pre, tst), post);
                }
                s = TextRenderer.MeasureText(dc, tst, font);

                // candidate string fits into control boundaries, try a longer string
                // stop when seg <= 1
                if (s.Width <= width)
                {
                    len += seg;
                    fit = tst;
                }
            }

            if (len == 0) // string can't fit into control
            {
                // "path" mode is off, just return ellipsis characters
                if (!isPath)
                    return EllipsisChars;

                // <drive> and <directory> are empty, return <filename>
                if (pre.Length == 0 && mid.Length == 0)
                    return post;

                // measure "C:\...\filename.ext"
                fit = Path.Combine(Path.Combine(pre, EllipsisChars), post);

                s = TextRenderer.MeasureText(dc, fit, font);

                // if still not fit then return "...\filename.ext"
                if (s.Width > width)
                    fit = Path.Combine(EllipsisChars, post);
            }
            return fit;
        }

	}
}
