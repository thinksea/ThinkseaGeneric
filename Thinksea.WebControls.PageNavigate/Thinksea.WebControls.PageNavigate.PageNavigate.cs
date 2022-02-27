using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;

using System.ComponentModel.Design;
using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Diagnostics;

namespace Thinksea.WebControls.PageNavigate
{
    /// <summary>
    /// 分页导航控件。
    /// </summary>
    [ToolboxData("<{0}:PageNavigate runat=server></{0}:PageNavigate>"),
    DefaultProperty("PageSize"),
    System.ComponentModel.DefaultEvent("PageSelectedCommand"),
    ValidationPropertyAttribute("Text"),
    System.ComponentModel.Designer(typeof(Thinksea.WebControls.PageNavigate.PageNavigateDesigner)),
    ]
    public class PageNavigate : System.Web.UI.WebControls.WebControl, IPostBackEventHandler, INamingContainer//, IStateManager
    {
        /// <summary>
        /// 获取与此 Web 服务器控件相对应的 System.Web.UI.HtmlTextWriterTag 值。此属性主要由控件开发人员使用。
        /// </summary>
        protected override HtmlTextWriterTag TagKey
        {
            get
            {
                return HtmlTextWriterTag.Div;
            }
        }

        /// <summary>
        /// 用于隐藏基类属性，勿使用。
        /// </summary>
        [
        System.ComponentModel.Browsable(false),
        System.ComponentModel.ReadOnly(true),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public override System.Web.UI.WebControls.Unit Width
        {
            set
            {
                //base.Width = System.Web.UI.WebControls.Unit.Empty;
            }
        }


        /// <summary>
        /// 用于隐藏基类属性，勿使用。
        /// </summary>
        [
        System.ComponentModel.Browsable(false),
        System.ComponentModel.ReadOnly(true),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public override System.Web.UI.WebControls.Unit Height
        {
            set
            {
                //base.Width = System.Web.UI.WebControls.Unit.Empty;
            }
        }


        /// <summary>
        /// 指示分页导航栏中最多显示的页索引数量。
        /// </summary>
        private int _NavigatePageSize = 10;
        /// <summary>
        /// 获取或设置一个值，该值指示分页导航栏中最多显示的页索引数量。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(10),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("指示分页导航栏中最多显示的页索引数量"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public int NavigatePageSize
        {
            get
            {
                object savedState = this.ViewState["NavigatePageSize"];
                if (savedState != null) this._NavigatePageSize = (int)savedState;
                return this._NavigatePageSize;
            }
            set
            {
                if (value < 1)
                {
                    throw new System.ArgumentOutOfRangeException(nameof(value), value, "指定的参数已超出有效取值的范围，该参数取值不能小于 1。");
                }
                this._NavigatePageSize = value;
                this.ViewState["NavigatePageSize"] = value;
            }
        }


        /// <summary>
        /// 指示每页最多显示的记录数量。
        /// </summary>
        private int _PageSize = 10;
        /// <summary>
        /// 获取或设置一个值，该值指示每页最多显示的记录数量。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(10),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("指示每页最多显示的记录数量"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public int PageSize
        {
            get
            {
                object savedState = this.ViewState["PageSize"];
                if (savedState != null) this._PageSize = (int)savedState;
                return this._PageSize;
            }
            set
            {
                if (value < 1)
                {
                    throw new System.ArgumentOutOfRangeException(nameof(value), value, "指定的参数已超出有效取值的范围，该参数取值不能小于 1。");
                }
                this._PageSize = value;
                this.ViewState["PageSize"] = value;
            }
        }


        private System.String _PageSizeParameter = "PageSize";
        /// <summary>
        /// 获取或设置位于 URL 中的分页尺寸参数名称（对于使用 URL 传参分页形式有效）。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("PageSize"),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("获取或设置位于 URL 中的分页尺寸参数名称（对于使用 URL 传参分页形式有效）。默认值为“PageSize”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String PageSizeParameter
        {
            get
            {
                object savedState = this.ViewState["PageSizeParameter"];
                if (savedState != null) this._PageSizeParameter = (System.String)savedState;
                return this._PageSizeParameter;
            }
            set
            {
                this._PageSizeParameter = value;
                this.ViewState["PageSizeParameter"] = value;
            }
        }


        /// <summary>
        /// 当前页索引编号。
        /// </summary>
        private int _PageIndex = 0;
        /// <summary>
        /// 获取或设置当前页索引编号，如果指定的赋值超出允许范围（小于0或大于PageCount - 1）则自动调整到允许范围内的最接近的值，但对于PageCount等于0的情况赋值必须为0。
        /// </summary>
        [
        System.ComponentModel.ReadOnly(true),
        System.ComponentModel.DefaultValue(0),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("当前页索引编号"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public int PageIndex
        {
            get
            {
                object savedState = this.ViewState["PageIndex"];
                if (savedState != null) this._PageIndex = (int)savedState;
                return this._PageIndex;
            }
            set
            {
                if (value < 0 || this.PageCount == 0)
                {
                    this._PageIndex = 0;
                }
                else
                {
                    if (value > this.PageCount - 1)
                    {
                        this._PageIndex = this.PageCount - 1;
                    }
                    else
                    {
                        this._PageIndex = value;
                    }
                }
                //				if( value < 0 || (value > this.PageCount - 1 && this.PageCount > 0) )
                //				{
                //					throw new System.ArgumentOutOfRangeException(nameof(value), value, "指定的参数已超出有效取值的范围，该参数取值不能小于 0 并且不能大于 (PageCount - 1)。");
                //				}
                //				this._PageIndex = value;
                this.ViewState["PageIndex"] = this._PageIndex;
            }
        }


        private System.String _PageIndexParameter = "PageIndex";
        /// <summary>
        /// 获取或设置位于 URL 中的分页索引参数名称（对于使用 URL 传参分页形式有效）。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("PageIndex"),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("获取或设置位于 URL 中的分页索引参数名称（对于使用 URL 传参分页形式有效）。默认值为“PageIndex”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String PageIndexParameter
        {
            get
            {
                object savedState = this.ViewState["PageIndexParameter"];
                if (savedState != null) this._PageIndexParameter = (System.String)savedState;
                return this._PageIndexParameter;
            }
            set
            {
                this._PageIndexParameter = value;
                this.ViewState["PageIndexParameter"] = value;
            }
        }


        /// <summary>
        /// 记录总数。
        /// </summary>
        private int _RecordsCount = 0;
        /// <summary>
        /// 获取或设置记录总数。
        /// </summary>
        [
        System.ComponentModel.ReadOnly(true),
        System.ComponentModel.DefaultValue(0),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("记录总数"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public int RecordsCount
        {
            get
            {
                object savedState = this.ViewState["RecordsCount"];
                if (savedState != null) this._RecordsCount = (int)savedState;
                return this._RecordsCount;
            }
            set
            {
                if (value < 0)
                {
                    throw new System.ArgumentOutOfRangeException(nameof(value), value, "指定的参数已超出有效取值的范围，该参数取值不能小于 0。");
                }
                this._RecordsCount = value;
                this.ViewState["RecordsCount"] = value;
            }
        }


        /// <summary>
        /// 获取分页总数。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(0),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("分页总数"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public int PageCount
        {
            get
            {
                return (this.RecordsCount + this.PageSize - 1) / this.PageSize;
            }
        }


        /// <summary>
        /// 获取当前页实际显示的记录数量。
        /// </summary>
        [
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("当前页实际显示的记录数量"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public int CurrentRecordsCount
        {
            get
            {
                int rCount = this.RecordsCount - this.PageSize * this.PageIndex;
                if (rCount > this.PageSize)
                {
                    return this.PageSize;
                }
                else
                {
                    return System.Convert.ToInt32(rCount);
                }
            }
        }


        private bool _EnableURLPage = false;
        /// <summary>
        /// 获取或设置一个值，指示是否启用默认的 URL 分页处理。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(false),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("指示是否启用默认的 URL 分页处理。"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool EnableURLPage
        {
            get
            {
                object savedState = this.ViewState["EnableURLPage"];
                if (savedState != null) this._EnableURLPage = (bool)savedState;
                return this._EnableURLPage;
            }
            set
            {
                this._EnableURLPage = value;
                this.ViewState["EnableURLPage"] = value;
            }
        }


        #region 子项样式。
        /// <summary>
        /// 导航页码的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _PageNumberStyle = null;
        /// <summary>
        /// 获取或设置导航页码的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("导航页码的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle PageNumberStyle
        {
            get
            {
                if (this._PageNumberStyle == null)
                {
                    this._PageNumberStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._PageNumberStyle).TrackViewState();
                }
                return this._PageNumberStyle;
            }
            set
            {
                this._PageNumberStyle = value;
            }
        }


        /// <summary>
        /// 导航页码被禁用后的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _PageNumberDisabledStyle = null;
        /// <summary>
        /// 获取或设置导航页码被禁用后的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("导航页码被禁用后的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle PageNumberDisabledStyle
        {
            get
            {
                if (this._PageNumberDisabledStyle == null)
                {
                    this._PageNumberDisabledStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._PageNumberDisabledStyle).TrackViewState();
                }
                return this._PageNumberDisabledStyle;
            }
            set
            {
                this._PageNumberDisabledStyle = value;
            }
        }


        /// <summary>
        /// 当前页导航页码的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _CurrentPageNumberStyle = null;
        /// <summary>
        /// 获取或设置当前页导航页码的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("当前页导航页码的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle CurrentPageNumberStyle
        {
            get
            {
                if (this._CurrentPageNumberStyle == null)
                {
                    this._CurrentPageNumberStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._CurrentPageNumberStyle).TrackViewState();
                }
                return this._CurrentPageNumberStyle;
            }
            set
            {
                this._CurrentPageNumberStyle = value;
            }
        }


        /// <summary>
        /// 当前页导航页码被禁用后的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _CurrentPageNumberDisabledStyle = null;
        /// <summary>
        /// 获取或设置当前页导航页码被禁用后的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("当前页导航页码被禁用后的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle CurrentPageNumberDisabledStyle
        {
            get
            {
                if (this._CurrentPageNumberDisabledStyle == null)
                {
                    this._CurrentPageNumberDisabledStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._CurrentPageNumberDisabledStyle).TrackViewState();
                }
                return this._CurrentPageNumberDisabledStyle;
            }
            set
            {
                this._CurrentPageNumberDisabledStyle = value;
            }
        }


        /// <summary>
        /// 页导航按钮的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _PageButtonStyle = null;
        /// <summary>
        /// 获取或设置页导航按钮的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("页导航按钮的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle PageButtonStyle
        {
            get
            {
                if (this._PageButtonStyle == null)
                {
                    this._PageButtonStyle = new System.Web.UI.WebControls.TableItemStyle();
                    //this._PageButtonStyle.Font.Name = "Webdings";
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._PageButtonStyle).TrackViewState();
                }
                return this._PageButtonStyle;
            }
            set
            {
                this._PageButtonStyle = value;
            }
        }


        /// <summary>
        /// 首页导航按钮的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _FirstPageButtonStyle = null;
        /// <summary>
        /// 获取或设置首页导航按钮的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("首页导航按钮的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle FirstPageButtonStyle
        {
            get
            {
                if (this._FirstPageButtonStyle == null)
                {
                    this._FirstPageButtonStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._FirstPageButtonStyle).TrackViewState();
                }
                return this._FirstPageButtonStyle;
            }
            set
            {
                this._FirstPageButtonStyle = value;
            }
        }


        /// <summary>
        /// 上一组分页导航按钮的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _PrevGroupPageButtonStyle = null;
        /// <summary>
        /// 获取或设置上一组分页导航按钮的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("上一组分页导航按钮的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle PrevGroupPageButtonStyle
        {
            get
            {
                if (this._PrevGroupPageButtonStyle == null)
                {
                    this._PrevGroupPageButtonStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._PrevGroupPageButtonStyle).TrackViewState();
                }
                return this._PrevGroupPageButtonStyle;
            }
            set
            {
                this._PrevGroupPageButtonStyle = value;
            }
        }


        /// <summary>
        /// 上一页导航按钮的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _PrevPageButtonStyle = null;
        /// <summary>
        /// 获取或设置上一页导航按钮的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("上一页导航按钮的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle PrevPageButtonStyle
        {
            get
            {
                if (this._PrevPageButtonStyle == null)
                {
                    this._PrevPageButtonStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._PrevPageButtonStyle).TrackViewState();
                }
                return this._PrevPageButtonStyle;
            }
            set
            {
                this._PrevPageButtonStyle = value;
            }
        }


        /// <summary>
        /// 下一页导航按钮的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _NextPageButtonStyle = null;
        /// <summary>
        /// 获取或设置下一页导航按钮的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("下一页导航按钮的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle NextPageButtonStyle
        {
            get
            {
                if (this._NextPageButtonStyle == null)
                {
                    this._NextPageButtonStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._NextPageButtonStyle).TrackViewState();
                }
                return this._NextPageButtonStyle;
            }
            set
            {
                this._NextPageButtonStyle = value;
            }
        }


        /// <summary>
        /// 下一组分页导航按钮的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _NextGroupPageButtonStyle = null;
        /// <summary>
        /// 获取或设置下一组分页导航按钮的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("下一组分页导航按钮的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle NextGroupPageButtonStyle
        {
            get
            {
                if (this._NextGroupPageButtonStyle == null)
                {
                    this._NextGroupPageButtonStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._NextGroupPageButtonStyle).TrackViewState();
                }
                return this._NextGroupPageButtonStyle;
            }
            set
            {
                this._NextGroupPageButtonStyle = value;
            }
        }


        /// <summary>
        /// 尾页导航按钮的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _LastPageButtonStyle = null;
        /// <summary>
        /// 获取或设置尾页导航按钮的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("尾页导航按钮的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle LastPageButtonStyle
        {
            get
            {
                if (this._LastPageButtonStyle == null)
                {
                    this._LastPageButtonStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._LastPageButtonStyle).TrackViewState();
                }
                return this._LastPageButtonStyle;
            }
            set
            {
                this._LastPageButtonStyle = value;
            }
        }


        /// <summary>
        /// 页码输入控件的样式。
        /// </summary>
        private System.Web.UI.WebControls.Style _InputPageNumberTextBoxStyle = null;
        /// <summary>
        /// 获取或设置页码输入控件的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("页码输入控件的样式。"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.Style InputPageNumberTextBoxStyle
        {
            get
            {
                if (this._InputPageNumberTextBoxStyle == null)
                {
                    this._InputPageNumberTextBoxStyle = new System.Web.UI.WebControls.Style();
                    //this._InputPageNumberTextBoxStyle.HorizontalAlign = HorizontalAlign.Center;
                    //this._InputPageNumberTextBoxStyle.Width = new Unit("50px");
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._InputPageNumberTextBoxStyle).TrackViewState();
                }
                return this._InputPageNumberTextBoxStyle;
            }
            set
            {
                this._InputPageNumberTextBoxStyle = value;
            }
        }



        /// <summary>
        /// 跳转到输入的页码按钮的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _InputPageNumberButtonStyle = null;
        /// <summary>
        /// 获取或设置跳转到输入的页码按钮的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("跳转到输入的页码按钮的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle InputPageNumberButtonStyle
        {
            get
            {
                if (this._InputPageNumberButtonStyle == null)
                {
                    this._InputPageNumberButtonStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._InputPageNumberButtonStyle).TrackViewState();
                }
                return this._InputPageNumberButtonStyle;
            }
            set
            {
                this._InputPageNumberButtonStyle = value;
            }
        }


        /// <summary>
        /// 首页导航按钮被禁用后的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _FirstPageButtonDisabledStyle = null;
        /// <summary>
        /// 获取或设置首页导航按钮被禁用后的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("首页导航按钮被禁用后的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle FirstPageButtonDisabledStyle
        {
            get
            {
                if (this._FirstPageButtonDisabledStyle == null)
                {
                    this._FirstPageButtonDisabledStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._FirstPageButtonDisabledStyle).TrackViewState();
                }
                return this._FirstPageButtonDisabledStyle;
            }
            set
            {
                this._FirstPageButtonDisabledStyle = value;
            }
        }


        /// <summary>
        /// 上一组分页导航按钮被禁用后的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _PrevGroupPageButtonDisabledStyle = null;
        /// <summary>
        /// 获取或设置上一组分页导航按钮被禁用后的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("上一组分页导航按钮被禁用后的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle PrevGroupPageButtonDisabledStyle
        {
            get
            {
                if (this._PrevGroupPageButtonDisabledStyle == null)
                {
                    this._PrevGroupPageButtonDisabledStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._PrevGroupPageButtonDisabledStyle).TrackViewState();
                }
                return this._PrevGroupPageButtonDisabledStyle;
            }
            set
            {
                this._PrevGroupPageButtonDisabledStyle = value;
            }
        }


        /// <summary>
        /// 上一页导航按钮被禁用后的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _PrevPageButtonDisabledStyle = null;
        /// <summary>
        /// 获取或设置上一页导航按钮被禁用后的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("上一页导航按钮被禁用后的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle PrevPageButtonDisabledStyle
        {
            get
            {
                if (this._PrevPageButtonDisabledStyle == null)
                {
                    this._PrevPageButtonDisabledStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._PrevPageButtonDisabledStyle).TrackViewState();
                }
                return this._PrevPageButtonDisabledStyle;
            }
            set
            {
                this._PrevPageButtonDisabledStyle = value;
            }
        }


        /// <summary>
        /// 下一页导航按钮被禁用后的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _NextPageButtonDisabledStyle = null;
        /// <summary>
        /// 获取或设置下一页导航按钮被禁用后的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("下一页导航按钮被禁用后的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle NextPageButtonDisabledStyle
        {
            get
            {
                if (this._NextPageButtonDisabledStyle == null)
                {
                    this._NextPageButtonDisabledStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._NextPageButtonDisabledStyle).TrackViewState();
                }
                return this._NextPageButtonDisabledStyle;
            }
            set
            {
                this._NextPageButtonDisabledStyle = value;
            }
        }


        /// <summary>
        /// 下一组分页导航按钮被禁用后的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _NextGroupPageButtonDisabledStyle = null;
        /// <summary>
        /// 获取或设置下一组分页导航按钮被禁用后的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("下一组分页导航按钮被禁用后的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle NextGroupPageButtonDisabledStyle
        {
            get
            {
                if (this._NextGroupPageButtonDisabledStyle == null)
                {
                    this._NextGroupPageButtonDisabledStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._NextGroupPageButtonDisabledStyle).TrackViewState();
                }
                return this._NextGroupPageButtonDisabledStyle;
            }
            set
            {
                this._NextGroupPageButtonDisabledStyle = value;
            }
        }


        /// <summary>
        /// 尾页导航按钮被禁用后的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _LastPageButtonDisabledStyle = null;
        /// <summary>
        /// 获取或设置尾页导航按钮被禁用后的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("尾页导航按钮被禁用后的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle LastPageButtonDisabledStyle
        {
            get
            {
                if (this._LastPageButtonDisabledStyle == null)
                {
                    this._LastPageButtonDisabledStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._LastPageButtonDisabledStyle).TrackViewState();
                }
                return this._LastPageButtonDisabledStyle;
            }
            set
            {
                this._LastPageButtonDisabledStyle = value;
            }
        }


        /// <summary>
        /// 页码输入控件被禁用后的样式。
        /// </summary>
        private System.Web.UI.WebControls.Style _InputPageNumberTextBoxDisabledStyle = null;
        /// <summary>
        /// 获取或设置页码输入控件被禁用后的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("页码输入控件被禁用后的样式。"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.Style InputPageNumberTextBoxDisabledStyle
        {
            get
            {
                if (this._InputPageNumberTextBoxDisabledStyle == null)
                {
                    this._InputPageNumberTextBoxDisabledStyle = new System.Web.UI.WebControls.Style();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._InputPageNumberTextBoxDisabledStyle).TrackViewState();
                }
                return this._InputPageNumberTextBoxDisabledStyle;
            }
            set
            {
                this._InputPageNumberTextBoxDisabledStyle = value;
            }
        }



        /// <summary>
        /// 跳转到输入的页码按钮被禁用后的样式。
        /// </summary>
        private System.Web.UI.WebControls.TableItemStyle _InputPageNumberButtonDisabledStyle = null;
        /// <summary>
        /// 获取或设置跳转到输入的页码按钮被禁用后的样式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("跳转到输入的页码按钮被禁用后的样式"),
        System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Content),
        System.ComponentModel.NotifyParentProperty(true),
        System.Web.UI.PersistenceMode(System.Web.UI.PersistenceMode.InnerProperty),
        ]
        public System.Web.UI.WebControls.TableItemStyle InputPageNumberButtonDisabledStyle
        {
            get
            {
                if (this._InputPageNumberButtonDisabledStyle == null)
                {
                    this._InputPageNumberButtonDisabledStyle = new System.Web.UI.WebControls.TableItemStyle();
                    if (this.IsTrackingViewState)
                        ((System.Web.UI.IStateManager)this._InputPageNumberButtonDisabledStyle).TrackViewState();
                }
                return this._InputPageNumberButtonDisabledStyle;
            }
            set
            {
                this._InputPageNumberButtonDisabledStyle = value;
            }
        }

        #endregion

        #region 子项文本定义。
        /// <summary>
        /// 页码文本格式。
        /// </summary>
        private System.String _PageNumberText = "{0}";
        /// <summary>
        /// 获取或设置页码文本格式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("{0}"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("页码文本格式，默认值为“{0}”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String PageNumberText
        {
            get
            {
                object savedState = this.ViewState["PageNumberText"];
                if (savedState != null) this._PageNumberText = (System.String)savedState;
                return this._PageNumberText;
            }
            set
            {
                this._PageNumberText = value;
                this.ViewState["PageNumberText"] = value;
            }
        }


        /// <summary>
        /// “本页{0}条记录”文本格式。
        /// </summary>
        private System.String _CurrentRecordsCountText = "本页{0}条记录";
        /// <summary>
        /// 获取或设置“本页{0}条记录”文本格式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("本页{0}条记录"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("“本页{0}条记录”文本格式，默认值为“本页{0}条记录”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String CurrentRecordsCountText
        {
            get
            {
                object savedState = this.ViewState["CurrentRecordsCountText"];
                if (savedState != null) this._CurrentRecordsCountText = (System.String)savedState;
                return this._CurrentRecordsCountText;
            }
            set
            {
                this._CurrentRecordsCountText = value;
                this.ViewState["CurrentRecordsCountText"] = value;
            }
        }


        /// <summary>
        /// “共{0}条记录”文本格式。
        /// </summary>
        private System.String _RecordsCountText = "共{0}条记录";
        /// <summary>
        /// 获取或设置“共{0}条记录”文本格式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("共{0}条记录"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("“共{0}条记录”文本格式，默认值为“共{0}条记录”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String RecordsCountText
        {
            get
            {
                object savedState = this.ViewState["RecordsCountText"];
                if (savedState != null) this._RecordsCountText = (System.String)savedState;
                return this._RecordsCountText;
            }
            set
            {
                this._RecordsCountText = value;
                this.ViewState["RecordsCountText"] = value;
            }
        }


        /// <summary>
        /// “第{0}页”文本格式。
        /// </summary>
        private System.String _PageIndexText = "第{0}页";
        /// <summary>
        /// 获取或设置“第{0}页”文本格式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("第{0}页"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("“第{0}页”文本格式，默认值为“第{0}页”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String PageIndexText
        {
            get
            {
                object savedState = this.ViewState["PageIndexText"];
                if (savedState != null) this._PageIndexText = (System.String)savedState;
                return this._PageIndexText;
            }
            set
            {
                this._PageIndexText = value;
                this.ViewState["PageIndexText"] = value;
            }
        }


        /// <summary>
        ///“共{0}页”文本格式。
        /// </summary>
        private System.String _PageCountText = "共{0}页";
        /// <summary>
        /// 获取或设置“共{0}页”文本格式。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("共{0}页"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("“共{0}页”文本格式，默认值为“共{0}页”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String PageCountText
        {
            get
            {
                object savedState = this.ViewState["PageCountText"];
                if (savedState != null) this._PageCountText = (System.String)savedState;
                return this._PageCountText;
            }
            set
            {
                this._PageCountText = value;
                this.ViewState["PageCountText"] = value;
            }
        }


        /// <summary>
        /// 上一页导航按钮文本。
        /// </summary>
        private System.String _PrevPageText = "上一页";
        /// <summary>
        /// 获取或设置上一页导航按钮文本。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("上一页"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("上一页导航按钮文本，默认值为“上一页”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String PrevPageText
        {
            get
            {
                object savedState = this.ViewState["PrevPageText"];
                if (savedState != null) this._PrevPageText = (System.String)savedState;
                return this._PrevPageText;
            }
            set
            {
                this._PrevPageText = value;
                this.ViewState["PrevPageText"] = value;
            }
        }


        /// <summary>
        /// 下一页导航按钮文本。
        /// </summary>
        private System.String _NextPageText = "下一页";
        /// <summary>
        /// 获取或设置下一页导航按钮文本。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("下一页"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("下一页导航按钮文本，默认值为“下一页”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String NextPageText
        {
            get
            {
                object savedState = this.ViewState["NextPageText"];
                if (savedState != null) this._NextPageText = (System.String)savedState;
                return this._NextPageText;
            }
            set
            {
                this._NextPageText = value;
                this.ViewState["NextPageText"] = value;
            }
        }


        /// <summary>
        /// 上一组分页导航按钮文本。
        /// </summary>
        private System.String _PrevGroupPageText = "上一组";
        /// <summary>
        /// 获取或设置上一组分页导航按钮文本。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("上一组"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("上一组分页导航按钮文本，默认值为“上一组”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String PrevGroupPageText
        {
            get
            {
                object savedState = this.ViewState["PrevGroupPageText"];
                if (savedState != null) this._PrevGroupPageText = (System.String)savedState;
                return this._PrevGroupPageText;
            }
            set
            {
                this._PrevGroupPageText = value;
                this.ViewState["PrevGroupPageText"] = value;
            }
        }


        /// <summary>
        /// 下一组分页导航按钮文本。
        /// </summary>
        private System.String _NextGroupPageText = "下一组";
        /// <summary>
        /// 获取或设置下一组分页导航按钮文本。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("下一组"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("下一组分页导航按钮文本，默认值为“下一组”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String NextGroupPageText
        {
            get
            {
                object savedState = this.ViewState["NextGroupPageText"];
                if (savedState != null) this._NextGroupPageText = (System.String)savedState;
                return this._NextGroupPageText;
            }
            set
            {
                this._NextGroupPageText = value;
                this.ViewState["NextGroupPageText"] = value;
            }
        }


        /// <summary>
        /// 首页导航按钮文本。
        /// </summary>
        private System.String _FirstPageText = "首页";
        /// <summary>
        /// 获取或设置首页导航按钮文本。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("首页"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("首页导航按钮文本，默认值为“首页”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String FirstPageText
        {
            get
            {
                object savedState = this.ViewState["FirstPageText"];
                if (savedState != null) this._FirstPageText = (System.String)savedState;
                return this._FirstPageText;
            }
            set
            {
                this._FirstPageText = value;
                this.ViewState["FirstPageText"] = value;
            }
        }


        /// <summary>
        /// 尾页导航按钮文本。
        /// </summary>
        private System.String _LastPageText = "尾页";
        /// <summary>
        /// 获取或设置尾页导航按钮文本。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("尾页"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("尾页导航按钮文本，默认值为“尾页”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String LastPageText
        {
            get
            {
                object savedState = this.ViewState["LastPageText"];
                if (savedState != null) this._LastPageText = (System.String)savedState;
                return this._LastPageText;
            }
            set
            {
                this._LastPageText = value;
                this.ViewState["LastPageText"] = value;
            }
        }


        /// <summary>
        /// 跳转到输入的页码标题文本。
        /// </summary>
        private System.String _InputPageNumberTitle = "跳转到{0}页";
        /// <summary>
        /// 获取或设置跳转到输入的页码标题文本。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("跳转到{0}页"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("跳转到输入的页码标题文本，默认值为“跳转到{0}页”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String InputPageNumberTitle
        {
            get
            {
                object savedState = this.ViewState["InputPageNumberTitle"];
                if (savedState != null) this._InputPageNumberTitle = (System.String)savedState;
                return this._InputPageNumberTitle;
            }
            set
            {
                this._InputPageNumberTitle = value;
                this.ViewState["InputPageNumberTitle"] = value;
            }
        }


        /// <summary>
        /// 跳转到输入的页码按钮文本。
        /// </summary>
        private System.String _InputPageNumberButtonText = "确定";
        /// <summary>
        /// 获取或设置跳转到输入的页码按钮文本。
        /// </summary>
        [
        System.ComponentModel.DefaultValue("确定"),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("跳转到输入的页码按钮文本，默认值为“确定”"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public System.String InputPageNumberButtonText
        {
            get
            {
                object savedState = this.ViewState["InputPageNumberButtonText"];
                if (savedState != null) this._InputPageNumberButtonText = (System.String)savedState;
                return this._InputPageNumberButtonText;
            }
            set
            {
                this._InputPageNumberButtonText = value;
                this.ViewState["InputPageNumberButtonText"] = value;
            }
        }

        #endregion

        #region 子项显示控制。
        /// <summary>
        /// 指示是否显示导航页码。
        /// </summary>
        private bool _ShowPageNumber = true;
        /// <summary>
        /// 获取或设置一个值，指示是否显示导航页码。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(true),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("指示是否显示导航页码"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool ShowPageNumber
        {
            get
            {
                object savedState = this.ViewState["ShowPageNumber"];
                if (savedState != null) this._ShowPageNumber = (bool)savedState;
                return this._ShowPageNumber;
            }
            set
            {
                this._ShowPageNumber = value;
                this.ViewState["ShowPageNumber"] = value;
            }
        }


        /// <summary>
        /// 指示是否显示上一页和下一页导航按钮。
        /// </summary>
        private bool _ShowPrevAndNext = true;
        /// <summary>
        /// 获取或设置一个值，指示是否显示上一页和下一页导航按钮。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(true),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("指示是否显示上一页和下一页导航按钮"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool ShowPrevAndNext
        {
            get
            {
                object savedState = this.ViewState["ShowPrevAndNext"];
                if (savedState != null) this._ShowPrevAndNext = (bool)savedState;
                return this._ShowPrevAndNext;
            }
            set
            {
                this._ShowPrevAndNext = value;
                this.ViewState["ShowPrevAndNext"] = value;
            }
        }


        /// <summary>
        /// 指示是否显示上一组和下一组分页导航按钮。
        /// </summary>
        private bool _ShowPrevAndNextGroup = true;
        /// <summary>
        /// 获取或设置一个值，指示是否显示上一组和下一组分页导航按钮。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(true),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("指示是否显示上一组和下一组分页导航按钮"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool ShowPrevAndNextGroup
        {
            get
            {
                object savedState = this.ViewState["ShowPrevAndNextGroup"];
                if (savedState != null) this._ShowPrevAndNextGroup = (bool)savedState;
                return this._ShowPrevAndNextGroup;
            }
            set
            {
                this._ShowPrevAndNextGroup = value;
                this.ViewState["ShowPrevAndNextGroup"] = value;
            }
        }


        /// <summary>
        /// 指示是否显示首页和尾页导航按钮。
        /// </summary>
        private bool _ShowFirstAndLast = true;
        /// <summary>
        /// 获取或设置一个值，指示是否显示首页和尾页导航按钮。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(true),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("指示是否显示首页和尾页导航按钮"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool ShowFirstAndLast
        {
            get
            {
                object savedState = this.ViewState["ShowFirstAndLast"];
                if (savedState != null) this._ShowFirstAndLast = (bool)savedState;
                return this._ShowFirstAndLast;
            }
            set
            {
                this._ShowFirstAndLast = value;
                this.ViewState["ShowFirstAndLast"] = value;
            }
        }


        /// <summary>
        /// 指示是否显示“本页{0}条记录”文本。
        /// </summary>
        private bool _ShowCurrentRecordsCount = true;
        /// <summary>
        /// 获取或设置一个值，指示是否显示“本页{0}条记录”文本。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(true),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("指示是否显示“本页{0}条记录”文本"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool ShowCurrentRecordsCount
        {
            get
            {
                object savedState = this.ViewState["ShowCurrentRecordsCount"];
                if (savedState != null) this._ShowCurrentRecordsCount = (bool)savedState;
                return this._ShowCurrentRecordsCount;
            }
            set
            {
                this._ShowCurrentRecordsCount = value;
                this.ViewState["ShowCurrentRecordsCount"] = value;
            }
        }


        /// <summary>
        /// 指示是否显示“共{0}条记录”文本。
        /// </summary>
        private bool _ShowRecordsCount = true;
        /// <summary>
        /// 获取或设置一个值，指示是否显示“共{0}条记录”文本。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(true),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("指示是否显示“共{0}条记录”文本"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool ShowRecordsCount
        {
            get
            {
                object savedState = this.ViewState["ShowRecordsCount"];
                if (savedState != null) this._ShowRecordsCount = (bool)savedState;
                return this._ShowRecordsCount;
            }
            set
            {
                this._ShowRecordsCount = value;
                this.ViewState["ShowRecordsCount"] = value;
            }
        }


        /// <summary>
        /// 指示是否显示“第{0}页”文本。
        /// </summary>
        private bool _ShowPageIndex = true;
        /// <summary>
        /// 获取或设置一个值，指示是否显示“第{0}页”文本。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(true),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("指示是否显示“第{0}页”文本"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool ShowPageIndex
        {
            get
            {
                object savedState = this.ViewState["ShowPageIndex"];
                if (savedState != null) this._ShowPageIndex = (bool)savedState;
                return this._ShowPageIndex;
            }
            set
            {
                this._ShowPageIndex = value;
                this.ViewState["ShowPageIndex"] = value;
            }
        }


        /// <summary>
        /// 指示是否显示“共{0}页”文本。
        /// </summary>
        private bool _ShowPageCount = true;
        /// <summary>
        /// 获取或设置一个值，指示是否显示“共{0}页”文本。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(true),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("指示是否显示“共{0}页”文本"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool ShowPageCount
        {
            get
            {
                object savedState = this.ViewState["ShowPageCount"];
                if (savedState != null) this._ShowPageCount = (bool)savedState;
                return this._ShowPageCount;
            }
            set
            {
                this._ShowPageCount = value;
                this.ViewState["ShowPageCount"] = value;
            }
        }


        /// <summary>
        /// 指示是否显示页码输入控件。
        /// </summary>
        private bool _ShowInputPageNumber = true;
        /// <summary>
        /// 获取或设置一个值，指示是否显示页码输入控件。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(true),
        System.ComponentModel.Category("Appearance"),
        System.ComponentModel.Description("指示是否显示页码输入控件"),
        System.ComponentModel.NotifyParentProperty(true),
        ]
        public bool ShowInputPageNumber
        {
            get
            {
                object savedState = this.ViewState["ShowInputPageNumber"];
                if (savedState != null) this._ShowInputPageNumber = (bool)savedState;
                return this._ShowInputPageNumber;
            }
            set
            {
                this._ShowInputPageNumber = value;
                this.ViewState["ShowInputPageNumber"] = value;
            }
        }
        #endregion

        #region 事件定义。
        /// <summary>
        /// 分页事件代理。
        /// </summary>
        /// <param name="sender">引发事件的控件。</param>
        /// <param name="e">提供分页事件数据。</param>
        public delegate void PageCommandEventHandler(object sender, System.Web.UI.WebControls.CommandEventArgs e);
        private event PageCommandEventHandler _PageSelectedCommand = null;
        /// <summary>
        /// 用户点击页码按钮事件。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("用户点击页码按钮事件。"),
        ]
        public event PageCommandEventHandler PageSelectedCommand
        {
            add
            {
                this._PageSelectedCommand += value;
            }
            remove
            {
                this._PageSelectedCommand -= value;
            }
        }
        /// <summary>
        /// 页绑定事件代理。
        /// </summary>
        /// <param name="sender">引发事件的控件。</param>
        /// <param name="e">提供分页事件数据。</param>
        public delegate void PageNumberEventHandler(object sender, Thinksea.WebControls.PageNavigate.PageNumberEventArgs e);
        private event PageNumberEventHandler _PageNumberDataBound = null;
        /// <summary>
        /// 在分页被绑定后发生。
        /// </summary>
        [
        System.ComponentModel.DefaultValue(null),
        System.ComponentModel.Category("Data"),
        System.ComponentModel.Description("在分页被绑定后发生"),
        ]
        public event PageNumberEventHandler PageNumberDataBound
        {
            add
            {
                this._PageNumberDataBound += value;
            }
            remove
            {
                this._PageNumberDataBound -= value;
            }
        }

        #endregion

        /// <summary>
        /// 此表格为子控件容器。
        /// </summary>
        private System.Web.UI.WebControls.Table table = new Table();

        /// <summary>
        /// 分页页码输入框。
        /// </summary>
        private System.Web.UI.WebControls.TextBox tbInputPageNumber = new TextBox();

        //private System.Web.UI.StateBag _viewstate = new System.Web.UI.StateBag();

        /// <summary>
        /// 一个构造方法。
        /// </summary>
        public PageNavigate()
        {
        }

        /// <summary>
        /// 处理回发数据事件。
        /// </summary>
        /// <param name="eventArgument"></param>
        public void RaisePostBackEvent(string eventArgument)
        {
            if (eventArgument.StartsWith("PageIndex_"))
            {
                int NewPageIndex = int.Parse(eventArgument.Substring("PageIndex_".Length));
                this.PageIndex = NewPageIndex;
                if (this._PageSelectedCommand != null)
                {
                    this._PageSelectedCommand(this, new System.Web.UI.WebControls.CommandEventArgs("PageSelected", this.PageIndex));
                }
            }
            else if (eventArgument == "GotoInputPageNumber")
            {
                this.PageIndex = System.Convert.ToInt32(this.tbInputPageNumber.Text) - 1;
                if (this._PageSelectedCommand != null)
                {
                    this._PageSelectedCommand(this, new System.Web.UI.WebControls.CommandEventArgs("PageSelected", this.PageIndex));
                }
            }
        }

        /// <summary>
        /// 创建子控件。
        /// </summary>
        protected override void CreateChildControls()
        {
            base.CreateChildControls();

            this.table.Style[HtmlTextWriterStyle.BorderCollapse] = "collapse";
            this.table.Style[HtmlTextWriterStyle.BorderWidth] = "0px";
            this.table.Style[HtmlTextWriterStyle.BorderStyle] = "none";
            this.table.Style["border-spacing"] = "0px";
            this.table.Style[HtmlTextWriterStyle.Padding] = "0px";
            this.table.Style[HtmlTextWriterStyle.Margin] = "auto";
            TableRow row = new TableRow();
            this.table.Rows.Add(row);
            this.Controls.Add(this.table);

            this.tbInputPageNumber.ID = "InputPageNumber";
            this.tbInputPageNumber.MaxLength = 9;
            //this.tbInputPageNumber.Style[HtmlTextWriterStyle.TextAlign] = "center";
            this.Controls.Add(this.tbInputPageNumber);

        }

        /// <summary>
        /// 引发 System.Web.UI.Control.PreRender 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs 对象。</param>
        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);

            this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PageNavigate_Validate", @"
//验证页码输入控件。
function PageNavigate_Validate(InputPageNumberControlID)
{
    var pnCtl = document.getElementById(InputPageNumberControlID);
    if(pnCtl.showErrorMessage==true)
    {
        return false;
    }
    var reg=/^\d+$/gi;
    if(!reg.test(pnCtl.value))
    {
        pnCtl.showErrorMessage=true;
        alert(""输入的页码格式无效。"");
        pnCtl.showErrorMessage=false;
        pnCtl.focus();
        return false;
    }
    return true;
}
", true);

            if (this.EnableURLPage)
            {
                this.Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "PageNavigate", @"
/*
功能：为指定的 URI 设置参数。
参数：
    uri：一个可能包含参数的 uri 字符串。
    Name：参数名。
    Value：新的参数值。
返回值：处理后的 uri。
备注：如果参数存在则更改它的值，否则添加这个参数。
*/
function PageNavigate_SetUriParameter(uri, Name, Value)
{
    uri=uri.replace(/(\s|\?)*$/g, """");//消除 URI 中无参数但存在问号“...?”这种 URI 中的问号。
    if(uri.indexOf(""?"")==-1)
    {//如果无参数。
        return uri + ""?"" + Name + ""="" + encodeURIComponent( Value );
    }
	else
	{//如果有参数。
	    var reg=new RegExp(""(\\?|&)"" + Name.replace(/\$/gi, ""\\$"") + ""=([^&]*)"",""gi"");//测试可能被替换的参数的正则表达式。
		if( reg.test(uri) )
		{//如果存在同名参数。
		    return uri.replace(reg, ""$1"" + Name.replace(/\$/gi, ""$$$$"") + ""="" + encodeURIComponent( Value ));
		}
		else
		{//如果无同名参数。
			return uri + ""&"" + Name + ""="" + encodeURIComponent( Value );
		}
	}
}

//跳转到指定的页码。
function PageNavigate_GotoPage(PageIndexParameter, InputPageNumberControlID)
{
    if(!PageNavigate_Validate(InputPageNumberControlID))
    {
        return;
    }
    var pnCtl = document.getElementById(InputPageNumberControlID);
    var pageIndex = parseInt(pnCtl.value) - 1;
    document.location.href = PageNavigate_SetUriParameter(document.location.href, PageIndexParameter, pageIndex);
}
", true);
            }

        }

        /// <summary>
        /// 将此控件呈现给指定的输出参数。
        /// </summary>
        /// <param name="output"> 要写出到的 HTML 编写器 </param>
        protected override void Render(HtmlTextWriter output)
        {
            this.Style[HtmlTextWriterStyle.WhiteSpace] = "nowrap";
            this.Style[HtmlTextWriterStyle.TextAlign] = "center";

            base.Render(output);
        }

        /// <summary>
        /// 设置控件为禁用状态。
        /// </summary>
        /// <param name="control"></param>
        private void SetButtonDisabled(WebControl control)
        {
            control.Enabled = false;
            //control.ForeColor = System.Drawing.ColorTranslator.FromHtml("#A0A0A0");
        }

        /// <summary>
        /// 添加子控件。
        /// </summary>
        /// <param name="control"></param>
        private void AddContentControl(System.Web.UI.Control control)
        {
            TableRow row = this.table.Rows[0];
            TableCell cell = new TableCell();
            cell.Controls.Add(control);
            row.Cells.Add(cell);
        }

        /// <summary>
        /// 将控件的内容呈现到指定的编写器中。此方法主要由控件开发人员使用。
        /// </summary>
        /// <param name="writer"></param>
        protected override void RenderContents(HtmlTextWriter writer)
        {
            if (!this.ChildControlsCreated)
            {
                this.CreateChildControls();
            }

            this.table.Rows[0].Cells.Clear(); //清理子控件，重新生成。

            #region 本页{0}条记录
            if (this.ShowCurrentRecordsCount && string.IsNullOrEmpty(this.CurrentRecordsCountText) == false)
            {
                System.Web.UI.WebControls.Literal lCurrentRecordsCount = new System.Web.UI.WebControls.Literal();
                lCurrentRecordsCount.Text = this.CurrentRecordsCountText.Replace("{0}", this.CurrentRecordsCount.ToString());
                this.AddContentControl(lCurrentRecordsCount);
            }
            #endregion

            #region 共{0}条记录
            if (this.ShowRecordsCount && string.IsNullOrEmpty(this.RecordsCountText) == false)
            {
                System.Web.UI.WebControls.Literal lRecordsCount = new System.Web.UI.WebControls.Literal();
                lRecordsCount.Text = this.RecordsCountText.Replace("{0}", this.RecordsCount.ToString());
                this.AddContentControl(lRecordsCount);
            }
            #endregion

            #region 首页按钮
            if (this.ShowFirstAndLast && string.IsNullOrEmpty(this.FirstPageText) == false)
            {
                System.Web.UI.WebControls.HyperLink btnFirstPageButton = new System.Web.UI.WebControls.HyperLink();
                btnFirstPageButton.Text = this.FirstPageText;
                btnFirstPageButton.ApplyStyle(this.PageButtonStyle);
                btnFirstPageButton.ApplyStyle(this.FirstPageButtonStyle);
                if (this.PageIndex == 0 || this.Enabled == false)
                {
                    this.SetButtonDisabled(btnFirstPageButton);
                    btnFirstPageButton.ApplyStyle(this.FirstPageButtonDisabledStyle);
                }
                else
                {
                    int pageNum = 0;
                    if (this.EnableURLPage)
                    {
                        this.SetPageNumberURL(btnFirstPageButton, pageNum);
                    }
                    else
                    {
                        btnFirstPageButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pageNum.ToString());
                    }
                    if (this._PageNumberDataBound != null)
                    {
                        Thinksea.WebControls.PageNavigate.PageNumberEventArgs pne = new Thinksea.WebControls.PageNavigate.PageNumberEventArgs(btnFirstPageButton, pageNum);
                        this._PageNumberDataBound(this, pne);
                        if (pne.PageNumber != pageNum) //如果用户改变了页面则重新处理分页问题。
                        {
                            if (this.EnableURLPage)
                            {
                                this.SetPageNumberURL(btnFirstPageButton, pne.PageNumber);
                            }
                            else
                            {
                                btnFirstPageButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pne.PageNumber.ToString());
                            }
                        }
                    }
                }
                this.AddContentControl(btnFirstPageButton);
            }
            #endregion

            #region 上一组分页按钮
            if (this.ShowPrevAndNextGroup && string.IsNullOrEmpty(this.PrevGroupPageText) == false)
            {
                System.Web.UI.WebControls.HyperLink btnPrevGroupPageButton = new System.Web.UI.WebControls.HyperLink();
                btnPrevGroupPageButton.Text = this.PrevGroupPageText;
                btnPrevGroupPageButton.ApplyStyle(this.PageButtonStyle);
                btnPrevGroupPageButton.ApplyStyle(this.PrevGroupPageButtonStyle);
                if (this.PageIndex == 0 || this.Enabled == false)
                {
                    this.SetButtonDisabled(btnPrevGroupPageButton);
                    btnPrevGroupPageButton.ApplyStyle(this.PrevGroupPageButtonDisabledStyle);
                }
                else
                {
                    int pageNum = (this.PageIndex - this.NavigatePageSize < 0) ? 0 : this.PageIndex - this.NavigatePageSize;
                    if (this.EnableURLPage)
                    {
                        this.SetPageNumberURL(btnPrevGroupPageButton, pageNum);
                    }
                    else
                    {
                        btnPrevGroupPageButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pageNum.ToString());
                    }
                    if (this._PageNumberDataBound != null)
                    {
                        Thinksea.WebControls.PageNavigate.PageNumberEventArgs pne = new Thinksea.WebControls.PageNavigate.PageNumberEventArgs(btnPrevGroupPageButton, pageNum);
                        this._PageNumberDataBound(this, pne);
                        if (pne.PageNumber != pageNum) //如果用户改变了页面则重新处理分页问题。
                        {
                            if (this.EnableURLPage)
                            {
                                this.SetPageNumberURL(btnPrevGroupPageButton, pne.PageNumber);
                            }
                            else
                            {
                                btnPrevGroupPageButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pne.PageNumber.ToString());
                            }
                        }
                    }
                }
                this.AddContentControl(btnPrevGroupPageButton);
            }
            #endregion

            #region 上一页按钮
            if (this.ShowPrevAndNext && string.IsNullOrEmpty(this.PrevPageText) == false)
            {
                System.Web.UI.WebControls.HyperLink btnPrevPageButton = new System.Web.UI.WebControls.HyperLink();
                btnPrevPageButton.Text = this.PrevPageText;
                btnPrevPageButton.ApplyStyle(this.PageButtonStyle);
                btnPrevPageButton.ApplyStyle(this.PrevPageButtonStyle);
                if (this.PageIndex == 0 || this.Enabled == false)
                {
                    this.SetButtonDisabled(btnPrevPageButton);
                    btnPrevPageButton.ApplyStyle(this.PrevPageButtonDisabledStyle);
                }
                else
                {
                    int pageNum = this.PageIndex - 1;
                    if (this.EnableURLPage)
                    {
                        this.SetPageNumberURL(btnPrevPageButton, pageNum);
                    }
                    else
                    {
                        btnPrevPageButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pageNum.ToString());
                    }
                    if (this._PageNumberDataBound != null)
                    {
                        Thinksea.WebControls.PageNavigate.PageNumberEventArgs pne = new Thinksea.WebControls.PageNavigate.PageNumberEventArgs(btnPrevPageButton, pageNum);
                        this._PageNumberDataBound(this, pne);
                        if (pne.PageNumber != pageNum) //如果用户改变了页面则重新处理分页问题。
                        {
                            if (this.EnableURLPage)
                            {
                                this.SetPageNumberURL(btnPrevPageButton, pne.PageNumber);
                            }
                            else
                            {
                                btnPrevPageButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pne.PageNumber.ToString());
                            }
                        }
                    }
                }
                this.AddContentControl(btnPrevPageButton);
            }
            #endregion

            #region 页索引
            if (this.ShowPageNumber && string.IsNullOrEmpty(this.PageNumberText) == false)
            {
                int pageNum = this.PageIndex - this.NavigatePageSize / 2; //分页页码。
                if (pageNum + this.NavigatePageSize > this.PageCount - 1)
                {
                    pageNum = this.PageCount - this.NavigatePageSize;
                }
                if (pageNum < 0)
                {
                    pageNum = 0;
                }
                for (int i = 0; i < this.NavigatePageSize && pageNum < this.PageCount; i++)
                {
                    System.Web.UI.WebControls.HyperLink btnPageNumberButton = new System.Web.UI.WebControls.HyperLink();
                    btnPageNumberButton.Text = string.Format(this.PageNumberText, pageNum + 1);
                    btnPageNumberButton.ApplyStyle(this.PageNumberStyle);
                    if (pageNum == this.PageIndex)
                    {
                        btnPageNumberButton.ApplyStyle(this.CurrentPageNumberStyle);
                    }
                    //else
                    //{
                    //    if (this.Enabled)
                    //    {
                    //        btnPageNumberButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + StartPageIndex.ToString());
                    //    }
                    //}
                    if (this.Enabled)
                    {
                        if (pageNum != this.PageIndex)
                        {
                            if (this.EnableURLPage)
                            {
                                this.SetPageNumberURL(btnPageNumberButton, pageNum);
                            }
                            else
                            {
                                btnPageNumberButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pageNum.ToString());
                            }
                        }
                        if (this._PageNumberDataBound != null)
                        {
                            Thinksea.WebControls.PageNavigate.PageNumberEventArgs pne = new Thinksea.WebControls.PageNavigate.PageNumberEventArgs(btnPageNumberButton, pageNum);
                            this._PageNumberDataBound(this, pne);
                            if (pne.PageNumber != pageNum) //如果用户改变了页面则重新处理分页问题。
                            {
                                if (pageNum != this.PageIndex)
                                {
                                    if (this.EnableURLPage)
                                    {
                                        this.SetPageNumberURL(btnPageNumberButton, pne.PageNumber);
                                    }
                                    else
                                    {
                                        btnPageNumberButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pne.PageNumber.ToString());
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        btnPageNumberButton.ApplyStyle(this.PageNumberDisabledStyle);
                        if (pageNum == this.PageIndex)
                        {
                            btnPageNumberButton.ApplyStyle(this.CurrentPageNumberDisabledStyle);
                        }
                    }
                    this.AddContentControl(btnPageNumberButton);
                    pageNum++;
                }
            }
            #endregion

            #region 下一页按钮
            if (this.ShowPrevAndNext && string.IsNullOrEmpty(this.NextPageText) == false)
            {
                System.Web.UI.WebControls.HyperLink btnNextPageButton = new System.Web.UI.WebControls.HyperLink();
                btnNextPageButton.Text = this.NextPageText;
                btnNextPageButton.ApplyStyle(this.PageButtonStyle);
                btnNextPageButton.ApplyStyle(this.NextPageButtonStyle);
                if (this.PageIndex < this.PageCount - 1 && this.Enabled)
                {
                    int pageNum = this.PageIndex + 1;
                    if (this.EnableURLPage)
                    {
                        this.SetPageNumberURL(btnNextPageButton, pageNum);
                    }
                    else
                    {
                        btnNextPageButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pageNum.ToString());
                    }
                    if (this._PageNumberDataBound != null)
                    {
                        Thinksea.WebControls.PageNavigate.PageNumberEventArgs pne = new Thinksea.WebControls.PageNavigate.PageNumberEventArgs(btnNextPageButton, pageNum);
                        this._PageNumberDataBound(this, pne);
                        if (pne.PageNumber != pageNum) //如果用户改变了页面则重新处理分页问题。
                        {
                            if (this.EnableURLPage)
                            {
                                this.SetPageNumberURL(btnNextPageButton, pne.PageNumber);
                            }
                            else
                            {
                                btnNextPageButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pne.PageNumber.ToString());
                            }
                        }
                    }
                }
                else
                {
                    this.SetButtonDisabled(btnNextPageButton);
                    btnNextPageButton.ApplyStyle(this.NextPageButtonDisabledStyle);
                }
                this.AddContentControl(btnNextPageButton);
            }
            #endregion

            #region 下一组分页按钮
            if (this.ShowPrevAndNextGroup && string.IsNullOrEmpty(this.NextGroupPageText) == false)
            {
                System.Web.UI.WebControls.HyperLink btnNextGroupPageButton = new System.Web.UI.WebControls.HyperLink();
                btnNextGroupPageButton.Text = this.NextGroupPageText;
                btnNextGroupPageButton.ApplyStyle(this.PageButtonStyle);
                btnNextGroupPageButton.ApplyStyle(this.NextGroupPageButtonStyle);
                if (this.PageIndex < this.PageCount - 1 && this.Enabled)
                {
                    int pageNum = (this.PageIndex + this.NavigatePageSize > this.PageCount - 1) ? this.PageCount - 1 : this.PageIndex + this.NavigatePageSize;
                    if (this.EnableURLPage)
                    {
                        this.SetPageNumberURL(btnNextGroupPageButton, pageNum);
                    }
                    else
                    {
                        btnNextGroupPageButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pageNum.ToString());
                    }
                    if (this._PageNumberDataBound != null)
                    {
                        Thinksea.WebControls.PageNavigate.PageNumberEventArgs pne = new Thinksea.WebControls.PageNavigate.PageNumberEventArgs(btnNextGroupPageButton, pageNum);
                        this._PageNumberDataBound(this, pne);
                        if (pne.PageNumber != pageNum) //如果用户改变了页面则重新处理分页问题。
                        {
                            if (this.EnableURLPage)
                            {
                                this.SetPageNumberURL(btnNextGroupPageButton, pne.PageNumber);
                            }
                            else
                            {
                                btnNextGroupPageButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pne.PageNumber.ToString());
                            }
                        }
                    }
                }
                else
                {
                    this.SetButtonDisabled(btnNextGroupPageButton);
                    btnNextGroupPageButton.ApplyStyle(this.NextGroupPageButtonDisabledStyle);
                }
                this.AddContentControl(btnNextGroupPageButton);
            }
            #endregion

            #region 尾页按钮
            if (this.ShowFirstAndLast && string.IsNullOrEmpty(this.LastPageText) == false)
            {
                System.Web.UI.WebControls.HyperLink btnLastPageButton = new System.Web.UI.WebControls.HyperLink();
                btnLastPageButton.Text = this.LastPageText;
                btnLastPageButton.ApplyStyle(this.PageButtonStyle);
                btnLastPageButton.ApplyStyle(this.LastPageButtonStyle);
                if (this.PageIndex < this.PageCount - 1 && this.Enabled)
                {
                    int pageNum = this.PageCount - 1;
                    if (this.EnableURLPage)
                    {
                        this.SetPageNumberURL(btnLastPageButton, pageNum);
                    }
                    else
                    {
                        btnLastPageButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pageNum.ToString());
                    }
                    if (this._PageNumberDataBound != null)
                    {
                        Thinksea.WebControls.PageNavigate.PageNumberEventArgs pne = new Thinksea.WebControls.PageNavigate.PageNumberEventArgs(btnLastPageButton, pageNum);
                        this._PageNumberDataBound(this, pne);
                        if (pne.PageNumber != pageNum) //如果用户改变了页面则重新处理分页问题。
                        {
                            if (this.EnableURLPage)
                            {
                                this.SetPageNumberURL(btnLastPageButton, pne.PageNumber);
                            }
                            else
                            {
                                btnLastPageButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "PageIndex_" + pne.PageNumber.ToString());
                            }
                        }
                    }
                }
                else
                {
                    this.SetButtonDisabled(btnLastPageButton);
                    btnLastPageButton.ApplyStyle(this.LastPageButtonDisabledStyle);
                }
                this.AddContentControl(btnLastPageButton);
            }
            #endregion

            #region 第{0}页
            if (this.ShowPageIndex && string.IsNullOrEmpty(this.PageIndexText) == false)
            {
                System.Web.UI.WebControls.Literal lPageIndex = new System.Web.UI.WebControls.Literal();
                lPageIndex.Text = this.PageIndexText.Replace("{0}", (this.RecordsCount == 0 ? 0 : this.PageIndex + 1).ToString());
                this.AddContentControl(lPageIndex);
            }
            #endregion

            #region 共{0}页
            if (this.ShowPageCount && string.IsNullOrEmpty(this.PageCountText) == false)
            {
                System.Web.UI.WebControls.Literal lPagesCount = new System.Web.UI.WebControls.Literal();
                lPagesCount.Text = this.PageCountText.Replace("{0}", this.PageCount.ToString());
                this.AddContentControl(lPagesCount);
            }
            #endregion

            #region 显示页码输入控件。
            if (this.ShowInputPageNumber)
            {
                string[] spInputPageNumberTitle = this.InputPageNumberTitle.Split(new string[] { "{0}" }, StringSplitOptions.None);
                if (spInputPageNumberTitle.Length > 0 && string.IsNullOrEmpty(spInputPageNumberTitle[0]) == false)
                {
                    System.Web.UI.WebControls.Literal lInputPageNumberTitle = new System.Web.UI.WebControls.Literal();
                    lInputPageNumberTitle.Text = spInputPageNumberTitle[0];
                    this.AddContentControl(lInputPageNumberTitle);
                }

                {
                    this.tbInputPageNumber.Text = (this.PageIndex + 1).ToString();
                    this.tbInputPageNumber.ReadOnly = !this.Enabled;
                    this.tbInputPageNumber.ApplyStyle(this.InputPageNumberTextBoxStyle);
                    if (!this.Enabled)
                    {
                        this.tbInputPageNumber.ApplyStyle(this.InputPageNumberTextBoxDisabledStyle);
                    }
                    this.AddContentControl(this.tbInputPageNumber);
                    this.tbInputPageNumber.Attributes["onblur"] = "PageNavigate_Validate('" + this.tbInputPageNumber.ClientID + "');";
                }

                if (spInputPageNumberTitle.Length > 1 && string.IsNullOrEmpty(spInputPageNumberTitle[1]) == false)
                {
                    System.Web.UI.WebControls.Literal lInputPageNumberTitle = new System.Web.UI.WebControls.Literal();
                    lInputPageNumberTitle.Text = spInputPageNumberTitle[1];
                    this.AddContentControl(lInputPageNumberTitle);
                }

                if (string.IsNullOrEmpty(this.InputPageNumberButtonText) == false)
                {
                    System.Web.UI.WebControls.HyperLink btnGotoInputPageNumberButton = new System.Web.UI.WebControls.HyperLink();
                    btnGotoInputPageNumberButton.ID = "GotoInputPageNumberButton";
                    btnGotoInputPageNumberButton.Text = this.InputPageNumberButtonText;
                    btnGotoInputPageNumberButton.ApplyStyle(this.PageButtonStyle);
                    btnGotoInputPageNumberButton.ApplyStyle(this.InputPageNumberButtonStyle);
                    this.AddContentControl(btnGotoInputPageNumberButton);
                    if (this.Enabled)
                    {
                        btnGotoInputPageNumberButton.NavigateUrl = "javascript:void(0)";
                        if (this.EnableURLPage)
                        {
                            //btnGotoInputPageNumberButton.NavigateUrl = "javascript:PageNavigate_GotoPage('" + Thinksea.Web.ConvertToJavaScriptString(this.PageIndexParameter) + "', '" + this.tbInputPageNumber.ClientID + "');";
                            btnGotoInputPageNumberButton.Attributes["onclick"] = "PageNavigate_GotoPage('" + Thinksea.Web.ConvertToJavaScriptString(this.PageIndexParameter) + "', '" + this.tbInputPageNumber.ClientID + "');";
                        }
                        else
                        {
                            //btnGotoInputPageNumberButton.NavigateUrl = "javascript:" + this.Page.ClientScript.GetPostBackEventReference(this, "GotoInputPageNumber");
                            btnGotoInputPageNumberButton.Attributes["onclick"] = "if(PageNavigate_Validate('" + this.tbInputPageNumber.ClientID + "')){" + this.Page.ClientScript.GetPostBackEventReference(this, "GotoInputPageNumber") + ";}";
                        }
                        this.tbInputPageNumber.Attributes["onkeydown"] = @"if(event.keyCode==13){var btn=document.getElementById('" + btnGotoInputPageNumberButton.ClientID + @"'); if(document.all){btn.click();} else if(document.createEvent){var ev = document.createEvent('HTMLEvents'); ev.initEvent('click', false, true); btn.dispatchEvent(ev);}return false;}";
                    }
                    else
                    {
                        this.SetButtonDisabled(btnGotoInputPageNumberButton);
                        btnGotoInputPageNumberButton.ApplyStyle(this.InputPageNumberButtonDisabledStyle);
                    }
                }
                else
                {
                    if (this.Enabled)
                    {
                        if (this.EnableURLPage)
                        {
                            this.tbInputPageNumber.Attributes["onkeydown"] = @"if(event.keyCode==13){PageNavigate_GotoPage('" + Thinksea.Web.ConvertToJavaScriptString(this.PageIndexParameter) + "', '" + this.tbInputPageNumber.ClientID + "'); return false;}";
                        }
                        else
                        {
                            this.tbInputPageNumber.Attributes["onkeydown"] = @"if(event.keyCode==13){if(PageNavigate_Validate('" + this.tbInputPageNumber.ClientID + "')){" + this.Page.ClientScript.GetPostBackEventReference(this, "GotoInputPageNumber") + ";} return false;}";
                        }
                    }
                }
            }
            else
            {
                this.tbInputPageNumber.Visible = false;
            }
            #endregion

            base.RenderContents(writer);
        }

        /// <summary>
        /// 对于 URL 传递分页数据的情况，处理分页按钮连接 URL。
        /// </summary>
        /// <param name="hc">一个分页链接按钮。</param>
        /// <param name="pageNumber">分页编号。</param>
        private void SetPageNumberURL(HyperLink hc, int pageNumber)
        {
            if (this.DesignMode)
            {
                return;
            }
            //int pageIndex = 0;
            //if (!string.IsNullOrEmpty(this.Page.Request[this.PageIndexParameter]))
            //{
            //    pageIndex = Convert.ToInt32(this.Page.Request[this.PageIndexParameter]);
            //}
            //if (pageIndex == pageNumber) //如果是当前页，则不提供连接 URL。
            //if (this.PageIndex == pageNumber) //如果是当前页，则不提供连接 URL。
            //{
            //    return;
            //}
            string url = Thinksea.Web.SetUriParameter(this.Page.Request.Url.ToString(), this.PageIndexParameter, pageNumber.ToString());
            if (!string.IsNullOrEmpty(this.Page.Request[this.PageSizeParameter]))
            {
                //url = Thinksea.Web.SetUriParameter(url, this.PageSizeParameter, this.Page.Request[this.PageSizeParameter]);
                url = Thinksea.Web.SetUriParameter(url, this.PageSizeParameter, this.PageSize.ToString());
            }
            hc.NavigateUrl = url; //设置连接 URL
        }

        /// <summary>
        /// 加载视图状态。
        /// </summary>
        /// <param name="savedState"></param>
        protected override void LoadViewState(object savedState)
        {
            if (savedState != null)
            {
                object[] myState = (object[])savedState;

                if (myState[0] != null)
                    base.LoadViewState(myState[0]);
                if (myState[1] != null)
                    ((System.Web.UI.IStateManager)this.PageNumberStyle).LoadViewState(myState[1]);
                if (myState[2] != null)
                    ((System.Web.UI.IStateManager)this.CurrentPageNumberStyle).LoadViewState(myState[2]);
                if (myState[3] != null)
                    ((System.Web.UI.IStateManager)this.PageButtonStyle).LoadViewState(myState[3]);

                if (myState[4] != null)
                    ((System.Web.UI.IStateManager)this.FirstPageButtonStyle).LoadViewState(myState[4]);
                if (myState[5] != null)
                    ((System.Web.UI.IStateManager)this.PrevGroupPageButtonStyle).LoadViewState(myState[5]);
                if (myState[6] != null)
                    ((System.Web.UI.IStateManager)this.PrevPageButtonStyle).LoadViewState(myState[6]);
                if (myState[7] != null)
                    ((System.Web.UI.IStateManager)this.NextPageButtonStyle).LoadViewState(myState[7]);
                if (myState[8] != null)
                    ((System.Web.UI.IStateManager)this.NextGroupPageButtonStyle).LoadViewState(myState[8]);
                if (myState[9] != null)
                    ((System.Web.UI.IStateManager)this.LastPageButtonStyle).LoadViewState(myState[9]);

                if (myState[10] != null)
                    ((System.Web.UI.IStateManager)this.FirstPageButtonDisabledStyle).LoadViewState(myState[10]);
                if (myState[11] != null)
                    ((System.Web.UI.IStateManager)this.PrevGroupPageButtonDisabledStyle).LoadViewState(myState[11]);
                if (myState[12] != null)
                    ((System.Web.UI.IStateManager)this.PrevPageButtonDisabledStyle).LoadViewState(myState[12]);
                if (myState[13] != null)
                    ((System.Web.UI.IStateManager)this.NextPageButtonDisabledStyle).LoadViewState(myState[13]);
                if (myState[14] != null)
                    ((System.Web.UI.IStateManager)this.NextGroupPageButtonDisabledStyle).LoadViewState(myState[14]);
                if (myState[15] != null)
                    ((System.Web.UI.IStateManager)this.LastPageButtonDisabledStyle).LoadViewState(myState[15]);

                if (myState[16] != null)
                    ((System.Web.UI.IStateManager)this.InputPageNumberButtonStyle).LoadViewState(myState[16]);
                if (myState[17] != null)
                    ((System.Web.UI.IStateManager)this.InputPageNumberButtonDisabledStyle).LoadViewState(myState[17]);
                if (myState[18] != null)
                    ((System.Web.UI.IStateManager)this.InputPageNumberTextBoxStyle).LoadViewState(myState[18]);
                if (myState[19] != null)
                    ((System.Web.UI.IStateManager)this.InputPageNumberTextBoxDisabledStyle).LoadViewState(myState[19]);
                if (myState[20] != null)
                    ((System.Web.UI.IStateManager)this.PageNumberDisabledStyle).LoadViewState(myState[20]);
                if (myState[21] != null)
                    ((System.Web.UI.IStateManager)this.CurrentPageNumberDisabledStyle).LoadViewState(myState[21]);
                //				if (myState[4] != null)
                //				{
                //					((System.Web.UI.IStateManager)this._viewstate).LoadViewState(myState[4]);;
                //
                //					this.NavigatePageSize = (int)this._viewstate["NavigatePageSize"];
                //					this.PageSize = (int)this._viewstate["PageSize"];
                //					this.PageIndex = (int)_viewstate["PageIndex"];
                //					this.RecordsCount = (int)this._viewstate["RecordsCount"];
                //					this.PrevPageText = (System.String)this._viewstate["PrevPageText"];
                //					this.NextPageText = (System.String)this._viewstate["NextPageText"];
                //					this.PrevGroupPageText = (System.String)this._viewstate["PrevGroupPageText"];
                //					this.NextGroupPageText = (System.String)this._viewstate["NextGroupPageText"];
                //					this.FirstPageText = (System.String)this._viewstate["FirstPageText"];
                //					this.LastPageText = (System.String)this._viewstate["LastPageText"];
                //					this.ShowPageNumber = (bool)this._viewstate["ShowPageNumber"];
                //					this.ShowPrevAndNext = (bool)this._viewstate["ShowPrevAndNext"];
                //					this.ShowPrevAndNextGroup = (bool)this._viewstate["ShowPrevAndNextGroup"];
                //					this.ShowFirstAndLast = (bool)this._viewstate["ShowFirstAndLast"];
                //					this.AllowPostBack = (bool)this._viewstate["AllowPostBack"];
                //				}
            }
        }

        /// <summary>
        /// 保存视图状态。
        /// </summary>
        /// <returns></returns>
        protected override object SaveViewState()
        {
            //			if(this._viewstate.IsItemDirty("NavigatePageSize"))
            //				this._viewstate.Remove("NavigatePageSize");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("NavigatePageSize")) )
            //				this._viewstate.Add("NavigatePageSize", this.NavigatePageSize);
            //
            //			if(this._viewstate.IsItemDirty("PageSize"))
            //				this._viewstate.Remove("PageSize");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("PageSize")) )
            //				this._viewstate.Add("PageSize", this.PageSize);
            //
            //			if(this._viewstate.IsItemDirty("PageIndex"))
            //				this._viewstate.Remove("PageIndex");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("PageIndex")) )
            //				this._viewstate.Add("PageIndex", this.PageIndex);
            //
            //			if(this._viewstate.IsItemDirty("RecordsCount"))
            //				this._viewstate.Remove("RecordsCount");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("RecordsCount")) )
            //				this._viewstate.Add("RecordsCount", this.RecordsCount);
            //
            //			if(this._viewstate.IsItemDirty("PrevPageText"))
            //				this._viewstate.Remove("PrevPageText");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("PrevPageText")) )
            //				this._viewstate.Add("PrevPageText", this.PrevPageText);
            //
            //			if(this._viewstate.IsItemDirty("NextPageText"))
            //				this._viewstate.Remove("NextPageText");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("NextPageText")))
            //				this._viewstate.Add("NextPageText", this.NextPageText);
            //
            //			if(this._viewstate.IsItemDirty("PrevGroupPageText"))
            //				this._viewstate.Remove("PrevGroupPageText");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("PrevGroupPageText")) )
            //				this._viewstate.Add("PrevGroupPageText", this.PrevGroupPageText);
            //
            //			if(this._viewstate.IsItemDirty("NextGroupPageText"))
            //				this._viewstate.Remove("NextGroupPageText");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("NextGroupPageText")) )
            //				this._viewstate.Add("NextGroupPageText", this.NextGroupPageText);
            //
            //			if(this._viewstate.IsItemDirty("FirstPageText"))
            //				this._viewstate.Remove("FirstPageText");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("FirstPageText")) )
            //				this._viewstate.Add("FirstPageText", this.FirstPageText);
            //
            //			if(this._viewstate.IsItemDirty("LastPageText"))
            //				this._viewstate.Remove("LastPageText");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("LastPageText")) )
            //				this._viewstate.Add("LastPageText", this.LastPageText);
            //
            //			if(this._viewstate.IsItemDirty("ShowPageNumber"))
            //				this._viewstate.Remove("ShowPageNumber");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("ShowPageNumber")) )
            //				this._viewstate.Add("ShowPageNumber", this.ShowPageNumber);
            //
            //			if(this._viewstate.IsItemDirty("ShowPrevAndNext"))
            //				this._viewstate.Remove("ShowPrevAndNext");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("ShowPrevAndNext")) )
            //				this._viewstate.Add("ShowPrevAndNext", this.ShowPrevAndNext);
            //
            //			if(this._viewstate.IsItemDirty("ShowPrevAndNextGroup"))
            //				this._viewstate.Remove("ShowPrevAndNextGroup");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("ShowPrevAndNextGroup")) )
            //				this._viewstate.Add("ShowPrevAndNextGroup", this.ShowPrevAndNextGroup);
            //
            //			if(this._viewstate.IsItemDirty("ShowFirstAndLast"))
            //				this._viewstate.Remove("ShowFirstAndLast");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("ShowFirstAndLast")) )
            //				this._viewstate.Add("ShowFirstAndLast", this.ShowFirstAndLast);
            //
            //			if(this._viewstate.IsItemDirty("AllowPostBack"))
            //				this._viewstate.Remove("AllowPostBack");
            //			if( (!((System.Collections.IDictionary)this._viewstate).Contains("AllowPostBack")) )
            //				this._viewstate.Add("AllowPostBack", this.AllowPostBack);

            object baseState = base.SaveViewState();

            object[] myState = new object[22];
            myState[0] = baseState;

            myState[1] = (this.PageNumberStyle != null) ? ((System.Web.UI.IStateManager)this.PageNumberStyle).SaveViewState() : null;
            myState[2] = (this.CurrentPageNumberStyle != null) ? ((System.Web.UI.IStateManager)this.CurrentPageNumberStyle).SaveViewState() : null;
            myState[3] = (this.PageButtonStyle != null) ? ((System.Web.UI.IStateManager)this.PageButtonStyle).SaveViewState() : null;

            myState[4] = (this.FirstPageButtonStyle != null) ? ((System.Web.UI.IStateManager)this.FirstPageButtonStyle).SaveViewState() : null;
            myState[5] = (this.PrevGroupPageButtonStyle != null) ? ((System.Web.UI.IStateManager)this.PrevGroupPageButtonStyle).SaveViewState() : null;
            myState[6] = (this.PrevPageButtonStyle != null) ? ((System.Web.UI.IStateManager)this.PrevPageButtonStyle).SaveViewState() : null;
            myState[7] = (this.NextPageButtonStyle != null) ? ((System.Web.UI.IStateManager)this.NextPageButtonStyle).SaveViewState() : null;
            myState[8] = (this.NextGroupPageButtonStyle != null) ? ((System.Web.UI.IStateManager)this.NextGroupPageButtonStyle).SaveViewState() : null;
            myState[9] = (this.LastPageButtonStyle != null) ? ((System.Web.UI.IStateManager)this.LastPageButtonStyle).SaveViewState() : null;

            myState[10] = (this.FirstPageButtonDisabledStyle != null) ? ((System.Web.UI.IStateManager)this.FirstPageButtonDisabledStyle).SaveViewState() : null;
            myState[11] = (this.PrevGroupPageButtonDisabledStyle != null) ? ((System.Web.UI.IStateManager)this.PrevGroupPageButtonDisabledStyle).SaveViewState() : null;
            myState[12] = (this.PrevPageButtonDisabledStyle != null) ? ((System.Web.UI.IStateManager)this.PrevPageButtonDisabledStyle).SaveViewState() : null;
            myState[13] = (this.NextPageButtonDisabledStyle != null) ? ((System.Web.UI.IStateManager)this.NextPageButtonDisabledStyle).SaveViewState() : null;
            myState[14] = (this.NextGroupPageButtonDisabledStyle != null) ? ((System.Web.UI.IStateManager)this.NextGroupPageButtonDisabledStyle).SaveViewState() : null;
            myState[15] = (this.LastPageButtonDisabledStyle != null) ? ((System.Web.UI.IStateManager)this.LastPageButtonDisabledStyle).SaveViewState() : null;

            myState[16] = (this.InputPageNumberButtonStyle != null) ? ((System.Web.UI.IStateManager)this.InputPageNumberButtonStyle).SaveViewState() : null;
            myState[17] = (this.InputPageNumberButtonDisabledStyle != null) ? ((System.Web.UI.IStateManager)this.InputPageNumberButtonDisabledStyle).SaveViewState() : null;
            myState[18] = (this.InputPageNumberTextBoxStyle != null) ? ((System.Web.UI.IStateManager)this.InputPageNumberTextBoxStyle).SaveViewState() : null;
            myState[19] = (this.InputPageNumberTextBoxDisabledStyle != null) ? ((System.Web.UI.IStateManager)this.InputPageNumberTextBoxDisabledStyle).SaveViewState() : null;
            myState[20] = (this.PageNumberDisabledStyle != null) ? ((System.Web.UI.IStateManager)this.PageNumberDisabledStyle).SaveViewState() : null;
            myState[21] = (this.CurrentPageNumberDisabledStyle != null) ? ((System.Web.UI.IStateManager)this.CurrentPageNumberDisabledStyle).SaveViewState() : null;
            //			myState[4] = (this._viewstate != null) ? ((System.Web.UI.IStateManager)this._viewstate).SaveViewState() : null;

            return myState;
        }

        //		protected override void TrackViewState()
        //		{
        //			base.TrackViewState();
        //
        //			if (this.PageNumberStyle != null)
        //				((System.Web.UI.IStateManager)this.PageNumberStyle).TrackViewState();
        //			if (this.CurrentPageNumberStyle != null)
        //				((System.Web.UI.IStateManager)this.CurrentPageNumberStyle).TrackViewState();
        //			if (this.PrevAndNextStyle != null)
        //				((System.Web.UI.IStateManager)this.PrevAndNextStyle).TrackViewState();
        //			if (this.FirstAndLastStyle != null)
        //				((System.Web.UI.IStateManager)this.FirstAndLastStyle).TrackViewState();
        //			if (this.PrevAndNextGroupStyle != null)
        //				((System.Web.UI.IStateManager)this.PrevAndNextGroupStyle).TrackViewState();
        //			if (this._viewstate != null)
        //				((System.Web.UI.IStateManager)this._viewstate).TrackViewState();
        //		}
        //

        /// <summary>
        /// 引发 System.Web.UI.Control.Init 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs 对象。</param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

            if (this.EnableURLPage && this.DesignMode == false)
            {
                if (!string.IsNullOrEmpty(this.Page.Request[this.PageSizeParameter]))
                {
                    int iPageSize = this.PageSize;
                    if (!int.TryParse(this.Page.Request[this.PageSizeParameter], out iPageSize))
                    {
                        throw new System.ArgumentOutOfRangeException(this.PageSizeParameter, this.Page.Request[this.PageSizeParameter], "无效的参数值。");
                    }
                    this.PageSize = iPageSize;
                }
                if (!string.IsNullOrEmpty(this.Page.Request[this.PageIndexParameter]))
                {
                    int value = this.PageIndex;
                    if (!int.TryParse(this.Page.Request[this.PageIndexParameter], out value))
                    {
                        throw new System.ArgumentOutOfRangeException(this.PageIndexParameter, this.Page.Request[this.PageIndexParameter], "无效的参数值。");
                    }
                    if (value < 0)
                    {
                        this._PageIndex = 0;
                    }
                    else
                    {
                        this._PageIndex = value;
                    }
                    this.ViewState["PageIndex"] = this._PageIndex;
                }
            }

        }

        /// <summary>
        /// 引发 System.Web.UI.Control.Load 事件。
        /// </summary>
        /// <param name="e">包含事件数据的 System.EventArgs 对象。</param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            if (this.EnableURLPage)
            {
                //if (!string.IsNullOrEmpty(this.Page.Request[this.PageSizeParameter]))
                //{
                //    this.PageSize = System.Convert.ToInt32(this.Page.Request[this.PageSizeParameter]);
                //}
                if (!string.IsNullOrEmpty(this.Page.Request[this.PageIndexParameter]))
                {
                    //this.PageIndex = System.Convert.ToInt32(this.Page.Request[this.PageIndexParameter]);
                    this.PageIndex = this.PageIndex; //调整 PageIndex 的取值为合理值。
                    if (this._PageSelectedCommand != null)
                    {
                        this._PageSelectedCommand(this, new System.Web.UI.WebControls.CommandEventArgs("PageSelected", this.PageIndex));
                    }
                }
            }

        }

    }
}
