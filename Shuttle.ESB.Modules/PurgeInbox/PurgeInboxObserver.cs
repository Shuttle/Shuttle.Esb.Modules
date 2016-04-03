using Shuttle.Core.Infrastructure;
using Shuttle.Esb;

namespace Shuttle.Esb.Modules
{
	public class PurgeInboxObserver : IPipelineObserver<OnAfterInitializeQueueFactories>
	{
		private readonly ILog _log;

		public PurgeInboxObserver()
		{
			_log = Log.For(this);
		}

		public void Execute(OnAfterInitializeQueueFactories pipelineEvent)
		{
			var purge = pipelineEvent.Pipeline.State.GetServiceBus().Configuration.Inbox.WorkQueue as IPurgeQueue;

			if (purge == null)
			{
				_log.Warning(string.Format(EsbModuleResources.IPurgeQueueNotImplemented, pipelineEvent.Pipeline.State.GetServiceBus().Configuration.Inbox.WorkQueue.GetType().FullName));

				return;
			}

			purge.Purge();
		}
	}
}