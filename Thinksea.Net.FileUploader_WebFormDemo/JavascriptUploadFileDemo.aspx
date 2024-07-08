<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JavascriptUploadFileDemo.aspx.cs" Inherits="Thinksea.Net.FileUploader_WebFormDemo.JavascriptUploadFileDemo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <title>JavaScript 上传大文件示例 - Thinksea.Net.FileUploader_WebFormDemo</title>
    <script type="text/javascript" charset="utf-8" src="lib/jsext-1.7.1/jsext.min.js"></script>
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/crypto-js/4.1.1/crypto-js.min.js"></script>
    <script type="text/javascript" charset="utf-8" src="Thinksea.Net.FileUploader.js"></script>
    <script type="text/javascript" charset="utf-8" src="JavascriptUploadFileDemo.js"></script>
</head>
<body>
    <form id="form1" runat="server">

        <div id="ctl5" class="form-group">
            <label>批量上传文件</label>
            <button type="button" class="btn btn-default" data-clickupload="true" data-dragoverupload="true">拖入可上传</button>
            <div data-dragoverupload="true">
                <div class="clearfix fileinsertmark"></div>
            </div>
            <input type="file" multiple="multiple" class="fileUpload" style="visibility: hidden; width: 0px; height: 0px;" />
            <input type="hidden" name="fileUrls" />
        </div>

    </form>
    <script type="text/javascript">
        JavascriptUploadFileDemo.init();
    </script>
</body>
</html>
