using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace WoodGrain
{
	public abstract class Simplify3DXmlFileInput<TOutput> : IGrainApplier<string, TOutput>
	{
		public void Apply(string input, TOutput output, IEnumerable<(int, int)> layers)
		{
			var doc = new XmlDocument();
			doc.Load(input);
		
			Simplify3DXmlApllier.Apply(doc, layers);

			// write the resulting xml to file
			Output(doc, output);
		}

		protected abstract void Output(XmlDocument doc, TOutput output);

		public Control GetInputConfigurationControl() => null;

		public string ConfigureInput(Control configurationControl) => string.Empty;

		public abstract Control GetOutputConfigurationControl();

		public abstract TOutput ConfigureOutput(Control configurationControl);
	}

	public class Simplify3DClipboard : Simplify3DXmlFileInput<object>
	{
		public static Simplify3DClipboard Instance = new Simplify3DClipboard();

		private Simplify3DClipboard() { }

		public override object ConfigureOutput(Control configurationControl) => null;

		public override Control GetOutputConfigurationControl() => null;

		protected override void Output(XmlDocument doc, object output) 
		{
			doc.PreserveWhitespace = false;
			using (var w = new StringWriter())
			{
				doc.Save(w);
				Clipboard.SetText(w.ToString());
			}
		}
	}

	public class Simplify3DXmlFileOutput : Simplify3DXmlFileInput<string>
	{
		public static Simplify3DXmlFileOutput Instance = new Simplify3DXmlFileOutput();

		private Simplify3DXmlFileOutput() { }

		public override string ConfigureOutput(Control configurationControl) => null;

		public override Control GetOutputConfigurationControl() => null;

		protected override void Output(XmlDocument doc, string output)
		{
			// set profile name to match (new) filename

			// get profile name
			var profileName = Path.GetFileNameWithoutExtension(output);

			// some heuristics which may be incorrect
			profileName = profileName.Replace("++", " - ");
			profileName = profileName.Replace('+', ' ');

			// change name in document
			var profile = doc.SelectSingleNode("profile");
			if (profile.Attributes["name"] == null)
				throw new XmlException("Cannot find attribute \"name\" in profile!");
			profile.Attributes["name"].Value = profileName;

			doc.PreserveWhitespace = false;
			doc.Save(output);
		}
	}
}
