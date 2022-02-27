﻿namespace Thinksea
{
    /// <summary>
    /// 封装了图像处理方法。
    /// </summary>
    public static class Image
    {
        /// <summary>
        /// 会产生graphics异常的PixelFormat（一般提示为“无法从带有索引像素格式的图像创建 Graphics 对象。”）
        /// </summary>
        private static readonly System.Drawing.Imaging.PixelFormat[] IndexedPixelFormats = {
                System.Drawing.Imaging.PixelFormat.Undefined,
                System.Drawing.Imaging.PixelFormat.DontCare,
                System.Drawing.Imaging.PixelFormat.Format16bppArgb1555,
                System.Drawing.Imaging.PixelFormat.Format1bppIndexed,
                System.Drawing.Imaging.PixelFormat.Format4bppIndexed,
                System.Drawing.Imaging.PixelFormat.Format8bppIndexed,
                (System.Drawing.Imaging.PixelFormat)8207
            };

        /// <summary>
        /// 判断图片的PixelFormat 是否在 引发异常的 PixelFormat 之中
        /// </summary>
        /// <param name="imgPixelFormat">原图片的PixelFormat</param>
        /// <returns></returns>
        private static bool IsPixelFormatIndexed(System.Drawing.Imaging.PixelFormat imgPixelFormat)
        {
            foreach (System.Drawing.Imaging.PixelFormat pf in Image.IndexedPixelFormats)
            {
                if (pf.Equals(imgPixelFormat)) return true;
            }

            return false;
        }

        /// <summary>
        /// 将指定的图片尺寸计算为不大于指定的缩略图最大尺寸的等比例缩略图尺寸。（返回值不会大于缩略图最大尺寸）
        /// </summary>
        /// <param name="width">原图像宽度</param>
        /// <param name="height">原图像高度</param>
        /// <param name="thumbnailWidth">缩略图最大宽度。取值为 0 时表示忽略此参数。</param>
        /// <param name="thumbnailHeight">缩略图最大高度。取值为 0 时表示忽略此参数。</param>
        /// <param name="lockSmallImage">锁定小尺寸图片。如果此项设置为 true，当原图像尺寸小于缩略图限制尺寸时返回原图像尺寸。</param>
        /// <returns>缩略图在图片框中的位置和缩略图的尺寸</returns>
        public static System.Drawing.RectangleF GetThumbnailImageSize(float width, float height, float thumbnailWidth, float thumbnailHeight, bool lockSmallImage)
        {
            int defaultSize = 0;
            System.Drawing.RectangleF Result = System.Drawing.RectangleF.Empty;

            #region 计算缩略图尺寸。
            Result.Width = width;
            Result.Height = height;
            if (thumbnailWidth != defaultSize && thumbnailHeight != defaultSize)
            {
                if (lockSmallImage == false || width > thumbnailWidth || height > thumbnailHeight)
                {
                    if (width / height < thumbnailWidth / thumbnailHeight)
                    {
                        Result.Height = thumbnailHeight;
                        Result.Width = System.Convert.ToInt32(width * thumbnailHeight / height);
                    }
                    else
                    {
                        Result.Width = thumbnailWidth;
                        Result.Height = System.Convert.ToInt32(height * thumbnailWidth / width);
                    }
                }
            }
            else if (thumbnailWidth != defaultSize)
            {
                if (lockSmallImage == false || width > thumbnailWidth)
                {
                    Result.Width = thumbnailWidth;
                    Result.Height = System.Convert.ToInt32(height * thumbnailWidth / width);
                }
            }
            else if (thumbnailHeight != defaultSize)
            {
                if (lockSmallImage == false || height > thumbnailHeight)
                {
                    Result.Height = thumbnailHeight;
                    Result.Width = System.Convert.ToInt32(width * thumbnailHeight / height);
                }
            }
            #endregion

            #region 计算缩略图尺寸。
            //Result.Width = Width;
            //Result.Height = Height;
            //if (LockSmallImage == false || Width > ThumbnailWidth || Height > ThumbnailHeight)
            //{
            //    if (ThumbnailWidth != defaultSize && ThumbnailHeight != defaultSize)
            //    {
            //        if (Width / Height < ThumbnailWidth / ThumbnailHeight)
            //        {
            //            Result.Height = ThumbnailHeight;
            //            Result.Width = System.Convert.ToInt32(Width * ThumbnailHeight / Height);
            //        }
            //        else
            //        {
            //            Result.Width = ThumbnailWidth;
            //            Result.Height = System.Convert.ToInt32(Height * ThumbnailWidth / Width);
            //        }
            //    }
            //    else if (ThumbnailWidth != defaultSize)
            //    {
            //        Result.Width = ThumbnailWidth;
            //        Result.Height = System.Convert.ToInt32(Height * ThumbnailWidth / Width);
            //    }
            //    else if (ThumbnailHeight != defaultSize)
            //    {
            //        Result.Height = ThumbnailHeight;
            //        Result.Width = System.Convert.ToInt32(Width * ThumbnailHeight / Height);
            //    }
            //}
            #endregion

            #region 计算缩略图在图片框中的位置。
            if (Result.Width < thumbnailWidth && thumbnailWidth != defaultSize)
            {
                Result.X = (thumbnailWidth - Result.Width) / 2;
            }
            if (Result.Height < thumbnailHeight && thumbnailHeight != defaultSize)
            {
                Result.Y = (thumbnailHeight - Result.Height) / 2;
            }
            #endregion

            return Result;

        }

        /// <summary>
        /// 从指定的文件装载图像。
        /// </summary>
        /// <param name="fileName">文件全名。</param>
        /// <returns>一个 System.Drawing.Image 实例。</returns>
        /// <remarks>
        /// 这个函数用于解决 System.Drawing.Image.FromFile 装载图片后不能及时释放文件的问题。
        /// </remarks>
        public static System.Drawing.Image GetImageFromFile(string fileName)
        {
            System.Drawing.Image bitmap = null;
            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {
                bitmap = GetImageFromStream(fs);
            }
            return bitmap;
        }

        /// <summary>
        /// 从指定的流装载图像。
        /// </summary>
        /// <param name="stream">数据流。</param>
        /// <returns>一个 System.Drawing.Image 实例。</returns>
        /// <remarks>
        /// 这个函数用于解决 System.Drawing.Image.FromFile 装载图片后不能及时释放文件的问题。
        /// </remarks>
        public static System.Drawing.Image GetImageFromStream(System.IO.Stream stream)
        {
            //System.Drawing.Bitmap bitmap = null;
            //try
            //{
            //    System.Drawing.Image image = System.Drawing.Image.FromStream(stream);
            //    #region 这段代码用于解除图像锁定状态。
            //    bitmap = new System.Drawing.Bitmap(image.Width, image.Height);
            //    System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
            //    //				g.Clear( System.Drawing.Color.Transparent );
            //    g.DrawImage(image, 0, 0, image.Width, image.Height);
            //    #endregion
            //}
            //catch
            //{
            //    throw;
            //}
            //return bitmap;
            return System.Drawing.Image.FromStream(stream);

        }

        /// <summary>
        /// 获取指定图像的等比例缩略图。
        /// </summary>
        /// <param name="image">用于提供图像数据的 System.Drawing.Image 实例。</param>
        /// <param name="width">缩略图宽度。取值为 0 时表示忽略此参数。</param>
        /// <param name="height">缩略图高度。取值为 0 时表示忽略此参数。</param>
        /// <param name="proportion">缩放时维持等比例。</param>
        /// <param name="lockSmallImage">锁定小尺寸图片。如果此项设置为 true，当原图像尺寸小于缩略图限制尺寸时返回原图像尺寸。</param>
        /// <param name="imageQuality">输出图片质量。</param>
        /// <param name="keepWhite">指示是否保留空白边距。如果保留空白边距则使用指定的颜色填充，以确保输出的图像的尺寸与指定的尺寸相等。否则输出的图像的尺寸可能小于指定的尺寸。</param>
        /// <param name="whiteFillColor">空白区域填充颜色。</param>
        /// <returns>一个 System.Drawing.Image 实例。</returns>
        public static System.Drawing.Bitmap GetThumbnailImage(System.Drawing.Image image, int width, int height, bool proportion, bool lockSmallImage, Thinksea.eImageQuality imageQuality, bool keepWhite, System.Drawing.Color whiteFillColor)
        {
            if (image == null)
            {
                throw new System.ArgumentNullException(nameof(image));
            }

            System.Drawing.RectangleF ImageRectangleF;
            if (width > 0 && height > 0 && proportion == false && keepWhite == true)
            {
                ImageRectangleF = new System.Drawing.RectangleF(0, 0, width, height);
                if (lockSmallImage)
                {
                    if (image.Width < width)
                    {
                        ImageRectangleF.Width = image.Width;
                        ImageRectangleF.X = (width - image.Width) / 2.0F;
                    }
                    if (image.Height < height)
                    {
                        ImageRectangleF.Height = image.Height;
                        ImageRectangleF.Y = (height - image.Height) / 2.0F;
                    }
                }
            }
            else
            {
                ImageRectangleF = Thinksea.Image.GetThumbnailImageSize(image.Width, image.Height, width, height, lockSmallImage);
                if (width == 0)
                {
                    width = (int)System.Math.Floor(ImageRectangleF.Right);
                }
                if (height == 0)
                {
                    height = (int)System.Math.Floor(ImageRectangleF.Bottom);
                }
            }

            System.Drawing.Imaging.PixelFormat outputFormat;

            System.Drawing.Bitmap bitmap;

            if (Image.IsPixelFormatIndexed(image.PixelFormat))
            {
                outputFormat = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
                bitmap = new System.Drawing.Bitmap(width, height, outputFormat);
            }
            else
            {
                switch (image.PixelFormat)
                {
                    case System.Drawing.Imaging.PixelFormat.Format48bppRgb:
                    case System.Drawing.Imaging.PixelFormat.Format64bppPArgb:
                    case System.Drawing.Imaging.PixelFormat.Format64bppArgb:
                        outputFormat = System.Drawing.Imaging.PixelFormat.Format32bppArgb;
                        break;
                    default:
                        outputFormat = image.PixelFormat;
                        break;
                }
                bitmap = new System.Drawing.Bitmap(width, height, outputFormat);
            }

            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap))
            {
                switch (imageQuality)
                {
                    case Thinksea.eImageQuality.High:
                        {
                            bitmap.SetResolution(image.HorizontalResolution, image.VerticalResolution);
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                        }
                        break;
                    case Thinksea.eImageQuality.Low:
                        {
                            //System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(System.Convert.ToInt32(ImageRectangleF.Width == 0 ? 1 : ImageRectangleF.Width), System.Convert.ToInt32(ImageRectangleF.Height == 0 ? 1 : ImageRectangleF.Height));
                            //System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
                            //g.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);

                            bitmap.SetResolution((image.HorizontalResolution > 96.0F ? 96.0F : image.HorizontalResolution), (image.VerticalResolution > 96.0F ? 96.0F : image.VerticalResolution));
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
                        }
                        break;
                    case Thinksea.eImageQuality.Web:
                        {
                            //System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(System.Convert.ToInt32(ImageRectangleF.Width == 0 ? 1 : ImageRectangleF.Width), System.Convert.ToInt32(ImageRectangleF.Height == 0 ? 1 : ImageRectangleF.Height));
                            //System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);
                            //g.DrawImage(image, 0, 0, bitmap.Width, bitmap.Height);

                            bitmap.SetResolution((image.HorizontalResolution > 72.0F ? 72.0F : image.HorizontalResolution), (image.VerticalResolution > 72.0F ? 72.0F : image.VerticalResolution));
                            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.None;
                            g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Low;
                            g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.Default;
                        }
                        break;
                }

                if (keepWhite && whiteFillColor != System.Drawing.Color.Transparent)
                {
                    g.Clear(whiteFillColor);
                }
                g.DrawImage(image, ImageRectangleF);

            }

            return bitmap;

        }

        /// <summary>
        /// 获取指定文件的等比例缩略图。
        /// </summary>
        /// <param name="fileName">图片文件全名。</param>
        /// <param name="width">缩略图宽度。取值为 0 时表示忽略此参数。</param>
        /// <param name="height">缩略图高度。取值为 0 时表示忽略此参数。</param>
        /// <param name="proportion">缩放时维持等比例。</param>
        /// <param name="lockSmallImage">锁定小尺寸图片。如果此项设置为 true，当原图像尺寸小于缩略图限制尺寸时返回原图像尺寸。</param>
        /// <param name="imageQuality">输出图片质量。</param>
        /// <param name="keepWhite">指示是否保留空白边距。如果保留空白边距则使用指定的颜色填充，以确保输出的图像的尺寸与指定的尺寸相等。否则输出的图像的尺寸可能小于指定的尺寸。</param>
        /// <param name="whiteFillColor">空白区域填充颜色。</param>
        /// <returns>一个 System.Drawing.Image 实例。</returns>
        public static System.Drawing.Bitmap GetThumbnailImage(string fileName, int width, int height, bool proportion, bool lockSmallImage, Thinksea.eImageQuality imageQuality, bool keepWhite, System.Drawing.Color whiteFillColor)
        {
            using (System.Drawing.Image img = Thinksea.Image.GetImageFromFile(fileName))
            {
                return Thinksea.Image.GetThumbnailImage(img, width, height, proportion, lockSmallImage, imageQuality, keepWhite, whiteFillColor);
            }
        }

        /// <summary>
        /// 获取图片尺寸。
        /// </summary>
        /// <param name="input">图片数据输入流。</param>
        /// <returns>图片尺寸。</returns>
        public static System.Drawing.Size GetImageSize(System.IO.Stream input)
        {
            System.Drawing.Size GetImageSize = new System.Drawing.Size();
            byte[] bTemp = new byte[4];
            long lFlen;
            long lPos;
            byte bHmsb;
            byte bHlsb;
            byte bWmsb;
            byte bWlsb;
            byte[] bBuf = new byte[8];
            byte bDone = 0;
            int iCount;

            System.IO.BinaryReader br = new System.IO.BinaryReader(input);
            lFlen = input.Length;
            //iFN = FreeFile
            //Open sFileName For Binary As iFN
            input.Read(bTemp, 0, bTemp.Length);//Get #iFN, 1, bTemp()

            //PNG file
            if (bTemp[0] == 0x89 && bTemp[1] == 0x50 && bTemp[2] == 0x4E && bTemp[3] == 0x47)
            {
                //Get #iFN, 19, bWmsb
                //Get #iFN, 20, bWlsb
                //Get #iFN, 23, bHmsb
                //Get #iFN, 24, bHlsb
                //GetImageSize.Width = CombineBytes(bWlsb, bWmsb)
                //GetImageSize.Height = CombineBytes(bHlsb, bHmsb)
                input.Seek(18, System.IO.SeekOrigin.Begin);
                bWmsb = br.ReadByte();
                bWlsb = br.ReadByte();
                input.Seek(22, System.IO.SeekOrigin.Begin);
                bHmsb = br.ReadByte();
                bHlsb = br.ReadByte();
                GetImageSize.Width = CombineBytes(bWlsb, bWmsb);
                GetImageSize.Height = CombineBytes(bHlsb, bHmsb);
            }

            //GIF file
            else if (bTemp[0] == 0x47 && bTemp[1] == 0x49 && bTemp[2] == 0x46 && bTemp[3] == 0x38)
            {
                //Get #iFN, 7, bWlsb
                //Get #iFN, 8, bWmsb
                //Get #iFN, 9, bHlsb
                //Get #iFN, 10, bHmsb
                //GetImageSize.Width = CombineBytes(bWlsb, bWmsb)
                //GetImageSize.Height = CombineBytes(bHlsb, bHmsb)
                input.Seek(6, System.IO.SeekOrigin.Begin);
                bWlsb = br.ReadByte();
                bWmsb = br.ReadByte();
                bHlsb = br.ReadByte();
                bHmsb = br.ReadByte();
                GetImageSize.Width = CombineBytes(bWlsb, bWmsb);
                GetImageSize.Height = CombineBytes(bHlsb, bHmsb);
            }


            //JPEG file
            else if (bTemp[0] == 0xFF && bTemp[1] == 0xD8 && bTemp[2] == 0xFF)
            {
                //Debug.Print "JPEG"
                //    lPos = 3
                //    Do
                //        Do
                //            Get #iFN, lPos, bBuf(1)
                //            Get #iFN, lPos + 1, bBuf(2)
                //            lPos = lPos + 1
                //        Loop Until (bBuf(1) = 0xFF && bBuf(2) <> 0xFF) Or lPos > lFlen

                //        For iCount = 0 To 7
                //            Get #iFN, lPos + iCount, bBuf(iCount)
                //        Next iCount
                //        If bBuf(0) >= 0xC0 && bBuf(0) <= 0xC3 Then
                //            bHmsb = bBuf(4)
                //            bHlsb = bBuf(5)
                //            bWmsb = bBuf(6)
                //            bWlsb = bBuf(7)
                //            bDone = 1
                //        Else
                //            lPos = lPos + (CombineBytes(bBuf(2), bBuf(1))) + 1
                //        End If
                //    Loop While lPos < lFlen && bDone = 0
                //    GetImageSize.Width = CombineBytes(bWlsb, bWmsb)
                //    GetImageSize.Height = CombineBytes(bHlsb, bHmsb)

                bWlsb = 0;
                bWmsb = 0;
                bHlsb = 0;
                bHmsb = 0;

                lPos = 2;
                do
                {
                    do
                    {
                        input.Seek(lPos, System.IO.SeekOrigin.Begin);
                        bBuf[1] = br.ReadByte();
                        input.Seek(lPos + 1, System.IO.SeekOrigin.Begin);
                        bBuf[2] = br.ReadByte();
                        lPos++;
                    } while ((bBuf[1] == 0xFF && bBuf[2] != 0xFF) == false && lPos < lFlen);

                    for (iCount = 0; iCount < bBuf.Length; iCount++)
                    {
                        input.Seek(lPos + iCount, System.IO.SeekOrigin.Begin);
                        bBuf[iCount] = br.ReadByte();
                    }

                    if (bBuf[0] >= 0xC0 && bBuf[0] <= 0xC3)
                    {
                        bHmsb = bBuf[4];
                        bHlsb = bBuf[5];
                        bWmsb = bBuf[6];
                        bWlsb = bBuf[7];
                        bDone = 1;
                    }
                    else
                    {
                        lPos = lPos + (CombineBytes(bBuf[2], bBuf[1])) + 1;
                    }
                } while (lPos < lFlen && bDone == 0);

                GetImageSize.Width = CombineBytes(bWlsb, bWmsb);
                GetImageSize.Height = CombineBytes(bHlsb, bHmsb);
            }

            //BMP 文件        
            else if (bTemp[0] == 0x42 && bTemp[1] == 0x4D)
            {
                //Get #iFN, 19, bWlsb            
                //Get #iFN, 20, bWmsb            
                //Get #iFN, 23, bHlsb            
                //Get #iFN, 24, bHmsb            
                //GetImageSize.Width = CombineBytes(bWlsb, bWmsb)            
                //GetImageSize.Height = CombineBytes(bHlsb, bHmsb)        
                input.Seek(18, System.IO.SeekOrigin.Begin);
                bWmsb = br.ReadByte();
                bWlsb = br.ReadByte();
                input.Seek(22, System.IO.SeekOrigin.Begin);
                bHmsb = br.ReadByte();
                bHlsb = br.ReadByte();
                GetImageSize.Width = CombineBytes(bWmsb, bWlsb);
                GetImageSize.Height = CombineBytes(bHmsb, bHlsb);
            }
            else
            {
                input.Seek(0, System.IO.SeekOrigin.Begin);
                System.Drawing.Image img = System.Drawing.Bitmap.FromStream(input);
                GetImageSize = img.Size;
            }
            return GetImageSize;
        }

        /// <summary>
        /// 获取图片尺寸。
        /// </summary>
        /// <param name="fileName">图片文件名。</param>
        /// <returns>图片尺寸。</returns>
        public static System.Drawing.Size GetImageSize(string fileName)
        {
            using (System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
            {
                return GetImageSize(fs);
            }
        }

        private static int CombineBytes(byte lsb, byte msb)
        {
            return System.Convert.ToInt32(lsb) + System.Convert.ToInt32(msb) * 256;
        }

    }

    /// <summary>
    /// 定义图片质量。
    /// </summary>
    public enum eImageQuality
    {
        /// <summary>
        /// 高质量。
        /// </summary>
        High,
        /// <summary>
        /// 低质量。
        /// </summary>
        Low,
        /// <summary>
        /// Web 质量。
        /// </summary>
        Web,
    }

}
