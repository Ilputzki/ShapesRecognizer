using System.Collections.Generic;
using System.Windows.Media;

namespace ShapesRecognizer.Model
{
    public class ShapeFinder : IShapeFinder
    {
        private IOutlineFinder outlineFinder;
        private Color blankColor;

        public ShapeFinder(IOutlineFinder outlineFinder, Color blankColor)
        {
            this.outlineFinder = outlineFinder;
            this.blankColor = blankColor;
        }

        public List<Shape> FindShapes(PixelMap pixelMap)
        {
            var shapeOutlines = new List<Shape>();
            var shapeOutlinesByX = new Dictionary<int, List<ShapePoint>>();
            var insideShape = false;
            for (var x = 0; x < pixelMap.Width; x++)
            {
                for (var y = 0; y < pixelMap.Height; y++)
                {
                    if (pixelMap[x, y] == blankColor)
                    {
                        insideShape = false;
                        continue;
                    }

                    if (insideShape)
                        continue;

                    var point = new ShapePoint(x, y);
                    if (shapeOutlinesByX.ContainsKey(x) && shapeOutlinesByX[x].Contains(point))
                    {
                        insideShape = true;
                        continue;
                    }

                    var shapeOutline = outlineFinder.FindShapeOutline(point, blankColor, pixelMap);
                    foreach (var shapePoint in shapeOutline)
                    {
                        if (!shapeOutlinesByX.ContainsKey(shapePoint.X))
                            shapeOutlinesByX.Add(shapePoint.X, new List<ShapePoint>());
                        shapeOutlinesByX[shapePoint.X].Add(shapePoint);
                    }

                    shapeOutlines.Add(new Shape(shapeOutline));
                }
            }
            return shapeOutlines;
        }
    }
}
