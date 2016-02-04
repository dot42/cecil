namespace Mono.Cecil
{
    /// <summary>
    /// Add extensions
    /// </summary>
    partial class FieldDefinition
    {
        private int originalAttributes = -1;

        /// <summary>
        /// Is this type used in an Interlocked.XXX call as reference?
        /// </summary>
        public bool IsUsedInInterlocked { get; set; }

        public FieldAttributes OriginalAttributes
        {
            get { return originalAttributes == -1 ? Attributes : (FieldAttributes)originalAttributes; }
            set { originalAttributes = (int)value; }
        }

    }
}
