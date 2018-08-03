using System;
using System.Collections.Generic;
using System.Text;

namespace Thinksea.Windows.Forms.IPAddress
{
    internal class TextChangedEventArgs : EventArgs
    {
        // Fields
        private int _fieldId;
        private string _text;

        // Properties
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
