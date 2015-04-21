namespace Mono.Cecil
{
    /// <summary>
    /// Add extensions
    /// </summary>
    partial class FieldDefinition
    {
        /// <summary>
        /// Is this type used in an Interlocked.XXX call as reference?
        /// </summary>
        public bool IsUsedInInterlocked { get; set; }
    }
}
