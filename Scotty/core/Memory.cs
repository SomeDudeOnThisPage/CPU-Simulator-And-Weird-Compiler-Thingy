using System;

namespace Scotty.core {
  public abstract class Memory : IAdressableDevice {
    private readonly byte[] _memory;
    private readonly bool _writable;

    public abstract string GetDeviceDesignation();

    public byte Read(ushort address) {
      if (address >= this._memory.Length) {
        throw new InvalidOperationException("attempted to read from address 0x" + Convert.ToString(address, 16) + 
          " of Device '" + this.GetDeviceDesignation() + "' with maximum address range of 0x" + 
          Convert.ToString(this._memory.Length, 16));
      }
      
      return this._memory[address];
    }

    public void Write(ushort address, byte data) {
      if (!this._writable) {
        throw new InvalidOperationException("attempted to write to address 0x" + Convert.ToString(address, 16) +
          " of non-writable Device '" + this.GetDeviceDesignation() + "'");
      }

      if (address >= this._memory.Length) {
        throw new InvalidOperationException("attempted to write to address 0x" + Convert.ToString(address, 16) + 
          " of Device '" + this.GetDeviceDesignation() + "' with maximum address range of 0x" + 
          Convert.ToString(this._memory.Length, 16));
      }
      
      this._memory[address] = data;
    }
    
    protected Memory(ushort size, bool writable) {
      this._memory = new byte[size];
      this._writable = writable;
    }
  }
  
  public class RandomAccessMemory : Memory {
    public RandomAccessMemory(ushort size) : base(size, true) {}
    public override string GetDeviceDesignation() {
      return "Default Random Access Memory v1.0.0";
    }
  }

  public class ReadOnlyMemory : Memory {
    public ReadOnlyMemory(ushort size) : base(size, false) {}
    public override string GetDeviceDesignation() {
      return "Default Read Only Memory v1.0.0";
    }
  }
}