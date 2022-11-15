using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShapesRecognizer.Model
{
    abstract public class PixelMap
    {
        abstract public PixelFormat PixelFormat { get; }

        public int Width { get; }

        public int Height { get; }

        protected byte[] pixelBytes;

        protected double dpiX;

        protected double dpiY;

        protected int stride;

        public PixelMap(BitmapSource bitmapSource)
        {
            this.stride = bitmapSource.PixelWidth * bitmapSource.Format.BitsPerPixel / 8;
            this.pixelBytes = new byte[(int)bitmapSource.PixelHeight * this.stride];
            bitmapSource.CopyPixels(pixelBytes, this.stride, 0);
            this.Width = bitmapSource.PixelWidth;
            this.Height = bitmapSource.PixelHeight;
            this.dpiX = bitmapSource.DpiX;
            this.dpiY = bitmapSource.DpiY;
        }

        abstract public Color this[int index1, int index2] { get; set; }

        public virtual BitmapSource ToBitmap()
        {
            return BitmapSource.Create(Width, Height, dpiX, dpiY, PixelFormat, null, pixelBytes, stride);
        }
    }
}
