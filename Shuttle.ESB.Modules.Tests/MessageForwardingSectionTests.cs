using System;
using NUnit.Framework;
using Shuttle.ESB.Core;

namespace Shuttle.ESB.Modules.Tests
{
    [TestFixture]
	public class MessageForwardingSectionTests
    {
        [Test]
		[TestCase("MessageForwarding.config")]
		[TestCase("MessageForwarding-Grouped.config")]
        public void Should_be_able_to_load_the_configuration(string file)
        {
			var section = ShuttleConfigurationSection.Open<MessageForwardingSection>("messageForwarding", string.Format(@"config-files\{0}", file));

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