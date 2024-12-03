using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;

namespace aoc_2024.Solvers;

public class SolverDay03 : ISolver {
    [PuzzleInput("03-01")]
    public void Solve(string[] input) {
        var answer = 0L;
        foreach (var inputLine in input) {
            answer += ParseLine(inputLine);
        }

        Console.WriteLine($"Answer: {answer}");
    }

    long ParseLine(string inputLine) {
        var regex = new Regex(@"mul\((\d+),(\d+)\)");
        var matches = regex.Matches(inputLine);
        var result = 0L;
        foreach (Match match in matches) {
            var x = int.Parse(match.Groups[1].Value);
            var y = int.Parse(match.Groups[2].Value);
            result += x * y;
        }

        return result;
    }
}