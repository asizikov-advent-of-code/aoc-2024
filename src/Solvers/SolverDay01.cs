namespace aoc_2024.Solvers;

public class SolverDay01 : ISolver {
    [PuzzleInput("01-01")]
    public void Solve(string[] input) {
        var (first, second) = (new List<int>(), new Dictionary<int, int>());
        foreach (var line in input) {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var (a, b) = (int.Parse(parts[0]), int.Parse(parts[1]));
            first.Add(a);
            second.TryAdd(b, 0);
            second[b]++;
        }

        var answer = 0;
        foreach (var a in first) {
            second.TryGetValue(a, out var times);
            answer += a * times;
        }
        Console.WriteLine($"Answer: {answer}");
    }
}