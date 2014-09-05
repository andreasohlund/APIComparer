using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace APIComparer
{
    public class TypeDiff
    {
        public TypeDefinition LeftType { get; set; }
        public TypeDefinition RightType { get; set; }

        public List<FieldDefinition> LeftOrphanFields { get; set; }
        public List<FieldDefinition> RightOrphanFields { get; set; }
        public List<Tuple<FieldDefinition, FieldDefinition>> MatchingFields { get; set; }

        public List<PropertyDefinition> LeftOrphanProperties { get; set; }
        public List<PropertyDefinition> RightOrphanProperties { get; set; }
        public List<Tuple<PropertyDefinition, PropertyDefinition>> MatchingProperties { get; set; }

        public List<MethodDefinition> LeftOrphanMethods { get; set; }
        public List<MethodDefinition> RightOrphanMethods { get; set; }
        public List<Tuple<MethodDefinition, MethodDefinition>> MatchingMethods { get; set; }
    }
}