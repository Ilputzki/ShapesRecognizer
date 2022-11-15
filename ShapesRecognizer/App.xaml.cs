using ShapesRecognizer.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace ShapesRecognizer
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var outlineFinder = new MooreOutlineFinder();
            var shapeFinder = new ShapeFinder(outlineFinder, Colors.White);
            var shapeRecognizers = new List<IShapeRecognizer> { new CircleRecognizer(0.05, 20) };
            var shapesRecognizer = new Model.ShapesRecognizer(shapeRecognizers);
            var shapeHighlighter = new CircleHighlighter();
            var shapesRecognizerView = new ShapesRecognizerView();
            var imageProcessingManager = new ImageProcessingManager();
            var shapesRecognizerViewModel = new ViewModel.ShapesRecognizerViewModel(shapesRecognizerView, shapeFinder, shapesRecognizer, shapeHighlighter, imageProcessingManager);
            shapesRecognizerView.ViewModel = shapesRecognizerViewModel;

            shapesRecognizerView.Show();
        }
    }
}
