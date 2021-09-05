using Scotty.scotty;

namespace Scotty.core
{
  public interface IProcessingUnit : IAdressableDevice
  {
    public void Attach<T>(Bus<ScottStackProcessor> bus) where T : IProcessingUnit;
  }
}