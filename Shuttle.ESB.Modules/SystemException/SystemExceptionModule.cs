using System.Collections.Generic;
using System.Linq;
using System.Net;
using Shuttle.Core.Infrastructure;
using Shuttle.Esb;
using Shuttle.Esb.Modules.Extensions;

namespace Shuttle.Esb.Modules
{
	public class SystemExceptionModule : IModule
	{
		private string _hostName;
		private string[] _ipAddresses;

		private IServiceBus _bus;

		private static readonly object Padlock = new object();
		private readonly List<object> _deferredEvents = new List<object>();

		public void Initialize(IServiceBus bus)
		{
		    Guard.AgainstNull(bus, "bus");

			_bus = bus;

			bus.Events.HandlerException += HandlerException;
			bus.Events.AfterPipelineExceptionHandled += PipelineException;

			_hostName = Dns.GetHostName();
			_ipAddresses = Dns.GetHostAddresses(_hostName).Select(address => address.ToString()).ToArray();
		}

		private void PipelineException(object sender, PipelineExceptionEventArgs e)
		{
			var @event = ExceptionEventExtensions.CorePipelineExceptionEvent(e.Pipeline.Exception, _hostName, _ipAddresses);

			@event.PipelineTypeFullName = e.Pipeline.GetType().FullName;
			@event.PipelineStageName = e.Pipeline.StageName;
			@event.PipelineEventTypeFullName = e.Pipeline.Event.GetType().FullName;

			lock(Padlock)
			{
				foreach (var deferredEvent in _deferredEvents)
				{
					_bus.Publish(deferredEvent);
				}

				_deferredEvents.Clear();
			}

			_bus.Publish(@event);
		}

		private void HandlerException(object sender, HandlerExceptionEventArgs e)
		{
			var @event = ExceptionEventExtensions.CoreHandlerExceptionEvent(e.Exception, _hostName, _ipAddresses);

		    @event.MessageId = e.TransportMessage.MessageId;
			@event.MessageTypeFullName = e.TransportMessage.MessageType;
			@event.WorkQueueUri = e.WorkQueue != null ? e.WorkQueue.Uri.ToString() : string.Empty;
			@event.ErrorQueueUri = e.ErrorQueue != null ? e.ErrorQueue.Uri.ToString() : string.Empty;
			@event.RetryCount = e.TransportMessage.FailureMessages.Count() + 1;
			@event.MaximumFailureCount = e.PipelineEvent.Pipeline.State.GetServiceBus().Configuration.Inbox.MaximumFailureCount;

			// cannot publish here since handler is wrapped in a transaction scope
			// will always also result in pipeline exception so publish there
			lock(Padlock)
			{
				_deferredEvents.Add(@event);
			}
		}
	}
}