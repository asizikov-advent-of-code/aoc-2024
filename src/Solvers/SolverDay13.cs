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

            (long dr, long dc) dirA = (long.Parse(dirRegEx.Match(buttonA[0]).Groups[1].Value), long.Parse(dirRegEx.Match(buttonA[1]).Groups[1].Value));
            (long dr, long dc) dirB = (long.Parse(dirRegEx.Match(buttonB[0]).Groups[1].Value), long.Parse(dirRegEx.Match(buttonB[1]).Groups[1].Value));

            (long r, long c) prizePos = (10000000000000 + long.Parse(posRegEx.Match(prize[0]).Groups[1].Value), 10000000000000 + long.Parse(posRegEx.Match(prize[1]).Groups[1].Value));

            var cost = Caculate([dirA, dirB], prizePos);
            if (cost != -1) {
                answer += cost;
            }
        }

        Console.WriteLine($"Answer: {answer}"); 
        
        long Caculate((long dr, long dc)[] dirs, (long r, long c) prizePos) {
            var a = (double)(prizePos.r * dirs[1].dc - prizePos.c * dirs[1].dr) / (dirs[0].dr * dirs[1].dc - dirs[0].dc * dirs[1].dr);
            var b = (double)(prizePos.r * dirs[0].dc - prizePos.c * dirs[0].dr) / (dirs[1].dr * dirs[0].dc - dirs[1].dc * dirs[0].dr);
            
            if (Math.Floor(a) != a || Math.Floor(b) != b || a < 0 || b < 0) return -1;
            return (long)a * 3 + (long)b;
            
        }
    }
}