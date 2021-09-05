using System;
using Scotty.core;

namespace Scotty.scotty.instructions
{
  internal class Utils
  {
    private static ushort ScottStackCalculateJumpAddress(params byte[] arguments)
    {
      ushort low = arguments[1];
      ushort high = arguments[0];
      ushort address = (ushort) (high << 8);
      address += low;
      return address;
    }

    public static void ScottStackPerformJump(ScottStackProcessor cpu, ushort address)
    {
      Console.WriteLine("JUMPING TO 0x" + address.ToString("X"));
      cpu.Jump(address);
    }

    public static void ScottStackCalculateAndJump(ScottStackProcessor cpu, params byte[] arguments)
    {
      Utils.ScottStackPerformJump(cpu, Utils.ScottStackCalculateJumpAddress(arguments));
    }
  }

  public struct Jmp : IInstruction<ScottStackProcessor>
  {
    public void execute(ScottStackProcessor cpu, params byte[] arguments)
    {
      Utils.ScottStackCalculateAndJump(cpu, arguments);
    }
  }

  /**
   * Pop a word from the stack, and jump to the contained memory address.
   */
  public struct Js : IInstruction<ScottStackProcessor>
  {
    public void execute(ScottStackProcessor cpu, params byte[] arguments)
    {
      Utils.ScottStackPerformJump(cpu, cpu.Pop());
    }
  }

  /**
   * Form a word out of b1, b2 and jump to the specified address if all specified cpu flags are set.
   */
  public readonly struct Jf : IInstruction<ScottStackProcessor>
  {
    private readonly int flags;

    public void execute(ScottStackProcessor cpu, params byte[] arguments)
    {
      if (!cpu.GetFlags().Get(this.flags)) return;
      Utils.ScottStackCalculateAndJump(cpu, arguments);
    }

    public Jf(int flags)
    {
      this.flags = flags;
    }
  }
}