using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace WoodGrain
{
	public static class Simplify3DXmlApllier
	{
		public static void Apply(XmlDocument doc, IEnumerable<(int, int)> layers)
		{
			// find "<temperatureController name="Primary Extruder">" child node
			var node = doc.SelectSingleNode("profile/temperatureController[@name='Primary Extruder']");
			if (node == null)
				throw new XmlException("Cannot find node: <temperatureController name=\"Primary Extruder\">!");

			// remove old nodes
			var oldLayers = node.SelectNodes("setpoint");
			foreach (var oldLayer in oldLayers.Cast<XmlNode>())
				node.RemoveChild(oldLayer);

			// generate and insert new nodes
			foreach (var (newLayer, temp) in layers)
			{
				var elem = doc.CreateElement("setpoint");
				elem.SetAttribute("layer", newLayer.ToString());
				elem.SetAttribute("temperature", temp.ToString());
				node.AppendChild(elem);
			}
		}
	}
}
