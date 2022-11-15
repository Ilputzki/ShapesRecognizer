using System.Collections.Generic;

namespace ShapesRecognizer.Model
{
    public class ShapesRecognizer : IShapesRecognizer
    {
        public List<IShapeRecognizer> Recognizers { get; set; }

        public ShapesRecognizer(List<IShapeRecognizer> recognizers)
        {
            this.Recognizers = recognizers;
        }

        public void Recognize(List<Shape> shapes)
        {
            foreach (var shape in shapes)
                foreach (var recognizer in Recognizers)
                    recognizer.Recognize(shape);
        }
    }
}
