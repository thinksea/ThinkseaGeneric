using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.Forms.IPAddress
{
    internal class SpecialKeyEventArgs : EventArgs
    {
        // Fields
        private int _fieldId;
        private Keys _keyCode;

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

        public Keys KeyCode
        {
            get
            {
                return this._keyCode;
            }
            set
            {
                this._keyCode = value;
            }
        }
    }


}
