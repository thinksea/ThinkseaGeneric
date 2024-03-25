using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace Thinksea.Drawing
{
	/// <summary>
	/// 封装了制作图像缩略图的相关功能。
	/// </summary>
	public class ThumbnailImage : IDisposable
    {
        /// <summary>
        /// 指示应该如何调整图像大小。
        /// </summary>
        public enum ResizeMode
        {
            /// <summary>
            /// 图像被等比例放大或缩小，以适合其容器的边界。保留空白区域并用指定的颜色填充。如果只传递一个尺寸，将保持原始纵横比。
            /// </summary>
            Pad,
            /// <summary>
            /// 图像被等比例缩小，以适合其容器的边界。除了图像不会被放大，其他行为与<see cref="Pad"/>相同。
            /// </summary>
            BoxPad,
            /// <summary>
            /// 图像被等比例放大或缩小，以适合其容器的边界。不保留空白区域。如果只传递一个尺寸，将保持原始纵横比。
            /// </summary>
            PadWithoutPad,
            /// <summary>
            /// 图像被等比例缩小，以适合其容器的边界。在此模式下，放大功能被禁用，如果尝试放大，将返回原始图像。除了图像不会被放大，其他行为与<see cref="PadWithoutPad"/>相同。
            /// </summary>
            BoxWithoutPad,
            /// <summary>
            /// 图像被拉伸或收缩至容器的尺寸。
            /// </summary>
            Stretch,
        }

        /// <summary>
        /// 调整图像大小的选项。
        /// </summary>
        public class ResizeOptions
        {
            /// <summary>
            /// 调整图像大小模式。
            /// </summary>
            public ResizeMode Mode { get; set; }

            /// <summary>
            /// 缩略图的尺寸。
            /// </summary>
            /// <remarks>
            /// 宽度和（或）高度设置为0时,表示不限制。
            /// </remarks>
            public System.Drawing.Size Size { get; set; }

            /// <summary>
            /// 填充图像时用作背景的颜色。
            /// </summary>
            public System.Drawing.Color PadColor { get; set; }

            /// <summary>
            /// 缩略图的最大水平分辨率（以“像素/英寸”为单位）。设置为 null 时表示使用不超过原图分辨率的最大值。
            /// </summary>
            public float? HorizontalResolution { get; set; }

            /// <summary>
            /// 缩略图的最大垂直分辨率（以“像素/英寸”为单位）。设置为 null 时表示使用不超过原图分辨率的最大值。
            /// </summary>
            public float? VerticalResolution { get; set; }

        }

        /// <summary>
        /// 定义图像格式。
        /// </summary>
        public enum ImageFormat
        {
            /// <summary>
            /// SVGZ 图像格式。
            /// </summary>
            Svgz,
            /// <summary>
            /// SVG 图像格式。
            /// </summary>
            Svg,
            /// <summary>
            /// WEBP 图像格式。
            /// </summary>
            Webp,
            /// <summary>
            /// PNG 图像格式。
            /// </summary>
            Png,
            /// <summary>
            /// AVIF 图像格式。
            /// </summary>
            Avif,
        }

        /// <summary>
        /// 将指定的图像尺寸计算为不大于指定的缩略图最大尺寸的等比例缩略图尺寸。（返回值不会大于缩略图最大尺寸）
        /// </summary>
        /// <param name="imageWidth">原图像宽度</param>
        /// <param name="imageHeight">原图像高度</param>
        /// <param name="thumbnailWidth">缩略图宽度。取值为 0 时表示忽略此参数。</param>
        /// <param name="thumbnailHeight">缩略图高度。取值为 0 时表示忽略此参数。</param>
        /// <param name="lockSmallImage">锁定小尺寸图像。如果此项设置为 true，当原图像尺寸小于缩略图限制尺寸时返回原图像尺寸。</param>
        /// <returns>缩略图在图像框中的位置和缩略图的尺寸</returns>
        public static System.Drawing.RectangleF GetThumbnailImageSize(float imageWidth, float imageHeight, float thumbnailWidth, float thumbnailHeight, bool lockSmallImage)
        {
            const int defaultValue = 0;
            System.Drawing.RectangleF result = new(0, 0, imageWidth, imageHeight);

            #region 计算缩略图尺寸。
            if (thumbnailWidth != defaultValue && thumbnailHeight != defaultValue) //如果同时指定了宽度和高度
            {
                if (lockSmallImage == false || imageWidth > thumbnailWidth || imageHeight > thumbnailHeight) //如果需要缩小原始图像或者允许放大小图
                {
                    if (imageWidth / imageHeight < thumbnailWidth / thumbnailHeight) //根据宽高比确定先计算高度或宽度
                    {
                        result.Height = thumbnailHeight;
                        result.Width = imageWidth * thumbnailHeight / imageHeight;
                    }
                    else
                    {
                        result.Width = thumbnailWidth;
                        result.Height = imageHeight * thumbnailWidth / imageWidth;
                    }
                }
            }
            else if (thumbnailWidth != defaultValue) //如果仅仅指定了宽度
            {
                if (lockSmallImage == false || imageWidth > thumbnailWidth) //如果需要缩小原始图像或者允许放大小图
                {
                    result.Width = thumbnailWidth;
                    result.Height = imageHeight * thumbnailWidth / imageWidth; //保持高度等比例。
                }
            }
            else if (thumbnailHeight != defaultValue) //如果仅仅指定了高度
            {
                if (lockSmallImage == false || imageHeight > thumbnailHeight) //如果需要缩小原始图像或者允许放大小图
                {
                    result.Height = thumbnailHeight;
                    result.Width = imageWidth * thumbnailHeight / imageHeight; //保持宽度等比例。
                }
            }
            #endregion

            #region 计算缩略图在图像框中的位置。
            if (result.Width < thumbnailWidth && thumbnailWidth != defaultValue) //如果指定了缩略图宽度并且输出的图像宽度小于缩略图
            {
                result.X = (thumbnailWidth - result.Width) / 2;
            }
            if (result.Height < thumbnailHeight && thumbnailHeight != defaultValue)
            {
                result.Y = (thumbnailHeight - result.Height) / 2;
            }
            #endregion

            return result;
        }

        #region 获取图片尺寸。
        /// <summary>
        /// 获取位图图片尺寸。
        /// </summary>
        /// <param name="imageStream">位图图像数据流。</param>
        /// <returns>图片尺寸。</returns>
        /// <exception cref="System.ArgumentNullException">参数“<paramref name="imageStream"/>”为 null。</exception>
        /// <exception cref="System.ArgumentException">无效的图像数据流。</exception>
        /// <remarks>
        /// 优先使用 ImageMagick 组件获取图像文件摘要信息，因为效率较高。
        /// </remarks>
        public static System.Drawing.Size GetImageSize(System.IO.Stream imageStream)
        {
            try
            {
                ImageMagick.MagickImageInfo tempImage = new ImageMagick.MagickImageInfo(imageStream);
                if (tempImage != null)
                {
                    return new System.Drawing.Size(tempImage.Width, tempImage.Height);
                }
            }
            catch (ImageMagick.MagickMissingDelegateErrorException ex)
            {
                if (ex.HResult != -2146233088) //当无法识别指定的图像类型时异常码为 -2146233088
                {
                    throw;
                }
            }

            try
            {
                imageStream.Seek(0, System.IO.SeekOrigin.Begin);
                var imageInfo = SixLabors.ImageSharp.Image.Identify(imageStream);
                if (imageInfo != null)
                {
                    return new System.Drawing.Size(imageInfo.Width, imageInfo.Height);
                }
            }
            catch (SixLabors.ImageSharp.UnknownImageFormatException)
            {

            }

            imageStream.Seek(0, System.IO.SeekOrigin.Begin);
            using (var inputStream = new SkiaSharp.SKManagedStream(imageStream))
            {
                using (var originalImage = SkiaSharp.SKBitmap.Decode(inputStream))
                {
                    return new System.Drawing.Size(originalImage.Width, originalImage.Height);
                }
            }

            throw new System.NotSupportedException("未能识别为有效的图像。");

            //         byte[] headerBytes = new byte[12];
            //         byte bHmsb;
            //         byte bHlsb;
            //         byte bWmsb;
            //         byte bWlsb;

            //         System.IO.BinaryReader br = new(imageStream);
            //         //iFN = FreeFile
            //         //Open sFileName For Binary As iFN
            //         int len = imageStream.Read(headerBytes, 0, headerBytes.Length);//Get #iFN, 1, headerBytes()
            //         if (len != headerBytes.Length)
            //         {
            //             throw new System.ArgumentException("无效的图像数据流。", nameof(imageStream));
            //         }

            //         if (headerBytes[0] == 0x89 && headerBytes[1] == 0x50 && headerBytes[2] == 0x4E && headerBytes[3] == 0x47 && headerBytes[4] == 0x0D && headerBytes[5] == 0x0A && headerBytes[6] == 0x1A && headerBytes[7] == 0x0A) // 如果是 PNG 格式
            //         {
            //             //Get #iFN, 19, bWmsb
            //             //Get #iFN, 20, bWlsb
            //             //Get #iFN, 23, bHmsb
            //             //Get #iFN, 24, bHlsb
            //             //imageSize.Width = CombineBytes(bWlsb, bWmsb)
            //             //imageSize.Height = CombineBytes(bHlsb, bHmsb)

            //             imageStream.Seek(18, System.IO.SeekOrigin.Begin);
            //             bWmsb = br.ReadByte();
            //             bWlsb = br.ReadByte();
            //             imageStream.Seek(22, System.IO.SeekOrigin.Begin);
            //             bHmsb = br.ReadByte();
            //             bHlsb = br.ReadByte();
            //             return new System.Drawing.Size(CombineBytes(bWlsb, bWmsb), CombineBytes(bHlsb, bHmsb));
            //         }

            //         //else if (headerBytes[0] == 0x52 && headerBytes[1] == 0x49 && headerBytes[2] == 0x46 && headerBytes[3] == 0x46 &&
            //         //	headerBytes[8] == 0x57 && headerBytes[9] == 0x45 && headerBytes[10] == 0x42 && headerBytes[11] == 0x50
            //         //	) // 如果是 WEBP 格式。
            //         //{
            //         //	imageStream.Seek(12, System.IO.SeekOrigin.Begin);
            //         //	byte[] vp8Chunk = br.ReadBytes(4);
            //         //	if (vp8Chunk[0] == 0x56 && vp8Chunk[1] == 0x50 && vp8Chunk[2] == 0x38)
            //         //	{
            //         //		if (vp8Chunk[3] == 0x20) //“VP8”文本块
            //         //		{
            //         //			imageStream.Seek(26, System.IO.SeekOrigin.Begin);
            //         //			bWlsb = br.ReadByte();
            //         //			bWmsb = br.ReadByte();
            //         //			bHlsb = br.ReadByte();
            //         //			bHmsb = br.ReadByte();
            //         //			return new System.Drawing.Size(CombineBytes(bWlsb, bWmsb), CombineBytes(bHlsb, bHmsb));
            //         //		}
            //         //		//else if (vp8Chunk[3] == 0x4C) //“VP8L”文本块
            //         //		//{
            //         //		//	imageStream.Seek(21, System.IO.SeekOrigin.Begin);
            //         //		//	byte[] wh = br.ReadBytes(4);
            //         //		//	int width = wh[0] << 6 | wh[1] >> 2;
            //         //		//	width = ReverseBits(width, 14);
            //         //		//	int height = (wh[1] & 0x3) << 12 | wh[2] << 4 | wh[3] >> 4;
            //         //		//	height = ReverseBits(height, 14);

            //         //		//	return new System.Drawing.Size(width + 1, height + 1);
            //         //		//}
            //         //		else if (vp8Chunk[3] == 0x58) //“VP8X”文本块
            //         //		{
            //         //			imageStream.Seek(24, System.IO.SeekOrigin.Begin);
            //         //			bWlsb = br.ReadByte();
            //         //			bWmsb = br.ReadByte();
            //         //			byte bWhsb = br.ReadByte();

            //         //			bHlsb = br.ReadByte();
            //         //			bHmsb = br.ReadByte();
            //         //			byte bHhsb = br.ReadByte();
            //         //			return new System.Drawing.Size(CombineBytes(bWlsb, bWmsb, bWhsb) + 1, CombineBytes(bHlsb, bHmsb, bHhsb) + 1);
            //         //		}
            //         //	}
            //         //}

            //         else if (headerBytes[0] == 0x47 && headerBytes[1] == 0x49 && headerBytes[2] == 0x46 && headerBytes[3] == 0x38) // 如果是 GIF 格式。
            //         {
            //             //Get #iFN, 7, bWlsb
            //             //Get #iFN, 8, bWmsb
            //             //Get #iFN, 9, bHlsb
            //             //Get #iFN, 10, bHmsb
            //             //imageSize.Width = CombineBytes(bWlsb, bWmsb)
            //             //imageSize.Height = CombineBytes(bHlsb, bHmsb)

            //             imageStream.Seek(6, System.IO.SeekOrigin.Begin);
            //             bWlsb = br.ReadByte();
            //             bWmsb = br.ReadByte();
            //             bHlsb = br.ReadByte();
            //             bHmsb = br.ReadByte();
            //             return new System.Drawing.Size(CombineBytes(bWlsb, bWmsb), CombineBytes(bHlsb, bHmsb));
            //         }

            //         else if (headerBytes[0] == 0xFF && headerBytes[1] == 0xD8 && headerBytes[2] == 0xFF) // 如果是 JPEG 格式
            //         {
            //             long lFlen = imageStream.Length;
            //             long lPos;
            //             byte[] bBuf = new byte[8];
            //             byte bDone = 0;
            //             int iCount;
            //             //Debug.Print "JPEG"
            //             //    lPos = 3
            //             //    Do
            //             //        Do
            //             //            Get #iFN, lPos, bBuf(1)
            //             //            Get #iFN, lPos + 1, bBuf(2)
            //             //            lPos = lPos + 1
            //             //        Loop Until (bBuf(1) = 0xFF && bBuf(2) <> 0xFF) Or lPos > lFlen

            //             //        For iCount = 0 To 7
            //             //            Get #iFN, lPos + iCount, bBuf(iCount)
            //             //        Next iCount
            //             //        If bBuf(0) >= 0xC0 && bBuf(0) <= 0xC3 Then
            //             //            bHmsb = bBuf(4)
            //             //            bHlsb = bBuf(5)
            //             //            bWmsb = bBuf(6)
            //             //            bWlsb = bBuf(7)
            //             //            bDone = 1
            //             //        Else
            //             //            lPos = lPos + (CombineBytes(bBuf(2), bBuf(1))) + 1
            //             //        End If
            //             //    Loop While lPos < lFlen && bDone = 0
            //             //    imageSize.Width = CombineBytes(bWlsb, bWmsb)
            //             //    imageSize.Height = CombineBytes(bHlsb, bHmsb)

            //             bWlsb = 0;
            //             bWmsb = 0;
            //             bHlsb = 0;
            //             bHmsb = 0;

            //             lPos = 2;
            //             do
            //             {
            //                 do
            //                 {
            //                     imageStream.Seek(lPos, System.IO.SeekOrigin.Begin);
            //                     bBuf[1] = br.ReadByte();
            //                     imageStream.Seek(lPos + 1, System.IO.SeekOrigin.Begin);
            //                     bBuf[2] = br.ReadByte();
            //                     lPos++;
            //                 } while ((bBuf[1] == 0xFF && bBuf[2] != 0xFF) == false && lPos < lFlen);

            //                 for (iCount = 0; iCount < bBuf.Length; iCount++)
            //                 {
            //                     imageStream.Seek(lPos + iCount, System.IO.SeekOrigin.Begin);
            //                     bBuf[iCount] = br.ReadByte();
            //                 }

            //                 if (bBuf[0] >= 0xC0 && bBuf[0] <= 0xC3)
            //                 {
            //                     bHmsb = bBuf[4];
            //                     bHlsb = bBuf[5];
            //                     bWmsb = bBuf[6];
            //                     bWlsb = bBuf[7];
            //                     bDone = 1;
            //                 }
            //                 else
            //                 {
            //                     lPos = lPos + (CombineBytes(bBuf[2], bBuf[1])) + 1;
            //                 }
            //             } while (lPos < lFlen && bDone == 0);

            //             return new System.Drawing.Size(CombineBytes(bWlsb, bWmsb), CombineBytes(bHlsb, bHmsb));
            //         }

            //         else if (headerBytes[0] == 0x42 && headerBytes[1] == 0x4D) // 如果是 BMP 格式
            //         {
            //             //Get #iFN, 19, bWlsb            
            //             //Get #iFN, 20, bWmsb            
            //             //Get #iFN, 23, bHlsb            
            //             //Get #iFN, 24, bHmsb            
            //             //imageSize.Width = CombineBytes(bWlsb, bWmsb)            
            //             //imageSize.Height = CombineBytes(bHlsb, bHmsb)        

            //             imageStream.Seek(18, System.IO.SeekOrigin.Begin);
            //             bWmsb = br.ReadByte();
            //             bWlsb = br.ReadByte();
            //             imageStream.Seek(22, System.IO.SeekOrigin.Begin);
            //             bHmsb = br.ReadByte();
            //             bHlsb = br.ReadByte();
            //             return new System.Drawing.Size(CombineBytes(bWmsb, bWlsb), CombineBytes(bHmsb, bHlsb));
            //         }

            //         else if (headerBytes[0] == 0x00 && headerBytes[1] == 0x00 && headerBytes[2] == 0x01 && headerBytes[3] == 0x00) // 如果是 ICO 格式。只有 SkiaSharp 组件支持 ICO 格式。
            //         {
            //             imageStream.Seek(0, System.IO.SeekOrigin.Begin);
            //             using (var inputStream = new SkiaSharp.SKManagedStream(imageStream))
            //             {
            //                 using (var originalImage = SkiaSharp.SKBitmap.Decode(inputStream))
            //                 {
            //                     return new System.Drawing.Size(originalImage.Width, originalImage.Height);
            //                 }
            //             }
            //         }
            //         else if (headerBytes[0] == 0x0 && headerBytes[1] == 0x0 && headerBytes[2] == 0x0 && (headerBytes[3] == 0x1c || headerBytes[3] == 0x20)
            //             && headerBytes[4] == 0x66 && headerBytes[5] == 0x74 && headerBytes[6] == 0x79 && headerBytes[7] == 0x70 && headerBytes[8] == 0x61 && headerBytes[9] == 0x76 && headerBytes[10] == 0x69 && headerBytes[11] == 0x66) // 如果是 AVIF 格式
            //         {//20 61 76 69
            //             imageStream.Seek(0, System.IO.SeekOrigin.Begin);
            //             var tempImage = new ImageMagick.MagickImageInfo(imageStream);
            //             return new System.Drawing.Size(tempImage.Width, tempImage.Height);
            //         }

            //imageStream.Seek(0, System.IO.SeekOrigin.Begin);
            //         //using (var originalImage = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(imageStream))
            //         //{
            //         //    return new System.Drawing.Size(originalImage.Width, originalImage.Height);
            //         //}
            //         var imageInfo = SixLabors.ImageSharp.Image.Identify(imageStream);
            //return new System.Drawing.Size(imageInfo.Width, imageInfo.Height);
        }

        //private static int CombineBytes(byte lsb, byte msb)
        //{
        //    //return System.Convert.ToInt32(lsb) + System.Convert.ToInt32(msb) * 256;
        //    return System.Convert.ToInt32(msb) << 8 | lsb;
        //}

        //private static int CombineBytes(byte lsb, byte msb, byte hsb)
        //{
        //    return System.Convert.ToInt32(hsb) << 16 | System.Convert.ToInt32(msb) << 8 | lsb;
        //}

        ///// <summary>
        ///// 颠倒高低位顺序。
        ///// </summary>
        ///// <param name="b"></param>
        ///// <returns></returns>
        //private static int ReverseBits(int b, int bitLength)
        //{
        //    int result = 0;
        //    for (int i = 0; i < bitLength; i++)
        //    {
        //        result = (result << i) | (b >> i) & 1;
        //    }
        //    return result;
        //}
        #endregion



        /// <summary>
        /// 原始位图图像。
        /// </summary>
        private SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>? SLOriginalImage = null;

        /// <summary>
        /// 已调整大小后的位图图像。
        /// </summary>
        private SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>? ResizedImage = null;

        /// <summary>
        /// SVGZ格式的图像数据。
        /// </summary>
        private System.IO.Stream? SvgzStream = null;
        /// <summary>
        /// 指示是否应该关闭并释放 <see cref="SvgzStream"/> 对象占用的资源。
        /// </summary>
        private bool CloseSvgzStream = false;

        /// <summary>
        /// SVG格式的图像数据。
        /// </summary>
        private System.IO.Stream? SvgStream = null;
        /// <summary>
        /// 指示是否应该关闭并释放 <see cref="SvgStream"/> 对象占用的资源。
        /// </summary>
        private bool CloseSvgStream = false;

        /// <summary>
        /// SVG 格式的原始图像。
        /// </summary>
        private Svg.Skia.SKSvg? SvgOriginalImage = null;

        /// <summary>
        /// 加载指定的位图数据。
        /// </summary>
        /// <param name="imageStream">位图图像数据流。</param>
        /// </param>
        /// <exception cref="System.ArgumentNullException">参数“<paramref name="imageStream"/>”为 null。</exception>
        /// <exception cref="System.ArgumentException">无效的图像数据流。</exception>
        /// <remarks>
        /// 图像转换过程优先使用 ImageSharp 组件。当 ImageSharp 组件不支持指定的图像类型时，使用 SkiaSharp 与 ImageMagick 组件进行辅助转换。
        /// 各组件对比：
        /// ImageSharp 的优点是与其他的组件相比，在相同的约束条件下，生成的缩略图大小更小，清晰度更高，跨平台兼容性更好。缺点是支持的图片格式较少（截止到2024年3月尚不持支 avif 格式与 ico 格式）。
        /// SkiaSharp 是以 Google的Skia图形库为基础进行的 .NET 包装 API，维护团队强大（微软公司），支持 ico 格式，缺点是截止到2024年3月尚不持支 avif 格式与 tiff 格式，考虑到 Google 主推 webp 格式，所以估计以后也不会支持 avif。
        /// ImageMagick 的优点是支持的图像格式更多，缺点是加载效率缓慢，占用内存大，跨平台兼容性欠佳。
        /// 已知的问题：无论是 ImageSharp 又或者是 SkiaSharp 组件，当原始图像为 CMYK 颜色模式时，存在转换后颜色失真的问题（红色系尤为明显）。
        /// </remarks>
        public void LoadImage(System.IO.Stream imageStream)
        {
			if (imageStream == null)
            {
                throw new System.ArgumentNullException(nameof(imageStream));
            }

            this.ClearResource();

            try
            {
                this.SLOriginalImage = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(imageStream);
                while (this.SLOriginalImage.Frames.Count > 1) //如果是动画，则仅保留第一帧图像
                {
                    this.SLOriginalImage.Frames.RemoveFrame(1);
                }
                return;
            }
            catch (SixLabors.ImageSharp.UnknownImageFormatException)
            {
            }

            imageStream.Seek(0, System.IO.SeekOrigin.Begin);
            try
            {
                using (System.IO.MemoryStream webpStream = new())
                {
                    using (var tempImage = new ImageMagick.MagickImage(imageStream))
                    {
                        tempImage.Quality = 100;
                        tempImage.Write(webpStream, ImageMagick.MagickFormat.WebP);
                    }
                    webpStream.Seek(0, System.IO.SeekOrigin.Begin);
                    this.SLOriginalImage = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(webpStream);
                    while (this.SLOriginalImage.Frames.Count > 1) //如果是动画，则仅保留第一帧图像
                    {
                        this.SLOriginalImage.Frames.RemoveFrame(1);
                    }
                    return;
                }
            }
            catch (ImageMagick.MagickMissingDelegateErrorException ex)
            {
                if (ex.HResult != -2146233088) //当无法识别指定的图像类型时异常码为 -2146233088
                {
                    throw;
                }
            }

            imageStream.Seek(0, System.IO.SeekOrigin.Begin);
            using (System.IO.MemoryStream webpStream = new())
            {
                #region 将 ICO 图像转换为 Webp 格式。
                using (var inputStream = new SkiaSharp.SKManagedStream(imageStream))
                {
                    using (var originalImage = SkiaSharp.SKBitmap.Decode(inputStream))
                    {
                        using (var skdata = originalImage.Encode(SkiaSharp.SKEncodedImageFormat.Webp, 100))
                        {
                            //skdata.AsSpan().ToArray();
                            skdata.SaveTo(webpStream);
                        }
                    }
                }
                #endregion

                webpStream.Seek(0, System.IO.SeekOrigin.Begin);
                this.SLOriginalImage = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(webpStream);
                while (this.SLOriginalImage.Frames.Count > 1) //如果是动画，则仅保留第一帧图像
                {
                    this.SLOriginalImage.Frames.RemoveFrame(1);
                }
                return;
            }

            throw new System.ArgumentException("无效的图像数据流。", nameof(imageStream));

            //byte[] headerBytes = new byte[12];
            //         int len = imageStream.Read(headerBytes, 0, headerBytes.Length);
            //         imageStream.Seek(0, System.IO.SeekOrigin.Begin);
            //         if (len == headerBytes.Length)
            //         {
            //             this.ClearResource();
            //             if (headerBytes[0] == 0x00 && headerBytes[1] == 0x00 && headerBytes[2] == 0x01 && headerBytes[3] == 0x00) // 如果是 ICO 格式。只有 SkiaSharp 组件支持 ICO 格式。
            //             {
            //                 using (System.IO.MemoryStream webpStream = new())
            //                 {
            //                     #region 将 ICO 图像转换为 Webp 格式。
            //                     using (var inputStream = new SkiaSharp.SKManagedStream(imageStream))
            //                     {
            //                         using (var originalImage = SkiaSharp.SKBitmap.Decode(inputStream))
            //                         {
            //                             using (var skdata = originalImage.Encode(SkiaSharp.SKEncodedImageFormat.Webp, 100))
            //                             {
            //                                 //skdata.AsSpan().ToArray();
            //                                 skdata.SaveTo(webpStream);
            //                             }
            //                         }
            //                     }
            //                     #endregion

            //                     webpStream.Seek(0, System.IO.SeekOrigin.Begin);
            //                     this.SLOriginalImage = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(webpStream);
            //                     return;
            //                 }
            //             }
            //             //else if (headerBytes[0] == 0x47 && headerBytes[1] == 0x49 && headerBytes[2] == 0x46 && headerBytes[3] == 0x38) // 如果是 GIF 格式。只有 SixLabors.ImageSharp 组件支持 GIF 格式的动图缩略转换。（SkiaSharp 组件转出来的会变成静态图）
            //             //{
            //             //    this.SLOriginalImage = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(imageStream);
            //             //    while (this.SLOriginalImage.Frames.Count > 1)
            //             //    {
            //             //        this.SLOriginalImage.Frames.RemoveFrame(1);
            //             //    }
            //             //    return;
            //             //}
            //             //else if (headerBytes[0] == 0x0 && headerBytes[1] == 0x0 && headerBytes[2] == 0x0 && (headerBytes[3] == 0x1c || headerBytes[3] == 0x20)
            //             //    && headerBytes[4] == 0x66 && headerBytes[5] == 0x74 && headerBytes[6] == 0x79 && headerBytes[7] == 0x70 && headerBytes[8] == 0x61 && headerBytes[9] == 0x76 && headerBytes[10] == 0x69 && headerBytes[11] == 0x66) // 如果是 AVIF 格式
            //             //{//20 61 76 69
            //             //    using (System.IO.MemoryStream webpStream = new())
            //             //    {
            //             //        using (var tempImage = new ImageMagick.MagickImage(imageStream, ImageMagick.MagickFormat.Avif))
            //             //        {
            //             //            tempImage.Quality = 100;
            //             //            tempImage.Write(webpStream, ImageMagick.MagickFormat.WebP);
            //             //        }
            //             //        webpStream.Seek(0, System.IO.SeekOrigin.Begin);
            //             //        this.SLOriginalImage = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(webpStream);
            //             //        return;
            //             //    }
            //             //}
            //             //else if (headerBytes[0] == 0x49 && headerBytes[1] == 0x49 && headerBytes[2] == 0x2A && headerBytes[3] == 0x00) // 判断图像类型 JPG JPEG PNG BMP 编码方式是 TIFF 格式的。只有 SixLabors.ImageSharp 组件支持 TIFF 格式。
            //             //{
            //             //}
            //             //else if (headerBytes[0] == 0x89 && headerBytes[1] == 0x50 && headerBytes[2] == 0x4E && headerBytes[3] == 0x47 && headerBytes[4] == 0x0D && headerBytes[5] == 0x0A && headerBytes[6] == 0x1A && headerBytes[7] == 0x0A) // 如果是 PNG 格式
            //             //{
            //             //}
            //             //else if (headerBytes[0] == 0xFF && headerBytes[1] == 0xD8 && headerBytes[2] == 0xFF) // 如果是 JPEG 格式
            //             //{
            //             //}
            //             //else if (headerBytes[0] == 0x42 && headerBytes[1] == 0x4D) // 如果是 BMP 格式
            //             //{
            //             //}
            //             else
            //             {
            //                 try
            //                 {
            //                     this.SLOriginalImage = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(imageStream);
            //                     return;
            //                 }
            //                 catch (SixLabors.ImageSharp.UnknownImageFormatException)
            //                 {
            //			imageStream.Seek(0, System.IO.SeekOrigin.Begin);
            //			using (System.IO.MemoryStream webpStream = new())
            //                     {
            //                         using (var tempImage = new ImageMagick.MagickImage(imageStream))
            //                         {
            //                             tempImage.Quality = 100;
            //                             tempImage.Write(webpStream, ImageMagick.MagickFormat.WebP);
            //                         }
            //                         webpStream.Seek(0, System.IO.SeekOrigin.Begin);
            //                         this.SLOriginalImage = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(webpStream);
            //                         return;
            //                     }
            //                 }
            //	}
            //         }
        }

        /// <summary>
        /// 加载指定的 SVG 图像数据。
        /// </summary>
        /// <param name="svgStream">SVG 格式的图像数据。</param>
        /// <exception cref="System.ArgumentNullException">参数“<paramref name="svgStream"/>”为 null。</exception>
        public void LoadSvg(System.IO.Stream svgStream)
        {
            if (svgStream == null)
            {
                throw new System.ArgumentNullException(nameof(svgStream));
            }

            this.ClearResource();
            #region 获取 SVGZ 压缩格式数据。
            this.SvgzStream = new System.IO.MemoryStream();
            this.CloseSvgzStream = true;
            //using (System.IO.Compression.GZipStream zipStream = new(this.svgzStream, System.IO.Compression.CompressionLevel.Optimal))   //使用 GZip 算法压缩 SVG 文件流
            using (System.IO.Compression.GZipStream zipStream = new(this.SvgzStream, System.IO.Compression.CompressionLevel.SmallestSize, true))   //使用 GZip 算法压缩 SVG 文件流
            {
                //var svgBytes = new byte[svgStream.Length];
                //svgStream.Read(svgBytes, 0, svgBytes.Length); //获取 SVG 格式数据。
                //zipStream.Write(svgBytes, 0, svgBytes.Length);  //写入压缩文件

                svgStream.CopyTo(zipStream);
            }
            #endregion

            #region 装载 SVG Image。
            svgStream.Seek(0, System.IO.SeekOrigin.Begin);
            this.SvgOriginalImage = new Svg.Skia.SKSvg();
            this.SvgOriginalImage.Load(svgStream);
            #endregion

            this.SvgStream = svgStream;
        }

        /// <summary>
        /// 加载指定的 SVGZ 图像数据。
        /// </summary>
        /// <param name="svgzStream">SVGZ 格式的图像数据。</param>
        /// <exception cref="System.ArgumentNullException">参数“<paramref name="svgzStream"/>”为 null。</exception>
        public void LoadSvgz(System.IO.Stream svgzStream)
        {
            if (svgzStream == null)
            {
                throw new System.ArgumentNullException(nameof(svgzStream));
            }

            this.ClearResource();
            #region 获取 SVG 格式数据。
            this.SvgStream = new System.IO.MemoryStream();
            this.CloseSvgStream = true;
            using (System.IO.Compression.GZipStream zipStream = new(svgzStream, System.IO.Compression.CompressionMode.Decompress, true))   //使用 GZip 算法解压缩 SVGZ 文件流
            {
                zipStream.CopyTo(this.SvgStream);  //读取原始数据
            }
            #endregion

            #region 装载 SVG Image。
            this.SvgStream.Seek(0, System.IO.SeekOrigin.Begin);
            this.SvgOriginalImage = new Svg.Skia.SKSvg();
            this.SvgOriginalImage.Load(this.SvgStream);
            #endregion

            this.SvgzStream = svgzStream;
        }

        /// <summary>
        /// 将以英寸为单位的值转换为指定的单位。
        /// </summary>
        /// <param name="valueInch">待转换的值（单位：英寸）</param>
        /// <param name="toUnit">目标单位。</param>
        /// <returns>以指定单位为目标的值。</returns>
        private static double InchConvertTo(double valueInch, SixLabors.ImageSharp.Metadata.PixelResolutionUnit toUnit)
        {
            const double CentimeterPerInch = 2.54; // 厘米/英寸。（1英寸=2.54厘米）
            const double InchPerMeter = 39.37; // 英寸/米。（1米=100/2.54英寸，约等于39.37英寸）

            switch (toUnit)
            {
                case SixLabors.ImageSharp.Metadata.PixelResolutionUnit.PixelsPerInch:
                    return valueInch;
                case SixLabors.ImageSharp.Metadata.PixelResolutionUnit.PixelsPerCentimeter:
                    return valueInch / CentimeterPerInch;
                case SixLabors.ImageSharp.Metadata.PixelResolutionUnit.PixelsPerMeter:
                    return valueInch * InchPerMeter;
                case SixLabors.ImageSharp.Metadata.PixelResolutionUnit.AspectRatio:
                default:
                    return valueInch;
            }
        }

        /// <summary>
        /// 将指定单位的值转换为以英寸为单位的值。
        /// </summary>
        /// <param name="value">带转换的值。</param>
        /// <param name="valueUnit">参数 <paramref name="value"/> 的单位。</param>
        /// <returns>以英寸为单位的值。</returns>
        private static double InchConvertFrom(double value, SixLabors.ImageSharp.Metadata.PixelResolutionUnit valueUnit)
        {
            const double CentimeterPerInch = 2.54; // 厘米/英寸。（1英寸=2.54厘米）
            const double InchPerMeter = 39.37; // 英寸/米。（1米=100/2.54英寸，约等于39.37英寸）

            switch (valueUnit)
            {
                case SixLabors.ImageSharp.Metadata.PixelResolutionUnit.PixelsPerInch:
                    return value;
                case SixLabors.ImageSharp.Metadata.PixelResolutionUnit.PixelsPerCentimeter:
                    return value * CentimeterPerInch;
                case SixLabors.ImageSharp.Metadata.PixelResolutionUnit.PixelsPerMeter:
                    return value / InchPerMeter;
                case SixLabors.ImageSharp.Metadata.PixelResolutionUnit.AspectRatio:
                default:
                    return value;
            }
        }

        /// <summary>
        /// 获取指定图像的等比例缩略图。（使用 ImageSharp 组件）
        /// </summary>
        /// <param name="imageStream">图像数据流。</param>
        /// <param name="resizeOptions">缩略图生成规则。</param>
        /// <returns>一个 <see cref="SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>"/> 实例。</returns>
        private static SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32> GetThumbnailImageSL(SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32> originalImage, ResizeOptions resizeOptions)
        {
            #region 调整 DPI。注意：一定要在执行缩放之前调整 DPI，因为调整 DPI 的时候图像尺寸可能会被调整。
            if (resizeOptions.HorizontalResolution != null && InchConvertFrom(originalImage.Metadata.HorizontalResolution, originalImage.Metadata.ResolutionUnits) > resizeOptions.HorizontalResolution.Value)
            {
                originalImage.Metadata.HorizontalResolution = InchConvertTo(resizeOptions.HorizontalResolution.Value, originalImage.Metadata.ResolutionUnits);
            }
            if (resizeOptions.VerticalResolution != null && InchConvertFrom(originalImage.Metadata.VerticalResolution, originalImage.Metadata.ResolutionUnits) > resizeOptions.VerticalResolution.Value)
            {
                originalImage.Metadata.VerticalResolution = InchConvertTo(resizeOptions.VerticalResolution.Value, originalImage.Metadata.ResolutionUnits);
            }
            #endregion

            #region 计算缩略图输出区域。
            int thumbnailWidth; //缩略图最大宽度。
            int thumbnailHeight; //缩略图最大高度。

            System.Drawing.RectangleF backgroundRectangle;
            switch (resizeOptions.Mode)
            {
                case ResizeMode.Pad:
                    backgroundRectangle = GetThumbnailImageSize(originalImage.Width, originalImage.Height, resizeOptions.Size.Width, resizeOptions.Size.Height, false);
                    thumbnailWidth = resizeOptions.Size.Width == 0 ? (int)System.Math.Floor(backgroundRectangle.Right) : resizeOptions.Size.Width;
                    thumbnailHeight = resizeOptions.Size.Height == 0 ? (int)System.Math.Floor(backgroundRectangle.Bottom) : resizeOptions.Size.Height;
                    break;
                case ResizeMode.BoxPad:
                    backgroundRectangle = GetThumbnailImageSize(originalImage.Width, originalImage.Height, resizeOptions.Size.Width, resizeOptions.Size.Height, true);
                    thumbnailWidth = resizeOptions.Size.Width == 0 ? (int)System.Math.Floor(backgroundRectangle.Right) : resizeOptions.Size.Width;
                    thumbnailHeight = resizeOptions.Size.Height == 0 ? (int)System.Math.Floor(backgroundRectangle.Bottom) : resizeOptions.Size.Height;
                    break;
                case ResizeMode.PadWithoutPad:
                    backgroundRectangle = GetThumbnailImageSize(originalImage.Width, originalImage.Height, resizeOptions.Size.Width, resizeOptions.Size.Height, false);
                    backgroundRectangle.X = 0;
                    backgroundRectangle.Y = 0;
                    thumbnailWidth = (int)System.Math.Floor(backgroundRectangle.Right);
                    thumbnailHeight = (int)System.Math.Floor(backgroundRectangle.Bottom);
                    break;
                case ResizeMode.BoxWithoutPad:
                    backgroundRectangle = GetThumbnailImageSize(originalImage.Width, originalImage.Height, resizeOptions.Size.Width, resizeOptions.Size.Height, true);
                    backgroundRectangle.X = 0;
                    backgroundRectangle.Y = 0;
                    thumbnailWidth = (int)System.Math.Floor(backgroundRectangle.Right);
                    thumbnailHeight = (int)System.Math.Floor(backgroundRectangle.Bottom);
                    break;
                case ResizeMode.Stretch:
                    thumbnailWidth = resizeOptions.Size.Width == 0 ? originalImage.Width : resizeOptions.Size.Width;
                    thumbnailHeight = resizeOptions.Size.Height == 0 ? originalImage.Height : resizeOptions.Size.Height;
                    backgroundRectangle = new System.Drawing.RectangleF(0, 0, thumbnailWidth, thumbnailHeight);
                    break;
                default:
                    throw new NotImplementedException($"未实现指定的调整模式{nameof(ResizeMode)}={resizeOptions.Mode}");
            }
            #endregion

            #region 绘制缩略图。
            var forgroundSize = new SixLabors.ImageSharp.Size((int)System.Math.Floor(backgroundRectangle.Width), (int)System.Math.Floor(backgroundRectangle.Height));
            if (forgroundSize.Width != originalImage.Width || forgroundSize.Height != originalImage.Height) //如果缩略图尺寸与原始图像的尺寸不相等，则需要对原图进行缩放。
            {
                //originalImage.Mutate(x => x.Resize(new SixLabors.ImageSharp.Processing.ResizeOptions()
                //{
                //	Size = forgroundSize,
                //	Mode = SixLabors.ImageSharp.Processing.ResizeMode.Stretch,
                //}));
                originalImage.Mutate(x => x.Resize(forgroundSize));
            }

            var padColor = SixLabors.ImageSharp.Color.FromRgba(resizeOptions.PadColor.R, resizeOptions.PadColor.G, resizeOptions.PadColor.B, resizeOptions.PadColor.A);
            var copy = new SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>(thumbnailWidth, thumbnailHeight, padColor);
            copy.Metadata.ResolutionUnits = originalImage.Metadata.ResolutionUnits;
            copy.Metadata.HorizontalResolution = originalImage.Metadata.HorizontalResolution;
            copy.Metadata.VerticalResolution = originalImage.Metadata.VerticalResolution;


            copy.Mutate(x => x.DrawImage(originalImage, new SixLabors.ImageSharp.Point((int)System.Math.Floor(backgroundRectangle.X), (int)System.Math.Floor(backgroundRectangle.Y)), 1F));
            return copy;
            #endregion
        }

        /// <summary>
        /// 获取指定图像的等比例缩略图。（使用 SkiaSharp 组件）
        /// </summary>
        /// <param name="imageStream">图像数据流。</param>
        /// <param name="resizeOptions">缩略图生成规则。</param>
        /// <returns>一个 <see cref="SkiaSharp.SKBitmap"/> 实例。</returns>
        private static SkiaSharp.SKBitmap GetThumbnailImageSK(System.IO.Stream imageStream, ResizeOptions resizeOptions)
        {
            using (var inputStream = new SkiaSharp.SKManagedStream(imageStream))
            {
                using (var originalImage = SkiaSharp.SKBitmap.Decode(inputStream))
                {
                    #region 计算缩略图输出区域。
                    int thumbnailWidth; //缩略图最大宽度。
                    int thumbnailHeight; //缩略图最大高度。

                    System.Drawing.RectangleF backgroundRectangle;
                    switch (resizeOptions.Mode)
                    {
                        case ResizeMode.Pad:
                            backgroundRectangle = GetThumbnailImageSize(originalImage.Width, originalImage.Height, resizeOptions.Size.Width, resizeOptions.Size.Height, false);
                            thumbnailWidth = resizeOptions.Size.Width == 0 ? (int)System.Math.Floor(backgroundRectangle.Right) : resizeOptions.Size.Width;
                            thumbnailHeight = resizeOptions.Size.Height == 0 ? (int)System.Math.Floor(backgroundRectangle.Bottom) : resizeOptions.Size.Height;
                            break;
                        case ResizeMode.BoxPad:
                            backgroundRectangle = GetThumbnailImageSize(originalImage.Width, originalImage.Height, resizeOptions.Size.Width, resizeOptions.Size.Height, true);
                            thumbnailWidth = resizeOptions.Size.Width == 0 ? (int)System.Math.Floor(backgroundRectangle.Right) : resizeOptions.Size.Width;
                            thumbnailHeight = resizeOptions.Size.Height == 0 ? (int)System.Math.Floor(backgroundRectangle.Bottom) : resizeOptions.Size.Height;
                            break;
                        case ResizeMode.PadWithoutPad:
                            backgroundRectangle = GetThumbnailImageSize(originalImage.Width, originalImage.Height, resizeOptions.Size.Width, resizeOptions.Size.Height, false);
                            backgroundRectangle.X = 0;
                            backgroundRectangle.Y = 0;
                            thumbnailWidth = (int)System.Math.Floor(backgroundRectangle.Right);
                            thumbnailHeight = (int)System.Math.Floor(backgroundRectangle.Bottom);
                            break;
                        case ResizeMode.BoxWithoutPad:
                            backgroundRectangle = GetThumbnailImageSize(originalImage.Width, originalImage.Height, resizeOptions.Size.Width, resizeOptions.Size.Height, true);
                            backgroundRectangle.X = 0;
                            backgroundRectangle.Y = 0;
                            thumbnailWidth = (int)System.Math.Floor(backgroundRectangle.Right);
                            thumbnailHeight = (int)System.Math.Floor(backgroundRectangle.Bottom);
                            break;
                        case ResizeMode.Stretch:
                            thumbnailWidth = resizeOptions.Size.Width == 0 ? originalImage.Width : resizeOptions.Size.Width;
                            thumbnailHeight = resizeOptions.Size.Height == 0 ? originalImage.Height : resizeOptions.Size.Height;
                            backgroundRectangle = new System.Drawing.RectangleF(0, 0, thumbnailWidth, thumbnailHeight);
                            break;
                        default:
                            throw new NotImplementedException($"未实现指定的调整模式{nameof(ResizeMode)}={resizeOptions.Mode}");
                    }
                    #endregion

                    #region 绘制缩略图。
                    var copy = new SkiaSharp.SKBitmap(thumbnailWidth, thumbnailHeight); // 如果颜色类型为Index8，则创建具有相同尺寸的新位图也可以避免第一次复制。
                    var canvas = new SkiaSharp.SKCanvas(copy); // 创建一个画布以便绘图。

                    if (resizeOptions.PadColor != System.Drawing.Color.Transparent) // 填充背景色。
                    {
                        var padColor = new SkiaSharp.SKColor(resizeOptions.PadColor.R, resizeOptions.PadColor.G, resizeOptions.PadColor.B, resizeOptions.PadColor.A);
                        canvas.Clear(padColor);
                    }

                    var rect = new SkiaSharp.SKRect(backgroundRectangle.X, backgroundRectangle.Y, backgroundRectangle.Right, backgroundRectangle.Bottom);
                    canvas.DrawBitmap(originalImage, rect);
                    return copy;
                    #endregion
                }
            }
        }

        /// <summary>
        /// 获取指定图像的等比例缩略图。（使用 ImageSharp 组件）
        /// </summary>
        /// <param name="resizeOptions">缩略图生成规则。</param>
        /// <returns>一个 <see cref="SixLabors.ImageSharp.Image<SixLabors.ImageSharp.PixelFormats.Rgba32>"/> 实例。</returns>
        public void Resize(ResizeOptions resizeOptions)
        {
            if (this.ResizedImage != null)
            {
                this.ResizedImage.Dispose();
            }
            if (this.SvgOriginalImage != null)
            {
                //var rawSize_Width = this.SvgOriginalImage.Drawable.Bounds.Width;
                //var rawSize_Height = this.SvgOriginalImage.Drawable.Bounds.Height;
                var originalSize = this.SvgOriginalImage.Picture.CullRect.Size;

                #region 计算缩略图输出区域。
                int thumbnailWidth; //缩略图最大宽度。
                int thumbnailHeight; //缩略图最大高度。

                System.Drawing.RectangleF backgroundRectangle;
                switch (resizeOptions.Mode)
                {
                    case ResizeMode.Pad:
                    case ResizeMode.BoxPad: //矢量图可高清放大，无需锁定小图。
                        backgroundRectangle = GetThumbnailImageSize(originalSize.Width, originalSize.Height, resizeOptions.Size.Width, resizeOptions.Size.Height, false);
                        thumbnailWidth = resizeOptions.Size.Width == 0 ? (int)System.Math.Floor(backgroundRectangle.Right) : resizeOptions.Size.Width;
                        thumbnailHeight = resizeOptions.Size.Height == 0 ? (int)System.Math.Floor(backgroundRectangle.Bottom) : resizeOptions.Size.Height;
                        break;
                    case ResizeMode.PadWithoutPad:
                    case ResizeMode.BoxWithoutPad: //矢量图可高清放大，无需锁定小图。
                        backgroundRectangle = GetThumbnailImageSize(originalSize.Width, originalSize.Height, resizeOptions.Size.Width, resizeOptions.Size.Height, false);
                        backgroundRectangle.X = 0;
                        backgroundRectangle.Y = 0;
                        thumbnailWidth = (int)System.Math.Floor(backgroundRectangle.Right);
                        thumbnailHeight = (int)System.Math.Floor(backgroundRectangle.Bottom);
                        break;
                    case ResizeMode.Stretch:
                        thumbnailWidth = resizeOptions.Size.Width == 0 ? (int)originalSize.Width : resizeOptions.Size.Width;
                        thumbnailHeight = resizeOptions.Size.Height == 0 ? (int)originalSize.Height : resizeOptions.Size.Height;
                        backgroundRectangle = new System.Drawing.RectangleF(0, 0, thumbnailWidth, thumbnailHeight);
                        break;
                    default:
                        throw new NotImplementedException($"未实现指定的调整模式{nameof(ResizeMode)}={resizeOptions.Mode}");
                }
                #endregion

                using (System.IO.MemoryStream msTempStream = new())
                {
                    this.SvgOriginalImage.Save(msTempStream, SkiaSharp.SKColors.Transparent, SkiaSharp.SKEncodedImageFormat.Webp, 100, backgroundRectangle.Width / originalSize.Width, backgroundRectangle.Height / originalSize.Height);

                    #region 将已经制作好的缩略图转换为合适的输出格式。
                    msTempStream.Seek(0, System.IO.SeekOrigin.Begin);
                    using (var tempImage = SixLabors.ImageSharp.Image.Load<SixLabors.ImageSharp.PixelFormats.Rgba32>(msTempStream))
                    {
                        this.ResizedImage = Thinksea.Drawing.ThumbnailImage.GetThumbnailImageSL(tempImage, new ResizeOptions()
                        {
                            //Size = resizeOptions.Size,
                            Mode = ResizeMode.Stretch,
                            PadColor = resizeOptions.PadColor,
                            HorizontalResolution = resizeOptions.HorizontalResolution,
                            VerticalResolution = resizeOptions.VerticalResolution,
                        });
                    }
                    #endregion
                }
            }
            else if (this.SLOriginalImage != null)
            {
                this.ResizedImage = Thinksea.Drawing.ThumbnailImage.GetThumbnailImageSL(this.SLOriginalImage, resizeOptions);
            }
            else
            {
                throw new System.Exception("未设置待处理的图像，请先加载图像。");
            }
        }

        /// <summary>
        /// 获取指定图像的等比例缩略图。
        /// </summary>
        /// <param name="outputStream">生成的缩略图写入此输出流。</param>
        /// <param name="outputFormat">生成的图像的格式。</param>
        /// <param name="imageQuality">生成的图像的压缩质量。介于0和100之间。对于有损，0表示最小的大小，100表示最大的大小。对于无损，此参数是压缩所需的工作量：0是最快的，但与最慢但最好的100相比，它提供了更大的文件。默认值为75。</param>
        public void Save(System.IO.Stream outputStream, ImageFormat outputFormat, int imageQuality)
        {
            if (this.ResizedImage == null)
            {
                throw new System.Exception("未设置待处理的图像，请先加载图像。");
            }

            switch (outputFormat)
            {
                case ImageFormat.Svgz:
                    if (this.SvgzStream == null)
                    {
                        throw new System.Exception("请先加载 SVGZ 或 SVG 格式的矢量图。");
                    }
                    this.SvgzStream.Seek(0, SeekOrigin.Begin);
                    this.SvgzStream.CopyTo(outputStream);
                    return;
                case ImageFormat.Svg:
                    if (this.SvgStream == null)
                    {
                        throw new System.Exception("请先加载 SVGZ 或 SVG 格式的矢量图。");
                    }
                    this.SvgStream.Seek(0, SeekOrigin.Begin);
                    this.SvgStream.CopyTo(outputStream);
                    return;
                case ImageFormat.Webp:
                    this.ResizedImage.SaveAsWebp(outputStream, new SixLabors.ImageSharp.Formats.Webp.WebpEncoder()
                    {
                        Quality = imageQuality,
                        //Method = SixLabors.ImageSharp.Formats.Webp.WebpEncodingMethod.BestQuality,
                    });
                    return;
                case ImageFormat.Png:
                    this.ResizedImage.SaveAsPng(outputStream, new SixLabors.ImageSharp.Formats.Png.PngEncoder()
                    {
                        CompressionLevel = (SixLabors.ImageSharp.Formats.Png.PngCompressionLevel)(SixLabors.ImageSharp.Formats.Png.PngCompressionLevel.BestCompression - (int)System.Math.Floor((int)SixLabors.ImageSharp.Formats.Png.PngCompressionLevel.BestCompression * imageQuality / 100.0)),
                    });
                    return;
                case ImageFormat.Avif:
                    using (System.IO.MemoryStream webpStream = new())
                    {
                        this.ResizedImage.SaveAsWebp(webpStream, new SixLabors.ImageSharp.Formats.Webp.WebpEncoder()
                        {
                            Quality = 100,
                            Method = SixLabors.ImageSharp.Formats.Webp.WebpEncodingMethod.BestQuality,
                        });
                        webpStream.Seek(0, System.IO.SeekOrigin.Begin);
                        using (var tempImage = new ImageMagick.MagickImage(webpStream, ImageMagick.MagickFormat.WebP))
                        {
                            //tempImage.Density = new ImageMagick.Density(InchConvertFrom(this.ResizedImage.Metadata.HorizontalResolution, this.ResizedImage.Metadata.ResolutionUnits), InchConvertFrom(this.ResizedImage.Metadata.VerticalResolution, this.ResizedImage.Metadata.ResolutionUnits));
                            tempImage.Quality = imageQuality;
                            tempImage.Write(outputStream, ImageMagick.MagickFormat.Avif);
                        }
                    }
                    return;
                default:
                    throw new NotImplementedException($"未实现指定的图像格式{nameof(outputFormat)}={outputFormat}");
            }
        }

        /// <summary>
        /// 释放占用的全部资源。
        /// </summary>
        private void ClearResource()
        {
            if (this.ResizedImage != null)
            {
                this.ResizedImage.Dispose();
                this.ResizedImage = null;
            }
            if (this.SLOriginalImage != null)
            {
                this.SLOriginalImage.Dispose();
                this.SLOriginalImage = null;
            }
            if (this.SvgOriginalImage != null)
            {
                this.SvgOriginalImage.Dispose();
                this.SvgOriginalImage = null;
            }
            if (this.SvgStream != null && this.CloseSvgStream)
            {
                this.SvgStream.Close();
                this.SvgStream = null;
                this.CloseSvgStream = false;
            }
            if (this.SvgzStream != null && this.CloseSvgzStream)
            {
                this.SvgzStream.Close();
                this.SvgzStream = null;
                this.CloseSvgzStream = false;
            }
        }

        public void Dispose()
        {
            this.ClearResource();
        }
    }
}
