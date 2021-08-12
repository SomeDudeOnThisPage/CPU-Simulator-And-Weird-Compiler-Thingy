using Scotty.core;

namespace Scotty.scotty.instructions {
  public struct Clf : IInstruction<ScottStackProcessor> {
    public void execute(ScottStackProcessor cpu, params byte[] arguments) {
      cpu.GetFlags().Clear();
    }
  }
}