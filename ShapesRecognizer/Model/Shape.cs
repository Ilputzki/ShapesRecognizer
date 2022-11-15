using System;
using System.Collections.Generic;
using System.Windows;

namespace ShapesRecognizer.Model
{
    public class Shape
    {
        public List<ShapePoint> OutlinePoints { get; }

        public Lazy<ShapePoint> Center;

        public ShapeType Type { get; set; }

        public Shape(List<ShapePoint> outlinePoints)
        {
            this.OutlinePoints = outlinePoints;
            this.Type = ShapeType.Undefined;
            this.Center = new Lazy<ShapePoint>(() =>
            {
                var centerX = 0;
                var centerY = 0;
                foreach (var point in OutlinePoints)
                {
                    centerX += point.X;
                    centerY += point.Y;
                }
                centerX /= OutlinePoints.Count;
                centerY /= OutlinePoints.Count;
                return new ShapePoint(centerX, centerY);
            });
        }
    }
}
