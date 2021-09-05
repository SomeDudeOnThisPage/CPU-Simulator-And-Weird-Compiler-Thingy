namespace mos6502.mos6502
{
  public interface IProcessingUnit : IAdressableDevice
  {
    public void Step();
  }
}