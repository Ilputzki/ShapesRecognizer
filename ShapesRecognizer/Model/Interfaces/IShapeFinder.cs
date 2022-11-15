using System.Collections.Generic;

namespace ShapesRecognizer.Model
{
    public interface IShapeFinder
    {
        List<Shape> FindShapes(PixelMap pixelMap);
    }
}
