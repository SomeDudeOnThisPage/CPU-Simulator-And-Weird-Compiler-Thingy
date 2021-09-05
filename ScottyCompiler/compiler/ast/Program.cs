namespace ScottyCompiler.compiler.ast
{
  public class Program : AbstractSyntaxTree
  {
    public Statement Statement { get; set; }

    public Program(Statement statement)
    {
      this.Statement = statement;
      this._children.Add(statement);
    }

    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitProgram(this, argument);
    }
  }
}