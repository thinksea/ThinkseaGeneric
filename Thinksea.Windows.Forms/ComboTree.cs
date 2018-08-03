using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Thinksea.Windows.Forms
{
    /// <summary>
    /// 一个树节点选择控件，提供动态加载节点的能力。
    /// </summary>
    public partial class ComboTree : UserControl
    {
        /// <summary>
        /// 获取或设置选中的节点名称。注意，如果需要设置此属性的值，则必须实现“GetNodeText”事件。
        /// </summary>
        [
        Browsable(false),
        ReadOnly(true),
        ]
        public string NodeName
        {
            get
            {
                return Convert.ToString(this.comboBox1.Tag);
            }
            set
            {
                TreeNode tn = this.FindNodeByName(this.treeView1.Nodes, value);
                if (tn != null)
                {
                    this.Text = tn.Text;
                    this.treeView1.SelectedNode = tn;
                }
                else
                {
                    string text = "";
                    if (this._GetNodeText != null)
                    {
                        text = this._GetNodeText(this, new GetNodeTextEventArgs(value));
                    }
                    //else
                    //{
                    //    MessageBox.Show(this, "没有处理 SelectedDynamicTreeNode 的“GetNodeText”事件，所以无法正确执行此操作。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    return;
                    //}
                    if (text == null)
                    {
                        //    throw new System.Exception("无法加载数据，指定的分类可能已经被删除。");
                        this.Text = "";
                    }
                    else
                    {
                        this.Text = text;
                    }
                }
                this.comboBox1.Tag = value;
                if (this._AfterSelect != null)
                {
                    this._AfterSelect(this, System.EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// 获取或设置选中的节点文本。
        /// </summary>
        [Browsable(false), ReadOnly(true)]
        public new string Text
        {
            get
            {
                return this.comboBox1.Text;
            }
            set
            {
                string v = value;
                if (v == null)
                {
                    v = "";
                }
                this.comboBox1.Items.Clear();
                this.comboBox1.Items.Add(v);
                this.comboBox1.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// 获取或设置下拉列表框高度。
        /// </summary>
        [
        DefaultValue(-1),
        Category("Data"),
        Description("下拉列表框高度。"),
        ]
        public int DropDownHeight
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置下拉列表框宽度。
        /// </summary>
        [
        DefaultValue(-1),
        Category("Data"),
        Description("下拉列表框宽度。"),
        ]
        public int DropDownWidth
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置正在加载节点时的文本提示信息。
        /// </summary>
        [
        DefaultValue("正在加载节点…"),
        Category("Data"),
        Description("正在加载节点时的文本提示信息。"),
        ]
        public string LoadingText
        {
            get
            {
                return this.treeView1.LoadingText;
            }
            set
            {
                this.treeView1.LoadingText = value;
            }
        }

        /// <summary>
        /// 获取全部的树节点集合。
        /// </summary>
        public TreeNodeCollection Nodes
        {
            get
            {
                return this.treeView1.Nodes;
            }
        }

        /// <summary>
        /// 指示是否只允许选中叶节点。
        /// </summary>
        [DefaultValue(false)]
        [Description("指示是否只允许选中叶节点")]
        public bool SelectLeafNodeOnly
        {
            get
            {
                return this.treeView1.SelectLeafNodeOnly;
            }
            set
            {
                this.treeView1.SelectLeafNodeOnly = value;
            }
        }

        System.Windows.Forms.Form treeForm = new Form();
        TreeViewExpand treeView1 = new TreeViewExpand();

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public ComboTree()
        {
            InitializeComponent();

            //this.LoadingText = "正在加载节点…";
            //this.Height = this.comboBox1.Height;
            this.DropDownHeight = -1;
            this.DropDownWidth = -1;

            this.treeForm.SuspendLayout();

            System.Windows.Forms.PictureBox sizeGrip = new PictureBox();
            //sizeGrip.SizeMode = PictureBoxSizeMode.AutoSize;
            //sizeGrip.Size = new Size(10, 10);
            //sizeGrip.Image = global::PublicControls.Properties.Resources.SizeGrip;
            //sizeGrip.Location = new Point(280, 254);
            sizeGrip.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            sizeGrip.Cursor = System.Windows.Forms.Cursors.SizeNWSE;
            sizeGrip.MouseDown += new MouseEventHandler(sizeGrip_MouseDown);
            sizeGrip.MouseMove += new MouseEventHandler(sizeGrip_MouseMove);
            sizeGrip.MouseUp += new MouseEventHandler(sizeGrip_MouseUp);
            //sizeGrip.BackColor = Color.Transparent;
            //sizeGrip.BackColor = Color.FromArgb(50, sizeGrip.BackColor);
            //sizeGrip.BackgroundImage = global::PublicControls.Properties.Resources.SizeGrip;
            AnomalisticControl.CreateControlRegion(sizeGrip, global::Thinksea.Windows.Forms.Properties.Resources.SizeGrip, true);
            sizeGrip.Location = new Point(treeForm.ClientSize.Width - sizeGrip.Width - 1, treeForm.ClientSize.Height - sizeGrip.Height - 1);
            treeForm.Controls.Add(sizeGrip);

            treeView1.Dock = DockStyle.Fill;
            treeView1.TabStop = false;
            //treeView1.AfterExpand += this.treeView1_AfterExpand;
            //treeView1.BeforeSelect += this.treeView1_BeforeSelect;
            treeView1.AfterSelect += this.treeView1_AfterSelect;
            treeView1.KeyDown += this.treeView1_KeyDown;
            treeView1.LostFocus += delegate(object sender, System.EventArgs e)
            {
                this.treeForm.Hide();
            };
            //treeView1.Visible = false;
            treeForm.Controls.Add(treeView1);

            treeForm.ShowInTaskbar = false;
            //treeForm.TopMost = true;
            treeForm.FormBorderStyle = FormBorderStyle.None;
            //treeForm.SizeGripStyle = SizeGripStyle.Show;
            treeForm.KeyPreview = true;
            treeForm.MinimumSize = new Size(sizeGrip.Right, sizeGrip.Bottom);
            treeForm.Visible = false;


            treeForm.Load += delegate(object sender, EventArgs e)
            {
                #region 初始化列表框尺寸。
                if (this.DropDownHeight == -1)
                {
                    this.treeForm.Height = 100;
                }
                else
                {
                    this.treeForm.Height = this.DropDownHeight;
                }

                if (this.DropDownWidth == -1)
                {
                    this.treeForm.Width = this.Width;
                }
                else
                {
                    this.treeForm.Width = this.DropDownWidth;
                }
                #endregion

                //treeView1.LoadRootNodes(); //加载根节点。

            };

            treeForm.VisibleChanged += delegate(object sender, System.EventArgs e)
            {
                if (treeForm.Visible)
                {
                    Point p = this.comboBox1.PointToScreen(new Point(this.comboBox1.Left, this.comboBox1.Bottom));
                    treeForm.Location = p;

                    #region 修正下拉列表项目显示区域的坐标。
                    if (this.treeForm.Right > Screen.PrimaryScreen.WorkingArea.Right)
                    {
                        this.treeForm.Left = Screen.PrimaryScreen.WorkingArea.Right - this.treeForm.Width;
                    }
                    if (this.treeForm.Bottom > Screen.PrimaryScreen.WorkingArea.Bottom)
                    {
                        this.treeForm.Top = Screen.PrimaryScreen.WorkingArea.Bottom - this.treeForm.Height;
                    }
                    if (this.treeForm.Left < 0)
                    {
                        this.treeForm.Left = 0;
                    }
                    if (this.treeForm.Top < 0)
                    {
                        this.treeForm.Top = 0;
                    }
                    #endregion
                }
                //else
                //{
                //    if (this.ParentForm.Visible)
                //    {
                //        //this.ParentForm.Activate();
                //        this.ParentForm.Select();
                //        //this.comboBox1.Focus();
                //    }
                //}
            };

            //treeForm.Activated += delegate(object sender, System.EventArgs e)
            //{
            //    this.ParentForm.Activate();
            //};

            //treeForm.Deactivate += delegate(object sender, System.EventArgs e)
            //{
            //    this.treeForm.Hide();
            //    //this.ParentForm.Activate();
            //};

            treeForm.FormClosing += delegate(object sender, System.Windows.Forms.FormClosingEventArgs e)
            {
                if (e.CloseReason == CloseReason.UserClosing)
                {
                    this.treeForm.Hide();
                    e.Cancel = true;
                }
            };
            treeForm.KeyDown += delegate(object sender, System.Windows.Forms.KeyEventArgs e)
            {
                if (e.KeyData == Keys.Escape)
                {
                    treeForm.Hide();
                    e.Handled = true;
                    e.SuppressKeyPress = false;
                }
                if (e.KeyData == Keys.Tab)
                {
                    treeForm.Hide();
                    //this.SelectNextControl(this, false, true, true, true);
                    e.Handled = true;
                    e.SuppressKeyPress = false;
                }
            };

            this.treeForm.ResumeLayout(false);
            this.treeForm.PerformLayout();
        }

        /// <summary>
        /// 引发 System.Windows.Forms.Control.SizeChanged 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs。</param>
        protected override void OnSizeChanged(EventArgs e)
        {
            this.Height = this.comboBox1.Height;
            base.OnSizeChanged(e);
        }

        void sizeGrip_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && this.isDropSize)
            {
                treeForm.Size += new Size(e.X - this.smPoint.X, e.Y - this.smPoint.Y);
            }
        }

        private Point smPoint;
        private bool isDropSize = false;
        void sizeGrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && e.Clicks == 1)
            {
                this.smPoint = e.Location;
                this.isDropSize = true;
            }
        }

        void sizeGrip_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                treeForm.Size += new Size(e.X - this.smPoint.X, e.Y - this.smPoint.Y);
                this.isDropSize = false;
            }
        }

        /// <summary>
        /// 查找符合指定编号的节点。
        /// </summary>
        private TreeNode FindNodeByName(TreeNodeCollection tnc, string name)
        {
            foreach (TreeNode tmp in tnc)
            {
                if (tmp.Name == name)
                {
                    return tmp;
                }
                else
                {
                    TreeNode tn = FindNodeByName(tmp.Nodes, name);
                    if (tn != null)
                    {
                        return tn;
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// 当需要异步获取节点时引发此事件。
        /// </summary>
        [
        Description("当需要异步获取节点时引发此事件。"),
        ReadOnly(false),
        ]
        public event AsyncLoadNodesEventHandler AsyncGetNodes
        {
            add
            {
                this.treeView1.AsyncLoadNodes += value;
            }
            remove
            {
                this.treeView1.AsyncLoadNodes -= value;
            }
        }

        private event GetNodeTextEventHandler _GetNodeText;
        /// <summary>
        /// 需要获取指定节点的文本时引发此事件。
        /// </summary>
        /// <remarks>
        /// 当回填数据时一般由用户代码设置属性“NodeName”的值，同时通过此事件提供应该显示的文本。这是限制必须提供呈现文本的解决方法。
        /// </remarks>
        [
        Description("需要获取指定节点的文本时引发此事件。"),
        Browsable(true),
        ]
        public event GetNodeTextEventHandler GetNodeText
        {
            add
            {
                this._GetNodeText += value;
            }
            remove
            {
                this._GetNodeText -= value;
            }
        }

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
                this.treeView1.AsyncLoadNodesError += value;
            }
            remove
            {
                this.treeView1.AsyncLoadNodesError -= value;
            }
        }

        private void comboBox1_DropDown(object sender, EventArgs e)
        {
            if (!this.treeForm.Visible)
            {
                //this.treeForm.Show(this);
                this.treeForm.Show();
                this.treeForm.BringToFront();
                this.treeView1.Focus();
            }

            #region 隐藏原下拉列表框的下拉框。
            {
                Timer t = new Timer();
                t.Interval = 1;
                t.Tick += delegate(object sender2, EventArgs e2)
                {
                    t.Stop();
                    this.comboBox1.DroppedDown = false; //隐藏 comboBox1 的下拉框。
                    t.Dispose();
                };
                t.Start();
            }
            #endregion

        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (e.Action == TreeViewAction.ByMouse)
            {
                this.comboBox1.Tag = e.Node.Name;
                this.Text = e.Node.Text;
                this.treeForm.Hide();
                //this.ParentForm.Activate();
                //this.ParentForm.Select();
                //this.comboBox1.Focus();
                if (this._AfterSelect != null)
                {
                    this._AfterSelect(this, System.EventArgs.Empty);
                }
            }
        }

        private void comboBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down && e.Modifiers == Keys.None && !this.treeForm.Visible)
            {
                //this.treeForm.Show(this);
                this.treeForm.Show();
                this.treeForm.BringToFront();
                this.treeView1.Focus();
                e.Handled = true;
                e.SuppressKeyPress = false;
            }

        }

        ///// <summary>
        ///// 处理键盘消息。
        ///// </summary>
        ///// <param name="msg"></param>
        ///// <param name="keyData"></param>
        ///// <returns></returns>
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    if (keyData == Keys.Escape && this.treeForm.Visible)
        //    {
        //        this.treeForm.Hide();
        //        return true;
        //    }
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}

        private void treeView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter && e.Modifiers == Keys.None)// && this.treeView1.Visible && this.treeView1.Focused
            {
                this.comboBox1.Tag = this.treeView1.SelectedNode.Name;
                this.Text = this.treeView1.SelectedNode.Text;
                this.treeForm.Hide();
                //this.comboBox1.Focus();
                e.Handled = true;
                e.SuppressKeyPress = false;
                if (this._AfterSelect != null)
                {
                    this._AfterSelect(this, System.EventArgs.Empty);
                }
            }
        }

        private void comboBox1_Leave(object sender, EventArgs e)
        {
            this.treeForm.Hide();
        }

        private event System.EventHandler _AfterSelect;
        /// <summary>
        /// 当选中节点后引发此事件。
        /// </summary>
        public event System.EventHandler AfterSelect
        {
            add
            {
                this._AfterSelect += value;
            }
            remove
            {
                this._AfterSelect -= value;
            }
        }

        //private readonly int MOUSEEVENTF_LEFTDOWN = 0x2;
        //private readonly int MOUSEEVENTF_LEFTUP = 0x4;
        //[System.Runtime.InteropServices.DllImport("user32")]
        //public static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);
        ////public static extern void mouse_event(int dwFlags);

        //public void MouseClick(int X, int Y)
        //{
        //    mouse_event(MOUSEEVENTF_LEFTDOWN, X * 65536 / 1024, X * 65536 / 768, 0, 0);
        //    mouse_event(MOUSEEVENTF_LEFTUP, Y * 65536 / 1024, Y * 65536 / 768, 0, 0);
        //}

    }

    /// <summary>
    /// 一个方法代理。用于获取指定节点的文本。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    /// <returns></returns>
    public delegate string GetNodeTextEventHandler(object sender, GetNodeTextEventArgs e);

    /// <summary>
    /// 用于为获取指定节点的文本事件提供数据的类。
    /// </summary>
    public class GetNodeTextEventArgs : System.EventArgs
    {
        /// <summary>
        /// 节点名。
        /// </summary>
        public string NodeName
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public GetNodeTextEventArgs()
        {
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="nodeName">节点名。</param>
        public GetNodeTextEventArgs(string nodeName)
        {
            this.NodeName = nodeName;

        }

    }
}
