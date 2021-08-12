namespace ScottyCompiler.compiler {
  public class VariableType {
    private static readonly VariableType BOOLEAN = new VariableType(VariableType.Type.BOOLEAN);
    private static readonly VariableType INTEGER = new VariableType(VariableType.Type.INT);
    private static readonly VariableType ERROR = new VariableType(VariableType.Type.ERROR);

    public enum Type : byte {
      BOOLEAN = 0,
      INT = 1,
      ERROR = 255
    }
    
    private readonly Type _type;

    protected bool Equals(VariableType other) {
      return _type == other._type;
    }

    public override int GetHashCode() {
      return (int) _type;
    }

    public VariableType(Type type) {
      this._type = type;
    }
  }
}