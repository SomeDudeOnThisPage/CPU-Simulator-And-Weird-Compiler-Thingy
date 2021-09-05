using System;
using System.Collections.Generic;
using System.IO;
using Scotty.compiler.lexer;
using Scotty.compiler.syntax_analyzer;
using ScottyCompiler.compiler.context_analyzer;

namespace ScottyCompiler
{
  public class Program
  {
    public static void Main(string[] args)
    {
      // tokenize text thing...
      var lexer = new Lexer(
        File.ReadAllText(@"F:\dev\IdeaProjects\ShitCPU\Scotty\ScottyCompiler\TestCode.txt")
      );
      var tokens = new List<Token>();
      while (!lexer.IsFinished())
      {
        tokens.Add(lexer.Scan());
      }

      var analyzer = new SyntaxAnalyzer(tokens);
      var program = analyzer.ParseProgram();
      program.PrintPretty("", true);

      var context = new ContextAnalyzer();
      context.VisitProgram(program, null);
    }
  }
}