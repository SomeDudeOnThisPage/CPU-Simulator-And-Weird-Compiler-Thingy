using System;
using System.Collections.Generic;
using Scotty.compiler.lexer;

namespace ScottyCompiler.compiler.ast {
  public abstract class AbstractSyntaxTree {
    protected readonly List<AbstractSyntaxTree> _children;
    private Token _token;

    public abstract object Visit(Visitor visitor, object argument);

    public AbstractSyntaxTree SetToken(Token token) {
      this._token = token;
      return this;
    }

    public Token GetToken() {
      return this._token;
    }
    
    public void PrintPretty(string indent, bool last) {
      Console.Write(indent);
      if (last) {
        Console.Write("\\-");
        indent += "  ";
      } else {
        Console.Write("|-");
        indent += "| ";
      }
      Console.WriteLine(this.GetType().Name);

      for (int i = 0; i < this._children.Count; i++)
        this._children[i].PrintPretty(indent, i == this._children.Count - 1);
    }
    
    protected AbstractSyntaxTree() {
      this._children = new List<AbstractSyntaxTree>();
    }
  }
}