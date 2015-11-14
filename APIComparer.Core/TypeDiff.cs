namespace APIComparer
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Mono.Cecil;

    [DebuggerDisplay("{RightType.FullName} => {LeftType.FullName}")]
    public class TypeDiff
    {
        public IEnumerable<MatchingMember<MethodDefinition>> MethodsChangedToNonPublic()
        {
            return MatchingMethods.Where(x => !x.Right.IsPublic && x.Left.IsPublic && !x.Left.HasObsoleteAttribute());
        }

        public IEnumerable<MatchingMember<MethodDefinition>> PublicMethodsObsoleted()
        {
            return MatchingMethods.Where(x => x.Left.IsPublic && x.Right.IsPublic && !x.Left.HasObsoleteAttribute() && x.Right.HasObsoleteAttribute());
        }

        public IEnumerable<MethodDefinition> PublicMethodsRemoved()
        {
            return LeftOrphanMethods.Where(x => x.IsPublic && !x.HasObsoleteAttribute());
        }

        public IEnumerable<MatchingMember<FieldDefinition>> FieldsChangedToNonPublic()
        {
            return MatchingFields.Where(x => !x.Right.IsPublic && x.Left.IsPublic && !x.Left.HasObsoleteAttribute());
        }

        public IEnumerable<FieldDefinition> PublicFieldsRemoved()
        {
            return LeftOrphanFields.Where(x => x.IsPublic && !x.HasObsoleteAttribute());
        }

        public bool TypeObsoleted()
        {
            return !LeftType.HasObsoleteAttribute() && RightType.HasObsoleteAttribute();
        }

        public IEnumerable<MatchingMember<FieldDefinition>> PublicFieldsObsoleted()
        {
            return MatchingFields.Where(x => x.Left.IsPublic && x.Right.IsPublic && !x.Left.HasObsoleteAttribute() && x.Right.HasObsoleteAttribute());
        }

        public List<FieldDefinition> LeftOrphanFields;

        public List<MethodDefinition> LeftOrphanMethods;
        public TypeDefinition LeftType;
        public List<MatchingMember<FieldDefinition>> MatchingFields;
        public List<MatchingMember<MethodDefinition>> MatchingMethods;
        public List<FieldDefinition> RightOrphanFields;
        public List<MethodDefinition> RightOrphanMethods;
        public TypeDefinition RightType;
    }
}