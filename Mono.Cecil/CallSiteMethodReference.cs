namespace Mono.Cecil
{
    /// <summary>
    /// Method reference used by a call site.
    /// </summary>
    internal class CallSiteMethodReference : MethodReference
    {
        private readonly CallSite type;

        /// <summary>
        /// Default ctor
        /// </summary>
        internal CallSiteMethodReference(CallSite type)
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
