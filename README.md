shuttle-esb-modules
===================

This package will contain common modules that extend [shuttle-esb](https://github.com/Shuttle/shuttle-esb) functionality.

### Purge Inbox Module

The module will attach the `PurgeInboxObserver` to the `OnAfterInitializeQueueFactories` event of the `StartupPipeline` and purges the inbox work queue if the relevant queue implementation has implemented the `IPurgeQueue` interface.  If it hasn't a warning is logged.

```c#
	var bus = ServiceBus
		.Create()
		.AddModule(new PurgeInboxModule())
		.Start();
```

### Purge Queues Module

The module will attach the `PurgeQueuesObserver` to the `OnAfterInitializeQueueFactories` event of the `StartupPipeline` and purges the configured queues if the relevant queue implementation has implemented the `IPurgeQueue` interface.  If it hasn't a warning is logged.

```xml
<configuration>
	<configSections>
		<section name="purgeQueues" type="Shuttle.ESB.Modules.PurgeQueuesSection, Shuttle.ESB.Modules"/>
	</configSections>

	<purgeQueues>
		<queues>
			<queue uri="msmq://./inbox" />
			<queue uri="sql://./inbox" />
		</queues>
	</purgeQueues>
</configuration>
```

```c#
	var bus = ServiceBus
		.Create()
		.AddModule(new PurgeQueuesModule())
		.Start();
```

### Message Forwarding Module

The module will attach the `MessageForwardingObserver` to the `OnAfterHandleMessage` and then send the handled message on to any defined endpoints.

```xml
<configuration>
	<configSections>
		<section name="messageForwarding" type="Shuttle.ESB.Modules.MessageForwardingSection, Shuttle.ESB.Modules"/>
	</configSections>

	<messageForwarding>
		<forwardingRoutes>
			<messageRoute uri="msmq://./inbox">
				<add specification="StartsWith" value="Shuttle.Messages1" />
				<add specification="StartsWith" value="Shuttle.Messages2" />
			</messageRoute>
			<messageRoute uri="sql://./inbox">
				<add specification="TypeList" value="DoSomethingCommand" />
			</messageRoute>
		</forwardingRoutes>
	</messageForwarding>
</configuration>
```

```c#
	var bus = ServiceBus
		.Create()
		.AddModule(new MessageForwardingModule())
		.Start();
```

### ActiveTimeRange

The module will attach the `ActiveTimeRangeObserver` to the `OnPipelineStarting` event of all pipelines except the `StartupPipeline` and abort the pipeline if the current time is not within the active time range.

```xml
  <appSettings>
    <add key="ActiveFromTime" value="*"/>
    <add key="ActiveToTime" value="*"/>
  </appSettings>
```

The default value of `*` indicates the whole day and your pipelines will never be stopped.

```c#
	var bus = ServiceBus
		.Create()
		.AddModule(new ActiveTimeRangeModule())
		.Start();
```

### Corrupt Transport Message Module

It will log any transport messages that fail deserailization via the `ServiceBus..Events.TransportMessageDeserializationException` event to a folder as specified in the application configuration `appSettings` key with name `CorruptTransportMessageFolder`:

```xml
  <appSettings>
    <add key="CorruptTransportMessageFolder" value="d:\shuttle-corrupt-messages"/>
  </appSettings>
```

```c#
	var bus = ServiceBus
		.Create()
		.AddModule(new CorruptTransportMessageModule())
		.Start();
```

