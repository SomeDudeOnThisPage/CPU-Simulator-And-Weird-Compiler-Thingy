using System;

namespace Scotty.compiler.lexer {
  public enum TokenType : byte {
    IDENTIFIER = 0,
    LITERAL = 1,
    OPERATOR = 2,
    INT = 3,
    BOOLEAN = 4,
    EQUALS = 5,
    SEMICOLON = 6,
    LH_PARENTHESES = 7,
    RH_PARENTHESES = 8,
    EOF = 9
  }
  
  public class Token {
    public static readonly string[] SPELLING = {
      "<>", "<>", "<>",
      "int", "boolean", "=", ";", "(", ")",
      "<>"
    };

    private readonly TokenType _type;
    private readonly string _spelling;
    private int _line;
    private int _position;

    public void SetPosition(int line, int position) {
      this._line = line;
      this._position = position;
    }

    public int[] GetPosition() {
      return new[] {this._line, this._position};
    }

    private static TokenType GetType(string spelling) {
      for (byte i = (byte) TokenType.INT; i < (byte) TokenType.RH_PARENTHESES; i++) {
        if (spelling == Token.SPELLING[i]) {
          return Enum.GetValues<TokenType>()[i];
        }
      }

      return 0;
    }

    public string Spelling() {
      return this._spelling;
    }
    
    public TokenType Type() {
      return this._type;
    }
    
    public Token(TokenType type, string spelling, int position) {
      this._position = position;
      this._spelling = spelling;
      this._type = type == TokenType.IDENTIFIER ? Token.GetType(spelling) : type;
    }
    
  }
}