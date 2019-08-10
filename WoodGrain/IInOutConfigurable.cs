using System.Windows.Forms;

namespace WoodGrain
{
	public interface IInOutConfigurable<TInput, TOutput>
	{
		// for future expansion, see IConfigurable

		Control GetInputConfigurationControl();

		TInput ConfigureInput(Control configurationControl);

		Control GetOutputConfigurationControl();

		TOutput ConfigureOutput(Control configurationControl);
	}
}
