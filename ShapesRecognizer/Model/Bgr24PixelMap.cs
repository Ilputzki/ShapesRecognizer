using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShapesRecognizer.Model
{
    class Bgr24PixelMap : PixelMap
    {
        public override PixelFormat PixelFormat { get { return PixelFormats.Bgr24; } }

        public Bgr24PixelMap(BitmapSource bitmapSource) : base(bitmapSource) { }

        public override Color this[int index1, int index2]
        {
            get
            {
                var firstByteNumber = index1 * 3 + index2 * this.Width * 3;
                return Color.FromRgb(pixelBytes[firstByteNumber + 2], pixelBytes[firstByteNumber + 1], pixelBytes[firstByteNumber]);
            }
            set
            {
                var firstByteNumber = index1 * 3 + index2 * this.Width * 3;
                pixelBytes[firstByteNumber] = value.B;
                pixelBytes[firstByteNumber + 1] = value.G;
                pixelBytes[firstByteNumber + 2] = value.R;
            }
        }
    }
}
