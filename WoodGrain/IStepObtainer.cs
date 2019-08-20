using System.Collections.Generic;

namespace WoodGrain
{
	public interface ILayerGenerator<TInput> : IConfigurable
	{
		IEnumerable<(int layer, int step)> GetLayers(TInput data);
	}
}
