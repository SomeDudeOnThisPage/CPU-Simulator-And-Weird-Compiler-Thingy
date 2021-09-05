namespace ScottyCompiler.compiler.context_analyzer
{
  public class ContextAttribute
  {
    public enum ContextAttributeType : byte
    {
      INT = 0
    }

    private ContextAttributeType _type;

    public ContextAttribute(ContextAttributeType type)
    {
      this._type = type;
    }
  }
}