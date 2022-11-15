using System.Windows;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using ShapesRecognizer.View;
using System;
using System.Windows.Media;
using ShapesRecognizer.Model;
using System.Collections.Generic;

namespace ShapesRecognizer
{
    public partial class ShapesRecognizerView : Window, IShapesRecognizerView
    {
        public ViewModel.IShapesRecognizerViewModel ViewModel { get; set; }

        public ShapesRecognizerView()
        {
            InitializeComponent();
            ImageAreaScrollViewer.MaxHeight = SystemParameters.PrimaryScreenHeight * 0.75;
            ImageAreaScrollViewer.MaxWidth = SystemParameters.PrimaryScreenWidth * 0.75;
        }

        public void SetFileSelectionAreaVisibility(Visibility visibility)
        {
            FileSelectionAreaGrid.Visibility = visibility;
        }

        public void SetImageAreaVisibility(Visibility visibility)
        {
            ImageAreaGrid.Visibility = visibility;
        }

        public void SetImage(ImageSource imageSource)
        {
            PreviewImage.Source = imageSource;
        }
        void IShapesRecognizerView.ShowMessage(string message)
        {
            MessageBox.Show(message);
        }

        public void ShowOpenFileDialog(OpenFileDialog dialog)
        {
            if (dialog.ShowDialog() == true)
                ViewModel.view_FilesSelected(dialog.FileNames);
        }

        private void FileSelectionAreaGrid_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
                ViewModel.view_FilesSelected(files);
            }
        }

        private void FileSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.view_FileSelectionButtonClicked();
        }

        private void SelectAnotherImageButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.view_FileSelectionButtonClicked();
        }

        private void HighlightCirclesButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.view_HighlightShapesButtonClicked();
        }
    }
}
