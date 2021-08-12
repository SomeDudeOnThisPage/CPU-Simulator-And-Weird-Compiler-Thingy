using System;
using System.Collections.Generic;

namespace Scotty.core {
  public class Bus<T> where T : IProcessingUnit {
    private record DeviceAddressSpace(IAdressableDevice device, ushort low, ushort high);
    
    private readonly List<DeviceAddressSpace> _devices;

    public void Write(ushort address, byte data) {
      foreach (var (device, low, high) in this._devices) {
        if (low > address || high < address) continue;
        
        device.Write((ushort) (address - low), data);
        return;
      }
      
      throw new InvalidOperationException("attempted to write to address 0x" + Convert.ToString(address, 16) +
        " - no device attached at this address");
    }

    public byte Read(ushort address) {
      foreach (var (device, low, high) in this._devices) {
        if (low <= address && high > address) {
          return device.Read((ushort) (address - low));
        }
      }

      return 0x00;
    }

    public void Attach(IAdressableDevice device, ushort low, ushort high) {
      this._devices.Add(new DeviceAddressSpace(device, low, high));
    }
    
    public Bus() {
      this._devices = new List<DeviceAddressSpace>();
    }
  }
}