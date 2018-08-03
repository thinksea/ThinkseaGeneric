using System;
using System.Collections.Generic;
using System.Text;

namespace Thinksea.Windows.Forms
{
    /// <summary>
    /// 定义 System.Windows.Forms.DataGridView 控件中的列。
    /// </summary>
    public class DataGridViewComboxTreeColumn : System.Windows.Forms.DataGridViewColumn
    {
        /// <summary>
        /// 指示是否只允许选中叶节点。
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Description("指示是否只允许选中叶节点")]
        public bool SelectLeafNodeOnly
        {
            get
            {
                return this.ComboxTreeCellTemplate.SelectLeafNodeOnly;
            }
            set
            {
                this.ComboxTreeCellTemplate.SelectLeafNodeOnly = value;
            }
        }
        /// <summary>
        /// 树视图中的节点集合。
        /// </summary>
        public System.Collections.Generic.List<System.Windows.Forms.TreeNode> Nodes
        {
            get
            {
                return this.ComboxTreeCellTemplate.Nodes;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public DataGridViewComboxTreeColumn()
            : base(new DataGridViewComboxTreeCell())
        {
        }

        /// <summary>
        /// 获取或设置用于创建新单元格的模板。
        /// </summary>
        /// <value>一个 System.Windows.Forms.DataGridViewCell，列中的所有其他单元格都以它为模型。默认为 null。</value>
        public override System.Windows.Forms.DataGridViewCell CellTemplate
        {
            get
            {
                return base.CellTemplate;
            }
            set
            {
                //   Ensure that the cell used for the template is a DataGridViewComboxTreeCell. 
                if (value != null && !value.GetType().IsAssignableFrom(typeof(DataGridViewComboxTreeCell)))
                {
                    throw new InvalidCastException("Must be a DataGridViewComboxTreeCell ");
                }
                base.CellTemplate = value;
            }
        }
        private DataGridViewComboxTreeCell ComboxTreeCellTemplate
        {
            get
            {
                return (DataGridViewComboxTreeCell)this.CellTemplate;
            }
        }
    }

    /// <summary>
    /// 显示 System.Windows.Forms.DataGridView 控件中的下拉列表树。
    /// </summary>
    public class DataGridViewComboxTreeCell : System.Windows.Forms.DataGridViewTextBoxCell
    {
        private bool _SelectLeafNodeOnly = false;
        /// <summary>
        /// 指示是否只允许选中叶节点。
        /// </summary>
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Description("指示是否只允许选中叶节点")]
        public bool SelectLeafNodeOnly
        {
            get
            {
                return this._SelectLeafNodeOnly;
            }
            set
            {
                this._SelectLeafNodeOnly = value;
            }
        }
        private System.Collections.Generic.List<System.Windows.Forms.TreeNode> _Nodes = null;
        /// <summary>
        /// 树视图中的节点集合。
        /// </summary>
        public System.Collections.Generic.List<System.Windows.Forms.TreeNode> Nodes
        {
            get
            {
                if (this._Nodes == null)
                {
                    this._Nodes = new List<System.Windows.Forms.TreeNode>();
                }
                return this._Nodes;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public DataGridViewComboxTreeCell()
            : base()
        {
        }

        /// <summary>
        /// 附加并初始化寄宿的编辑控件。
        /// </summary>
        /// <param name="rowIndex">所编辑的行的索引。</param>
        /// <param name="initialFormattedValue">要在控件中显示的初始值。</param>
        /// <param name="dataGridViewCellStyle">用于确定寄宿控件外观的单元格样式。</param>
        public override void InitializeEditingControl(int rowIndex, object initialFormattedValue, System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle)
        {
            //   Set   the   value   of   the   editing   control   to   the   current   cell   value. 
            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);
            if (this.RowIndex == -1) return;
            if (this.OwningColumn is DataGridViewComboxTreeColumn)
            {
                DataGridViewComboxTreeEditingControl ctl = this.DataGridView.EditingControl as DataGridViewComboxTreeEditingControl;
                ctl.SuspendLayout();
                ctl.SelectLeafNodeOnly = this.SelectLeafNodeOnly;
                ctl.Nodes.Clear();
                ctl.Nodes.AddRange(this.Nodes.ToArray());
                ctl.ResumeLayout(false);
                //ctl.PerformLayout();

                ctl.NodeName = System.Convert.ToString(this.Value);
            }
        }

        /// <summary>
        /// 获取单元格的寄宿编辑控件的类型。
        /// </summary>
        /// <value>表示 System.Windows.Forms.DataGridViewTextBoxEditingControl 类型的 System.Type。</value>
        public override Type EditType
        {
            get
            {
                return typeof(DataGridViewComboxTreeEditingControl);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <value>表示单元格中值的数据类型的 System.Type。</value>
        public override Type ValueType
        {
            get
            {
                return typeof(string);
            }
        }

        /// <summary>
        /// 获取新记录所在行中单元格的默认值。
        /// </summary>
        /// <value>表示默认值的 System.Object。</value>
        public override object DefaultNewRowValue
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// 创建此单元格的精确副本。
        /// </summary>
        /// <returns>一个 System.Object，表示克隆的 <see cref="DataGridViewComboxTreeCell"/>。</returns>
        public override object Clone()
        {
            object o = base.Clone();
            DataGridViewComboxTreeCell cell = o as DataGridViewComboxTreeCell;
            if (cell != null)
            {
                cell._SelectLeafNodeOnly = this.SelectLeafNodeOnly;
                cell._Nodes = this.Nodes;
            }
            return o;
        }

    }

    /// <summary>
    /// 表示 DataGridViewComboxTreeCell 中承载的下拉列表树控件。
    /// </summary>
    public class DataGridViewComboxTreeEditingControl : Thinksea.Windows.Forms.ComboTree, System.Windows.Forms.IDataGridViewEditingControl
    {
        System.Windows.Forms.DataGridView dataGridView;
        private bool valueChanged = false;
        int rowIndex;

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public DataGridViewComboxTreeEditingControl()
        {
            this.SelectLeafNodeOnly = true;
            this.GetNodeText += new Thinksea.Windows.Forms.GetNodeTextEventHandler(DataGridViewComboxTreeEditingControl_GetNodeText);
            this.AfterSelect += new EventHandler(DataGridViewComboxTreeEditingControl_AfterSelect);
        }

        string DataGridViewComboxTreeEditingControl_GetNodeText(object sender, Thinksea.Windows.Forms.GetNodeTextEventArgs e)
        {
            return e.NodeName;
        }

        void DataGridViewComboxTreeEditingControl_AfterSelect(object sender, EventArgs e)
        {
            this.NotifyDataGridViewOfValueChange();
        }

        public object EditingControlFormattedValue
        {
            get
            {
                return this.NodeName;
            }
            set
            {
                String newValue = value as String;
                if (newValue != null)
                {
                    this.NodeName = newValue;
                    NotifyDataGridViewOfValueChange();
                }
            }
        }

        protected virtual void NotifyDataGridViewOfValueChange()
        {
            this.valueChanged = true;
            if (this.dataGridView != null)
            {
                this.dataGridView.NotifyCurrentCellDirty(true);
            }
        }

        public object GetEditingControlFormattedValue(System.Windows.Forms.DataGridViewDataErrorContexts context)
        {
            return this.EditingControlFormattedValue;
        }

        public void ApplyCellStyleToEditingControl(System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle)
        {
            this.Font = dataGridViewCellStyle.Font;
            this.ForeColor = dataGridViewCellStyle.ForeColor;
            this.BackColor = dataGridViewCellStyle.BackColor;
        }

        public int EditingControlRowIndex
        {
            get
            {
                return rowIndex;
            }
            set
            {
                rowIndex = value;
            }
        }

        public bool EditingControlWantsInputKey(System.Windows.Forms.Keys key, bool dataGridViewWantsInputKey)
        {
            switch (key & System.Windows.Forms.Keys.KeyCode)
            {
                case System.Windows.Forms.Keys.Left:
                case System.Windows.Forms.Keys.Up:
                case System.Windows.Forms.Keys.Down:
                case System.Windows.Forms.Keys.Right:
                case System.Windows.Forms.Keys.Home:
                case System.Windows.Forms.Keys.End:
                case System.Windows.Forms.Keys.PageDown:
                case System.Windows.Forms.Keys.PageUp:
                    return true;
                default:
                    return false;
            }
        }

        public void PrepareEditingControlForEdit(bool selectAll)
        {
        }

        public bool RepositionEditingControlOnValueChange
        {
            get
            {
                return true;
            }
        }

        public System.Windows.Forms.DataGridView EditingControlDataGridView
        {
            get
            {
                return dataGridView;
            }
            set
            {
                dataGridView = value;
            }
        }

        public bool EditingControlValueChanged
        {
            get
            {
                return valueChanged;
            }
            set
            {
                valueChanged = value;
            }
        }

        /// <summary>
        /// 获取或设置当鼠标指针位于控件上时显示的光标。
        /// </summary>
        /// <value>一个 System.Windows.Forms.Cursor，表示当鼠标指针位于控件上时显示的光标。</value>
        public System.Windows.Forms.Cursor EditingPanelCursor
        {
            get
            {
                return base.Cursor;
            }
        }

    }
}
