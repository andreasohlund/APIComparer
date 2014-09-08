using System;
using System.Collections.Generic;
using System.Linq;
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

        public List<MethodDefinition> LeftOrphanMethods { get; set; }
        public List<MethodDefinition> RightOrphanMethods { get; set; }
        public List<Tuple<MethodDefinition, MethodDefinition>> MatchingMethods { get; set; }


        public IEnumerable<Tuple<MethodDefinition, MethodDefinition>> GetMissingMethods()
        {
            return LeftOrphanMethods.Select(m => new Tuple<MethodDefinition, MethodDefinition>(m, null))
                .Concat(MatchingMethods)
                .OrderBy(t => t.Item1.Name);
        }
        public IEnumerable<Tuple<FieldDefinition, FieldDefinition>> GetMissingFields()
        {
            return LeftOrphanFields.Select(f => new Tuple<FieldDefinition, FieldDefinition>(f, null))
                .Concat(MatchingFields)
                .OrderBy(f => f.Item1.Name);
        }

    }
}