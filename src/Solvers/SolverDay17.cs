using System.Text;
using System.Text.RegularExpressions;

namespace aoc_2024.Solvers;

public class SolverDay17 : ISolver {
    [PuzzleInput("17-01")]
    public void Solve(string[] input) {
        var registers = new Dictionary<char, int>();
        var registerRegex = new Regex(@"Register (?<code>[A-Z]): (?<value>\d+)"); 
        var program = new List<int>();
        var registresParsed = false;
        foreach (var line in input) {
            if (line is "") {
                registresParsed = true;
                continue;
            }

            if (!registresParsed) {
                var groups = registerRegex.Match(line).Groups;
                registers[groups["code"].Value[0]] = int.Parse(groups["value"].Value);
            }else {
                var s = line.Substring(9);
                program = s.Split(',').Select(int.Parse).ToList();
            }
        }


        var output = new StringBuilder();
        for (var i = 0; i < program.Count - 1;) {
            var opcode = program[i];
            var operand = GetOperand(program[i + 1]);
            switch (opcode) {
                case 0:
                    var denom = 1 << operand;
                    registers['A'] /= denom;
                    i += 2;
                    break;
                case 1: 
                    registers['B'] ^= operand;
                    i += 2;
                    break;
                case 2:
                    registers['B'] =  mod(operand);
                    i += 2;
                    break;
                case 3:
                    if (registers['A'] is 0) {
                        i += 2;
                        break;
                    }
                    i = operand;
                    break;
                case 4: 
                    registers['B'] ^= registers['C'];
                    i += 2;
                    break;
                case 5:
                    output.Append($"{mod(operand)},");
                    i += 2;
                    break;
                case 6:
                    registers['B'] = registers['A'] / operand * operand;
                    i += 2;
                    break;
                case 7:
                    var num = 1 << operand;
                    registers['C'] = registers['A'] / num;
                    i+= 2;
                    break;
                default:
                    throw new InvalidOperationException($"Unknown opcode {opcode}");
            }
        }
        
        Console.WriteLine("Registers:");
        foreach (var (key, value) in registers) {
            Console.WriteLine($"\t{key}: {value}");
        }
        Console.WriteLine(output.ToString());
        
        var answer = 0L;
        Console.WriteLine($"Answer: {answer}");

        int mod(int x) => x % 8;

        int GetOperand(int val) {
            if (val <= 3) return val;
            if (val == 4) return registers['A'];
            if (val == 5) return registers['B'];
            if (val == 6) return registers['C'];
            throw new Exception("unknown operand");
        }
    }
}