using System;
using System.Linq;
using Shuttle.Core.Infrastructure;
using Shuttle.ESB.Core;

namespace Shuttle.ESB.Modules
{
	public class MessageForwardingObserver : IPipelineObserver<OnAfterHandleMessage>
	{
		private readonly IMessageRouteCollection _messageRoutes = new MessageRouteCollection();

		private readonly ILog _log;

		public MessageForwardingObserver()
		{
			_log = Log.For(this);
		}

		internal void Initialize(IServiceBus bus)
		{
			var section = ShuttleConfigurationSection.Open<MessageForwardingSection>("messageForwarding");

			if (section == null || section.ForwardingRoutes == null)
			{
				return;
			}

			var factory = new MessageRouteSpecificationFactory();

			foreach (MessageRouteElement mapElement in section.ForwardingRoutes)
			{
				var map = _messageRoutes.Find(mapElement.Uri);

				if (map == null)
				{
					map = new MessageRoute(bus.Configuration.QueueManager.GetQueue(mapElement.Uri));

					_messageRoutes.Add(map);
				}

				foreach (SpecificationElement specificationElement in mapElement)
				{
					map.AddSpecification(factory.Create(specificationElement.Name, specificationElement.Value));
				}
			}
		}

		public void Execute(OnAfterHandleMessage pipelineEvent)
		{
			var state = pipelineEvent.Pipeline.State;
			var message = state.GetMessage();
			var transportMessage = state.GetTransportMessage();
			var handlerContext = state.GetHandlerContext();

			Guard.AgainstNull(message, "message");
			Guard.AgainstNull(transportMessage, "transportMessage");
			Guard.AgainstNull(handlerContext, "handlerContext");

			foreach (var uri in _messageRoutes.FindAll(message.GetType().FullName).Select(messageRoute => messageRoute.Queue.Uri.ToString()).ToList())
			{
				var recipientUri = uri;

				if (_log.IsTraceEnabled)
				{
					_log.Trace(string.Format(ESBModuleResources.TraceForwarding, transportMessage.MessageType, transportMessage.MessageId, new Uri(recipientUri).Secured()));
				}

				handlerContext.Send(message, c => c.WithRecipient(recipientUri));
			}
		}
	}
}