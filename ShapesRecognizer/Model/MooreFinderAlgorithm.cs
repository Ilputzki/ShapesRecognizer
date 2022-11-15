using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace ShapesRecognizer.Model
{
    public class MooreOutlineFinder : IOutlineFinder
    {
        public List<ShapePoint> FindShapeOutline(ShapePoint startPoint, Color blankColor, PixelMap pixelMap)
        {
            var shapeOutline = new List<ShapePoint>();
            var currentPoint = startPoint;
            var previousPoint = new ShapePoint(startPoint.X, startPoint.Y - 1);

            var previousPointEndCriteria = new ShapePoint(startPoint.X, startPoint.Y - 1);
            var visitedStartPointCountEndCriteria = 2;
            var overflowEndCriteria = pixelMap.Height * pixelMap.Width;

            var visitedStartPointCount = 0;
            do
            {
                shapeOutline.Add(currentPoint);
                var roundPath = GetPointRoundPath(currentPoint, previousPoint, pixelMap.Width, pixelMap.Height);
                for (var i = 1; i < roundPath.Count; i++)
                {
                    if (pixelMap[roundPath[i].X, roundPath[i].Y] == blankColor)
                        continue;

                    currentPoint = roundPath[i];
                    previousPoint = roundPath[i - 1];

                    if (currentPoint == startPoint)
                        visitedStartPointCount++;
                    break;
                }
            }
            while ((previousPoint != previousPointEndCriteria || currentPoint != startPoint) &&
            visitedStartPointCount < visitedStartPointCountEndCriteria && shapeOutline.Count < overflowEndCriteria);

            if (visitedStartPointCount >= visitedStartPointCountEndCriteria || shapeOutline.Count >= overflowEndCriteria)
                shapeOutline = shapeOutline.Distinct().ToList();

            return shapeOutline;
        }

        private List<ShapePoint> GetPointRoundPath(ShapePoint mainPoint, ShapePoint startPoint, int imageWidth, int imageHeight)
        {
            var roundPath = new List<ShapePoint>();
            roundPath.Add(startPoint);
            var currentPoint = startPoint;
            while (roundPath.Count < 8)
            {
                var difference = ShapePoint.GetPointDifference(currentPoint, mainPoint);
                if (difference.Y == 1)
                {
                    if (difference.X < 1)
                        currentPoint.X += 1;
                    else
                        currentPoint.Y -= 1;
                }
                if (difference.Y == 0)
                {
                    if (difference.X == 1)
                        currentPoint.Y -= 1;
                    else
                        currentPoint.Y += 1;
                }
                if (difference.Y == -1)
                {
                    if (difference.X > -1)
                        currentPoint.X -= 1;
                    else
                        currentPoint.Y += 1;
                }
                if (currentPoint.X >= 0 && currentPoint.X < imageWidth && currentPoint.Y >= 0 && currentPoint.Y < imageHeight)
                    roundPath.Add(currentPoint);
            }

            return roundPath;
        }
    }
}
