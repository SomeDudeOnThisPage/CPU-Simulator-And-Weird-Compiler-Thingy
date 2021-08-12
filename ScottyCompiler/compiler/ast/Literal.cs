namespace ScottyCompiler.compiler.ast {
  public class Literal : AbstractSyntaxTree {
    public override object Visit(Visitor visitor, object argument) {
      return visitor.VisitLiteral(this, argument);
    }
  }
}