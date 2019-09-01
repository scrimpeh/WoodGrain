using System.Windows.Forms;

namespace WoodGrain
{
	public interface IConfigurable : IStatable
	{
		// return null if no configuration is desired
		Control ConfigurationControl { get; }
	}
}
