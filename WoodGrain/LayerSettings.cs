using System;

namespace WoodGrain
{
	public struct LayerSettings
	{
		public const int MinLayers = 1;
		public const int MaxLayers = 999;
		public const int MinSteps = 1;
		public const int MaxSteps = 999;
		public const decimal MinTemp = 1;
		public const decimal MaxTemp = 999;

		public int Layers { get; private set; } 
		public int StepsMin { get; private set; }
		public int StepsMax { get; private set; }

		public decimal TempMin { get; private set; }
		public decimal TempMax { get; private set; }

		private LayerSettings(Builder builder)
		{
			Layers = builder.Layers;
			StepsMin = builder.StepsMin;
			StepsMax = builder.StepsMax;
			TempMin = builder.TempMin;
			TempMax = builder.TempMax;

			var ok = true;
			ok &= MinLayers <= Layers && Layers <= MaxLayers;
			ok &= MinSteps <= StepsMin && StepsMin <= StepsMax && StepsMax <= MaxSteps;
			ok &= MinTemp <= TempMin && TempMin <= TempMax && TempMax <= MaxTemp;

			if (!ok)
				throw new ArgumentOutOfRangeException();
		}

		public struct Builder
		{
			public int Layers { get; set; }
			public int StepsMin { get; set; }
			public int StepsMax { get; set; }

			public decimal TempMin { get; set; }
			public decimal TempMax { get; set; }

			public LayerSettings Build()
				=> new LayerSettings(this);

			public bool TryBuild(out LayerSettings settings)
			{
				try 
				{
					settings = new LayerSettings(this);
					return true;
				} 
				catch
				{
					settings = default(LayerSettings);
					return false;
				}
			}
		}
	}
}
