namespace Mono.Cecil
{
    /// <summary>
    /// Method reference used by a function pointer.
    /// </summary>
    internal class FunctionPointerTypeMethodReference : MethodReference
    {
        private readonly FunctionPointerType type;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal FunctionPointerTypeMethodReference(FunctionPointerType type)
        {
            this.type = type;
        }

        /// <summary>
        /// Gets the module containing this element
        /// </summary>
        public override ModuleDefinition Module
        {
            get { return type.Module; }
        }
    }
}
