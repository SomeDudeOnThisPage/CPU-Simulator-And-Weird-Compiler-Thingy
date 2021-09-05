using System;

namespace mos6502.mos6502
{
  public class Mos6502ProcessingUnit : IProcessingUnit
  {
    [System.Flags]
    public enum Flags : byte
    {
      CARRY = 1 << 0,
      ZERO = 1 << 1,
      INTERRUPT_DISABLE = 1 << 2,
      DECIMAL_MODE = 1 << 3,
      _ = 1 << 4,
      BREAK = 1 << 5,
      OVERFLOW = 1 << 6,
      NEGATIVE = 1 << 7
    }

    public ushort _pc; // program counter
    public byte _sp; // stack pointer

    public byte _a; // accumulator
    public byte _x;
    public byte _y;

    public Mos6502ProcessingUnit.Flags _flags;
    // public byte _flags;


    public byte[] _ram;

    public void JumpRelative(short offset)
    {
      this._pc = (ushort) (this._pc + offset);
    }

    public void Jump(ushort address)
    {
      this._pc = address;
    }

    public byte Fetch()
    {
      return this._ram[++this._pc];
    }

    public byte Read(ushort address)
    {
      return this._ram[address];
    }

    public void Write(ushort address, byte data)
    {
      this._ram[address] = data;
    }

    public ushort ReadWord(ushort address)
    {
      return (ushort) (this.Read(address) << 8 + this.Read(++address));
    }

    /**
     * <summary>
     * Constructs a <code>byte</code>-array for an operation based on the addressing mode.
     * </summary>
     */
    public byte[] ResolveAddressingMode(Mos6502Instruction.AddressingMode mode)
    {
      byte[] data = new byte[2];
      switch (mode)
      {
        case Mos6502Instruction.AddressingMode.IMPLICIT: // the data is part of the instruction
          break; // data doesn't matter
        case Mos6502Instruction.AddressingMode.ACCUMULATOR:
          break; // todo;
        case Mos6502Instruction.AddressingMode.IMMEDIATE:
          data[0] = this.Fetch();
          break;
        case Mos6502Instruction.AddressingMode.RELATIVE:
          data[0] = this.Fetch();
          break;
        case Mos6502Instruction.AddressingMode.ABSOLUTE:
          data[0] = this.Fetch();
          data[1] = this.Fetch();
          break;
        case Mos6502Instruction.AddressingMode.ZERO_PAGE:
          data[0] = this.Fetch();
          break;
        case Mos6502Instruction.AddressingMode.ZERO_PAGE_X:
          break;
        case Mos6502Instruction.AddressingMode.ZERO_PAGE_Y:
          break;
      }

      return data;
    }

    public void Reset()
    {
      this._pc = 0x0000;
      this._sp = 0x00;
      this._a = 0x00;
      this._x = 0x00;
      this._y = 0x00;
      this._flags = 0x00;
    }

    public void Step()
    {
      // fetch next instruction

      // check instruction addressing mode

      // fetch data according to addressing mode and size

      // execute instruction with data (regardless of addressing mode)
    }

    public Mos6502ProcessingUnit()
    {
      this.Reset();
      this._ram = new byte[0xFFFF];
    }
  }
}