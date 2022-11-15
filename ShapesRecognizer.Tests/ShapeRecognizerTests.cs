using NUnit.Framework;
using ShapesRecognizer.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Media;

namespace ShapesRecognizer.Tests
{
    [TestFixture]
    public class ShapeRecognizerTests
    {
        private IShapeFinder shapeFinder;
        private IShapesRecognizer shapesRecognizer;
        private IImageProcessingManager imageProcessingManager;

        [SetUp]
        public void Setup()
        {
            var outlineFinder = new MooreOutlineFinder();
            shapeFinder = new ShapeFinder(outlineFinder, Colors.White);
            var shapeRecognizers = new List<IShapeRecognizer> { new CircleRecognizer(0.05, 20) };
            shapesRecognizer = new Model.ShapesRecognizer(shapeRecognizers);
            imageProcessingManager = new ImageProcessingManager();
        }

        [TestCase(@"TestResources\Indexed8TestCase.png", 3, 1)]
        [TestCase(@"TestResources\Bgra2TestCase.png", 16, 3)]
        [Test]
        public void FindShapesAndRecognizeCirclesTest(string path, int shapesCount, int circlesCount)
        {
            // Arrange
            var fullPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), path);
            var bitmapSource = imageProcessingManager.OpenImage(fullPath);
            var pixelMap = imageProcessingManager.CreatePixelMap(bitmapSource);

            // Act
            var shapes = shapeFinder.FindShapes(pixelMap);
            shapesRecognizer.Recognize(shapes);
            var resultCirclesCount = shapes.Count(x => x.Type == ShapeType.Circle);

            // Assert
            Assert.AreEqual(shapesCount, shapes.Count);
            Assert.AreEqual(circlesCount, resultCirclesCount);
        }
    }
}