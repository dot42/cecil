using System;

namespace Mono.Cecil
{
    public class ResolutionException : Exception
    {

        readonly MemberReference member;

        public MemberReference Member
        {
            get { return member; }
        }

        public ResolutionException(MemberReference member)
            : base("Failed to resolve " + member.FullName)
        {
            this.member = member;
        }
    }

}
