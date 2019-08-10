using System.Windows.Forms;

namespace WoodGrain
{
	public interface IConfigurable<TInput>
	{
		// for future expansion: provide some kind of control to gather configuration
		// from, retrieve configuration
		// for now, these methods may safely return null

		// return null if no configuration is desired
		Control GetConfigurationControl();

		// would be called with the control returned from Get Configuration panel
		TInput Configure(Control configurationControl);
	}
}
