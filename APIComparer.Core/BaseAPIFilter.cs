using System.Collections.Generic;
using EqualityComparers;
using Mono.Cecil;

namespace APIComparer
{
    public class BaseAPIFilter
    {
        public BaseAPIFilter()
        {
            TypeComparer = EqualityCompare<TypeDefinition>.EquateBy(t => t.FullName);
            FieldComparer = EqualityCompare<FieldDefinition>.EquateBy(f => f.FullName);
            PropertyComparer = EqualityCompare<PropertyDefinition>.EquateBy(p => p.FullName);
            MethodComparer = EqualityCompare<MethodDefinition>.EquateBy(m => m.FullName);
        }

        public IEqualityComparer<TypeDefinition> TypeComparer { get; protected set; }
        public IEqualityComparer<FieldDefinition> FieldComparer { get; protected set; }
        public IEqualityComparer<PropertyDefinition> PropertyComparer { get; protected set; }
        public IEqualityComparer<MethodDefinition> MethodComparer { get; protected set; }

        public virtual bool FilterLeftType(TypeDefinition type)
        {
            return true;
        }

        public virtual bool FilterRightType(TypeDefinition type)
        {
            return true;
        }

        public virtual bool FilterMatchedType(TypeDiff diff)
        {
            return true;
        }

        public virtual bool FilterMemberTypeDiff(TypeDiff diff)
        {
            return true;
        }

        public virtual bool FilterLeftField(FieldDefinition field)
        {
            return true;
        }

        public virtual bool FilterRightField(FieldDefinition field)
        {
            return true;
        }

        public virtual bool FilterMatchedField(FieldDefinition left, FieldDefinition right)
        {
            return true;
        }

        public virtual bool FilterLeftProperty(PropertyDefinition property)
        {
            return true;
        }

        public virtual bool FilterRightProperty(PropertyDefinition property)
        {
            return true;
        }

        public virtual bool FilterMatchedProperty(PropertyDefinition left, PropertyDefinition right)
        {
            return true;
        }

        public virtual bool FilterLeftMethod(MethodDefinition method)
        {
            return true;
        }

        public virtual bool FilterRightMethod(MethodDefinition method)
        {
            return true;
        }

        public virtual bool FilterMatchedMethod(MethodDefinition left, MethodDefinition right)
        {
            return true;
        }
    }
}