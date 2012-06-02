using System.Threading;
using Dot42.Cecil;

namespace Mono.Cecil
{
    /// <summary>
    /// Add extensions for pruning.
    /// </summary>
    partial class MemberReference
    {
        private int reachable = 0; // No bool is used since we're using Interlocked.Exchange.

        /// <summary>
        /// Is this item reachable?
        /// </summary>
        public bool IsReachable { get { return (reachable != 0); } }

        /// <summary>
        /// Mark this type reachable.
        /// </summary>
        public void SetReachable(IReachableContext context)
        {
            // Already reachable?
            if (reachable != 0) { return; }

            // Mark it reachable
            if (Interlocked.Exchange(ref reachable, 1) == 0)
            {
                // Member was not yet walked, do it now unless its a type that is not in the project
                if (ShouldWalk(context, this))
                {
                    context.Walk(this);
                }
            }
        }

        /// <summary>
        /// Should we walk through the member for all children?
        /// </summary>
        private static bool ShouldWalk(IReachableContext context, MemberReference member)
        {
            TypeReference typeRef = member as TypeReference;
            if (typeRef == null) { return true; }
            if (typeRef is GenericParameter) { return true; }
            if (typeRef is TypeSpecification) { return true; }
            return context.Contains(typeRef);
        }
    }
}
