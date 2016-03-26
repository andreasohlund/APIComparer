namespace APIComparer
{
    using Mono.Cecil;

    public class RemovedType
    {
        public RemovedType(TypeDefinition typeDefinition)
        {
            Name = typeDefinition.GetName();

            if (typeDefinition.HasObsoleteAttribute())
            {
                var obsoleteInfo = typeDefinition.GetObsoleteInfo();
                UpgradeInstructions = obsoleteInfo.Message;
            }
        }

        public RemovedType(TypeDefinition typeDefinition, ObsoleteInfo obsoleteInfo) : this(typeDefinition)
        {
            if (obsoleteInfo != null)
            {
                UpgradeInstructions = obsoleteInfo.Message;
            }
        }

        public string Name { get; }
        public string UpgradeInstructions { get; }


    }
}