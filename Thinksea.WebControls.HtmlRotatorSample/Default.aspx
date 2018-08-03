<%@ Page Language="C#" AutoEventWireup="true" %>
<%@ Register TagPrefix="cc1" Namespace="Thinksea.WebControls.HtmlRotator" Assembly="Thinksea.WebControls.HtmlRotator" %>
<script runat="server">
private void Page_Load(object sender, System.EventArgs e)
{
	// 在此处放置用户代码以初始化页面
    this.HtmlRotator2.AddHtmlRange(
        new string[]{
							"<a href='http://www.sohu.com'><img src='adv/1.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='http://www.163.com'><img src='adv/2.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='http://www.163.com'><img src='adv/3.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='http://www.163.com'><img src='adv/4.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><a href='http://www.163.com'><img src='adv/5.jpg' width='218' height='128' border=0 style='display: block;'/></a></td></tr><tr><td align=center><a href='http://www.163.com'>eeeeeeeeeee</a></td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><a href='http://www.163.com'><img src='adv/6.jpg' width='218' height='128' border=0 style='display: block;'/></a></td></tr><tr><td align=center><a href='http://www.163.com'>fffffffffff</a></td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><a href='http://www.163.com'><img src='adv/7.jpg' width='218' height='128' border=0 style='display: block;'/></a></td></tr><tr><td align=center><a href='http://www.163.com'>ggggggggggg</a></td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><a href='http://www.163.com'><img src='adv/8.jpg' width='218' height='128' border=0 style='display: block;'/></a></td></tr><tr><td align=center><a href='http://www.163.com'>hhhhhhhhhhh</a></td></tr></table>"
						}
        );
    this.Htmlrotator3.AddHtmlRange(
        new string[]{
							"<a href='#'><img src='adv/1.jpg' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/2.jpg' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/3.jpg' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/4.jpg' height='128' border=0 style='display: block;'/></a>"
							, "<table height='128' cellspacing='0' cellpadding='0' border='0' style='display: block;'><tr><td height='100'><img src='adv/5.jpg' height='128'/></td></tr><tr><td align=center>eeeeeeeeeee</td></tr></table>"
							, "<table height='128' cellspacing='0' cellpadding='0' border='0' style='display: block;'><tr><td height='100'><img src='adv/6.jpg' height='128'/></td></tr><tr><td align=center>fffffffffff</td></tr></table>"
							, "<table height='128' cellspacing='0' cellpadding='0' border='0' style='display: block;'><tr><td height='100'><img src='adv/7.jpg' height='128'/></td></tr><tr><td align=center>ggggggggggg</td></tr></table>"
							, "<table height='128' cellspacing='0' cellpadding='0' border='0' style='display: block;'><tr><td height='100'><img src='adv/8.jpg' height='128'/></td></tr><tr><td align=center>hhhhhhhhhhh</td></tr></table>"
						}
        );
    this.Htmlrotator4.AddHtmlRange(
        new string[]{
							"<a href='#'><img src='adv/1.jpg' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/2.jpg' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/3.jpg' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/4.jpg' height='128' border=0 style='display: block;'/></a>"
							, "<table height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/5.jpg' height='128' style='display: block;'/></td></tr><tr><td align=center>eeeeeeeeeee</td></tr></table>"
							, "<table height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/6.jpg' height='128' style='display: block;'/></td></tr><tr><td align=center>fffffffffff</td></tr></table>"
							, "<table height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/7.jpg' height='128' style='display: block;'/></td></tr><tr><td align=center>ggggggggggg</td></tr></table>"
							, "<table height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/8.jpg' height='128' style='display: block;'/></td></tr><tr><td align=center>hhhhhhhhhhh</td></tr></table>"
						}
        );

    this.Htmlrotator5.AddHtmlRange(
        new string[]{
							"<a href='#'><img src='adv/1.jpg' width='218' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/2.jpg' width='218' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/3.jpg' width='218' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/4.jpg' width='218' border=0 style='display: block;'/></a>"
							, "<table width='218' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/5.jpg' width='218' style='display: block;'/></td></tr><tr><td align=center>eeeeeeeeeee</td></tr></table>"
							, "<table width='218' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/6.jpg' width='218' style='display: block;'/></td></tr><tr><td align=center>fffffffffff</td></tr></table>"
							, "<table width='218' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/7.jpg' width='218' style='display: block;'/></td></tr><tr><td align=center>ggggggggggg</td></tr></table>"
							, "<table width='218' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/8.jpg' width='218' style='display: block;'/></td></tr><tr><td align=center>hhhhhhhhhhh</td></tr></table>"
						}
        );
    this.Htmlrotator6.AddHtmlRange(
        new string[]{
							"<a href='#'><img src='adv/1.jpg' width='218' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/2.jpg' width='218' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/3.jpg' width='218' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/4.jpg' width='218' border=0 style='display: block;'/></a>"
							, "<table width='218' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/5.jpg' width='218' style='display: block;'/></td></tr><tr><td align=center>eeeeeeeeeee</td></tr></table>"
							, "<table width='218' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/6.jpg' width='218' style='display: block;'/></td></tr><tr><td align=center>fffffffffff</td></tr></table>"
							, "<table width='218' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/7.jpg' width='218' style='display: block;'/></td></tr><tr><td align=center>ggggggggggg</td></tr></table>"
							, "<table width='218' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/8.jpg' width='218' style='display: block;'/></td></tr><tr><td align=center>hhhhhhhhhhh</td></tr></table>"
						}
        );

    this.Htmlrotator7.AddHtmlRange(
        new string[]{
							"<a href='#'><img src='adv/1.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/2.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/3.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/4.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/5.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>eeeeeeeeeee</td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/6.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>fffffffffff</td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/7.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>ggggggggggg</td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/8.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>hhhhhhhhhhh</td></tr></table>"
						}
        );
    this.Htmlrotator8.AddHtmlRange(
        new string[]{
							"<a href='#'><img src='adv/1.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/2.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/3.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/4.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/5.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>eeeeeeeeeee</td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/6.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>fffffffffff</td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/7.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>ggggggggggg</td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/8.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>hhhhhhhhhhh</td></tr></table>"
						}
        );

    this.Htmlrotator9.AddHtmlRange(
        new string[]{
							"<a href='#'><img src='adv/1.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/2.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/3.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/4.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/5.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>eeeeeeeeeee</td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/6.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>fffffffffff</td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/7.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>ggggggggggg</td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/8.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>hhhhhhhhhhh</td></tr></table>"
						}
        );
    this.Htmlrotator10.AddHtmlRange(
        new string[]{
							"<a href='#'><img src='adv/1.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/2.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/3.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/4.jpg' width='218' height='128' border=0 style='display: block;'/></a>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/5.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>eeeeeeeeeee</td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/6.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>fffffffffff</td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/7.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>ggggggggggg</td></tr></table>"
							, "<table width='218' height='128' cellspacing='0' cellpadding='0' border='0'><tr><td height='100'><img src='adv/8.jpg' width='218' height='100' style='display: block;'/></td></tr><tr><td align=center>hhhhhhhhhhh</td></tr></table>"
						}
        );
    this.Htmlrotator11.AddHtmlRange(
        new string[]{
							"<a href='#'><img src='adv/1.jpg' width='216' height='50' border=1 style='display: block;'/></a>"
							, "<a href='#'><img src='adv/2.jpg' width='216' height='50' border=1 style='display: block;'/></a>"
						}
        );

}

</script>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<!--
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
-->
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>无标题页</title>
<script type="text/javascript">

</script>
</head>
<body>
    <form id="form1" runat="server">
		<cc1:htmlrotator id="HtmlRotator2" style="Z-INDEX: 100; LEFT: 40px; POSITION: absolute; TOP: 40px"
			runat="server" StopOnMouseOver="True" PlaySpeed="5000" Height="128px" Width="218px"></cc1:htmlrotator>
		<cc1:htmlrotator id="Htmlrotator3" style="Z-INDEX: 101; LEFT: 40px; POSITION: absolute; TOP: 216px"
			runat="server" StopOnMouseOver="True" PlaySpeed="30" Height="128px" Width="218px"
			HtmlRotatorType="RightToLeft"></cc1:htmlrotator>
		<cc1:htmlrotator id="Htmlrotator4" style="Z-INDEX: 102; LEFT: 274px; POSITION: absolute; TOP: 216px"
			runat="server" StopOnMouseOver="True" PlaySpeed="30" Height="128px" Width="218px"
			HtmlRotatorType="LeftToRight"></cc1:htmlrotator>
		<cc1:htmlrotator id="Htmlrotator5" style="Z-INDEX: 104; LEFT: 504px; POSITION: absolute; TOP: 216px"
			runat="server" StopOnMouseOver="True" PlaySpeed="30" Height="128px" Width="218px"
			HtmlRotatorType="TopToBottom"></cc1:htmlrotator>
		<cc1:htmlrotator id="Htmlrotator6" style="Z-INDEX: 105; LEFT: 734px; POSITION: absolute; TOP: 216px"
			runat="server" StopOnMouseOver="True" PlaySpeed="30" Height="128px" Width="218px"
			HtmlRotatorType="BottomToTop"></cc1:htmlrotator>
		<cc1:htmlrotator id="Htmlrotator7" style="Z-INDEX: 101; LEFT: 40px; POSITION: absolute; TOP: 406px"
			runat="server" StopOnMouseOver="True" PlaySpeed="30" Height="128px" Width="218px"
			HtmlRotatorType="RightToLeft" PauseTime="3000"></cc1:htmlrotator>
		<cc1:htmlrotator id="Htmlrotator8" style="Z-INDEX: 102; LEFT: 274px; POSITION: absolute; TOP: 406px"
			runat="server" StopOnMouseOver="True" PlaySpeed="30" Height="128px" Width="218px"
			HtmlRotatorType="LeftToRight" PauseTime="3000"></cc1:htmlrotator>
		<cc1:htmlrotator id="Htmlrotator9" style="Z-INDEX: 104; LEFT: 504px; POSITION: absolute; TOP: 406px"
			runat="server" StopOnMouseOver="True" PlaySpeed="30" Height="128px" Width="218px"
			HtmlRotatorType="TopToBottom" PauseTime="3000"></cc1:htmlrotator>
		<cc1:htmlrotator id="Htmlrotator10" style="Z-INDEX: 105; LEFT: 734px; POSITION: absolute; TOP: 406px"
			runat="server" StopOnMouseOver="True" PlaySpeed="30" Height="128px" Width="218px"
			HtmlRotatorType="BottomToTop" PauseTime="3000"></cc1:htmlrotator>
		<cc1:htmlrotator id="Htmlrotator11" style="Z-INDEX: 105; LEFT: 274px; POSITION: absolute; TOP: 40px"
			runat="server" StopOnMouseOver="True" PlaySpeed="30" Height="128px" Width="218px"
			HtmlRotatorType="BottomToTop"></cc1:htmlrotator>
    </form>
</body>
</html>
