<%@ Page Language="C#" %>
<%@ Register TagPrefix="cc1" Namespace="Thinksea.WebControls.DateTimePicker" Assembly="Thinksea.WebControls.DateTimePicker" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Strict//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-strict.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>无标题页</title>
    <script runat="server">
        private void DateTimePicker1_DateTimeChanged(object sender, System.EventArgs e)
        {
            DateTimePicker dtp = (DateTimePicker)sender;
            this.Response.Write("【" + dtp.ID + "】时间已经更改：" + dtp.DateTime.ToString());
        }

        private void DateTimePicker1_CheckError(object sender, Thinksea.WebControls.DateTimePicker.CheckErrorEventArgs ce)
        {
            this.Response.Write("输入日期格式错误！");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.IsPostBack)
            {
            }
        }
</script>
<style type="text/css">
</style>
<script type="text/javascript">

</script>
</head>
<body>
    <form id="form1" runat="server">
        <cc1:DateTimePicker id="DateTimePicker1" runat="server" OnDateTimeChanged="DateTimePicker1_DateTimeChanged" OnCheckError="DateTimePicker1_CheckError" UseIncludeResource="true" >
        </cc1:DateTimePicker>
        <cc1:DateTimePicker id="DateTimePicker2" runat="server" 
            OnDateTimeChanged="DateTimePicker1_DateTimeChanged" 
            OnCheckError="DateTimePicker1_CheckError" UseIncludeResource="True" 
            ReadOnly="True" >
        </cc1:DateTimePicker>
        <cc1:DateTimePicker id="DateTimePicker3" runat="server" OnDateTimeChanged="DateTimePicker1_DateTimeChanged" OnCheckError="DateTimePicker1_CheckError" UseIncludeResource="true" >
        </cc1:DateTimePicker>
        <cc1:DateTimePicker id="DateTimePicker4" runat="server" OnDateTimeChanged="DateTimePicker1_DateTimeChanged" OnCheckError="DateTimePicker1_CheckError" UseIncludeResource="true" Enabled="false" ShowDataSelector="true" >
        </cc1:DateTimePicker>
        <asp:Button ID="Button1" runat="server" Text="提交"></asp:Button>
    </form>
</body>
</html>
