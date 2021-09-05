using Scotty.core;

namespace Scotty.scotty.instructions
{
  public struct Nop : IInstruction<ScottStackProcessor>
  {
    public void execute(ScottStackProcessor cpu, params byte[] arguments)
    {
    }
  }
}