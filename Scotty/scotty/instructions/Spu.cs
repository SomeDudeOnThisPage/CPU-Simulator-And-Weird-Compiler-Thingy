using System;
using Scotty.core;

namespace Scotty.scotty.instructions
{
  public struct Spu : IInstruction<ScottStackProcessor>
  {
    public void execute(ScottStackProcessor cpu, params byte[] arguments)
    {
      Console.WriteLine("SPU " + arguments[0] + " " + arguments[1]);
      ushort word = BitConverter.ToUInt16(new[] {arguments[0], arguments[1]});
      cpu.Push(word);
    }
  }
}