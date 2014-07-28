The following types are missing in the new API.

    NServiceBus.Audit.MessageAuditer
    NServiceBus.AutomaticSubscriptions.AutoSubscriber
    NServiceBus.AutomaticSubscriptions.DefaultAutoSubscriptionStrategy
    NServiceBus.AutomaticSubscriptions.IAutoSubscriptionStrategy
    NServiceBus.CircuitBreakers.CircuitBreaker
    NServiceBus.Config.AddressInitializer
    NServiceBus.Config.ChannelCollection
    NServiceBus.Config.ChannelConfig
    NServiceBus.Config.DisplayInfrastructureServicesStatus
    NServiceBus.Config.GatewayConfig
    NServiceBus.Config.InfrastructureServices
    NServiceBus.Config.SatelliteConfigurer
    NServiceBus.Config.SiteCollection
    NServiceBus.Config.SiteConfig
    NServiceBus.Config.WindowsInstallerRunner
    NServiceBus.ConfigureGateway
    NServiceBus.ConfigureSagas
    NServiceBus.ConfigureSecondLevelRetriesExtensions
    NServiceBus.DataBus.Config.Bootstrapper
    NServiceBus.DataBus.DefaultDataBusSerializer
    NServiceBus.DataBus.FileShare.FileShareDataBus
    NServiceBus.DataBus.InMemory.InMemoryDataBus
    NServiceBus.Encryption.EncryptionMessageMutator
    NServiceBus.Faults.Forwarder.Config.FaultsQueueCreator
    NServiceBus.Faults.Forwarder.FaultManager
    NServiceBus.Faults.InMemory.FaultManager
    NServiceBus.Features.Categories.Serializers
    NServiceBus.Features.EnableDefaultFeatures
    NServiceBus.Features.Feature`1
    NServiceBus.Features.FeatureCategory
    NServiceBus.Features.FeatureInitializer
    NServiceBus.Features.Gateway
    NServiceBus.FeatureSettingsExtensions
    NServiceBus.Gateway.Channels.Channel
    NServiceBus.Gateway.Channels.ChannelFactory
    NServiceBus.Gateway.Channels.ChannelTypeAttribute
    NServiceBus.Gateway.Channels.DataReceivedOnChannelArgs
    NServiceBus.Gateway.Channels.Http.DefaultResponder
    NServiceBus.Gateway.Channels.Http.HttpChannelReceiver
    NServiceBus.Gateway.Channels.Http.HttpChannelSender
    NServiceBus.Gateway.Channels.Http.HttpHeaders
    NServiceBus.Gateway.Channels.Http.IHttpResponder
    NServiceBus.Gateway.Channels.Http.SetDefaultResponder
    NServiceBus.Gateway.Channels.IChannelFactory
    NServiceBus.Gateway.Channels.IChannelReceiver
    NServiceBus.Gateway.Channels.IChannelSender
    NServiceBus.Gateway.Channels.ReceiveChannel
    NServiceBus.Gateway.Deduplication.GatewayMessage
    NServiceBus.Gateway.Deduplication.InMemoryDeduplication
    NServiceBus.Gateway.Deduplication.RavenDBDeduplication
    NServiceBus.Gateway.DefaultInputAddress
    NServiceBus.Gateway.HeaderManagement.DataBusHeaderManager
    NServiceBus.Gateway.HeaderManagement.GatewayHeaderManager
    NServiceBus.Gateway.HeaderManagement.GatewayHeaders
    NServiceBus.Gateway.HeaderManagement.HeaderMapper
    NServiceBus.Gateway.Notifications.IMessageNotifier
    NServiceBus.Gateway.Notifications.INotifyAboutMessages
    NServiceBus.Gateway.Notifications.MessageNotifier
    NServiceBus.Gateway.Notifications.MessageReceivedOnChannelArgs
    NServiceBus.Gateway.Persistence.InMemoryPersistence
    NServiceBus.Gateway.Persistence.MessageInfo
    NServiceBus.Gateway.Persistence.Raven.GatewayMessage
    NServiceBus.Gateway.Persistence.Raven.RavenDbPersistence
    NServiceBus.Gateway.Receiving.ChannelException
    NServiceBus.Gateway.Receiving.ConfigurationBasedChannelManager
    NServiceBus.Gateway.Receiving.ConventionBasedChannelManager
    NServiceBus.Gateway.Receiving.GatewayReceiver
    NServiceBus.Gateway.Receiving.IManageReceiveChannels
    NServiceBus.Gateway.Receiving.IReceiveMessagesFromSites
    NServiceBus.Gateway.Receiving.SingleCallChannelReceiver
    NServiceBus.Gateway.Routing.Endpoints.DefaultEndpointRouter
    NServiceBus.Gateway.Routing.IRouteMessagesToEndpoints
    NServiceBus.Gateway.Routing.IRouteMessagesToSites
    NServiceBus.Gateway.Routing.Site
    NServiceBus.Gateway.Routing.Sites.ConfigurationBasedSiteRouter
    NServiceBus.Gateway.Routing.Sites.KeyPrefixConventionSiteRouter
    NServiceBus.Gateway.Routing.Sites.OriginatingSiteHeaderRouter
    NServiceBus.Gateway.Sending.CallInfo
    NServiceBus.Gateway.Sending.CallType
    NServiceBus.Gateway.Sending.GatewaySender
    NServiceBus.Gateway.Sending.IForwardMessagesToSites
    NServiceBus.Gateway.Sending.SingleCallChannelForwarder
    NServiceBus.Gateway.Utils.Hasher
    NServiceBus.Hosting.Configuration.ConfigManager
    NServiceBus.Hosting.Wcf.WcfServiceHost
    NServiceBus.Impersonation.Windows.WindowsIdentityEnricher
    NServiceBus.Installation.GatewayHttpListenerInstaller
    NServiceBus.Installation.PerformanceMonitorUsersInstaller
    NServiceBus.Management.Retries.SecondLevelRetries
    NServiceBus.ObjectBuilder.Common.CommonObjectBuilder
    NServiceBus.ObjectBuilder.Common.Config.ConfigureCommon
    NServiceBus.ObjectBuilder.Common.SynchronizedInvoker
    NServiceBus.Persistence.InMemory.InMemoryPersistence
    NServiceBus.Persistence.InMemory.SagaPersister.InMemorySagaPersister
    NServiceBus.Persistence.InMemory.SubscriptionStorage.InMemorySubscriptionStorage
    NServiceBus.Persistence.InMemory.TimeoutPersister.InMemoryTimeoutPersistence
    NServiceBus.Persistence.Msmq.SubscriptionStorage.Config.SubscriptionsQueueCreator
    NServiceBus.Persistence.Msmq.SubscriptionStorage.Entry
    NServiceBus.Persistence.Msmq.SubscriptionStorage.MsmqSubscriptionStorage
    NServiceBus.Persistence.Raven.RavenConventions
    NServiceBus.Persistence.Raven.RavenPersistenceConstants
    NServiceBus.Persistence.Raven.RavenSessionFactory
    NServiceBus.Persistence.Raven.RavenUnitOfWork
    NServiceBus.Persistence.Raven.RavenUserInstaller
    NServiceBus.Persistence.Raven.SagaPersister.RavenSagaPersister
    NServiceBus.Persistence.Raven.SagaPersister.SagaUniqueIdentity
    NServiceBus.Persistence.Raven.StoreAccessor
    NServiceBus.Persistence.Raven.SubscriptionStorage.MessageTypeConverter
    NServiceBus.Persistence.Raven.SubscriptionStorage.RavenSubscriptionStorage
    NServiceBus.Persistence.Raven.SubscriptionStorage.Subscription
    NServiceBus.Persistence.Raven.TimeoutPersister.RavenTimeoutPersistence
    NServiceBus.Persistence.SetupDefaultPersistence
    NServiceBus.Sagas.ConfigureHowToFindSagaWithMessageDispatcher
    NServiceBus.Sagas.ConfigureTimeoutAsSystemMessages
    NServiceBus.Sagas.Finders.HeaderSagaIdFinder`1
    NServiceBus.Sagas.Finders.PropertySagaFinder`2
    NServiceBus.Satellites.Config.SatelliteContext
    NServiceBus.Satellites.SatelliteLauncher
    NServiceBus.Satellites.SatellitesQueuesCreator
    NServiceBus.Scheduling.Configuration.ConfigureScheduledTaskAsSystemMessages
    NServiceBus.Scheduling.Configuration.SchedulerConfiguration
    NServiceBus.Scheduling.DefaultScheduler
    NServiceBus.Scheduling.InMemoryScheduledTaskStorage
    NServiceBus.Scheduling.IScheduledTaskStorage
    NServiceBus.Scheduling.IScheduler
    NServiceBus.Scheduling.Messages.ScheduledTask
    NServiceBus.Scheduling.ScheduledTaskMessageHandler
    NServiceBus.SecondLevelRetries.Helpers.SecondLevelRetriesHeaders
    NServiceBus.SecondLevelRetries.Helpers.TransportMessageHelpers
    NServiceBus.SecondLevelRetries.SecondLevelRetriesProcessor
    NServiceBus.Serializers.Json.Internal.MessageContractResolver
    NServiceBus.Serializers.Json.Internal.MessageSerializationBinder
    NServiceBus.Serializers.Json.Internal.XContainerConverter
    NServiceBus.Serializers.XML.Config.MessageTypesInitializer
    NServiceBus.Serializers.XML.XmlSanitizingStream
    NServiceBus.ServiceAsyncResult
    NServiceBus.Settings.Endpoint
    NServiceBus.Settings.ISetDefaultSettings
    NServiceBus.Settings.TransportSettings
    NServiceBus.SyncConfig
    NServiceBus.Timeout.Core.DefaultTimeoutManager
    NServiceBus.Timeout.Core.TimeoutManagerDefaults
    NServiceBus.Timeout.Hosting.Windows.TimeoutDispatcherProcessor
    NServiceBus.Timeout.Hosting.Windows.TimeoutMessageProcessor
    NServiceBus.Timeout.Hosting.Windows.TimeoutPersisterReceiver
    NServiceBus.Timeout.TimeoutManagerDeferrer
    NServiceBus.Timeout.TimeoutManagerHeaders
    NServiceBus.Transports.Msmq.Config.CheckMachineNameForComplianceWithDtcLimitation
    NServiceBus.Transports.Msmq.MsmqDequeueStrategy
    NServiceBus.Transports.Msmq.MsmqMessageSender
    NServiceBus.Transports.Msmq.MsmqQueueCreator
    NServiceBus.Unicast.BusAsyncResultEventArgs
    NServiceBus.Unicast.Config.DefaultToTimeoutManagerBasedDeferal
    NServiceBus.Unicast.Config.DefaultTransportForHost
    NServiceBus.Unicast.DefaultDispatcherFactory
    NServiceBus.Unicast.IMessageDispatcherFactory
    NServiceBus.Unicast.MessagingBestPractices
    NServiceBus.Unicast.Monitoring.CausationMutator
    NServiceBus.Unicast.Monitoring.CriticalTimeCalculator
    NServiceBus.Unicast.Monitoring.EstimatedTimeToSLABreachCalculator
    NServiceBus.Unicast.Monitoring.PerformanceCounterInitializer
    NServiceBus.Unicast.Monitoring.ProcessingStatistics
    NServiceBus.Unicast.Publishing.StorageDrivenPublisher
    NServiceBus.Unicast.Queuing.Installers.AuditQueueCreator
    NServiceBus.Unicast.Queuing.Installers.EndpointInputQueueCreator
    NServiceBus.Unicast.Queuing.Installers.ForwardReceivedMessagesToQueueCreator
    NServiceBus.Unicast.Queuing.QueuesCreator
    NServiceBus.Unicast.Subscriptions.MessageDrivenSubscriptions.EnableMessageDrivenPublisherIfStorageIsFound
    NServiceBus.Unicast.Subscriptions.MessageDrivenSubscriptions.MessageDrivenSubscriptionManager
    NServiceBus.Unicast.Subscriptions.MessageDrivenSubscriptions.NoopSubscriptionAuthorizer
    NServiceBus.Unicast.Subscriptions.MessageDrivenSubscriptions.SubcriberSideFiltering.FilteringMutator
    NServiceBus.Unicast.Subscriptions.MessageDrivenSubscriptions.SubcriberSideFiltering.SubscriptionPredicatesEvaluator
    NServiceBus.Unicast.Transport.Config.Bootstrapper
    NServiceBus.Unicast.Transport.Transactional.Config.AdvancedTransactionalConfig
    NServiceBus.Unicast.Transport.TransportConnectionString
    NServiceBus.Unicast.Transport.TransportMessageExtensions

The following members are missing on the public types.

    <>f__AnonymousType2`2
        <Id>j__TPar <>f__AnonymousType2`2::get_Id()
        <Time>j__TPar <>f__AnonymousType2`2::get_Time()

    <>f__AnonymousType3`2
        <handlers>j__TPar <>f__AnonymousType3`2::get_handlers()
        <typeHandled>j__TPar <>f__AnonymousType3`2::get_typeHandled()

    NServiceBus.Audit.AuditBehavior
        NServiceBus.Audit.MessageAuditer NServiceBus.Audit.AuditBehavior::get_MessageAuditer()
        System.Void NServiceBus.Audit.AuditBehavior::Invoke(NServiceBus.Pipeline.Contexts.ReceivePhysicalMessageContext,System.Action)
        System.Void NServiceBus.Audit.AuditBehavior::set_MessageAuditer(NServiceBus.Audit.MessageAuditer)

    NServiceBus.AutomaticSubscriptions.Config.AutoSubscribeSettings
        NServiceBus.AutomaticSubscriptions.Config.AutoSubscribeSettings NServiceBus.AutomaticSubscriptions.Config.AutoSubscribeSettings::AutoSubscribePlainMessages()
        NServiceBus.AutomaticSubscriptions.Config.AutoSubscribeSettings NServiceBus.AutomaticSubscriptions.Config.AutoSubscribeSettings::CustomAutoSubscriptionStrategy()
        NServiceBus.AutomaticSubscriptions.Config.AutoSubscribeSettings NServiceBus.AutomaticSubscriptions.Config.AutoSubscribeSettings::DoNotAutoSubscribeSagas()
        NServiceBus.AutomaticSubscriptions.Config.AutoSubscribeSettings NServiceBus.AutomaticSubscriptions.Config.AutoSubscribeSettings::DoNotRequireExplicitRouting()

    NServiceBus.AutoSubscribeSettingsExtensions
        NServiceBus.Features.FeatureSettings NServiceBus.AutoSubscribeSettingsExtensions::AutoSubscribe(NServiceBus.Features.FeatureSettings,System.Action`1<NServiceBus.AutomaticSubscriptions.Config.AutoSubscribeSettings>)

    NServiceBus.BinarySerializerConfigurationExtensions
        NServiceBus.Settings.SerializationSettings NServiceBus.BinarySerializerConfigurationExtensions::Binary(NServiceBus.Settings.SerializationSettings)

    NServiceBus.Bootstrapper
        System.Void NServiceBus.Bootstrapper::Init()

    NServiceBus.Config.IFinalizeConfiguration
        System.Void NServiceBus.Config.IFinalizeConfiguration::FinalizeConfiguration()

    NServiceBus.Config.IWantToRunWhenConfigurationIsComplete
        System.Void NServiceBus.Config.IWantToRunWhenConfigurationIsComplete::Run()

    NServiceBus.Config.SatelliteConfigurer
        System.Void NServiceBus.Config.SatelliteConfigurer::Init()

    NServiceBus.Configure
        System.Func`1<System.String> NServiceBus.Configure::DefineEndpointVersionRetriever
        NServiceBus.Config.ConfigurationSource.IConfigurationSource NServiceBus.Configure::get_ConfigurationSource()
        NServiceBus.Settings.Endpoint NServiceBus.Configure::get_Endpoint()
        NServiceBus.Settings.TransportSettings NServiceBus.Configure::get_Transports()
        System.Boolean NServiceBus.Configure::BuilderIsConfigured()
        System.Boolean NServiceBus.Configure::get_SendOnlyMode()
        System.Boolean NServiceBus.Configure::WithHasBeenCalled()
        System.Void NServiceBus.Configure::add_ConfigurationComplete(System.Action)
        System.Void NServiceBus.Configure::Initialize()
        System.Void NServiceBus.Configure::remove_ConfigurationComplete(System.Action)
        System.Void NServiceBus.Configure::ScaleOut(System.Action`1<NServiceBus.Settings.ScaleOutSettings>)
        System.Void NServiceBus.Configure::set_Builder(NServiceBus.ObjectBuilder.IBuilder)
        System.Void NServiceBus.Configure::set_ConfigurationSource(NServiceBus.Config.ConfigurationSource.IConfigurationSource)
        System.Void NServiceBus.Configure::set_Configurer(NServiceBus.ObjectBuilder.IConfigureComponents)

    NServiceBus.ConfigureCriticalErrorAction
        System.Void NServiceBus.ConfigureCriticalErrorAction::RaiseCriticalError(NServiceBus.Configure,System.String,System.Exception)

    NServiceBus.ConfigureRavenPersistence
        NServiceBus.Configure NServiceBus.ConfigureRavenPersistence::CustomiseRavenPersistence(NServiceBus.Configure,System.Action`1<Raven.Client.IDocumentStore>)
        NServiceBus.Configure NServiceBus.ConfigureRavenPersistence::RavenPersistenceWithStore(NServiceBus.Configure,Raven.Client.IDocumentStore)

    NServiceBus.ConfigureUnicastBus
        NServiceBus.Unicast.Config.ConfigUnicastBus NServiceBus.ConfigureUnicastBus::UnicastBus(NServiceBus.Configure)

    NServiceBus.DataBus.DataBusReceiveBehavior
        System.Void NServiceBus.DataBus.DataBusReceiveBehavior::Invoke(NServiceBus.Pipeline.Contexts.ReceiveLogicalMessageContext,System.Action)

    NServiceBus.DataBus.DataBusSendBehavior
        System.Void NServiceBus.DataBus.DataBusSendBehavior::Invoke(NServiceBus.Pipeline.Contexts.SendLogicalMessageContext,System.Action)

    NServiceBus.Encryption.Bootstrapper
        System.Void NServiceBus.Encryption.Bootstrapper::Init()

    NServiceBus.Faults.Forwarder.Config.FaultsQueueCreator
        System.Boolean NServiceBus.Faults.Forwarder.Config.FaultsQueueCreator::get_IsDisabled()

    NServiceBus.Features.Feature
        NServiceBus.Features.FeatureCategory NServiceBus.Features.Feature::get_Category()
        System.Boolean NServiceBus.Features.Feature::get_Enabled()
        System.Boolean NServiceBus.Features.Feature::IsEnabled()
        System.Boolean NServiceBus.Features.Feature::IsEnabled(System.Type)
        System.Boolean NServiceBus.Features.Feature::op_Equality(NServiceBus.Features.Feature,NServiceBus.Features.Feature)
        System.Boolean NServiceBus.Features.Feature::op_Inequality(NServiceBus.Features.Feature,NServiceBus.Features.Feature)
        System.Boolean NServiceBus.Features.Feature::ShouldBeEnabled()
        System.Collections.Generic.IEnumerable`1<NServiceBus.Features.Feature> NServiceBus.Features.Feature::ByCategory(NServiceBus.Features.FeatureCategory)
        System.Void NServiceBus.Features.Feature::Disable()
        System.Void NServiceBus.Features.Feature::Disable(System.Type)
        System.Void NServiceBus.Features.Feature::DisableByDefault(System.Type)
        System.Void NServiceBus.Features.Feature::Enable()
        System.Void NServiceBus.Features.Feature::Enable(System.Type)
        System.Void NServiceBus.Features.Feature::EnableByDefault()
        System.Void NServiceBus.Features.Feature::EnableByDefault(System.Type)
        System.Void NServiceBus.Features.Feature::Initialize()

    NServiceBus.Features.FeatureSettings
        NServiceBus.Features.FeatureSettings NServiceBus.Features.FeatureSettings::Disable()
        NServiceBus.Features.FeatureSettings NServiceBus.Features.FeatureSettings::Enable()

    NServiceBus.Features.Sagas
        System.Collections.Generic.IDictionary`2<System.Type,System.Collections.Generic.IDictionary`2<System.Type,System.Collections.Generic.KeyValuePair`2<System.Reflection.PropertyInfo,System.Reflection.PropertyInfo>>> NServiceBus.Features.Sagas::SagaEntityToMessageToPropertyLookup
        System.Boolean NServiceBus.Features.Sagas::FindAndConfigureSagasIn(System.Collections.Generic.IEnumerable`1<System.Type>)
        System.Boolean NServiceBus.Features.Sagas::IsSagaType(System.Type)
        System.Boolean NServiceBus.Features.Sagas::ShouldMessageStartSaga(System.Type,System.Type)
        System.Collections.Generic.IEnumerable`1<System.Type> NServiceBus.Features.Sagas::GetFindersForMessageAndEntity(System.Type,System.Type)
        System.Collections.Generic.IEnumerable`1<System.Type> NServiceBus.Features.Sagas::GetSagaDataTypes()
        System.Reflection.MethodInfo NServiceBus.Features.Sagas::GetFindByMethodForFinder(NServiceBus.Saga.IFinder,System.Object)
        System.Type NServiceBus.Features.Sagas::GetSagaEntityTypeForSagaType(System.Type)
        System.Type NServiceBus.Features.Sagas::GetSagaTypeForSagaEntityType(System.Type)
        System.Void NServiceBus.Features.Sagas::ConfigureFinder(System.Type)
        System.Void NServiceBus.Features.Sagas::ConfigureSaga(System.Type)

    NServiceBus.Features.TimeoutManager
        NServiceBus.Address NServiceBus.Features.TimeoutManager::get_DispatcherAddress()
        NServiceBus.Address NServiceBus.Features.TimeoutManager::get_InputAddress()

    NServiceBus.Hosting.GenericHost
        System.Void NServiceBus.Hosting.GenericHost::Install(System.String)
        System.Void NServiceBus.Hosting.GenericHost::Start()
        System.Void NServiceBus.Hosting.GenericHost::Stop()

    NServiceBus.Hosting.HostInformation
        NServiceBus.Hosting.HostInformation NServiceBus.Hosting.HostInformation::CreateDefault()
        System.String NServiceBus.Hosting.HostInformation::get_DisplayInstanceIdentifier()

    NServiceBus.Hosting.IHost
        System.Void NServiceBus.Hosting.IHost::Install(System.String)
        System.Void NServiceBus.Hosting.IHost::Start()
        System.Void NServiceBus.Hosting.IHost::Stop()

    NServiceBus.Hosting.Profiles.IHandleProfile
        System.Void NServiceBus.Hosting.Profiles.IHandleProfile::ProfileActivated()

    NServiceBus.Hosting.Profiles.ProfileActivator
        System.Void NServiceBus.Hosting.Profiles.ProfileActivator::Run()

    NServiceBus.Hosting.Profiles.ProfileManager
        System.Void NServiceBus.Hosting.Profiles.ProfileManager::ActivateProfileHandlers()

    NServiceBus.Hosting.Roles.IConfigureRole
        NServiceBus.Unicast.Config.ConfigUnicastBus NServiceBus.Hosting.Roles.IConfigureRole::ConfigureRole(NServiceBus.IConfigureThisEndpoint)

    NServiceBus.Hosting.Roles.RoleManager
        System.Void NServiceBus.Hosting.Roles.RoleManager::ConfigureBusForEndpoint(NServiceBus.IConfigureThisEndpoint)

    NServiceBus.Hosting.Wcf.WcfManager
        System.Void NServiceBus.Hosting.Wcf.WcfManager::Startup()

    NServiceBus.Impersonation.Windows.ConfigureWindowsIdentityEnricher
        System.Void NServiceBus.Impersonation.Windows.ConfigureWindowsIdentityEnricher::Run()

    NServiceBus.Installation.INeedToInstallSomething
        System.Void NServiceBus.Installation.INeedToInstallSomething::Install(System.String)

    NServiceBus.Installation.PerformanceMonitorUsersInstaller
        System.Void NServiceBus.Installation.PerformanceMonitorUsersInstaller::Install(System.String)

    NServiceBus.Installer`1
        System.Void NServiceBus.Installer`1::Install()
        System.Void NServiceBus.Installer`1::set_RunOtherInstallers(System.Boolean)

    NServiceBus.IWantCustomInitialization
        System.Void NServiceBus.IWantCustomInitialization::Init()

    NServiceBus.IWantCustomLogging
        System.Void NServiceBus.IWantCustomLogging::Init()

    NServiceBus.IWantTheEndpointConfig
        NServiceBus.IConfigureThisEndpoint NServiceBus.IWantTheEndpointConfig::get_Config()
        System.Void NServiceBus.IWantTheEndpointConfig::set_Config(NServiceBus.IConfigureThisEndpoint)

    NServiceBus.IWantToRunBeforeConfiguration
        System.Void NServiceBus.IWantToRunBeforeConfiguration::Init()

    NServiceBus.IWantToRunBeforeConfigurationIsFinalized
        System.Void NServiceBus.IWantToRunBeforeConfigurationIsFinalized::Run()

    NServiceBus.JsonSerializerConfigurationExtensions
        NServiceBus.Settings.SerializationSettings NServiceBus.JsonSerializerConfigurationExtensions::Bson(NServiceBus.Settings.SerializationSettings)
        NServiceBus.Settings.SerializationSettings NServiceBus.JsonSerializerConfigurationExtensions::Json(NServiceBus.Settings.SerializationSettings)

    NServiceBus.Licensing.LicenseBehavior
        System.Void NServiceBus.Licensing.LicenseBehavior::Invoke(NServiceBus.Pipeline.Contexts.ReceivePhysicalMessageContext,System.Action)

    NServiceBus.Licensing.LicenseInitializer
        System.Void NServiceBus.Licensing.LicenseInitializer::Init()

    NServiceBus.Licensing.LicenseManager
        NServiceBus.Licensing.License NServiceBus.Licensing.LicenseManager::get_License()

    NServiceBus.Logging.Log4NetBridge.ConfigureInternalLog4NetBridge
        System.Void NServiceBus.Logging.Log4NetBridge.ConfigureInternalLog4NetBridge::Init()

    NServiceBus.Logging.Loggers.ConsoleLogger
        System.Boolean NServiceBus.Logging.Loggers.ConsoleLogger::get_IsDebugEnabled()
        System.Boolean NServiceBus.Logging.Loggers.ConsoleLogger::get_IsErrorEnabled()
        System.Boolean NServiceBus.Logging.Loggers.ConsoleLogger::get_IsFatalEnabled()
        System.Boolean NServiceBus.Logging.Loggers.ConsoleLogger::get_IsInfoEnabled()
        System.Boolean NServiceBus.Logging.Loggers.ConsoleLogger::get_IsWarnEnabled()
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::Debug(System.String)
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::Debug(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::DebugFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::Error(System.String)
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::Error(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::ErrorFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::Fatal(System.String)
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::Fatal(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::FatalFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::Info(System.String)
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::Info(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::InfoFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::Warn(System.String)
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::Warn(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.ConsoleLogger::WarnFormat(System.String,System.Object[])

    NServiceBus.Logging.Loggers.ConsoleLoggerFactory
        NServiceBus.Logging.ILog NServiceBus.Logging.Loggers.ConsoleLoggerFactory::GetLogger(System.String)
        NServiceBus.Logging.ILog NServiceBus.Logging.Loggers.ConsoleLoggerFactory::GetLogger(System.Type)

    NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetAppenderFactory
        System.Object NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetAppenderFactory::CreateColoredConsoleAppender(System.String)
        System.Object NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetAppenderFactory::CreateConsoleAppender(System.String)
        System.Object NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetAppenderFactory::CreateRollingFileAppender(System.String,System.String)

    NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetConfigurator
        System.Boolean NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetConfigurator::get_Log4NetExists()
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetConfigurator::Configure()
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetConfigurator::Configure(System.Object,System.String)

    NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger
        System.Boolean NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::get_IsDebugEnabled()
        System.Boolean NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::get_IsErrorEnabled()
        System.Boolean NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::get_IsFatalEnabled()
        System.Boolean NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::get_IsInfoEnabled()
        System.Boolean NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::get_IsWarnEnabled()
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::Debug(System.String)
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::Debug(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::DebugFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::Error(System.String)
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::Error(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::ErrorFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::Fatal(System.String)
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::Fatal(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::FatalFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::Info(System.String)
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::Info(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::InfoFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::Warn(System.String)
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::Warn(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLogger::WarnFormat(System.String,System.Object[])

    NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLoggerFactory
        NServiceBus.Logging.ILog NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLoggerFactory::GetLogger(System.String)
        NServiceBus.Logging.ILog NServiceBus.Logging.Loggers.Log4NetAdapter.Log4NetLoggerFactory::GetLogger(System.Type)

    NServiceBus.Logging.Loggers.NLogAdapter.NLogConfigurator
        System.Boolean NServiceBus.Logging.Loggers.NLogAdapter.NLogConfigurator::get_NLogExists()
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogConfigurator::Configure()
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogConfigurator::Configure(System.Object,System.String)
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogConfigurator::Configure(System.Object[],System.String)

    NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger
        System.Boolean NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::get_IsDebugEnabled()
        System.Boolean NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::get_IsErrorEnabled()
        System.Boolean NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::get_IsFatalEnabled()
        System.Boolean NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::get_IsInfoEnabled()
        System.Boolean NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::get_IsWarnEnabled()
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::Debug(System.String)
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::Debug(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::DebugFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::Error(System.String)
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::Error(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::ErrorFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::Fatal(System.String)
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::Fatal(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::FatalFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::Info(System.String)
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::Info(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::InfoFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::Warn(System.String)
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::Warn(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.NLogAdapter.NLogLogger::WarnFormat(System.String,System.Object[])

    NServiceBus.Logging.Loggers.NLogAdapter.NLogLoggerFactory
        NServiceBus.Logging.ILog NServiceBus.Logging.Loggers.NLogAdapter.NLogLoggerFactory::GetLogger(System.String)
        NServiceBus.Logging.ILog NServiceBus.Logging.Loggers.NLogAdapter.NLogLoggerFactory::GetLogger(System.Type)

    NServiceBus.Logging.Loggers.NLogAdapter.NLogTargetFactory
        System.Object NServiceBus.Logging.Loggers.NLogAdapter.NLogTargetFactory::CreateColoredConsoleTarget(System.String)
        System.Object NServiceBus.Logging.Loggers.NLogAdapter.NLogTargetFactory::CreateConsoleTarget(System.String)
        System.Object NServiceBus.Logging.Loggers.NLogAdapter.NLogTargetFactory::CreateRollingFileTarget(System.String,System.String)

    NServiceBus.Logging.Loggers.NullLogger
        System.Boolean NServiceBus.Logging.Loggers.NullLogger::get_IsDebugEnabled()
        System.Boolean NServiceBus.Logging.Loggers.NullLogger::get_IsErrorEnabled()
        System.Boolean NServiceBus.Logging.Loggers.NullLogger::get_IsFatalEnabled()
        System.Boolean NServiceBus.Logging.Loggers.NullLogger::get_IsInfoEnabled()
        System.Boolean NServiceBus.Logging.Loggers.NullLogger::get_IsWarnEnabled()
        System.Void NServiceBus.Logging.Loggers.NullLogger::Debug(System.String)
        System.Void NServiceBus.Logging.Loggers.NullLogger::Debug(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.NullLogger::DebugFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.NullLogger::Error(System.String)
        System.Void NServiceBus.Logging.Loggers.NullLogger::Error(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.NullLogger::ErrorFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.NullLogger::Fatal(System.String)
        System.Void NServiceBus.Logging.Loggers.NullLogger::Fatal(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.NullLogger::FatalFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.NullLogger::Info(System.String)
        System.Void NServiceBus.Logging.Loggers.NullLogger::Info(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.NullLogger::InfoFormat(System.String,System.Object[])
        System.Void NServiceBus.Logging.Loggers.NullLogger::Warn(System.String)
        System.Void NServiceBus.Logging.Loggers.NullLogger::Warn(System.String,System.Exception)
        System.Void NServiceBus.Logging.Loggers.NullLogger::WarnFormat(System.String,System.Object[])

    NServiceBus.Logging.Loggers.NullLoggerFactory
        NServiceBus.Logging.ILog NServiceBus.Logging.Loggers.NullLoggerFactory::GetLogger(System.String)
        NServiceBus.Logging.ILog NServiceBus.Logging.Loggers.NullLoggerFactory::GetLogger(System.Type)

    NServiceBus.MessageInterfaces.MessageMapper.Reflection.MessageMapper
        System.String NServiceBus.MessageInterfaces.MessageMapper.Reflection.MessageMapper::GetNewTypeName(System.Type)
        System.Type NServiceBus.MessageInterfaces.MessageMapper.Reflection.MessageMapper::CreateTypeFrom(System.Type,System.Reflection.Emit.ModuleBuilder)
        System.Void NServiceBus.MessageInterfaces.MessageMapper.Reflection.MessageMapper::InitType(System.Type,System.Reflection.Emit.ModuleBuilder)

    NServiceBus.MessageMutator.ApplyIncomingTransportMessageMutatorsBehavior
        System.Void NServiceBus.MessageMutator.ApplyIncomingTransportMessageMutatorsBehavior::Invoke(NServiceBus.Pipeline.Contexts.ReceivePhysicalMessageContext,System.Action)

    NServiceBus.MessageMutator.IMutateOutgoingTransportMessages
        System.Void NServiceBus.MessageMutator.IMutateOutgoingTransportMessages::MutateOutgoing(System.Object[],NServiceBus.TransportMessage)

    NServiceBus.MessageMutator.MutateOutgoingMessageBehavior
        System.Void NServiceBus.MessageMutator.MutateOutgoingMessageBehavior::Invoke(NServiceBus.Pipeline.Contexts.SendLogicalMessageContext,System.Action)

    NServiceBus.MessageMutator.MutateOutgoingPhysicalMessageBehavior
        System.Void NServiceBus.MessageMutator.MutateOutgoingPhysicalMessageBehavior::Invoke(NServiceBus.Pipeline.Contexts.SendPhysicalMessageContext,System.Action)

    NServiceBus.MonitoringConfig
        System.Boolean NServiceBus.MonitoringConfig::PerformanceCountersEnabled(NServiceBus.Configure)
        System.TimeSpan NServiceBus.MonitoringConfig::EndpointSLA(NServiceBus.Configure)

    NServiceBus.ObjectBuilder.Common.CommonObjectBuilder
        NServiceBus.ObjectBuilder.IComponentConfig NServiceBus.ObjectBuilder.Common.CommonObjectBuilder::ConfigureComponent(System.Type,NServiceBus.ObjectBuilder.ComponentCallModelEnum)
        NServiceBus.ObjectBuilder.IComponentConfig`1<T> NServiceBus.ObjectBuilder.Common.CommonObjectBuilder::ConfigureComponent(NServiceBus.ObjectBuilder.ComponentCallModelEnum)
        System.Void NServiceBus.ObjectBuilder.Common.CommonObjectBuilder::DisposeManaged()

    NServiceBus.ObjectBuilder.IConfigureComponents
        System.Boolean NServiceBus.ObjectBuilder.IConfigureComponents::HasComponent(System.Type)

    NServiceBus.Pipeline.BehaviorContext
        System.Boolean NServiceBus.Pipeline.BehaviorContext::get_ChainAborted()
        System.Void NServiceBus.Pipeline.BehaviorContext::AbortChain()

    NServiceBus.Pipeline.IBehavior`1
        System.Void NServiceBus.Pipeline.IBehavior`1::Invoke(T,System.Action)

    NServiceBus.Pipeline.MessageMutator.ApplyIncomingMessageMutatorsBehavior
        System.Void NServiceBus.Pipeline.MessageMutator.ApplyIncomingMessageMutatorsBehavior::Invoke(NServiceBus.Pipeline.Contexts.ReceiveLogicalMessageContext,System.Action)

    NServiceBus.Pipeline.PipelineBuilder
        System.Collections.Generic.List`1<System.Type> NServiceBus.Pipeline.PipelineBuilder::get_handlerInvocationBehaviorList()
        System.Collections.Generic.List`1<System.Type> NServiceBus.Pipeline.PipelineBuilder::get_receiveLogicalMessageBehaviorList()
        System.Collections.Generic.List`1<System.Type> NServiceBus.Pipeline.PipelineBuilder::get_receivePhysicalMessageBehaviorList()
        System.Collections.Generic.List`1<System.Type> NServiceBus.Pipeline.PipelineBuilder::get_sendLogicalMessageBehaviorList()
        System.Collections.Generic.List`1<System.Type> NServiceBus.Pipeline.PipelineBuilder::get_sendLogicalMessagesBehaviorList()
        System.Collections.Generic.List`1<System.Type> NServiceBus.Pipeline.PipelineBuilder::get_sendPhysicalMessageBehaviorList()

    NServiceBus.Pipeline.PipelineExecutor
        NServiceBus.Pipeline.Contexts.HandlerInvocationContext NServiceBus.Pipeline.PipelineExecutor::InvokeHandlerPipeline(NServiceBus.Unicast.Behaviors.MessageHandler)
        NServiceBus.Pipeline.Contexts.SendLogicalMessageContext NServiceBus.Pipeline.PipelineExecutor::InvokeSendPipeline(NServiceBus.Unicast.SendOptions,NServiceBus.Unicast.Messages.LogicalMessage)
        NServiceBus.Pipeline.Contexts.SendLogicalMessagesContext NServiceBus.Pipeline.PipelineExecutor::InvokeSendPipeline(NServiceBus.Unicast.SendOptions,System.Collections.Generic.IEnumerable`1<NServiceBus.Unicast.Messages.LogicalMessage>)
        System.Void NServiceBus.Pipeline.PipelineExecutor::CompletePhysicalMessagePipelineContext()
        System.Void NServiceBus.Pipeline.PipelineExecutor::DisposeManaged()
        System.Void NServiceBus.Pipeline.PipelineExecutor::InvokeLogicalMessagePipeline(NServiceBus.Unicast.Messages.LogicalMessage)
        System.Void NServiceBus.Pipeline.PipelineExecutor::InvokeReceivePhysicalMessagePipeline()
        System.Void NServiceBus.Pipeline.PipelineExecutor::InvokeSendPipeline(NServiceBus.Unicast.SendOptions,NServiceBus.TransportMessage)
        System.Void NServiceBus.Pipeline.PipelineExecutor::PreparePhysicalMessagePipelineContext(NServiceBus.TransportMessage,System.Boolean)

    NServiceBus.Sagas.ActiveSagaInstance
        NServiceBus.Saga.ISaga NServiceBus.Sagas.ActiveSagaInstance::get_Instance()
        System.Void NServiceBus.Sagas.ActiveSagaInstance::AttachExistingEntity(NServiceBus.Saga.IContainSagaData)
        System.Void NServiceBus.Sagas.ActiveSagaInstance::MarkAsNotFound()

    NServiceBus.Sagas.Finders.PropertySagaFinder`2
        System.Reflection.PropertyInfo NServiceBus.Sagas.Finders.PropertySagaFinder`2::get_MessageProperty()
        System.Reflection.PropertyInfo NServiceBus.Sagas.Finders.PropertySagaFinder`2::get_SagaProperty()
        System.Void NServiceBus.Sagas.Finders.PropertySagaFinder`2::set_MessageProperty(System.Reflection.PropertyInfo)
        System.Void NServiceBus.Sagas.Finders.PropertySagaFinder`2::set_SagaProperty(System.Reflection.PropertyInfo)
        TSaga NServiceBus.Sagas.Finders.PropertySagaFinder`2::FindBy(TMessage)

    NServiceBus.Sagas.SagaPersistenceBehavior
        System.Void NServiceBus.Sagas.SagaPersistenceBehavior::Invoke(NServiceBus.Pipeline.Contexts.HandlerInvocationContext,System.Action)

    NServiceBus.Satellites.SatelliteLauncher
        NServiceBus.ObjectBuilder.IBuilder NServiceBus.Satellites.SatelliteLauncher::get_Builder()
        System.Void NServiceBus.Satellites.SatelliteLauncher::set_Builder(NServiceBus.ObjectBuilder.IBuilder)

    NServiceBus.Satellites.SatellitesQueuesCreator
        System.Void NServiceBus.Satellites.SatellitesQueuesCreator::Install(System.String)

    NServiceBus.Scheduling.DefaultScheduler
        System.Void NServiceBus.Scheduling.DefaultScheduler::Schedule(NServiceBus.Scheduling.ScheduledTask)

    NServiceBus.Serializers.Binary.BinaryMessageSerializer
        System.Void NServiceBus.Serializers.Binary.BinaryMessageSerializer::Serialize(System.Object[],System.IO.Stream)

    NServiceBus.Serializers.Json.JsonMessageSerializer
        T NServiceBus.Serializers.Json.JsonMessageSerializer::DeserializeObject(System.String)

    NServiceBus.Serializers.Json.JsonMessageSerializerBase
        System.Void NServiceBus.Serializers.Json.JsonMessageSerializerBase::Serialize(System.Object[],System.IO.Stream)

    NServiceBus.Serializers.XML.Config.MessageTypesInitializer
        System.Void NServiceBus.Serializers.XML.Config.MessageTypesInitializer::Run()

    NServiceBus.Serializers.XML.XmlMessageSerializer
        System.Boolean NServiceBus.Serializers.XML.XmlMessageSerializer::get_SkipWrappingElementForSingleMessages()
        System.Void NServiceBus.Serializers.XML.XmlMessageSerializer::Serialize(System.Object[],System.IO.Stream)
        System.Void NServiceBus.Serializers.XML.XmlMessageSerializer::set_SkipWrappingElementForSingleMessages(System.Boolean)

    NServiceBus.Settings.ScaleOutSettings
        NServiceBus.Settings.ScaleOutSettings NServiceBus.Settings.ScaleOutSettings::UseSingleBrokerQueue()
        NServiceBus.Settings.ScaleOutSettings NServiceBus.Settings.ScaleOutSettings::UseUniqueBrokerQueuePerMachine()

    NServiceBus.Settings.SettingsHolder
        System.Void NServiceBus.Settings.SettingsHolder::ApplyTo()
        System.Void NServiceBus.Settings.SettingsHolder::PreventChanges()
        System.Void NServiceBus.Settings.SettingsHolder::Reset()

    NServiceBus.SLAInitializer
        System.Void NServiceBus.SLAInitializer::Init()

    NServiceBus.Timeout.Core.DefaultTimeoutManager
        System.Void NServiceBus.Timeout.Core.DefaultTimeoutManager::add_TimeoutPushed(System.EventHandler`1<NServiceBus.Timeout.Core.TimeoutData>)
        System.Void NServiceBus.Timeout.Core.DefaultTimeoutManager::remove_TimeoutPushed(System.EventHandler`1<NServiceBus.Timeout.Core.TimeoutData>)

    NServiceBus.Timeout.Core.IManageTimeouts
        System.Void NServiceBus.Timeout.Core.IManageTimeouts::add_TimeoutPushed(System.EventHandler`1<NServiceBus.Timeout.Core.TimeoutData>)
        System.Void NServiceBus.Timeout.Core.IManageTimeouts::PushTimeout(NServiceBus.Timeout.Core.TimeoutData)
        System.Void NServiceBus.Timeout.Core.IManageTimeouts::remove_TimeoutPushed(System.EventHandler`1<NServiceBus.Timeout.Core.TimeoutData>)
        System.Void NServiceBus.Timeout.Core.IManageTimeouts::RemoveTimeout(System.String)
        System.Void NServiceBus.Timeout.Core.IManageTimeouts::RemoveTimeoutBy(System.Guid)

    NServiceBus.Timeout.Core.IPersistTimeouts
        System.Collections.Generic.List`1<System.Tuple`2<System.String,System.DateTime>> NServiceBus.Timeout.Core.IPersistTimeouts::GetNextChunk(System.DateTime,System.DateTime&)

    NServiceBus.Timeout.Core.TimeoutData
        System.String NServiceBus.Timeout.Core.TimeoutData::get_CorrelationId()
        System.Void NServiceBus.Timeout.Core.TimeoutData::set_CorrelationId(System.String)

    NServiceBus.Timeout.Hosting.Windows.TimeoutMessageProcessor
        NServiceBus.Timeout.Core.IManageTimeouts NServiceBus.Timeout.Hosting.Windows.TimeoutMessageProcessor::get_TimeoutManager()
        System.Void NServiceBus.Timeout.Hosting.Windows.TimeoutMessageProcessor::set_TimeoutManager(NServiceBus.Timeout.Core.IManageTimeouts)

    NServiceBus.Timeout.Hosting.Windows.TimeoutPersisterReceiver
        NServiceBus.Timeout.Core.IManageTimeouts NServiceBus.Timeout.Hosting.Windows.TimeoutPersisterReceiver::get_TimeoutManager()
        System.Void NServiceBus.Timeout.Hosting.Windows.TimeoutPersisterReceiver::set_TimeoutManager(NServiceBus.Timeout.Core.IManageTimeouts)

    NServiceBus.Timeout.TimeoutManagerDeferrer
        System.Void NServiceBus.Timeout.TimeoutManagerDeferrer::Defer(NServiceBus.TransportMessage,System.DateTime,NServiceBus.Address)

    NServiceBus.TransportMessage
        System.String NServiceBus.TransportMessage::get_IdForCorrelation()
        System.Void NServiceBus.TransportMessage::set_ReplyToAddress(NServiceBus.Address)

    NServiceBus.TransportReceiverConfig
        NServiceBus.Configure NServiceBus.TransportReceiverConfig::UseTransport(NServiceBus.Configure,System.String)
        NServiceBus.Configure NServiceBus.TransportReceiverConfig::UseTransport(NServiceBus.Configure,System.Type,System.Func`1<System.String>)
        NServiceBus.Configure NServiceBus.TransportReceiverConfig::UseTransport(NServiceBus.Configure,System.Type,System.String)

    NServiceBus.Transports.IDeferMessages
        System.Void NServiceBus.Transports.IDeferMessages::Defer(NServiceBus.TransportMessage,System.DateTime,NServiceBus.Address)

    NServiceBus.Transports.IPublishMessages
        System.Boolean NServiceBus.Transports.IPublishMessages::Publish(NServiceBus.TransportMessage,System.Collections.Generic.IEnumerable`1<System.Type>)

    NServiceBus.Transports.ISendMessages
        System.Void NServiceBus.Transports.ISendMessages::Send(NServiceBus.TransportMessage,NServiceBus.Address)

    NServiceBus.Transports.Msmq.CorrelationIdMutatorForBackwardsCompatibilityWithV3
        System.Void NServiceBus.Transports.Msmq.CorrelationIdMutatorForBackwardsCompatibilityWithV3::MutateOutgoing(System.Object[],NServiceBus.TransportMessage)

    NServiceBus.Transports.Msmq.MsmqMessageSender
        System.Void NServiceBus.Transports.Msmq.MsmqMessageSender::Send(NServiceBus.TransportMessage,NServiceBus.Address)

    NServiceBus.Transports.Msmq.MsmqUnitOfWork
        System.Void NServiceBus.Transports.Msmq.MsmqUnitOfWork::ClearTransaction()
        System.Void NServiceBus.Transports.Msmq.MsmqUnitOfWork::SetTransaction(System.Messaging.MessageQueueTransaction)

    NServiceBus.Transports.Msmq.MsmqUtilities
        NServiceBus.Address NServiceBus.Transports.Msmq.MsmqUtilities::GetIndependentAddressForQueue(System.Messaging.MessageQueue)
        NServiceBus.TransportMessage NServiceBus.Transports.Msmq.MsmqUtilities::Convert(System.Messaging.Message)
        System.Messaging.Message NServiceBus.Transports.Msmq.MsmqUtilities::Convert(NServiceBus.TransportMessage)
        System.String NServiceBus.Transports.Msmq.MsmqUtilities::GetFullPath(NServiceBus.Address)
        System.String NServiceBus.Transports.Msmq.MsmqUtilities::GetReturnAddress(NServiceBus.Address,NServiceBus.Address)
        System.String NServiceBus.Transports.Msmq.MsmqUtilities::GetReturnAddress(System.String,System.String)

    NServiceBus.Unicast.BackwardCompatibility.MutateMessageContentTypeOfIncomingTransportMessages
        System.Void NServiceBus.Unicast.BackwardCompatibility.MutateMessageContentTypeOfIncomingTransportMessages::Init()

    NServiceBus.Unicast.Behaviors.CallbackInvocationBehavior
        System.Void NServiceBus.Unicast.Behaviors.CallbackInvocationBehavior::Invoke(NServiceBus.Pipeline.Contexts.ReceivePhysicalMessageContext,System.Action)

    NServiceBus.Unicast.Behaviors.ChildContainerBehavior
        System.Void NServiceBus.Unicast.Behaviors.ChildContainerBehavior::Invoke(NServiceBus.Pipeline.Contexts.ReceivePhysicalMessageContext,System.Action)

    NServiceBus.Unicast.Behaviors.CreatePhysicalMessageBehavior
        System.Void NServiceBus.Unicast.Behaviors.CreatePhysicalMessageBehavior::Invoke(NServiceBus.Pipeline.Contexts.SendLogicalMessagesContext,System.Action)

    NServiceBus.Unicast.Behaviors.DispatchMessageToTransportBehavior
        System.Void NServiceBus.Unicast.Behaviors.DispatchMessageToTransportBehavior::Invoke(NServiceBus.Pipeline.Contexts.SendPhysicalMessageContext,System.Action)

    NServiceBus.Unicast.Behaviors.ForwardBehavior
        NServiceBus.Transports.ISendMessages NServiceBus.Unicast.Behaviors.ForwardBehavior::get_MessageSender()
        NServiceBus.Unicast.UnicastBus NServiceBus.Unicast.Behaviors.ForwardBehavior::get_UnicastBus()
        System.TimeSpan NServiceBus.Unicast.Behaviors.ForwardBehavior::get_TimeToBeReceivedOnForwardedMessages()
        System.Void NServiceBus.Unicast.Behaviors.ForwardBehavior::Invoke(NServiceBus.Pipeline.Contexts.ReceivePhysicalMessageContext,System.Action)
        System.Void NServiceBus.Unicast.Behaviors.ForwardBehavior::set_MessageSender(NServiceBus.Transports.ISendMessages)
        System.Void NServiceBus.Unicast.Behaviors.ForwardBehavior::set_TimeToBeReceivedOnForwardedMessages(System.TimeSpan)
        System.Void NServiceBus.Unicast.Behaviors.ForwardBehavior::set_UnicastBus(NServiceBus.Unicast.UnicastBus)

    NServiceBus.Unicast.Behaviors.InvokeHandlersBehavior
        System.Collections.Generic.IDictionary`2<System.Type,System.Type> NServiceBus.Unicast.Behaviors.InvokeHandlersBehavior::get_MessageDispatcherMappings()
        System.Void NServiceBus.Unicast.Behaviors.InvokeHandlersBehavior::Invoke(NServiceBus.Pipeline.Contexts.HandlerInvocationContext,System.Action)
        System.Void NServiceBus.Unicast.Behaviors.InvokeHandlersBehavior::set_MessageDispatcherMappings(System.Collections.Generic.IDictionary`2<System.Type,System.Type>)

    NServiceBus.Unicast.Behaviors.LoadHandlersBehavior
        System.Void NServiceBus.Unicast.Behaviors.LoadHandlersBehavior::Invoke(NServiceBus.Pipeline.Contexts.ReceiveLogicalMessageContext,System.Action)

    NServiceBus.Unicast.Behaviors.SendValidatorBehavior
        System.Void NServiceBus.Unicast.Behaviors.SendValidatorBehavior::Invoke(NServiceBus.Pipeline.Contexts.SendLogicalMessageContext,System.Action)

    NServiceBus.Unicast.Behaviors.SerializeMessagesBehavior
        System.Void NServiceBus.Unicast.Behaviors.SerializeMessagesBehavior::Invoke(NServiceBus.Pipeline.Contexts.SendPhysicalMessageContext,System.Action)

    NServiceBus.Unicast.Behaviors.SetCurrentMessageBeingHandledBehavior
        System.Void NServiceBus.Unicast.Behaviors.SetCurrentMessageBeingHandledBehavior::Invoke(NServiceBus.Pipeline.Contexts.HandlerInvocationContext,System.Action)

    NServiceBus.Unicast.Callback
        System.IAsyncResult NServiceBus.Unicast.Callback::Register(System.AsyncCallback,System.Object)
        System.String NServiceBus.Unicast.Callback::get_MessageId()
        System.Threading.Tasks.Task NServiceBus.Unicast.Callback::Register(System.Action`1<NServiceBus.CompletionResult>)
        System.Threading.Tasks.Task`1<System.Int32> NServiceBus.Unicast.Callback::Register()
        System.Threading.Tasks.Task`1<T> NServiceBus.Unicast.Callback::Register()
        System.Threading.Tasks.Task`1<T> NServiceBus.Unicast.Callback::Register(System.Func`2<NServiceBus.CompletionResult,T>)
        System.Void NServiceBus.Unicast.Callback::add_Registered(System.EventHandler`1<NServiceBus.Unicast.BusAsyncResultEventArgs>)
        System.Void NServiceBus.Unicast.Callback::Register(System.Action`1<T>)
        System.Void NServiceBus.Unicast.Callback::Register(System.Action`1<T>,System.Object)
        System.Void NServiceBus.Unicast.Callback::remove_Registered(System.EventHandler`1<NServiceBus.Unicast.BusAsyncResultEventArgs>)

    NServiceBus.Unicast.Config.ConfigUnicastBus
        NServiceBus.Unicast.Config.ConfigUnicastBus NServiceBus.Unicast.Config.ConfigUnicastBus::DefaultDispatcherFactory()
        NServiceBus.Unicast.Config.ConfigUnicastBus NServiceBus.Unicast.Config.ConfigUnicastBus::ForwardReceivedMessagesTo(System.String)
        NServiceBus.Unicast.Config.ConfigUnicastBus NServiceBus.Unicast.Config.ConfigUnicastBus::LoadMessageHandlers()
        NServiceBus.Unicast.Config.ConfigUnicastBus NServiceBus.Unicast.Config.ConfigUnicastBus::LoadMessageHandlers()
        NServiceBus.Unicast.Config.ConfigUnicastBus NServiceBus.Unicast.Config.ConfigUnicastBus::LoadMessageHandlers(NServiceBus.First`1<T>)
        NServiceBus.Unicast.Config.ConfigUnicastBus NServiceBus.Unicast.Config.ConfigUnicastBus::PropagateReturnAddressOnSend(System.Boolean)
        System.Void NServiceBus.Unicast.Config.ConfigUnicastBus::Configure(NServiceBus.Configure)

    NServiceBus.Unicast.IUnicastBus
        System.Void NServiceBus.Unicast.IUnicastBus::add_ClientSubscribed(System.EventHandler`1<NServiceBus.Unicast.Subscriptions.SubscriptionEventArgs>)
        System.Void NServiceBus.Unicast.IUnicastBus::add_MessagesSent(System.EventHandler`1<NServiceBus.Unicast.MessagesEventArgs>)
        System.Void NServiceBus.Unicast.IUnicastBus::add_NoSubscribersForMessage(System.EventHandler`1<NServiceBus.Unicast.MessageEventArgs>)
        System.Void NServiceBus.Unicast.IUnicastBus::remove_ClientSubscribed(System.EventHandler`1<NServiceBus.Unicast.Subscriptions.SubscriptionEventArgs>)
        System.Void NServiceBus.Unicast.IUnicastBus::remove_MessagesSent(System.EventHandler`1<NServiceBus.Unicast.MessagesEventArgs>)
        System.Void NServiceBus.Unicast.IUnicastBus::remove_NoSubscribersForMessage(System.EventHandler`1<NServiceBus.Unicast.MessageEventArgs>)

    NServiceBus.Unicast.Messages.ExecuteLogicalMessagesBehavior
        System.Void NServiceBus.Unicast.Messages.ExecuteLogicalMessagesBehavior::Invoke(NServiceBus.Pipeline.Contexts.ReceivePhysicalMessageContext,System.Action)

    NServiceBus.Unicast.Messages.LogicalMessageFactory
        NServiceBus.MessageInterfaces.IMessageMapper NServiceBus.Unicast.Messages.LogicalMessageFactory::get_MessageMapper()
        NServiceBus.Pipeline.PipelineExecutor NServiceBus.Unicast.Messages.LogicalMessageFactory::get_PipelineExecutor()
        NServiceBus.Unicast.Messages.LogicalMessage NServiceBus.Unicast.Messages.LogicalMessageFactory::Create(System.Object)
        NServiceBus.Unicast.Messages.LogicalMessage NServiceBus.Unicast.Messages.LogicalMessageFactory::Create(System.Type,System.Object)
        NServiceBus.Unicast.Messages.LogicalMessage NServiceBus.Unicast.Messages.LogicalMessageFactory::Create(System.Type,System.Object,System.Collections.Generic.Dictionary`2<System.String,System.String>)
        NServiceBus.Unicast.Messages.MessageMetadataRegistry NServiceBus.Unicast.Messages.LogicalMessageFactory::get_MessageMetadataRegistry()
        System.Void NServiceBus.Unicast.Messages.LogicalMessageFactory::set_MessageMapper(NServiceBus.MessageInterfaces.IMessageMapper)
        System.Void NServiceBus.Unicast.Messages.LogicalMessageFactory::set_MessageMetadataRegistry(NServiceBus.Unicast.Messages.MessageMetadataRegistry)
        System.Void NServiceBus.Unicast.Messages.LogicalMessageFactory::set_PipelineExecutor(NServiceBus.Pipeline.PipelineExecutor)

    NServiceBus.Unicast.Messages.MessageMetadata
        System.Void NServiceBus.Unicast.Messages.MessageMetadata::set_MessageHierarchy(System.Collections.Generic.IEnumerable`1<System.Type>)
        System.Void NServiceBus.Unicast.Messages.MessageMetadata::set_MessageType(System.Type)
        System.Void NServiceBus.Unicast.Messages.MessageMetadata::set_Recoverable(System.Boolean)
        System.Void NServiceBus.Unicast.Messages.MessageMetadata::set_TimeToBeReceived(System.TimeSpan)

    NServiceBus.Unicast.Messages.MessageMetadataRegistry
        NServiceBus.Unicast.Messages.MessageMetadata NServiceBus.Unicast.Messages.MessageMetadataRegistry::GetMessageDefinition(System.Type)
        System.Boolean NServiceBus.Unicast.Messages.MessageMetadataRegistry::get_DefaultToNonPersistentMessages()
        System.Boolean NServiceBus.Unicast.Messages.MessageMetadataRegistry::HasDefinitionFor(System.Type)
        System.Collections.Generic.IEnumerable`1<NServiceBus.Unicast.Messages.MessageMetadata> NServiceBus.Unicast.Messages.MessageMetadataRegistry::GetAllMessages()
        System.Void NServiceBus.Unicast.Messages.MessageMetadataRegistry::RegisterMessageType(System.Type)
        System.Void NServiceBus.Unicast.Messages.MessageMetadataRegistry::set_DefaultToNonPersistentMessages(System.Boolean)

    NServiceBus.Unicast.MessagingBestPractices
        System.Void NServiceBus.Unicast.MessagingBestPractices::AssertIsValidForPubSub(System.Type)
        System.Void NServiceBus.Unicast.MessagingBestPractices::AssertIsValidForReply(System.Collections.Generic.IEnumerable`1<System.Object>)
        System.Void NServiceBus.Unicast.MessagingBestPractices::AssertIsValidForSend(System.Type,NServiceBus.MessageIntentEnum)

    NServiceBus.Unicast.Monitoring.CausationMutator
        System.Void NServiceBus.Unicast.Monitoring.CausationMutator::Init()
        System.Void NServiceBus.Unicast.Monitoring.CausationMutator::MutateOutgoing(System.Object[],NServiceBus.TransportMessage)

    NServiceBus.Unicast.Monitoring.CriticalTimeCalculator
        System.Void NServiceBus.Unicast.Monitoring.CriticalTimeCalculator::DisposeManaged()

    NServiceBus.Unicast.Monitoring.EstimatedTimeToSLABreachCalculator
        System.Void NServiceBus.Unicast.Monitoring.EstimatedTimeToSLABreachCalculator::DisposeManaged()

    NServiceBus.Unicast.Monitoring.PerformanceCounterInitializer
        System.Void NServiceBus.Unicast.Monitoring.PerformanceCounterInitializer::Run()

    NServiceBus.Unicast.Monitoring.ProcessingStatistics
        System.Void NServiceBus.Unicast.Monitoring.ProcessingStatistics::Init()

    NServiceBus.Unicast.Publishing.StorageDrivenPublisher
        System.Boolean NServiceBus.Unicast.Publishing.StorageDrivenPublisher::Publish(NServiceBus.TransportMessage,System.Collections.Generic.IEnumerable`1<System.Type>)

    NServiceBus.Unicast.Queuing.Installers.AuditQueueCreator
        NServiceBus.Audit.MessageAuditer NServiceBus.Unicast.Queuing.Installers.AuditQueueCreator::get_Auditer()
        System.Boolean NServiceBus.Unicast.Queuing.Installers.AuditQueueCreator::get_IsDisabled()
        System.Void NServiceBus.Unicast.Queuing.Installers.AuditQueueCreator::set_Auditer(NServiceBus.Audit.MessageAuditer)

    NServiceBus.Unicast.Queuing.Installers.EndpointInputQueueCreator
        System.Boolean NServiceBus.Unicast.Queuing.Installers.EndpointInputQueueCreator::get_IsDisabled()

    NServiceBus.Unicast.Queuing.Installers.ForwardReceivedMessagesToQueueCreator
        System.Boolean NServiceBus.Unicast.Queuing.Installers.ForwardReceivedMessagesToQueueCreator::get_IsDisabled()

    NServiceBus.Unicast.Queuing.IWantQueueCreated
        System.Boolean NServiceBus.Unicast.Queuing.IWantQueueCreated::get_IsDisabled()

    NServiceBus.Unicast.Queuing.QueuesCreator
        System.Void NServiceBus.Unicast.Queuing.QueuesCreator::Init()
        System.Void NServiceBus.Unicast.Queuing.QueuesCreator::Install(System.String)

    NServiceBus.Unicast.SendOptions
        NServiceBus.Address NServiceBus.Unicast.SendOptions::get_ReplyToAddress()
        NServiceBus.MessageIntentEnum NServiceBus.Unicast.SendOptions::get_Intent()
        NServiceBus.Unicast.SendOptions NServiceBus.Unicast.SendOptions::ReplyTo(NServiceBus.Address)
        System.Boolean NServiceBus.Unicast.SendOptions::get_EnforceMessagingBestPractices()
        System.Nullable`1<System.TimeSpan> NServiceBus.Unicast.SendOptions::get_DelayDeliveryWith()
        System.Void NServiceBus.Unicast.SendOptions::set_DelayDeliveryWith(System.Nullable`1<System.TimeSpan>)
        System.Void NServiceBus.Unicast.SendOptions::set_EnforceMessagingBestPractices(System.Boolean)
        System.Void NServiceBus.Unicast.SendOptions::set_Intent(NServiceBus.MessageIntentEnum)
        System.Void NServiceBus.Unicast.SendOptions::set_ReplyToAddress(NServiceBus.Address)

    NServiceBus.Unicast.Transport.ControlMessage
        NServiceBus.TransportMessage NServiceBus.Unicast.Transport.ControlMessage::Create(NServiceBus.Address)

    NServiceBus.Unicast.Transport.ITransport
        System.Int32 NServiceBus.Unicast.Transport.ITransport::get_MaxThroughputPerSecond()
        System.Int32 NServiceBus.Unicast.Transport.ITransport::get_NumberOfWorkerThreads()
        System.Void NServiceBus.Unicast.Transport.ITransport::AbortHandlingCurrentMessage()
        System.Void NServiceBus.Unicast.Transport.ITransport::add_FailedMessageProcessing(System.EventHandler`1<NServiceBus.Unicast.Transport.FailedMessageProcessingEventArgs>)
        System.Void NServiceBus.Unicast.Transport.ITransport::remove_FailedMessageProcessing(System.EventHandler`1<NServiceBus.Unicast.Transport.FailedMessageProcessingEventArgs>)
        System.Void NServiceBus.Unicast.Transport.ITransport::set_MaxThroughputPerSecond(System.Int32)
        System.Void NServiceBus.Unicast.Transport.ITransport::Stop()

    NServiceBus.Unicast.Transport.Monitoring.ReceivePerformanceDiagnostics
        System.Void NServiceBus.Unicast.Transport.Monitoring.ReceivePerformanceDiagnostics::DisposeManaged()

    NServiceBus.Unicast.Transport.TransactionSettings
        NServiceBus.Unicast.Transport.TransactionSettings NServiceBus.Unicast.Transport.TransactionSettings::get_Default()
        System.Boolean NServiceBus.Unicast.Transport.TransactionSettings::get_DontUseDistributedTransactions()
        System.Void NServiceBus.Unicast.Transport.TransactionSettings::set_DontUseDistributedTransactions(System.Boolean)

    NServiceBus.Unicast.Transport.TransportConnectionString
        System.String NServiceBus.Unicast.Transport.TransportConnectionString::DefaultConnectionStringName
        System.String NServiceBus.Unicast.Transport.TransportConnectionString::GetConnectionStringOrNull(System.String)
        System.Void NServiceBus.Unicast.Transport.TransportConnectionString::Override(System.Func`1<System.String>)

    NServiceBus.Unicast.Transport.TransportReceiver
        System.Int32 NServiceBus.Unicast.Transport.TransportReceiver::get_MaxThroughputPerSecond()
        System.Int32 NServiceBus.Unicast.Transport.TransportReceiver::get_NumberOfWorkerThreads()
        System.Void NServiceBus.Unicast.Transport.TransportReceiver::ChangeNumberOfWorkerThreads(System.Int32)
        System.Void NServiceBus.Unicast.Transport.TransportReceiver::DisposeManaged()
        System.Void NServiceBus.Unicast.Transport.TransportReceiver::set_MaximumConcurrencyLevel(System.Int32)
        System.Void NServiceBus.Unicast.Transport.TransportReceiver::set_MaxThroughputPerSecond(System.Int32)
        System.Void NServiceBus.Unicast.Transport.TransportReceiver::set_Receiver(NServiceBus.Transports.IDequeueMessages)
        System.Void NServiceBus.Unicast.Transport.TransportReceiver::set_TransactionSettings(NServiceBus.Unicast.Transport.TransactionSettings)
        System.Void NServiceBus.Unicast.Transport.TransportReceiver::Start(System.String)

    NServiceBus.Unicast.UnicastBus
        NServiceBus.Address NServiceBus.Unicast.UnicastBus::get_ForwardReceivedMessagesTo()
        NServiceBus.Address NServiceBus.Unicast.UnicastBus::get_InputAddress()
        NServiceBus.Address NServiceBus.Unicast.UnicastBus::get_MasterNodeAddress()
        NServiceBus.Audit.MessageAuditer NServiceBus.Unicast.UnicastBus::get_MessageAuditer()
        NServiceBus.IBus NServiceBus.Unicast.UnicastBus::Start()
        NServiceBus.IBus NServiceBus.Unicast.UnicastBus::Start(System.Action)
        NServiceBus.ICallback NServiceBus.Unicast.UnicastBus::Defer(System.DateTime,System.Object)
        NServiceBus.ICallback NServiceBus.Unicast.UnicastBus::Defer(System.DateTime,System.Object[])
        NServiceBus.ICallback NServiceBus.Unicast.UnicastBus::Defer(System.TimeSpan,System.Object[])
        NServiceBus.ICallback NServiceBus.Unicast.UnicastBus::Send(NServiceBus.Address,System.Object[])
        NServiceBus.ICallback NServiceBus.Unicast.UnicastBus::Send(NServiceBus.Address,System.String,System.Object[])
        NServiceBus.ICallback NServiceBus.Unicast.UnicastBus::Send(System.Object[])
        NServiceBus.ICallback NServiceBus.Unicast.UnicastBus::Send(System.String,System.Object[])
        NServiceBus.ICallback NServiceBus.Unicast.UnicastBus::Send(System.String,System.String,System.Object[])
        NServiceBus.ICallback NServiceBus.Unicast.UnicastBus::SendLocal(System.Object[])
        NServiceBus.ICallback NServiceBus.Unicast.UnicastBus::SendToSites(System.Collections.Generic.IEnumerable`1<System.String>,System.Object[])
        NServiceBus.IInMemoryOperations NServiceBus.Unicast.UnicastBus::get_InMemory()
        NServiceBus.IMessageContext NServiceBus.Unicast.UnicastBus::get_CurrentMessageContext()
        NServiceBus.Serialization.IMessageSerializer NServiceBus.Unicast.UnicastBus::get_MessageSerializer()
        NServiceBus.Transports.IDeferMessages NServiceBus.Unicast.UnicastBus::get_MessageDeferrer()
        NServiceBus.Transports.IPublishMessages NServiceBus.Unicast.UnicastBus::get_MessagePublisher()
        NServiceBus.Unicast.Messages.MessageMetadataRegistry NServiceBus.Unicast.UnicastBus::get_MessageMetadataRegistry()
        NServiceBus.Unicast.Subscriptions.MessageDrivenSubscriptions.SubcriberSideFiltering.SubscriptionPredicatesEvaluator NServiceBus.Unicast.UnicastBus::get_SubscriptionPredicatesEvaluator()
        System.Boolean NServiceBus.Unicast.UnicastBus::get_DoNotStartTransport()
        System.Boolean NServiceBus.Unicast.UnicastBus::get_SkipDeserialization()
        System.Collections.Generic.IDictionary`2<System.String,System.String> NServiceBus.Unicast.UnicastBus::get_OutgoingHeaders()
        System.Collections.Generic.IDictionary`2<System.Type,System.Type> NServiceBus.Unicast.UnicastBus::get_MessageDispatcherMappings()
        System.TimeSpan NServiceBus.Unicast.UnicastBus::get_TimeToBeReceivedOnForwardedMessages()
        System.Void NServiceBus.Unicast.UnicastBus::add_MessageReceived(NServiceBus.Unicast.UnicastBus/MessageReceivedDelegate)
        System.Void NServiceBus.Unicast.UnicastBus::add_MessagesSent(System.EventHandler`1<NServiceBus.Unicast.MessagesEventArgs>)
        System.Void NServiceBus.Unicast.UnicastBus::add_NoSubscribersForMessage(System.EventHandler`1<NServiceBus.Unicast.MessageEventArgs>)
        System.Void NServiceBus.Unicast.UnicastBus::add_Started(System.EventHandler)
        System.Void NServiceBus.Unicast.UnicastBus::Dispose()
        System.Void NServiceBus.Unicast.UnicastBus::DisposeManaged()
        System.Void NServiceBus.Unicast.UnicastBus::DoNotContinueDispatchingCurrentMessageToHandlers()
        System.Void NServiceBus.Unicast.UnicastBus::Publish(T[])
        System.Void NServiceBus.Unicast.UnicastBus::Raise(System.Action`1<T>)
        System.Void NServiceBus.Unicast.UnicastBus::Raise(T)
        System.Void NServiceBus.Unicast.UnicastBus::remove_MessageReceived(NServiceBus.Unicast.UnicastBus/MessageReceivedDelegate)
        System.Void NServiceBus.Unicast.UnicastBus::remove_MessagesSent(System.EventHandler`1<NServiceBus.Unicast.MessagesEventArgs>)
        System.Void NServiceBus.Unicast.UnicastBus::remove_NoSubscribersForMessage(System.EventHandler`1<NServiceBus.Unicast.MessageEventArgs>)
        System.Void NServiceBus.Unicast.UnicastBus::remove_Started(System.EventHandler)
        System.Void NServiceBus.Unicast.UnicastBus::Reply(System.Object[])
        System.Void NServiceBus.Unicast.UnicastBus::set_DisableMessageHandling(System.Boolean)
        System.Void NServiceBus.Unicast.UnicastBus::set_DoNotStartTransport(System.Boolean)
        System.Void NServiceBus.Unicast.UnicastBus::set_ForwardReceivedMessagesTo(NServiceBus.Address)
        System.Void NServiceBus.Unicast.UnicastBus::set_InputAddress(NServiceBus.Address)
        System.Void NServiceBus.Unicast.UnicastBus::set_MasterNodeAddress(NServiceBus.Address)
        System.Void NServiceBus.Unicast.UnicastBus::set_MessageAuditer(NServiceBus.Audit.MessageAuditer)
        System.Void NServiceBus.Unicast.UnicastBus::set_MessageDeferrer(NServiceBus.Transports.IDeferMessages)
        System.Void NServiceBus.Unicast.UnicastBus::set_MessageDispatcherMappings(System.Collections.Generic.IDictionary`2<System.Type,System.Type>)
        System.Void NServiceBus.Unicast.UnicastBus::set_MessageMetadataRegistry(NServiceBus.Unicast.Messages.MessageMetadataRegistry)
        System.Void NServiceBus.Unicast.UnicastBus::set_MessagePublisher(NServiceBus.Transports.IPublishMessages)
        System.Void NServiceBus.Unicast.UnicastBus::set_MessageSerializer(NServiceBus.Serialization.IMessageSerializer)
        System.Void NServiceBus.Unicast.UnicastBus::set_SkipDeserialization(System.Boolean)
        System.Void NServiceBus.Unicast.UnicastBus::set_SubscriptionPredicatesEvaluator(NServiceBus.Unicast.Subscriptions.MessageDrivenSubscriptions.SubcriberSideFiltering.SubscriptionPredicatesEvaluator)
        System.Void NServiceBus.Unicast.UnicastBus::set_TimeToBeReceivedOnForwardedMessages(System.TimeSpan)
        System.Void NServiceBus.Unicast.UnicastBus::Shutdown()
        System.Void NServiceBus.Unicast.UnicastBus::Subscribe(System.Predicate`1<T>)
        System.Void NServiceBus.Unicast.UnicastBus::Subscribe(System.Type,System.Predicate`1<System.Object>)

    NServiceBus.UnitOfWork.UnitOfWorkBehavior
        System.Void NServiceBus.UnitOfWork.UnitOfWorkBehavior::Invoke(NServiceBus.Pipeline.Contexts.ReceivePhysicalMessageContext,System.Action)

    NServiceBus.Utils.FileVersionRetriever
        System.String NServiceBus.Utils.FileVersionRetriever::GetFileVersion(System.Type)

    NServiceBus.Utils.Reflection.DelegateFactory
        NServiceBus.Utils.Reflection.LateBoundField NServiceBus.Utils.Reflection.DelegateFactory::Create(System.Reflection.FieldInfo)
        NServiceBus.Utils.Reflection.LateBoundFieldSet NServiceBus.Utils.Reflection.DelegateFactory::CreateSet(System.Reflection.FieldInfo)
        NServiceBus.Utils.Reflection.LateBoundMethod NServiceBus.Utils.Reflection.DelegateFactory::Create(System.Reflection.MethodInfo)
        NServiceBus.Utils.Reflection.LateBoundProperty NServiceBus.Utils.Reflection.DelegateFactory::Create(System.Reflection.PropertyInfo)
        NServiceBus.Utils.Reflection.LateBoundPropertySet NServiceBus.Utils.Reflection.DelegateFactory::CreateSet(System.Reflection.PropertyInfo)

    NServiceBus.Utils.RegistryReader`1
        T NServiceBus.Utils.RegistryReader`1::Read(System.String,T)

    NServiceBus.XmlSerializerConfigurationExtensions
        NServiceBus.Settings.SerializationSettings NServiceBus.XmlSerializerConfigurationExtensions::Xml(NServiceBus.Settings.SerializationSettings,System.Action`1<NServiceBus.Serializers.XML.Config.XmlSerializationSettings>)

    System.Threading.Tasks.Schedulers.MTATaskScheduler
        System.Void System.Threading.Tasks.Schedulers.MTATaskScheduler::Dispose()

