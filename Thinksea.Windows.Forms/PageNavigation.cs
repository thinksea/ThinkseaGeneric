using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace Thinksea.Windows.Forms
{
    /// <summary>
    /// 一个分页导航控件。
    /// </summary>
    public class PageNavigation : System.Windows.Forms.ToolStrip
    {
        /// <summary>
        /// 左侧空白占位符。
        /// </summary>
        [
        Browsable(false),
        ]
        private ToolStripLabel LeftSpace;

        private ToolStripSeparator _Separator1;
        /// <summary>
        /// 位于“共X条记录”按钮与“首页”按钮中间的分割线。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripSeparator Separator1
        {
            get
            {
                return this._Separator1;
            }
        }

        private ToolStripSeparator _Separator2;
        /// <summary>
        /// 位于“尾页”按钮与“第X页”按钮中间的分割线。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripSeparator Separator2
        {
            get
            {
                return this._Separator2;
            }
        }

        private ToolStripLabel _LineNumberLabel;
        /// <summary>
        /// 获取“行号”文本标签。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripLabel LineNumberLabel
        {
            get
            {
                return this._LineNumberLabel;
            }
        }

        private ToolStripTextBox _LineNumberTextBox;
        /// <summary>
        /// 获取“行号”文本输入框。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripTextBox LineNumberTextBox
        {
            get
            {
                return this._LineNumberTextBox;
            }
        }

        /// <summary>
        /// 获取或设置是否显示“行号”文本输入框。
        /// </summary>
        [
        Category("Appearance"),
        Description("是否显示“行号”文本输入框。"),
        DefaultValue(true),
        ReadOnly(false),
        ]
        public bool ShowLineNumberTextBox
        {
            get
            {
                return this._LineNumberTextBox.Available;
            }
            set
            {
                if (this._LineNumberTextBox.Available == value)
                {
                    return;
                }

                this._LineNumberTextBox.Available = value;
                this._LineNumberLabel.Available = value;
                this.FixItemsPosition();

            }
        }

        /// <summary>
        /// 获取或设置“行号”文本标签说明。
        /// </summary>
        [
        Category("Appearance"),
        Description("“行号”文本标签说明。"),
        DefaultValue("行号"),
        ReadOnly(false),
        ]
        public string LineNumberLabelText
        {
            get
            {
                return this._LineNumberLabel.Text;
            }
            set
            {
                if (this._LineNumberLabel.Text == value)
                {
                    return;
                }

                this._LineNumberLabel.Text = value;
                this.FixItemsPosition();

            }
        }

        /// <summary>
        /// 用于保存行号输入框最后输入的有效数据。
        /// </summary>
        private string LineNumberTextBoxLastValue = string.Empty;

        private ToolStripButton _FirstPageButton;
        /// <summary>
        /// 获取“首页”按钮。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripButton FirstPageButton
        {
            get
            {
                return this._FirstPageButton;
            }
        }

        private ToolStripButton _PreviousPageGroupButton;
        /// <summary>
        /// 获取“上一组分页”按钮。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripButton PreviousPageGroupButton
        {
            get
            {
                return this._PreviousPageGroupButton;
            }
        }

        private ToolStripButton _PreviousPageButton;
        /// <summary>
        /// 获取“上一页”按钮。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripButton PreviousPageButton
        {
            get
            {
                return this._PreviousPageButton;
            }
        }

        private ToolStripButton _NextPageButton;
        /// <summary>
        /// 获取“下一页”按钮。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripButton NextPageButton
        {
            get
            {
                return this._NextPageButton;
            }
        }

        private ToolStripButton _NextPageGroupButton;
        /// <summary>
        /// 获取“下一组分页”按钮。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripButton NextPageGroupButton
        {
            get
            {
                return this._NextPageGroupButton;
            }
        }

        private ToolStripButton _LastPageButton;
        /// <summary>
        /// 获取“尾页”按钮。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripButton LastPageButton
        {
            get
            {
                return this._LastPageButton;
            }
        }

        private ToolStripLabel _PageIndexLabel;
        /// <summary>
        /// 获取“第X页”按钮。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripLabel PageIndexLabel
        {
            get
            {
                return this._PageIndexLabel;
            }
        }

        private string _PageIndexLabelFormatString = "第 {0} 页";
        /// <summary>
        /// 获取或设置当前分页说明格式化字符串。
        /// </summary>
        [
        Category("Appearance"),
        Description("当前分页说明格式化字符串。"),
        DefaultValue("第 {0} 页"),
        ReadOnly(false),
        ]
        public string PageIndexLabelFormatString
        {
            get
            {
                return this._PageIndexLabelFormatString;
            }
            set
            {
                if (this._PageIndexLabelFormatString == value)
                {
                    return;
                }

                this._PageIndexLabelFormatString = value;
                this.RefreshPageNumberButtons(true);

            }
        }

        /// <summary>
        /// 获取或设置是否显示当前分页说明。
        /// </summary>
        [
        Category("Appearance"),
        Description("是否显示当前分页说明。"),
        DefaultValue(true),
        ReadOnly(false),
        ]
        public bool ShowPageIndexLabel
        {
            get
            {
                return this._PageIndexLabel.Available;
            }
            set
            {
                if (this._PageIndexLabel.Available == value)
                {
                    return;
                }

                this._PageIndexLabel.Available = value;
                this.RefreshPageNumberButtons(true);

            }
        }

        private ToolStripLabel _PagesCountLabel;
        /// <summary>
        /// 获取“共X页”按钮。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripLabel PagesCountLabel
        {
            get
            {
                return this._PagesCountLabel;
            }
        }

        private string _PagesCountLabelFormatString = "共 {0} 页";
        /// <summary>
        /// 获取或设置分页总数说明格式化字符串。
        /// </summary>
        [
        Category("Appearance"),
        Description("分页总数说明格式化字符串。"),
        DefaultValue("共 {0} 页"),
        ReadOnly(false),
        ]
        public string PagesCountLabelFormatString
        {
            get
            {
                return this._PagesCountLabelFormatString;
            }
            set
            {
                if (this._PagesCountLabelFormatString == value)
                {
                    return;
                }

                this._PagesCountLabelFormatString = value;
                this.RefreshPageNumberButtons(true);

            }
        }

        /// <summary>
        /// 获取或设置是否显示分页总数说明。
        /// </summary>
        [
        Category("Appearance"),
        Description("是否显示分页总数说明。"),
        DefaultValue(true),
        ReadOnly(false),
        ]
        public bool ShowPagesCountLabel
        {
            get
            {
                return this._PagesCountLabel.Available;
            }
            set
            {
                if (this._PagesCountLabel.Available == value)
                {
                    return;
                }

                this._PagesCountLabel.Available = value;
                this.FixItemsPosition();

            }
        }

        private ToolStripLabel _PageSizeLabel;
        /// <summary>
        /// 获取“本页X条记录”按钮。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripLabel PageSizeLabel
        {
            get
            {
                return this._PageSizeLabel;
            }
        }

        private string _PageSizeLabelFormatString = "本页 {0} 条";
        /// <summary>
        /// 获取或设置当前分页记录数量说明格式化字符串。
        /// </summary>
        [
        Category("Appearance"),
        Description("当前分页记录数量说明格式化字符串。"),
        DefaultValue("本页 {0} 条"),
        ReadOnly(false),
        ]
        public string PageSizeLabelFormatString
        {
            get
            {
                return this._PageSizeLabelFormatString;
            }
            set
            {
                if (this._PageSizeLabelFormatString == value)
                {
                    return;
                }

                this._PageSizeLabelFormatString = value;
                this.RefreshPageNumberButtons(true);

            }
        }

        /// <summary>
        /// 获取或设置是否显示当前分页记录数量说明。
        /// </summary>
        [
        Category("Appearance"),
        Description("是否显示当前分页记录数量说明。"),
        DefaultValue(true),
        ReadOnly(false),
        ]
        public bool ShowPageSizeLabel
        {
            get
            {
                return this._PageSizeLabel.Available;
            }
            set
            {
                if (this._PageSizeLabel.Available == value)
                {
                    return;
                }

                this._PageSizeLabel.Available = value;
                this.FixItemsPosition();

            }
        }

        private ToolStripLabel _RecordsCountLabel;
        /// <summary>
        /// 获取“共X条记录”按钮。
        /// </summary>
        [
        Browsable(false),
        ]
        public ToolStripLabel RecordsCountLabel
        {
            get
            {
                return this._RecordsCountLabel;
            }
        }

        private string _RecordsCountLabelFormatString = "共 {0} 条";
        /// <summary>
        /// 获取或设置记录总数说明格式化字符串。
        /// </summary>
        [
        Category("Appearance"),
        Description("记录总数说明格式化字符串。"),
        DefaultValue("共 {0} 条"),
        ReadOnly(false),
        ]
        public string RecordsCountLabelFormatString
        {
            get
            {
                return this._RecordsCountLabelFormatString;
            }
            set
            {
                if (this._RecordsCountLabelFormatString == value)
                {
                    return;
                }

                this._RecordsCountLabelFormatString = value;
                this.RefreshPageNumberButtons(true);

            }
        }

        /// <summary>
        /// 获取或设置是否显示记录总数说明。
        /// </summary>
        [
        Category("Appearance"),
        Description("是否显示记录总数说明。"),
        DefaultValue(true),
        ReadOnly(false),
        ]
        public bool ShowRecordsCountLabel
        {
            get
            {
                return this._RecordsCountLabel.Available;
            }
            set
            {
                if (this._RecordsCountLabel.Available == value)
                {
                    return;
                }

                this._RecordsCountLabel.Available = value;
                this.FixItemsPosition();

            }
        }

        private bool _ShowPageNumberButtons = true;
        /// <summary>
        /// 获取或设置是否显示数字页码按钮。
        /// </summary>
        [
        Category("Appearance"),
        Description("是否显示数字页码按钮。"),
        DefaultValue(true),
        ReadOnly(false),
        ]
        public bool ShowPageNumberButtons
        {
            get
            {
                return this._ShowPageNumberButtons;
            }
            set
            {
                if (this._ShowPageNumberButtons == value)
                {
                    return;
                }

                this._ShowPageNumberButtons = value;
                this.RefreshPageNumberButtons(true);

            }
        }

        /// <summary>
        /// 获取或设置是否是否显示“上一组分页”和“下一组分页”按钮。
        /// </summary>
        [
        Category("Appearance"),
        Description("是否显示“上一组分页”和“下一组分页”按钮。"),
        DefaultValue(true),
        ReadOnly(false),
        ]
        public bool ShowPageGroupButton
        {
            get
            {
                return this._PreviousPageGroupButton.Available;
            }
            set
            {
                if (this._PreviousPageGroupButton.Available == value)
                {
                    return;
                }

                this._PreviousPageGroupButton.Available = value;
                this._NextPageGroupButton.Available = value;
                this.FixItemsPosition();

            }
        }

        /// <summary>
        /// 获取或设置是否是否显示“首页”和“尾页”按钮。
        /// </summary>
        [
        Category("Appearance"),
        Description("是否显示“首页”和“尾页”按钮。"),
        DefaultValue(true),
        ReadOnly(false),
        ]
        public bool ShowFirstAndLastPageButton
        {
            get
            {
                return this._FirstPageButton.Available;
            }
            set
            {
                if (this._FirstPageButton.Available == value)
                {
                    return;
                }

                this._FirstPageButton.Available = value;
                this._LastPageButton.Available = value;
                this.FixItemsPosition();

            }
        }

        private int _PageIndex = 0;
        /// <summary>
        /// 获取或设置当前分页索引。
        /// </summary>
        /// <remarks>
        /// 如果没有数据记录返回 -1；否则返回一个从 0 开始的整数。
        /// 禁止在赋值操作中为属性设置 -1 值。
        /// </remarks>
        [
        Category("Appearance"),
        Description("当前分页索引。"),
        ReadOnly(true),
        ]
        public int PageIndex
        {
            get
            {
                int pc = this.PagesCount - 1;
                if (this._PageIndex > pc)
                {
                    return pc;
                }
                return this._PageIndex;
            }
            set
            {
                if (value < 0)
                {
                    throw new System.ArgumentOutOfRangeException("value", "分页索引不能小于 0。");
                }

                if (this.PagesCount > 0 && value > this.PagesCount - 1)
                {
                    throw new System.ArgumentOutOfRangeException("value", "分页索引不能大于属性“PagesCount”。");
                }

                if (this._PageIndex == value)
                {
                    return;
                }

                //if (this._OnPageIndexChanging != null)
                //{
                //    PageIndexChangingEventArgs pice = new PageIndexChangingEventArgs(value);
                //    this._OnPageIndexChanging(this, pice);
                //    if (pice.Cancel)
                //    {
                //        return;
                //    }
                //}

                this._PageIndex = value;
                this.RefreshPageNumberButtons(true);

                //if (this._OnPageIndexChanged != null)
                //{
                //    PageIndexChangedEventArgs pice = new PageIndexChangedEventArgs(value);
                //    this._OnPageIndexChanged(this, pice);
                //}

            }
        }

        private int _RecordsCount;
        /// <summary>
        /// 获取或设置可用记录总数。
        /// </summary>
        [
        Category("Appearance"),
        Description("可用记录总数。"),
        DefaultValue(10),
        ReadOnly(true),
        ]
        public int RecordsCount
        {
            get
            {
                return this._RecordsCount;
            }
            set
            {
                if (value < 0)
                {
                    throw new System.ArgumentOutOfRangeException("value", "可用记录总数不能小于 0。");
                }

                if (this._RecordsCount == value)
                {
                    return;
                }
                this._RecordsCount = value;
                this.RefreshPageNumberButtons(false);
            }
        }

        private int _PageSize = 15;
        /// <summary>
        /// 获取或设置每页最大显示记录数。
        /// </summary>
        [
        Category("Appearance"),
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
                    throw new System.ArgumentOutOfRangeException("value", "每页最大显示记录数不能小于 1。");
                }
                if (this._PageSize == value)
                {
                    return;
                }
                this._PageSize = value;
                this.RefreshPageNumberButtons(false);
            }
        }

        /// <summary>
        /// 获取可用分页总数。
        /// </summary>
        [
        Category("Appearance"),
        Description("分页总数。"),
        ReadOnly(true),
        ]
        public int PagesCount
        {
            get
            {
                return (this.RecordsCount + this.PageSize - 1) / this.PageSize;
            }
        }

        private int _PageGroupSize = 10;
        /// <summary>
        /// 获取或设置一个值，用于指示每个页码分组中所包含的页码最大数量。
        /// </summary>
        [
        Category("Appearance"),
        Description("指示每个页码分组中所包含的页码最大数量。"),
        DefaultValue(10),
        ReadOnly(false),
        ]
        public int PageGroupSize
        {
            get
            {
                return this._PageGroupSize;
            }
            set
            {
                if (value < 1)
                {
                    throw new System.ArgumentOutOfRangeException("value", "页码分组中所包含的页码最大数量不能小于 1。");
                }
                if (this._PageGroupSize == value)
                {
                    return;
                }
                this._PageGroupSize = value;
                this.RefreshPageNumberButtons(false);
            }
        }

        private System.Drawing.Color _CurrentPageButtonForColor = System.Drawing.Color.Red;
        /// <summary>
        /// 获取或设置当前页按钮文本和图形的前景色。
        /// </summary>
        [
        Category("Appearance"),
        Description("当前页按钮文本和图形的前景色。"),
        DefaultValue(typeof(System.Drawing.Color), "Red"),
        ReadOnly(false),
        ]
        public System.Drawing.Color CurrentPageButtonForColor
        {
            get
            {
                return this._CurrentPageButtonForColor;
            }
            set
            {
                if (this._CurrentPageButtonForColor == value)
                {
                    return;
                }
                this._CurrentPageButtonForColor = value;
                this.RefreshPageNumberButtons(true);
            }
        }

        private System.Drawing.Color _CurrentPageButtonBackColor = System.Drawing.Color.White;
        /// <summary>
        /// 获取或设置当前页按钮文本和图形的背景色。
        /// </summary>
        [
        Category("Appearance"),
        Description("当前页按钮文本和图形的背景色。"),
        DefaultValue(typeof(System.Drawing.Color), "White"),
        ReadOnly(false),
        ]
        public System.Drawing.Color CurrentPageButtonBackColor
        {
            get
            {
                return this._CurrentPageButtonBackColor;
            }
            set
            {
                if (this._CurrentPageButtonBackColor == value)
                {
                    return;
                }
                this._CurrentPageButtonBackColor = value;
                this.RefreshPageNumberButtons(true);
            }
        }

        private System.Drawing.Font _CurrentPageButtonFont = new System.Drawing.Font("宋体", 12, System.Drawing.FontStyle.Bold);
        /// <summary>
        /// 获取或设置当前页按钮的字体设置。
        /// </summary>
        [
        Category("Appearance"),
        Description("当前页按钮的字体设置。"),
        DefaultValue(typeof(System.Drawing.Font), "宋体, 12pt, style=Bold"),
        ReadOnly(false),
        ]
        public System.Drawing.Font CurrentPageButtonFont
        {
            get
            {
                return this._CurrentPageButtonFont;
            }
            set
            {
                if (this._CurrentPageButtonFont == value)
                {
                    return;
                }
                this._CurrentPageButtonFont = value;
                this.RefreshPageNumberButtons(true);
            }
        }

        private System.Windows.Forms.HorizontalAlignment _HorizontalAlignment = HorizontalAlignment.Center;
        /// <summary>
        /// 获取或设置项目的水平对齐方式。
        /// </summary>
        [
        Category("Appearance"),
        Description("项目的水平对齐方式。"),
        DefaultValue(System.Windows.Forms.HorizontalAlignment.Center),
        ReadOnly(false),
        ]
        public System.Windows.Forms.HorizontalAlignment HorizontalAlignment
        {
            get
            {
                return this._HorizontalAlignment;
            }
            set
            {
                if (this._HorizontalAlignment == value)
                {
                    return;
                }
                this._HorizontalAlignment = value;
                this.RefreshPageNumberButtons(true);
            }
        }

        /// <summary>
        /// 获取或设置是否显示分页控制按钮。
        /// </summary>
        [
        Category("Appearance"),
        Description("是否显示分页控制按钮。"),
        DefaultValue(true),
        ReadOnly(false),
        ]
        public bool ShowPageControlButtons
        {
            get
            {
                if (!this._PageSizeLabel.Available &&
                !this._Separator1.Available &&
                !this._FirstPageButton.Available &&
                !this._PreviousPageGroupButton.Available &&
                !this._PreviousPageButton.Available &&
                !this._ShowPageNumberButtons &&
                !this._NextPageButton.Available &&
                !this._NextPageGroupButton.Available &&
                !this._LastPageButton.Available &&
                !this._Separator2.Available &&
                !this._PageIndexLabel.Available &&
                !this._PagesCountLabel.Available)
                {
                    return false;
                }
                return true;
            }
            set
            {
                //if (this._PageIndexLabel.Available == value)
                //{
                //    return;
                //}

                this._PageSizeLabel.Available = value;
                this._Separator1.Available = value;
                this._FirstPageButton.Available = value;
                this._PreviousPageGroupButton.Available = value;
                this._PreviousPageButton.Available = value;
                this._ShowPageNumberButtons = value;
                this._NextPageButton.Available = value;
                this._NextPageGroupButton.Available = value;
                this._LastPageButton.Available = value;
                this._Separator2.Available = value;
                this._PageIndexLabel.Available = value;
                this._PagesCountLabel.Available = value;

                this.RefreshPageNumberButtons(true);

            }
        }


        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public PageNavigation()
            : base()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PageNavigation));

            this.GripStyle = ToolStripGripStyle.Hidden;

            #region 初始化控件。
            this.LeftSpace = new ToolStripLabel();
            this.LeftSpace.AutoSize = false;

            this._LineNumberLabel = new ToolStripLabel();
            this._LineNumberLabel.Text = "行号";

            this._LineNumberTextBox = new ToolStripTextBox();
            this._LineNumberTextBox.AutoSize = false;
            this._LineNumberTextBox.Width = 50;
            this._LineNumberTextBox.Text = "0";
            this._LineNumberTextBox.Enter += delegate (object sender, System.EventArgs e)
            {
                this.LineNumberTextBoxLastValue = this._LineNumberTextBox.Text;

            };
            this._LineNumberTextBox.Leave += delegate (object sender, System.EventArgs e)
            {
                this.CheckedLineNumberTextBoxInput();

            };
            this._LineNumberTextBox.KeyDown += delegate (object sender, KeyEventArgs e)
            {
                if (e.KeyData == Keys.Enter && this._LineNumberTextBox.Focused)
                {
                    e.Handled = true;
                    e.SuppressKeyPress = false;
                    this.CheckedLineNumberTextBoxInput();
                }

            };

            this._PageSizeLabel = new ToolStripLabel();
            this._RecordsCountLabel = new ToolStripLabel();

            this._Separator1 = new ToolStripSeparator();

            this._FirstPageButton = new ToolStripButton();
            this._FirstPageButton.Image = ((System.Drawing.Image)(resources.GetObject("FirstPageButton.Image")));
            this._FirstPageButton.Text = "首页";
            this._FirstPageButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._FirstPageButton.Click += delegate (object sender, System.EventArgs e)
            {
                //this.PageIndex = 0;
                this.DoPageIndexChanging(0);
            };

            this._PreviousPageGroupButton = new ToolStripButton();
            this._PreviousPageGroupButton.Image = ((System.Drawing.Image)(resources.GetObject("PreviousPageGroupButton.Image")));
            this._PreviousPageGroupButton.Text = "上一组分页";
            this._PreviousPageGroupButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._PreviousPageGroupButton.Click += delegate (object sender, System.EventArgs e)
            {
                if (this.PageIndex > 0)
                {
                    int ni = this.PageIndex - this.PageGroupSize;
                    if (ni < 0)
                    {
                        ni = 0;
                    }
                    //this.PageIndex = ni;
                    this.DoPageIndexChanging(ni);
                }
            };

            this._PreviousPageButton = new ToolStripButton();
            this._PreviousPageButton.Image = ((System.Drawing.Image)(resources.GetObject("PreviousPageButton.Image")));
            this._PreviousPageButton.Text = "上一页";
            this._PreviousPageButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._PreviousPageButton.Click += delegate (object sender, System.EventArgs e)
            {
                if (this.PageIndex > 0)
                {
                    //this.PageIndex--;
                    this.DoPageIndexChanging(this.PageIndex - 1);
                }
            };

            this._NextPageButton = new ToolStripButton();
            this._NextPageButton.Image = ((System.Drawing.Image)(resources.GetObject("NextPageButton.Image")));
            this._NextPageButton.Text = "下一页";
            this._NextPageButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._NextPageButton.Click += delegate (object sender, System.EventArgs e)
            {
                if (this.PageIndex < this.PagesCount - 1)
                {
                    //this.PageIndex++;
                    this.DoPageIndexChanging(this.PageIndex + 1);
                }
            };

            this._NextPageGroupButton = new ToolStripButton();
            this._NextPageGroupButton.Image = ((System.Drawing.Image)(resources.GetObject("NextPageGroupButton.Image")));
            this._NextPageGroupButton.Text = "下一组分页";
            this._NextPageGroupButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._NextPageGroupButton.Click += delegate (object sender, System.EventArgs e)
            {
                if (this.PageIndex < this.PagesCount - 1)
                {
                    int ni = this.PageIndex + this.PageGroupSize;
                    if (ni > this.PagesCount - 1)
                    {
                        ni = this.PagesCount - 1;
                    }
                    //this.PageIndex = ni;
                    this.DoPageIndexChanging(ni);
                }
            };

            this._LastPageButton = new ToolStripButton();
            this._LastPageButton.Image = ((System.Drawing.Image)(resources.GetObject("LastPageButton.Image")));
            this._LastPageButton.Text = "尾页";
            this._LastPageButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            this._LastPageButton.Click += delegate (object sender, System.EventArgs e)
            {
                if (this.PagesCount > 0)
                {
                    //this.PageIndex = this.PagesCount - 1;
                    this.DoPageIndexChanging(this.PagesCount - 1);
                }
            };

            this._Separator2 = new ToolStripSeparator();

            this._PageIndexLabel = new ToolStripLabel();
            this._PagesCountLabel = new ToolStripLabel();

            #endregion

            #region 添加控件到面板。
            this.Items.Add(this.LeftSpace);

            this.Items.Add(this._LineNumberLabel);
            this.Items.Add(this._LineNumberTextBox);

            this.Items.Add(this._PageSizeLabel);
            this.Items.Add(this._RecordsCountLabel);

            this.Items.Add(this._Separator1);
            this.Items.Add(this._FirstPageButton);
            this.Items.Add(this._PreviousPageGroupButton);
            this.Items.Add(this._PreviousPageButton);

            this.RefreshPageNumberButtons(true);

            this.Items.Add(this._NextPageButton);
            this.Items.Add(this._NextPageGroupButton);
            this.Items.Add(this._LastPageButton);
            this.Items.Add(this._Separator2);

            this.Items.Add(this._PageIndexLabel);
            this.Items.Add(this._PagesCountLabel);

            #endregion

        }

        /// <summary>
        /// 当行号输入框中的用户输入的数据改变时引发此事件。
        /// </summary>
        private void CheckedLineNumberTextBoxInput()
        {
            if (this._LineNumberTextBox.Text == this.LineNumberTextBoxLastValue)
            {
                return;
            }

            int lineNumber = 0;
            if (!int.TryParse(this._LineNumberTextBox.Text, out lineNumber))
            {
                this._LineNumberTextBox.Text = LineNumberTextBoxLastValue;
                return;
            }

            if (this._OnLineNumberChanging != null)
            {
                LineNumberChangingEventArgs lncea = new LineNumberChangingEventArgs(Convert.ToInt32(this._LineNumberTextBox.Text));
                this._OnLineNumberChanging(this, lncea);
                if (lncea.Cancel)
                {
                    this._LineNumberTextBox.Text = this.LineNumberTextBoxLastValue;
                    return;
                }
            }
            this.LineNumberTextBoxLastValue = this._LineNumberTextBox.Text;

        }

        /// <summary>
        /// 删除所有的分页按钮。
        /// </summary>
        private void ClearPageNumberButton()
        {
            for (int i = this.Items.Count - 1; i >= 0; i--)
            {
                if (this.Items[i] is PageNumberButton || this.Items[i] is CurrentPageNumberButton)
                {
                    this.Items.RemoveAt(i);
                }
            }
        }

        /// <summary>
        /// 更新显示的分页按钮。
        /// </summary>
        /// <param name="ABS">是否强制更新分页页码按钮。</param>
        private void RefreshPageNumberButtons(bool ABS)
        {
            int startPageIndex = this.PageIndex - this.PageGroupSize / 2;
            if (startPageIndex + this.PageGroupSize > this.PagesCount - 1)
            {
                startPageIndex = this.PagesCount - this.PageGroupSize;
            }
            if (startPageIndex < 0)
            {
                startPageIndex = 0;
            }
            this.FirstPageButton.Enabled = this.PreviousPageGroupButton.Enabled = this.PreviousPageButton.Enabled = (this.PageIndex > 0);
            this.LastPageButton.Enabled = this.NextPageGroupButton.Enabled = this.NextPageButton.Enabled = (this.PageIndex < this.PagesCount - 1);

            bool ReCreatePageNumberButtons = true; //指示是否需要重新设置分页编号按钮。
            #region 判断是否需要重新设置分页编号按钮。
            if (!ABS && this._ShowPageNumberButtons)
            {
                #region 找到当前显示的最大分页编号和最小分页编号。
                int curSPI = 0; //最小分页编号。
                int curMPI = 0; //最大分页编号。
                bool first = true;
                foreach (var tmp in this.Items)
                {
                    if (tmp is PageNumberButton || tmp is CurrentPageNumberButton)
                    {
                        int k;
                        if (tmp is PageNumberButton)
                        {
                            k = ((PageNumberButton)tmp).PageNumber - 1;
                        }
                        else// if (tmp is CurrentPageNumberButton)
                        {
                            k = ((CurrentPageNumberButton)tmp).PageNumber - 1;
                        }
                        if (first)
                        {
                            first = false;
                            curSPI = k;
                            curMPI = k;
                        }
                        else
                        {
                            if (k < curSPI)
                            {
                                curSPI = k;
                            }
                            else if (k > curMPI)
                            {
                                curMPI = k;
                            }
                        }
                    }
                }
                #endregion

                if (!first) //如果 first 发生反转则执行重新生成分页编号按钮的判断。
                {
                    int jj = startPageIndex + this.PageGroupSize - 1;
                    if (jj >= this.PagesCount)
                    {
                        jj = this.PagesCount - 1;
                    }
                    if (curSPI == startPageIndex && curMPI == jj)
                    {
                        ReCreatePageNumberButtons = false;
                    }
                }
            }
            #endregion

            #region 重新生成页码按钮。
            if (!this._ShowPageNumberButtons || ReCreatePageNumberButtons) //当不显示分页编码按钮或需要重新创建分页编码按钮时，需要清除已存在的分页编码按钮。
            {
                this.ClearPageNumberButton();
            }

            if (this._ShowPageNumberButtons && ReCreatePageNumberButtons)
            {
                int insertIndex = this.Items.IndexOf(this.NextPageButton); //计算记录插入位置。

                for (int i = startPageIndex; i < startPageIndex + this.PageGroupSize && i < this.PagesCount; i++)
                {
                    if (i == this.PageIndex) //当前分页。
                    {
                        CurrentPageNumberButton btn = new CurrentPageNumberButton();
                        btn.ForeColor = this.CurrentPageButtonForColor;
                        btn.BackColor = this.CurrentPageButtonBackColor;
                        btn.Font = this.CurrentPageButtonFont;
                        btn.Text = (i + 1).ToString();
                        btn.ToolTipText = string.Format("第 {0} 页", i + 1);
                        this.Items.Insert(insertIndex++, btn);
                    }
                    else
                    {
                        PageNumberButton btn = new PageNumberButton(i + 1);
                        btn.ToolTipText = string.Format("第 {0} 页", i + 1);
                        btn.Click += new EventHandler(this.PageNumberButton_Click);
                        this.Items.Insert(insertIndex++, btn);
                    }
                }
            }
            #endregion

            int crc = this.PageSize; //当前页记录总数
            if (this.RecordsCount <= this.PageSize * (this.PageIndex + 1)) //如果尾页则需要统计记录实际数量。
            {
                crc = this.RecordsCount - this.PageSize * this.PageIndex;
            }
            if (crc < 0)
            {
                crc = 0;
            }
            this._PageSizeLabel.Text = string.Format(this.PageSizeLabelFormatString, crc);
            this._RecordsCountLabel.Text = string.Format(this.RecordsCountLabelFormatString, this.RecordsCount);
            this._PageIndexLabel.Text = string.Format(this.PageIndexLabelFormatString, this.PageIndex + 1);
            this._PagesCountLabel.Text = string.Format(this.PagesCountLabelFormatString, this.PagesCount);

            #region 调整当前分页说明标签的位置，以满足按钮项目的布局合理性。
            if (this.ShowPageIndexLabel && !this.ShowPageNumberButtons)
            {
                this.Items.Remove(this._PageIndexLabel);
                int insertIndex = this.Items.IndexOf(this._NextPageButton);
                if (insertIndex != -1)
                {
                    this.Items.Insert(insertIndex, this._PageIndexLabel);
                }

            }
            else
            {
                this.Items.Remove(this._PageIndexLabel);
                int insertIndex = this.Items.IndexOf(this._PagesCountLabel);
                if (insertIndex != -1)
                {
                    this.Items.Insert(insertIndex, this._PageIndexLabel);
                }

            }
            #endregion

            this.FixItemsPosition();

        }

        /// <summary>
        /// 当用户点击分页编号按钮时引发此事件。
        /// </summary>
        /// <param name="sender">分页按钮对象。</param>
        /// <param name="e">默认的时间数据。</param>
        private void PageNumberButton_Click(object sender, EventArgs e)
        {
            PageNumberButton btn = sender as PageNumberButton;
            //this.PageIndex = btn.PageNumber - 1;
            this.DoPageIndexChanging(btn.PageNumber - 1);

        }

        private delegate void FixItemsPositionHandler();
        /// <summary>
        /// 调整子项目的位置。
        /// </summary>
        private void FixItemsPosition()
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new FixItemsPositionHandler(this.FixItemsPosition));
            }
            else
            {
                switch (this.HorizontalAlignment)
                {
                    case HorizontalAlignment.Left:
                        this.LeftSpace.Available = false;
                        break;
                    case HorizontalAlignment.Center:
                        {
                            int width = 0;
                            foreach (ToolStripItem tmp in this.Items)
                            {
                                if (this.DesignMode)//处于计模式需要特殊处理，因为处于设计模式时，项目的 Visible 属性为 false。
                                {
                                    if (tmp != this.LeftSpace && tmp.Available)
                                    {
                                        width += tmp.Width;
                                    }
                                }
                                else
                                {
                                    if (tmp != this.LeftSpace && tmp.Available && tmp.Visible)
                                    {
                                        width += tmp.Width;
                                    }
                                }
                            }

                            if (this.ClientSize.Width > width)
                            {
                                this.LeftSpace.Width = (this.ClientSize.Width - width) / 2;
                            }
                            else
                            {
                                this.LeftSpace.Width = 0;
                            }
                            this.LeftSpace.Available = true;
                        }
                        break;
                    case HorizontalAlignment.Right:
                        {
                            int width = 0;
                            foreach (ToolStripItem tmp in this.Items)
                            {
                                if (this.DesignMode)//因为处于设计模式时，项目的 Visible 属性为 false，这将导致局中等效果混乱，所以此处对处于计模式进行特殊处理。
                                {
                                    if (tmp != this.LeftSpace && tmp.Available)
                                    {
                                        width += tmp.Width;
                                    }
                                }
                                else
                                {
                                    if (tmp != this.LeftSpace && tmp.Available && tmp.Visible)
                                    {
                                        width += tmp.Width;
                                    }
                                }
                            }

                            if (this.ClientSize.Width > width)
                            {
                                this.LeftSpace.Width = this.ClientSize.Width - width - 1;
                            }
                            else
                            {
                                this.LeftSpace.Width = 0;
                            }
                            this.LeftSpace.Available = true;
                        }
                        break;
                }
            }

        }

        /// <summary>
        /// 当控件尺寸改变时引发此事件。
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            this.FixItemsPosition();

            base.OnSizeChanged(e);

        }

        private event PageIndexChangingEventHandler _OnPageIndexChanging;
        /// <summary>
        /// 当分页索引准备更改时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("当分页索引准备更改时引发此事件。"),
        ]
        public event PageIndexChangingEventHandler OnPageIndexChanging
        {
            add
            {
                this._OnPageIndexChanging += value;
            }
            remove
            {
                this._OnPageIndexChanging -= value;
            }
        }

        private event PageIndexChangedEventHandler _OnPageIndexChanged;
        /// <summary>
        /// 当分页索引已经更改时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("当分页索引已经更改时引发此事件。"),
        ]
        public event PageIndexChangedEventHandler OnPageIndexChanged
        {
            add
            {
                this._OnPageIndexChanged += value;
            }
            remove
            {
                this._OnPageIndexChanged -= value;
            }
        }

        private event LineNumberChangingEventHandler _OnLineNumberChanging;
        /// <summary>
        /// 当行号准备更改时引发此事件。
        /// </summary>
        [
        Category("Action"),
        Description("当行号准备更改时引发此事件。"),
        ]
        public event LineNumberChangingEventHandler OnLineNumberChanging
        {
            add
            {
                this._OnLineNumberChanging += value;
            }
            remove
            {
                this._OnLineNumberChanging -= value;
            }
        }

        /// <summary>
        /// 获取或设置工具条移动手柄是可见还是隐藏。
        /// </summary>
        [
        Category("Appearance"),
        Description("工具条移动手柄是可见还是隐藏。"),
        DefaultValue(ToolStripGripStyle.Hidden),
        ReadOnly(false),
        ]
        public new ToolStripGripStyle GripStyle
        {
            get
            {
                return base.GripStyle;
            }
            set
            {
                base.GripStyle = value;
            }
        }

        /// <summary>
        /// 引发分页索引更改事件。
        /// </summary>
        /// <param name="newPageIndex">新的分页索引。</param>
        private void DoPageIndexChanging(int newPageIndex)
        {
            if (this._OnPageIndexChanging != null)
            {
                PageIndexChangingEventArgs pice = new PageIndexChangingEventArgs(newPageIndex);
                this._OnPageIndexChanging(this, pice);
                if (pice.Cancel)
                {
                    return;
                }
            }
            this.PageIndex = newPageIndex;

            if (this._OnPageIndexChanged != null)
            {
                PageIndexChangedEventArgs pice = new PageIndexChangedEventArgs(newPageIndex);
                this._OnPageIndexChanged(this, pice);
            }

        }

    }

    /// <summary>
    /// 分页编号按钮。
    /// </summary>
    public class PageNumberButton : System.Windows.Forms.ToolStripButton
    {
        /// <summary>
        /// 获取或设置分页编号（从 1 开始的整数）
        /// </summary>
        public int PageNumber
        {
            get
            {
                return Convert.ToInt32(this.Text);
            }
            set
            {
                this.Text = value.ToString();
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public PageNumberButton()
        {
        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="pageNumber">分页编号（从 1 开始的整数）</param>
        public PageNumberButton(int pageNumber)
        {
            this.PageNumber = pageNumber;

        }
    }

    /// <summary>
    /// 当前分页编号按钮。
    /// </summary>
    public class CurrentPageNumberButton : System.Windows.Forms.ToolStripLabel
    {
        /// <summary>
        /// 获取或设置分页编号（从 1 开始的整数）
        /// </summary>
        public int PageNumber
        {
            get
            {
                return Convert.ToInt32(this.Text);
            }
            set
            {
                this.Text = value.ToString();
            }
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public CurrentPageNumberButton()
        {
            this.PageNumber = 0;

        }

        /// <summary>
        /// 用指定的数据初始化此实例。
        /// </summary>
        /// <param name="pageNumber">分页编号（从 1 开始的整数）</param>
        public CurrentPageNumberButton(int pageNumber)
        {
            this.PageNumber = pageNumber;

        }
    }

    /// <summary>
    /// 分页索引准备更改事件方法代理。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PageIndexChangingEventHandler(object sender, PageIndexChangingEventArgs e);

    /// <summary>
    /// 为分页索引准备更改事件提供数据。
    /// </summary>
    public class PageIndexChangingEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取或者设置新的分页索引编号。
        /// </summary>
        public int NewPageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 获取或这设置一个值，用来指示是否应该取消操作。
        /// </summary>
        public bool Cancel
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public PageIndexChangingEventArgs()
        {
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="newPageIndex">新的分页索引编号。</param>
        public PageIndexChangingEventArgs(int newPageIndex)
        {
            this.NewPageIndex = NewPageIndex;

        }

    }

    /// <summary>
    /// 分页索引已经更改事件方法代理。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void PageIndexChangedEventHandler(object sender, PageIndexChangedEventArgs e);

    /// <summary>
    /// 为分页索引已经更改事件提供数据。
    /// </summary>
    public class PageIndexChangedEventArgs : System.EventArgs
    {
        /// <summary>
        /// 获取或者设置分页索引编号。
        /// </summary>
        public int PageIndex
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public PageIndexChangedEventArgs()
        {
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="pageIndex">分页索引编号。</param>
        public PageIndexChangedEventArgs(int pageIndex)
        {
            this.PageIndex = pageIndex;

        }

    }

    /// <summary>
    /// 行号已经更改事件方法代理。
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void LineNumberChangingEventHandler(object sender, LineNumberChangingEventArgs e);

    /// <summary>
    /// 为行号已经更改事件提供数据。
    /// </summary>
    public class LineNumberChangingEventArgs : System.EventArgs
    {
        private int _LineNumber;
        /// <summary>
        /// 获取或者设置行号。从 1 开始的整数。
        /// </summary>
        public int LineNumber
        {
            get
            {
                return this._LineNumber;
            }
            set
            {
                if (value < 1)
                {
                    throw new System.ArgumentOutOfRangeException("value", "参数 value 取值不能小于 1。");
                }
                this._LineNumber = value;
            }
        }

        /// <summary>
        /// 获取或这设置一个值，用来指示是否应该取消操作。
        /// </summary>
        public bool Cancel
        {
            get;
            set;
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public LineNumberChangingEventArgs()
        {
        }

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        /// <param name="lineNumber">行号。从 1 开始的整数。</param>
        public LineNumberChangingEventArgs(int lineNumber)
        {
            this.LineNumber = lineNumber;

        }

    }

}
