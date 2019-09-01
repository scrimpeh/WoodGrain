using System.Windows.Forms;

namespace WoodGrain
{
	public interface IInOutConfigurable : IStatable
	{
		// for future expansion, see IConfigurable

		Control InputConfigurationControl { get; }

		Control OutputConfigurationControl { get; }
	}
}
