using Microsoft.Win32;
using ShapesRecognizer.ViewModel;
using System.Windows;
using System.Windows.Media;

namespace ShapesRecognizer.View
{
    public interface IShapesRecognizerView
    {
        IShapesRecognizerViewModel ViewModel { get; set; }

        void SetImage(ImageSource imageSource);
        void SetFileSelectionAreaVisibility(Visibility visibility);
        void SetImageAreaVisibility(Visibility visibility);
        void ShowOpenFileDialog(OpenFileDialog dialog);
        void ShowMessage(string message);
    }
}
