using System;
using System.Collections.Generic;
using ScottyCompiler.compiler.ast;
using ScottyCompiler.compiler.exception;

namespace ScottyCompiler.compiler.context_analyzer {
  public class ContextAnalyzer : Visitor {
    private readonly Dictionary<string, Declaration> _idt;

    public object VisitProgram(ast.Program program, object argument) {
      return program.Statement.Visit(this, argument);
    }

    public object VisitIntegerExpression(IntegerExpression expression, object argument) {
      expression.type = VariableType.Type.INT;
      return expression.type;
    }

    public object VisitVariableExpression(VariableExpression expression, object argument) {
      expression.type = (VariableType.Type) expression.variable.Visit(this, argument);
      Console.WriteLine("VISITED VARIABLE EXPRESSION OF TYPE " + expression.type);
      return expression.type;
    }

    public object VisitUnaryExpression(UnaryExpression expression, object argument) {
      throw new System.NotImplementedException();
    }

    public object VisitBinaryExpression(BinaryExpression expression, object argument) {
      var t0 = (VariableType.Type) expression.e0.Visit(this, argument);
      var t1 = (VariableType.Type) expression.e1.Visit(this, argument);

      var op = expression.op;
      var opDeclaration = this._idt[op.spelling];
      if (opDeclaration is not BinaryOperatorDeclaration or null) {
        // todo: throw syntax error
        Console.WriteLine("NOT A BINARY OPERATOR");
      }

      var binaryOpDeclaration = (BinaryOperatorDeclaration) opDeclaration;
      if (binaryOpDeclaration.t0 == t0 && binaryOpDeclaration.t1 == t1) {
        return t1;
      }
      
      int[] p = expression.e0.GetToken().GetPosition();
      throw new TypeMismatchException("expression", t0, t1, p[0], p[1]);
    }

    public object VisitSequentialStatement(SequentialStatement statement, object argument) {
      statement.c1.Visit(this, argument);
      statement.c2.Visit(this, argument);
      return null;
    }

    public object VisitVariableDeclarationStatement(VariableDeclarationStatement statement, object argument) {
      statement.declaration.Visit(this, argument);
      statement.value.Visit(this, argument);
      return null;
    }

    public object VisitVariableAssignStatement(VariableAssignStatement statement, object argument) {
      Console.WriteLine(statement.value);
      var t0 = (VariableType.Type) statement.identity.Visit(this, null);
      var t1 = (VariableType.Type) statement.value.Visit(this, null);
      if (t0 == t1) return null;
      int[] p = statement.value.GetToken().GetPosition();
      throw new TypeMismatchException(statement.identity.spelling, t0, t1, p[0], p[1]);
    }

    public object VisitVariableName(VariableName name, object argument) {
      throw new System.NotImplementedException();
    }

    public object VisitDeclaration(Declaration declaration, object argument) {
      Console.WriteLine("VISIT DECL");
      return null;
    }

    public object VisitVariableDeclaration(VariableDeclaration declaration, object argument) {
      // add declaration to id table
      declaration.dataType.Visit(this, argument);
      
      Console.WriteLine("ADDED " + declaration.identity.spelling + " TO ID TABLE.");
      Console.WriteLine("DECLARATION TYPE IS " + declaration.dataType.type);

      this._idt[declaration.identity.spelling] = declaration;
      return null;
    }

    public object VisitIdentity(Identity identity, object argument) {
      identity.declaration = (VariableDeclaration) this._idt[identity.spelling];
      Console.WriteLine("VISIT IDENTITY " + identity.spelling + " WITH DATA TYPE " + identity.declaration.dataType.type);
      return identity.declaration.dataType.type;
    }

    public object VisitLiteral(Literal literal, object argument) {
      throw new System.NotImplementedException();
    }

    public object VisitOperator(Operator op, object argument) {
      throw new System.NotImplementedException();
    }

    public object VisitDataType(DataType type, object argument) {
      return type.type;
    }
    
    public ContextAnalyzer() {
      this._idt = new Dictionary<string, Declaration>();
      var falseIdentity = new Identity("false");
      var falseDeclaration = new VariableDeclaration(new DataType(VariableType.Type.BOOLEAN), falseIdentity);
      falseIdentity.declaration = falseDeclaration;
      this._idt["false"] = falseDeclaration;

      var plusOperatorDeclaration = new BinaryOperatorDeclaration(VariableType.Type.INT, VariableType.Type.INT);
      this._idt["+"] = plusOperatorDeclaration;

    }
  }
}