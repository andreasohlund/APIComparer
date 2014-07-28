using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace APIComparer
{
    public class TypeDiff
    {
        public TypeDefinition LeftType { get; set; }
        public TypeDefinition RightType { get; set; }

        public IList<FieldDefinition> LeftOrphanFields { get; set; }
        public IList<FieldDefinition> RightOrphanFields { get; set; }
        public IList<Tuple<FieldDefinition, FieldDefinition>> MatchingFields { get; set; }

        public IList<PropertyDefinition> LeftOrphanProperties { get; set; }
        public IList<PropertyDefinition> RightOrphanProperties { get; set; }
        public IList<Tuple<PropertyDefinition, PropertyDefinition>> MatchingProperties { get; set; }

        public IList<MethodDefinition> LeftOrphanMethods { get; set; }
        public IList<MethodDefinition> RightOrphanMethods { get; set; }
        public IList<Tuple<MethodDefinition, MethodDefinition>> MatchingMethods { get; set; }
    }
}