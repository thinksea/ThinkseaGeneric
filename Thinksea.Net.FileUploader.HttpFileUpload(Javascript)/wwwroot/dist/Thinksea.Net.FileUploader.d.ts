declare namespace Thinksea.Net.FileUploader {
    interface ServiceResult {
        readonly ErrorCode: GLint;
        readonly Message: string;
        readonly Data: any;
    }
    class HttpFileUpload {
        uploadServiceUrl: string;
        beginUpload: ((e: Thinksea.Net.FileUploader.HttpFileUpload.BeginUploadEventArgs) => void) | null;
        findBreakpoint: ((e: Thinksea.Net.FileUploader.HttpFileUpload.BreakpointUploadEventArgs) => void) | null;
        uploadProgressChanged: ((e: Thinksea.Net.FileUploader.HttpFileUpload.UploadProgressChangedEventArgs) => void) | null;
        errorOccurred: ((e: Thinksea.Net.FileUploader.HttpFileUpload.UploadErrorEventArgs) => void) | null;
        onabort: ((e: Thinksea.Net.FileUploader.HttpFileUpload.AbortEventArgs) => void) | null;
        private customParameter?;
        private checkCode;
        private cancelling;
        private static arrayBufferToWordArray;
        private getCheckCode;
        private startFastUpload;
        private startBreakpointUpload;
        private beginUploadFile;
        startUpload(file: File, customParameter?: string): void;
        cancelUpload(): void;
    }
    namespace HttpFileUpload {
        interface BeginUploadEventArgs {
            readonly FileLength: GLint64;
            readonly StartPosition: GLint64;
            readonly CustomParameter?: string;
        }
        interface UploadProgressChangedEventArgs {
            readonly FileLength: GLint64;
            readonly FinishedSize: GLint64;
            readonly CustomParameter?: string;
            readonly ResultData: any;
        }
        interface BreakpointUploadEventArgs {
            readonly Breakpoint: GLint64;
            readonly CustomParameter?: string;
        }
        interface UploadErrorEventArgs {
            readonly Message: string;
            readonly CustomParameter?: string;
        }
        interface AbortEventArgs {
            readonly CustomParameter?: string;
        }
    }
}
