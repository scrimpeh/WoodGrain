using System.Collections.Generic;

namespace WoodGrain
{
	public interface IGrainApplier<TInput, TOutput> : IInOutConfigurable<TInput, TOutput>
	{
		void Apply(TInput inConfig, TOutput outConfig, IEnumerable<(int layer, int temperature)> layers);
	}
}
