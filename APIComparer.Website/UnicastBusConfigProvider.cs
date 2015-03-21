namespace APIComparer.Website
{
    using NServiceBus.Config;
    using NServiceBus.Config.ConfigurationSource;

    public class UnicastBusConfigProvider : IProvideConfiguration<UnicastBusConfig>
    {
        public UnicastBusConfig GetConfiguration()
        {
            return new UnicastBusConfig
            {
                MessageEndpointMappings = new MessageEndpointMappingCollection
                {
                    new MessageEndpointMapping
                    {
                        AssemblyName = "APIComparer.Contracts",
                        Endpoint = "APIComparer.Backend"
                    }
                }
            };
        }
    }
}