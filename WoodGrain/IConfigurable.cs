using System.Windows.Forms;

namespace WoodGrain
{
	public interface IConfigurable
	{
		// return null if no configuration is desired
		Control ConfigurationControl { get; }
	}
}
