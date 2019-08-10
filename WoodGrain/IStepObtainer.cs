using System.Collections.Generic;

namespace WoodGrain
{
	public interface ILayerGenerator<TInput> : IConfigurable<TInput>
	{
		IEnumerable<(int layer, int step)> GetLayers(TInput data);
	}
}
