using System;
using Shuttle.Core.Infrastructure;
using Shuttle.ESB.Core;

namespace Shuttle.ESB.Modules
{
	public class MessageForwardingModule : IModule
	{
		private readonly MessageForwardingObserver _observer = new MessageForwardingObserver();

		private readonly string inboxMessagePipelineName = typeof(InboxMessagePipeline).FullName;

		public void Initialize(IServiceBus bus)
		{
			Guard.AgainstNull(bus, "bus");

			bus.Events.PipelineCreated += PipelineCreated;

			_observer.Initialize(bus);
		}

		private void PipelineCreated(object sender, PipelineEventArgs e)
		{
			if (!e.Pipeline.GetType().FullName.Equals(inboxMessagePipelineName, StringComparison.InvariantCultureIgnoreCase))
			{
				return;
			}

			e.Pipeline.RegisterObserver(_observer);
		}
	}
}