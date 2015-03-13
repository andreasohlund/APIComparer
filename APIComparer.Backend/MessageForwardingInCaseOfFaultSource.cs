namespace APIComparer.Backend
{
    using NServiceBus.Config;
    using NServiceBus.Config.ConfigurationSource;

    internal class MessageForwardingInCaseOfFaultSource : IProvideConfiguration<MessageForwardingInCaseOfFaultConfig>
    {
        public MessageForwardingInCaseOfFaultConfig GetConfiguration()
        {
            return new MessageForwardingInCaseOfFaultConfig
            {
                ErrorQueue = "error"
            };
        }
    }
}