<%@ Page Language="C#" %>

<!DOCTYPE html>
<script runat="server">
    protected void Page_Load(object sender, EventArgs e)
    {
        string VerifyCode1 = this.Request["VerifyCode1"];
        string VerifyCode2 = this.Request["VerifyCode2"];
        if (!string.IsNullOrWhiteSpace(VerifyCode1) || !string.IsNullOrWhiteSpace(VerifyCode2))
        {
            this.Response.Write(Thinksea.VerifyCode_WebFormDemo.VerifyCode.GetVerifyCode("VerifyCode1") + "<br/>");
            this.Response.Write(Thinksea.VerifyCode_WebFormDemo.VerifyCode.GetVerifyCode("VerifyCode2") + "<br/>");
            if (Thinksea.VerifyCode_WebFormDemo.VerifyCode.IsVerify(VerifyCode1, "VerifyCode1"))
            {
                this.Response.Write("【验证码1输入正确】");
            }
            else
            {
                this.Response.Write("【验证码1输入错误】");
            }
            if (Thinksea.VerifyCode_WebFormDemo.VerifyCode.IsVerify(VerifyCode2, "VerifyCode2"))
            {
                this.Response.Write("【验证码2输入正确】");
            }
            else
            {
                this.Response.Write("【验证码2输入错误】");
            }
        }
    }
</script>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script type="text/javascript">
        /**
         * 更换验证码。
         * @param imageID
         */
        function changeVerifyCode(imageID) {
            var Name = "rt";
            var Value = Math.random()
            var uri = "/VerifyCode.ashx?VerifyCodeID=" + imageID + "&rt=" + Value;
            document.getElementById(imageID).src = uri;
        }

        /**
         * 用户点击验证码控件时引发此事件。
         * @param e
         */
        function verifyCodeClick(e) {
            var cId = this.children[0].id;
            changeVerifyCode(cId);
        }

        function load() {
            {
                var ctlId = "VerifyCode1";
                changeVerifyCode(ctlId);
                var ctl = document.getElementById(ctlId);
                ctl.parentElement.onclick = verifyCodeClick;
            }
            {
                var ctlId = "VerifyCode2";
                changeVerifyCode(ctlId);
                var ctl = document.getElementById(ctlId);
                ctl.parentElement.onclick = verifyCodeClick;
            }
            {
                var ctlId = "VerifyCode3";
                changeVerifyCode(ctlId);
            }
            {
                var ctlId = "VerifyCode4";
                changeVerifyCode(ctlId);
            }
            {
                var ctlId = "VerifyCode5";
                changeVerifyCode(ctlId);
            }
            {
                var ctlId = "VerifyCode6";
                changeVerifyCode(ctlId);
            }
            {
                var ctlId = "VerifyCode7";
                changeVerifyCode(ctlId);
            }
            {
                var ctlId = "VerifyCode8";
                changeVerifyCode(ctlId);
            }
            {
                var ctlId = "VerifyCode9";
                changeVerifyCode(ctlId);
            }
        }

        function btnClick() {
            document.location.href = "/?VerifyCode1=" + TextBox1.value + "&VerifyCode2=" + TextBox2.value;
        }
    </script>
</head>
<body onload="load()">
    <form id="form1" runat="server">
        <a href="javascript:;"><img id="VerifyCode1" /></a>
	    <input id="TextBox1" type="text" />
        <p>
            <a href="javascript:;"><img id="VerifyCode2" /></a>
	        <input id="TextBox2" type="text" />
            <button type="button" onclick="btnClick()">验证</button>
        </p>
        <p>
            <img id="VerifyCode3" />
            <img id="VerifyCode4" />
            <img id="VerifyCode5" />
            <img id="VerifyCode6" />
            <img id="VerifyCode7" />
            <img id="VerifyCode8" />
            <img id="VerifyCode9" />
        </p>
    </form>
</body>
</html>
