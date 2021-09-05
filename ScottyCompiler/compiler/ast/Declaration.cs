namespace ScottyCompiler.compiler.ast
{
  public class Declaration : AbstractSyntaxTree
  {
    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitDeclaration(this, argument);
    }
  }

  public class VariableDeclaration : Declaration
  {
    public DataType dataType { get; set; }
    public Identity identity { get; set; }

    public VariableDeclaration(DataType type, Identity identity)
    {
      this.dataType = type;
      this.identity = identity;

      this._children.Add(type);
      this._children.Add(identity);
    }

    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitVariableDeclaration(this, argument);
    }
  }
}