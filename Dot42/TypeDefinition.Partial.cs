namespace Mono.Cecil
{
    /// <summary>
    /// Add extensions
    /// </summary>
    partial class TypeDefinition
    {
        /// <summary>
        /// Is this type used in a Nullable(T) construct?
        /// </summary>
        public bool UsedInNullableT { get; set; }
    }
}
