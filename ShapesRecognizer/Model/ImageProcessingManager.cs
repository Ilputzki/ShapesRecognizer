using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShapesRecognizer.Model
{
    public class ImageProcessingManager : IImageProcessingManager
    {
        public string[] supportedExtensions { get; }
        public PixelFormat[] supportedPixelFormats { get; }

        public ImageProcessingManager()
        {
            supportedExtensions = new string[] { ".bmp", ".gif", ".jpg", ".jpeg", ".png" };
            supportedPixelFormats = new PixelFormat[] { PixelFormats.Bgra32, PixelFormats.Bgr24, PixelFormats.Indexed8 };
        }

        public bool IsSupportExtension(string extension)
        {
            return supportedExtensions.Contains(extension.ToLower());
        }

        public bool IsSupportPixelFormat(PixelFormat pixelFormat)
        {
            return supportedPixelFormats.Contains(pixelFormat);
        }

        public BitmapSource OpenImage(string imagePath)
        {
            var imageUri = new Uri(imagePath);
            var imageExtension = Path.GetExtension(imagePath).ToLower();
            BitmapDecoder bitmapDecoder = null;
            var bitmapCreateOptions = BitmapCreateOptions.PreservePixelFormat;
            var bitmapCacheOption = BitmapCacheOption.Default;
            switch (imageExtension)
            {
                case ".png":
                    bitmapDecoder = new PngBitmapDecoder(imageUri, bitmapCreateOptions, bitmapCacheOption);
                    break;
                case ".gif":
                    bitmapDecoder = new GifBitmapDecoder(imageUri, bitmapCreateOptions, bitmapCacheOption);
                    break;
                case ".jpeg":
                case ".jpg":
                    bitmapDecoder = new JpegBitmapDecoder(imageUri, bitmapCreateOptions, bitmapCacheOption);
                    break;
                case ".bmp":
                    bitmapDecoder = new BmpBitmapDecoder(imageUri, bitmapCreateOptions, bitmapCacheOption);
                    break;
            }

            if (bitmapDecoder == null)
                return null;

            return bitmapDecoder.Frames.FirstOrDefault();
        }

        public PixelMap CreatePixelMap(BitmapSource bitmapSource)
        {
            PixelMap pixelMap = null;
            if (bitmapSource.Format == PixelFormats.Bgra32)
                pixelMap = new Bgra32PixelMap(bitmapSource);
            if (bitmapSource.Format == PixelFormats.Bgr24)
                pixelMap = new Bgr24PixelMap(bitmapSource);
            if (bitmapSource.Format == PixelFormats.Indexed8)
                pixelMap = new Indexed8PixelMap(bitmapSource);
            return pixelMap;
        }
    }
}
