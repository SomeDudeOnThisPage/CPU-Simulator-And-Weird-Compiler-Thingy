using Scotty.core;

namespace Scotty.scotty.instructions
{
  public struct Or : IInstruction<ScottStackProcessor>
  {
    public void execute(ScottStackProcessor cpu, params byte[] arguments)
    {
      ushort s0 = cpu.Pop();
      ushort s1 = cpu.Pop();

      cpu.Push((ushort) (s0 | s1));
    }
  }
}