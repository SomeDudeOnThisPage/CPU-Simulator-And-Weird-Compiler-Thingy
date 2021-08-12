using Scotty.core;

namespace Scotty.scotty.instructions {
  public struct Shr : IInstruction<ScottStackProcessor> {
    public void execute(ScottStackProcessor cpu, params byte[] arguments) {
      ushort s0 = cpu.Pop();
      ushort s1 = cpu.Pop();
      
      if (s0 >> s1 < 0) {
        cpu.GetFlags().Set(ScottStackProcessor.Flags.C);
      }
      
      cpu.Push((ushort) (s0 >> s1));
    }
  }
}