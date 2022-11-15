namespace ShapesRecognizer.ViewModel
{
    public interface IShapesRecognizerViewModel
    {
        void view_FilesSelected(string[] filesPaths);

        void view_FileSelectionButtonClicked();

        void view_HighlightShapesButtonClicked();
    }
}
