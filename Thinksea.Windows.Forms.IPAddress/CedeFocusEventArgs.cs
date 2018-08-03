using System;
using System.Collections.Generic;
using System.Text;

namespace Thinksea.Windows.Forms.IPAddress
{
    internal class CedeFocusEventArgs : EventArgs
    {
        // Fields
        private Direction _direction;
        private int _fieldId;
        private Selection _selection;

        // Properties
        public Direction Direction
        {
            get
            {
                return this._direction;
            }
            set
            {
                this._direction = value;
            }
        }

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

        public Selection Selection
        {
            get
            {
                return this._selection;
            }
            set
            {
                this._selection = value;
            }
        }
    }

}
