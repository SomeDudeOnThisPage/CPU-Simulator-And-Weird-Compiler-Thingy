using System;
using System.Collections.Generic;
using Scotty.core;

namespace Scotty.assembler
{
  public class TestAssembler<U> where U : IProcessingUnit
  {
    private static ushort NumberFromString(string number)
    {
      return Convert.ToUInt16(number, number.StartsWith("0x") ? 16 : 10);
    }

    public byte[] assemble(string program, IInstructionSet<U> instructions)
    {
      var lines = program.Trim().Split("\n");
      var bytes = new List<byte>();

      var labels = new Dictionary<string, int>();

      // construct label dictionary
      int c = 0;
      foreach (var native in lines)
      {
        string line = native.Trim();

        if (line.Equals("") || line.StartsWith(";"))
        {
          continue;
        }

        if (line.StartsWith('.'))
        {
          int end = Math.Max(line.IndexOf('.'), line.Length);
          labels[line[0..end]] = c * instructions.getInstructionSize();
          continue;
        }

        c++;
      }

      foreach (var native in lines)
      {
        string line = native.Trim();

        if (line.Equals("") || line.StartsWith(";") || line.StartsWith("."))
        {
          continue;
        }

        if (line.Contains('.'))
        {
          int end = Math.Max(line.IndexOf(' ', line.IndexOf('.')), line.Length);
          string label = line[line.IndexOf('.')..end];
          Console.WriteLine("FOUND LABEL " + label + " " + labels[label]);
          line = line.Replace(label, labels[label] + "");
        }

        byte opcode = 0; // opcode part of the instruction
        ushort encoded = 0; // data part of the instruction

        string[] tokens = line.Split(" ");

        // parse instruction from token 0
        opcode = instructions.getInstruction(tokens[0].ToUpper());
        var data0 = tokens.Length > 1 ? tokens[1] : "0x0000";

        encoded += NumberFromString(data0);

        if (tokens[0] == "ja")
        {
          Console.WriteLine("AYOO " + encoded + " " + (encoded / 3));
        }

        bytes.Add(opcode);
        bytes.Add((byte) (encoded >> 8));
        bytes.Add((byte) (encoded & 0xFF));
        Console.Out.WriteLine(Convert.ToString(encoded, 2));
      }

      return bytes.ToArray();
    }
  }
}