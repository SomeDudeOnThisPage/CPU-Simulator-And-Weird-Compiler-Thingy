using System;
using Scotty.core;

namespace Scotty.scotty.instructions
{
  public struct Ld : IInstruction<ScottStackProcessor>
  {
    public void execute(ScottStackProcessor cpu, params byte[] arguments)
    {
      ushort address = cpu.Pop();

      // convert starting with high byte
      ushort word = BitConverter.ToUInt16(new[]
      {
        cpu.Read((ushort) (address + 1)),
        cpu.Read(address)
      });

      cpu.Push(word);
    }
  }
}