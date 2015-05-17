using System.Threading;

namespace Mono.Cecil
{
    public partial class MethodDefinition
    {
        private string originalName;
        private int originalAttributes = -1;

        public string OriginalName { get { return originalName ?? Name; } }

        public MethodAttributes OriginalAttributes
        {
            get { return originalAttributes == -1 ? Attributes : (MethodAttributes)originalAttributes; }
            set { if (originalAttributes == -1) originalAttributes = (int)value; }
        }

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
