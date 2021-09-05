using System;
using System.Globalization;
using Scotty.core;

namespace Scotty.scotty
{
  public class ScottStackProcessor : IProcessingUnit
  {
    public struct Flags
    {
      public const int C = 1 << 0;
      public const int A = 1 << 2;
      public const int E = 1 << 3;
      public const int Z = 1 << 4;

      private int caez;

      public void Set(int flags)
      {
        this.caez |= flags;
      }

      public bool Get(int flags)
      {
        return (this.caez ^ flags) == 0x0;
      }

      public void Clear()
      {
        this.caez = 0x0;
      }
    }

    private readonly ScottStackInstructionSet _instructions;
    private Flags _flags;

    private ushort _st; // stack top
    private ushort _sb; // stack base

    private ushort _cb; // code store base
    private ushort _ct; // code store top
    private ushort _cp; // code store pointer (current instruction)

    private ushort ir;
    private ushort iar;

    private Bus<ScottStackProcessor> _bus;

    public bool Halt { get; set; }

    public void Attach(Bus<ScottStackProcessor> bus)
    {
      this._bus = bus;
    }

    public ref Flags GetFlags()
    {
      return ref this._flags;
    }

    /**
     * Pops a word from the stack. A word is two bytes long, thus, this operation will decrease the stack pointer
     * by 0x2.
     */
    public ushort Pop()
    {
      byte s0 = this._bus.Read(--this._st);
      byte s1 = this._bus.Read(--this._st);

      // byte s0 = this._memory[--this._st];
      // byte s1 = this._memory[--this._st];

      // load high - low
      return BitConverter.ToUInt16(new[] {s1, s0});
    }

    /**
     * Pushes a word onto the stack. A word is two bytes long, thus, this operation will increase the stack pointer
     * by 0x2.
     */
    public void Push(ushort data)
    {
      byte[] bytes = BitConverter.GetBytes(data);

      // store high - low
      // this._memory[this._st++] = bytes[1];
      // this._memory[this._st++] = bytes[0];
      this._bus.Write(this._st++, bytes[1]);
      this._bus.Write(this._st++, bytes[0]);
    }

    public void Write(ushort address, byte data)
    {
      this._bus.Write(address, data);
    }

    public void Attach<T>(Bus<ScottStackProcessor> bus) where T : IProcessingUnit
    {
      this._bus = bus;
    }

    public byte Read(ushort address)
    {
      return this._bus.Read(address);
    }

    public void Jump(ushort address)
    {
      if (address + this._cb >= this._ct)
      {
        throw new InvalidOperationException(
          "attempted to jump to illegal address " +
          "0x" + Convert.ToString(address + this._cp, 16) + " - out of range " +
          "[0x" + Convert.ToString(this._cb, 16) + ", 0x" + Convert.ToString(this._ct, 16) + "]"
        );
      }

      this._cp = (ushort) (address + this._cb - 1);
    }

    public ushort GetCodeBase()
    {
      return this._cb;
    }

    /**
     * Fetches the next byte of memory at cp + 1.
     * Increments the program counter cp.
     */
    private byte Fetch()
    {
      return this.Read(++this._cp);
    }

    public void Step()
    {
      byte opcode = this.Fetch();
      byte[] data = new byte[this._instructions.getInstructionSize() - 1];
      for (byte i = 0; i < data.Length; i++)
      {
        data[i] = this.Fetch();
      }

      var instruction = this._instructions.getInstruction(opcode);
      if (instruction == null)
      {
        throw new InvalidOperationException("attempted to call illegal operation " + Convert.ToString(opcode, 16) +
                                            "invalid instruction"
        );
      }

      instruction.execute(this, data);
    }

    public byte[] getStack(uint size)
    {
      byte[] data = new byte[size];

      for (uint i = 0; i < size; i++)
      {
        data[i] = this.Read((ushort) (this._st - i - 1));
      }

      return data;
    }

    public void Flash(byte[] memory, ushort start)
    {
      for (int i = 0; i < memory.Length; i++)
      {
        this.Write((ushort) (i + start), memory[i]);
      }
    }

    public string GetDeviceDesignation()
    {
      return "Stack Processor - v0.0.1";
    }

    public ScottStackProcessor(ushort cs, ushort cb)
    {
      this._instructions = new ScottStackInstructionSet();
      this._flags = new Flags();
      this._flags.Clear();

      this._cb = cb;
      this._ct = (ushort) (cb + cs);
      this._cp = (ushort) (this._cb - 1);

      this._sb = 0x0100;
      this._st = this._sb;
    }
  }
}