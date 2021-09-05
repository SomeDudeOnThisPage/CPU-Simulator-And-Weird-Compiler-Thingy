namespace ScottyCompiler.compiler.ast
{
  public interface Visitor
  {
    // programs
    public object VisitProgram(Program program, object argument);

    // expressions
    public object VisitIntegerExpression(IntegerExpression expression, object argument);
    public object VisitVariableExpression(VariableExpression expression, object argument);
    public object VisitUnaryExpression(UnaryExpression expression, object argument);
    public object VisitBinaryExpression(BinaryExpression expression, object argument);

    // statements
    public object VisitSequentialStatement(SequentialStatement statement, object argument);
    public object VisitVariableDeclarationStatement(VariableDeclarationStatement statement, object argument);
    public object VisitVariableAssignStatement(VariableAssignStatement statement, object argument);

    // variable name
    public object VisitVariableName(VariableName name, object argument);

    // declarations
    public object VisitDeclaration(Declaration declaration, object argument);
    public object VisitVariableDeclaration(VariableDeclaration declaration, object argument);

    // identities
    public object VisitIdentity(Identity identity, object argument);

    // todo: remove
    public object VisitLiteral(Literal literal, object argument);

    // operators
    public object VisitOperator(Operator op, object argument);

    // types
    public object VisitDataType(DataType type, object argument);
  }
}