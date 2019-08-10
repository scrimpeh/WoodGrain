using System.IO;
using System.Linq;
using System.Xml;

namespace WoodGrain
{
	public static class GrainMaker
	{
		public static string Apply(string filename, LayerSettings settings)
		{
			var doc = new XmlDocument();
			doc.Load(filename);

			// find "<temperatureController name="Primary Extruder">" child node
			var node = doc.SelectSingleNode("profile/temperatureController[@name='Primary Extruder']");
			if (node == null)
				throw new XmlException("Cannot find node: <temperatureController name=\"Primary Extruder\">!");

			// remove old nodes
			var oldLayers = node.SelectNodes("setpoint");
			foreach (var oldLayer in oldLayers.Cast<XmlNode>())
				node.RemoveChild(oldLayer);

			// generate and insert new nodes
			foreach (var (newLayer, temp) in StepObtainer.Generate(settings)) 
			{
				var elem = doc.CreateElement("setpoint");
				elem.SetAttribute("layer", newLayer.ToString());
				elem.SetAttribute("temperature", temp.ToString());
				node.AppendChild(elem);
			}

			// now dump the resulting xml back into a string
			doc.PreserveWhitespace = false;
			using (var w = new StringWriter())
			{
				doc.Save(w);
				return w.ToString();
			}
		}
	}
}
