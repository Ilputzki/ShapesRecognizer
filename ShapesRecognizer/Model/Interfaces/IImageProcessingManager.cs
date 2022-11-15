using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShapesRecognizer.Model
{
    public interface IImageProcessingManager
    {
        string[] supportedExtensions { get; }
        PixelFormat[] supportedPixelFormats { get; }

        bool IsSupportExtension(string extension);

        bool IsSupportPixelFormat(PixelFormat pixelFormat);

        BitmapSource OpenImage(string imagePath);

        PixelMap CreatePixelMap(BitmapSource bitmapSource);
    }
}
