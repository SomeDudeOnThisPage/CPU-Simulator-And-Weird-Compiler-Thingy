namespace mos6502.mos6502.instructions
{
  public class Lda : Mos6502Instruction
  {
    private static void SetFlags(Mos6502ProcessingUnit cpu)
    {
      if (cpu._a == 0)
      {
        cpu._flags |= Mos6502ProcessingUnit.Flags.ZERO;
      }

      if ((cpu._a >> 7 & 1) == 1)
      {
        cpu._flags |= Mos6502ProcessingUnit.Flags.NEGATIVE;
      }
    }

    public override void Execute(Mos6502ProcessingUnit cpu, ushort address)
    {
      if (this._mode == Mos6502Instruction.AddressingMode.IMMEDIATE)
      {
        cpu._a = (byte) (address & 0xFF); // the lower byte of the address is the immediate data to be loaded
      }
      else
      {
        cpu._a = cpu.Read(address); // load the data in the supplied address
      }

      Lda.SetFlags(cpu);
    }

    public Lda(byte size, byte cycles, Mos6502Instruction.AddressingMode mode) : base(size, cycles, mode)
    {
    }
  }
}