using System;
using System.Collections.Generic;
using System.Drawing;

namespace WoodGrain
{
	public class LowHighColorInterpPreview : IGrainPreview
	{
		public Color Low { get; set; } = Color.White;
		public Color High { get; set; } = Color.Black;

		public int LowTemp { get; set; } = 150;
		public int HighTemp { get; set; } = 250;

		public Image GetPreviewImage(IList<(int layer, int temp)> grain, int width, int zoom, out Color finalColor)
		{
			var layers = new List<int>();
			for (var i = 0; i < grain.Count - 1; i++)
			{
				var (layer, temp) = grain[i];
				var (layerNext, _) = grain[i + 1];

				for (var j = layer; j < layerNext; j++)
					layers.Add(temp);
			}
			layers.Add(grain[grain.Count - 1].temp);

			var bitmap = new Bitmap(width, zoom * layers.Count);

			for (var y = 0; y < layers.Count; y++)
				for (var i = 0; i < zoom; i++)
					for (var x = 0; x < bitmap.Width; x++)
						bitmap.SetPixel(x, y * zoom + i, GetColorForTemperature(layers[y]));

			finalColor = GetColorForTemperature(layers[layers.Count - 1]);

			return bitmap;
		}

		private static float LinearInterp(float x0, float x, float x1, float y0, float y1)
		{
			return (y0 * (x1 - x) + y1 * (x - x0)) / (x1 - x0);
		}

		private Color GetColorForTemperature(int temperature) 
		{
			// If the result is out of range, return early
			if (temperature <= LowTemp)
				return Low;
			if (temperature >= HighTemp)
				return High;

			// Do RGB interpolation

			var newR = LinearInterp(LowTemp, temperature, HighTemp, Low.R, High.R);
			var newG = LinearInterp(LowTemp, temperature, HighTemp, Low.G, High.G);
			var newB = LinearInterp(LowTemp, temperature, HighTemp, Low.B, High.B);

			return Color.FromArgb((int)newR, (int)newG, (int)newB);
		}
	}
}
