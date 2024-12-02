namespace aoc_2024.Solvers;

public class SolverDay02 : ISolver {
    [PuzzleInput("02-01")]
    public void Solve(string[] input) {
        var answer = 0;
        foreach (var line in input) {
            var levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var isUnsafe = false;
            var isIncreasing = levels[0] < levels[1];
            for (var i = 0; i < levels.Length - 1; i++) {
                if (levels[i] == levels[i + 1]) {
                    isUnsafe = true;
                    break;
                }
                if (isIncreasing && levels[i] > levels[i + 1]) {
                    isUnsafe = true;
                    break;
                }
                if (!isIncreasing && levels[i] < levels[i + 1]) {
                    isUnsafe = true;
                    break;
                }
                if (Math.Abs(levels[i] - levels[i + 1]) > 3) {
                    isUnsafe = true;
                    break;
                }
                
            }
            if (!isUnsafe) {
                answer++;
            }
        }


        Console.WriteLine($"Answer: {answer}");
    }
}