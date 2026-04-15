"use strict";
var Thinksea;
(function (Thinksea) {
    var Net;
    (function (Net) {
        var FileUploader;
        (function (FileUploader) {
            ;
            class HttpFileUpload {
                constructor() {
                    this.uploadServiceUrl = "";
                    this.beginUpload = null;
                    this.findBreakpoint = null;
                    this.uploadProgressChanged = null;
                    this.errorOccurred = null;
                    this.onabort = null;
                    this.checkCode = null;
                    this.cancelling = false;
                }
                static arrayBufferToWordArray(ab) {
                    let i8a = new Uint8Array(ab);
                    let a = [];
                    for (let i = 0; i < i8a.length; i += 4) {
                        a.push(i8a[i] << 24 | i8a[i + 1] << 16 | i8a[i + 2] << 8 | i8a[i + 3]);
                    }
                    return CryptoJS.lib.WordArray.create(a, i8a.length);
                }
                getCheckCode(file) {
                    let _self = this;
                    let chunkSize = 204800;
                    let pos = 0, end = 0;
                    let sha1Instance = CryptoJS.algo.SHA1.create();
                    let reader = new FileReader();
                    reader.onload = function (e) {
                        pos = end;
                        sha1Instance.update(Thinksea.Net.FileUploader.HttpFileUpload.arrayBufferToWordArray(e.target.result));
                        if (pos < file.size) {
                            progressiveReadNext();
                        }
                        else {
                            let sha1Value = sha1Instance.finalize();
                            _self.checkCode = sha1Value.toString().toUpperCase();
                            _self.startFastUpload(file);
                        }
                    };
                    function progressiveReadNext() {
                        end = Math.min(pos + chunkSize, file.size);
                        if (_self.cancelling === true) {
                            if (_self.onabort) {
                                let e = {
                                    CustomParameter: _self.customParameter,
                                };
                                _self.onabort(e);
                            }
                            return;
                        }
                        let blob;
                        if (file.slice) {
                            blob = file.slice(pos, end);
                        }
                        else if (file.webkitSlice) {
                            blob = file.webkitSlice(pos, end);
                        }
                        else if (File.prototype.mozSlice) {
                            blob = file.mozSlice(pos, end);
                        }
                        reader.readAsArrayBuffer(blob);
                    }
                    progressiveReadNext();
                }
                startFastUpload(file) {
                    var _a, _b;
                    let _self = this;
                    if (_self.cancelling === true) {
                        if (_self.onabort) {
                            let e = {
                                CustomParameter: _self.customParameter,
                            };
                            _self.onabort(e);
                        }
                        return;
                    }
                    let url = _self.uploadServiceUrl;
                    url = url.setUriParameter("cmd", "fastupload")
                        .setUriParameter("filename", file.name)
                        .setUriParameter("filesize", file.size.toString())
                        .setUriParameter("checkcode", (_a = _self.checkCode) !== null && _a !== void 0 ? _a : "")
                        .setUriParameter("param", (_b = _self.customParameter) !== null && _b !== void 0 ? _b : "");
                    let xhr = new XMLHttpRequest();
                    if (_self.errorOccurred) {
                        xhr.addEventListener("error", function (e2) {
                            var _a;
                            let e = {
                                Message: e2.message ||
                                    ((_a = e2.target) === null || _a === void 0 ? void 0 : _a.statusText) ||
                                    'Upload failed',
                                CustomParameter: _self.customParameter,
                            };
                            _self.errorOccurred(e);
                        }, false);
                    }
                    if (_self.onabort) {
                        xhr.addEventListener("abort", function (e2) {
                            let e = {
                                CustomParameter: _self.customParameter,
                            };
                            _self.onabort(e);
                        }, false);
                    }
                    xhr.addEventListener("load", function (evt) {
                        let response = evt.target.responseText;
                        let result = JSON.parse(response);
                        let ErrorCode = result.ErrorCode;
                        if (ErrorCode == 0) {
                            if (result.Data !== null) {
                                let pos = file.size;
                                if (_self.uploadProgressChanged) {
                                    let data = {
                                        FinishedSize: pos,
                                        FileLength: file.size,
                                        CustomParameter: _self.customParameter,
                                        ResultData: result.Data,
                                    };
                                    _self.uploadProgressChanged(data);
                                }
                            }
                            else {
                                _self.startBreakpointUpload(file);
                            }
                        }
                        else {
                            if (_self.errorOccurred) {
                                let e = {
                                    Message: result.Message,
                                    CustomParameter: _self.customParameter,
                                };
                                _self.errorOccurred(e);
                            }
                            else {
                                alert("上传出现错误。" + result.Message);
                            }
                        }
                    }, false);
                    xhr.open("POST", url);
                    xhr.send();
                }
                startBreakpointUpload(file) {
                    var _a, _b;
                    let _self = this;
                    if (_self.cancelling === true) {
                        if (_self.onabort) {
                            let e = {
                                CustomParameter: _self.customParameter,
                            };
                            _self.onabort(e);
                        }
                        return;
                    }
                    let url = _self.uploadServiceUrl;
                    url = url.setUriParameter("cmd", "getoffset")
                        .setUriParameter("filename", file.name)
                        .setUriParameter("filesize", file.size.toString())
                        .setUriParameter("checkcode", (_a = _self.checkCode) !== null && _a !== void 0 ? _a : "")
                        .setUriParameter("param", (_b = _self.customParameter) !== null && _b !== void 0 ? _b : "");
                    let xhr = new XMLHttpRequest();
                    if (_self.errorOccurred) {
                        xhr.addEventListener("error", function (e2) {
                            var _a;
                            let e = {
                                Message: e2.message ||
                                    ((_a = e2.target) === null || _a === void 0 ? void 0 : _a.statusText) ||
                                    'Upload failed',
                                CustomParameter: _self.customParameter,
                            };
                            _self.errorOccurred(e);
                        }, false);
                    }
                    if (_self.onabort) {
                        xhr.addEventListener("abort", function (e2) {
                            let e = {
                                CustomParameter: _self.customParameter,
                            };
                            _self.onabort(e);
                        }, false);
                    }
                    xhr.addEventListener("load", function (evt) {
                        let response = evt.target.responseText;
                        let result = JSON.parse(response);
                        let ErrorCode = result.ErrorCode;
                        if (ErrorCode == 0) {
                            let breakpoint = result.Data;
                            if (_self.findBreakpoint && breakpoint !== 0) {
                                let p = {
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
                                let e = {
                                    Message: result.Message,
                                    CustomParameter: _self.customParameter,
                                };
                                _self.errorOccurred(e);
                            }
                            else {
                                alert("上传出现错误。" + result.Message);
                            }
                        }
                    }, false);
                    xhr.open("POST", url);
                    xhr.send();
                }
                beginUploadFile(file, startPosition) {
                    var _a, _b;
                    let _self = this;
                    if (_self.beginUpload) {
                        let e = {
                            FileLength: file.size,
                            CustomParameter: _self.customParameter,
                            StartPosition: startPosition,
                        };
                        _self.beginUpload(e);
                    }
                    let url = _self.uploadServiceUrl;
                    url = url.setUriParameter("cmd", "upload")
                        .setUriParameter("filename", file.name)
                        .setUriParameter("filesize", file.size.toString())
                        .setUriParameter("checkcode", (_a = _self.checkCode) !== null && _a !== void 0 ? _a : "")
                        .setUriParameter("param", (_b = _self.customParameter) !== null && _b !== void 0 ? _b : "");
                    let chunkSize = 204800;
                    let pos = startPosition, end = startPosition;
                    let xhr = new XMLHttpRequest();
                    if (_self.errorOccurred) {
                        xhr.addEventListener("error", function (e2) {
                            var _a;
                            let e = {
                                Message: e2.message ||
                                    ((_a = e2.target) === null || _a === void 0 ? void 0 : _a.statusText) ||
                                    'Upload failed',
                                CustomParameter: _self.customParameter,
                            };
                            _self.errorOccurred(e);
                        }, false);
                    }
                    if (_self.onabort) {
                        xhr.addEventListener("abort", function (e2) {
                            let e = {
                                CustomParameter: _self.customParameter,
                            };
                            _self.onabort(e);
                        }, false);
                    }
                    xhr.addEventListener("load", function (evt) {
                        let response = evt.target.responseText;
                        let result = JSON.parse(response);
                        let ErrorCode = result.ErrorCode;
                        if (ErrorCode == 0) {
                            pos = end;
                            if (_self.uploadProgressChanged) {
                                let data = {
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
                                let e = {
                                    Message: result.Message,
                                    CustomParameter: _self.customParameter,
                                };
                                _self.errorOccurred(e);
                            }
                            else {
                                alert("上传出现错误。" + result.Message);
                            }
                        }
                    }, false);
                    function progressiveUploadNext() {
                        end = Math.min(pos + chunkSize, file.size);
                        if (_self.cancelling === true) {
                            if (_self.onabort) {
                                let e = {
                                    CustomParameter: _self.customParameter,
                                };
                                _self.onabort(e);
                            }
                            return;
                        }
                        let blob;
                        if (file.slice) {
                            blob = file.slice(pos, end);
                        }
                        else if (file.webkitSlice) {
                            blob = file.webkitSlice(pos, end);
                        }
                        else if (File.prototype.mozSlice) {
                            blob = file.mozSlice(pos, end);
                        }
                        let fd = new FormData();
                        fd.append("fileToUpload", blob);
                        let u = url.setUriParameter("offset", pos.toString());
                        xhr.open("POST", u);
                        xhr.send(fd);
                    }
                    progressiveUploadNext();
                }
                startUpload(file, customParameter) {
                    let _self = this;
                    _self.customParameter = customParameter;
                    _self.cancelling = false;
                    _self.getCheckCode(file);
                }
                cancelUpload() {
                    this.cancelling = true;
                }
            }
            FileUploader.HttpFileUpload = HttpFileUpload;
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