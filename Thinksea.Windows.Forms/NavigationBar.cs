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
    /// 导航菜单控件。
    /// </summary>
    [DefaultEvent("OnNavigation"),]
    public partial class NavigationBar : System.Windows.Forms.FlowLayoutPanel, IDisposable
    {
        private NavigationItemCollections _Items;
        /// <summary>
        /// 获取导航菜单项目集合。
        /// </summary>
        public NavigationItemCollections Items
        {
            get
            {
                if (this._Items == null)
                {
                    this._Items = new NavigationItemCollections(this);
                }
                return this._Items;
            }
        }

        private event NavigationEventHandler _OnNavigation;
        /// <summary>
        /// 当用户点击导航按钮时引发此事件。
        /// </summary>
        [Description("当用户点击导航按钮时引发此事件。"),]
        public event NavigationEventHandler OnNavigation
        {
            add
            {
                this._OnNavigation += value;
            }
            remove
            {
                this._OnNavigation -= value;
            }
        }
        private string _SplitString;
        /// <summary>
        /// 获取或设置导航项目之间的分隔符。
        /// </summary>
        [Description("导航项目之间的分隔符。"),
        DefaultValue(">"),]
        public string SplitString
        {
            get
            {
                return this._SplitString;
            }
            set
            {
                this._SplitString = value;
                this.RefreshData();
            }
        }
        private string _StartText;
        /// <summary>
        /// 获取或设置位于导航栏起始位置的文字。
        /// </summary>
        [Description("位于导航栏起始位置的文字。"),
        DefaultValue("当前位置："),]
        public string StartText
        {
            get
            {
                return this._StartText;
            }
            set
            {
                this._StartText = value;
                this.RefreshData();
            }
        }

        /// <summary>
        /// 获取或设置文本颜色。
        /// </summary>
        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                this.RefreshData();
            }
        }

        /// <summary>
        /// 获取或设置文本显示字体。
        /// </summary>
        public override Font Font
        {
            get
            {
                return base.Font;
            }
            set
            {
                base.Font = value;
                this.RefreshData();
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public NavigationBar()
        {
            this._SplitString = ">";
            this._StartText = "当前位置：";
            InitializeComponent();
        }

        //~NavigationBar()
        //{
        //    if (this._NavigationItems != null)
        //    {
        //        this._NavigationItems.Clear();
        //        this._NavigationItems._NavigationBar = null;
        //        this._NavigationItems = null;
        //    }
        //}

        /// <summary>
        /// 更新界面显示的数据。
        /// </summary>
        internal void RefreshData()
        {
            //if (this.DesignMode)
            //{
            //    return;
            //}
            System.Collections.Generic.List<System.Windows.Forms.Control> cs = new List<Control>();
            if (this._Items != null)
            {
                foreach (NavigationItem tmp in this.Items)
                {
                    System.Windows.Forms.Control c;
                    if (tmp.EnableLink)
                    {
                        System.Windows.Forms.LinkLabel lb = new LinkLabel();
                        c = lb;
                        lb.AutoSize = true;
                        lb.Margin = new Padding(0);
                        lb.Font = this.Font;
                        lb.LinkColor = lb.ForeColor = this.ForeColor;
                        lb.Text = tmp.Text;
                        lb.Name = tmp.ID;
                        lb.Tag = tmp;
                        lb.LinkClicked += delegate(object s, LinkLabelLinkClickedEventArgs e2)
                        {
                            if (this._OnNavigation != null)
                            {
                                this._OnNavigation(this, new NavigationEventArgs((NavigationItem)lb.Tag));
                            }
                        };
                    }
                    else
                    {
                        System.Windows.Forms.Label lb = new Label();
                        c = lb;
                        lb.AutoSize = true;
                        lb.Margin = new Padding(0);
                        lb.Font = this.Font;
                        lb.ForeColor = this.ForeColor;
                        lb.Text = tmp.Text;
                        lb.Name = tmp.ID;
                    }
                    if (cs.Count == 0)
                    {
                        cs.Add(c);
                    }
                    else
                    {
                        Label lb = new Label();
                        lb.AutoSize = true;
                        lb.Margin = new Padding(0);
                        lb.Font = this.Font;
                        lb.ForeColor = this.ForeColor;
                        lb.Text = this.SplitString;
                        cs.Add(lb);
                        cs.Add(c);
                    }
                }
            }

            if(string.IsNullOrEmpty(this.StartText) == false)
            {
                Label l = new Label();
                l.AutoSize = true;
                l.Margin = new Padding(0);
                l.Text = this.StartText;
                cs.Insert(0, l);
            }

            this.Controls.Clear();
            this.Controls.AddRange(cs.ToArray());
        }

        private event EventHandler _Load;
        /// <summary>
        /// 当加载控件时引发此事件。
        /// </summary>
        [Description("当加载控件时引发此事件。"),]
        public event EventHandler Load
        {
            add
            {
                this._Load += value;
            }
            remove
            {
                this._Load -= value;
            }
        }

        /// <summary>
        /// 引发控件加载事件。
        /// </summary>
        /// <param name="e"></param>
        [Description("引发控件加载事件。"),]
        public virtual void OnLoad(EventArgs e)
        {
            if (this._Load != null)
            {
                this._Load(this, e);
            }
        }

        /// <summary>
        /// 创建子控件。
        /// </summary>
        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.OnLoad(new EventArgs());

        }

        /// <summary>
        /// 获取导航路径的文本表示形式。
        /// </summary>
        /// <param name="split">分隔符。</param>
        /// <returns>导航项目的 ID 组合。</returns>
        public string GetNavigationPath(string split)
        {
            string path = "";
            foreach (var tmp in this.Items)
            {
                if (path.Length == 0)
                {
                    path = tmp.ID;
                }
                else
                {
                    path += split + tmp.ID;
                }
            }
            return path;

        }


        #region IDisposable 成员

        void IDisposable.Dispose()
        {
            if (this._Items != null)
            {
                this._Items.Clear();
                this._Items._NavigationBar = null;
                this._Items = null;
            }
        }

        #endregion
    }
}
