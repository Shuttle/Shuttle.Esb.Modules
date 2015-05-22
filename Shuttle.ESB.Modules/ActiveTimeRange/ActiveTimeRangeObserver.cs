using Shuttle.Core.Infrastructure;
using Shuttle.ESB.Core;

namespace Shuttle.ESB.Modules
{
	internal class ActiveTimeRangeObserver : IPipelineObserver<OnPipelineStarting>
	{
		private readonly IThreadState _state;

		private readonly ActiveTimeRange _activeTimeRange;

		public ActiveTimeRangeObserver(IThreadState state, ActiveTimeRange activeTimeRange)
		{
			Guard.AgainstNull(state, "state");

			_state = state;
			_activeTimeRange = activeTimeRange;
		}

		public void Execute(OnPipelineStarting pipelineEvent)
		{
			const int SLEEP = 15000;

			if (_activeTimeRange.Active())
			{
				return;
			}

			pipelineEvent.Pipeline.Abort();

			ThreadSleep.While(SLEEP, _state);
		}
	}
}