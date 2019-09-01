using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace WoodGrain
{
	public class GrainGenerator : ILayerGenerator<GrainLayerSettings>
	{
		public static GrainGenerator Instance = new GrainGenerator();

		private GrainGenerator() { }

		public Control ConfigurationControl => null;

		public virtual void Save() 
		{
			// nada
		}

		public IEnumerable<(int layer, int step)> GetLayers(GrainLayerSettings settings)
		{
			var r = new Random();
			var min = (int)settings.TempMin;
			var max = (int)settings.TempMax;

			var layer = 1;
			if (settings.StartLayers > 0)
			{
				yield return (1, (int)settings.StartTemp);
				layer += settings.StartLayers;
			}
			while (layer <= settings.Layers)
			{
				var temp = r.Next(min, max);
				yield return (layer, temp);
				layer += r.Next(settings.StepsMin, settings.StepsMax);
			}
		}
	}
}
