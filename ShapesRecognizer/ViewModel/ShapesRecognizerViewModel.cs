using Microsoft.Win32;
using ShapesRecognizer.View;
using System.IO;
using System.Windows;
using System.Linq;
using System.Windows.Media.Imaging;
using System;
using System.Windows.Media;
using ShapesRecognizer.Model;
using System.Collections.Generic;

namespace ShapesRecognizer.ViewModel
{
    public class ShapesRecognizerViewModel : IShapesRecognizerViewModel
    {
        private PixelMap pixelMap;
        private IShapesRecognizerView view;
        private IShapeFinder shapeFinder;
        private IShapesRecognizer shapesRecognizer;
        private IShapeHighlighter shapeHighlighter;
        private IImageProcessingManager imageProcessingManager;
        private List<Shape> shapes;

        public ShapesRecognizerViewModel(IShapesRecognizerView view, IShapeFinder shapeFinder, IShapesRecognizer shapesRecognizer,
            IShapeHighlighter shapeHighlighter, IImageProcessingManager imageProcessingManager)
        {
            this.view = view;
            this.shapeFinder = shapeFinder;
            this.shapesRecognizer = shapesRecognizer;
            this.shapeHighlighter = shapeHighlighter;
            this.imageProcessingManager = imageProcessingManager;
        }

        public void view_FilesSelected(string[] filesPaths)
        {
            var supportedFilePath = filesPaths.Where(p => imageProcessingManager.IsSupportExtension(Path.GetExtension(p))).FirstOrDefault();
            if (supportedFilePath == null)
            {
                view.ShowMessage("Selected file is not supported.");
                return;
            }

            var imageBitmapSource = imageProcessingManager.OpenImage(supportedFilePath);
            if (imageBitmapSource == null)
            {
                view.ShowMessage("Selected file is corrupted.");
                return;
            }

            if (!imageProcessingManager.IsSupportPixelFormat(imageBitmapSource.Format))
            {
                view.ShowMessage("Selected file's pixel format is not supported.");
                return;
            }

            view.SetImage(imageBitmapSource);
            view.SetFileSelectionAreaVisibility(Visibility.Hidden);
            view.SetImageAreaVisibility(Visibility.Visible);

            try
            {
                this.pixelMap = imageProcessingManager.CreatePixelMap(imageBitmapSource);
                this.shapes = shapeFinder.FindShapes(this.pixelMap);
                shapesRecognizer.Recognize(shapes);
            }
            catch (Exception ex)
            {
                view.ShowMessage($"Image processing failed. Exception: {ex.Message}");
            }
        }

        public void view_FileSelectionButtonClicked()
        {
            var dialog = new OpenFileDialog();
            view.ShowOpenFileDialog(dialog);
        }

        public void view_HighlightShapesButtonClicked()
        {
            var circles = shapes.Where(x => x.Type == ShapeType.Circle);
            if (!circles.Any())
            {
                view.ShowMessage("No circles found.");
                return;
            }

            try
            {
                foreach (var circle in circles)
                    shapeHighlighter.Highlight(circle, pixelMap, Colors.Red);
                view.SetImage(pixelMap.ToBitmap());
            }
            catch (Exception ex)
            {
                view.ShowMessage($"Shapes highlighting failed. Exception: {ex.Message}");
            }
        }
    }
}
