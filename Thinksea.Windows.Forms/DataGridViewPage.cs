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
    /// 带分页导航能力的数据视图控件。
    /// </summary>
    [
        DefaultEvent("PageIndexChange"),
    ]
    public partial class DataGridViewPage : UserControl
    {
        private System.Data.DataSet _DataSource;
        /// <summary>
        /// 获取 DataGridViewPage 所显示数据的数据源。
        /// </summary>
        [
        Category("Data"),
        Description("DataGridViewPage 所显示数据的数据源。"),
        ]
        public System.Data.DataSet DataSource
        {
            get
            {
                return this._DataSource;
            }
        }
        private BindingSource bindingSource;

        /// <summary>
        /// 当前数据分页试图帮定到的数据表。
        /// </summary>
        public DataTable CurrentDataTable
        {
            get
            {
                if (this.PagesCount > 0)
                {
                    return this.DataSource.Tables[this.CurrentPageIndex.ToString()];
                }
                return null;
            }
        }

        /// <summary>
        /// 获取 DataGridView 数据显示控件引用。
        /// </summary>
        [
        Category("Data"),
        Description("DataGridView 数据显示控件引用。"),
        ]
        public System.Windows.Forms.DataGridView DataGridView
        {
            get
            {
                return this.dataGridView1;
            }
        }

        /// <summary>
        /// 获取 PageNavigation 数据导航控件。
        /// </summary>
        [
        Category("Data"),
        Description("PageNavigation 数据导航控件引用。"),
        ]
        public PageNavigation PageNavigation
        {
            get
            {
                return this.pageNavigation1;
            }
        }

        /// <summary>
        /// 获取或设置一个值，用于指示是否允许用户一次选择多行记录。
        /// </summary>
        [
        Category("Appearance"),
        Description("指示是否允许用户一次选择多行记录。"),
        DefaultValue(false),
        ]
        public bool MultiSelect
        {
            get
            {
                return this.dataGridView1.MultiSelect;
            }
            set
            {
                this.dataGridView1.MultiSelect = value;
            }
        }

        /// <summary>
        /// 获取数据视图列。
        /// </summary>
        [
        Category("Data"),
        Description("数据视图列。"),
        ]
        public System.Windows.Forms.DataGridViewColumnCollection Columns
        {
            get
            {
                return this.DataGridView.Columns;
            }
        }

        /// <summary>
        /// 获取或设置当前分页索引。
        /// </summary>
        [
        Category("Data"),
        Description("当前分页索引。"),
        ReadOnly(true),
        ]
        public int CurrentPageIndex
        {
            get
            {
                if (string.IsNullOrEmpty(this.bindingSource.DataMember))
                {
                    return 0;
                }
                return System.Convert.ToInt32(this.bindingSource.DataMember);
            }
            set
            {
                #region 输入数据验证。
                if (value < 0)
                {
                    throw new System.ArgumentOutOfRangeException(nameof(value), "分页索引取值不能小于 0。");
                }
                #endregion

                #region 引发事件（例如：用户可以在此事件中对数据源追加数据，以满足显示指定索引分页）。
                if (this._OnRequestNewPage != null)
                {
                    if (value >= this.PagesCount && this.CanRequestNewPage) //如果请求了新页，则引发此事件，以便通知用户从服务器加载数据。
                    {
                        DataTable data = new DataTable();
                        RequestNewPageEventArgs pice = new RequestNewPageEventArgs(this.PagesCount * this.PageSize, this.PageSize, data);
                        this._OnRequestNewPage(this, pice);
                        if (data.Rows.Count > 0)
                        {
                            data.TableName = this.PagesCount.ToString();
                            data.RowDeleting += delegate(object rdsender, DataRowChangeEventArgs drcea)
                            {
                                if (this._RowDeleting != null)
                                {
                                    RemoveRowEventArgs rre = new RemoveRowEventArgs();
                                    rre.dataRow = drcea.Row;
                                    this._RowDeleting(this, rre);
                                }
                            };
                            if (this.DataSource.Tables.Contains(this.PagesCount.ToString()))
                            {
                                DataTable dtmp = this.DataSource.Tables[this.PagesCount.ToString()];
                                this.DataSource.Tables.Remove(dtmp);
                                dtmp.Dispose();
                            }
                            this.DataSource.Tables.Add(data);
                            //if (this.bindingSource.DataMember != value.ToString())
                            //{
                            this.bindingSource.DataMember = value.ToString();
                            //}
                        }
                        this.CanRequestNewPage = data.Rows.Count >= this.PageSize;
                        this.RebindData();
                        if (this._RequestNewPageSuccess != null)
                        {
                            this._RequestNewPageSuccess(this, new EventArgs());
                        }
                        return;
                    }
                }
                #endregion

                if (this._PageIndexChange != null)
                {
                    PageIndexChangeEventArgs pice = new PageIndexChangeEventArgs(this.CurrentPageIndex, value);
                    this._PageIndexChange(this, pice);
                    if (pice.Cancel)
                    {
                        return;
                    }
                }

                #region 输入数据验证。
                if ((this.PagesCount == 0 && value > 0) || (this.PagesCount > 0 && value >= this.PagesCount)) //如果有记录并且分页索引超出可用分页总数。
                {
                    throw new System.ArgumentOutOfRangeException(nameof(value), "分页索引取值不能大于当前可用的总页数。");
                }
                #endregion

                #region 执行操作。
                if (this.bindingSource.DataMember != value.ToString())
                {
                    this.bindingSource.DataMember = value.ToString();
                    //this.lbCurrentPageIndex.Text = string.Format(this.CurrentPageIndexText, this.PagesCount == 0 ? 0 : value + 1);
                }
                this.RebindData();
                #endregion
            }
        }

        /// <summary>
        /// 获取或设置当前选中的数据行。
        /// </summary>
        [
        Browsable(false),
        ]
        public DataRow [] CurrentSelectedRows
        {
            get
            {
                if (this.dataGridView1.SelectedRows.Count == 0)
                {
                    return null;
                }
                System.Collections.Generic.List<DataRow> l = new List<DataRow>();
                for (int i = this.dataGridView1.SelectedRows.Count - 1; i >= 0; i-- )
                {
                    DataGridViewRow tmp = this.dataGridView1.SelectedRows[i];
                    l.Add(((DataRowView)tmp.DataBoundItem).Row);
                }
                return l.ToArray();
            }
        }

        /// <summary>
        /// 获取可用记录总数。
        /// </summary>
        public int RecordsCount
        {
            get
            {
                int c = 0;
                System.Data.DataTableCollection dtc = this.DataSource.Tables;
                for (int i = 0; i < dtc.Count; i++)
                {
                    c += dtc[i].Rows.Count;
                }
                return c;
            }
        }

        private int _PageSize = 15;
        /// <summary>
        /// 获取或设置每页最大显示记录数。
        /// </summary>
        [
        Category("Data"),
        Description("每页最大显示记录数。"),
        DefaultValue(15),
        DesignOnly(true),
        ]
        public int PageSize
        {
            get
            {
                return this._PageSize;
            }
            set
            {
                if (value < 1)
                {
                    throw new System.Exception("每页最大显示记录数不能小于 1。");
                }
                this._PageSize = value;
                //if (this._PageSize == value)
                //{
                //    return;
                //}
                //if (this.HasChanges())
                //{
                //    if (this._LoseChanged != null)
                //    {
                //        LoseChangedEvent e = new LoseChangedEvent();
                //        this._LoseChanged(this, e);
                //        if (e.Cancel)
                //        {
                //            return;
                //        }
                //    }
                //    else
                //    {
                //        if (MessageBox.Show("此项操作将导致已修改数据无法存盘，是否放弃这些修改？", "丢失已经修改的数据", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) != DialogResult.OK)
                //        {
                //            return;
                //        }
                //    }
                //}
                //this._PageSize = value;
                //if (this.RecordsCount == 0 || (this.PagesCount == 1 && this.RecordsCount <= this._PageSize))
                //{
                //    return;
                //}
                //System.Data.DataTable dtFirst = new DataTable();
                //for (int i = 0; i < this.PagesCount; i++)
                //{
                //    dtFirst.Merge(this.DataSource.Tables[i.ToString()], true);
                //}
                //int pageIndex = 0;
                //while (pageIndex * this._PageSize < dtFirst.Rows.Count)
                //{
                //    DataTable dt = dtFirst.Clone();
                //    dt.TableName = pageIndex.ToString();
                //    this.CopyDataTable(dtFirst, dt, pageIndex * this._PageSize, this._PageSize);
                //    this.DataSource.Tables.Add(dt);
                //    pageIndex++;
                //}
                //dtFirst.Dispose();
                //this.CurrentPageIndex = 0;
            }
        }

        /// <summary>
        /// 获取可用分页总数。
        /// </summary>
        [
        Category("Data"),
        Description("分页总数。"),
        ReadOnly(true),
        ]
        public int PagesCount
        {
            get
            {
                return this.DataSource.Tables.Count;
            }
        }

        /// <summary>
        /// 获取或设置当前分页索引显示文本。
        /// </summary>
        [
        Category("Appearance"),
        Description("当前分页索引显示文本。"),
        DefaultValue("第 {0} 页"),
        ]
        public string CurrentPageIndexText
        {
            get
            {
                return this.pageNavigation1.PageIndexLabelFormatString;
            }
            set
            {
                this.pageNavigation1.PageIndexLabelFormatString = value;
                //this._CurrentPageIndexText = value;
                //this.lbCurrentPageIndex.Text = string.Format(value, this.PagesCount == 0 ? 0 : this.CurrentPageIndex + 1);
            }
        }

        /// <summary>
        /// 获取或设置“首页”按钮显示文本。
        /// </summary>
        [
        Category("Appearance"),
        Description("“首页”按钮显示文本。"),
        DefaultValue("首页"),
        ]
        public string FirstPageText
        {
            get
            {
                return this.pageNavigation1.FirstPageButton.Text;
            }
            set
            {
                this.pageNavigation1.FirstPageButton.Text = value;
            }
        }

        /// <summary>
        /// 获取或设置“上一页”按钮显示文本。
        /// </summary>
        [
        Category("Appearance"),
        Description("“上一页”按钮显示文本。"),
        DefaultValue("上一页"),
        ]
        public string PreviousPageText
        {
            get
            {
                return this.pageNavigation1.PreviousPageButton.Text;
            }
            set
            {
                this.pageNavigation1.PreviousPageButton.Text = value;
            }
        }

        /// <summary>
        /// 获取或设置“下一页”按钮显示文本。
        /// </summary>
        [
        Category("Appearance"),
        Description("“下一页”按钮显示文本。"),
        DefaultValue("下一页"),
        ]
        public string NextPageText
        {
            get
            {
                return this.pageNavigation1.NextPageButton.Text;
            }
            set
            {
                this.pageNavigation1.NextPageButton.Text = value;
            }
        }

        /// <summary>
        /// 获取或设置“尾页”按钮显示文本。
        /// </summary>
        [
        Category("Appearance"),
        Description("“尾页”按钮显示文本。"),
        DefaultValue("尾页"),
        ]
        public string LastPageText
        {
            get
            {
                return this.pageNavigation1.LastPageButton.Text;
            }
            set
            {
                this.pageNavigation1.LastPageButton.Text = value;
            }
        }

        /// <summary>
        /// 指示是否能够获取新的分页数据。用于判断是否已经加载完全部的数据。
        /// </summary>
        private bool CanRequestNewPage = true;

        /// <summary>
        /// 获取或设置一个值，用于指示是否显示导航条控件。
        /// </summary>
        [
        Category("Appearance"),
        Description("是否显示导航条控件。"),
        DefaultValue(true),
        ]
        public bool ShowPageNavigatorBar
        {
            get
            {
                return this.pageNavigation1.Visible;
            }
            set
            {
                this.pageNavigation1.Visible = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值，用于指示删除按钮是否可见。
        /// </summary>
        [
        Category("Appearance"),
        Description("删除按钮是否可见。"),
        DefaultValue(true),
        ]
        public bool ShowDeleteButton
        {
            get
            {
                return this.bindingNavigatorDeleteItem.Available;
            }
            set
            {
                this.bindingNavigatorDeleteItem.Available = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值，用于指示添加新记录按钮是否可见。
        /// </summary>
        [
        Category("Appearance"),
        Description("添加新记录按钮是否可见。"),
        DefaultValue(true),
        ]
        public bool ShowAddNewButton
        {
            get
            {
                return this.bindingNavigatorAddNewItem.Available;
            }
            set
            {
                this.bindingNavigatorAddNewItem.Available = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值，用于指示保存记录按钮是否可见。
        /// </summary>
        [
        Category("Appearance"),
        Description("保存记录按钮是否可见。"),
        DefaultValue(true),
        ]
        public bool ShowSaveButton
        {
            get
            {
                return this.toolStripButtonSave.Available;
            }
            set
            {
                this.toolStripButtonSave.Available = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值，用于指示刷新数据按钮是否可见。
        /// </summary>
        [
        Category("Appearance"),
        Description("刷新数据按钮是否可见。"),
        DefaultValue(true),
        ]
        public bool ShowRefreshDataButton
        {
            get
            {
                return this.toolStripButtonRefresh.Available;
            }
            set
            {
                this.toolStripButtonRefresh.Available = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值，用于指示是否显示分页导航控制按钮。
        /// </summary>
        [
        Category("Appearance"),
        Description("是否显示分页导航控制按钮。"),
        DefaultValue(true),
        ]
        public bool ShowPageControls
        {
            get
            {
                return this.pageNavigation1.ShowPageControlButtons;
            }
            set
            {
                this.pageNavigation1.ShowPageControlButtons = value;
            }
        }

        /// <summary>
        /// 获取或设置一个值，用于指示是否显示行记录编号导航控制按钮。
        /// </summary>
        [
        Category("Appearance"),
        Description("是否显示行记录编号导航控制按钮。"),
        DefaultValue(true),
        ]
        public bool ShowRowNumberControls
        {
            get
            {
                return this.pageNavigation1.ShowLineNumberTextBox;
            }
            set
            {
                this.pageNavigation1.ShowLineNumberTextBox = value;
            }
        }

        /// <summary>
        /// 获取或设置导航条的停靠位置。
        /// </summary>
        [
        Category("Appearance"),
        Description("导航条的停靠位置。"),
        DefaultValue(DockStyle.Bottom),
        ]
        public DockStyle NavigatorBarDock
        {
            get
            {
                return this.pageNavigation1.Dock;
            }
            set
            {
                this.pageNavigation1.Dock = value;
            }
        }

        private bool _CustomSave = false;
        /// <summary>
        /// 指示是否由用户处理已变更数据的保存任务。这将导致控件忽略对已变更的数据的保存任务。
        /// </summary>
        [
        Category("Appearance"),
        Description("指示是否由用户处理已变更数据的保存任务。这将导致控件忽略对已变更的数据的保存任务。"),
        DefaultValue(false),
        ]
        public bool CustomSave
        {
            get
            {
                return this._CustomSave;
            }
            set
            {
                this._CustomSave = value;
            }
        }

        private event PageIndexChangeEventHandler _PageIndexChange;
        /// <summary>
        /// 当分页更改时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("当分页更改时引发此事件。"),
        ]
        public event PageIndexChangeEventHandler PageIndexChange
        {
            add
            {
                this._PageIndexChange += value;
            }
            remove
            {
                this._PageIndexChange -= value;
            }
        }

        private event RequestNewPageEventHandler _OnRequestNewPage;
        /// <summary>
        /// 当请求新分页时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("当请求新分页时引发此事件。"),
        ]
        public event RequestNewPageEventHandler OnRequestNewPage
        {
            add
            {
                this._OnRequestNewPage += value;
            }
            remove
            {
                this._OnRequestNewPage -= value;
            }
        }

        private event RequestNewPageSuccessEventHandler _RequestNewPageSuccess;
        /// <summary>
        /// 当请求新分页完成时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("当请求新分页完成时引发此事件。"),
        ]
        public event RequestNewPageSuccessEventHandler RequestNewPageSuccess
        {
            add
            {
                this._RequestNewPageSuccess += value;
            }
            remove
            {
                this._RequestNewPageSuccess -= value;
            }
        }

        private event System.EventHandler _RequestFirstPageEvent;
        /// <summary>
        /// 当第一次请求数据完成时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("当第一次请求数据完成时引发此事件。"),
        ]
        public event System.EventHandler RequestFirstPageEvent
        {
            add
            {
                this._RequestFirstPageEvent += value;
            }
            remove
            {
                this._RequestFirstPageEvent -= value;
            }
        }

        private event DataGridViewCellEventHandler _CellDoubleClick;
        /// <summary>
        /// 双击单元格时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("双击单元格时引发此事件。"),
        ]
        public event DataGridViewCellEventHandler CellDoubleClick
        {
            add
            {
                this._CellDoubleClick += value;
            }
            remove
            {
                this._CellDoubleClick -= value;
            }
        }

        private event AddNewRowEventHandler _AddNewRowClick;
        /// <summary>
        /// 点击添加数据按钮时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("点击添加数据按钮时引发此事件。"),
        ]
        public event AddNewRowEventHandler AddNewRowClick
        {
            add
            {
                this._AddNewRowClick += value;
            }
            remove
            {
                this._AddNewRowClick -= value;
            }
        }

        private event RemoveRowEventHandler _DeleteRowClick;
        /// <summary>
        /// 当点击删除按钮删除数据时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("当点击删除按钮删除数据时引发此事件。"),
        ]
        public event RemoveRowEventHandler DeleteRowClick
        {
            add
            {
                this._DeleteRowClick += value;
            }
            remove
            {
                this._DeleteRowClick -= value;
            }
        }

        private event RemoveRowEventHandler _RowDeleting;
        /// <summary>
        /// 当删除数据时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("当删除数据时引发此事件。"),
        ]
        public event RemoveRowEventHandler RowDeleting
        {
            add
            {
                this._RowDeleting += value;
            }
            remove
            {
                this._RowDeleting -= value;
            }
        }

        private event SaveChangedEventHandler _SaveChanged;
        /// <summary>
        /// 当需要保存已更改数据时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("当需要保存已更改数据时引发此事件。"),
        ]
        public event SaveChangedEventHandler SaveChanged
        {
            add
            {
                this._SaveChanged += value;
            }
            remove
            {
                this._SaveChanged -= value;
            }
        }

        private event DataGridViewColumnEventHandler _ColumnAdded;
        /// <summary>
        /// 向控件添加列时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("向控件添加列时引发此事件。"),
        ]
        public event DataGridViewColumnEventHandler ColumnAdded
        {
            add
            {
                this._ColumnAdded += value;
            }
            remove
            {
                this._ColumnAdded -= value;
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public DataGridViewPage()
        {
            InitializeComponent();

            this.dataGridView1.CellDoubleClick += delegate(object sender, DataGridViewCellEventArgs e)
            {
                if (this._CellDoubleClick != null)
                {
                    this._CellDoubleClick(sender, e);
                }

            };

            this.dataGridView1.ColumnAdded += delegate(object sender, DataGridViewColumnEventArgs e)
            {
                if (this._ColumnAdded != null)
                {
                    this._ColumnAdded(this, e);
                }
            };
            this.dataGridView1.SelectionChanged += delegate(object sender, EventArgs e)
            {
                this.RebindData();
            };

            this.pageNavigation1.OnLineNumberChanging += new LineNumberChangingEventHandler(pageNavigation1_OnLineNumberChanging);
            this.pageNavigation1.OnPageIndexChanging += new PageIndexChangingEventHandler(pageNavigation1_OnPageIndexChanging);
            //this.lbFirstPage.Click += delegate(object sender, EventArgs e)
            //{
            //    this.CurrentPageIndex = 0;

            //};

            //this.lbPreviousPage.Click += delegate(object sender, EventArgs e)
            //{
            //    int newPageIndex = this.CurrentPageIndex - 1;
            //    if (newPageIndex > this.PagesCount - 1)
            //    {
            //        newPageIndex = this.PagesCount - 1;
            //    }
            //    if (newPageIndex < 0)
            //    {
            //        newPageIndex = 0;
            //    }
            //    this.CurrentPageIndex = newPageIndex;

            //};

            //this.lbNextPage.Click += delegate(object sender, EventArgs e)
            //{
            //    int newPageIndex = this.CurrentPageIndex + 1;
            //    //if (newPageIndex > this.PagesCount - 1)
            //    //{
            //    //    newPageIndex = this.PagesCount - 1;
            //    //}
            //    //if (newPageIndex < 0)
            //    //{
            //    //    newPageIndex = 0;
            //    //}
            //    this.CurrentPageIndex = newPageIndex;

            //};

            //this.lbLastPage.Click += delegate(object sender, EventArgs e)
            //{
            //    int newPageIndex = this.PagesCount - 1;
            //    if (newPageIndex < 0)
            //    {
            //        newPageIndex = 0;
            //    }
            //    this.CurrentPageIndex = newPageIndex;

            //};

            this.bindingNavigatorAddNewItem.Click += delegate(object sender, EventArgs e)
            {
                this.DoAddNewRow();
            };

            this.bindingNavigatorDeleteItem.Click += delegate(object sender, EventArgs e)
            {
                this.DoRemoveRow();
            };

            this.toolStripButtonSave.Click += delegate(object sender, EventArgs e)
            {
                this.DoSave();
            };

            this.toolStripButtonRefresh.Click += delegate(object sender, EventArgs e)
            {
                this.DoRefreshData();
            };


            this._DataSource = new DataSet();
            bindingSource = new BindingSource();
            bindingSource.DataSource = this.DataSource;
            bindingSource.DataSourceChanged += delegate(object sender, EventArgs e)
            {
                this.pageNavigation1.RecordsCount = bindingSource.Count;
            };

            bindingSource.DataMemberChanged += delegate(object sender, EventArgs e)
            {
                this.pageNavigation1.RecordsCount = bindingSource.Count;
            };

            //bindingSource.CurrentItemChanged += delegate(object sender, EventArgs e)
            //{
            //    RebindData();

            //};
            bindingSource.PositionChanged += delegate(object sender, EventArgs e)
            {
                this.pageNavigation1.LineNumberTextBox.Text = (bindingSource.Position + 1).ToString();
            };

            this.dataGridView1.DataSource = bindingSource;
            //this.bindingNavigator1.BindingSource = bindingSource;
            //this.CurrentPageIndexText = "第 {0} 页";
            if (!base.DesignMode)
            {
                RebindData();
            }

        }

        void pageNavigation1_OnPageIndexChanging(object sender, PageIndexChangingEventArgs e)
        {
            //if(this.CurrentPageIndex != e.NewPageIndex)
            this.CurrentPageIndex = e.NewPageIndex;

        }

        void pageNavigation1_OnLineNumberChanging(object sender, LineNumberChangingEventArgs e)
        {
            this.bindingSource.Position = e.LineNumber - 1;
        }

        /// <summary>
        /// 按照预先设置好的分页设置显示记录。
        /// </summary>
        private void RebindData()
        {
            if (this.RecordsCount == 0)
            {
                this.pageNavigation1.RecordsCount = 0;
                //this.lbPreviousPage.Enabled = this.lbFirstPage.Enabled = false;
                //this.lbNextPage.Enabled = this.lbLastPage.Enabled = false;
                //this.lbCurrentPageIndex.Text = string.Format(this.CurrentPageIndexText, 0);
                this.bindingNavigatorDeleteItem.Enabled = false;
            }
            else
            {
                //if (this.CanRequestNewPage)
                //{
                //    this.lbNextPage.Enabled = true;
                //    this.lbLastPage.Enabled = true;
                //}
                //else
                //{
                //    this.lbNextPage.Enabled = this.lbLastPage.Enabled = (this.CurrentPageIndex < this.PagesCount - 1);
                //}
                //this.lbPreviousPage.Enabled = this.lbFirstPage.Enabled = (this.CurrentPageIndex > 0);
                //this.lbCurrentPageIndex.Text = string.Format(this.CurrentPageIndexText, this.PagesCount == 0 ? 0 : this.CurrentPageIndex + 1);
                ////this.bindingNavigatorDeleteItem.Enabled = (this.dgv_colligation.CurrentRow != null);
                this.pageNavigation1.RecordsCount = this.RecordsCount;
                this.bindingNavigatorDeleteItem.Enabled = (this.dataGridView1.SelectedRows.Count > 0);
            }
            this.bindingNavigatorAddNewItem.Enabled = this.CurrentDataTable != null;
            this.toolStripButtonSave.Enabled = this.HasChanges();

        }

        /// <summary>
        /// 判断是否有新增、修改、删除的数据行。
        /// </summary>
        /// <returns></returns>
        public bool HasChanges()
        {
            return this.DataSource.HasChanges();
        }

        /// <summary>
        /// 跳转到最后有效页索引。
        /// </summary>
        public void GoToLastPage()
        {
            if (this.PagesCount == 0)
            {
                this.CurrentPageIndex = 0;
            }
            else
            {
                this.CurrentPageIndex = this.PagesCount - 1;
            }
        }

        /// <summary>
        /// 清理所有数据。
        /// </summary>
        public void ClearData()
        {
            if (this.DataSource != null)
            {
                this.DataSource.Clear();
                this.DataSource.Tables.Clear();
                this.DataSource.AcceptChanges();
                this.bindingSource.DataMember = "";
                this.CanRequestNewPage = true;
                this.RebindData();
            }
        }

        /// <summary>
        /// 引发一个刷新数据事件。
        /// </summary>
        public void DoRefreshData()
        {
            if (!this.CustomSave && this.HasChanges())
            {
                switch (MessageBox.Show(this, "数据已经变更，需要保存吗？", "", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1))
                {
                    case DialogResult.Yes:
                        this.DoSave();
                        if (this.HasChanges()) //如果执行保存操作后仍然有未保存的数据（例如保存数据失败）则结束此方法。
                        {
                            return;
                        }
                        break;
                    case DialogResult.No:
                        break;
                    case DialogResult.Cancel:
                    default:
                        return;
                }
            }

            if (this._OnRequestNewPage != null)
            {
                this.ClearData();
                //if (this.CurrentPageIndex >= this.PagesCount) //如果请求了新页，则引发此事件，以便通知用户从服务器加载数据。
                {
                    DataTable data = new DataTable();
                    RequestNewPageEventArgs pice = new RequestNewPageEventArgs(0, this.PageSize, data);
                    this._OnRequestNewPage(this, pice);
                    //if (data.Rows.Count > 0)
                    {//没有数据也要显示表头。
                        data.TableName = this.CurrentPageIndex.ToString();
                        data.RowDeleting += delegate(object rdsender, DataRowChangeEventArgs drcea)
                        {
                            if (this._RowDeleting != null)
                            {
                                RemoveRowEventArgs rre = new RemoveRowEventArgs();
                                rre.dataRow = drcea.Row;
                                this._RowDeleting(this, rre);
                            }
                        };
                        if (this.DataSource.Tables.Contains(this.CurrentPageIndex.ToString()))
                        {
                            DataTable dtmp = this.DataSource.Tables[this.CurrentPageIndex.ToString()];
                            this.DataSource.Tables.Remove(dtmp);
                            dtmp.Dispose();
                        }
                        this.DataSource.Tables.Add(data);
                        //{
                        //    this.bindingSource.DataMember = this.CurrentPageIndex.ToString();
                        //}
                        this.CanRequestNewPage = data.Rows.Count >= this.PageSize;
                        this.CurrentPageIndex = 0;
                        //this.RebindData();
                        if (this._RequestFirstPageEvent != null)
                        {
                            this._RequestFirstPageEvent(this, new EventArgs());
                        }
                        if (this._RequestNewPageSuccess != null)
                        {
                            this._RequestNewPageSuccess(this, new EventArgs());
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show(this, "没有处理 OnRequestNewPage 事件，这将导致永远没有可用数据！", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        /// <summary>
        /// 引发一个保存已变更数据事件。
        /// </summary>
        public void DoSave()
        {
            if (this._SaveChanged != null)
            {
                //System.Data.DataSet ds = this.DataSource.GetChanges();
                //if (ds == null)
                //{
                //    return;
                //}
                //foreach (DataTable tmp in ds.Tables) //循环保存每一张表。
                foreach (DataTable stable in this.DataSource.Tables) //循环保存每一张表。
                {
                    System.Data.DataTable tmp = stable.GetChanges();
                    if (tmp == null || tmp.Rows.Count == 0)
                    {
                        continue;
                    }
                    SaveChangedEventArgs anre = new SaveChangedEventArgs();
                    anre.Data = tmp;
                    anre.PageIndex = System.Convert.ToInt32(tmp.TableName);
                    try
                    {
                        this._SaveChanged(this, anre);
                        if (anre.IsSuccess)
                        {
                            stable.AcceptChanges();
                        }
                    }
                    catch (System.Exception ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }
                }
                //this.DataSource.AcceptChanges();
            }
            else
            {
                MessageBox.Show(this, "没有处理保存数据事件，这将导致数据变更丢失！", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 引发一个添加新记录事件。
        /// </summary>
        public void DoAddNewRow()
        {
            if (this.CurrentDataTable == null)
            {
                return;
            }

            if (this._AddNewRowClick != null)
            {
                AddNewRowEventArgs anre = new AddNewRowEventArgs();
                anre.dataTable = this.CurrentDataTable;
                this._AddNewRowClick(this, anre);
            }
            else
            {
                this.bindingSource.AddNew();
            }

        }

        /// <summary>
        /// 引发一个删除选中记录的事件。
        /// </summary>
        public void DoRemoveRow()
        {
            if (this.dataGridView1.SelectedRows.Count == 0)
            {
                return;
            }

            System.Collections.Generic.List<DataRow> l = new List<DataRow>();
            foreach (DataGridViewRow tmp in this.dataGridView1.SelectedRows)
            {
                l.Add(((DataRowView)tmp.DataBoundItem).Row);
            }
            if (this._DeleteRowClick != null)
            {
                foreach (var tmp in l)
                {
                    RemoveRowEventArgs rre = new RemoveRowEventArgs();
                    rre.dataRow = tmp;
                    this._DeleteRowClick(this, rre);
                }
            }
            else
            {
                foreach (var tmp in l)
                {
                    tmp.Delete();
                }
            }
        }

        private void timer状态维护_Tick(object sender, EventArgs e)
        {
            //this.bindingNavigatorDeleteItem.Enabled = (this.dgv_colligation.CurrentRow != null);
            //this.bindingNavigatorDeleteItem.Enabled = (this.dgv_colligation.SelectedRows.Count > 0);
            this.toolStripButtonSave.Enabled = this.HasChanges();
        }

        ///// <summary>
        ///// 加载位于指定分页的数据。
        ///// </summary>
        ///// <param name="pageIndex">分页索引编号。从 0 开始的整数。</param>
        //public void RequestPageData(int pageIndex)
        //{
        //    #region 引发事件（例如：用户可以在此事件中对数据源追加数据，以满足显示指定索引分页）。
        //    if (this._OnRequestNewPage != null)
        //    {
        //        //if (pageIndex >= this.PagesCount && this.CanRequestNewPage) //如果请求了新页，则引发此事件，以便通知用户从服务器加载数据。
        //        //{
        //            DataTable data = new DataTable();
        //            RequestNewPageEventArgs pice = new RequestNewPageEventArgs(pageIndex * this.PageSize, this.PageSize + 1, data); //多获取 1 条记录，用于判断是否还有新的数据分页可用。
        //            this._OnRequestNewPage(this, pice);
        //            if (pice.Cancel)
        //            {
        //                return;
        //            }
        //            if (data.Rows.Count > 0)
        //            {
        //                data.Rows[data.Rows.Count - 1].Delete();
        //                data.Rows[data.Rows.Count - 1].AcceptChanges();

        //                data.TableName = pageIndex.ToString();
        //                data.RowDeleting += delegate(object rdsender, DataRowChangeEventArgs drcea)
        //                {
        //                    if (this._RowDeleting != null)
        //                    {
        //                        RemoveRowEventArgs rre = new RemoveRowEventArgs();
        //                        rre.dataRow = drcea.Row;
        //                        this._RowDeleting(this, rre);
        //                    }
        //                };
        //                if (this.DataSource.Tables.Contains(pageIndex.ToString())) //删除指定分页编号的表。
        //                {
        //                    DataTable dtmp = this.DataSource.Tables[this.DataSource.Tables.IndexOf(pageIndex.ToString())];
        //                    this.DataSource.Tables.Remove(dtmp);
        //                    dtmp.Dispose();
        //                }
        //                this.DataSource.Tables.Add(data);
        //                //if (this.bindingSource.DataMember != value.ToString())
        //                //{
        //                this.bindingSource.DataMember = pageIndex.ToString();
        //                //}
        //            }
        //            this.CanRequestNewPage = data.Rows.Count >= this.PageSize;
        //            this.RebindData();
        //            if (this._RequestNewPageSuccess != null)
        //            {
        //                this._RequestNewPageSuccess(this, new EventArgs());
        //            }
        //            return;
        //        //}
        //    }
        //    #endregion
        //}

    }

    /// <summary>
    /// 分页更改事件代理。
    /// </summary>
    /// <param name="sender">引发此事件的控件。</param>
    /// <param name="e">分页更改前后的相关数据。</param>
    public delegate void PageIndexChangeEventHandler(object sender, PageIndexChangeEventArgs e);

    /// <summary>
    /// 请求新分页事件代理。
    /// </summary>
    /// <param name="sender">引发此事件的控件。</param>
    /// <param name="e">分页更改前后的相关数据。</param>
    public delegate void RequestNewPageEventHandler(object sender, RequestNewPageEventArgs e);

    /// <summary>
    /// 请求新分页完成事件代理。
    /// </summary>
    /// <param name="sender">引发此事件的控件。</param>
    /// <param name="e">为事件提供数据。</param>
    public delegate void RequestNewPageSuccessEventHandler(object sender, EventArgs e);

    /// <summary>
    /// 添加新数据记录事件代理。
    /// </summary>
    public delegate void AddNewRowEventHandler(object sender, AddNewRowEventArgs e);

    /// <summary>
    /// 删除数据记录事件代理。
    /// </summary>
    public delegate void RemoveRowEventHandler(object sender, RemoveRowEventArgs e);

    /// <summary>
    /// 保存已更改数据记录事件代理。
    /// </summary>
    public delegate void SaveChangedEventHandler(object sender, SaveChangedEventArgs e);

    /// <summary>
    /// 分页索引准备更改事件数据。
    /// </summary>
    public class PageIndexChangeEventArgs : System.EventArgs
    {
        /// <summary>
        /// 当前分页索引。
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 请求的新的分页索引。
        /// </summary>
        public int NewPageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 是否应该取消分页更改事件。
        /// </summary>
        public bool Cancel
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public PageIndexChangeEventArgs()
        {
            this.Cancel = false;

        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="PageIndex">当前分页索引。</param>
        /// <param name="NewPageIndex">新的分页索引。</param>
        public PageIndexChangeEventArgs(int PageIndex, int NewPageIndex)
        {
            this.Cancel = false;
            this.PageIndex = PageIndex;
            this.NewPageIndex = NewPageIndex;

        }

    }

    /// <summary>
    /// 请求新的分页事件数据。
    /// </summary>
    public class RequestNewPageEventArgs : System.EventArgs
    {
        /// <summary>
        /// 请求记录的起始索引。
        /// </summary>
        public int StartIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 最多请求的记录数量。
        /// </summary>
        public int MaxCount
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置数据容器，用于向控件提供所需数据。
        /// </summary>
        public DataTable Data
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或设置一个值，用于指示是否应该取消操作。
        /// </summary>
        public bool Cancel
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public RequestNewPageEventArgs()
        {
            this.StartIndex = 0;
            this.MaxCount = 0;
            this.Cancel = false;
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="StartIndex">请求记录的起始索引。</param>
        /// <param name="MaxCount">请求记录的数量。</param>
        /// <param name="Data">用于向控件提供所需数据。</param>
        public RequestNewPageEventArgs(int StartIndex, int MaxCount, DataTable Data)
        {
            this.StartIndex = StartIndex;
            this.MaxCount = MaxCount;
            this.Data = Data;
            this.Cancel = false;

        }

    }

    /// <summary>
    /// 添加新数据记录事件。
    /// </summary>
    public class AddNewRowEventArgs : System.EventArgs
    {
        /// <summary>
        /// 新数据应该添加到这个集合中。
        /// </summary>
        public System.Data.DataTable dataTable
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public AddNewRowEventArgs()
        {

        }

    }

    /// <summary>
    /// 删除数据记录事件。
    /// </summary>
    public class RemoveRowEventArgs:System.EventArgs
    {
        /// <summary>
        /// 被删除的数据行。
        /// </summary>
        public System.Data.DataRow dataRow
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public RemoveRowEventArgs()
        {

        }

    }

    /// <summary>
    /// 保存已更改数据记录事件。
    /// </summary>
    public class SaveChangedEventArgs : System.EventArgs
    {
        /// <summary>
        /// 数据所属分页索引。
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 已经产生数据变更的数据表。
        /// </summary>
        public System.Data.DataTable Data
        {
            get;
            set;
        }

        /// <summary>
        /// 向数据视图控件发出一个通知，指示操作是否成功。如果为 True 则会导致控件接受用户对数据源的操作。
        /// </summary>
        public bool IsSuccess
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public SaveChangedEventArgs()
        {
            this.IsSuccess = true;

        }

    }

}
