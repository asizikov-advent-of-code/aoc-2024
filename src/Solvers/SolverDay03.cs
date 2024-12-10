using System.Text.RegularExpressions;

namespace aoc_2024.Solvers;

public class SolverDay03 : ISolver {
    [PuzzleInput("03-01")]
    public void Solve(string[] input) {
        var answer = 0L;
        var enabled = true;
        foreach (var inputLine in input) {
            var ( sum, state) = ParseLine(inputLine, enabled);
            answer += sum;
            enabled = state;
        }

        Console.WriteLine($"Answer: {answer}");
    }

    (long sum, bool enabled) ParseLine(string inputLine, bool enabled) {
        var regex = new Regex(@"mul\((\d+),(\d+)\)|do\(\)|don't\(\)");
        var matches = regex.Matches(inputLine);
        var result = 0L;
        var multiply = enabled;
        foreach (Match match in matches) {
            switch (match.Groups[0].Value)
            {
                case "do()":
                    multiply = true;
                    break;
                case "don't()":
                    multiply = false;
                    break;
                default: {
                    if (multiply) {
                        var x = long.Parse(match.Groups[1].Value);
                        var y = long.Parse(match.Groups[2].Value);
                        result += x * y;
                    }
                    break;
                }
            }
        }

        return (result, multiply);
    }
}