using Scotty.core;

namespace Scotty.scotty.instructions {
  public struct Duck : IInstruction<ScottStackProcessor> {
    public void execute(ScottStackProcessor cpu, params byte[] arguments) {
      cpu.Halt = true;
    }
  }
}