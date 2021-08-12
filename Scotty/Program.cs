using System;
using System.IO;
using Scotty.assembler;
using Scotty.core;
using Scotty.scotty;

namespace Scotty {
  public class Program {
    private static void Main(string[] args) {
      var program = File.ReadAllText(@"F:\dev\IdeaProjects\ShitCPU\Scotty\Scotty\programs\add_until_20.asm");
      var bytecode = new TestAssembler<ScottStackProcessor>().assemble(program, new ScottStackInstructionSet());
      
      // load bytecode into cpu
      var cpu = new ScottStackProcessor(0x0800, 0x0100);
      var bus = new Bus<ScottStackProcessor>();
      var ram = new RandomAccessMemory(0x7FFF);
      
      bus.Attach(ram, 0x0000, 0x7FFF);
      cpu.Attach(bus);
      cpu.Flash(bytecode, cpu.GetCodeBase());

      Console.WriteLine("\n------------------------------------\n");
      for (int j = 0; j < 1000; j++) {
        cpu.Step();
        if (cpu.Halt) {
          break;
        }
      }
      
      // Console.Clear();
      Console.WriteLine("[0x0DFF] :: " + cpu.Read(0x0DFF));
      Console.WriteLine("[0x0DFD] :: " + cpu.Read(0x0DFD));
      Console.WriteLine("Hello World!");
    }
  }
}