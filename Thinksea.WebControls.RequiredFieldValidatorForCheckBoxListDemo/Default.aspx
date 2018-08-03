<%@ Page Language="C#"%>

<%@ Register assembly="Thinksea.WebControls.RequiredFieldValidatorForCheckBoxList" namespace="Thinksea.WebControls" tagprefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<script runat="server">

    protected void Button1_Click(object sender, EventArgs e)
    {
        this.Response.Write("是否通过用户输入验证：" + this.IsValid.ToString() + "<br/>选择的项目如下：");
        foreach (ListItem tmp in this.CheckBoxList1.Items)
        {
            if (tmp.Selected)
            {
                this.Response.Write("<br/>" + tmp.Text);
            }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatColumns="3">
        <asp:ListItem>项目1</asp:ListItem>
        <asp:ListItem>项目2</asp:ListItem>
        <asp:ListItem>项目3</asp:ListItem>
        <asp:ListItem>项目4</asp:ListItem>
        <asp:ListItem>项目5</asp:ListItem>
        <asp:ListItem>项目6</asp:ListItem>
    </asp:CheckBoxList>
    <cc2:RequiredFieldValidatorForCheckBoxList ID="RequiredFieldValidatorForCheckBoxList1" 
        runat="server" ControlToValidate="CheckBoxList1" 
        ErrorMessage="至少需要选择一项" SetFocusOnError="True"></cc2:RequiredFieldValidatorForCheckBoxList>
    <br />
    <asp:Button ID="Button1" runat="server" Text=" 提 交 " onclick="Button1_Click" />
    </form>
</body>
</html>
