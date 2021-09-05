using Scotty.core;

namespace Scotty.scotty.instructions
{
  public interface Instruction3B : IInstruction<ScottStackProcessor>
  {
    public new uint size()
    {
      return 3;
    }
  }
}