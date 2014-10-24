using System;
using System.Linq;
using Shuttle.Core.Infrastructure;
using Shuttle.ESB.Core;

namespace Shuttle.ESB.Modules
{
	public class MessageForwardingObserver : IPipelineObserver<OnAfterHandleMessage>
	{
		private readonly IMessageRouteCollection messageRoutes = new MessageRouteCollection();

		private ILog _log;

		public MessageForwardingObserver()
		{
			_log = Log.For(this);
		}

		internal void Initialize(IServiceBus bus)
		{
			var section = ShuttleConfigurationSection.Open<ModulesServiceBusSection>();

			if (section == null || section.ForwardingRoutes == null)
			{
				return;
			}

			var factory = new MessageRouteSpecificationFactory();

			foreach (MessageRouteElement mapElement in section.ForwardingRoutes)
			{
				var map = messageRoutes.Find(mapElement.Uri);

				if (map == null)
				{
					map = new MessageRoute(bus.Configuration.QueueManager.GetQueue(mapElement.Uri));

					messageRoutes.Add(map);
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

			foreach (var uri in messageRoutes.FindAll(message.GetType().FullName).Select(messageRoute => messageRoute.Queue.Uri.ToString()).ToList())
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