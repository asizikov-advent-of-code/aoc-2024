namespace aoc_2024.Solvers;

public class SolverDay01 : ISolver {
    [PuzzleInput("01-01")]
    public void Solve(string[] input) {
        var (first, second) = (new PriorityQueue<int, int>(), new PriorityQueue<int, int>());
        foreach (var line in input) {
            var parts = line.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var (a, b) = (int.Parse(parts[0]), int.Parse(parts[1]));
            first.Enqueue(a, -a);
            second.Enqueue(b, -b);
        }

        var answer = 0;
        while (first.Count > 0) {
            var (a, b) = (first.Dequeue(), second.Dequeue());
            answer += Math.Abs(a - b);
        }

        Console.WriteLine($"Answer: {answer}");
    }
}