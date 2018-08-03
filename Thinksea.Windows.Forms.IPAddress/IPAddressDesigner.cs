using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms.Design.Behavior;
using System.Collections;

namespace Thinksea.Windows.Forms.IPAddress
{
    internal class IPAddressDesigner : ControlDesigner
    {
        // Properties
        public override SelectionRules SelectionRules
        {
            get
            {
                if (this.Control.AutoSize)
                {
                    return (SelectionRules.Visible | SelectionRules.Moveable);
                }
                return (SelectionRules.Visible | SelectionRules.Moveable | SelectionRules.AllSizeable);
            }
        }

        public override IList SnapLines
        {
            get
            {
                IPAddress control = (IPAddress)this.Control;
                IList snapLines = base.SnapLines;
                snapLines.Add(new SnapLine(SnapLineType.Baseline, control.Baseline));
                return snapLines;
            }
        }
    }


}
