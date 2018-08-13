namespace Thinksea
{
    /// <summary>
    /// 封装了通用的基本功能。（不能继承此类）
    /// </summary>
    /// <remarks>
    /// <note>不要尝试从此类继承或对其进行实例化。</note>
    /// </remarks>
    public static class General
    {
        #region Max 方法重载
        /// <summary>
        /// 获取数组中的最大数。
        /// </summary>
        /// <param name="value">一个 sbyte 数组。</param>
        /// <returns>找到的最大数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static sbyte Max(sbyte[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            sbyte max = value[0];
            foreach (sbyte tmp in value)
            {
                if (tmp > max) max = tmp;
            }
            return max;
        }

        /// <summary>
        /// 获取数组中的最大数。
        /// </summary>
        /// <param name="value">一个 byte 数组。</param>
        /// <returns>找到的最大数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static byte Max(byte[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            byte max = value[0];
            foreach (byte tmp in value)
            {
                if (tmp > max) max = tmp;
            }
            return max;
        }

        /// <summary>
        /// 获取数组中的最大数。
        /// </summary>
        /// <param name="value">一个 short 数组。</param>
        /// <returns>找到的最大数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static short Max(short[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            short max = value[0];
            foreach (short tmp in value)
            {
                if (tmp > max) max = tmp;
            }
            return max;
        }

        /// <summary>
        /// 获取数组中的最大数。
        /// </summary>
        /// <param name="value">一个 ushort 数组。</param>
        /// <returns>找到的最大数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static ushort Max(ushort[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            ushort max = value[0];
            foreach (ushort tmp in value)
            {
                if (tmp > max) max = tmp;
            }
            return max;
        }

        /// <summary>
        /// 获取数组中的最大数。
        /// </summary>
        /// <param name="value">一个 int 数组。</param>
        /// <returns>找到的最大数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static int Max(int[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            int max = value[0];
            foreach (int tmp in value)
            {
                if (tmp > max) max = tmp;
            }
            return max;
        }

        /// <summary>
        /// 获取数组中的最大数。
        /// </summary>
        /// <param name="value">一个 uint 数组。</param>
        /// <returns>找到的最大数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static uint Max(uint[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            uint max = value[0];
            foreach (uint tmp in value)
            {
                if (tmp > max) max = tmp;
            }
            return max;
        }

        /// <summary>
        /// 获取数组中的最大数。
        /// </summary>
        /// <param name="value">一个 long 数组。</param>
        /// <returns>找到的最大数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static long Max(long[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            long max = value[0];
            foreach (long tmp in value)
            {
                if (tmp > max) max = tmp;
            }
            return max;
        }

        /// <summary>
        /// 获取数组中的最大数。
        /// </summary>
        /// <param name="value">一个 ulong 数组。</param>
        /// <returns>找到的最大数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static ulong Max(ulong[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            ulong max = value[0];
            foreach (ulong tmp in value)
            {
                if (tmp > max) max = tmp;
            }
            return max;
        }

        /// <summary>
        /// 获取数组中的最大数。
        /// </summary>
        /// <param name="value">一个 decimal 数组。</param>
        /// <returns>找到的最大数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static decimal Max(decimal[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            decimal max = value[0];
            foreach (decimal tmp in value)
            {
                if (tmp > max) max = tmp;
            }
            return max;
        }

        /// <summary>
        /// 获取数组中的最大数。
        /// </summary>
        /// <param name="value">一个 float 数组。</param>
        /// <returns>找到的最大数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static float Max(float[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            float max = value[0];
            foreach (float tmp in value)
            {
                if (tmp > max) max = tmp;
            }
            return max;
        }

        /// <summary>
        /// 获取数组中的最大数。
        /// </summary>
        /// <param name="value">一个 double 数组。</param>
        /// <returns>找到的最大数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static double Max(double[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            double max = value[0];
            foreach (double tmp in value)
            {
                if (tmp > max) max = tmp;
            }
            return max;
        }

        #endregion

        #region Min 方法重载
        /// <summary>
        /// 获取数组中的最小数。
        /// </summary>
        /// <param name="value">一个 sbyte 数组。</param>
        /// <returns>找到的最小数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static sbyte Min(sbyte[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            sbyte min = value[0];
            foreach (sbyte tmp in value)
            {
                if (tmp < min) min = tmp;
            }
            return min;
        }

        /// <summary>
        /// 获取数组中的最小数。
        /// </summary>
        /// <param name="value">一个 byte 数组。</param>
        /// <returns>找到的最小数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static byte Min(byte[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            byte min = value[0];
            foreach (byte tmp in value)
            {
                if (tmp < min) min = tmp;
            }
            return min;
        }

        /// <summary>
        /// 获取数组中的最小数。
        /// </summary>
        /// <param name="value">一个 short 数组。</param>
        /// <returns>找到的最小数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static short Min(short[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            short min = value[0];
            foreach (short tmp in value)
            {
                if (tmp < min) min = tmp;
            }
            return min;
        }

        /// <summary>
        /// 获取数组中的最小数。
        /// </summary>
        /// <param name="value">一个 ushort 数组。</param>
        /// <returns>找到的最小数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static ushort Min(ushort[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            ushort min = value[0];
            foreach (ushort tmp in value)
            {
                if (tmp < min) min = tmp;
            }
            return min;
        }

        /// <summary>
        /// 获取数组中的最小数。
        /// </summary>
        /// <param name="value">一个 int 数组。</param>
        /// <returns>找到的最小数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static int Min(int[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            int min = value[0];
            foreach (int tmp in value)
            {
                if (tmp < min) min = tmp;
            }
            return min;
        }

        /// <summary>
        /// 获取数组中的最小数。
        /// </summary>
        /// <param name="value">一个 uint 数组。</param>
        /// <returns>找到的最小数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static uint Min(uint[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            uint min = value[0];
            foreach (uint tmp in value)
            {
                if (tmp < min) min = tmp;
            }
            return min;
        }

        /// <summary>
        /// 获取数组中的最小数。
        /// </summary>
        /// <param name="value">一个 long 数组。</param>
        /// <returns>找到的最小数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static long Min(long[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            long min = value[0];
            foreach (long tmp in value)
            {
                if (tmp < min) min = tmp;
            }
            return min;
        }

        /// <summary>
        /// 获取数组中的最小数。
        /// </summary>
        /// <param name="value">一个 ulong 数组。</param>
        /// <returns>找到的最小数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static ulong Min(ulong[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            ulong min = value[0];
            foreach (ulong tmp in value)
            {
                if (tmp < min) min = tmp;
            }
            return min;
        }

        /// <summary>
        /// 获取数组中的最小数。
        /// </summary>
        /// <param name="value">一个 decimal 数组。</param>
        /// <returns>找到的最小数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static decimal Min(decimal[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            decimal min = value[0];
            foreach (decimal tmp in value)
            {
                if (tmp < min) min = tmp;
            }
            return min;
        }

        /// <summary>
        /// 获取数组中的最小数。
        /// </summary>
        /// <param name="value">一个 float 数组。</param>
        /// <returns>找到的最小数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static float Min(float[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            float min = value[0];
            foreach (float tmp in value)
            {
                if (tmp < min) min = tmp;
            }
            return min;
        }

        /// <summary>
        /// 获取数组中的最小数。
        /// </summary>
        /// <param name="value">一个 double 数组。</param>
        /// <returns>找到的最小数。</returns>
        /// <exception cref="System.ArgumentOutOfRangeException">value 元素数量为零。</exception>
        public static double Min(double[] value)
        {
            if (value.Length == 0) throw new System.ArgumentOutOfRangeException("value 元素数量为零。", "value");
            double min = value[0];
            foreach (double tmp in value)
            {
                if (tmp < min) min = tmp;
            }
            return min;
        }

        #endregion

        #region Join 方法重载
        /// <summary>
        /// 在指定 sbyte 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 sbyte 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, sbyte[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 sbyte 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 sbyte 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, sbyte[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 byte 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 byte 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, byte[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 byte 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 byte 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, byte[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 char 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 char 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, char[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 char 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 char 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, char[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 short 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 short 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, short[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 short 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 short 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, short[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 ushort 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 ushort 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, ushort[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 ushort 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 ushort 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, ushort[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 int 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 int 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, int[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 int 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 int 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, int[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 uint 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 uint 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, uint[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 uint 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 uint 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, uint[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 long 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 long 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, long[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 long 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 long 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, long[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 ulong 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 ulong 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, ulong[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 ulong 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 ulong 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, ulong[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 bool 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 bool 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, bool[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 bool 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 bool 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, bool[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 decimal 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 decimal 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, decimal[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 decimal 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 decimal 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, decimal[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 float 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 float 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, float[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 float 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 float 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, float[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 double 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 double 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, double[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 double 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 double 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, double[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        /// <summary>
        /// 在指定 string 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 string 数组。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value) 返回“apple, orange, grape, pear”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, string[] value)
        {
            return Thinksea.General.Join(separator, value, 0, value.Length);

        }

        /// <summary>
        /// 在指定 string 数组的每个元素之间串联指定的分隔符 String，从而产生单个串联的字符串。
        /// </summary>
        /// <param name="separator">一个 String。</param>
        /// <param name="value">一个 string 数组。</param>
        /// <param name="startIndex">要使用的 value 中的第一个数组元素。</param>
        /// <param name="count">要使用的 value 的元素数。</param>
        /// <exception cref="System.ArgumentNullException">value 为空引用（Visual Basic 中为 Nothing）。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">
        /// startIndex 或 count 小于 0。 
        /// - 或 -
        /// startIndex 加上 count 大于 value 中的元素数。
        /// </exception>
        /// <returns>
        /// String，由通过 separator 联接的 value 中的字符串组成。
        /// - 或 -
        /// 如果 count 为零、value 没有元素，则为 Empty。
        /// </returns>
        /// <remarks>
        /// 例如，如果 separator 为“,”，并且 value 的元素为“apple”、“orange”、“grape”和“pear”，则 Join(separator, value, 1, 2) 返回“orange, grape”。
        /// 如果 separator 为空引用（Visual Basic 中为 Nothing），则改用空字符串 (Empty)。
        /// </remarks>
        public static string Join(string separator, string[] value, int startIndex, int count)
        {
            if (value == null) throw new System.ArgumentNullException("value 为空引用（Visual Basic 中为 Nothing）。");
            if (startIndex < 0 || count < 0) throw new System.ArgumentOutOfRangeException("startIndex 或 count 小于 0。");
            if (startIndex + count > value.Length) throw new System.ArgumentOutOfRangeException("startIndex 加上 count 大于 value 中的元素数。");

            if (count == 0 || value.Length == 0) return string.Empty;

            if (separator == null)
            {
                separator = string.Empty;
            }
            string result = string.Empty;
            int len = startIndex + count;
            if (len > value.Length)
            {
                len = value.Length;
            }

            for (int i = startIndex; i < len; i++)
            {
                if (i == startIndex)
                {
                    result += value[i].ToString();
                }
                else
                {
                    result += separator + value[i].ToString();
                }
            }
            return result;

        }

        #endregion

        #region Concat 方法重载
        /// <summary>
        /// 连接两个 sbyte 数组成为一个 sbyte 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static sbyte[] Concat(sbyte[] a, sbyte[] b)
        {
            if (a == null && b == null)
            {
                return new sbyte[0];
            }
            if (a == null)
            {
                a = new sbyte[0];
            }
            if (b == null)
            {
                b = new sbyte[0];
            }

            sbyte[] result = new sbyte[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 byte 数组成为一个 byte 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static byte[] Concat(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return new byte[0];
            }
            if (a == null)
            {
                a = new byte[0];
            }
            if (b == null)
            {
                b = new byte[0];
            }

            byte[] result = new byte[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 char 数组成为一个 char 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static char[] Concat(char[] a, char[] b)
        {
            if (a == null && b == null)
            {
                return new char[0];
            }
            if (a == null)
            {
                a = new char[0];
            }
            if (b == null)
            {
                b = new char[0];
            }

            char[] result = new char[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 short 数组成为一个 short 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static short[] Concat(short[] a, short[] b)
        {
            if (a == null && b == null)
            {
                return new short[0];
            }
            if (a == null)
            {
                a = new short[0];
            }
            if (b == null)
            {
                b = new short[0];
            }

            short[] result = new short[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 ushort 数组成为一个 ushort 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static ushort[] Concat(ushort[] a, ushort[] b)
        {
            if (a == null && b == null)
            {
                return new ushort[0];
            }
            if (a == null)
            {
                a = new ushort[0];
            }
            if (b == null)
            {
                b = new ushort[0];
            }

            ushort[] result = new ushort[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 int 数组成为一个 int 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static int[] Concat(int[] a, int[] b)
        {
            if (a == null && b == null)
            {
                return new int[0];
            }
            if (a == null)
            {
                a = new int[0];
            }
            if (b == null)
            {
                b = new int[0];
            }

            int[] result = new int[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 uint 数组成为一个 uint 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static uint[] Concat(uint[] a, uint[] b)
        {
            if (a == null && b == null)
            {
                return new uint[0];
            }
            if (a == null)
            {
                a = new uint[0];
            }
            if (b == null)
            {
                b = new uint[0];
            }

            uint[] result = new uint[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 long 数组成为一个 long 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static long[] Concat(long[] a, long[] b)
        {
            if (a == null && b == null)
            {
                return new long[0];
            }
            if (a == null)
            {
                a = new long[0];
            }
            if (b == null)
            {
                b = new long[0];
            }

            long[] result = new long[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 ulong 数组成为一个 ulong 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static ulong[] Concat(ulong[] a, ulong[] b)
        {
            if (a == null && b == null)
            {
                return new ulong[0];
            }
            if (a == null)
            {
                a = new ulong[0];
            }
            if (b == null)
            {
                b = new ulong[0];
            }

            ulong[] result = new ulong[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 bool 数组成为一个 bool 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static bool[] Concat(bool[] a, bool[] b)
        {
            if (a == null && b == null)
            {
                return new bool[0];
            }
            if (a == null)
            {
                a = new bool[0];
            }
            if (b == null)
            {
                b = new bool[0];
            }

            bool[] result = new bool[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 decimal 数组成为一个 decimal 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static decimal[] Concat(decimal[] a, decimal[] b)
        {
            if (a == null && b == null)
            {
                return new decimal[0];
            }
            if (a == null)
            {
                a = new decimal[0];
            }
            if (b == null)
            {
                b = new decimal[0];
            }

            decimal[] result = new decimal[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 float 数组成为一个 float 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static float[] Concat(float[] a, float[] b)
        {
            if (a == null && b == null)
            {
                return new float[0];
            }
            if (a == null)
            {
                a = new float[0];
            }
            if (b == null)
            {
                b = new float[0];
            }

            float[] result = new float[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 double 数组成为一个 double 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static double[] Concat(double[] a, double[] b)
        {
            if (a == null && b == null)
            {
                return new double[0];
            }
            if (a == null)
            {
                a = new double[0];
            }
            if (b == null)
            {
                b = new double[0];
            }

            double[] result = new double[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 string 数组成为一个 string 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static string[] Concat(string[] a, string[] b)
        {
            if (a == null && b == null)
            {
                return new string[0];
            }
            if (a == null)
            {
                a = new string[0];
            }
            if (b == null)
            {
                b = new string[0];
            }

            string[] result = new string[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        /// <summary>
        /// 连接两个 object 数组成为一个 object 数组。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>合并后的数组。</returns>
        /// <remarks>
        /// a 或 b 如果为空引用（Visual Basic 中为 Nothing），则改用长度为 0 元素的同类型数组。
        /// </remarks>
        public static object[] Concat(object[] a, object[] b)
        {
            if (a == null && b == null)
            {
                return new object[0];
            }
            if (a == null)
            {
                a = new object[0];
            }
            if (b == null)
            {
                b = new object[0];
            }

            object[] result = new object[a.Length + b.Length];
            int index = 0;
            {
                for (int i = 0; i < a.Length; i++)
                {
                    result[index++] = a[i];
                }
            }

            {
                for (int i = 0; i < b.Length; i++)
                {
                    result[index++] = b[i];
                }
            }
            return result;
        }

        #endregion

        #region MS-SQL 相关。
        /// <summary>
        /// 批量执行 SQL 代码。（支持 GO 语句）
        /// </summary>
        /// <param name="Connection">已经打开的数据库联接。</param>
        /// <param name="SQLString">SQL 代码。</param>
        public static void ExecuteSQL(System.Data.SqlClient.SqlConnection Connection, string SQLString)
        {
            System.Data.SqlClient.SqlCommand sqlCommand = new System.Data.SqlClient.SqlCommand();
            sqlCommand.Connection = Connection;

            System.Collections.Generic.List<string> al = new System.Collections.Generic.List<string>();
            System.Text.RegularExpressions.Regex reg = new System.Text.RegularExpressions.Regex(@"^(\s*)go(\s*)$", System.Text.RegularExpressions.RegexOptions.IgnoreCase | System.Text.RegularExpressions.RegexOptions.Multiline | System.Text.RegularExpressions.RegexOptions.Compiled | System.Text.RegularExpressions.RegexOptions.ExplicitCapture);
            al.AddRange(reg.Split(SQLString));
            foreach (string tmp in al)
            {
                sqlCommand.CommandText = tmp.Trim();
                if (sqlCommand.CommandText.Length > 0)
                {
                    sqlCommand.ExecuteNonQuery();
                }
            }

        }

        /// <summary>
        /// 将指定的字符串按照 SQL 的字符串语法规则进行字符串转义处理。这个被处理的字符串将置于两个单引号之间共同构成符合 SQL 语法的字符串。
        /// </summary>
        /// <param name="CommandText">需要进行转义处理的字符串。</param>
        /// <returns>转义后的符合 SQL 语法规则的字符串。</returns>
        /// <remarks>
        /// 当我们需要执行一条这样的 SQL 语句，其中查询条件[Name]的取值为“abx'de”，如果我们写成 SELECT * FROM [abc] WHERE [Name]='abx'de' 显然是不符合 SQL 语法的，正确的写法应该是 SELECT * FROM [abc] WHERE [Name]='abx''de'，这个方法就是用来处理这种问题的，它会将参数指定的字符串中包含的这些需要作转义处理的内容（类似于单引号“'”等容易导致 SQL 语法错误特殊符号）进行符合 SQL 语法规则的转义处理。
        /// </remarks>
        /// <example>
        /// <para lang="C#">
        /// 下面的代码演示了如何使用这个方法：
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[System.Console.WriteLine("SELECT * FROM [abc] WHERE [Name]='" + FixSQLCommandText("abx'de") + "'");
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>SELECT * FROM [abc] WHERE [Name]='abx''de'
        /// </para>
        /// </example>
        public static System.String FixSQLCommandText(System.String CommandText)
        {
            CommandText = CommandText.Replace("'", "''");
            return CommandText;
        }

        /// <summary>
        /// 将指定的字符串按照 SQL 的 LIKE 命令语法规则进行字符串转义处理。这个被处理的字符串将作为 LIKE 命令参数。
        /// </summary>
        /// <param name="CommandText">需要进行转义处理的字符串。</param>
        /// <returns>转义后的符合 SQL 语法规则的字符串。</returns>
        /// <remarks>
        /// 当我们需要执行一条这样的 SQL 语句，其中查询条件[Name]的取值为“a%b'x[de”，如果我们写成 SELECT * FROM [abc] WHERE [Name] LIKE 'a%b'x[de' 显然是不符合 SQL 语法的，正确的写法应该是 SELECT * FROM [abc] WHERE [Name] LIKE 'a[%]b''x[[]de'，这个方法就是用来处理这种问题的，它会将参数指定的字符串中包含的这些需要作转义处理的内容（类似于百分号“%”等容易导致 SQL 语法错误特殊符号）进行符合 SQL LIKE 命令语法规则的转义处理。
        /// </remarks>
        /// <example>
        /// <para lang="C#">
        /// 下面的代码演示了如何使用这个方法：
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[System.Console.WriteLine("SELECT * FROM [abc] WHERE [Name] LIKE '" + FixSQLCommandTextLike("a%b'x[de") + "'");
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>SELECT * FROM [abc] WHERE [Name] LIKE 'a[%]b''x[[]de'
        /// </para>
        /// </example>
        public static System.String FixSQLCommandTextLike(System.String CommandText)
        {
            CommandText = CommandText.Replace("[", "[[]");
            CommandText = CommandText.Replace("'", "''");
            CommandText = CommandText.Replace("%", "[%]");
            CommandText = CommandText.Replace("_", "[_]");
            return CommandText;
        }

        /// <summary>
        /// 将指定的字段名按照 SQL 的字段语法规则进行字符串转义处理。这个被处理的字符串将被用作 SQL 字段名称。
        /// </summary>
        /// <param name="FieldName">需要进行转义处理的字段名。</param>
        /// <returns>转义后的符合 SQL 字段语法规则的字符串。</returns>
        /// <remarks>
        /// 假设我们需要创建一个命名为“Name]Sex”的 SQL 字段，如果不进行转义处理显然是不符合 SQL 语法的，正确的写法应该是“[Name]]Sex]”，这个方法就是用来处理这种问题的，它会将参数指定的字段名中包含的这些需要作转义处理的内容进行符合 SQL 字段语法规则的转义处理。
        /// </remarks>
        /// <example>
        /// <para lang="C#">
        /// 下面的代码演示了如何使用这个方法：
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[System.Console.WriteLine("SELECT * FROM [abc] WHERE [" + FixSQLFieldName("Name]Sex") + "]='abcde'");
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>SELECT * FROM [abc] WHERE [Name]]Sex]='abcde'
        /// </para>
        /// </example>
        public static string FixSQLFieldName(string FieldName)
        {
            FieldName = FieldName.Replace("]", "]]");
            return FieldName;

        }

        #endregion

        #region 文件大小转换。
        private const long FileLengthB = 1;
        private const long FileLengthKB = FileLengthB * 1024;
        private const long FileLengthMB = FileLengthKB * 1024;
        private const long FileLengthGB = FileLengthMB * 1024;
        private const long FileLengthTB = FileLengthGB * 1024;
        private const long FileLengthPB = FileLengthTB * 1024;
        private const long FileLengthEB = FileLengthPB * 1024;
        //private const long FileLengthZB = FileLengthEB * 1024;
        //private const long FileLengthYB = FileLengthZB * 1024;
        //private const long FileLengthBB = FileLengthYB * 1024;

        /// <summary>
        /// 将文件大小转换为以合适的单位（“EB”、“PB”、“TB”、“GB”、“MB”、“KB”、“B”）表示形式的文本。
        /// </summary>
        /// <param name="size">以字节“B”为单位的文件大小。</param>
        /// <returns>表示文件大小的带有单位的字符串。</returns>
        /// <example>
        /// <para lang="C#">
        /// 下面的代码演示了如何使用这个方法：
        /// </para>
        /// <code lang="C#">
        /// <![CDATA[System.Console.WriteLine(ConvertToFileSize(11));
        /// System.Console.WriteLine(ConvertToFileSize(12989));
        /// System.Console.WriteLine(ConvertToFileSize(1726752));
        /// System.Console.WriteLine(ConvertToFileSize(1526725236));
        /// System.Console.WriteLine(ConvertToFileSize(95393296753236));
        /// ]]>
        /// </code>
        /// <para lang="C#">
        /// 输出结果：
        /// <br/>11 B
        /// <br/>12.68 KB
        /// <br/>1.65 MB
        /// <br/>1.42 GB
        /// <br/>86.76 TB
        /// </para>
        /// </example>
        public static string ConvertToFileSize(long size)
        {
            if (size >= FileLengthEB)
            {
                return (((double)size) / ((double)FileLengthEB)).ToString("0.##") + " EB";
            }
            else if (size >= FileLengthPB)
            {
                return (((double)size) / ((double)FileLengthPB)).ToString("0.##") + " PB";
            }
            else if (size >= FileLengthTB)
            {
                return (((double)size) / ((double)FileLengthTB)).ToString("0.##") + " TB";
            }
            else if (size >= FileLengthGB)
            {
                return (((double)size) / ((double)FileLengthGB)).ToString("0.##") + " GB";
            }
            else if (size >= FileLengthMB)
            {
                return (((double)size) / ((double)FileLengthMB)).ToString("0.##") + " MB";
            }
            else if (size >= FileLengthKB)
            {
                return (((double)size) / ((double)FileLengthKB)).ToString("0.##") + " KB";
            }
            else
            {
                return size.ToString() + " B";
            }
        }

        /// <summary>
        /// 将带文件计算机单位（“EB”、“PB”、“TB”、“GB”、“MB”、“KB”、“B”）的文件尺寸描述形式转换为以 B 为单位的基础值。
        /// </summary>
        /// <param name="size">表示文件大小的带有单位的字符串。</param>
        /// <returns></returns>
        public static long ConvertFileSizeToByte(string size)
        {
            if (size.EndsWith("EB"))
            {
                return System.Convert.ToInt64(System.Convert.ToDouble(size.Substring(0, size.Length - 2).TrimEnd().Replace(",", "")) * FileLengthEB);
            }
            else if (size.EndsWith("PB"))
            {
                return System.Convert.ToInt64(System.Convert.ToDouble(size.Substring(0, size.Length - 2).TrimEnd().Replace(",", "")) * FileLengthPB);
            }
            else if (size.EndsWith("TB"))
            {
                return System.Convert.ToInt64(System.Convert.ToDouble(size.Substring(0, size.Length - 2).TrimEnd().Replace(",", "")) * FileLengthTB);
            }
            else if (size.EndsWith("GB"))
            {
                return System.Convert.ToInt64(System.Convert.ToDouble(size.Substring(0, size.Length - 2).TrimEnd().Replace(",", "")) * FileLengthGB);
            }
            else if (size.EndsWith("MB"))
            {
                return System.Convert.ToInt64(System.Convert.ToDouble(size.Substring(0, size.Length - 2).TrimEnd().Replace(",", "")) * FileLengthMB);
            }
            else if (size.EndsWith("KB"))
            {
                return System.Convert.ToInt64(System.Convert.ToDouble(size.Substring(0, size.Length - 2).TrimEnd().Replace(",", "")) * FileLengthKB);
            }
            else if (size.EndsWith("B"))
            {
                return System.Convert.ToInt64(size.Substring(0, size.Length - 1).TrimEnd().Replace(",", ""));// * FileLengthB
            }
            return System.Convert.ToInt64(size);
        }
        #endregion

        /// <summary>
        /// 生成一个由字母、数字和符号字符组成的密码。
        /// </summary>
        /// <param name="passwordLength">生成的密码长度。</param>
        /// <returns>生成的密码字符串。</returns>
        public static string GeneratePassword(int passwordLength)
        {
            string PasswordSeed = "abcdefghijklmnpqrstuvwxyz"//排除小写字符o
                + "ABCDEFGHIJKLMNPQRSTUVWXYZ"//排除大写字符O
                + "123456789"//排除数字0
                + "!@#$&_|?";

            System.Random rand = new System.Random();
            string result = "";
            for (int i = 0; i < passwordLength; i++)
            {
                result += PasswordSeed[rand.Next(PasswordSeed.Length - 1)];
            }
            return result;

        }

        /// <summary>
        /// 生成一个密码，密码内容取决于 PasswordSeed。
        /// </summary>
        /// <param name="passwordLength">生成的密码长度。</param>
        /// <param name="PasswordSeed">密码字符种子列表。</param>
        /// <returns>生成的密码字符串。</returns>
        public static string GeneratePassword(int passwordLength, string PasswordSeed)
        {
            System.Random rand = new System.Random();
            string result = "";
            for (int i = 0; i < passwordLength; i++)
            {
                result += PasswordSeed[rand.Next(PasswordSeed.Length - 1)];
            }
            return result;

        }

        /// <summary>
        /// 获取路径2相对于路径1的相对路径
        /// </summary>
        /// <param name="strPath1">路径1</param>
        /// <param name="strPath2">路径2</param>
        /// <returns>返回路径2相对于路径1的路径</returns>
        /// <example>
        /// string strPath = GetRelativePath(@"C:\WINDOWS\system32", @"C:\WINDOWS\system\*.*" );
        /// //strPath == @"..\system\*.*"
        /// </example>
        public static string GetRelativePath(string strPath1, string strPath2)
        {
            if (!strPath1.EndsWith("\\") && !strPath1.EndsWith("/")) strPath1 += "\\";    //如果不是以"\"结尾的加上"\"
            int intIndex = -1;

            //以"\"为分界比较从开始处到第一个"\"处对两个地址进行比较,如果相同则扩展到
            //下一个"\"处;直到比较出不同或第一个地址的结尾.
            for (int i = 0; i < strPath1.Length && i < strPath2.Length; i++)
            {
                char ch1 = strPath1[i];
                char ch2 = strPath2[i];
                if ((ch1 == '\\' || ch1 == '/') && (ch2 == '\\' || ch2 == '/'))
                {
                    intIndex = i;
                }
                else if (char.ToLower(ch1) != char.ToLower(ch2))
                {
                    break;
                }
            }

            //如果从不是第一个"\"处开始有不同,则从最后一个发现有不同的"\"处开始将strPath2
            //的后面部分付值给自己,在strPath1的同一个位置开始望后计算每有一个"\"则在strPath2
            //的前面加上一个"..\"(经过转义后就是"..\\").
            if (intIndex >= 0)
            {
                strPath2 = strPath2.Substring(intIndex + 1);
                for (int i = intIndex + 1; i < strPath1.Length; i++)
                {
                    char ch1 = strPath1[i];
                    if (ch1 == '\\' || ch1 == '/')
                    {
                        strPath2 = "..\\" + strPath2;
                    }
                }
            }

            //否则直接返回strPath2
            return strPath2;
        }

        /// <summary>
        /// 比较两个数组中的元素是否相同。
        /// </summary>
        /// <param name="a">第一个数组。</param>
        /// <param name="b">第二个数组。</param>
        /// <returns>全为 null 返回 true；只有一个为 null 返回 false；元素相同返回 true；否则返回 false。</returns>
        public static bool CompareArray(byte[] a, byte[] b)
        {
            if (a == null && b == null)
            {
                return true;
            }
            else if (a == null || b == null)
            {
                return false;
            }

            if (a.Length != b.Length) return false;
            for (int i = 0; i < a.Length; i++)
            {
                if (a[i] != b[i])
                {
                    return false;
                }
            }
            return true;

        }

        #region 校验码。
        /// <summary>
        /// 计算数据流的 SHA1 值。从指定流的当前位置开始计算。
        /// </summary>
        /// <param name="stream">提供数据的流。</param>
        /// <returns>计算得出的 SHA1 值。</returns>
        public static byte[] GetSHA1(System.IO.Stream stream)
        {
            byte[] b;
            System.Security.Cryptography.SHA1 sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            try
            {
                b = sha1.ComputeHash(stream);
            }
            finally
            {
                sha1.Clear();
            }
            return b;

        }

        /// <summary>
        /// 计算数据流的 SHA1 值。
        /// </summary>
        /// <param name="stream">提供数据的流。</param>
        /// <param name="startPosition">指示从指定的位置开始读取数据计算 SHA1 值。</param>
        /// <returns>计算得出的 SHA1 值。</returns>
        public static byte[] GetSHA1(System.IO.Stream stream, long startPosition)
        {
            long oldP = stream.Position;
            byte[] b;
            System.Security.Cryptography.SHA1 sha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            try
            {
                stream.Position = startPosition;
                b = sha1.ComputeHash(stream);
            }
            finally
            {
                sha1.Clear();
                stream.Position = oldP;
            }
            return b;

        }

        /// <summary>
        /// 计算文件的 SHA1 值。
        /// </summary>
        /// <param name="filePath">文件。</param>
        /// <returns>计算得出的文件 SHA1 值。</returns>
        public static byte[] GetSHA1(string filePath)
        {
            byte[] b;
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            try
            {
                b = GetSHA1(fs);
            }
            finally
            {
                fs.Close();
            }
            return b;

        }

        /// <summary>
        /// 计算数据流的 MD5 值。从指定流的当前位置开始计算。
        /// </summary>
        /// <param name="stream">提供数据的流。</param>
        /// <returns>计算得出的 MD5 值。</returns>
        public static byte[] GetMD5(System.IO.Stream stream)
        {
            byte[] b;
            System.Security.Cryptography.MD5 m = new System.Security.Cryptography.MD5CryptoServiceProvider();
            try
            {
                b = m.ComputeHash(stream);
            }
            finally
            {
                m.Clear();
            }
            return b;

        }

        /// <summary>
        /// 计算数据流的 MD5 值。
        /// </summary>
        /// <param name="stream">提供数据的流。</param>
        /// <param name="startPosition">指示从指定的位置开始读取数据计算 MD5 值。</param>
        /// <returns>计算得出的 MD5 值。</returns>
        public static byte[] GetMD5(System.IO.Stream stream, long startPosition)
        {
            long oldP = stream.Position;
            byte[] b;
            System.Security.Cryptography.MD5 m = new System.Security.Cryptography.MD5CryptoServiceProvider();
            try
            {
                stream.Position = startPosition;
                b = m.ComputeHash(stream);
            }
            finally
            {
                m.Clear();
                stream.Position = oldP;
            }
            return b;

        }

        /// <summary>
        /// 计算文件的 MD5 值。
        /// </summary>
        /// <param name="filePath">文件。</param>
        /// <returns>计算得出的文件 MD5 值。</returns>
        public static byte[] GetMD5(string filePath)
        {
            byte[] b;
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            try
            {
                b = GetMD5(fs);
            }
            finally
            {
                fs.Close();
            }
            return b;

        }
        #endregion

        /// <summary>
        /// byte 数组转 16 进制字符串。
        /// </summary>
        /// <param name="bytes">一个 byte 数组。</param>
        /// <returns>16 进制字符串。</returns>
        public static string Bytes2HexString(byte[] bytes)
        {
            string r = "";
            foreach (byte tmp in bytes)
            {
                r += tmp.ToString("X2");
            }
            return r;
        }

        /// <summary>
        /// 将 16 进制字符串转为 byte 数组。
        /// </summary>
        /// <param name="hexStr">16 进制字符串。</param>
        /// <returns>byte 数组。</returns>
        public static byte[] HexString2Bytes(string hexStr)
        {
            string strTemp = "";
            byte[] b = new byte[hexStr.Length / 2];
            for (int i = 0; i < hexStr.Length / 2; i++)
            {
                strTemp = hexStr.Substring(i * 2, 2);
                b[i] = System.Convert.ToByte(strTemp, 16);
            }
            return b;
        }

        /// <summary>
        /// 构造方法。
        /// </summary>
        static General()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //

        }

    }
}
