using System;
using System.ComponentModel;

namespace Thinksea.Windows.Forms
{
    /// <summary>
    /// 扩展树视图控件。
    /// </summary>
    /// <remarks>
    /// 1、支持节点移动操作。
    /// 2、支持鼠标拖拽完成节点移动。
    /// 3、支持异步加载节点。
    /// 4、支持在准备编辑节点文本标签事件中设置待编辑文本。
    /// </remarks>
    public class TreeViewExpand : System.Windows.Forms.TreeView
    {
        private event NodeDragHandler _NodeDrag;
        /// <summary>
        /// 当开始拖动节点时引发此事件。
        /// </summary>
        [Description("当开始拖动节点时引发此事件。")]
        public event NodeDragHandler NodeDrag
        {
            add
            {
                this._NodeDrag += value;
            }
            remove
            {
                this._NodeDrag -= value;
            }
        }

        private event NodeDragOverHandler _NodeDragOver;
        /// <summary>
        /// 当节点被拖拽到新位置时引发此事件。
        /// </summary>
        [Description("当节点被拖拽到新位置时引发此事件。")]
        public event NodeDragOverHandler NodeDragOver
        {
            add
            {
                this._NodeDragOver += value;
            }
            remove
            {
                this._NodeDragOver -= value;
            }
        }

        private event NodeMovingHandler _NodeMoving;
        /// <summary>
        /// 当准备移动节点时引发此事件。
        /// </summary>
        [Description("当准备移动节点时引发此事件。")]
        public event NodeMovingHandler NodeMoving
        {
            add
            {
                this._NodeMoving += value;
            }
            remove
            {
                this._NodeMoving -= value;
            }
        }

        private event NodeMovedEventHandler _NodeMoved;
        /// <summary>
        /// 当移动节点完成时引发此事件。
        /// </summary>
        [Description("当移动节点完成时引发此事件。")]
        public event NodeMovedEventHandler NodeMoved
        {
            add
            {
                this._NodeMoved += value;
            }
            remove
            {
                this._NodeMoved -= value;
            }
        }

        private bool _SelectChildNodeOnly = false;
        /// <summary>
        /// 指示是否只允许选中叶节点。
        /// </summary>
        [DefaultValue(false)]
        [Description("指示是否只允许选中叶节点")]
        public bool SelectLeafNodeOnly
        {
            get
            {
                return this._SelectChildNodeOnly;
            }
            set
            {
                this._SelectChildNodeOnly = value;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public TreeViewExpand()
            : base()
        {
            this.LoadingText = "正在加载节点…";
            this.AutoLoadRootNodes = true;
            this.LoadNodeMode = eLoadNodeMode.Async;

            this.DisableNodeForeColor = System.Drawing.Color.Black;
            this.DisableNodeBackColor = System.Drawing.Color.White;

            this.EnableNodeForeColor = System.Drawing.Color.Black;
            this.EnableNodeBackColor = System.Drawing.Color.LightGray;

        }

        /// <summary>
        /// 当更改控件的可见状态时发生。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnVisibleChanged(EventArgs e)
        {
            if (this.Visible && this.AutoLoadRootNodes && this.Nodes.Count == 0)
            {
                this.LoadRootNodes(); //加载根节点。

            }

            base.OnVisibleChanged(e);
        }

        /// <summary>
        /// 判断是否允许的移动规则。
        /// </summary>
        /// <param name="node">待移动节点。</param>
        /// <param name="parentNode">目的父级节点。设置为 null 时返回 false。</param>
        /// <returns>符合移动规则返回 true；否则返回 false。</returns>
        /// <remarks>
        /// 当 parentNode 是 node 的一个子节点时禁止移动。
        /// 当两个节点为同一节点时禁止移动。
        /// </remarks>
        private bool AllowMove(System.Windows.Forms.TreeNode node, System.Windows.Forms.TreeNode parentNode)
        {
            if (node == null)
            {
                throw new System.ArgumentNullException(nameof(node), "参数“node”不能为 null。");
            }
            if (parentNode == null)
            {
                return true;
            }
            bool r = false;
            System.Windows.Forms.TreeNode tmpNode = parentNode;
            while (tmpNode != null)
            {
                if (tmpNode == node)
                {
                    r = true;
                    break;
                }
                tmpNode = tmpNode.Parent;
            }
            return !r;

        }

        /// <summary>
        /// 移动节点。
        /// </summary>
        /// <param name="node">待移动节点。</param>
        /// <param name="parentNode">移动到这里的新的父级节点。如果设置为 null，则表示将 node 设置为根节点。</param>
        /// <param name="insertIndex">位于新位置的插入索引。</param>
        public void MoveNode(System.Windows.Forms.TreeNode node, System.Windows.Forms.TreeNode parentNode, int insertIndex)
        {
            if (!this.AllowMove(node, parentNode)) //如果不符合移动规则则禁止移动。
            {
                throw new System.Exception("不符合树节点移动规则。");
            }

            if (node.Parent == parentNode && node.Index == insertIndex) //如果无需移动（节点移动后的位置无变化）则结束过程。
            {
                return;
            }

            System.Windows.Forms.TreeNode oldParent = node.Parent; //记录节点移动前的父级节点。
            if (this._NodeMoving != null) //引发节点移动事件。
            {
                NodeMovingEventArgs nmea = new NodeMovingEventArgs(node, oldParent, parentNode, insertIndex);
                this._NodeMoving(this, nmea);
                if (nmea.Cancel)
                {
                    return;
                }
            }
            ////////接受更改();

            bool 还原选择 = (this.SelectedNode == node); //确定是否需要维护当前选中节点的状态，即如果被移动的节点已经处于选中状态，则在节点移动到新位置后需要将其选中以保持其选中状态。
            if (parentNode == null) //如果没有设置目标节点，则作为根节点添加到末尾。
            {
                node.Remove();
                this.Nodes.Insert(insertIndex, node);
            }
            else
            {
                //if (movingNode.Parent != null)
                //{
                //    if (movingNode.Parent.Nodes.Count == 1)
                //    {
                //        movingNode.Parent.ImageIndex = 2;
                //        movingNode.Parent.SelectedImageIndex = 2;
                //    }
                //}

                //把被移动的节点作为这个节点的子节点
                node.Remove();
                parentNode.Nodes.Insert(insertIndex, node);
                //展开父节点
                parentNode.Expand();
                ////更改为展开后的图标
                //overNode.ImageIndex = 1;
            }
            if (还原选择)
            {
                this.SelectedNode = node;
            }

            if (this._NodeMoved != null) //引发节点移动完成事件。
            {
                NodeMovedEventArgs nmea = new NodeMovedEventArgs(node, oldParent, parentNode, insertIndex);
                this._NodeMoved(this, nmea);
            }

            ////////this.反向更新数据源(this.DataSource, this.Nodes);
            ////////更新界面();
        }

        #region 鼠标拖动节点。
        private bool _AllowDragNode = false;

        /// <summary>
        /// 获取或设置一个值，指示是否允许鼠标拖拽的操作方式移动节点位置。（注意：这需要将“AllowDrop”属性设置为 True。）
        /// </summary>
        [
        Description("指示是否允许鼠标拖拽的操作方式移动节点位置。（注意：这需要将“AllowDrop”属性设置为 True。）"),
        DefaultValue(false),
        ReadOnly(false),
        ]
        public bool AllowDragNode
        {
            get
            {
                return this._AllowDragNode;
            }
            set
            {
                this._AllowDragNode = value;
            }
        }

        /// <summary>
        /// 节点被拖拽到允许放置的节点的背景色。
        /// </summary>
        [
        Description("节点被拖拽到允许放置的节点的背景色。"),
        DefaultValue(typeof(System.Drawing.Color), "LightGray"),
        ReadOnly(false),
        ]
        public System.Drawing.Color EnableNodeBackColor
        {
            get;
            set;
        }
        /// <summary>
        /// 节点被拖拽到允许放置的节点的前景色。
        /// </summary>
        [
        Description("节点被拖拽到允许放置的节点的前景色。"),
        DefaultValue(typeof(System.Drawing.Color), "Black"),
        ReadOnly(false),
        ]
        public System.Drawing.Color EnableNodeForeColor
        {
            get;
            set;
        }
        /// <summary>
        /// 节点被拖拽到禁止放置的节点的背景色。
        /// </summary>
        [
        Description("节点被拖拽到禁止放置的节点的背景色。"),
        DefaultValue(typeof(System.Drawing.Color), "White"),
        ReadOnly(false),
        ]
        public System.Drawing.Color DisableNodeBackColor
        {
            get;
            set;
        }
        /// <summary>
        /// 节点被拖拽到禁止放置的节点的前景色。
        /// </summary>
        [
        Description("节点被拖拽到禁止放置的节点的前景色。"),
        DefaultValue(typeof(System.Drawing.Color), "Black"),
        ReadOnly(false),
        ]
        public System.Drawing.Color DisableNodeForeColor
        {
            get;
            set;
        }

        /// <summary>
        /// 用于指示是否正在执行节点拖放操作。
        /// </summary>
        private bool IsDrag = false;

        /// <summary>
        /// 用于记录被覆盖的节点。
        /// </summary>
        private System.Windows.Forms.TreeNode oldNode;
        /// <summary>
        /// 用于记录被覆盖的节点前景色。
        /// </summary>
        private System.Drawing.Color oldNodeForeColor;
        /// <summary>
        /// 用于记录被覆盖的节点背景色。
        /// </summary>
        private System.Drawing.Color oldNodeBackColor;

        /// <summary>
        /// 开始拖动项时发生。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnItemDrag(System.Windows.Forms.ItemDragEventArgs e)
        {
            if (this.AllowDragNode)
            {
                bool cancel = false;
                if (this._NodeDrag != null)
                {
                    NodeDragEventArgs ndea = new NodeDragEventArgs(e.Button, (System.Windows.Forms.TreeNode)e.Item);
                    this._NodeDrag(this, ndea);
                    if (ndea.Cancel)
                    {
                        cancel = ndea.Cancel;
                    }
                }
                if (!cancel)
                {
                    IsDrag = true;
                    //启动拖放操作
                    DoDragDrop(e.Item, System.Windows.Forms.DragDropEffects.Move);
                }
            }
            base.OnItemDrag(e);

        }

        /// <summary>
        /// 在用鼠标拖动某项时发生。系统请求是否允许拖放操作继续。
        /// </summary>
        /// <param name="qcdevent"></param>
        protected override void OnQueryContinueDrag(System.Windows.Forms.QueryContinueDragEventArgs qcdevent)
        {
            if (this.AllowDragNode)
            {
                if (qcdevent.Action == System.Windows.Forms.DragAction.Cancel || qcdevent.Action == System.Windows.Forms.DragAction.Drop) //节点拖放操作结束。
                {
                    //System.Diagnostics.Debug.WriteLine(qcdevent.Action.ToString());

                    #region 恢复被覆盖的节点数据。
                    if (oldNode != null)
                    {
                        //恢复上次鼠标位置处节点的颜色
                        oldNode.BackColor = oldNodeBackColor;
                        oldNode.ForeColor = oldNodeForeColor;
                        //把这个节点设为null
                        oldNode = null;
                    }
                    #endregion
                }
            }

            base.OnQueryContinueDrag(qcdevent);

        }

        /// <summary>
        /// 当拖放操作完成时发生。
        /// </summary>
        /// <param name="drgevent"></param>
        protected override void OnDragDrop(System.Windows.Forms.DragEventArgs drgevent)
        {
            if (this.AllowDragNode)
            {
                this.treeView1_DragDrop(this, drgevent);
            }
            base.OnDragDrop(drgevent);
        }
        private void treeView1_DragDrop(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //如果允许移动，则可以肯定这是可以接受的数据
            if (this.IsDrag)
            //if (e.Effect == DragDropEffects.Move)
            {
                //获取当前移动的节点
                System.Windows.Forms.TreeNode movingNode = (System.Windows.Forms.TreeNode)(e.Data.GetData(typeof(System.Windows.Forms.TreeNode)));
                //获取鼠标位置处的节点
                System.Windows.Forms.TreeNode overNode = this.GetNodeAt(this.PointToClient(new System.Drawing.Point(e.X, e.Y)));

                int newIndex; //对于目的节点与被拖拽节点的父节点为同一节点，则只是从当前索引位置移动到节点集合的最后索引位置。
                if (overNode == null) //如果目的节点为根节点。
                {
                    newIndex = (movingNode.Parent == overNode ? this.Nodes.Count - 1 : this.Nodes.Count);
                }
                else
                {
                    newIndex = (movingNode.Parent == overNode ? overNode.Nodes.Count - 1 : overNode.Nodes.Count);
                }

                this.MoveNode(movingNode, overNode, newIndex);

                IsDrag = false;
            }
        }

        /// <summary>
        /// 在鼠标将某项拖动到控件的工作区时发生。
        /// </summary>
        /// <param name="drgevent"></param>
        protected override void OnDragEnter(System.Windows.Forms.DragEventArgs drgevent)
        {
            if (this.AllowDragNode)
            {
                treeView1_DragEnter(this, drgevent);
            }
            base.OnDragEnter(drgevent);
        }
        private void treeView1_DragEnter(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //获取TreeNode格式的数据
            object data = e.Data.GetData(typeof(System.Windows.Forms.TreeNode));
            //如果数据类型为TreeNode
            if (data != null)
            {
                //把这个数据转换成TreeNode类型
                System.Windows.Forms.TreeNode dragedNode = (System.Windows.Forms.TreeNode)data;
                //检查它是否属于这个窗体内的treeView控件
                if (dragedNode.TreeView.Equals(this))
                {
                    //如果是，则可以实现移动拖放功能
                    e.Effect = System.Windows.Forms.DragDropEffects.Move;
                }
                else
                {
                    //否则，不接受这个数据
                    e.Effect = System.Windows.Forms.DragDropEffects.None;
                }
            }
            else
            {
                //如果数据类型不为TreeNode，则不接受这个数据
                e.Effect = System.Windows.Forms.DragDropEffects.None;
            }

        }

        /// <summary>
        /// 将对象拖过控件的边界时发生。
        /// </summary>
        /// <param name="drgevent"></param>
        protected override void OnDragOver(System.Windows.Forms.DragEventArgs drgevent)
        {
            if (this.AllowDragNode)
            {
                treeView1_DragOver(this, drgevent);
            }

            base.OnDragOver(drgevent);
        }
        private void treeView1_DragOver(object sender, System.Windows.Forms.DragEventArgs e)
        {
            //如果允许移动，则可以肯定这是可以接受的数据
            if (this.IsDrag)
            //if (e.Effect == DragDropEffects.Move)
            {
                //获取鼠标位置处的节点
                System.Windows.Forms.TreeNode overNode = this.GetNodeAt(this.PointToClient(new System.Drawing.Point(e.X, e.Y)));
                //如果鼠标位置处的节点已经改变（即：不是上一次鼠标位置处的节点）
                if (overNode != oldNode)
                {
                    //如果上一次鼠标位置处有节点，则恢复那个节点的颜色
                    if (oldNode != null)
                    {
                        oldNode.BackColor = oldNodeBackColor;
                        oldNode.ForeColor = oldNodeForeColor;
                    }
                    #region 保存被覆盖的节点数据
                    oldNode = overNode;
                    if (overNode != null)
                    {
                        //保存当前鼠标位置处节点的颜色
                        oldNodeBackColor = overNode.BackColor;
                        oldNodeForeColor = overNode.ForeColor;
                    }
                    #endregion

                    //获取当前移动的节点
                    System.Windows.Forms.TreeNode movingNode = (System.Windows.Forms.TreeNode)(e.Data.GetData(typeof(System.Windows.Forms.TreeNode)));
                    //根据当前鼠标位置处的节点是否为正被拖动的节点分别设置颜色
                    if (this.AllowMove(movingNode, overNode))
                    {
                        bool r = true;
                        if (this._NodeDragOver != null)
                        {
                            NodeDragOverEventArgs ndoea = new NodeDragOverEventArgs(movingNode, overNode);
                            this._NodeDragOver(this, ndoea);
                            if (!ndoea.AllowMove)
                            {
                                r = false;
                            }
                        }
                        if (r)
                        {
                            if (overNode != null)
                            {
                                overNode.BackColor = this.EnableNodeBackColor;
                                overNode.ForeColor = this.EnableNodeForeColor;
                            }
                            e.Effect = System.Windows.Forms.DragDropEffects.Move;
                            return;

                        }
                    }

                    if (overNode != null)
                    {
                        overNode.BackColor = this.DisableNodeBackColor;
                        overNode.ForeColor = this.DisableNodeForeColor;
                    }
                    e.Effect = System.Windows.Forms.DragDropEffects.None;

                }
            }

        }

        #endregion

        #region 节点移动。
        /// <summary>
        /// 将节点向上移动一个索引位置。
        /// </summary>
        /// <param name="node">被移动的节点。</param>
        public void MoveNodeToUp(System.Windows.Forms.TreeNode node)
        {
            System.Windows.Forms.TreeNode changeNode = node.PrevNode;
            if (changeNode == null)
            {
                return;
            }
            this.MoveNode(node, node.Parent, node.Index - 1); //移动到上一个节点前面。

        }

        /// <summary>
        /// 将节点向下移动一个索引位置。
        /// </summary>
        /// <param name="node">被移动的节点。</param>
        public void MoveNodeToDown(System.Windows.Forms.TreeNode node)
        {
            System.Windows.Forms.TreeNode changeNode = node.NextNode;
            if (changeNode == null)
            {
                return;
            }
            this.MoveNode(node, node.Parent, node.Index + 1); //移动到下一个节点后面。

        }

        /// <summary>
        /// 将节点向左移动一个索引位置。
        /// </summary>
        /// <param name="node">被移动的节点。</param>
        public void MoveNodeToLeft(System.Windows.Forms.TreeNode node)
        {
            System.Windows.Forms.TreeNode changeNode = node.Parent;
            if (changeNode == null)
            {
                return;
            }
            this.MoveNode(node, changeNode.Parent, changeNode.Index + 1); //移动到父级节点的下一个索引位置。

        }

        /// <summary>
        /// 将节点向右移动一个索引位置。
        /// </summary>
        /// <param name="node">被移动的节点。</param>
        public void MoveNodeToRight(System.Windows.Forms.TreeNode node)
        {
            System.Windows.Forms.TreeNode changeNode = node.PrevNode;
            if (changeNode == null)
            {
                return;
            }
            this.MoveNode(node, changeNode, changeNode.Nodes.Count); //移动到上一个节点的末尾。

        }

        #endregion

        //void SetPropertyReadOnly(object obj, string propertyName, bool readOnly)
        //{
        //    Type type = typeof(System.ComponentModel.ReadOnlyAttribute);
        //    PropertyDescriptorCollection props = TypeDescriptor.GetProperties(obj);
        //    AttributeCollection attrs = props[propertyName].Attributes;
        //    FieldInfo fld = type.GetField("isReadOnly", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.CreateInstance);
        //    fld.SetValue(attrs[type], readOnly);
        //}//使用方法：SetPropertyVisibility(obj, "名称", true); 

        #region 动态加载节点。
        private eLoadNodeMode _LoadNodeMode;
        /// <summary>
        /// 获取或设置节点加载模式。
        /// </summary>
        [
        Description("节点加载模式。"),
        DefaultValue(eLoadNodeMode.Async),
        ReadOnly(false),
            //System.ComponentModel.RefreshPropertiesAttribute(System.ComponentModel.RefreshProperties.All),
        ]
        public eLoadNodeMode LoadNodeMode
        {
            get
            {
                //SetPropertyReadOnly(this, "AutoLoadRootNodes", true); 
                //switch (this._LoadNodeMode)
                //{
                //    case eLoadNodeMode.Async:
                //    case eLoadNodeMode.Sync:
                //        SetPropertyReadOnly(this, "AutoLoadRootNodes", false);
                //        break;
                //    case eLoadNodeMode.UserCustom:
                //    default:
                //        SetPropertyReadOnly(this, "AutoLoadRootNodes", true);
                //        break;
                //}
                return this._LoadNodeMode;
            }
            set
            {
                this._LoadNodeMode = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值，指示是否由控件自动决定何时加载根节点。
        /// </summary>
        [
        Description("指示是否由控件自动决定何时加载根节点。"),
        DefaultValue(true),
        ReadOnly(false),
        ]
        public bool AutoLoadRootNodes
        {
            get;
            set;
        }

        /// <summary>
        /// 正在加载数据的特殊节点名。
        /// </summary>
        private static readonly string LoadingNodeName = System.Guid.NewGuid().ToString();

        /// <summary>
        /// 获取或设置正在加载节点时的文本提示信息。
        /// </summary>
        [
        DefaultValue("正在加载节点…"),
        Category("Data"),
        Description("正在加载节点时的文本提示信息。"),
        ReadOnly(false),
        ]
        public string LoadingText
        {
            get;
            set;
        }

        private event AsyncLoadNodesEventHandler _AsyncLoadNodes;
        /// <summary>
        /// 当需要异步加载节点时引发此事件。
        /// </summary>
        [
        Description("当需要异步加载节点时引发此事件。"),
        Browsable(true),
        ReadOnly(false),
        ]
        public event AsyncLoadNodesEventHandler AsyncLoadNodes
        {
            add
            {
                this._AsyncLoadNodes += value;
            }
            remove
            {
                this._AsyncLoadNodes -= value;
            }
        }

        /// <summary>
        /// 创建“正在加载节点”这一特殊节点。
        /// </summary>
        /// <returns></returns>
        private System.Windows.Forms.TreeNode CreateLoadingNode()
        {
            System.Windows.Forms.TreeNode loadNode = new System.Windows.Forms.TreeNode(this.LoadingText);
            loadNode.Name = LoadingNodeName;
            return loadNode;

        }

        /// <summary>
        /// 判断指定的节点是否“正在加载节点”这一特殊节点。
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private bool IsLoadingNode(System.Windows.Forms.TreeNode node)
        {
            return node != null && node.Name == LoadingNodeName;
        }

        /// <summary>
        /// 判断是否需要加载子节点。
        /// </summary>
        private bool IsNeedChildNodes(System.Windows.Forms.TreeNodeCollection tnc)
        {
            return (tnc.Count == 1 && this.IsLoadingNode(tnc[0]));
        }

        /// <summary>
        /// 一个方法代理。
        /// </summary>
        /// <param name="parentNodes"></param>
        /// <param name="nodes"></param>
        private delegate void FillNodesHandler(System.Windows.Forms.TreeNodeCollection parentNodes, System.Windows.Forms.TreeNode[] nodes);
        /// <summary>
        /// 填充节点。注意：填充之前先删除已有节点。
        /// </summary>
        /// <param name="parentNodes">被填充的父级节点。</param>
        /// <param name="nodes">待填充的子节点集合。</param>
        private void FillNodes(System.Windows.Forms.TreeNodeCollection parentNodes, System.Windows.Forms.TreeNode[] nodes)
        {
            if (nodes == null)
            {
                return;
            }

            if (this.InvokeRequired)
            {
                this.Invoke(new FillNodesHandler(FillNodes), parentNodes, nodes);
            }
            else
            {
                parentNodes.Clear();
                foreach (System.Windows.Forms.TreeNode tmp in nodes)
                {
                    if (tmp.Nodes.Count == 0)
                    {
                        tmp.Nodes.Add(CreateLoadingNode());
                    }
                    parentNodes.Add(tmp);
                }
            }
        }

        private event AsyncLoadNodesErrorEventHandler _AsyncLoadNodesError;
        /// <summary>
        /// 当异步加载节点产生异常时引发此事件。
        /// </summary>
        [
        Description("当异步加载节点产生异常时引发此事件。"),
        Browsable(true),
        ReadOnly(false),
        ]
        public event AsyncLoadNodesErrorEventHandler AsyncLoadNodesError
        {
            add
            {
                this._AsyncLoadNodesError += value;
            }
            remove
            {
                this._AsyncLoadNodesError -= value;
            }
        }

        /// <summary>
        /// 用于捕获处理当加载节点过程中引发的异常。
        /// </summary>
        private void InvokeAsyncLoadNodesError(object sender, AsyncLoadNodesErrorEventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new AsyncLoadNodesErrorEventHandler(this.InvokeAsyncLoadNodesError), sender, e);
            }
            else
            {
                if (this._AsyncLoadNodesError != null)
                {
                    this._AsyncLoadNodesError(this, e);
                }
                else
                {
                    System.Windows.Forms.DialogResult dr = System.Windows.Forms.MessageBox.Show(this, @"捕获到一个未处理的异常！
点击“确定”终止程序运行；
点击“取消”忽略此错误并继续运行。

详细信息如下：
" + e.Exception.ToString(), "错误", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Error, System.Windows.Forms.MessageBoxDefaultButton.Button1);
                    if (dr == System.Windows.Forms.DialogResult.OK)
                    {
                        System.Windows.Forms.Application.Exit(); //结束应用程序。
                        //System.Diagnostics.Process.GetCurrentProcess().Kill(); //强行结束应用程序。
                    }
                }
            }

        }

        /// <summary>
        /// 加载根节点。
        /// </summary>
        public void LoadRootNodes()
        {
            #region 加载根节点。
            switch (this.LoadNodeMode)
            {
                case eLoadNodeMode.Async:
                    if (this._AsyncLoadNodes != null)
                    {
                        if (this.Nodes.Count == 0)
                        {
                            this.Nodes.Add(CreateLoadingNode());
                        }

                        //if (this.IsNeedChildNodes(this.Nodes))
                        {
                            System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(this.动态加载根节点线程方法));
                            t.IsBackground = true;
                            t.Start(this);
                        }
                    }
                    break;
                case eLoadNodeMode.Sync:
                    if (this._AsyncLoadNodes != null)
                    {
                        if (this.Nodes.Count == 0)
                        {
                            this.Nodes.Add(CreateLoadingNode());
                        }

                        //if (this.IsNeedChildNodes(this.Nodes))
                        {
                            this.动态加载根节点线程方法(this);
                        }
                    }
                    break;
                case eLoadNodeMode.UserCustom:
                    break;
            }
            #endregion

        }

        private void 动态加载根节点线程方法(object p)
        {
            if (this._AsyncLoadNodes != null)
            {
                //System.Collections.Generic.List<TreeNode> l = new List<TreeNode>();
                AsyncLoadNodesEventArgs e = new AsyncLoadNodesEventArgs((System.Windows.Forms.TreeView)p);
                try
                {
                    this._AsyncLoadNodes(this, e);
                }
                catch (Exception ex)
                {
                    this.InvokeAsyncLoadNodesError(this, new AsyncLoadNodesErrorEventArgs(ex));
                }
                this.FillNodes(((System.Windows.Forms.TreeView)p).Nodes, e.Nodes.ToArray());
            }
        }

        /// <summary>
        /// 加载指定的节点下属的子节点。
        /// </summary>
        /// <param name="node">一个 TreeNode 实例。</param>
        public void LoadChildNodes(System.Windows.Forms.TreeNode node)
        {
            #region 加载子节点。
            switch (this.LoadNodeMode)
            {
                case eLoadNodeMode.Async:
                    if (this._AsyncLoadNodes != null)// && this.IsNeedChildNodes(node.Nodes)
                    {
                        System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ParameterizedThreadStart(this.动态加载子节点线程方法));
                        t.IsBackground = true;
                        t.Start(node);
                    }
                    break;
                case eLoadNodeMode.Sync:
                    if (this._AsyncLoadNodes != null)// && this.IsNeedChildNodes(node.Nodes)
                    {
                        this.动态加载子节点线程方法(node);
                    }
                    break;
                case eLoadNodeMode.UserCustom:
                    break;
            }
            #endregion

        }

        private void 动态加载子节点线程方法(object p)
        {
            if (this._AsyncLoadNodes != null)
            {
                //System.Collections.Generic.List<TreeNode> l = new List<TreeNode>();
                AsyncLoadNodesEventArgs e = new AsyncLoadNodesEventArgs((System.Windows.Forms.TreeNode)p);
                try
                {
                    this._AsyncLoadNodes(this, e);
                }
                catch (Exception ex)
                {
                    this.InvokeAsyncLoadNodesError(this, new AsyncLoadNodesErrorEventArgs(ex));
                }
                this.FillNodes(((System.Windows.Forms.TreeNode)p).Nodes, e.Nodes.ToArray());
            }

        }

        /// <summary>
        /// 当展开节点之后发生。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnAfterExpand(System.Windows.Forms.TreeViewEventArgs e)
        {
            if (this._AsyncLoadNodes != null && this.IsNeedChildNodes(e.Node.Nodes))
            {
                this.LoadChildNodes(e.Node);
            }

            base.OnAfterExpand(e);
        }

        /// <summary>
        /// 当选择一个节点之前发生。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnBeforeSelect(System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            if (this.IsLoadingNode(e.Node))
            {
                e.Cancel = true;
                return;
            }

            if (this.SelectLeafNodeOnly)
            {
                if (e.Node.Nodes.Count > 0)
                {
                    e.Cancel = true;
                    return;
                }
            }

            base.OnBeforeSelect(e);
        }
        #endregion

        #region 节点编辑方法扩展，主要修补了标准 TreeView Windows 控件的准备编辑节点事件中无法修改编辑区域文本的缺陷。
        /// <summary>
        /// 引发 System.Windows.Forms.TreeView.BeforeLabelEdit 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.NodeLabelEditEventArgs。</param>
        protected override void OnBeforeLabelEdit(System.Windows.Forms.NodeLabelEditEventArgs e)
        {
            base.OnBeforeLabelEdit(e);
            if (e.CancelEdit)
            {
                return;
            }

            Thinksea.Windows.Forms.BeforeLabelEditExpandEventArgs bnee = new Thinksea.Windows.Forms.BeforeLabelEditExpandEventArgs(e.Node, e.Node.Text);
            bnee.CancelEdit = e.CancelEdit;
            if (this._BeforeLabelEdit != null)
            {
                this._BeforeLabelEdit(this, bnee);
            }
            if (bnee.CancelEdit)
            {
                e.CancelEdit = true;
                return;
            }

            System.Windows.Forms.TextBox tb = new System.Windows.Forms.TextBox();
            tb.AutoSize = false;
            tb.Font = this.Font;
            tb.ForeColor = this.ForeColor;
            tb.BackColor = this.BackColor;
            tb.Margin = new System.Windows.Forms.Padding(0);
            tb.Text = bnee.Label;
            tb.MinimumSize = new System.Drawing.Size(e.Node.Bounds.Width + 1, e.Node.Bounds.Height);

            {
                System.Drawing.Graphics g = tb.CreateGraphics();
                System.Drawing.SizeF s = g.MeasureString(tb.Text, tb.Font);
                tb.Bounds = new System.Drawing.Rectangle(e.Node.Bounds.X - 1, e.Node.Bounds.Y - 1, (int)s.Width + 14, e.Node.Bounds.Height + 2);
            }
            //tb.Location = e.Node.Bounds.Location;
            //tb.Size = e.Node.Bounds.Size;
            this.Controls.Add(tb);
            //tb.SelectAll();
            tb.Focus();

            bool TextChanged = false;
            EventHandler LeaveEvent = delegate(object o, EventArgs et)
            {
                System.Windows.Forms.NodeLabelEditEventArgs anlee = new System.Windows.Forms.NodeLabelEditEventArgs(e.Node, (TextChanged ? tb.Text : null));
                anlee.CancelEdit = false;
                this.OnAfterLabelEdit(anlee);
                if (!anlee.CancelEdit && anlee.Label != null)
                {
                    if (e.Node.Text != anlee.Label)
                    {
                        e.Node.Text = anlee.Label;
                    }
                }
                this.Controls.Remove(tb);
                tb.Hide();
                tb.Dispose();
            };
            tb.Leave += LeaveEvent;
            tb.KeyDown += delegate(object s2, System.Windows.Forms.KeyEventArgs e2)
            {
                if (e2.KeyCode == System.Windows.Forms.Keys.Escape)
                {
                    tb.Leave -= LeaveEvent;
                    this.Controls.Remove(tb);
                    tb.Hide();
                    tb.Dispose();
                }
                else if (e2.KeyCode == System.Windows.Forms.Keys.Enter)
                {
                    tb.Leave -= LeaveEvent;
                    LeaveEvent(tb, new EventArgs());
                }
            };
            tb.TextChanged += delegate(object sender2, System.EventArgs e2)
            {
                System.Drawing.Graphics g = tb.CreateGraphics();
                System.Drawing.SizeF s = g.MeasureString(tb.Text, tb.Font);

                tb.Width = (int)s.Width + 14;
                TextChanged = true;
            };

            e.CancelEdit = true;

        }

        private event BeforeLabelEditExpandHandler _BeforeLabelEdit;
        /// <summary>
        /// 在编辑树节点标签前发生。
        /// </summary>
        [
        Description("在编辑树节点标签前引发此事件。"),
        Browsable(true),
        ReadOnly(false),
        ]
        public new event BeforeLabelEditExpandHandler BeforeLabelEdit
        {
            add
            {
                this._BeforeLabelEdit += value;
            }
            remove
            {
                this._BeforeLabelEdit -= value;
            }
        }
        //public event BeforeLabelEditExpandHandler BeforeLabelEditExpand
        //{
        //    add
        //    {
        //        this._BeforeLabelEditExpand += value;
        //    }
        //    remove
        //    {
        //        this._BeforeLabelEditExpand -= value;
        //    }
        //}

        #endregion

        private bool _ExpandOnMouseDoubleClick = true;
        /// <summary>
        /// 获取或设置一个值，指示双击节点文本时是否展开或折叠节点，如果有子节点。
        /// </summary>
        [
        Description("指示双击节点文本时是否展开或折叠节点，如果有子节点。"),
        DefaultValue(true),
        ReadOnly(false),
            //System.ComponentModel.RefreshPropertiesAttribute(System.ComponentModel.RefreshProperties.All),
        ]
        public bool ExpandOnMouseDoubleClick
        {
            get
            {
                return this._ExpandOnMouseDoubleClick;
            }
            set
            {
                this._ExpandOnMouseDoubleClick = value;
            }
        }

        /// <summary>
        /// 记录鼠标在TreeView控件上按下的次数
        /// </summary>
        private int m_MouseClicks = 0;
        /// <summary>
        /// 引发 System.Windows.Forms.Control.MouseDown 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.MouseEventArgs。</param>
        protected override void OnMouseDown(System.Windows.Forms.MouseEventArgs e)
        {
            this.m_MouseClicks = e.Clicks;
            base.OnMouseDown(e);
        }

        /// <summary>
        /// 引发 System.Windows.Forms.TreeView.BeforeExpand 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.TreeViewCancelEventArgs。</param>
        protected override void OnBeforeExpand(System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            if (!this.ExpandOnMouseDoubleClick)
            {
                if (this.m_MouseClicks > 1)
                {
                    e.Cancel = true;
                    return;
                }
            }
            base.OnBeforeExpand(e);
        }

        /// <summary>
        /// 引发 System.Windows.Forms.TreeView.BeforeCollapse 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.Windows.Forms.TreeViewCancelEventArgs。</param>
        protected override void OnBeforeCollapse(System.Windows.Forms.TreeViewCancelEventArgs e)
        {
            if (!this.ExpandOnMouseDoubleClick)
            {
                if (this.m_MouseClicks > 1)
                {
                    e.Cancel = true;
                    return;
                }
            }
            base.OnBeforeCollapse(e);
        }

        /// <summary>
        /// 处理命令键。
        /// </summary>
        /// <param name="msg">通过引用传递的 System.Windows.Forms.Message，它表示要处理的窗口消息。</param>
        /// <param name="keyData">System.Windows.Forms.Keys 值之一，它表示要处理的键。</param>
        /// <returns>如果字符已由控件处理，则为 true；否则为 false。</returns>
        protected override bool ProcessCmdKey(ref System.Windows.Forms.Message msg, System.Windows.Forms.Keys keyData)
        {
            #region 如果只允许选中叶节点，则接管键盘事件处理代码使用自定义逻辑处理请求。
            if (this.SelectLeafNodeOnly)
            {
                switch (keyData)
                {
                    case System.Windows.Forms.Keys.Up:
                        {
                            System.Windows.Forms.TreeNode sNode = this.SelectedNode;
                            if (sNode != null)
                            {
                                do
                                {
                                    sNode = sNode.PrevVisibleNode;
                                }
                                while (sNode != null && sNode.Nodes.Count > 0);
                                if (sNode != null)
                                {
                                    this.SelectedNode = sNode;
                                }
                            }
                            return true;
                        }
                    case System.Windows.Forms.Keys.Down:
                        {
                            System.Windows.Forms.TreeNode sNode = this.SelectedNode;
                            if (sNode == null)
                            {
                                if (this.Nodes.Count > 0)
                                {
                                    sNode = this.Nodes[0];
                                }
                            }
                            else
                            {
                                sNode = sNode.NextVisibleNode;
                            }
                            while (sNode != null && sNode.Nodes.Count > 0)
                            {
                                sNode = sNode.NextVisibleNode;
                            }
                            #region 如果不能定位到有效的可选节点则定位到树视图中第一个可选节点。
                            if (sNode == null && this.SelectedNode == null && this.Nodes.Count > 0)
                            {
                                sNode = this.Nodes[0];
                                while (sNode != null && sNode.Nodes.Count > 0)
                                {
                                    sNode = sNode.FirstNode;
                                }
                            }
                            #endregion
                            if (sNode != null)
                            {
                                this.SelectedNode = sNode;
                            }
                            return true;
                        }
                    case System.Windows.Forms.Keys.Left:
                        {
                            if (this.SelectedNode != null)
                            {
                                System.Windows.Forms.TreeNode sNode = this.SelectedNode;
                                System.Windows.Forms.TreeNode oldParent = null;
                                if (sNode.PrevNode != null && sNode.PrevNode.Nodes.Count > 0 && sNode.PrevNode.IsExpanded == false)
                                {
                                    sNode = sNode.PrevNode.LastNode;
                                }
                                else
                                {
                                    oldParent = sNode.Parent;
                                    if (sNode.Parent == null)
                                    {
                                        sNode = null;
                                    }
                                    else
                                    {
                                        sNode = sNode.Parent.PrevNode;
                                    }
                                }
                                while (sNode != null && sNode.Nodes.Count > 0)
                                {
                                    sNode = sNode.LastNode;
                                }
                                if (sNode != null)
                                {
                                    if (oldParent != null)
                                    {
                                        oldParent.Collapse();
                                    }
                                    this.SelectedNode = sNode;
                                }
                            }
                            return true;
                        }
                    case System.Windows.Forms.Keys.Right:
                        {
                            System.Windows.Forms.TreeNode sNode = this.SelectedNode;
                            System.Windows.Forms.TreeNode oldParent = null;
                            if (sNode == null)
                            {
                                if (this.Nodes.Count > 0)
                                {
                                    sNode = this.Nodes[0];
                                }
                            }
                            else
                            {
                                oldParent = sNode.Parent;
                                if (sNode.Nodes.Count > 0)
                                {
                                    sNode = sNode.FirstNode;
                                }
                                else if (sNode.NextVisibleNode != null && sNode.NextVisibleNode.Nodes.Count > 0 && sNode.NextVisibleNode.IsExpanded == false)
                                {
                                    sNode = sNode.NextVisibleNode;
                                }
                            }
                            while (sNode != null && sNode.Nodes.Count > 0)
                            {
                                sNode = sNode.FirstNode;
                            }
                            if (sNode != null)
                            {
                                //if (preNode != null)
                                //{
                                //    preNode.Collapse();
                                //}
                                this.SelectedNode = sNode;
                            }
                            return true;
                        }
                }
            }
            #endregion

            return base.ProcessCmdKey(ref msg, keyData);
        }

    }

    /// <summary>
    /// 开始拖动节点时引发事件代理。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void NodeDragHandler(object sender, NodeDragEventArgs e);

    /// <summary>
    /// 为开始拖动节点事件提供数据。
    /// </summary>
    public class NodeDragEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取一个值，该值指示在拖动操作过程中按下的鼠标按钮。
        /// </summary>
        /// <value>System.Windows.Forms.MouseButtons 值的按位组合。</value>
        public System.Windows.Forms.MouseButtons Button
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取当前操作的节点。
        /// </summary>
        public System.Windows.Forms.TreeNode Node
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取或设置一个值，用于指示是否取消操作。
        /// </summary>
        public bool Cancel
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public NodeDragEventArgs()
        {
            this.Cancel = false;
        }

        /// <summary>
        /// 用指定的数据初始化此节点。
        /// </summary>
        /// <param name="button">指示在拖动操作过程中按下的鼠标按钮。</param>
        /// <param name="node">当前操作的节点。</param>
        public NodeDragEventArgs(System.Windows.Forms.MouseButtons button, System.Windows.Forms.TreeNode node)
        {
            this.Button = button;
            this.Cancel = false;
            this.Node = node;
        }

    }

    /// <summary>
    /// 当节点被拖拽到新位置时的事件方法代理。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void NodeDragOverHandler(object sender, NodeDragOverEventArgs e);

    /// <summary>
    /// 为节点被拖拽到新位置时的事件提供数据。
    /// </summary>
    public class NodeDragOverEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取当前操作的节点。
        /// </summary>
        public System.Windows.Forms.TreeNode Node
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取节点的新位置。
        /// </summary>
        public System.Windows.Forms.TreeNode NewParentNode
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取或设置一个值，用于指示是否允许移动到指定位置。
        /// </summary>
        public bool AllowMove
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public NodeDragOverEventArgs()
        {
            this.AllowMove = true;
        }

        /// <summary>
        /// 用指定的数据初始化此节点。
        /// </summary>
        /// <param name="node">当前操作的节点。</param>
        /// <param name="newParentNode">节点的新位置。</param>
        public NodeDragOverEventArgs(System.Windows.Forms.TreeNode node, System.Windows.Forms.TreeNode newParentNode)
        {
            this.AllowMove = true;
            this.Node = node;
            this.NewParentNode = newParentNode;

        }

    }

    /// <summary>
    /// 节点准备移动方法代理。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void NodeMovingHandler(object sender, NodeMovingEventArgs e);

    /// <summary>
    /// 为节点移动事件提供数据。
    /// </summary>
    public class NodeMovingEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取当前操作的节点。
        /// </summary>
        public System.Windows.Forms.TreeNode Node
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取节点的原始位置。
        /// </summary>
        public System.Windows.Forms.TreeNode OldParentNode
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取节点的新位置。
        /// </summary>
        public System.Windows.Forms.TreeNode NewParentNode
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取节点位于新位置的索引值。
        /// </summary>
        public int NewIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取或设置一个值，用于指示是否取消操作。
        /// </summary>
        public bool Cancel
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public NodeMovingEventArgs()
        {
            this.Cancel = false;
        }

        /// <summary>
        /// 用指定的数据初始化此节点。
        /// </summary>
        /// <param name="node">当前操作的节点。</param>
        /// <param name="oldParentNode">节点的原始位置。</param>
        /// <param name="newParentNode">节点的新位置。</param>
        /// <param name="newIndex">节点位于新位置的索引值。</param>
        public NodeMovingEventArgs(System.Windows.Forms.TreeNode node, System.Windows.Forms.TreeNode oldParentNode, System.Windows.Forms.TreeNode newParentNode, int newIndex)
        {
            this.Cancel = false;
            this.Node = node;
            this.OldParentNode = oldParentNode;
            this.NewParentNode = newParentNode;
            this.NewIndex = NewIndex;

        }

    }

    /// <summary>
    /// 节点移动完成方法代理。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void NodeMovedEventHandler(object sender, NodeMovedEventArgs e);

    /// <summary>
    /// 为节点移动完成事件提供数据。
    /// </summary>
    public class NodeMovedEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取当前操作的节点。
        /// </summary>
        public System.Windows.Forms.TreeNode Node
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取节点的原始位置。
        /// </summary>
        public System.Windows.Forms.TreeNode OldParentNode
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取节点的新位置。
        /// </summary>
        public System.Windows.Forms.TreeNode NewParentNode
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取节点位于新位置的索引值。
        /// </summary>
        public int NewIndex
        {
            get;
            private set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public NodeMovedEventArgs()
        {
        }

        /// <summary>
        /// 用指定的数据初始化此节点。
        /// </summary>
        /// <param name="node">当前操作的节点。</param>
        /// <param name="oldParentNode">节点的原始位置。</param>
        /// <param name="newParentNode">节点的新位置。</param>
        /// <param name="newIndex">节点位于新位置的索引值。</param>
        public NodeMovedEventArgs(System.Windows.Forms.TreeNode node, System.Windows.Forms.TreeNode oldParentNode, System.Windows.Forms.TreeNode newParentNode, int newIndex)
        {
            this.Node = node;
            this.OldParentNode = oldParentNode;
            this.NewParentNode = newParentNode;
            this.NewIndex = newIndex;
        }

    }

    /// <summary>
    /// 节点加载方式。
    /// </summary>
    public enum eLoadNodeMode
    {
        /// <summary>
        /// 使用同步加载模式（将异步加载模式变成同步加载模式。）
        /// </summary>
        Sync = 0,
        /// <summary>
        /// 异步加载。
        /// </summary>
        Async = 1,
        /// <summary>
        /// 用户自定义加载方案。
        /// </summary>
        UserCustom = 2,
    }

    /// <summary>
    /// 一个方法代理，用于异步加载节点。
    /// </summary>
    /// <param name="sender">引发此事件的控件。</param>
    /// <param name="e">事件数据。</param>
    public delegate void AsyncLoadNodesEventHandler(object sender, AsyncLoadNodesEventArgs e);

    /// <summary>
    /// 用于为异步加载节点事件提供数据的类。
    /// </summary>
    public class AsyncLoadNodesEventArgs : System.EventArgs
    {
        /// <summary>
        /// 对于获取根节点是待填充树视图（TreeView 实例），子节点是待填充节点（TreeNode 实例）。一般情况可以通过“if (e.ParentNode is TreeView)”代码判断执行加载根节点操作还是加载子节点操作。
        /// </summary>
        public object ParentNode
        {
            get;
            private set;
        }

        private System.Collections.Generic.List<System.Windows.Forms.TreeNode> _Nodes;
        /// <summary>
        /// 获取一个集合，用于存储待填充的节点。
        /// </summary>
        public System.Collections.Generic.List<System.Windows.Forms.TreeNode> Nodes
        {
            get
            {
                return this._Nodes;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public AsyncLoadNodesEventArgs()
        {
            this._Nodes = new System.Collections.Generic.List<System.Windows.Forms.TreeNode>();
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="parentNode">对于获取根节点是待填充树视图（TreeView 实例），子节点是待填充节点（TreeNode 实例）。</param>
        public AsyncLoadNodesEventArgs(object parentNode)
        {
            this.ParentNode = parentNode;
            this._Nodes = new System.Collections.Generic.List<System.Windows.Forms.TreeNode>();
        }

    }

    /// <summary>
    /// 一个方法代理，用于处理异步加载节点时出现的异常。
    /// </summary>
    /// <param name="sender">引发此事件的控件。</param>
    /// <param name="e">事件数据。</param>
    public delegate void AsyncLoadNodesErrorEventHandler(object sender, AsyncLoadNodesErrorEventArgs e);

    /// <summary>
    /// 用于为异步加载节点异常处理方法提供数据。
    /// </summary>
    public class AsyncLoadNodesErrorEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取异常信息。
        /// </summary>
        public System.Exception Exception
        {
            get;
            private set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public AsyncLoadNodesErrorEventArgs()
        {
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="exception">异常信息。</param>
        public AsyncLoadNodesErrorEventArgs(System.Exception exception)
        {
            this.Exception = exception;
        }

    }

    /// <summary>
    /// 表示将用于处理 Thinksea.Windows.Forms.TreeViewExpand 控件的 Thinksea.Windows.Forms.TreeViewExpand.BeforeLabelEdit2 和 System.Windows.Forms.TreeView.AfterLabelEdit 事件的方法。
    /// </summary>
    /// <param name="sender">事件源。</param>
    /// <param name="e">包含事件数据的 Thinksea.Windows.Forms.NodeLabelEditEventArgs。</param>
    public delegate void BeforeLabelEditExpandHandler(object sender, Thinksea.Windows.Forms.BeforeLabelEditExpandEventArgs e);

    /// <summary>
    /// 为节点编辑事件提供数据。
    /// </summary>
    public class BeforeLabelEditExpandEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取或设置指示是否已取消编辑的值。
        /// </summary>
        public bool CancelEdit
        {
            get;
            set;
        }

        /// <summary>
        /// 获取包含待编辑文本的树节点。
        /// </summary>
        public System.Windows.Forms.TreeNode Node
        {
            get;
            private set;
        }

        /// <summary>
        /// 获取或设置处于编辑状态的节点显示的文本。
        /// </summary>
        public string Label
        {
            get;
            set;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="node">包含待编辑文本的树节点。</param>
        public BeforeLabelEditExpandEventArgs(System.Windows.Forms.TreeNode node)
        {
            this.CancelEdit = false;
            this.Node = node;
            this.Label = node.Text;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="node">包含待编辑文本的树节点。</param>
        /// <param name="label">与树节点关联的新文本。</param>
        public BeforeLabelEditExpandEventArgs(System.Windows.Forms.TreeNode node, string label)
        {
            this.CancelEdit = false;
            this.Node = node;
            this.Label = label;
        }

    }

}
