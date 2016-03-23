using System;

namespace Shuttle.Esb.Modules.Events
{
	public class HandlerExceptionEvent
	{
	    public Guid MessageId { get; set; }
		public string MessageTypeFullName { get; set; }
		public string WorkQueueUri { get; set; }
		public string ErrorQueueUri { get; set; }
		public int RetryCount { get; set; }
		public int MaximumFailureCount { get; set; }
		public string ExceptionTypeFullName { get; set; }
		public string Source { get; set; }
		public string StackTrace { get; set; }
		public string Message { get; set; }
		public string TargetSite { get; set; }
		public string TargetSiteName { get; set; }
		public string TargetSiteDeclaringTypeFullName { get; set; }
		public string TargetSiteMemberType { get; set; }
		public string TargetSiteModuleFullyQualifiedName { get; set; }
		public string TargetSiteModuleAssemblyFullName { get; set; }
		public DateTime DateThrown { get; set; }
		public string MachineName { get; set; }
		public string HostName { get; set; }
		public string[] IPAddresses { get; set; }
	}
}