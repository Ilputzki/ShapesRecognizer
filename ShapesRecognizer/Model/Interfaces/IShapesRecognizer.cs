using System.Collections.Generic;

namespace ShapesRecognizer.Model
{
    public interface IShapesRecognizer
    {
        List<IShapeRecognizer> Recognizers { get; set; }
        void Recognize(List<Shape> shapes);
    }
}
