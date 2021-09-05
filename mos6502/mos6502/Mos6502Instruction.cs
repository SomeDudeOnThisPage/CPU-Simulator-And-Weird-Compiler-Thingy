using System;

namespace mos6502.mos6502
{
  public abstract class Mos6502Instruction
  {
    public enum AddressingMode
    {
      IMPLICIT,
      ACCUMULATOR,
      IMMEDIATE,
      ZERO_PAGE,
      ZERO_PAGE_X,
      ZERO_PAGE_Y,
      RELATIVE,
      ABSOLUTE,
      ABSOLUTE_X,
      ABSOLUTE_Y,
      INDIRECT,
      INDEXED_INDIRECT,
      INDIRECT_INDEXED
    }

    private byte _size;
    private byte _cycles;
    protected Mos6502Instruction.AddressingMode _mode;

    public abstract void Execute(Mos6502ProcessingUnit cpu, ushort address);

    protected Mos6502Instruction(byte size, byte cycles, Mos6502Instruction.AddressingMode mode)
    {
      this._size = size;
      this._cycles = cycles;
      this._mode = mode;
    }
  }
}