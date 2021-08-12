using System;
using System.Collections.Generic;
using Scotty.compiler.lexer;
using ScottyCompiler.compiler;
using ScottyCompiler.compiler.ast;
using BinaryExpression = ScottyCompiler.compiler.ast.BinaryExpression;
using Expression = ScottyCompiler.compiler.ast.Expression;

namespace Scotty.compiler.syntax_analyzer {
  public class SyntaxAnalyzer {
    private int _index;
    private readonly List<Token> _tokens;
    private Token _current;

    private void Accept(TokenType token) {
      if (this._current.Type() != token) {
        // throw SyntaxError
        throw new InvalidOperationException("invalid token " + this._current.Type() + ", expected " + token);
      }

      this.Accept();
    }
    
    private void Accept() {
      this._current = this._tokens[++this._index];
    }
    
    public Program ParseProgram() {
      var statement = this.ParseStatement();
      var token = this._current;
      while (this._current.Type() == TokenType.SEMICOLON) {
        this.Accept(); // accept semicolon
        if (this._current.Type() == TokenType.EOF) {
          break;
        }
        
        var s1 = this.ParseStatement();
        var sn = new SequentialStatement(statement, s1);
        statement = sn;
      }
      
      var program = new Program(statement);
      program.SetToken(token);
      return program;
    }
    
    private Statement ParseStatement() {
      Statement statement = null;
      var token = this._current;
      
      switch (this._current.Type()) {
        case TokenType.INT: case TokenType.BOOLEAN: // int | bool identity = expression;
          var type = this._current.Type();
          this.Accept(); // accept int / boolean
          var identity = this.ParseIdentity();
          this.Accept(TokenType.EQUALS);
          var expression = this.ParseExpression();
          var declaration = new VariableDeclaration(new DataType(type == TokenType.INT ? VariableType.Type.INT : VariableType.Type.BOOLEAN), identity);
          statement = new VariableDeclarationStatement(declaration, expression);
          break;
        case TokenType.IDENTIFIER:                  // identity = expression;
          var ident = this.ParseIdentity();
          this.Accept(TokenType.EQUALS);
          var expr = this.ParseExpression();
          Console.WriteLine("EXPRESSION " + expr + " IDENTITY " + ident);
          statement = new VariableAssignStatement(ident, expr);
          break;
      }

      if (statement == null) throw new InvalidOperationException();
      
      statement.SetToken(token);
      return statement;
    }

    private Expression ParseLiteralExpression() {
      Expression expression = null;
      var token = this._current;
      
      switch (this._current.Type()) {
        case TokenType.LITERAL:
          expression = new IntegerExpression(this.ParseLiteral());
          break;
        case TokenType.IDENTIFIER:
          expression = new VariableExpression(this.ParseIdentity());
          break;
        case TokenType.OPERATOR:
          this.ParseOperator();
          this.ParseLiteralExpression();
          expression =  new UnaryExpression(this.ParseOperator(), this.ParseLiteralExpression());
          break;
        case TokenType.LH_PARENTHESES:
          this.Accept();
          expression = this.ParseExpression();
          this.Accept(TokenType.RH_PARENTHESES);
          break;
      }

      if (expression == null)
        throw new InvalidOperationException("invalid syntax of literal expression - got " + this._current.Type() +
                                            ", expected LITERAL | IDENTIFIER | OPERATOR | LH_PARENTHESES");
     
      expression.SetToken(token);
      return expression;
    }

    private Expression ParseExpression() {
      var expression = this.ParseLiteralExpression();

      while (this._current.Type() == TokenType.OPERATOR) {
        var token = this._current;
        var op = this.ParseOperator();
        var e1 = this.ParseLiteralExpression();
        var be = new BinaryExpression(expression, e1, op);
        be.SetToken(token);
        expression = be;
      }

      return expression;
    }

    private Identity ParseIdentity() {
      var identity = new Identity(this._current.Spelling());
      identity.SetToken(this._current);
      this.Accept(TokenType.IDENTIFIER);
      return identity;
    }
    
    private Literal ParseLiteral() {
      var literal = new Literal();
      literal.SetToken(this._current);
      this.Accept(TokenType.LITERAL);
      return literal;
    }
    
    private Operator ParseOperator() {
      var op = new Operator(this._current.Spelling());
      op.SetToken(this._current);
      this.Accept(TokenType.OPERATOR);
      return op;
    }
    
    public SyntaxAnalyzer(List<Token> tokens) {
      this._tokens = tokens;
      this._index = 0;
      this._current = this._tokens[this._index];
    }
  }
}