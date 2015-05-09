using System.Threading;

namespace Mono.Cecil
{
    public partial class MethodDefinition
    {
        private string originalName;
        public string OriginalName { get { return originalName ?? Name; } }

        /// <summary>
        /// use this method to set a new name, but preserve the original name
        /// in OriginalName
        /// </summary>
        public void SetName(string name)
        {
            Interlocked.CompareExchange(ref originalName, Name, null);
            Name = name;
        }
    }
}
