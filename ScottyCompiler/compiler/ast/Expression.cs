using Scotty.compiler.lexer;

namespace ScottyCompiler.compiler.ast
{
  public abstract class Expression : AbstractSyntaxTree
  {
    public VariableType.Type type { get; set; }
  }

  public class IntegerExpression : Expression
  {
    public Literal literal { get; set; }

    public IntegerExpression(Literal literal)
    {
      this.literal = literal;
      this._children.Add(literal);
    }

    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitIntegerExpression(this, argument);
    }
  }

  public class VariableExpression : Expression
  {
    public Identity variable { get; set; }

    public VariableExpression(Identity identity)
    {
      this.variable = identity;
      this._children.Add(identity);
    }

    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitVariableExpression(this, argument);
    }
  }

  public class UnaryExpression : Expression
  {
    public Expression e0 { get; set; }
    public Operator op { get; set; }

    public UnaryExpression(Operator op, Expression e0)
    {
      this.e0 = e0;
      this.op = op;

      this._children.Add(op);
      this._children.Add(e0);
    }

    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitUnaryExpression(this, argument);
    }
  }

  public class BinaryExpression : Expression
  {
    public Expression e0 { get; set; }
    public Expression e1 { get; set; }
    public Operator op { get; set; }

    public BinaryExpression(Expression e0, Expression e1, Operator op)
    {
      this.e0 = e0;
      this.e1 = e1;
      this.op = op;

      this._children.Add(e0);
      this._children.Add(op);
      this._children.Add(e1);
    }

    public override object Visit(Visitor visitor, object argument)
    {
      return visitor.VisitBinaryExpression(this, argument);
    }
  }
}