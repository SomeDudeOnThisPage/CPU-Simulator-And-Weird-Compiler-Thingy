namespace ScottyCompiler.compiler.ast
{
  public abstract class Statement : AbstractSyntaxTree
  {
  }

  public class SequentialStatement : Statement
  {
    // statement ; statement
    public Statement c1 { get; set; }
    public Statement c2 { get; set; }

    public SequentialStatement(Statement c1, Statement c2)
    {
      this.c1 = c1;
      this.c2 = c2;

      this._children.Add(this.c1);
      this._children.Add(this.c2);
    }

    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitSequentialStatement(this, argument);
    }
  }

  public class VariableDeclarationStatement : Statement
  {
    // var-name = expression ;
    public Declaration declaration { get; set; }
    public Expression value { get; set; }

    public VariableDeclarationStatement(Declaration declaration, Expression value)
    {
      this.declaration = declaration;
      this.value = value;
      this._children.Add(this.declaration);
      this._children.Add(this.value);
    }

    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitVariableDeclarationStatement(this, argument);
    }
  }

  public class VariableAssignStatement : Statement
  {
    // var-name = expression ;
    public Identity identity { get; set; }
    public Expression value { get; set; }

    public VariableAssignStatement(Identity identity, Expression value)
    {
      this.value = value;
      this.identity = identity;
      this._children.Add(this.identity);
      this._children.Add(this.value);
    }

    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitVariableAssignStatement(this, argument);
    }
  }
}