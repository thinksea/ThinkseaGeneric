"use strict";
var JavascriptUploadFileDemo;
(function (JavascriptUploadFileDemo) {
    ;
    function convertToFileSize(size, format = "0.##") {
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
    function convertFileSizeToByte(size) {
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
            return parseInt(size.substring(0, size.length - 1).trimEnd(null).replace(",", ""));
        }
        return parseInt(size);
    }
    (function (convertToFileSize) {
        convertToFileSize.FileLengthB = 1;
        convertToFileSize.FileLengthKB = convertToFileSize.FileLengthB * 1024;
        convertToFileSize.FileLengthMB = convertToFileSize.FileLengthKB * 1024;
        convertToFileSize.FileLengthGB = convertToFileSize.FileLengthMB * 1024;
        convertToFileSize.FileLengthTB = convertToFileSize.FileLengthGB * 1024;
        convertToFileSize.FileLengthPB = convertToFileSize.FileLengthTB * 1024;
        convertToFileSize.FileLengthEB = convertToFileSize.FileLengthPB * 1024;
    })(convertToFileSize || (convertToFileSize = {}));
    function getFileTypeImage(ext) {
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
            case ".wps":
                r = "word";
                break;
            case ".xls":
            case ".xlsx":
            case ".et":
                r = "excel";
                break;
            case ".ppt":
            case ".pptx":
            case ".dps":
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
    function uploadNextItem(ctl) {
        if (ctl.classList.contains("uploadItem")) {
            ctl = ctl.parentElement;
        }
        let nextItem = ctl.querySelector(".uploadItem[data-uploadstate='wait']");
        if (nextItem) {
            uploadFile(nextItem.file, nextItem);
        }
    }
    function beginUpload(e) {
        let ctl = document.getElementById(e.CustomParameter);
        ctl.setAttribute("data-begin_upload_time", new Date().toString());
        ctl.setAttribute("data-start_position", e.StartPosition.toString());
    }
    ;
    function progressChanged(e) {
        var _a;
        let ctl = document.getElementById(e.CustomParameter);
        let progress = ctl.querySelector("progress");
        if (progress) {
            let percentComplete;
            if (e.FileLength === 0) {
                percentComplete = 100.0;
                progress.value = 0;
            }
            else {
                percentComplete = e.FinishedSize * 100.0 / e.FileLength;
                progress.value = Math.round((e.FinishedSize * 1.0 / e.FileLength) * progress.max);
            }
            let ctlIe = progress.querySelector(".ie");
            if (ctlIe) {
                ctlIe.style.width = Math.round(percentComplete) + "%";
            }
        }
        let sTimeSpan = "";
        {
            let nowTime = new Date();
            let beginUploadTime = new Date(ctl.getAttribute("data-begin_upload_time"));
            let startPosition = parseInt(ctl.getAttribute("data-start_position"));
            let timeSpan = (nowTime.getTime() - beginUploadTime.getTime()) / 1000.0;
            let sendSize = e.FinishedSize - startPosition;
            let speed = sendSize / timeSpan;
            let timeNeeded = e.FileLength / speed;
            let days = Math.floor(timeNeeded / 86400);
            let hour = Math.floor((timeNeeded % 86400) / 3600);
            let minute = Math.floor((timeNeeded % 3600) / 60);
            let seconds = Math.ceil(timeNeeded % 60);
            if (days > 0) {
                sTimeSpan += days.format("0") + ".";
            }
            sTimeSpan += hour.format("00") + ":";
            sTimeSpan += minute.format("00") + ":";
            sTimeSpan += seconds.format("00");
        }
        let fileSize = ctl.querySelector(".fileSize");
        if (fileSize) {
            fileSize.innerText = Math.round(e.FinishedSize * 1.0 / e.FileLength * 100) + "%,剩余时间:" + sTimeSpan;
        }
        if (e.FinishedSize == e.FileLength) {
            let result = e.ResultData;
            progress.style.display = "none";
            let fileUpload = ctl.querySelector(".fileUpload");
            if (fileUpload) {
                fileUpload.value = "";
            }
            ctl.setAttribute("data-uploadstate", "success");
            let uploadState = ctl.querySelector(".uploadState");
            if (uploadState) {
                uploadState.innerText = result.IsFastUpload ? "秒传完成" : "上传完成";
                uploadState.style.color = "green";
                uploadState.removeAttribute("title");
                uploadState.style.removeProperty("display");
            }
            if (ctl.classList.contains("uploadItem")) {
                let file = result;
                let fileSize = ctl.querySelector(".fileSize");
                if (fileSize) {
                    fileSize.innerText = convertToFileSize(file.FileLength, "0.#");
                }
                ctl.setAttribute("data-savepath", file.SavePath);
                let filesPanel = (_a = ctl.parentElement) === null || _a === void 0 ? void 0 : _a.parentElement;
                if (filesPanel.id === "ctl5") {
                    let uploadSuccessItems = filesPanel.querySelectorAll(".uploadItem[data-uploadstate='success']");
                    let savePaths = "";
                    for (let i = 0; i < uploadSuccessItems.length; i++) {
                        if (savePaths.length > 0) {
                            savePaths += ";";
                        }
                        savePaths += (uploadSuccessItems[i]).getAttribute("data-savepath");
                    }
                    let fileUrls = filesPanel.querySelector("input[name='fileUrls']");
                    fileUrls.value = savePaths;
                }
                uploadNextItem(ctl);
            }
        }
    }
    ;
    function onerror(e) {
        let ctl = document.getElementById(e.CustomParameter);
        let progress = ctl.querySelector("progress");
        if (progress) {
            progress.style.display = 'none';
        }
        let fileSize = ctl.querySelector(".fileSize");
        if (fileSize) {
            fileSize.style.display = "none";
        }
        let fileUpload = ctl.querySelector(".fileUpload");
        if (fileUpload) {
            fileUpload.value = "";
        }
        ctl.setAttribute("data-uploadstate", "error");
        let uploadState = ctl.querySelector(".uploadState");
        if (uploadState) {
            uploadState.innerText = "上传出错！";
            uploadState.style.color = "red";
            uploadState.setAttribute("title", e.Message);
            uploadState.style.removeProperty("display");
        }
        if (ctl.classList.contains("uploadItem")) {
            uploadNextItem(ctl);
        }
    }
    ;
    function onabort(e) {
        let ctl = document.getElementById(e.CustomParameter);
        let progress = ctl.querySelector("progress");
        if (progress) {
            progress.style.display = "none";
        }
        let fileSize = ctl.querySelector(".fileSize");
        if (fileSize) {
            fileSize.style.display = "none";
        }
        let fileUpload = ctl.querySelector(".fileUpload");
        if (fileUpload) {
            fileUpload.value = "";
        }
        ctl.setAttribute("data-uploadstate", "abort");
        let uploadState = ctl.querySelector(".uploadState");
        if (uploadState) {
            uploadState.innerText = "操作取消！";
            uploadState.style.color = "darkgray";
            uploadState.style.removeProperty("display");
        }
        if (ctl.classList.contains("uploadItem")) {
            uploadNextItem(ctl);
        }
    }
    ;
    function uploadFile(file, ctl) {
        let progress = ctl.querySelector("progress");
        if (progress) {
            progress.style.removeProperty("display");
        }
        let fileSize = ctl.querySelector(".fileSize");
        if (fileSize) {
            fileSize.innerText = "正在上传…";
            fileSize.style.removeProperty("display");
        }
        let uploader = new Thinksea.Net.FileUploader.HttpFileUpload();
        uploader.uploadServiceUrl = "/HttpUploadHandler";
        uploader.beginUpload = beginUpload;
        uploader.uploadProgressChanged = progressChanged;
        uploader.errorOccurred = onerror;
        uploader.onabort = onabort;
        ctl.uploader = uploader;
        uploader.startUpload(file, ctl.id);
    }
    function fileSelected(event, ctl) {
        if (event.preventDefault) {
            event.preventDefault();
        }
        let files = (event.dataTransfer && event.dataTransfer.files) || (event.originalEvent && event.originalEvent.dataTransfer.files) || event.files;
        if (files && files.length > 0) {
            if (ctl.id === "ctl5") {
                let fileInsertMark = ctl.querySelector(".fileinsertmark");
                if (fileInsertMark === null) {
                    alert('ERROR：未找到文件插入标记元素。');
                    return;
                }
                let baseId = ctl.id;
                let nextId = 0;
                for (let i = 0; i < files.length; i++) {
                    let file = files[i];
                    let itemId;
                    do {
                        itemId = baseId + '_file' + nextId.toString();
                        nextId++;
                    } while (ctl.querySelector("#" + itemId) !== null);
                    let htmlCreator = document.createElement('div');
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
                    let item = htmlCreator.firstElementChild;
                    item.querySelector(".deleteButton").onclick = function (e) {
                        let panel = this.parentElement;
                        let uploader = panel.uploader;
                        if (uploader) {
                            uploader.cancelUpload();
                        }
                        let filesControl = panel.parentElement;
                        panel.remove();
                        uploadNextItem(filesControl);
                    };
                    fileInsertMark.parentElement.insertBefore(item, fileInsertMark);
                    item.file = files[i];
                }
                uploadNextItem(ctl);
            }
            else {
                uploadFile(files[0], ctl);
            }
        }
    }
    function initUploadControl(ctl) {
        {
            let ctls = ctl.querySelectorAll(".fileUpload");
            for (let i = 0; i < ctls.length; i++) {
                let item = ctls[i];
                item.onchange = function () {
                    fileSelected(this, ctl);
                };
            }
        }
        {
            let ctls = ctl.querySelectorAll("[data-clickupload='true']");
            for (let i = 0; i < ctls.length; i++) {
                let item = ctls[i];
                item.onclick = function () {
                    let fileUpload = ctl.querySelector(".fileUpload");
                    fileUpload.click();
                };
            }
        }
        {
            let ctls = ctl.querySelectorAll("[data-dragoverupload='true']");
            for (let i = 0; i < ctls.length; i++) {
                let item = ctls[i];
                item.ondragover = function (event) {
                    event.preventDefault();
                };
                item.ondrop = function (event) {
                    fileSelected(event, ctl);
                };
            }
        }
    }
    JavascriptUploadFileDemo.initUploadControl = initUploadControl;
    function checkForm() {
        let forms = document.forms;
        for (let i = 0; i < forms.length; i++) {
            let form = forms[i];
            if (!form.checkValidity()) {
                return false;
            }
        }
        return true;
    }
    function init() {
        document.body.ondragover = function (ev) { ev.preventDefault(); };
        document.body.ondrop = function (ev) {
            ev.preventDefault();
        };
        initUploadControl(document.getElementById("ctl5"));
        if (document.forms.length > 0) {
            document.forms[0].onsubmit = function () {
                return checkForm();
            };
        }
    }
    JavascriptUploadFileDemo.init = init;
})(JavascriptUploadFileDemo || (JavascriptUploadFileDemo = {}));
//# sourceMappingURL=JavascriptUploadFileDemo.js.map