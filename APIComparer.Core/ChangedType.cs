namespace APIComparer
{
    using System.Collections.Generic;
    using Mono.Cecil;

    public class ChangedType
    {
        public List<RemovedMember> RemovedMembers { get; }
        public string Name { get; private set; }

        public ChangedType(TypeDefinition typeDefinition, List<RemovedMember> removedMembers)
        {
            Name = typeDefinition.GetName();
            RemovedMembers = removedMembers;

            //foreach (var removedMethod in typeDiff.PublicMethodsRemoved())
            //{
            //    RemovedMembers.Add(new RemovedMember
            //    {
            //        IsField = false,
            //        Name = removedMethod.GetName(),
            //    });
            //}
            //foreach (var matchingMember in typeDiff.MethodsChangedToNonPublic())
            //{
            //    RemovedMembers.Add(new RemovedMember
            //    {
            //        IsField = false,
            //        Name = matchingMember.Right.GetName(),
            //    });
            //}
            //foreach (var matchingMember in typeDiff.PublicMethodsObsoleted())
            //{
            //    var obsoleteInfo = matchingMember.Right.GetObsoleteInfo();

            //    RemovedMembers.Add(new RemovedMember
            //    {
            //        IsField = false,
            //        Name = matchingMember.Right.GetName(),
            //        UpgradeInstructions = obsoleteInfo.Message
            //    });
            //}

            //foreach (var removedField in typeDiff.PublicFieldsRemoved())
            //{
            //    RemovedMembers.Add(new RemovedMember
            //    {
            //        IsField = true,
            //        Name = removedField.GetName(),
            //    });
            //}
            //foreach (var matchingMember in typeDiff.FieldsChangedToNonPublic())
            //{
            //    RemovedMembers.Add(new RemovedMember
            //    {
            //        IsField = true,
            //        Name = matchingMember.Right.GetName(),
            //    });
            //}
            //foreach (var matchingMember in typeDiff.PublicFieldsObsoleted())
            //{
            //    var obsoleteInfo = matchingMember.Right.GetObsoleteInfo();

            //    RemovedMembers.Add(new RemovedMember
            //    {
            //        IsField = true,
            //        Name = matchingMember.Right.GetName(),
            //        UpgradeInstructions = obsoleteInfo.Message
            //    });
            //}
           
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
    }
}