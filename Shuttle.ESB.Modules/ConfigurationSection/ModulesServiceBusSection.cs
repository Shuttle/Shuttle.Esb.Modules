using System.Configuration;
using System.Xml;
using Shuttle.ESB.Core;

namespace Shuttle.ESB.Modules
{
    public class ModulesServiceBusSection : ConfigurationSection
    {
		public static ModulesServiceBusSection Open(string file)
        {
			return ShuttleConfigurationSection.Open<ModulesServiceBusSection>("serviceBus", file);
        }

		[ConfigurationProperty("forwardingRoutes", IsRequired = false, DefaultValue = null)]
		public MessageRouteElementCollection ForwardingRoutes
        {
			get { return (MessageRouteElementCollection)this["forwardingRoutes"]; }
        }

		protected override bool OnDeserializeUnrecognizedElement(string elementName, XmlReader reader)
		{
			return true;
		}
	}
}