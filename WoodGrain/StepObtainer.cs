using System;
using System.Collections.Generic;

namespace WoodGrain
{
	public static class StepObtainer
	{
		public static IEnumerable<(int layer, int temp)> Generate(LayerSettings settings) 
		{
			var r = new Random();
			var min = (int)settings.TempMin;
			var max = (int)settings.TempMax;
			
			var layer = 1;
			while (layer < settings.Layers)
			{
				var temp = r.Next(min, max);
				yield return (layer, temp);
				layer += r.Next(settings.StepsMin, settings.StepsMax);
			}
		}
	}
}
