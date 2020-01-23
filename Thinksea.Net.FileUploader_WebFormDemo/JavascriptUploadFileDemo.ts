namespace JavascriptUploadFileDemo {
    /**
     * 文件上传返回结果。
     */
    export interface FileUploadResult {
        /**
         * 文件存储路径。
         */
        readonly SavePath: string;
        /**
         * 文件大小（单位：字节）
         */
        readonly FileLength: GLint64;
        /**
         * 指示是否秒传。
         */
        readonly IsFastUpload: boolean;
    };

    /**
     * 将文件大小转换为以合适的单位（“EB”、“PB”、“TB”、“GB”、“MB”、“KB”、“B”）表示形式的文本。
     * @param size 以字节“B”为单位的文件大小。
     * @param format 格式化字符串。
     * @returns 表示文件大小的带有单位的字符串。
     * 
     * 下面的代码演示了如何使用这个方法：
     * @example
     * convertToFileSize(11);
     * convertToFileSize(12989);
     * convertToFileSize(1726752);
     * convertToFileSize(1526725236);
     * convertToFileSize(95393296753236);
     * 
     * 输出结果：
     * 11 B
     * 12.68 KB
     * 1.65 MB
     * 1.42 GB
     * 86.76 TB
     */
    function convertToFileSize(size: GLint, format: string = "0.##"): string {
        if (size >= convertToFileSize.FileLengthEB) {
            return (size * 1.0 / convertToFileSize.FileLengthEB).format(format) + " EB";
        }
        else if (size >= convertToFileSize.FileLengthPB) {
            return (size * 1.0 / convertToFileSize.FileLengthPB).format(format) + " PB";
        }
        else if (size >= convertToFileSize.FileLengthTB) {
            return (size * 1.0 / convertToFileSize.FileLengthTB).format(format) + " TB";
        }
        else if (size >= convertToFileSize.FileLengthGB) {
            return (size * 1.0 / convertToFileSize.FileLengthGB).format(format) + " GB";
        }
        else if (size >= convertToFileSize.FileLengthMB) {
            return (size * 1.0 / convertToFileSize.FileLengthMB).format(format) + " MB";
        }
        else if (size >= convertToFileSize.FileLengthKB) {
            return (size * 1.0 / convertToFileSize.FileLengthKB).format(format) + " KB";
        }
        else {
            return size + " B";
        }
    }

    /**
     * 将带文件计算机单位（“EB”、“PB”、“TB”、“GB”、“MB”、“KB”、“B”）的文件尺寸描述形式转换为以 B 为单位的基础值。
     * @param size 表示文件大小的带有单位的字符串。
     * @returns 以字节“B”为单位的文件大小。
     */
    function convertFileSizeToByte(size: string): GLint {
        if (size.endsWith("EB")) {
            return parseFloat(size.substr(0, size.length - 2).trimEnd(null).replace(",", "")) * convertToFileSize.FileLengthEB;
        }
        else if (size.endsWith("PB")) {
            return parseFloat(size.substr(0, size.length - 2).trimEnd(null).replace(",", "")) * convertToFileSize.FileLengthPB;
        }
        else if (size.endsWith("TB")) {
            return parseFloat(size.substr(0, size.length - 2).trimEnd(null).replace(",", "")) * convertToFileSize.FileLengthTB;
        }
        else if (size.endsWith("GB")) {
            return parseFloat(size.substr(0, size.length - 2).trimEnd(null).replace(",", "")) * convertToFileSize.FileLengthGB;
        }
        else if (size.endsWith("MB")) {
            return parseFloat(size.substr(0, size.length - 2).trimEnd(null).replace(",", "")) * convertToFileSize.FileLengthMB;
        }
        else if (size.endsWith("KB")) {
            return parseFloat(size.substr(0, size.length - 2).trimEnd(null).replace(",", "")) * convertToFileSize.FileLengthKB;
        }
        else if (size.endsWith("B")) {
            return parseInt(size.substr(0, size.length - 1).trimEnd(null).replace(",", ""));// * FileLengthB
        }
        return parseInt(size);
    }

    namespace convertToFileSize {
        export const FileLengthB: GLint = 1;
        export const FileLengthKB: GLint = FileLengthB * 1024;
        export const FileLengthMB: GLint = FileLengthKB * 1024;
        export const FileLengthGB: GLint = FileLengthMB * 1024;
        export const FileLengthTB: GLint = FileLengthGB * 1024;
        export const FileLengthPB: GLint = FileLengthTB * 1024;
        export const FileLengthEB: GLint = FileLengthPB * 1024;
        //export const FileLengthZB: GLint = FileLengthEB * 1024;
        //export const FileLengthYB: GLint = FileLengthZB * 1024;
        //export const FileLengthBB: GLint = FileLengthYB * 1024;
    }

    /**
     * 获取文件类型图标。
     * @param ext 文件扩展名。
     * @returns CSS 样式表
     */
    function getFileTypeImage(ext: string): string {
        let r = "";
        switch (ext) {
            case ".ico":
                r = "ico";
                break;
            case ".bmp":
                r = "bmp";
                break;
            case ".gif":
                r = "gif";
                break;
            case ".jpg":
            case ".jpeg":
            case ".jpe":
                r = "jpg";
                break;
            case ".png":
                r = "png";
                break;
            case ".svgz":
            case ".svg":
                r = "svg";
                break;
            case ".mp4":
            case ".webm":
            case ".ogv":
            case ".rm":
            case ".rmvb":
            case ".mpg":
            case ".mpeg":
            case ".mpeg4":
            case ".avi":
            case ".wmv":
            case ".mov":
            case ".mkv":
            case ".3gp":
            case ".flv":
            case ".f4v":
                r = "video";
                break;
            case ".flac":
            case ".ape":
            case ".mp3":
            case ".wma":
            case ".wav":
                r = "audio";
                break;
            case ".zip":
                r = "zip";
                break;
            case ".rar":
                r = "rar";
                break;
            case ".7z":
                r = "_7zip";
                break;
            case ".ai":
                r = "ai";
                break;
            case ".browser":
                r = "browser";
                break;
            case ".iso":
                r = "cd";
                break;
            case ".bat":
            case ".cmd":
                r = "cmd";
                break;
            case ".css":
                r = "css";
                break;
            case ".eml":
                r = "email";
                break;
            case ".emf":
                r = "emf";
                break;
            case ".doc":
            case ".docx":
            case ".wps": //WPS文档格式
                r = "word";
                break;
            case ".xls":
            case ".xlsx":
            case ".et": //WPS文档格式
                r = "excel";
                break;
            case ".ppt":
            case ".pptx":
            case ".dps": //WPS文档格式
                r = "ppt";
                break;
            case ".exe":
                r = "exe";
                break;
            case ".fla":
                r = "flash";
                break;
            case ".font":
                r = "font";
                break;
            case ".help":
                r = "help";
                break;
            case ".js":
                r = "js";
                break;
            case ".pdf":
                r = "pdf";
                break;
            case ".psd":
                r = "psd";
                break;
            case ".raw":
                r = "raw";
                break;
            case ".rtf":
                r = "rtf";
                break;
            case ".swf":
                r = "swf";
                break;
            case ".tif":
                r = "tif";
                break;
            case ".txt":
                r = "txt";
                break;
            case ".wmf":
                r = "wmf";
                break;
            default:
                r = "system";
                break;
        }
        return "filetypes " + r;
    }

    /**
     * 尝试上传下一个文件。
     * @param ctl 上传元素。
     */
    function uploadNextItem(ctl: JQuery) {
        if (ctl.hasClass("uploadItem")) { //如果是上传元素
            ctl = ctl.parent(); //查找文件列表面板。
        }
        let nextItem = ctl.find(".uploadItem[data-uploadstate='wait']").first(); //查找待上传的元素
        if (nextItem.length > 0) {
            uploadFile((nextItem[0] as any).file as File, nextItem);
        }
    }

    /**
     * 当开始上传时调用此方法。
     * @param e
     */
    function beginUpload(e: Thinksea.Net.FileUploader.HttpFileUpload.BeginUploadEventArgs): void {
        let ctl = $("#" + e.CustomParameter);
        ctl.attr("data-begin_upload_time", new Date().toString());
        ctl.attr("data-start_position", e.StartPosition);
    };

    /**
     * 当上传进度更改时调用此方法。
     * @param e
     */
    function progressChanged(e: Thinksea.Net.FileUploader.HttpFileUpload.UploadProgressChangedEventArgs): void {
        //#region 处理上传进度已更改事件。
        let percentComplete: GLfloat = e.FinishedSize * 100.0 / e.FileLength;
        let sPosition: string = convertToFileSize(e.FinishedSize, "0.#");
        let sTotalSize: string = convertToFileSize(e.FileLength, "0.#");

        let ctl = $("#" + e.CustomParameter);
        let progress = ctl.find("progress");
        progress.prop("value", Math.round((e.FinishedSize * 1.0 / e.FileLength) * parseInt(progress.prop("max"))));
        progress.children(".ie").css("width", Math.round(percentComplete) + "%");
        //percentComplete.toFixed(1) + "% "
        let sTimeSpan: string = "";
        {
            let nowTime = new Date();
            let beginUploadTime: Date = new Date(ctl.attr("data-begin_upload_time"));
            let startPosition: GLint = parseInt(ctl.attr("data-start_position")); //上传的起始位置。
            let timeSpan: GLfloat = (nowTime.getTime() - beginUploadTime.getTime()) / 1000.0; //耗费的时间（单位：秒）
            let sendSize: GLint = e.FinishedSize - startPosition; //已经发送的数据大小。
            let speed: GLfloat = sendSize / timeSpan; //上传速度
            let timeNeeded: GLint = e.FileLength / speed; //剩余时间（单位：秒）
            let days: GLint = Math.floor(timeNeeded / 86400);
            let hour: GLint = Math.floor((timeNeeded % 86400) / 3600);
            let minute: GLint = Math.floor((timeNeeded % 3600) / 60);
            let seconds: GLint = Math.ceil(timeNeeded % 60);
            if (days > 0) {
                sTimeSpan += days.format("0") + ".";
            }
            sTimeSpan += hour.format("00") + ":";
            sTimeSpan += minute.format("00") + ":";
            sTimeSpan += seconds.format("00");
        }
        ctl.find(".fileSize").text(Math.round(e.FinishedSize * 1.0 / e.FileLength * 100) + "%,剩余时间:" + sTimeSpan);
        //#endregion

        if (e.FinishedSize == e.FileLength) { //上传完成
            //alert(JSON.stringify(result));
            let result: FileUploadResult = e.ResultData;
            //let ctl = $("#" + result.CallbackParams);
            //let progress = ctl.find("progress");
            progress.hide();
            ctl.find(".fileUpload").val("");
            ctl.attr("data-uploadstate", "success");
            ctl.find(".uploadState").text(result.IsFastUpload ? "秒传完成" : "上传完成").css("color", "green").removeAttr("title").show();

            //if (result.CallbackParams === "ctl4") { //上传文件
            //}
            if (ctl.hasClass("uploadItem")) { //如果是批量上传文件
                let file = result as FileUploadResult;
                ctl.find(".fileSize").text(convertToFileSize(file.FileLength, "0.#"));
                ctl.attr("data-savepath", file.SavePath);
                let filesPanel = ctl.parent().parent(); //查找文件列表面板。
                if (filesPanel.prop("id") === "ctl5") { //批量上传文件
                    let uploadSuccessItems = filesPanel.find(".uploadItem[data-uploadstate='success']");
                    let savePaths = "";
                    for (let i = 0; i < uploadSuccessItems.length; i++) {
                        if (savePaths.length > 0) {
                            savePaths += ";";
                        }
                        savePaths += $(uploadSuccessItems[i]).attr("data-savepath");
                    }
                    let fileUrls = filesPanel.find("input[name='fileUrls']");
                    fileUrls.val(savePaths);
                }
                uploadNextItem(ctl);
            }
        }
    };

    /**
     * 当上传出现错误时调用此方法。
     * @param e
     */
    function onerror(e: Thinksea.Net.FileUploader.HttpFileUpload.UploadErrorEventArgs) {
        let ctl = $("#" + e.CustomParameter);
        let progress = ctl.find("progress");
        progress.hide();
        ctl.find(".fileSize").hide();
        ctl.find(".fileUpload").val("");
        ctl.attr("data-uploadstate", "error");
        ctl.find(".uploadState").text("上传出错！").css("color", "red").attr("title", e.Message).show();
        if (ctl.hasClass("uploadItem")) { //如果是批量上传文件
            uploadNextItem(ctl);
        }
    };

    /**
     * 当上传中止时调用此方法。
     * @param e
     */
    function onabort(e: Thinksea.Net.FileUploader.HttpFileUpload.AbortEventArgs) {
        let ctl = $("#" + e.CustomParameter);
        let progress = ctl.find("progress");
        progress.hide();
        ctl.find(".fileSize").hide();
        ctl.find(".fileUpload").val("");
        ctl.attr("data-uploadstate", "abort");
        ctl.find(".uploadState").text("操作取消！").css("color", "darkgray").show();
        if (ctl.hasClass("uploadItem")) { //如果是批量上传文件
            uploadNextItem(ctl);
        }
    };

    /**
     * 上传一个文件。
     * @param file 待上传的文件。
     * @param ctl 上传元素
     */
    function uploadFile(file: File, ctl: JQuery): void {
        let progress = ctl.find("progress");
        progress.show();
        let fileSize = ctl.find(".fileSize");
        fileSize.text("正在上传…");
        fileSize.show();

        let uploader: Thinksea.Net.FileUploader.HttpFileUpload = new Thinksea.Net.FileUploader.HttpFileUpload();
        uploader.uploadServiceUrl = "/HttpUploadHandler.ashx";
        uploader.beginUpload = beginUpload;
        uploader.uploadProgressChanged = progressChanged;
        uploader.errorOccurred = onerror;
        uploader.onabort = onabort;
        (ctl[0] as any).uploader = uploader;
        uploader.startUpload(file, ctl.prop("id"));

    }

    /**
     * 处理文件选中事件。
     * @param event
     * @param ctl
     */
    function fileSelected(event: any, ctl: JQuery): void {
        if (event.preventDefault) {
            event.preventDefault();
        }
        let files: FileList = (event.dataTransfer && event.dataTransfer.files) || (event.originalEvent && event.originalEvent.dataTransfer.files) || (event as HTMLInputElement).files; //选中的文件列表
        if (files && files.length > 0) { //如果选中了1个或多个文件
            if (ctl.prop("id") === "ctl5") { //如果是多文件上传
                let fileInsertMark = ctl.find(".fileinsertmark"); //查找文件插入标记元素。
                if (fileInsertMark.length == 0) {
                    alert('ERROR：未找到文件插入标记元素。');
                    return;
                }
                let baseId = ctl.prop("id");
                let nextId = 0;
                for (let i = 0; i < files.length; i++) {
                    let file = files[i];
                    let itemId; //新上传元素的ID
                    do { //生成ID
                        itemId = baseId + '_file' + nextId.toString();
                        nextId++;
                    } while (ctl.find("#" + itemId).length > 0);
                    let item = $('<div id="' + itemId + '" class="uploadItem thumbnail pull-left" style="position: relative; width: 350px; margin: 5px;" data-uploadstate="wait">\
    <div class="uploadThumbnail pull-left ' + getFileTypeImage(file.name.getExtensionName().toLowerCase()) + '" style="width: 32px; height: 32px; margin:3px 5px 3px 3px;"></div>\
    <div class="pull-left">\
        <div style="white-space: nowrap; overflow: hidden; text-overflow: ellipsis; width: 240px;" title="' + htmlEncode(file.name) + '">' + htmlEncode(file.name) + '</div>\
        <progress max="100" class="progress progress-striped active progress-success" style="margin-bottom: unset; height: 10px; width: 80px; margin-top: 5px;">\
            <span class="ie" style="display: block; height: 10px;"></span>\
        </progress>\
        <span class="fileSize" style="color: darkgray;">待上传，请稍后…</span>\
        <span class="uploadState" style="display: none;"></span>\
    </div>\
    <button type="button" class="deleteButton btn btn-link pull-right">删除</button>\
</div>'); //新建上传元素。
                    item.find(".deleteButton").on("click", function (e) {
                        let panel = $(this).parent();
                        let uploader: Thinksea.Net.FileUploader.HttpFileUpload = (panel[0] as any).uploader as Thinksea.Net.FileUploader.HttpFileUpload;
                        if (uploader) {
                            uploader.cancelUpload();
                        }
                        let filesControl = panel.parent();
                        panel.remove();
                        uploadNextItem(filesControl);
                    });
                    item.insertBefore(fileInsertMark); //将上传元素添加到文件列表面板。
                    (item[0] as any).file = files[i]; //将文件关联到上传元素。
                }
                uploadNextItem(ctl);
            }
            else {
                uploadFile(files[0], ctl);
            }
        }
    }

    /**
     * 初始化上传元素。
     * @param ctl
     */
    function initUploadControl(ctl: JQuery): void {
        ctl.find(".fileUpload").change(function (): void {
            fileSelected(this, ctl);
        });

        ctl.find("[data-clickupload='true']").click(function () {
            ctl.find(".fileUpload").click();
        });
        ctl.find("[data-dragoverupload='true']").on("dragover", function (event) {
            event.preventDefault();
        }).on("drop", function (event: any) {
            fileSelected(event, ctl);
        });
    }

    /**
     * 校验表单数据。
     */
    function checkForm() {
        $("form").validator('validate');
        if (!($("form")[0] as HTMLFormElement).checkValidity()) {
            return false;
        }
        return true;
    }

    /**
     * 用于功能初始化。此方法是网页的 JavaScript 功能代码入口。
     */
    export function init(): void {
        //#region 阻止拖放文件到非法区域引发页面跳转。
        $("body").on("dragover", function (ev) { ev.preventDefault(); });
        ($("body")[0] as any).ondrop = function (ev) {
            ev.preventDefault();
        };
        //#endregion

        initUploadControl($("#ctl5")); //批量上传文件

        $("form").submit(function () {
            return checkForm();
        });
    }
}

$(function () {
    JavascriptUploadFileDemo.init();
});
