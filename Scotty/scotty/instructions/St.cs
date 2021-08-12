using System;
using Scotty.core;

namespace Scotty.scotty.instructions {
  public struct St : IInstruction<ScottStackProcessor> {
    public void execute(ScottStackProcessor cpu, params byte[] arguments) {
      ushort address = cpu.Pop();
      ushort s0 = cpu.Pop();

      // store high - low
      byte[] bytes = BitConverter.GetBytes(s0);
      cpu.Write(address, bytes[1]);
      cpu.Write((ushort) (address + 1), bytes[0]);

      ushort bitties = BitConverter.ToUInt16(new[] {bytes[1], bytes[0]});
      Console.WriteLine("PUSHED VALUE 0x" + bitties.ToString("X") + " TO MEMORY LOCATION 0x" + address.ToString("x"));
    }
  }
}