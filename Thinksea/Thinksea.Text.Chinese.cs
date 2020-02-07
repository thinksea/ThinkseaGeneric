namespace Thinksea.Text
{
    /// <summary>
    /// 封装了中文处理的基础功能。
    /// </summary>
    public static class Chinese
    {
        /// <summary>
        /// 截取指定字符串的子串。（注意：一个汉字视为两个英文字符）
        /// </summary>
        /// <param name="input">输入字符串。</param>
        /// <param name="maxLength">最大长度。</param>
        /// <returns>截取后的子字符串。</returns>
        /// <remarks>
        /// 从字符串的首字符开始，最多截取 maxLength 个字符。如果截取结果包含半个汉字，为了保持数据完整性同时兼顾最大长度限制，则自动获取 maxLength-1 个字符。
        /// 例如：从字符串“abc欢迎您”截取前6个字符，则返回结果为“abc欢”
        /// <note>
        /// 注意：一个汉字视为两个英文字符。
        /// </note>
        /// </remarks>
        /// <example>
        /// <para lang="C#">
        /// 下面的代码演示了如何使用这个方法：
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[System.Console.WriteLine(Substring("欢迎您abc", 7));
        /// System.Console.WriteLine(Substring("abc欢迎您", 6));
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>欢迎您a
        /// <br/>abc欢
        /// </para>
        /// </example>
        public static string Substring(string input, int maxLength)
        {
            int ltmp;
            int lengthTemp = 0;
            int index = 0;
            while (index < input.Length && lengthTemp < maxLength)
            {
                if (0 <= input[index] && input[index] <= 255)//如果是 ASCII 字符
                {
                    ltmp = 1;
                }
                else
                {
                    ltmp = 2;
                }
                if (lengthTemp + ltmp <= maxLength)
                {
                    lengthTemp += ltmp;
                    index++;
                }
                else
                {
                    break;
                }
            }
            return input.Substring(0, index);
            //return input.Substring(0, (input.Length > maxLength? maxLength: input.Length));
        }

    }

}
