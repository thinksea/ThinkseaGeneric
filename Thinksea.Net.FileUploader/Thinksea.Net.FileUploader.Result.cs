namespace Thinksea.Net.FileUploader
{
    /// <summary>
    /// 封装了返回到客户端的数据格式标准。
    /// </summary>
    internal class Result
    {
        private int _ErrorCode = 0;
        /// <summary>
        /// 错误码。赋值为0时表示正确执行；否则表示出现了错误。
        /// </summary>
        public int ErrorCode
        {
            get
            {
                return this._ErrorCode;
            }
            set
            {
                this._ErrorCode = value;
            }
        }

        private string _Message = "";
        /// <summary>
        /// 获取或设置对于属性“ErrorCode”的友好文字描述。特别是当“ErrorCode”非0时，应该为其设置此属性。
        /// </summary>
        public string Message
        {
            get
            {
                return this._Message;
            }
            set
            {
                this._Message = value;
            }
        }

        private object _Data = null;
        /// <summary>
        /// 返回到客户端的用户自定义数据。
        /// </summary>
        public object Data
        {
            get
            {
                return this._Data;
            }
            set
            {
                this._Data = value;
            }
        }

    }

}
