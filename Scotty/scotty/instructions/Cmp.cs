using System;
using Scotty.core;

namespace Scotty.scotty.instructions {
  public struct Cmp : IInstruction<ScottStackProcessor> {
    public void execute(ScottStackProcessor cpu, params byte[] arguments) {
      ushort s0 = cpu.Pop();
      ushort s1 = cpu.Pop();

      Console.WriteLine("COMPARING " + s0 + " TO " + s1);
      
      if (s0 > s1) {
        cpu.GetFlags().Set(ScottStackProcessor.Flags.A);
      }

      if (s0 == s1) {
        cpu.GetFlags().Set(ScottStackProcessor.Flags.E);
      }

      if (s0 == 0x00 && s1 == 0x00) {
        cpu.GetFlags().Set(ScottStackProcessor.Flags.Z);
      }
    }
  }
}