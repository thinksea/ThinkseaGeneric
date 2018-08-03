using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SelectedDynamicTreeNodeDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            for (int i = 1; i <= 5; i++)
            {
                this.navigationBar1.Items.Add(new Thinksea.Windows.Forms.NavigationItem(i.ToString(), "Item" + i.ToString(), i % 2 == 0));
            }

            this.pageNavigation1.RecordsCount = 100;
            //this.pageNavigation1.PageIndex = 0;
            this.selectedTreeNode1.NodeName = "abc";
            this.selectedTreeNode1.Text = "def";

        }

        private void selectedDynamicTreeNode1_AsyncGetNodes(object sender, Thinksea.Windows.Forms.AsyncLoadNodesEventArgs e)
        {
            System.Random rand = new Random();
            if (rand.Next(1, 10) < 3)
            {
                throw new System.Exception("通过一个随机产生的异常信息演示如何简单捕获加载节点过程中产生的异常信息！");
            }
            if (e.ParentNode is TreeView) //如果是获取根节点。
            {
                #region 获取根节点代码。
                TreeView t = (TreeView)e.ParentNode;
                for (int i = 0; i < 10; i++)
                {
                    TreeNode tn = new TreeNode(i.ToString());
                    tn.Name = i.ToString();
                    e.Nodes.Add(tn);
                }
                #endregion
            }
            else //获取子节点。
            {
                #region 获取子节点代码。
                TreeNode t = (TreeNode)e.ParentNode; //当前操作的节点。
                for (int i = 0; i < 10; i++)
                {
                    TreeNode tn = new TreeNode(t.Text + ":" + i.ToString());
                    tn.Name = i.ToString();
                    e.Nodes.Add(tn);
                }
                #endregion
            }
            System.Threading.Thread.Sleep(300); //演示延迟效果
        }

        private void selectedTreeNode1_AsyncLoadNodesError(object sender, Thinksea.Windows.Forms.AsyncLoadNodesErrorEventArgs e)
        {
            this.richTextBox1.Text = @"（此消息由用户代码发出）演示如何同步化处理加载节点过程中产生的异常信息。
详细异常信息如下：
" + e.Exception.ToString();
        }

        private void pageNavigation1_OnLineNumberChanging(object sender, Thinksea.Windows.Forms.LineNumberChangingEventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(e.LineNumber.ToString());

        }

        private void pageNavigation1_OnPageIndexChanged(object sender, Thinksea.Windows.Forms.PageIndexChangedEventArgs e)
        {
            switch (e.PageIndex)
            {
                case 0:
                    this.pageNavigation1.PageSize = 20;
                    this.pageNavigation1.RecordsCount = 1000;
                    break;
                case 1:
                    this.pageNavigation1.PageSize = 30;
                    this.pageNavigation1.RecordsCount = 1800;
                    break;
                case 2:
                    this.pageNavigation1.PageSize = 10;
                    this.pageNavigation1.RecordsCount = 400;
                    break;
                default:
                    this.pageNavigation1.RecordsCount = 100;
                    this.pageNavigation1.PageSize = 15;
                    break;
            }

        }

        private void btn显示下拉列表树当前值_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Name=" + this.selectedTreeNode1.NodeName + @"
Text=" + this.selectedTreeNode1.Text);
        }

    }

}
