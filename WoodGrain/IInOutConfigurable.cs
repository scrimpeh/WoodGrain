using System.Windows.Forms;

namespace WoodGrain
{
	public interface IInOutConfigurable
	{
		// for future expansion, see IConfigurable

		Control GetInputConfigurationControl();

		Control GetOutputConfigurationControl();
	}
}
