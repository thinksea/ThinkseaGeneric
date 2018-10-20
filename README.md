# ThinkseaGeneric
使用 C# 编写的 .NET 公共资源库，封装了程序开发过程中常用的基础功能。

说明：因为解决方案中包含了过多的项目，为了方便描述，所以主要使用提交时间作为解决方案整体版本命名。

特别声明：部分内容来源于互联网，由于时间较为久远已不知原作者归属，如侵犯了您的权益请发送邮件至“thinksea@163.com”，感谢原作者做出的贡献。

=======
###### version：2018-10-20

重要修改：
+ 新增事件 HttpFileUpload.BeforeUpload，当计算文件校验码完成之后开始上传文件之前引发此事件。

###### version：2018-8-17

重要修改：
+ 修复BUG：当上传文件校验失败后，无法重新上传。在这个版本中删除校验失败的临时存盘文件。

###### version：2018-8-13

新增方法：
+ public static string Thinksea.General.Bytes2HexString(byte[] bytes) //byte 数组转 16 进制字符串。
+ public static byte[] Thinksea.General.HexString2Bytes(string hexStr) //将 16 进制字符串转为 byte 数组。

重要修改：
+ Thinksea.Net.FileUploader 文件上传校验码参数改为直接传递 16 进制字符串，而非  base64 编码格式字符串。

###### version：2018-8-3

+ Thinksea　　封装了编程过程中常用的基本功能。
+ Thinksea.IIS　　封装了对 IIS 的操作方法
+ Thinksea.Net.FileUploader　　封装了基于 HTTP 协议的文件上传功能
+ Thinksea.SQL　　封装了对 SQL 的操作方法
+ Thinksea.VisualStudio　　封装了对 VisualStudio 的访问接口。
+ Thinksea.WebControls.DateTimePicker　　一个日期选择控件
+ Thinksea.WebControls.HtmlRotator　　一个用于轮换显示HTML内容的控件
+ Thinksea.WebControls.Menu2　　一个两级菜单
+ Thinksea.WebControls.PageNavigate　　一个分页导航控件
+ Thinksea.WebControls.RequiredFieldValidatorForCheckBoxList　　对复选框列表控件“CheckBoxList”执行至少选中一项的验证。
+ Thinksea.WebControls.VerifyCode　　一个网页验证码控件
+ Thinksea.Windows　　封装了对 Windows 系统的操作方法
+ Thinksea.Windows.Forms　　封装了常用的 Windows Form 控件。
+ Thinksea.Windows.Forms.IPAddress　　IP 地址输入控件
+ Thinksea.Windows.Forms.MdiTabControl　一个选项卡控件。
