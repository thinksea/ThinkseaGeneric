<%@ Page language="c#" Codebehind="WebForm1.aspx.cs" AutoEventWireup="false" Inherits="sample.WebForm1" %>
<%@ Register TagPrefix="cc1" Namespace="Thinksea.WebControls.Menu2" Assembly="Thinksea.WebControls.Menu2" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="style/main.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<cc1:menu2 id="Menu21" runat="server" Height="50px" CssClass="OuterHead" MenuXML="MainMenu.config"
				MaxColumn="2" style="Z-INDEX: 102; LEFT: 32px; POSITION: absolute; TOP: 24px">
				<MenuTitleStyle CssClass="MenuTitleBGShow"></MenuTitleStyle>
				<MenuItemSeparatorStyle CssClass="MenuItemSeparator"></MenuItemSeparatorStyle>
				<MenuTitleOnMouseOverStyle CssClass="MenuTitleMouseIn"></MenuTitleOnMouseOverStyle>
				<MenuTitleOnMouseOutStyle CssClass="MenuTitleMouseOut"></MenuTitleOnMouseOutStyle>
				<MenuTitleCollapseStyle CssClass="MenuTitleBGHide"></MenuTitleCollapseStyle>
				<MenuGroupSeparatorStyle CssClass="MenuGroupSeparator"></MenuGroupSeparatorStyle>
				<MenuItemStyle CssClass="MenuItem"></MenuItemStyle>
				<MenuGroupStyle CssClass="MenuPand"></MenuGroupStyle>
			</cc1:menu2>
			<asp:Label id="Label1" style="Z-INDEX: 101; LEFT: 128px; POSITION: absolute; TOP: 272px" runat="server">您选中的菜单项编号在这里显示</asp:Label>
		</form>
	</body>
</HTML>
