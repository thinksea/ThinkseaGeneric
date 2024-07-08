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
            return parseFloat(size.substring(0, size.length - 2).trimEnd(null).replace(",", "")) * convertToFileSize.FileLengthEB;
        }
        else if (size.endsWith("PB")) {
            return parseFloat(size.substring(0, size.length - 2).trimEnd(null).replace(",", "")) * convertToFileSize.FileLengthPB;
        }
        else if (size.endsWith("TB")) {
            return parseFloat(size.substring(0, size.length - 2).trimEnd(null).replace(",", "")) * convertToFileSize.FileLengthTB;
        }
        else if (size.endsWith("GB")) {
            return parseFloat(size.substring(0, size.length - 2).trimEnd(null).replace(",", "")) * convertToFileSize.FileLengthGB;
        }
        else if (size.endsWith("MB")) {
            return parseFloat(size.substring(0, size.length - 2).trimEnd(null).replace(",", "")) * convertToFileSize.FileLengthMB;
        }
        else if (size.endsWith("KB")) {
            return parseFloat(size.substring(0, size.length - 2).trimEnd(null).replace(",", "")) * convertToFileSize.FileLengthKB;
        }
        else if (size.endsWith("B")) {
            return parseInt(size.substring(0, size.length - 1).trimEnd(null).replace(",", ""));// * FileLengthB
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
    function uploadNextItem(ctl: HTMLElement): void {
        if (ctl.classList.contains("uploadItem")) { //如果是上传元素
            ctl = ctl.parentElement; //查找文件列表面板。
        }
        let nextItem = ctl.querySelector(".uploadItem[data-uploadstate='wait']"); //查找待上传的元素
        if (nextItem) {
            uploadFile((nextItem as any).file as File, nextItem as HTMLElement);
        }
    }

    /**
     * 当开始上传时调用此方法。
     * @param e
     */
    function beginUpload(e: Thinksea.Net.FileUploader.HttpFileUpload.BeginUploadEventArgs): void {
        let ctl = document.getElementById(e.CustomParameter) as HTMLElement;
        ctl.setAttribute("data-begin_upload_time", new Date().toString());
        ctl.setAttribute("data-start_position", e.StartPosition.toString());
    };

    /**
     * 当上传进度更改时调用此方法。
     * @param e
     */
    function progressChanged(e: Thinksea.Net.FileUploader.HttpFileUpload.UploadProgressChangedEventArgs): void {
        //#region 处理上传进度已更改事件。
        //let sPosition: string = convertToFileSize(e.FinishedSize, "0.#");
        //let sTotalSize: string = convertToFileSize(e.FileLength, "0.#");

        let ctl = document.getElementById(e.CustomParameter) as HTMLElement;
        let progress = ctl.querySelector("progress");
        if (progress) {
            let percentComplete: GLfloat;
            if (e.FileLength === 0) {
                percentComplete = 100.0;
                progress.value = 0;
            }
            else {
                percentComplete = e.FinishedSize * 100.0 / e.FileLength;
                progress.value = Math.round((e.FinishedSize * 1.0 / e.FileLength) * progress.max);
            }
            let ctlIe = progress.querySelector(".ie") as HTMLElement;
            if (ctlIe) {
                ctlIe.style.width = Math.round(percentComplete) + "%";
            }
        }
        //percentComplete.toFixed(1) + "% "
        let sTimeSpan: string = "";
        {
            let nowTime = new Date();
            let beginUploadTime: Date = new Date(ctl.getAttribute("data-begin_upload_time"));
            let startPosition: GLint = parseInt(ctl.getAttribute("data-start_position")); //上传的起始位置。
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
        let fileSize = ctl.querySelector(".fileSize") as HTMLElement;
        if (fileSize) {
            fileSize.innerText = Math.round(e.FinishedSize * 1.0 / e.FileLength * 100) + "%,剩余时间:" + sTimeSpan;
        }
        //#endregion

        if (e.FinishedSize == e.FileLength) { //上传完成
            //alert(JSON.stringify(result));
            let result: FileUploadResult = e.ResultData;
            //let ctl = $("#" + result.CallbackParams);
            //let progress = ctl.find("progress");
            progress.style.display = "none";
            let fileUpload = ctl.querySelector(".fileUpload") as HTMLInputElement;
            if (fileUpload) {
                fileUpload.value = "";
            }
            ctl.setAttribute("data-uploadstate", "success");
            let uploadState = ctl.querySelector(".uploadState") as HTMLElement;
            if (uploadState) {
                uploadState.innerText = result.IsFastUpload ? "秒传完成" : "上传完成";
                uploadState.style.color = "green";
                uploadState.removeAttribute("title");
                uploadState.style.removeProperty("display");
            }

            //if (result.CallbackParams === "ctl4") { //上传文件
            //}
            if (ctl.classList.contains("uploadItem")) { //如果是批量上传文件
                let file = result as FileUploadResult;
                let fileSize = ctl.querySelector(".fileSize") as HTMLElement;
                if (fileSize) {
                    fileSize.innerText = convertToFileSize(file.FileLength, "0.#");
                }
                ctl.setAttribute("data-savepath", file.SavePath);
                let filesPanel = ctl.parentElement.parentElement; //查找文件列表面板。
                if (filesPanel.id === "ctl5") { //批量上传文件
                    let uploadSuccessItems = filesPanel.querySelectorAll(".uploadItem[data-uploadstate='success']");
                    let savePaths = "";
                    for (let i = 0; i < uploadSuccessItems.length; i++) {
                        if (savePaths.length > 0) {
                            savePaths += ";";
                        }
                        savePaths += (uploadSuccessItems[i]).getAttribute("data-savepath");
                    }
                    let fileUrls = filesPanel.querySelector("input[name='fileUrls']") as HTMLInputElement;
                    fileUrls.value = savePaths;
                }
                uploadNextItem(ctl);
            }
        }
    };

    /**
     * 当上传出现错误时调用此方法。
     * @param e
     */
    function onerror(e: Thinksea.Net.FileUploader.HttpFileUpload.UploadErrorEventArgs): void {
        let ctl = document.getElementById(e.CustomParameter) as HTMLElement;
        let progress = ctl.querySelector("progress");
        if (progress) {
            progress.style.display = 'none';
        }
        let fileSize = ctl.querySelector(".fileSize") as HTMLElement;
        if (fileSize) {
            fileSize.style.display = "none";
        }
        let fileUpload = ctl.querySelector(".fileUpload") as HTMLInputElement;
        if (fileUpload) {
            fileUpload.value = "";
        }
        ctl.setAttribute("data-uploadstate", "error");
        let uploadState = ctl.querySelector(".uploadState") as HTMLElement;
        if (uploadState) {
            uploadState.innerText = "上传出错！";
            uploadState.style.color = "red";
            uploadState.setAttribute("title", e.Message);
            uploadState.style.removeProperty("display");
        }
        if (ctl.classList.contains("uploadItem")) { //如果是批量上传文件
            uploadNextItem(ctl);
        }
    };

    /**
     * 当上传中止时调用此方法。
     * @param e
     */
    function onabort(e: Thinksea.Net.FileUploader.HttpFileUpload.AbortEventArgs): void {
        let ctl = document.getElementById(e.CustomParameter) as HTMLElement;
        let progress = ctl.querySelector("progress");
        if (progress) {
            progress.style.display = "none";
        }
        let fileSize = ctl.querySelector(".fileSize") as HTMLElement;
        if (fileSize) {
            fileSize.style.display = "none";
        }
        let fileUpload = ctl.querySelector(".fileUpload") as HTMLInputElement;
        if (fileUpload) {
            fileUpload.value = "";
        }
        ctl.setAttribute("data-uploadstate", "abort");
        let uploadState = ctl.querySelector(".uploadState") as HTMLElement;
        if (uploadState) {
            uploadState.innerText = "操作取消！";
            uploadState.style.color = "darkgray";
            uploadState.style.removeProperty("display");
        }
        if (ctl.classList.contains("uploadItem")) { //如果是批量上传文件
            uploadNextItem(ctl);
        }
    };

    /**
     * 上传一个文件。
     * @param file 待上传的文件。
     * @param ctl 上传元素
     */
    function uploadFile(file: File, ctl: HTMLElement): void {
        let progress = ctl.querySelector("progress");
        if (progress) {
            progress.style.removeProperty("display");
        }
        let fileSize = ctl.querySelector(".fileSize") as HTMLElement;
        if (fileSize) {
            fileSize.innerText = "正在上传…";
            fileSize.style.removeProperty("display");
        }
        let uploader: Thinksea.Net.FileUploader.HttpFileUpload = new Thinksea.Net.FileUploader.HttpFileUpload();
        uploader.uploadServiceUrl = "/HttpUploadHandler";
        uploader.beginUpload = beginUpload;
        uploader.uploadProgressChanged = progressChanged;
        uploader.errorOccurred = onerror;
        uploader.onabort = onabort;
        (ctl as any).uploader = uploader;
        uploader.startUpload(file, ctl.id);

    }

    /**
     * 处理文件选中事件。
     * @param event
     * @param ctl
     */
    function fileSelected(event: any, ctl: HTMLElement): void {
        if (event.preventDefault) {
            event.preventDefault();
        }
        let files: FileList = (event.dataTransfer && event.dataTransfer.files) || (event.originalEvent && event.originalEvent.dataTransfer.files) || (event as HTMLInputElement).files; //选中的文件列表
        if (files && files.length > 0) { //如果选中了1个或多个文件
            if (ctl.id === "ctl5") { //如果是多文件上传
                let fileInsertMark = ctl.querySelector(".fileinsertmark"); //查找文件插入标记元素。
                if (fileInsertMark === null) {
                    alert('ERROR：未找到文件插入标记元素。');
                    return;
                }
                let baseId = ctl.id;
                let nextId = 0;
                for (let i = 0; i < files.length; i++) {
                    let file = files[i];
                    let itemId; //新上传元素的ID
                    do { //生成ID
                        itemId = baseId + '_file' + nextId.toString();
                        nextId++;
                    } while (ctl.querySelector("#" + itemId) !== null);
                    let htmlCreator = document.createElement('div'); //创建一个 HTML 元素创建器（利用 DIV 元素将 HTML 代码片段转换为 HTML 元素）。
                    htmlCreator.innerHTML = '<div id="' + itemId + '" class="uploadItem thumbnail pull-left" style="position: relative; width: 350px; margin: 5px;" data-uploadstate="wait">\
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
</div>';
                    let item = htmlCreator.firstElementChild as HTMLDivElement; //新建上传元素。
                    (item.querySelector(".deleteButton") as HTMLButtonElement).onclick = function (e) {
                        let panel = (this as HTMLButtonElement).parentElement;
                        let uploader: Thinksea.Net.FileUploader.HttpFileUpload = (panel as any).uploader as Thinksea.Net.FileUploader.HttpFileUpload;
                        if (uploader) {
                            uploader.cancelUpload();
                        }
                        let filesControl = panel.parentElement;
                        panel.remove();
                        uploadNextItem(filesControl);
                    };
                    fileInsertMark.parentElement.insertBefore(item, fileInsertMark); //将上传元素添加到文件列表面板。
                    (item as any).file = files[i]; //将文件关联到上传元素。
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
    export function initUploadControl(ctl: HTMLElement): void {
        {
            let ctls = ctl.querySelectorAll(".fileUpload");
            for (let i = 0; i < ctls.length; i++) {
                let item = ctls[i] as HTMLInputElement;
                item.onchange = function (): void {
                    fileSelected(this, ctl);
                };
            }
        }

        {
            let ctls = ctl.querySelectorAll("[data-clickupload='true']");
            for (let i = 0; i < ctls.length; i++) {
                let item = ctls[i] as HTMLElement;
                item.onclick = function () {
                    let fileUpload = ctl.querySelector(".fileUpload") as HTMLElement;
                    fileUpload.click();
                };
            }
        }

        {
            let ctls = ctl.querySelectorAll("[data-dragoverupload='true']");
            for (let i = 0; i < ctls.length; i++) {
                let item = ctls[i] as HTMLElement;
                item.ondragover = function (event) {
                    event.preventDefault();
                };
                item.ondrop = function (event) {
                    fileSelected(event, ctl);
                };
            }
        }
    }

    /**
     * 校验表单数据。
     */
    function checkForm(): boolean {
        let forms = document.forms;
        for (let i = 0; i < forms.length; i++) {
            let form = forms[i];
            if (!form.checkValidity()) {
                return false;
            }
        }
        //.validator('validate');
        return true;
    }

    /**
     * 用于功能初始化。此方法是网页的 JavaScript 功能代码入口。
     */
    export function init(): void {
        //#region 阻止拖放文件到非法区域引发页面跳转。
        document.body.ondragover = function (ev) { ev.preventDefault(); };
        document.body.ondrop = function (ev) {
            ev.preventDefault();
        };
        //#endregion

        initUploadControl(document.getElementById("ctl5")); //批量上传文件

        if (document.forms.length > 0) {
            document.forms[0].onsubmit = function () {
                return checkForm();
            };
        }
    }
}
