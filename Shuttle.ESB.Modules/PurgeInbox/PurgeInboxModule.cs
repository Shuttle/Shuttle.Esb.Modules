using System;
using Shuttle.Esb;

namespace Shuttle.Esb.Modules
{
	public class PurgeInboxModule : IModule
	{
		private IServiceBus _bus;

		private readonly string _startupPipelineName = typeof(StartupPipeline).FullName;

		public void Initialize(IServiceBus bus)
		{
			_bus = bus;

			_bus.Events.PipelineCreated += PipelineCreated;
		}

		private void PipelineCreated(object sender, PipelineEventArgs e)
		{
			if (!e.Pipeline.GetType().FullName.Equals(_startupPipelineName, StringComparison.InvariantCultureIgnoreCase))
			{
				return;
			}

			e.Pipeline.RegisterObserver(new PurgeInboxObserver());
		}
	}
}