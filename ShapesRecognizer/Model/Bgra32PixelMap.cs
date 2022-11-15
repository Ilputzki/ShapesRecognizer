using System;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace ShapesRecognizer.Model
{
    public class Bgra32PixelMap : PixelMap
    {
        public override PixelFormat PixelFormat { get { return PixelFormats.Bgra32; } }

        public Bgra32PixelMap(BitmapSource bitmapSource) : base(bitmapSource) { }

        public override Color this[int index1, int index2] 
        {
            get
            {
                var firstByteNumber = index1 * 4 + index2 * this.Width * 4;
                return Color.FromArgb(pixelBytes[firstByteNumber + 3], pixelBytes[firstByteNumber + 2], pixelBytes[firstByteNumber + 1], pixelBytes[firstByteNumber]);
            }
            set
            {
                var firstByteNumber = index1 * 4 + index2 * this.Width * 4;
                pixelBytes[firstByteNumber] = value.B;
                pixelBytes[firstByteNumber + 1] = value.G;
                pixelBytes[firstByteNumber + 2] = value.R;
                pixelBytes[firstByteNumber + 3] = value.A;
            }
        }
    }
}
