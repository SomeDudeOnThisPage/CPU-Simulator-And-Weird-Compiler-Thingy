namespace Scotty.core {
  public interface IAdressableDevice {
    public string GetDeviceDesignation();
    public byte Read(ushort address);
    public void Write(ushort address, byte data);
  }
}