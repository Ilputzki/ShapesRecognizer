using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ShapesRecognizer.Model
{
    public class Indexed8PixelMap : PixelMap
    {
        public override PixelFormat PixelFormat { get { return PixelFormats.Indexed8; } }

        private BitmapPalette palette;

        private Lazy<Dictionary<Color, byte>> paletteColorIndexes;

        public Indexed8PixelMap(BitmapSource bitmapSource) : base(bitmapSource) 
        {
            this.palette = bitmapSource.Palette;
            paletteColorIndexes = new Lazy<Dictionary<Color, byte>>(() =>
            {
                var colorIndexes = new Dictionary<Color, byte>();
                var colors = this.palette.Colors.Distinct().ToArray();
                for (var i = 0; i < colors.Length; i++)
                    colorIndexes.Add(colors[i], Convert.ToByte(i));
                return colorIndexes;
            });
        }

        public override Color this[int index1, int index2]
        {
            get
            {
                var palleteColorNumber = pixelBytes[index1 + index2 * this.Width];
                return palette.Colors[palleteColorNumber];
            }
            set
            {
                if (!paletteColorIndexes.Value.ContainsKey(value))
                    AddColor(value);

                pixelBytes[index1 + index2 * this.Width] = paletteColorIndexes.Value[value];
            }
        }

        public override BitmapSource ToBitmap()
        {
            return BitmapSource.Create(Width, Height, dpiX, dpiY, PixelFormat, palette, pixelBytes, stride);
        }

        private void AddColor(Color color)
        {
            if (paletteColorIndexes.Value.ContainsKey(color))
                return;

            if (palette.Colors.Count <= paletteColorIndexes.Value.Count)
                throw new NotSupportedException("Cannot add a new color to the palette. The palette is full.");

            var colors = new List<Color>(palette.Colors);
            var indexForNewColor = paletteColorIndexes.Value.Count;
            colors[indexForNewColor] = color;
            palette = new BitmapPalette(colors);
            paletteColorIndexes.Value.Add(color, Convert.ToByte(indexForNewColor));
        }
    }
}
