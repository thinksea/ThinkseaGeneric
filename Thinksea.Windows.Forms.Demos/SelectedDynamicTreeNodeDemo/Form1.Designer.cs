namespace SelectedDynamicTreeNodeDemo
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("节点0");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("节点2");
            System.Windows.Forms.TreeNode treeNode3 = new System.Windows.Forms.TreeNode("节点3");
            System.Windows.Forms.TreeNode treeNode4 = new System.Windows.Forms.TreeNode("节点4");
            System.Windows.Forms.TreeNode treeNode5 = new System.Windows.Forms.TreeNode("节点1", new System.Windows.Forms.TreeNode[] {
            treeNode2,
            treeNode3,
            treeNode4});
            System.Windows.Forms.TreeNode treeNode6 = new System.Windows.Forms.TreeNode("节点5");
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.btn显示下拉列表树当前值 = new System.Windows.Forms.Button();
            this.navigationBar1 = new Thinksea.Windows.Forms.NavigationBar();
            this.pageNavigation1 = new Thinksea.Windows.Forms.PageNavigation();
            this.treeViewExpand1 = new Thinksea.Windows.Forms.TreeViewExpand();
            this.selectedTreeNode1 = new Thinksea.Windows.Forms.ComboTree();
            this.SuspendLayout();
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(166, 142);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 21);
            this.textBox2.TabIndex = 1;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(49, 242);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(585, 113);
            this.richTextBox1.TabIndex = 7;
            this.richTextBox1.Text = "在这里输出异常信息";
            // 
            // btn显示下拉列表树当前值
            // 
            this.btn显示下拉列表树当前值.Location = new System.Drawing.Point(334, 79);
            this.btn显示下拉列表树当前值.Name = "btn显示下拉列表树当前值";
            this.btn显示下拉列表树当前值.Size = new System.Drawing.Size(75, 23);
            this.btn显示下拉列表树当前值.TabIndex = 8;
            this.btn显示下拉列表树当前值.Text = "显示下拉列表树当前值";
            this.btn显示下拉列表树当前值.UseVisualStyleBackColor = true;
            this.btn显示下拉列表树当前值.Click += new System.EventHandler(this.btn显示下拉列表树当前值_Click);
            // 
            // navigationBar1
            // 
            this.navigationBar1.Font = new System.Drawing.Font("宋体", 10F);
            this.navigationBar1.ForeColor = System.Drawing.Color.Blue;
            this.navigationBar1.Location = new System.Drawing.Point(0, 38);
            this.navigationBar1.Name = "navigationBar1";
            this.navigationBar1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 3);
            this.navigationBar1.Size = new System.Drawing.Size(670, 25);
            this.navigationBar1.TabIndex = 9;
            // 
            // pageNavigation1
            // 
            this.pageNavigation1.LineNumberLabelText = "行号2";
            this.pageNavigation1.Location = new System.Drawing.Point(0, 0);
            this.pageNavigation1.Name = "pageNavigation1";
            this.pageNavigation1.Size = new System.Drawing.Size(700, 26);
            this.pageNavigation1.TabIndex = 6;
            this.pageNavigation1.Text = "pageNavigation1";
            this.pageNavigation1.OnPageIndexChanged += new Thinksea.Windows.Forms.PageIndexChangedEventHandler(this.pageNavigation1_OnPageIndexChanged);
            this.pageNavigation1.OnLineNumberChanging += new Thinksea.Windows.Forms.LineNumberChangingEventHandler(this.pageNavigation1_OnLineNumberChanging);
            // 
            // treeViewExpand1
            // 
            this.treeViewExpand1.AllowDragNode = true;
            this.treeViewExpand1.AllowDrop = true;
            this.treeViewExpand1.LabelEdit = true;
            this.treeViewExpand1.Location = new System.Drawing.Point(155, 130);
            this.treeViewExpand1.Name = "treeViewExpand1";
            treeNode1.Name = "节点0";
            treeNode1.Text = "节点0";
            treeNode2.Name = "节点2";
            treeNode2.Text = "节点2";
            treeNode3.Name = "节点3";
            treeNode3.Text = "节点3";
            treeNode4.Name = "节点4";
            treeNode4.Text = "节点4";
            treeNode5.Name = "节点1";
            treeNode5.Text = "节点1";
            treeNode6.Name = "节点5";
            treeNode6.Text = "节点5";
            this.treeViewExpand1.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode1,
            treeNode5,
            treeNode6});
            this.treeViewExpand1.Size = new System.Drawing.Size(121, 97);
            this.treeViewExpand1.TabIndex = 5;
            // 
            // selectedTreeNode1
            // 
            this.selectedTreeNode1.Location = new System.Drawing.Point(49, 79);
            this.selectedTreeNode1.Name = "selectedTreeNode1";
            this.selectedTreeNode1.Size = new System.Drawing.Size(260, 20);
            this.selectedTreeNode1.TabIndex = 4;
            this.selectedTreeNode1.AsyncGetNodes += new Thinksea.Windows.Forms.AsyncLoadNodesEventHandler(this.selectedDynamicTreeNode1_AsyncGetNodes);
            this.selectedTreeNode1.AsyncLoadNodesError += new Thinksea.Windows.Forms.AsyncLoadNodesErrorEventHandler(this.selectedTreeNode1_AsyncLoadNodesError);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(700, 367);
            this.Controls.Add(this.navigationBar1);
            this.Controls.Add(this.btn显示下拉列表树当前值);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.pageNavigation1);
            this.Controls.Add(this.treeViewExpand1);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.selectedTreeNode1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox2;
        private Thinksea.Windows.Forms.ComboTree selectedTreeNode1;
        private Thinksea.Windows.Forms.TreeViewExpand treeViewExpand1;
        private Thinksea.Windows.Forms.PageNavigation pageNavigation1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Button btn显示下拉列表树当前值;
        private Thinksea.Windows.Forms.NavigationBar navigationBar1;


    }
}

