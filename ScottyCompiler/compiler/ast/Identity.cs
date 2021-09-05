namespace ScottyCompiler.compiler.ast
{
  public class Identity : AbstractSyntaxTree
  {
    // decorated declaration of the identity
    public VariableDeclaration declaration { get; set; }
    public string spelling { get; set; }

    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitIdentity(this, argument);
    }

    public Identity(string spelling)
    {
      this.spelling = spelling;
    }
  }
}