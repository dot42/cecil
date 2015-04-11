using System;
using Mono.Collections.Generic;

namespace Mono.Cecil
{
    /// <summary>
    /// Implementation of an interface by a class.
    /// </summary>
    public partial class InterfaceImpl : ICustomAttributeProvider
    {
        private readonly TypeReference @interface;
        private Collection<CustomAttribute> custom_attributes;

        /// <summary>
        /// Default ctor
        /// </summary>
        public InterfaceImpl(TypeReference @interface)
        {
            if (@interface == null)
                throw new ArgumentNullException("interface");
            this.@interface = @interface;
        }

        /// <summary>
        /// Token used in the assembly.
        /// </summary>
        public MetadataToken MetadataToken { get; set; }

        /// <summary>
        /// Gets the interface being implemented
        /// </summary>
        public TypeReference Interface { get { return @interface; } }

        /// <summary>
        /// Are there any custom attributes on this interface implementation?
        /// </summary>
        public bool HasCustomAttributes
        {
            get
            {
                if (custom_attributes != null)
                    return custom_attributes.Count > 0;

                return this.GetHasCustomAttributes(@interface.Module);
            }
        }

        /// <summary>
        /// Gets all custom attributes added to this interface implementation.
        /// </summary>
        public Collection<CustomAttribute> CustomAttributes
        {
            get { return custom_attributes ?? (this.GetCustomAttributes(ref custom_attributes, @interface.Module)); }
        }
    }
}
