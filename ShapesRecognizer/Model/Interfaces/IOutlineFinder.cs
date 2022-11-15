using System.Collections.Generic;
using System.Windows.Media;

namespace ShapesRecognizer.Model
{
    public interface IOutlineFinder
    {
        List<ShapePoint> FindShapeOutline(ShapePoint startPoint, Color blankColor, PixelMap pixelMap);
    }
}
