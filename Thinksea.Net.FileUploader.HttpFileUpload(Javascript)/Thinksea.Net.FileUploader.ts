/**
 * 封装了基于 HTTP 协议的文件上传客户端功能。（组件“Thinksea.Net.FileUploader”的 javascript 版本）
 * @version 4.0.0.0
 * @description 此组件依赖于下列组件
 * 1、jsext 1.2.0 source link：https://github.com/thinksea/jsext
 * 2、谷歌的 JS 散列算法组件“CryptoJS”（版本 3.1.9-1）支持。 source link：https://github.com/brix/crypto-js
 */
namespace Thinksea.Net.FileUploader {
    /**
    * 散列算法对象。
    */
    declare let CryptoJS: any;

    /**
     * 封装了服务器返回到客户端的数据格式标准。
     */
    export interface ServiceResult {
        /**
         * 错误码。赋值为0时表示正确执行；否则表示出现了错误。
         */
        readonly ErrorCode: GLint;

        /**
         * 获取或设置对于属性“ErrorCode”的友好文字描述。特别是当“ErrorCode”非0时，应该为其设置此属性。
         */
        readonly Message: string;

        /**
         * 返回到客户端的用户自定义数据。
         */
        readonly Data: any;
    };

    /**
     * 封装了上传文件功能实现。
     */
    export class HttpFileUpload {
        /**
         * 文件上传服务地址。
         */
        public uploadServiceUrl: string = "";
        /**
         * 当开始上传文件时引发此事件。
         * @param e 事件参数。
         */
        public beginUpload: (e: Thinksea.Net.FileUploader.HttpFileUpload.BeginUploadEventArgs) => void = null;
        /**
         * 当发现可用的断点上传信息时引发此事件。
         * @param e 事件参数。
         */
        public findBreakpoint: (e: Thinksea.Net.FileUploader.HttpFileUpload.BreakpointUploadEventArgs) => void = null;
        /**
         * 当上传进度更改后引发此事件。
         * @param e 上传进度事件数据。
         */
        public uploadProgressChanged: (e: Thinksea.Net.FileUploader.HttpFileUpload.UploadProgressChangedEventArgs) => void = null;
        /**
         * 当上传过程中出现错误时引发此事件。
         * @param e 事件参数。
         */
        public errorOccurred: (e: Thinksea.Net.FileUploader.HttpFileUpload.UploadErrorEventArgs) => void = null;
        /**
         * 当上传中止时引发此事件。
         * @param e 事件参数。
         */
        public onabort: (e: Thinksea.Net.FileUploader.HttpFileUpload.AbortEventArgs) => void = null;

        /**
         * 自定义参数。
         */
        private customParameter: string;

        /**
         * 文件完整性校验码，例如 SHA1 或 MD5 等。
         */
        private checkCode: string = null;


        /**
         * 一个标识，用于指示是否应该中止上传。
         */
        private cancelling: boolean = false;

        /**
         * 将 ArrayBuffer 对象转换为 CryptoJS 的 WordArray 对象。
         * @param ab 一个 ArrayBuffer 对象。
         * @returns CryptoJS 的 WordArray 对象。
         */
        private static arrayBufferToWordArray(ab: ArrayBuffer): any {
            let i8a: Uint8Array = new Uint8Array(ab);
            let a = [];
            for (let i = 0; i < i8a.length; i += 4) {
                a.push(i8a[i] << 24 | i8a[i + 1] << 16 | i8a[i + 2] << 8 | i8a[i + 3]);
            }
            return CryptoJS.lib.WordArray.create(a, i8a.length);
        }

        /**
         * 获取文件的完整性校验码。
         * @param file 待上传的文件。
         */
        private getCheckCode(file: File): void {
            //    let files: FileList = event.target.files;
            //    if (!files) {
            //        alert("At least one selected file is invalid - do not select any folders.\
            //Please reselect and try again.");
            //        return false;
            //    }

            //    let file = files[0];

            let _self = this;
            let chunkSize = 204800;
            let pos = 0, end = 0;
            let sha1Instance = CryptoJS.algo.SHA1.create();
            let reader = new FileReader();

            /**
             * 当文件加载完成时回调此方法。
             */
            reader.onload = function (e) {
                pos = end;
                //sha1Instance.update(CryptoJS.enc.Latin1.parse(e.target.result));
                sha1Instance.update(Thinksea.Net.FileUploader.HttpFileUpload.arrayBufferToWordArray((e.target as any).result));
                //let present = ((pos * 1.0) / file.size) * 100;
                //$("#div_load").css("width", Math.round(present) + "%");
                if (pos < file.size) {
                    progressiveReadNext();
                }
                else {
                    let sha1Value: object = sha1Instance.finalize();
                    _self.checkCode = sha1Value.toString().toUpperCase(); //文件完整性校验码（SHA1 算法）
                    //console.log(sha1Value.toString());
                    //$("#sha1_show").html(sha1Value.toString());
                    _self.startFastUpload(file);

                }
            }

            /**
             * 采用这种分块处理的方式可以处理大文件。
             */
            function progressiveReadNext() {
                end = Math.min(pos + chunkSize, file.size);
                if (_self.cancelling === true) {
                    if (_self.onabort) {
                        let e: Thinksea.Net.FileUploader.HttpFileUpload.AbortEventArgs = {
                            CustomParameter: _self.customParameter,
                        };
                        _self.onabort(e);
                    }
                    return;
                }
                let blob;
                if (file.slice) {
                    blob = file.slice(pos, end);
                } else if ((file as any).webkitSlice) {
                    blob = (file as any).webkitSlice(pos, end);
                } else if ((File.prototype as any).mozSlice) {
                    blob = (file as any).mozSlice(pos, end);
                }
                //reader.readAsBinaryString(blob);
                reader.readAsArrayBuffer(blob);
            }
            progressiveReadNext();
        }

        /**
         * 开始上传文件，支持秒传。
         * @param file 待上传的文件。
         */
        private startFastUpload(file: File): void {
            let _self = this;
            if (_self.cancelling === true) {
                if (_self.onabort) {
                    let e: Thinksea.Net.FileUploader.HttpFileUpload.AbortEventArgs = {
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
                .setUriParameter("checkcode", _self.checkCode)
                .setUriParameter("param", _self.customParameter);

            let xhr = new XMLHttpRequest();
            //xhr.upload.addEventListener("progress", uploadProgress, false);
            if (_self.errorOccurred) {
                xhr.addEventListener("error", function (e2: ErrorEvent) {
                    let e: Thinksea.Net.FileUploader.HttpFileUpload.UploadErrorEventArgs = {
                        Message: e2.message,
                        CustomParameter: _self.customParameter,
                    };
                    _self.errorOccurred(e);
                }, false);
            }
            if (_self.onabort) {
                xhr.addEventListener("abort", function (e2: Event) {
                    let e: Thinksea.Net.FileUploader.HttpFileUpload.AbortEventArgs = {
                        CustomParameter: _self.customParameter,
                    };
                    _self.onabort(e);
                }, false);
            }
            /**
             * 当文件片段发送完成时回调此方法。
             */
            xhr.addEventListener("load", function (evt: Event): void {
                let response: string = (evt.target as XMLHttpRequest).responseText;
                let result: Thinksea.Net.FileUploader.ServiceResult = JSON.parse(response);
                let ErrorCode = result.ErrorCode;
                if (ErrorCode == 0) {
                    if (result.Data !== null) //断点续传成功
                    {
                        let pos = file.size;
                        if (_self.uploadProgressChanged) {
                            let data: Thinksea.Net.FileUploader.HttpFileUpload.UploadProgressChangedEventArgs = {
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
                        let e: Thinksea.Net.FileUploader.HttpFileUpload.UploadErrorEventArgs = {
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
        }

        /**
         * 开始上传文件，支持断点续传。
         * @param file 待上传的文件。
         */
        private startBreakpointUpload(file: File): void {
            let _self = this;
            if (_self.cancelling === true) {
                if (_self.onabort) {
                    let e: Thinksea.Net.FileUploader.HttpFileUpload.AbortEventArgs = {
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
                .setUriParameter("checkcode", _self.checkCode)
                .setUriParameter("param", _self.customParameter);

            let xhr = new XMLHttpRequest();
            //xhr.upload.addEventListener("progress", uploadProgress, false);
            if (_self.errorOccurred) {
                xhr.addEventListener("error", function (e2: ErrorEvent) {
                    let e: Thinksea.Net.FileUploader.HttpFileUpload.UploadErrorEventArgs = {
                        Message: e2.message,
                        CustomParameter: _self.customParameter,
                    };
                    _self.errorOccurred(e);
                }, false);
            }
            if (_self.onabort) {
                xhr.addEventListener("abort", function (e2: Event) {
                    let e: Thinksea.Net.FileUploader.HttpFileUpload.AbortEventArgs = {
                        CustomParameter: _self.customParameter,
                    };
                    _self.onabort(e);
                }, false);
            }
            /**
             * 当文件片段发送完成时回调此方法。
             */
            xhr.addEventListener("load", function (evt: Event): void {
                let response: string = (evt.target as XMLHttpRequest).responseText;
                let result: Thinksea.Net.FileUploader.ServiceResult = JSON.parse(response);
                let ErrorCode = result.ErrorCode;
                if (ErrorCode == 0) {
                    let breakpoint: GLint64 = result.Data;

                    if (_self.findBreakpoint && breakpoint !== 0) {
                        let p: Thinksea.Net.FileUploader.HttpFileUpload.BreakpointUploadEventArgs = {
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
                        let e: Thinksea.Net.FileUploader.HttpFileUpload.UploadErrorEventArgs = {
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
        }

        /**
         * 从指定的位置开始上传文件。
         * @param file 待上传的文件。
         * @param startPosition 上传起始位置。
         */
        private beginUploadFile(file: File, startPosition: GLint64): void {
            let _self = this;
            if (_self.beginUpload) {
                let e: Thinksea.Net.FileUploader.HttpFileUpload.BeginUploadEventArgs = {
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
                .setUriParameter("checkcode", _self.checkCode)
                .setUriParameter("param", _self.customParameter);

            let chunkSize = 204800;
            let pos: GLint64 = startPosition, end: GLint64 = startPosition;

            let xhr = new XMLHttpRequest();
            //xhr.upload.addEventListener("progress", uploadProgress, false);
            if (_self.errorOccurred) {
                xhr.addEventListener("error", function (e2: ErrorEvent) {
                    let e: Thinksea.Net.FileUploader.HttpFileUpload.UploadErrorEventArgs = {
                        Message: e2.message,
                        CustomParameter: _self.customParameter,
                    };
                    _self.errorOccurred(e);
                }, false);
            }
            if (_self.onabort) {
                xhr.addEventListener("abort", function (e2: Event) {
                    let e: Thinksea.Net.FileUploader.HttpFileUpload.AbortEventArgs = {
                        CustomParameter: _self.customParameter,
                    };
                    _self.onabort(e);
                }, false);
            }
            /**
             * 当文件片段发送完成时回调此方法。
             */
            xhr.addEventListener("load", function (evt: Event): void {
                let response: string = (evt.target as XMLHttpRequest).responseText;
                let result: Thinksea.Net.FileUploader.ServiceResult = JSON.parse(response);
                let ErrorCode = result.ErrorCode;
                if (ErrorCode == 0) {
                    pos = end;
                    if (_self.uploadProgressChanged) {
                        let data: Thinksea.Net.FileUploader.HttpFileUpload.UploadProgressChangedEventArgs = {
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
                        let e: Thinksea.Net.FileUploader.HttpFileUpload.UploadErrorEventArgs = {
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
                        let e: Thinksea.Net.FileUploader.HttpFileUpload.AbortEventArgs = {
                            CustomParameter: _self.customParameter,
                        };
                        _self.onabort(e);
                    }
                    return;
                }
                let blob;
                if (file.slice) {
                    blob = file.slice(pos, end);
                } else if ((file as any).webkitSlice) {
                    blob = (file as any).webkitSlice(pos, end);
                } else if ((File.prototype as any).mozSlice) {
                    blob = (file as any).mozSlice(pos, end);
                }

                let fd = new FormData();
                //fd.delete("fileToUpload");
                fd.append("fileToUpload", blob);
                let u = url.setUriParameter("offset", pos.toString());
                xhr.open("POST", u);
                xhr.send(fd);
            }

            progressiveUploadNext();
        }

        /**
         * 开始上传指定文件。
         * @param file 待上传的文件。
         * @param customParameter 自定义参数。
         */
        public startUpload(file: File, customParameter?: string): void {
            let _self = this;
            _self.customParameter = customParameter;
            _self.cancelling = false;
            _self.getCheckCode(file);
        }

        /**
         * 放弃上传文件。
         */
        public cancelUpload(): void {
            this.cancelling = true;
        }

    }

    /**
     * 封装了上传文件功能实现。
     */
    export namespace HttpFileUpload {
        /**
         * 定义开始上传文件事件参数。
         */
        export interface BeginUploadEventArgs {
            /**
             * 文件大小（单位：字节）
             */
            readonly FileLength: GLint64;
            /**
             * 上传起始位置。
             */
            readonly StartPosition: GLint64;
            /**
             * 自定义参数。
             */
            readonly CustomParameter: string;
        };

        /**
         * 文件上传进度更改事件参数。
         */
        export interface UploadProgressChangedEventArgs {
            /**
             * 文件大小（单位：字节）
             */
            readonly FileLength: GLint64;
            /**
             * 获取已成功上传的数据大小。
             */
            readonly FinishedSize: GLint64;
            /**
             * 自定义参数。
             */
            readonly CustomParameter: string;
            /**
             * 获取需要返回到客户端的数据。
             */
            readonly ResultData: any;
        };

        /**
         * 断点上传信息事件参数。
         */
        export interface BreakpointUploadEventArgs {
            /**
             * 获取或设置断点上传起始位置，设置为 0 时表示重新上传。
             */
            readonly Breakpoint: GLint64;
            /**
             * 自定义参数。
             */
            readonly CustomParameter: string;
        };

        /**
         * 定义上传过程中出现错误事件参数。
         */
        export interface UploadErrorEventArgs {
            /**
             * 错误消息文本。
             */
            readonly Message: string;
            /**
             * 自定义参数。
             */
            readonly CustomParameter: string;
        };

        /**
         * 定义上传中止事件参数。
         */
        export interface AbortEventArgs {
            /**
             * 自定义参数。
             */
            readonly CustomParameter: string;
        };

    }

}
