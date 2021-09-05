namespace mos6502.mos6502
{
  public interface IAdressableDevice
  {
    public byte Read(ushort address);
    public void Write(ushort address, byte data);
  }
}