namespace APIComparer.Backend
{
    using NServiceBus.Config;
    using NServiceBus.Config.ConfigurationSource;

    internal class Audit : IProvideConfiguration<AuditConfig>
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