using System.Configuration;
using System.Xml;
using Shuttle.Esb;

namespace Shuttle.Esb.Modules
{
	public class MessageForwardingSection : ConfigurationSection
    {
		[ConfigurationProperty("forwardingRoutes", IsRequired = true, DefaultValue = null)]
		public MessageRouteElementCollection ForwardingRoutes
        {
			get { return (MessageRouteElementCollection)this["forwardingRoutes"]; }
        }
	}
}