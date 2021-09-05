namespace ScottyCompiler.compiler.ast
{
  public class DataType : AbstractSyntaxTree
  {
    public VariableType.Type type { get; set; }

    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitDataType(this, argument);
    }

    public DataType(VariableType.Type type)
    {
      this.type = type;
    }
  }
}