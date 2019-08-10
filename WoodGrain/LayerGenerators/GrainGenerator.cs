using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WoodGrain
{
	public class GrainGenerator : ILayerGenerator<LayerSettings>
	{
		public static GrainGenerator Instance = new GrainGenerator();

		private GrainGenerator() { }

		public LayerSettings Configure(Control configurationPanel) => default(LayerSettings);

		public Control GetConfigurationControl() => null;

		public IEnumerable<(int layer, int step)> GetLayers(LayerSettings settings)
		{
			var r = new Random();
			var min = (int)settings.TempMin;
			var max = (int)settings.TempMax;

			var layer = 1;
			while (layer <= settings.Layers)
			{
				var temp = r.Next(min, max);
				yield return (layer, temp);
				layer += r.Next(settings.StepsMin, settings.StepsMax);
			}
		}
	}
}
