using System.Configuration;

namespace Shuttle.Esb.Modules
{
    public class PurgeQueueElement : ConfigurationElement
    {
        [ConfigurationProperty("uri", IsRequired = true)]
        public string Uri
        {
            get
            {
				return (string)this["uri"];
            }
        }
    }
}