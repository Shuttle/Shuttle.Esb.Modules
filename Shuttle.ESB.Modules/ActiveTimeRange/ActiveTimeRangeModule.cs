using System;
using Shuttle.Core.Infrastructure;

namespace Shuttle.Esb.Modules
{
	public class ActiveTimeRangeModule : IModule, IDisposable, IThreadState
	{
		private volatile bool _active;
		private readonly string _startupPipelineName = typeof (StartupPipeline).FullName;
		private readonly ActiveTimeRange _activeTimeRange = new ActiveTimeRangeConfiguration().CreateActiveTimeRange();

		public void Initialize(IServiceBus bus)
		{
			Guard.AgainstNull(bus, "bus");

			bus.Events.PipelineCreated += PipelineCreated;
		}

		private void PipelineCreated(object sender, PipelineEventArgs e)
		{
			if (e.Pipeline.GetType().FullName.Equals(_startupPipelineName, StringComparison.InvariantCultureIgnoreCase))
			{
				return;
			}

			e.Pipeline.RegisterObserver(new ActiveTimeRangeObserver(this, _activeTimeRange));
		}

		public void Dispose()
		{
			_active = false;
		}

		public bool Active
		{
			get { return _active; }
		}
	}
}