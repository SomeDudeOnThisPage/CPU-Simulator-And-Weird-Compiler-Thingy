using System;
using System.IO;

/*
 * Token ::= Letter (Letter | Digit)*
 *            | Digit Digit* 
 *            | '+' | '-' | '<' | '>' | '='  
 *            | ; | = | ( | ) 
 *            | EOF
 * Separator ::= EOL | ' '
 * Digit ::= a number lmao
 */
namespace Scotty.compiler.lexer {
  public class Lexer : StringReader {
    private char _current;
    private string _spelling;
    private bool _finished;

    private int _line;
    private int _position;
    
    private bool IsLetter(char character) {
      return character is >= 'a' and <= 'z';
    }

    private bool IsDigit(char character) {
      return character is >= '0' and <= '9';
    }
    
    private TokenType ScanToken() {
      // parse letters
      if (this.IsLetter(this._current)) {
        this.Take();
        while (this.IsLetter(this._current) || this.IsDigit(this._current)) {
           this.Take();
        }
        return TokenType.IDENTIFIER;
      }

      if (this.IsDigit(this._current)) {
        this.Take();
        while (this.IsDigit(this._current)) {
          this.Take();
        }

        return TokenType.LITERAL;
      }

      switch (this._current) {
        case '+': case '-': case '<': case '>':
          this.Take();
          return TokenType.OPERATOR;
        case '=':
          this.Take();
          if (this._current != '=') return TokenType.EQUALS;
          this.Take(); // == is operator, = is equals assignment
          return TokenType.OPERATOR;
        case '(':
          this.Take();
          return TokenType.LH_PARENTHESES;
        case ')':
          this.Take();
          return TokenType.RH_PARENTHESES;
        case ';':
          this.Take();
          return TokenType.SEMICOLON;
        case (char) 0x1a:
          this.Take();
          return TokenType.EOF;
      }

      throw new InvalidOperationException("unknown token " + this._current);
    }

    private void Take() {
      this._spelling += this._current;
      this._current = (char) this.Read();
    }

    private char ScanSeparator() {
      var character = (char) this.Read();
      while (character is ' ' or '\n' or '\r') {
        if (character == '\n') {
          this._line++;
          this._position = 0;
        } else {
          this._position++;
        }

        character = (char) this.Read();
      }

      return character;
    }

    public bool IsFinished() {
      return this._finished;
    }
    
    public Token Scan() {
      if (this._current == ' ' || this._current == '\n' || this._current == '\r') {
        if (this._current == '\n') {
          this._line++;
          this._position = 0;
        } else {
          this._position++;
        }
        
        this._current = this.ScanSeparator();
      }

      if (this.Peek() == -1) {
        this._finished = true;
        return new Token(TokenType.EOF, this._spelling, 0);
      }

      this._spelling = "";
      var token = new Token(this.ScanToken(), this._spelling, 0);
      token.SetPosition(this._line, this._position);
      return token;
    }
    

    public Lexer(string s) : base(s) {
      this._current = '\n';
      this._spelling = "";
      this._finished = false;

      this._line = 0;
      this._position = 0;
    }
  }
}