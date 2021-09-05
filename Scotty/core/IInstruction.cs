namespace Scotty.core
{
  public interface IInstruction<in T> where T : IProcessingUnit
  {
    public void execute(T cpu, params byte[] arguments);
  }
}