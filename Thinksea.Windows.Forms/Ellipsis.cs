using System;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace Thinksea.Windows.Forms
{
	/// <summary>
	/// ���������ʡ�Ժ��滻�����ı���ģʽ��
	/// </summary>
	[Flags]
	public enum EllipsisFormat
	{
		/// <summary>
		/// �������ַ������޼�����������ֻ�ܵ���ʹ�ã�
		/// </summary>
		None = 0,
		/// <summary>
        /// ���������ı�ĩβ����ʡ�Ժš����� Path �� Word ���ʹ�ã�
		/// </summary>
		End = 1,
		/// <summary>
        /// ���������ı���ʼλ������ʡ�Ժš����� Path �� Word ���ʹ�ã�
		/// </summary>
		Start = 2,
		/// <summary>
        /// ���������ı��м�λ������ʡ�Ժš����� Path �� Word ���ʹ�ã�
		/// </summary>
		Middle = 3,
		/// <summary>
        /// �����������ܱ����ı������һ��·���ָ���������ı������������� Start�� Middle �� End ���ʹ�ã�
		/// </summary>
		Path = 4,
		/// <summary>
        /// �����������ܱ��ֵ������������� Start�� Middle �� End ���ʹ�ã�
		/// </summary>
		Word = 8
	}


    /// <summary>
    /// ��װ�˼����ı��Ĺ��ܡ�
    /// </summary>
	public class Ellipsis
	{
		/// <summary>
		/// ����һ�����������滻ʡ�Ե����ı���
		/// </summary>
		public static readonly string EllipsisChars = "...";

		private static Regex prevWord = new Regex(@"\W*\w*$");
		private static Regex nextWord = new Regex(@"\w*\W*");

		/// <summary>
        /// ת���ı�ʹ�䳤���ܹ���ָ���ؼ��Ŀ��ȷ�Χ����ʾ��ʹ��ʡ�Ժ��滻�������Ĳ��֡�
		/// </summary>
		/// <param name="text">��Ҫ��ת�����ı���</param>
		/// <param name="ctrl">��������ı������ڴ˿ؼ�����ʾ��</param>
		/// <param name="options">ָʾ��δ����ı���</param>
		/// <returns>��������text��Ϊ null �� string.Empty ���� text ��ֵ�����򷵻�ת��������ݡ�</returns>
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
        /// ת���ı�ʹ�䳤���ܹ���ָ���ؼ��Ŀ��ȷ�Χ����ʾ��ʹ��ʡ�Ժ��滻�������Ĳ��֡�
		/// </summary>
        /// <param name="dc">�����ı���ʹ�õ��豸�����ġ�</param>
        /// <param name="text">��Ҫ��ת�����ı���</param>
        /// <param name="width">��ʾ����Ŀ��ȡ�</param>
        /// <param name="font">ҪӦ�����Ѽ����ı��� System.Drawing.Font��</param>
		/// <param name="options">ָʾ��δ����ı���</param>
		/// <returns>��������text��Ϊ null �� string.Empty ���� text ��ֵ�����򷵻�ת��������ݡ�</returns>
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