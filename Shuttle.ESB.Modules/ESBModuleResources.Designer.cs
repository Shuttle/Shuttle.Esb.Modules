﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18449
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Shuttle.Esb.Modules {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ESBModuleResources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ESBModuleResources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Shuttle.Esb.Modules.ESBModuleResources", typeof(ESBModuleResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Published DistributorCapabilityEvent for work queue uri &apos;{0}&apos;..
        /// </summary>
        public static string DistributorCapabilityEventPublishedx {
            get {
                return ResourceManager.GetString("DistributorCapabilityEventPublishedx", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ActiveFromTime &apos;{0}&apos; is not a valid time..
        /// </summary>
        public static string InvalidActiveFromTime {
            get {
                return ResourceManager.GetString("InvalidActiveFromTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ActiveToTime &apos;{0}&apos; is not a valid time..
        /// </summary>
        public static string InvalidActiveToTime {
            get {
                return ResourceManager.GetString("InvalidActiveToTime", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Interface &apos;IPurgeQueue&apos; has not been implemented on queue type &apos;{0}&apos;..
        /// </summary>
        public static string IPurgeQueueNotImplemented {
            get {
                return ResourceManager.GetString("IPurgeQueueNotImplemented", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Published MessageTypeHandlerCapabilityEvent for message type &apos;{0}&apos; on work queue uri &apos;{1}&apos;..
        /// </summary>
        public static string MessageTypeHandlerCapabilityEventPublished {
            get {
                return ResourceManager.GetString("MessageTypeHandlerCapabilityEventPublished", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Published MessageTypePublisherCapabilityEvent for message type &apos;{0}&apos; on work queue uri &apos;{1}&apos;..
        /// </summary>
        public static string MessageTypePublisherCapabilityEventPublished {
            get {
                return ResourceManager.GetString("MessageTypePublisherCapabilityEventPublished", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Published MessageTypeSenderCapabilityEvent for message type &apos;{0}&apos; on work queue uri &apos;{1}&apos;..
        /// </summary>
        public static string MessageTypeSenderCapabilityEventPublished {
            get {
                return ResourceManager.GetString("MessageTypeSenderCapabilityEventPublished", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Forwarding transport message type &apos;{0}&apos; with id &apos;{1}&apos; to &apos;{2}&apos;..
        /// </summary>
        public static string TraceForwarding {
            get {
                return ResourceManager.GetString("TraceForwarding", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Published WorkerCapabilityEvent for work queue uri &apos;{0}&apos;..
        /// </summary>
        public static string WorkerCapabilityEventPublished {
            get {
                return ResourceManager.GetString("WorkerCapabilityEventPublished", resourceCulture);
            }
        }
    }
}
