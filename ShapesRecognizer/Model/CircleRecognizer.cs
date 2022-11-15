using System;
using System.Linq;

namespace ShapesRecognizer.Model
{
    public class CircleRecognizer : IShapeRecognizer
    {
        private double allowableError;

        private int minimumNumberOfPoints;

        public CircleRecognizer(double allowableError, int minimumNumberOfPoints)
        {
            this.allowableError = allowableError;
            this.minimumNumberOfPoints = minimumNumberOfPoints;
        }

        public void Recognize(Shape shape)
        {
            if (shape.OutlinePoints.Count < this.minimumNumberOfPoints)
                return;

            var center = shape.Center.Value;
            var shapeDistances = shape.OutlinePoints.Select(p => Math.Sqrt(Math.Pow(p.X - center.X, 2) + Math.Pow(p.Y - center.Y, 2)));
            var averageRadius = shapeDistances.Average();
            if (shapeDistances.All(d => Math.Abs(averageRadius - d) / averageRadius <= allowableError))
                shape.Type = ShapeType.Circle;
        }
    }
}
