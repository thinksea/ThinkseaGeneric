/**
 * 封装了基于 HTTP 协议的文件上传客户端功能。（组件“Thinksea.Net.FileUploader”的 javascript 版本）
 * @version 4.2.0
 * @description 此组件依赖于下列组件
 * 1、jsext 1.5.0 source link：https://github.com/thinksea/jsext
 * 2、谷歌的 JS 散列算法组件“CryptoJS”（版本 4.1.1）支持。 source link：https://github.com/brix/crypto-js
 */
declare namespace Thinksea.Net.FileUploader {
    /**
     * 封装了服务器返回到客户端的数据格式标准。
     */
    interface ServiceResult {
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
    }
    /**
     * 封装了上传文件功能实现。
     */
    class HttpFileUpload {
        /**
         * 文件上传服务地址。
         */
        uploadServiceUrl: string;
        /**
         * 当开始上传文件时引发此事件。
         * @param e 事件参数。
         */
        beginUpload: (e: Thinksea.Net.FileUploader.HttpFileUpload.BeginUploadEventArgs) => void;
        /**
         * 当发现可用的断点上传信息时引发此事件。
         * @param e 事件参数。
         */
        findBreakpoint: (e: Thinksea.Net.FileUploader.HttpFileUpload.BreakpointUploadEventArgs) => void;
        /**
         * 当上传进度更改后引发此事件。
         * @param e 上传进度事件数据。
         */
        uploadProgressChanged: (e: Thinksea.Net.FileUploader.HttpFileUpload.UploadProgressChangedEventArgs) => void;
        /**
         * 当上传过程中出现错误时引发此事件。
         * @param e 事件参数。
         */
        errorOccurred: (e: Thinksea.Net.FileUploader.HttpFileUpload.UploadErrorEventArgs) => void;
        /**
         * 当上传中止时引发此事件。
         * @param e 事件参数。
         */
        onabort: (e: Thinksea.Net.FileUploader.HttpFileUpload.AbortEventArgs) => void;
        /**
         * 自定义参数。
         */
        private customParameter;
        /**
         * 文件完整性校验码，例如 SHA1 或 MD5 等。
         */
        private checkCode;
        /**
         * 一个标识，用于指示是否应该中止上传。
         */
        private cancelling;
        /**
         * 将 ArrayBuffer 对象转换为 CryptoJS 的 WordArray 对象。
         * @param ab 一个 ArrayBuffer 对象。
         * @returns CryptoJS 的 WordArray 对象。
         */
        private static arrayBufferToWordArray;
        /**
         * 获取文件的完整性校验码。
         * @param file 待上传的文件。
         */
        private getCheckCode;
        /**
         * 开始上传文件，支持秒传。
         * @param file 待上传的文件。
         */
        private startFastUpload;
        /**
         * 开始上传文件，支持断点续传。
         * @param file 待上传的文件。
         */
        private startBreakpointUpload;
        /**
         * 从指定的位置开始上传文件。
         * @param file 待上传的文件。
         * @param startPosition 上传起始位置。
         */
        private beginUploadFile;
        /**
         * 开始上传指定文件。
         * @param file 待上传的文件。
         * @param customParameter 自定义参数。
         */
        startUpload(file: File, customParameter?: string): void;
        /**
         * 放弃上传文件。
         */
        cancelUpload(): void;
    }
    /**
     * 封装了上传文件功能实现。
     */
    namespace HttpFileUpload {
        /**
         * 定义开始上传文件事件参数。
         */
        interface BeginUploadEventArgs {
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
        }
        /**
         * 文件上传进度更改事件参数。
         */
        interface UploadProgressChangedEventArgs {
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
        }
        /**
         * 断点上传信息事件参数。
         */
        interface BreakpointUploadEventArgs {
            /**
             * 获取或设置断点上传起始位置，设置为 0 时表示重新上传。
             */
            readonly Breakpoint: GLint64;
            /**
             * 自定义参数。
             */
            readonly CustomParameter: string;
        }
        /**
         * 定义上传过程中出现错误事件参数。
         */
        interface UploadErrorEventArgs {
            /**
             * 错误消息文本。
             */
            readonly Message: string;
            /**
             * 自定义参数。
             */
            readonly CustomParameter: string;
        }
        /**
         * 定义上传中止事件参数。
         */
        interface AbortEventArgs {
            /**
             * 自定义参数。
             */
            readonly CustomParameter: string;
        }
    }
}
