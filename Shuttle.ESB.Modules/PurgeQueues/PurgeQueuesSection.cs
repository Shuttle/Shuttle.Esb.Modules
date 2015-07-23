using System.Configuration;

namespace Shuttle.ESB.Modules
{
	public class PurgeQueuesSection : ConfigurationSection
	{
		[ConfigurationProperty("queues", IsRequired = true, DefaultValue = null)]
		public PurgeQueueElementCollection Queues
		{
			get { return (PurgeQueueElementCollection) this["queues"]; }
		}
	}
}