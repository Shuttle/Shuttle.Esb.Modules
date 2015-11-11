using Shuttle.Core.Infrastructure;
using Shuttle.ESB.Core;

namespace Shuttle.ESB.Modules
{
    public class InboxLoggingObserver :
        IPipelineObserver<OnAfterGetMessage>,
        IPipelineObserver<OnAfterDeserializeTransportMessage>,
        IPipelineObserver<OnAfterDecompressMessage>,
        IPipelineObserver<OnAfterDecryptMessage>,
        IPipelineObserver<OnAfterDeserializeMessage>,
        IPipelineObserver<OnAfterAssessMessageHandling>,
        IPipelineObserver<OnAfterHandleMessage>,
        IPipelineObserver<OnAfterSendDeferred>,
        IPipelineObserver<OnAfterDispatchTransportMessage>,
        IPipelineObserver<OnAfterProcessDeferredMessage>,
        IPipelineObserver<OnAfterAcknowledgeMessage>
    {
        private readonly ILog _log;
        private IServiceBus _bus;

        public InboxLoggingObserver()
        {
            _log = Log.For(this);
        }

        public void Execute(OnAfterAcknowledgeMessage pipelineEvent)
        {
            var transportMessage = pipelineEvent.Pipeline.State.GetTransportMessage();

            _log.Trace(string.Format(LoggingResources.MessageAcknowledged, transportMessage.MessageType,
                transportMessage.MessageId));
        }

        public void Execute(OnAfterDecompressMessage pipelineEvent)
        {
            if (!_log.IsTraceEnabled)
            {
                return;
            }

            var transportMessage = pipelineEvent.Pipeline.State.GetTransportMessage();

            if (!transportMessage.CompressionEnabled())
            {
                return;
            }

            _log.Trace(string.Format(LoggingResources.DecompressedMessage, transportMessage.MessageType,
                transportMessage.MessageId, transportMessage.CompressionAlgorithm));
        }

        public void Execute(OnAfterDecryptMessage pipelineEvent)
        {
            if (!_log.IsTraceEnabled)
            {
                return;
            }

            var transportMessage = pipelineEvent.Pipeline.State.GetTransportMessage();

            if (!transportMessage.EncryptionEnabled())
            {
                return;
            }

            _log.Trace(string.Format(LoggingResources.DecryptedMessage, transportMessage.MessageType,
                transportMessage.MessageId, transportMessage.CompressionAlgorithm));
        }

        public void Execute(OnAfterDeserializeMessage pipelineEvent)
        {
            if (_log.IsVerboseEnabled)
            {
                return;
            }

            var transportMessage = pipelineEvent.Pipeline.State.GetTransportMessage();
            var message = pipelineEvent.Pipeline.State.GetMessage();

            _log.Trace(string.Format(LoggingResources.MessageDeserialized, message.GetType(), transportMessage.MessageId));
        }

        public void Execute(OnAfterDeserializeTransportMessage pipelineEvent)
        {
            if (_log.IsVerboseEnabled)
            {
                return;
            }

            var transportMessage = pipelineEvent.Pipeline.State.GetTransportMessage();

            _log.Verbose(string.Format(LoggingResources.TransportMessageDeserialized, transportMessage.MessageType,
                transportMessage.MessageId));
        }

        public void Execute(OnAfterDispatchTransportMessage pipelineEvent)
        {
            if (!_log.IsTraceEnabled)
            {
                return;
            }

            var transportMessage = pipelineEvent.Pipeline.State.GetTransportMessage();

            var queue = !_bus.Configuration.HasOutbox
                ? _bus.Configuration.QueueManager.GetQueue(transportMessage.RecipientInboxWorkQueueUri)
                : _bus.Configuration.Outbox.WorkQueue;

            _log.Trace(string.Format(ESBResources.TraceMessageEnqueued,
                transportMessage.MessageType,
                transportMessage.MessageId,
                queue.Uri));

            if (_log.IsVerboseEnabled)
            {
                _log.Verbose(string.Format(LoggingResources.CorrelationIdReceived, transportMessage.CorrelationId));

                foreach (var header in transportMessage.Headers)
                {
                    _log.Verbose(string.Format(LoggingResources.TransportHeaderReceived, header.Key, header.Value));
                }
            }
        }

        public void Execute(OnAfterGetMessage pipelineEvent)
        {
            if (!_log.IsVerboseEnabled)
            {
                return;
            }

            var queue = pipelineEvent.Pipeline.State.GetDeferredQueue();

            _log.Trace(string.Format(LoggingResources.StreamDequeued, queue.Uri.Secured()));
        }

        public void Execute(OnAfterHandleMessage pipelineEvent)
        {
            if (!_log.IsTraceEnabled)
            {
                return;
            }

            var state = pipelineEvent.Pipeline.State;
            var transportMessage = state.GetTransportMessage();
            var handler = state.GetMessageHandler();

            _log.Trace(string.Format(LoggingResources.CorrelationIdReceived, transportMessage.CorrelationId));

            foreach (var header in transportMessage.Headers)
            {
                _log.Trace(string.Format(LoggingResources.TransportHeaderReceived, header.Key, header.Value));
            }

            _log.Trace(string.Format(LoggingResources.MessageHandlerInvoked,
                transportMessage.MessageType,
                transportMessage.MessageId,
                handler != null ? handler.GetType().FullName : LoggingResources.MesssageHandlerNull));

            switch (state.GetProcessingStatus())
            {
                case ProcessingStatus.Ignore:
                {
                    _log.Trace(string.Format(LoggingResources.MessageIgnored, transportMessage.MessageType,
                        transportMessage.MessageId));

                    break;
                }
                case ProcessingStatus.MessageHandled:
                {
                    _log.Trace(string.Format(LoggingResources.MessageHandled, transportMessage.MessageType,
                        transportMessage.MessageId));

                    break;
                }
                case ProcessingStatus.Assigned:
                {
                    _log.Trace(string.Format(LoggingResources.MessageAssigned, transportMessage.MessageType,
                        transportMessage.MessageId));

                    break;
                }
            }
        }

        public void Execute(OnAfterProcessDeferredMessage pipelineEvent)
        {
            if (!_log.IsTraceEnabled)
            {
                return;
            }

            _log.Trace(string.Format(LoggingResources.DeferredTransportMessageReturned,
                pipelineEvent.Pipeline.State.GetTransportMessage().MessageId));
        }

        public void Initialize(IServiceBus bus)
        {
            Guard.AgainstNull(bus, "bus");

            _bus = bus;
        }

        public void Execute(OnAfterAssessMessageHandling pipelineEvent1)
        {
            throw new System.NotImplementedException();
        }

        public void Execute(OnAfterSendDeferred pipelineEvent1)
        {
            throw new System.NotImplementedException();
        }
    }
}