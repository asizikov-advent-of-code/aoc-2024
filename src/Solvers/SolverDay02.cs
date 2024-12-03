namespace aoc_2024.Solvers;

public class SolverDay02 : ISolver {
    [PuzzleInput("02-01")]
    public void Solve(string[] input) {
        var answer = 0;
        foreach (var line in input) {
            var levels = line.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var isUnsafe = ValidateLevels(levels);
            if (isUnsafe) {
                for (var i = 0; i < levels.Length; i++) {
                    var copy = new int[levels.Length - 1];
                    Array.Copy(levels, 0, copy, 0, i);
                    Array.Copy(levels, i + 1, copy, i, levels.Length - i - 1);
                    if (!ValidateLevels(copy)) {
                        isUnsafe = false;
                        break;
                    }
                }
            }

            if (!isUnsafe) {
                answer++;
            }
        }
        
        Console.WriteLine($"Answer: {answer}");
    }

    private bool ValidateLevels(int[] levels) {
        for (var i = 0; i < levels.Length - 1; i++) {
            if (levels[i] == levels[i + 1] ||
                Math.Abs(levels[i] - levels[i + 1]) > 3 ||
                (levels[0] < levels[1] && levels[i] > levels[i + 1]) ||
                (levels[0] > levels[1] && levels[i] < levels[i + 1])) {
                return true;
            }
        }

        return false;
    }
}