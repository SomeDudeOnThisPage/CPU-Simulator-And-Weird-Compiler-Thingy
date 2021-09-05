namespace ScottyCompiler.compiler.ast
{
  public abstract class Variable : AbstractSyntaxTree
  {
  }

  public class VariableName : Variable
  {
    // public
    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitVariableName(this, argument);
    }
  }
}