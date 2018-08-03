<%@ Page Language="c#" %>

<%@ Register TagPrefix="cc1" Namespace="Thinksea.WebControls.PageNavigate" Assembly="Thinksea.WebControls.PageNavigate" %>
<!DOCTYPE html>

<script runat="server">

    private void PageNavigate1_PageSelectedCommand(object source, System.Web.UI.WebControls.CommandEventArgs e)
    {
        this.Label3.Text = "您选中了页码：" + (((Thinksea.WebControls.PageNavigate.PageNavigate)source).PageIndex + 1).ToString();

    }

    private void PageNavigate2_PageSelectedCommand(object source, System.Web.UI.WebControls.CommandEventArgs e)
    {
        this.Label4.Text = "您选中了页码：" + (((Thinksea.WebControls.PageNavigate.PageNavigate)source).PageIndex + 1).ToString();

    }

    private void PageNavigate2_PageNumberDataBound(object source, Thinksea.WebControls.PageNavigate.PageNumberEventArgs e)
    {
        //e.PageNumber = e.PageNumber + 1; //如果需要 URL 的 PageIndex 参数与分页导航控件上显示的当前页码匹配（默认设置，PageIndex 参数的值从 0 开始，即期值为分页导航控件上显示的当前页码-1）
        //e.PageNumberControl.NavigateUrl = "Default.aspx?PageSize=" + ((Thinksea.WebControls.PageNavigate.PageNavigate)source).PageSize.ToString() + "&PageIndex=" + e.PageNumber.ToString();

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        int PageIndex = this.PageNavigate2.PageIndex;
        int PageSize = this.PageNavigate2.PageSize;

        #region 对于使用 URL 参数传递分页导航数据的情况一般需要处理此逻辑。
        if (this.Request["PageIndex"] != null && this.Request["PageIndex"] != "")
        {
            PageIndex = int.Parse(this.Request["PageIndex"]);
            //PageIndex = int.Parse(this.Request["PageIndex"]) - 1;
        }
        if (this.Request["PageSize"] != null && this.Request["PageSize"] != "")
        {
            PageSize = int.Parse(this.Request["PageSize"]);
        }
        #endregion

        //这里放置您的业务逻辑代码。可能包括计算总记录数，调整修正分页索引号等。
        int RecordsCount = 501; //总记录数。

        #region 为分页导航控件设置数据。
        this.PageNavigate1.RecordsCount = RecordsCount;
        //this.PageNavigate1.PageIndex = PageIndex;

        this.PageNavigate2.RecordsCount = RecordsCount;
        this.PageNavigate2.PageIndex = PageIndex;
        this.PageNavigate2.PageSize = PageSize;
        #endregion

    }

</script>

<html lang="zh-cn" xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Thinksea PageNavigate 2.0 示例</title>
    <meta charset="utf-8" />
    <link href="css.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table>
            <tr>
                <td style="font-size: large">
                    <strong>Thinksea PageNavigate 2.0 示例</strong></td>
            </tr>
            <tr>
                <td>
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server">示范代码1：使用 Post 方式提交数据（EnableURLPage=False）</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <cc1:PageNavigate ID="PageNavigate1" runat="server" CssClass="Page" OnPageSelectedCommand="PageNavigate1_PageSelectedCommand" FirstPageText="&lt;div&gt;首页&lt;/div&gt;" LastPageText="&lt;div&gt;尾页&lt;/div&gt;" NextGroupPageText="&lt;div&gt;下一组&lt;/div&gt;" NextPageText="&lt;div&gt;下一页&lt;/div&gt;" PrevGroupPageText="&lt;div&gt;上一组&lt;/div&gt;" PrevPageText="&lt;div&gt;上一页&lt;/div&gt;" InputPageNumberButtonText="&lt;div&gt;确定&lt;/div&gt;" PageNumberText="&lt;div&gt;{0}&lt;/div&gt;">
                        <CurrentPageNumberStyle CssClass="CurrentPageNumber"></CurrentPageNumberStyle>
                    </cc1:PageNavigate>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server">示范代码1执行结果在这里显示</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <br />
                    <br />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server">示范代码2：使用 URL 参数方式提交数据（EnableURLPage=True），请注意IE地址栏的变化</asp:Label></td>
            </tr>
            <tr>
                <td>
                    <cc1:PageNavigate ID="PageNavigate2" runat="server" CssClass="Page2" OnPageNumberDataBound="PageNavigate2_PageNumberDataBound" EnableURLPage="True" OnPageSelectedCommand="PageNavigate2_PageSelectedCommand">
                        <CurrentPageNumberStyle CssClass="CurrentPageNumber"></CurrentPageNumberStyle>
                    </cc1:PageNavigate>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server">示范代码2执行结果在这里显示</asp:Label></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    使用帮助：</td>
            </tr>
            <tr>
                <td>
<style type="text/css">
    .treeList {
        border: 1px solid gray;
    }
    .treeList div {
        margin-left:30px;
        line-height:150%;
    }
</style>
                    <div class="treeList">
                        <div style="background-color:#dfdfdf; margin:0px;">控件的子元素样式表继承规则：（*同级之间无继承关系）</div>
                        <div>导航页码的样式（PageNumberStyle）
                            <div>导航页码被禁用后的样式（PageNumberDisabledStyle）</div>
                            <div>当前页导航页码的样式（CurrentPageNumberStyle）
                                <div>导航页码被禁用后的样式（PageNumberDisabledStyle）
                                    <div>当前页导航页码被禁用后的样式（CurrentPageNumberDisabledStyle）</div>
                                </div>
                            </div>
                        </div>
                        <div>页导航按钮的样式（PageButtonStyle）
                            <div>首页导航按钮的样式(FirstPageButtonStyle)
                                <div>首页导航按钮被禁用后的样式（FirstPageButtonDisabledStyle）</div>
                            </div>
                            <div>上一组分页导航按钮的样式（PrevGroupPageButtonStyle）
                                <div>上一组分页导航按钮被禁用后的样式（PrevGroupPageButtonDisabledStyle）</div>
                            </div>
                            <div>上一页导航按钮的样式（PrevPageButtonStyle）
                                <div>上一页导航按钮被禁用后的样式（PrevPageButtonDisabledStyle）</div>
                            </div>
                            <div>下一页导航按钮的样式（NextPageButtonStyle）
                                <div>下一页导航按钮被禁用后的样式（NextPageButtonDisabledStyle）</div>
                            </div>
                            <div>下一组分页导航按钮的样式（NextGroupPageButtonStyle）
                                <div>下一组分页导航按钮被禁用后的样式（NextGroupPageButtonDisabledStyle）</div>
                            </div>
                            <div>尾页导航按钮的样式（LastPageButtonStyle）
                                <div>尾页导航按钮被禁用后的样式（LastPageButtonDisabledStyle）</div>
                            </div>
                            <div>跳转到输入的页码按钮的样式（InputPageNumberButtonStyle）
                                <div>跳转到输入的页码按钮被禁用后的样式（InputPageNumberButtonDisabledStyle）</div>
                            </div>
                        </div>
                        <div>页码输入控件的样式（InputPageNumberTextBoxStyle）
                            <div>页码输入控件被禁用后的样式（InputPageNumberTextBoxDisabledStyle）</div>
                        </div>
                        <div style="background-color:#dfdfdf; margin:0px;">
                            <span style="color:darkblue;">例如：CurrentPageNumberDisabledStyle 的继承顺序是：<br />
                            PageNumberDisabledStyle->CurrentPageNumberStyle->PageNumberDisabledStyle->CurrentPageNumberDisabledStyle</span>
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
