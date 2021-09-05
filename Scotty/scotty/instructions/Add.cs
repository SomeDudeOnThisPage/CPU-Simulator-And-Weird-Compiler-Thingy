using System;
using Scotty.core;

namespace Scotty.scotty.instructions
{
  public struct Add : IInstruction<ScottStackProcessor>
  {
    public void execute(ScottStackProcessor cpu, params byte[] arguments)
    {
      ushort s0 = cpu.Pop();
      ushort s1 = cpu.Pop();

      if (s0 + s1 > ushort.MaxValue)
      {
        Console.WriteLine("AYOO WE DONE");
        cpu.GetFlags().Set(ScottStackProcessor.Flags.C);
      }

      Console.WriteLine("ADD " + s0 + " + " + s1);

      cpu.Push((ushort) (s0 + s1));
    }
  }
}