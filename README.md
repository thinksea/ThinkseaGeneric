# ThinkseaGeneric
使用 C# 编写的 .NET 公共资源库，封装了程序开发过程中常用的基础功能。

说明：因为解决方案中包含了过多的项目，为了方便描述，所以主要使用提交时间作为解决方案整体版本命名。

特别声明：部分内容来源于互联网，由于时间较为久远已不知原作者归属，如侵犯了您的权益请发送邮件至“thinksea@163.com”，感谢原作者做出的贡献。

=======
###### version：2022-11-22

+ 更新项目中引用的基础组件。
+ 不再支持较旧的“.NET Core 3.1”，“NET 4.6.1”和“NET 5.0”。

###### version：2021-12-19

+ 更新项目中引用的基础组件，部分项目支持编译版本 .NET 6.0。
+ 【注意】更新组件“Thinksea.Net.FileUploader”（版本5.0）支持组件“crypto-js-4.1.1”或更高版本。由于排除了对“Newtonsoft.Json”组件的依赖，这【可能会】导致不兼容。
+ 排除了对“JQuery”组件的依赖。
+ 排除了对“Newtonsoft.Json”组件的依赖，改为使用微软的“System.Text.Json”组件（高版本 .NET 已内置此功能，否则可以通过 Nuget 添加此组件的依赖项）。

###### version：2021-08-17

+ 更新项目中引用的基础组件，部分项目支持编译版本 .NET 5.0。

###### version：2021-01-19

+ 【注意】更新组件“Thinksea.Net.FileUploader”（版本4.1.0）依赖于组件 crypto-js-4.0.0。

###### version：2020-11-13

+ 更新项目中引用的基础组件，受基础组件影响，不再支持 .NET Framework 4.0，不再支持 .NET Core 3.0，同时支持编译版本 .NET Framework 4.6.1，4.8，部分项目支持编译版本 .NET Core 3.1，.NET 5.0。

###### version：2020-02-07

+ 已将组件“Thinksea.Logs”从项目“Thinksea”中剥离。仅用于兼容旧项目的目的，如无必要将不再进行升级维护。推荐使用.NET项目自带的日志组件或 NLog 等同类产品替代。
+ 其他的代码优化。

###### version：2020-01-26

+ 修复组件“Thinksea.Net.FileUploader”的BUG，增加和完善 Demo。

###### version：2020-01-24

+ 修复组件“Thinksea.Net.FileUploader”的BUG，增加和完善 Demo。（注意：此组件版本已升级到 4.0.0.0，部分接口有变化）

###### version：2019-12-08

+ 迁徙解决方案中的全部项目，同时支持编译版本 .NET Framework 4.0，4.8，部分项目支持编译版本 .NET Core 3.0，3.1。

###### version：2019-07-23

+ 迁移项目“Thinksea”，同时支持编译版本 .Net Framework 和 .Net Core
+ 修改方法“IsMobileOrPad()”对设备的识别方式。

###### version：2019-03-15

移除方法 Thinksea.Image.GetSvgImageSize。因算法存在缺陷，并且有更好的替代方法 Svg.SvgDocument.GetDimensions()，详情参考：
+ https://archive.codeplex.com/?p=svg
+ https://github.com/vvvv/SVG

###### version：2019-03-13

+ 优化缩略图计算策略。
+ 修复 Thinksea.Image.Size Thinksea.Image.GetSvgImageSize 方法返回结果无法保留小数的 BUG

###### version：2019-03-12

+ 优化图片处理方法。
+ 完善 SVG 矢量图处理方法并排除了一些 BUG。

###### version：2019-03-10

新增方法：
+ Thinksea.Image.Size Thinksea.Image.GetSvgImageSize(string fileName); //获取 SVG 格式图片尺寸

###### version：2019-01-26

新增功能 Web 输出图片质量：
+ Thinksea.eImageQuality.Web //Web 质量。

###### version：2018-12-10

新增方法：
+ public static bool Thinksea.Web.IsMobile() //判断用户端访问设备是否手机。
+ public static bool Thinksea.Web.isMobileOrPad() //判断用户端访问设备是否手机或平板。
+ public static bool Thinksea.Web.IsWeixinBrowser() //判断是否在微信浏览器内访问网页。

###### version：2018-10-22

重要修改：
+ 重构组件 Thinksea.Net.FileUploader。
+ 新增功能：组件 Thinksea.Net.FileUploader 支持秒传。

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
