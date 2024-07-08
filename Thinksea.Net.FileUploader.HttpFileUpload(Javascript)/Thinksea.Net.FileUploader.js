/**
 * 封装了基于 HTTP 协议的文件上传客户端功能。（组件“Thinksea.Net.FileUploader”的 javascript 版本）
 * @version 4.2.0
 * @description 此组件依赖于下列组件
 * 1、jsext 1.5.0 source link：https://github.com/thinksea/jsext
 * 2、谷歌的 JS 散列算法组件“CryptoJS”（版本 4.1.1）支持。 source link：https://github.com/brix/crypto-js
 */
var Thinksea;
(function (Thinksea) {
    var Net;
    (function (Net) {
        var FileUploader;
        (function (FileUploader) {
            ;
            /**
             * 封装了上传文件功能实现。
             */
            var HttpFileUpload = /** @class */ (function () {
                function HttpFileUpload() {
                    /**
                     * 文件上传服务地址。
                     */
                    this.uploadServiceUrl = "";
                    /**
                     * 当开始上传文件时引发此事件。
                     * @param e 事件参数。
                     */
                    this.beginUpload = null;
                    /**
                     * 当发现可用的断点上传信息时引发此事件。
                     * @param e 事件参数。
                     */
                    this.findBreakpoint = null;
                    /**
                     * 当上传进度更改后引发此事件。
                     * @param e 上传进度事件数据。
                     */
                    this.uploadProgressChanged = null;
                    /**
                     * 当上传过程中出现错误时引发此事件。
                     * @param e 事件参数。
                     */
                    this.errorOccurred = null;
                    /**
                     * 当上传中止时引发此事件。
                     * @param e 事件参数。
                     */
                    this.onabort = null;
                    /**
                     * 文件完整性校验码，例如 SHA1 或 MD5 等。
                     */
                    this.checkCode = null;
                    /**
                     * 一个标识，用于指示是否应该中止上传。
                     */
                    this.cancelling = false;
                }
                /**
                 * 将 ArrayBuffer 对象转换为 CryptoJS 的 WordArray 对象。
                 * @param ab 一个 ArrayBuffer 对象。
                 * @returns CryptoJS 的 WordArray 对象。
                 */
                HttpFileUpload.arrayBufferToWordArray = function (ab) {
                    var i8a = new Uint8Array(ab);
                    var a = [];
                    for (var i = 0; i < i8a.length; i += 4) {
                        a.push(i8a[i] << 24 | i8a[i + 1] << 16 | i8a[i + 2] << 8 | i8a[i + 3]);
                    }
                    return CryptoJS.lib.WordArray.create(a, i8a.length);
                };
                /**
                 * 获取文件的完整性校验码。
                 * @param file 待上传的文件。
                 */
                HttpFileUpload.prototype.getCheckCode = function (file) {
                    //    let files: FileList = event.target.files;
                    //    if (!files) {
                    //        alert("At least one selected file is invalid - do not select any folders.\
                    //Please reselect and try again.");
                    //        return false;
                    //    }
                    //    let file = files[0];
                    var _self = this;
                    var chunkSize = 204800;
                    var pos = 0, end = 0;
                    var sha1Instance = CryptoJS.algo.SHA1.create();
                    var reader = new FileReader();
                    /**
                     * 当文件加载完成时回调此方法。
                     */
                    reader.onload = function (e) {
                        pos = end;
                        //sha1Instance.update(CryptoJS.enc.Latin1.parse(e.target.result));
                        sha1Instance.update(Thinksea.Net.FileUploader.HttpFileUpload.arrayBufferToWordArray(e.target.result));
                        //let present = ((pos * 1.0) / file.size) * 100;
                        //$("#div_load").css("width", Math.round(present) + "%");
                        if (pos < file.size) {
                            progressiveReadNext();
                        }
                        else {
                            var sha1Value = sha1Instance.finalize();
                            _self.checkCode = sha1Value.toString().toUpperCase(); //文件完整性校验码（SHA1 算法）
                            //console.log(sha1Value.toString());
                            //$("#sha1_show").html(sha1Value.toString());
                            _self.startFastUpload(file);
                        }
                    };
                    /**
                     * 采用这种分块处理的方式可以处理大文件。
                     */
                    function progressiveReadNext() {
                        end = Math.min(pos + chunkSize, file.size);
                        if (_self.cancelling === true) {
                            if (_self.onabort) {
                                var e = {
                                    CustomParameter: _self.customParameter,
                                };
                                _self.onabort(e);
                            }
                            return;
                        }
                        var blob;
                        if (file.slice) {
                            blob = file.slice(pos, end);
                        }
                        else if (file.webkitSlice) {
                            blob = file.webkitSlice(pos, end);
                        }
                        else if (File.prototype.mozSlice) {
                            blob = file.mozSlice(pos, end);
                        }
                        //reader.readAsBinaryString(blob);
                        reader.readAsArrayBuffer(blob);
                    }
                    progressiveReadNext();
                };
                /**
                 * 开始上传文件，支持秒传。
                 * @param file 待上传的文件。
                 */
                HttpFileUpload.prototype.startFastUpload = function (file) {
                    var _self = this;
                    if (_self.cancelling === true) {
                        if (_self.onabort) {
                            var e = {
                                CustomParameter: _self.customParameter,
                            };
                            _self.onabort(e);
                        }
                        return;
                    }
                    var url = _self.uploadServiceUrl;
                    url = url.setUriParameter("cmd", "fastupload")
                        .setUriParameter("filename", file.name)
                        .setUriParameter("filesize", file.size.toString())
                        .setUriParameter("checkcode", _self.checkCode)
                        .setUriParameter("param", _self.customParameter);
                    var xhr = new XMLHttpRequest();
                    //xhr.upload.addEventListener("progress", uploadProgress, false);
                    if (_self.errorOccurred) {
                        xhr.addEventListener("error", function (e2) {
                            var e = {
                                Message: e2.message,
                                CustomParameter: _self.customParameter,
                            };
                            _self.errorOccurred(e);
                        }, false);
                    }
                    if (_self.onabort) {
                        xhr.addEventListener("abort", function (e2) {
                            var e = {
                                CustomParameter: _self.customParameter,
                            };
                            _self.onabort(e);
                        }, false);
                    }
                    /**
                     * 当文件片段发送完成时回调此方法。
                     */
                    xhr.addEventListener("load", function (evt) {
                        var response = evt.target.responseText;
                        var result = JSON.parse(response);
                        var ErrorCode = result.ErrorCode;
                        if (ErrorCode == 0) {
                            if (result.Data !== null) //断点续传成功
                             {
                                var pos = file.size;
                                if (_self.uploadProgressChanged) {
                                    var data = {
                                        FinishedSize: pos,
                                        FileLength: file.size,
                                        CustomParameter: _self.customParameter,
                                        ResultData: result.Data,
                                    };
                                    _self.uploadProgressChanged(data);
                                }
                            }
                            else { //无法秒传，开始尝试断点续传。
                                _self.startBreakpointUpload(file);
                            }
                        }
                        else {
                            if (_self.errorOccurred) {
                                var e = {
                                    Message: result.Message,
                                    CustomParameter: _self.customParameter,
                                };
                                //new upload.HttpFileUpload.UploadErrorEventArgs("", {
                                //    message: result.Message
                                //});
                                _self.errorOccurred(e);
                            }
                            else {
                                alert("上传出现错误。" + result.Message);
                            }
                        }
                    }, false);
                    xhr.open("POST", url);
                    xhr.send();
                };
                /**
                 * 开始上传文件，支持断点续传。
                 * @param file 待上传的文件。
                 */
                HttpFileUpload.prototype.startBreakpointUpload = function (file) {
                    var _self = this;
                    if (_self.cancelling === true) {
                        if (_self.onabort) {
                            var e = {
                                CustomParameter: _self.customParameter,
                            };
                            _self.onabort(e);
                        }
                        return;
                    }
                    var url = _self.uploadServiceUrl;
                    url = url.setUriParameter("cmd", "getoffset")
                        .setUriParameter("filename", file.name)
                        .setUriParameter("filesize", file.size.toString())
                        .setUriParameter("checkcode", _self.checkCode)
                        .setUriParameter("param", _self.customParameter);
                    var xhr = new XMLHttpRequest();
                    //xhr.upload.addEventListener("progress", uploadProgress, false);
                    if (_self.errorOccurred) {
                        xhr.addEventListener("error", function (e2) {
                            var e = {
                                Message: e2.message,
                                CustomParameter: _self.customParameter,
                            };
                            _self.errorOccurred(e);
                        }, false);
                    }
                    if (_self.onabort) {
                        xhr.addEventListener("abort", function (e2) {
                            var e = {
                                CustomParameter: _self.customParameter,
                            };
                            _self.onabort(e);
                        }, false);
                    }
                    /**
                     * 当文件片段发送完成时回调此方法。
                     */
                    xhr.addEventListener("load", function (evt) {
                        var response = evt.target.responseText;
                        var result = JSON.parse(response);
                        var ErrorCode = result.ErrorCode;
                        if (ErrorCode == 0) {
                            var breakpoint = result.Data;
                            if (_self.findBreakpoint && breakpoint !== 0) {
                                var p = {
                                    Breakpoint: breakpoint,
                                    CustomParameter: _self.customParameter,
                                };
                                _self.findBreakpoint(p);
                                breakpoint = p.Breakpoint;
                            }
                            _self.beginUploadFile(file, breakpoint);
                        }
                        else {
                            if (_self.errorOccurred) {
                                var e = {
                                    Message: result.Message,
                                    CustomParameter: _self.customParameter,
                                };
                                //new upload.HttpFileUpload.UploadErrorEventArgs("", {
                                //    message: result.Message
                                //});
                                _self.errorOccurred(e);
                            }
                            else {
                                alert("上传出现错误。" + result.Message);
                            }
                        }
                    }, false);
                    xhr.open("POST", url);
                    xhr.send();
                };
                /**
                 * 从指定的位置开始上传文件。
                 * @param file 待上传的文件。
                 * @param startPosition 上传起始位置。
                 */
                HttpFileUpload.prototype.beginUploadFile = function (file, startPosition) {
                    var _self = this;
                    if (_self.beginUpload) {
                        var e = {
                            FileLength: file.size,
                            CustomParameter: _self.customParameter,
                            StartPosition: startPosition,
                        };
                        _self.beginUpload(e);
                    }
                    var url = _self.uploadServiceUrl;
                    url = url.setUriParameter("cmd", "upload")
                        .setUriParameter("filename", file.name)
                        .setUriParameter("filesize", file.size.toString())
                        .setUriParameter("checkcode", _self.checkCode)
                        .setUriParameter("param", _self.customParameter);
                    var chunkSize = 204800;
                    var pos = startPosition, end = startPosition;
                    var xhr = new XMLHttpRequest();
                    //xhr.upload.addEventListener("progress", uploadProgress, false);
                    if (_self.errorOccurred) {
                        xhr.addEventListener("error", function (e2) {
                            var e = {
                                Message: e2.message,
                                CustomParameter: _self.customParameter,
                            };
                            _self.errorOccurred(e);
                        }, false);
                    }
                    if (_self.onabort) {
                        xhr.addEventListener("abort", function (e2) {
                            var e = {
                                CustomParameter: _self.customParameter,
                            };
                            _self.onabort(e);
                        }, false);
                    }
                    /**
                     * 当文件片段发送完成时回调此方法。
                     */
                    xhr.addEventListener("load", function (evt) {
                        var response = evt.target.responseText;
                        var result = JSON.parse(response);
                        var ErrorCode = result.ErrorCode;
                        if (ErrorCode == 0) {
                            pos = end;
                            if (_self.uploadProgressChanged) {
                                var data = {
                                    FinishedSize: pos,
                                    FileLength: file.size,
                                    CustomParameter: _self.customParameter,
                                    ResultData: result.Data,
                                };
                                _self.uploadProgressChanged(data);
                            }
                            if (pos < file.size) {
                                progressiveUploadNext();
                            }
                        }
                        else {
                            if (_self.errorOccurred) {
                                var e = {
                                    Message: result.Message,
                                    CustomParameter: _self.customParameter,
                                };
                                //new upload.HttpFileUpload.UploadErrorEventArgs("", {
                                //    message: result.Message
                                //});
                                _self.errorOccurred(e);
                            }
                            else {
                                alert("上传出现错误。" + result.Message);
                            }
                        }
                    }, false);
                    /**
                     * 采用这种分块处理的方式可以处理大文件。
                     */
                    function progressiveUploadNext() {
                        end = Math.min(pos + chunkSize, file.size);
                        if (_self.cancelling === true) {
                            if (_self.onabort) {
                                var e = {
                                    CustomParameter: _self.customParameter,
                                };
                                _self.onabort(e);
                            }
                            return;
                        }
                        var blob;
                        if (file.slice) {
                            blob = file.slice(pos, end);
                        }
                        else if (file.webkitSlice) {
                            blob = file.webkitSlice(pos, end);
                        }
                        else if (File.prototype.mozSlice) {
                            blob = file.mozSlice(pos, end);
                        }
                        var fd = new FormData();
                        //fd.delete("fileToUpload");
                        fd.append("fileToUpload", blob);
                        var u = url.setUriParameter("offset", pos.toString());
                        xhr.open("POST", u);
                        xhr.send(fd);
                    }
                    progressiveUploadNext();
                };
                /**
                 * 开始上传指定文件。
                 * @param file 待上传的文件。
                 * @param customParameter 自定义参数。
                 */
                HttpFileUpload.prototype.startUpload = function (file, customParameter) {
                    var _self = this;
                    _self.customParameter = customParameter;
                    _self.cancelling = false;
                    _self.getCheckCode(file);
                };
                /**
                 * 放弃上传文件。
                 */
                HttpFileUpload.prototype.cancelUpload = function () {
                    this.cancelling = true;
                };
                return HttpFileUpload;
            }());
            FileUploader.HttpFileUpload = HttpFileUpload;
            /**
             * 封装了上传文件功能实现。
             */
            (function (HttpFileUpload) {
                ;
                ;
                ;
                ;
                ;
            })(HttpFileUpload = FileUploader.HttpFileUpload || (FileUploader.HttpFileUpload = {}));
        })(FileUploader = Net.FileUploader || (Net.FileUploader = {}));
    })(Net = Thinksea.Net || (Thinksea.Net = {}));
})(Thinksea || (Thinksea = {}));
//# sourceMappingURL=Thinksea.Net.FileUploader.js.map