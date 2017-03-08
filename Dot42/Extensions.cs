using Mono.Cecil.Cil;

namespace TallApplications.Dot42
{
    public static class Extensions
    {
        public static SequencePoint SequencePoint(this Instruction ins, MethodBody body)
        { 
            return body.Method.DebugInformation.GetSequencePoint(ins);
        } 


    }
}
