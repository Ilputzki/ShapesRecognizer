using System.Windows.Media;

namespace ShapesRecognizer.Model
{
    public interface IShapeHighlighter
    {
        void Highlight(Shape shape, PixelMap pixelMap, Color color);
    }
}
