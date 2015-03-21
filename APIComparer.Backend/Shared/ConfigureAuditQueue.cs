namespace APIComparer.Shared
{
    using NServiceBus.Config;
    using NServiceBus.Config.ConfigurationSource;

    internal class ConfigureAuditQueue : IProvideConfiguration<AuditConfig>
    {
        public AuditConfig GetConfiguration()
        {
            return new AuditConfig
            {
                QueueName = "audit"
            };
        }
    }
}