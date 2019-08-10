using System.Windows.Forms;

namespace WoodGrain
{
	public interface IInOutConfigurable<TInput, TOutput>
	{
		Control GetInputConfigurationControl();

		TInput ConfigureInput(Control configurationControl);

		Control GetOutputConfigurationControl();

		TOutput ConfigureOutput(Control configurationControl);
	}
}
