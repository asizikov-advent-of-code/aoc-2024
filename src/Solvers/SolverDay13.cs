using System.Text.RegularExpressions;

namespace aoc_2024.Solvers;

public class SolverDay13 : ISolver {
    [PuzzleInput("13-01")]
    public void Solve(string[] input) {
        var answer = 0L;
        var dirRegEx = new Regex("[X|Y]\\+([0-9]+)");
        var posRegEx = new Regex("[X|Y]=([0-9]+)");
        for (var i = 0; i <= input.Length - 3; i += 4) {
            var buttonA = input[i].Split(": ")[1].Split(", ");
            var buttonB = input[i + 1].Split(": ")[1].Split(", ");
            var prize = input[i + 2].Split(": ")[1].Split(", ");

            (int dr, int dc) dirA = (int.Parse(dirRegEx.Match(buttonA[0]).Groups[1].Value), int.Parse(dirRegEx.Match(buttonA[1]).Groups[1].Value));
            (int dr, int dc) dirB = (int.Parse(dirRegEx.Match(buttonB[0]).Groups[1].Value), int.Parse(dirRegEx.Match(buttonB[1]).Groups[1].Value));

            (int r, int c) prizePos = (int.Parse(posRegEx.Match(prize[0]).Groups[1].Value), int.Parse(posRegEx.Match(prize[1]).Groups[1].Value));

            var cost = Greedy([dirA, dirB], prizePos);
            if (cost != -1) {
                Console.WriteLine($"Prize: {prizePos.r}, {prizePos.c}, Button A: {dirA.dr}, {dirA.dc}, Button B: {dirB.dr}, {dirB.dc}");
                Console.WriteLine($"Cost: {cost}");
                answer += cost;
            }
        }

        Console.WriteLine($"Answer: {answer}");

        int Greedy((int dr, int dc)[] dirs, (int r, int c) prizePos) {
            var minCost = int.MaxValue;

            for (var a = 0; a < 100; a++)
            for (var b = 0; b < 100; b++) {
                var calculatedR = a * dirs[0].dr + b * dirs[1].dr;
                var calculatedC = a * dirs[0].dc + b * dirs[1].dc;

                if (calculatedR == prizePos.r && calculatedC == prizePos.c) {
                    var cost = a * 3 + b;
                    if (cost < minCost) {
                        minCost = cost;
                    }
                }
            }

            return minCost == int.MaxValue ? -1 : minCost;
        }
    }
}