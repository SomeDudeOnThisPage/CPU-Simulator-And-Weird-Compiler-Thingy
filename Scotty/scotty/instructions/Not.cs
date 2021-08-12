using Scotty.core;

namespace Scotty.scotty.instructions {
  public struct Not : IInstruction<ScottStackProcessor> {
    public void execute(ScottStackProcessor cpu, params byte[] arguments) {
      ushort s0 = cpu.Pop();
      cpu.Push((ushort) ~s0);
    }
  }
}