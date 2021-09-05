namespace mos6502.mos6502.instructions
{
  public class Adc : Mos6502Instruction
  {
    private static void SetFlags(Mos6502ProcessingUnit cpu, bool overflow)
    {
      if (overflow)
      {
        cpu._flags |= Mos6502ProcessingUnit.Flags.CARRY;
      }

      if (cpu._a == 0)
      {
        cpu._flags |= Mos6502ProcessingUnit.Flags.ZERO;
      }
    }

    public override void Execute(Mos6502ProcessingUnit cpu, ushort address)
    {
      byte value = cpu.Read(address);
      bool overflow = cpu._a + value > byte.MaxValue || cpu._a + value < byte.MinValue;

      cpu._a += value;
      cpu._a += (byte) (cpu._flags & Mos6502ProcessingUnit.Flags.CARRY); // add current carry bit to accumulator

      Adc.SetFlags(cpu, overflow);
    }

    public Adc(byte size, byte cycles, Mos6502Instruction.AddressingMode mode) : base(size, cycles, mode)
    {
    }
  }
}