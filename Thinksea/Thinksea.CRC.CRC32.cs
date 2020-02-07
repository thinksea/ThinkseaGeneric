namespace Thinksea.CRC
{
    /// <summary>
    /// 提供 CRC 冗余效验算法。
    /// </summary>
    /// <remarks>
    /// 这个类完全符合 CLS，除非另外注明。
    /// 它将对 System.UInt32, System.Int64, 和 System.Int32 这些类型进行操作。
    /// 所有数据在内部使用 UInt32 变量表示。
    /// </remarks>
    public sealed class CRC32
    {
        /// <summary>
        /// CRC 表的大小( 256 )。
        /// </summary>
        private const int TABLE_SIZE = 256;
        /// <summary>
        /// 标准的 CRC32 算法的多项式。
        /// </summary>
        private const uint STANDARD_POLYNOMIAL = 0xedb88320;
        private bool _Initialized;
        /// <summary>
        /// 获取一个值，指示 CRC 表是否已经初始化。
        /// </summary>
        public bool Initialized
        {
            get
            {
                return _Initialized;
            }
        }
        /// <summary>
        /// 提供在检查期间使用的 CRC 多项式的内部存储。
        /// </summary>
        private uint _Polynomial = STANDARD_POLYNOMIAL;
        /// <summary>
        /// 获取当前使用的多项式。
        /// </summary>
        /// <remarks>
        /// 这个属性不符合 CLS。
        /// </remarks>
        public uint Polynomial
        {
            get
            {
                return _Polynomial;
            }
        }

        /// <summary>
        /// 存储 CRC 表。
        /// </summary>
        private uint[] table = new uint[TABLE_SIZE];

        /// <summary>
        /// 一个构造方法。用标准的 CRC 多项式初始化 CRC 表。
        /// </summary>
        public CRC32()
        {
            //table = new uint[TABLE_SIZE];
            //_Polynomial = STANDARD_POLYNOMIAL;
            Init();
        }

        /// <summary>
        /// 一个构造方法。用指定的 CRC 多项式初始化 CRC 表。
        /// </summary>
        /// <remarks>
        /// 一个初始化变量的方法，在你允许调用 Crc32 方法之前必须调用此方法。
        /// 用指定的多项式初始化 CRC 表。
        /// </remarks>
        /// <param name="polynomial">初始化表时使用的多项式。</param>
        public CRC32(int polynomial)
        {
            this.Init(polynomial);
        }

        /// <summary>
        /// 一个构造方法。用指定的 CRC 多项式初始化 CRC 表。
        /// </summary>
        /// <remarks>
        /// 一个初始化变量的方法，在你允许调用 Crc32 方法之前必须调用此方法。
        /// 用指定的多项式初始化 CRC 表。
        /// </remarks>
        /// <param name="polynomial">初始化表时使用的多项式。</param>
        public CRC32(long polynomial)
        {
            this.Init(polynomial);
        }

        /// <summary>
        /// 一个构造方法。用指定的 CRC 多项式初始化 CRC 表。
        /// </summary>
        /// <remarks>
        /// 一个初始化变量的方法，在你允许调用 Crc32 方法之前必须调用此方法。
        /// 用指定的多项式初始化 CRC 表。
        /// </remarks>
        /// <param name="polynomial">初始化表时使用的多项式。</param>
        public CRC32(uint polynomial)
        {
            this.Init(polynomial);
        }


        /// <summary>
        /// 用标准的 CRC 多项式初始化 CRC 表。
        /// </summary>
        public void Init()
        {
            Init(STANDARD_POLYNOMIAL);
        }

        /// <summary>
        /// 用指定的 CRC 多项式初始化 CRC 表。
        /// </summary>
        /// <remarks>
        /// 一个初始化变量的方法，在你允许调用 Crc32 方法之前必须调用此方法。
        /// 用指定的多项式初始化 CRC 表。
        /// </remarks>
        /// <param name="polynomial">初始化表时使用的多项式。</param>
        public void Init(int polynomial)
        {
            // 使用 BitConverter 类转换有符号值为无符号值，因为我们不想引发异常。
            byte[] intBytes = System.BitConverter.GetBytes(polynomial);
            uint poly = System.BitConverter.ToUInt32(intBytes, 0);
            // 既然我们获得了无符号表示，用它来初始化这个类。
            Init(poly);
        }

        /// <summary>
        /// 用指定的 CRC 多项式初始化 CRC 表。
        /// </summary>
        /// <remarks>
        /// 一个初始化变量的方法，在你允许调用 Crc32 方法之前必须调用此方法。
        /// 用指定的多项式初始化 CRC 表。
        /// </remarks>
        /// <param name="polynomial">初始化表时使用的多项式。</param>
        public void Init(long polynomial)
        {
            const uint Mask = 0xffffffff;
            uint poly = (uint)(polynomial & Mask);
            Init(poly);
        }

        /// <summary>
        /// 用指定的 CRC 多项式初始化 CRC 表。
        /// </summary>
        /// <remarks>
        /// 一个初始化变量的方法，在你允许调用 Crc32 方法之前必须调用此方法。
        /// 用指定的多项式初始化 CRC 表。
        /// </remarks>
        /// <param name="polynomial">初始化表时使用的多项式。</param>
        public void Init(uint polynomial)
        {
            _Polynomial = polynomial;
            // 计数器
            uint i = 0, j = 0;
            // 创建表的变量
            uint dwCRC = 0;

            for (i = 0; i < TABLE_SIZE; i++)
            {
                dwCRC = i;
                for (j = 8; j > 0; j--)
                {
                    if ((dwCRC & 1) != 0)
                    {
                        dwCRC = (dwCRC >> 1) ^ _Polynomial;
                    }
                    else
                    {
                        dwCRC >>= 1;
                    }
                }
                table[i] = dwCRC;
            }

            _Initialized = true;
        }


        /// <summary>
        /// 计算 bytes 类型数组的循环冗余效验。
        /// </summary>
        /// <param name="data">检查的数据。</param>
        /// <param name="offset">data 中的字节偏移量，从此处开始读取。</param>
        /// <param name="count">最多读取的字节数。</param>
        /// <returns>
        /// 一个4比特数组存贮的 CRC 数据。
        /// </returns>
        /// <remarks>
        /// 使用 System.BitConverter 类转换这个字节数组为有效格式。
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">如果 CRC 表没被初始化（调用 Crc32 重载之前调用一个Init()重载方法）则引发此异常。</exception>
        /// <exception cref="System.ArgumentOutOfRangeException">参数 offset 超出数组 data 最大索引范围。</exception>
        public byte[] Crc32(byte[] data, int offset, int count)
        {
            if (!_Initialized)
            {
                throw new System.InvalidOperationException("在试图进行数据效验之前必须初始化 CRC 表。");
            }
            if (offset >= data.Length)
            {
                throw new System.ArgumentOutOfRangeException("参数 offset 超出数组最大索引范围。");
            }

            // 一个用于存放返回值的数组。
            byte[] crc32_result;
            // 存储返回值
            uint _result = 0;
            //一个长度标识符
            int len = offset + count;
            if (len > data.Length)
            {
                len = data.Length;
            }
            // 计数器
            int i = 0;

            const uint tableIndexMask = 0xff;

            uint dwCRC32 = 0xffffffff;
            for (i = offset; i < len; i++)
            {
                dwCRC32 = (dwCRC32 >> 8) ^ table[(uint)data[i] ^ (dwCRC32 & tableIndexMask)];
            }

            _result = dwCRC32 ^ 0xffffffff;

            crc32_result = System.BitConverter.GetBytes(_result);
            return crc32_result;
        }

        /// <summary>
        /// 计算 bytes 类型数组的循环冗余效验。
        /// </summary>
        /// <param name="data">检查的数据。</param>
        /// <returns>
        /// 一个4比特数组存贮的 CRC 数据。
        /// </returns>
        /// <remarks>
        /// 使用 System.BitConverter 类转换这个字节数组为有效格式。
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">如果 CRC 表没被初始化（调用 Crc32 重载之前调用一个Init()重载方法）则引发此异常。</exception>
        public byte[] Crc32(byte[] data)
        {
            if (!_Initialized)
            {
                throw new System.InvalidOperationException("在试图进行数据效验之前必须初始化 CRC 表。");
            }
            // 一个用于存放返回值的数组。
            byte[] crc32_result;
            // 存储返回值
            uint _result = 0;
            //一个长度标识符
            int len = data.Length;
            // 计数器
            int i = 0;

            const uint tableIndexMask = 0xff;

            uint dwCRC32 = 0xffffffff;
            for (i = 0; i < len; i++)
            {
                dwCRC32 = (dwCRC32 >> 8) ^ table[(uint)data[i] ^ (dwCRC32 & tableIndexMask)];
            }

            _result = dwCRC32 ^ 0xffffffff;

            crc32_result = System.BitConverter.GetBytes(_result);
            return crc32_result;
        }
        /// <summary>
        /// 计算 string 类型数据的循环冗余效验。使用 ASCII 格式。
        /// </summary>
        /// <param name="data">检查的字符串。</param>
        /// <returns>一个4比特数组存贮的 CRC 数据。</returns>
        /// <remarks>
        /// 使用 System.BitConverter 类转换这个字节数组为有效格式。
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">如果 CRC 表没被初始化（调用 Crc32 重载之前调用一个Init()重载方法）则引发此异常。</exception>
        public byte[] Crc32(string data)
        {
            return Crc32(data, System.Text.Encoding.ASCII);
        }

        /// <summary>
        /// 计算 string 类型数据的循环冗余效验。使用指定的编码方案。
        /// </summary>
        /// <param name="data">检查的字符串。</param>
        /// <param name="encoding">使用的编码方案。</param>
        /// <returns>一个4比特数组存贮的 CRC 数据。</returns>
        /// <remarks>
        /// 使用 System.BitConverter 类转换这个字节数组为有效格式。
        /// </remarks>
        /// <exception cref="System.InvalidOperationException">如果 CRC 表没被初始化（调用 Crc32 重载之前调用一个Init()重载方法）则引发此异常。</exception>
        public byte[] Crc32(string data, System.Text.Encoding encoding)
        {
            byte[] encData = encoding.GetBytes(data);
            return Crc32(encData);
        }
        /// <summary>
        /// 计算一个文件数据的循环冗余效验。使用指定的编码方案。
        /// </summary>
        /// <param name="file">文件。</param>
        /// <param name="buffLength">允许使用的缓冲区大小。</param>
        /// <returns></returns>
        /// <remarks>
        /// 计算循环冗余效验码时文件长度也在计算之内。
        /// </remarks>
        public byte[] Crc32OfFile(string file, int buffLength)
        {
            const int CRCLength = 4;//定义 CRC 长度。
            byte[] crc = null;
            byte[] buff = new byte[CRCLength + buffLength];
            int rl = -1;
            System.IO.FileStream fs = new System.IO.FileStream(file, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read);
            try
            {
                while (fs.Position < fs.Length)
                {
                    rl = fs.Read(buff, CRCLength, buffLength);//从文件读取数据存放到 CRC 空间后。
                    if (rl > 0)
                    {
                        if (crc == null)
                        {
                            crc = this.Crc32(buff, CRCLength, rl);
                        }
                        else
                        {
                            for (int i = 0; i < CRCLength; i++)//将上次获取的 CRC 复制到 buff 开始位置。
                            {
                                buff[i] = crc[i];
                            }
                            crc = this.Crc32(buff, 0, CRCLength + rl);
                        }
                    }
                }
            }
            finally
            {
                fs.Close();
            }

            ;
            System.IO.FileInfo fi = new System.IO.FileInfo(file);
            long fileLength = fi.Length;

            if (crc == null)
            {
                return this.Crc32(Thinksea.General.Concat(this.Crc32(new byte[0]), System.Text.Encoding.ASCII.GetBytes(fileLength.ToString())));
            }
            else
            {
                return this.Crc32(Thinksea.General.Concat(crc, System.Text.Encoding.ASCII.GetBytes(fileLength.ToString())));
            }

        }

        /// <summary>
        /// 计算一个文件数据的循环冗余效验。使用指定的编码方案。
        /// </summary>
        /// <param name="file">文件。</param>
        /// <returns></returns>
        /// <remarks>
        /// 计算循环冗余效验码时文件长度也在计算之内。
        /// </remarks>
        public byte[] Crc32OfFile(string file)
        {
            return this.Crc32OfFile(file, 1024 * 8);
        }

    }


}
