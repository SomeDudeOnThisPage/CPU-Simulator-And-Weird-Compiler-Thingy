using System;
using System.Collections.Generic;
using Scotty.core;
using Scotty.scotty.instructions;

namespace Scotty.scotty
{
  public class ScottStackInstructionSet : IInstructionSet<ScottStackProcessor>
  {
    private const int INSTRUCTION_SIZE = 3;

    private enum Instruction : byte
    {
      ADD = 0x80,
      SHR = 0x90,
      SHL = 0xA0,
      NOT = 0xB0,
      AND = 0xC0,
      OR = 0xD0,
      XOR = 0xE0,
      CMP = 0xF0,
      LD = 0x00,
      ST = 0x10,
      SPU = 0x20,
      SPO = 0x30,
      JMP = 0x40,
      JS = 0x55,
      JC = 0x54,
      JA = 0x52,
      JE = 0x51,
      JZ = 0x50,
      CLF = 0x60,
      NOP = 0xEF,
      DUCK = 0xFF
    }

    private readonly Dictionary<byte, IInstruction<ScottStackProcessor>> instructions;

    public byte getInstruction(string instruction)
    {
      return (byte) Enum.Parse<Instruction>(instruction);
    }

    public IInstruction<ScottStackProcessor> getInstruction(byte instruction)
    {
      return this.instructions[instruction];
    }

    public int getInstructionSize()
    {
      return ScottStackInstructionSet.INSTRUCTION_SIZE;
    }

    public ScottStackInstructionSet()
    {
      this.instructions = new Dictionary<byte, IInstruction<ScottStackProcessor>>
      {
        [(byte) Instruction.ADD] = new Add(),
        [(byte) Instruction.SHR] = new Shr(),
        [(byte) Instruction.SHL] = new Shl(),

        [(byte) Instruction.NOT] = new Not(),
        [(byte) Instruction.AND] = new And(),
        [(byte) Instruction.OR] = new Or(),
        [(byte) Instruction.XOR] = new Xor(),

        [(byte) Instruction.CMP] = new Cmp(),
        [(byte) Instruction.LD] = new Ld(),
        [(byte) Instruction.ST] = new St(),
        [(byte) Instruction.SPU] = new Spu(),
        [(byte) Instruction.SPO] = new Spo(),

        [(byte) Instruction.JMP] = new Jmp(),
        [(byte) Instruction.JS] = new Js(),
        [(byte) Instruction.JC] = new Jf(ScottStackProcessor.Flags.C),
        [(byte) Instruction.JA] = new Jf(ScottStackProcessor.Flags.A),
        [(byte) Instruction.JE] = new Jf(ScottStackProcessor.Flags.E),
        [(byte) Instruction.JZ] = new Jf(ScottStackProcessor.Flags.Z),

        [(byte) Instruction.CLF] = new Clf(),


        [(byte) Instruction.NOP] = new Nop(),
        [(byte) Instruction.DUCK] = new Duck()
      };

      // todo: generate all valid jump instructions from flags CAEZ
    }
  }
}