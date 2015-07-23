using System.Configuration;

namespace Shuttle.ESB.Modules
{
	[ConfigurationCollection(typeof(PurgeQueueElement), AddItemName = "queue", CollectionType = ConfigurationElementCollectionType.BasicMap)]
    public class PurgeQueueElementCollection : ConfigurationElementCollection
    {
        protected override ConfigurationElement CreateNewElement()
        {
            return new PurgeQueueElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
			return ((PurgeQueueElement)element).Uri;
        }
    }
}