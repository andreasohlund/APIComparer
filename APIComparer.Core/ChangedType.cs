namespace APIComparer
{
    using System.Collections.Generic;
    using Mono.Cecil;
    using System.Linq;

    public class ChangedType
    {
        public List<RemovedMember> RemovedMembers { get; }
        public string Name { get; private set; }
        public List<ChangedEnumMember> ChangedEnumMembers { get; }

        public ChangedType(TypeDefinition typeDefinition, List<RemovedMember> removedMembers, List<ChangedEnumMember> changedEnumMembers = null)
        {
            Name = typeDefinition.GetName();
            RemovedMembers = removedMembers ?? Enumerable.Empty<RemovedMember>().ToList();
            ChangedEnumMembers = changedEnumMembers ?? Enumerable.Empty<ChangedEnumMember>().ToList();
        }

        public class RemovedMember
        {
            public RemovedMember(MethodDefinition methodDefinition)
            {
                Name = methodDefinition.GetName();
                IsField = false;

                if (methodDefinition.HasObsoleteAttribute())
                {
                    var obsoleteInfo = methodDefinition.GetObsoleteInfo();

                    UpgradeInstructions = obsoleteInfo.Message;
                }
            }
            public RemovedMember(FieldDefinition fieldDefinition)
            {
                Name = fieldDefinition.GetName();
                IsField = true;

                if (fieldDefinition.HasObsoleteAttribute())
                {
                    var obsoleteInfo = fieldDefinition.GetObsoleteInfo();

                    UpgradeInstructions = obsoleteInfo.Message;
                }
            }

            public string UpgradeInstructions { get;}

            public bool IsField { get;}

            public string Name { get;}

            public bool IsMethod => !IsField;
        }

        public class ChangedEnumMember
        {
            public ChangedEnumMember(FieldDefinition fieldDefinition)
            {
                Name = fieldDefinition.GetName();

                if (fieldDefinition.HasObsoleteAttribute())
                {
                    var obsoleteInfo = fieldDefinition.GetObsoleteInfo();

                    UpgradeInstructions = obsoleteInfo.Message;
                }
            }

            public string UpgradeInstructions { get; }

            public string Name { get; }
        }
    }
}