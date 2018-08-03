<%@ Page language="c#" %>
<%@ Register TagPrefix="cc1" Namespace="Thinksea.WebControls.VerifyCode" Assembly="Thinksea.WebControls.VerifyCode" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        //Thinksea.WebControls.VerifyCode.VerifyCodeHandler.VerifyCodeEnumerable = new string[0];
        //Thinksea.WebControls.VerifyCode.VerifyCodeHandler.KeyValueVerifyCodeEnumerable.Clear();
        //Thinksea.WebControls.VerifyCode.VerifyCodeHandler.KeyValueVerifyCodeEnumerable.Add("你好", "丽丽");
    }

    protected void Button1_Click2(object sender, EventArgs e)
    {
		this.Response.Write(this.VerifyCode1.ID + ":" + this.VerifyCode1.GetVerifyCode() + "<br/>");
		this.Response.Write(this.VerifyCode2.ID + ":" + this.VerifyCode2.GetVerifyCode() + "<br/>");
		if (this.VerifyCode1.IsVerify(this.TextBox1.Text))
        {
            this.Response.Write("输入正确");
        }
        else
        {
            this.Response.Write("输入错误");
        }

    }

    protected void Button2_Click(object sender, EventArgs e)
    {
		this.Response.Write(this.VerifyCode1.ID + ":" + this.VerifyCode1.GetVerifyCode() + "<br/>");
		this.Response.Write(this.VerifyCode2.ID + ":" + this.VerifyCode2.GetVerifyCode() + "<br/>");
		if (this.VerifyCode2.IsVerify(this.TextBox2.Text))
        {
            this.Response.Write("输入正确");
        }
        else
        {
            this.Response.Write("输入错误");
        }

    }

</script>

<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<cc1:VerifyCode id="VerifyCode1" runat="server"></cc1:VerifyCode>
		    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" onclick="Button1_Click2" 
                Text="Button" />
            <p>
                <cc1:VerifyCode ID="VerifyCode2" runat="server">
                </cc1:VerifyCode>
                <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                <asp:Button ID="Button2" runat="server" onclick="Button2_Click" Text="Button" />
            </p>
            <p>
    			<cc1:VerifyCode id="VerifyCode8" runat="server"></cc1:VerifyCode>
                <cc1:VerifyCode ID="VerifyCode9" runat="server" ></cc1:VerifyCode>
                <cc1:VerifyCode ID="VerifyCode4" runat="server" />
                <cc1:VerifyCode ID="VerifyCode5" runat="server" />
                <cc1:VerifyCode ID="VerifyCode6" runat="server" />
                <cc1:VerifyCode ID="VerifyCode7" runat="server" />
                <cc1:VerifyCode ID="VerifyCode3" runat="server" />
            </p>
		</form>
	</body>
</HTML>
