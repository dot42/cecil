using System;
using System.Collections.Generic;
using Mono.Cecil;

namespace Dot42.Cecil
{
    /// <summary>
    /// Used to accept visits from all kinds of type references.
    /// </summary>
    public interface ITypeVisitor<T, DataT>
    {
        T Visit(TypeDefinition type, DataT data);
        T Visit(TypeReference type, DataT data);
        T Visit(GenericParameter type, DataT data);
        T Visit(GenericInstanceType type, DataT data);
        T Visit(ArrayType type, DataT data);
        T Visit(ByReferenceType type, DataT data);
        T Visit(FunctionPointerType type, DataT data);
        T Visit(OptionalModifierType type, DataT data);
        T Visit(RequiredModifierType type, DataT data);
        T Visit(PinnedType type, DataT data);
        T Visit(PointerType type, DataT data);
        T Visit(SentinelType type, DataT data);
    }

    /// <summary>
    /// Cecil extension methods
    /// </summary>
    public static partial class CecilExt
    {
        /// <summary>
        /// Accept a visit from the given visitor
        /// </summary>
        public static T Accept<T, DataT>(this TypeReference type, ITypeVisitor<T, DataT> visitor, DataT data)
        {
            if (type == null) { return default(T); }
            if (type.IsDefinition) { return visitor.Visit((TypeDefinition)type, data); }
            if (type.IsGenericParameter) { return visitor.Visit((GenericParameter)type, data); }
            if (type is TypeSpecification)
            {
                if (type.IsArray) { return visitor.Visit((ArrayType)type, data); }
                if (type.IsByReference) { return visitor.Visit((ByReferenceType)type, data); }
                if (type.IsFunctionPointer) { return visitor.Visit((FunctionPointerType)type, data); }
                if (type.IsGenericInstance) { return visitor.Visit((GenericInstanceType)type, data); }
                if (type.IsOptionalModifier) { return visitor.Visit((OptionalModifierType)type, data); }
                if (type.IsPinned) { return visitor.Visit((PinnedType)type, data); }
                if (type.IsPointer) { return visitor.Visit((PointerType)type, data); }
                if (type.IsRequiredModifier) { return visitor.Visit((RequiredModifierType)type, data); }
                if (type.IsSentinel) { return visitor.Visit((SentinelType)type, data); }
                throw new ArgumentException("Unknown TypeReference: " + type.GetType().FullName);
            }
            else
            {
                return visitor.Visit(type, data);
            }
        }

        /// <summary>
        /// Accept a visit from the given visitor for each item
        /// </summary>
        internal static void AcceptAll<T, VisitorT, DataT>(this IEnumerable<T> items, ITypeVisitor<VisitorT, DataT> visitor, DataT data)
            where T : TypeReference
        {
            foreach (var x in items) { x.Accept(visitor, data); }
        }
    }
}
