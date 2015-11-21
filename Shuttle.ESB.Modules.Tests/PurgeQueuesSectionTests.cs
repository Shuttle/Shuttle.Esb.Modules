using System;
using System.IO;
using NUnit.Framework;
using Shuttle.ESB.Core;

namespace Shuttle.ESB.Modules.Tests
{
    [TestFixture]
    public class PurgeQueuesSectionTests
    {
        [Test]
        [TestCase("PurgeQueues.config")]
        [TestCase("PurgeQueues-Grouped.config")]
        public void Should_be_able_to_load_the_configuration(string file)
        {
            var section = ShuttleConfigurationSection.Open<PurgeQueuesSection>("purgeQueues", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"config-files\{0}", file)));

            Assert.IsNotNull(section);
            Assert.AreEqual(2, section.Queues.Count);

            foreach (PurgeQueueElement element in section.Queues)
            {
                Console.WriteLine(element.Uri);
            }
        }
    }
}