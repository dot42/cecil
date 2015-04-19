using System.ComponentModel;
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
        private int usedInSerialization = 0;

        /// <summary>
        /// Is this item reachable?
        /// </summary>
        public bool IsReachable { get { return (reachable != 0); } }

        public bool IsUsedInSerialization { get { return (usedInSerialization != 0); } }

        /// <summary>
        /// Mark this type reachable.
        /// </summary>
        public void SetReachable(IReachableContext context, bool useInSerialization = false)
        {
            // Already reachable?
            if (reachable != 0 && (!useInSerialization || usedInSerialization != 0)) { return; }

            if (useInSerialization)
            {
                if (useInSerialization)
                {
                }
            }
            // Mark it reachable
            bool reachableChanged = Interlocked.Exchange(ref reachable, 1) == 0;
            bool serializationChanged = useInSerialization && Interlocked.Exchange(ref usedInSerialization, 1) == 0;

            if (reachableChanged || serializationChanged)
            {
                if (context != null)
                {
                    context.NewReachableDetected();

                    // Member was not yet walked, do it now unless its a type that is not in the project
                    if (ShouldWalk(context, this))
                    {
                        context.Walk(this);
                    }
                }
            }
        }

        /// <summary>
        /// Mark this type not reachable. 
        /// USE WITH CARE
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetNotReachable()
        {
            // Already reachable?
            if (reachable == 0) { return; }

            // Mark it not reachable
            Interlocked.Exchange(ref reachable, 0);
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
