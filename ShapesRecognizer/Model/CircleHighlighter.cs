using System;
using System.Linq;
using System.Windows.Media;

namespace ShapesRecognizer.Model
{
    public class CircleHighlighter : IShapeHighlighter
    {
        public void Highlight(Shape shape, PixelMap pixelMap, Color color)
        {
            if (shape.Type != ShapeType.Circle)
                return;

            var minX = shape.OutlinePoints.Min(p => p.X) - 1;
            var minY = shape.OutlinePoints.Min(p => p.Y) - 1;
            var maxX = shape.OutlinePoints.Max(p => p.X) + 1;
            var maxY = shape.OutlinePoints.Max(p => p.Y) + 1;
            var squareSideLength = Math.Max(maxX - minX, maxY - minY);

            for (var i = 0; i <= squareSideLength; i++)
            {
                pixelMap[minX + i, minY] = color;
                pixelMap[minX + i, maxY] = color;
                pixelMap[minX, minY + i] = color;
                pixelMap[maxX, minY + i] = color;
            }
        }
    }
}
