namespace ScottyCompiler.compiler.ast {
  public class Operator : AbstractSyntaxTree {
    public string spelling { get; set; }

    public Operator(string spelling) {
      this.spelling = spelling;
    }
    
    public override object Visit(Visitor visitor, object argument) {
      return visitor.VisitOperator(this, argument);
    }
  }
  
  public class BinaryOperatorDeclaration : Declaration {
    public VariableType.Type t0;
    public VariableType.Type t1;

    public BinaryOperatorDeclaration(VariableType.Type t0, VariableType.Type t1) {
      this.t0 = t0;
      this.t1 = t1;
    }
    
    public override object Visit(Visitor visitor, object argument) {
      return null;
    }
  }
}