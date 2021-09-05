using Scotty.core;

namespace Scotty.scotty.instructions
{
  public struct Spo : IInstruction<ScottStackProcessor>
  {
    public void execute(ScottStackProcessor cpu, params byte[] arguments)
    {
      cpu.Pop();
    }
  }
}