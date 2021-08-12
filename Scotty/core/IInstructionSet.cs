namespace Scotty.core {
  public interface IInstructionSet<U> where U : IProcessingUnit {
    public byte getInstruction(string instruction);
    public IInstruction<U> getInstruction(byte instruction);
    public int getInstructionSize();
  }
}