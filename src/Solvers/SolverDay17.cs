using System.Text.RegularExpressions;

namespace aoc_2024.Solvers;

public class SolverDay17 : ISolver {
    [PuzzleInput("17-01")]
    public void Solve(string[] input) {
        var registers = new Dictionary<char, long>();
        var registerRegex = new Regex(@"Register (?<code>[A-Z]): (?<value>\d+)");
        var program = new List<long>();
        var registresParsed = false;
        foreach (var line in input) {
            if (line is "") {
                registresParsed = true;
                continue;
            }

            if (!registresParsed) {
                var groups = registerRegex.Match(line).Groups;
                registers[groups["code"].Value[0]] = int.Parse(groups["value"].Value);
            }
            else {
                var s = line.Substring(9);
                program = s.Split(',').Select(long.Parse).ToList();
            }
        }
        
        var answer = FindAnswer(program, program).Min();
        Console.WriteLine($"Answer: {answer}");


        IList<long> Run(Dictionary<char, long> registers, List<long> program) {
            var output = new List<long>();
            for (var i = 0; i < program.Count - 1;) {
                var opcode = program[i];
                var operand = GetOperand((int)program[i + 1]);
                switch (opcode) {
                    case 0:
                        var denom = 1 << (int)operand;
                        registers['A'] /= denom;
                        i += 2;
                        break;
                    case 1:
                        registers['B'] ^= (int)operand;
                        i += 2;
                        break;
                    case 2:
                        registers['B'] = mod(operand);
                        i += 2;
                        break;
                    case 3:
                        if (registers['A'] is 0) {
                            i += 2;
                            break;
                        }

                        i = (int)operand;
                        break;
                    case 4:
                        registers['B'] ^= registers['C'];
                        i += 2;
                        break;
                    case 5:
                        output.Add(mod(operand));
                        i += 2;
                        break;
                    case 6:
                        registers['B'] = registers['A'] / operand * operand;
                        i += 2;
                        break;
                    case 7:
                        var num = 1 << (int)operand;
                        registers['C'] = registers['A'] / num;
                        i += 2;
                        break;
                    default:
                        throw new InvalidOperationException($"Unknown opcode {opcode}");
                }
            }

            return output;

            long GetOperand(int val) {
                if (val <= 3) return val;
                if (val == 4) return registers['A'];
                if (val == 5) return registers['B'];
                if (val == 6) return registers['C'];
                throw new Exception("unknown operand");
            }

            long mod(long x) => x % 8;
        }

        IEnumerable<long> FindAnswer(List<long> program, List<long> output) {
            if (!output.Any()) {
                yield return 0;
                yield break;
            }

            foreach (var ah in FindAnswer(program, output[1..])) {
                for (var al = 0; al < 8; al++) {
                    var a = ah * 8 + al;
                    if (Run(
                            new Dictionary<char, long> {
                                { 'A', a },
                                { 'B', 0 },
                                { 'C', 0 }
                            }
                            , program).SequenceEqual(output)) {
                        yield return a;
                    }
                }
            }
        }
    }
}