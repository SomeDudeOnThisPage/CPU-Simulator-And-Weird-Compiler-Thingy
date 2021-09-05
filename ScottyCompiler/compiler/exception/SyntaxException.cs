using System;

namespace ScottyCompiler.compiler.exception
{
  public class SyntaxException : Exception
  {
    private int _line;
    private int _position;

    public override string ToString()
    {
      return "Error at line " + this._line + " at position " + this._position + " - " + this.Message;
    }

    protected SyntaxException(string message, int line, int position) : base(message)
    {
      this._line = line;
      this._position = position;
    }
  }

  public class TypeMismatchException : SyntaxException
  {
    public TypeMismatchException(string v, VariableType.Type expected, VariableType.Type got, int line, int position)
      : base("type mismatch for variable '" + v + "' - expected " + expected + ", got " + got, line, position + 2)
    {
    }
  }
}