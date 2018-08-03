using System;
using System.Collections.Generic;
using System.Text;

namespace Thinksea.Windows.Forms.IPAddress
{
    /// <summary>
    /// 字段更改事件参数。
    /// </summary>
    public class FieldChangedEventArgs : EventArgs
    {
        // Fields
        private int _fieldId;
        private string _text;

        /// <summary>
        /// 字段编号。
        /// </summary>
        public int FieldId
        {
            get
            {
                return this._fieldId;
            }
            set
            {
                this._fieldId = value;
            }
        }

        /// <summary>
        /// 文本。
        /// </summary>
        public string Text
        {
            get
            {
                return this._text;
            }
            set
            {
                this._text = value;
            }
        }
    }


}
