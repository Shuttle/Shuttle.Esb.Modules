using System;
using System.IO;
using NUnit.Framework;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Esb.Modules.Tests
{
    [TestFixture]
    public class MessageForwardingSectionTests
    {
        [Test]
        [TestCase("MessageForwarding.config")]
        [TestCase("MessageForwarding-Grouped.config")]
        public void Should_be_able_to_load_the_configuration(string file)
        {
            var section = ConfigurationSectionProvider.OpenFile<MessageForwardingSection>("shuttle", "messageForwarding", Path.Combine(AppDomain.CurrentDomain.BaseDirectory, string.Format(@"config-files\{0}", file)));

            Assert.IsNotNull(section);
            Assert.AreEqual(2, section.ForwardingRoutes.Count);

            foreach (MessageRouteElement map in section.ForwardingRoutes)
            {
                Console.WriteLine(map.Uri);

                foreach (SpecificationElement specification in map)
                {
                    Console.WriteLine("-> {0} - {1}", specification.Name, specification.Value);
                }

                Console.WriteLine();
            }
        }
    }
}