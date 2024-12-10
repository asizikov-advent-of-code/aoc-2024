using System.Security;

namespace aoc_2024.Solvers;

public class SolverDay10 : ISolver {
    [PuzzleInput("10-01")]
    public void Solve(string[] input) {
        var answer = 0L;
        for (var r = 0; r < input.Length; r++) {
            for (var c = 0; c < input[r].Length; c++) {
                if (input[r][c] != '0') continue;
                answer += Walk(r, c);
            }
        }

        Console.WriteLine($"Answer: {answer}");

        int Walk(int cr, int cc) {
            var score = 0;
            var queue = new Queue<(int r, int c, int steps, char value)>();
            queue.Enqueue((cr, cc,  0, (char)('0'-1)));
            var visited = new HashSet<(int r, int c)>();
            while (queue.Count > 0) {
                var (r, c, s, val) = queue.Dequeue();
                if (r < 0 || r >= input.Length || c < 0 || c >= input[r].Length || val + 1 != input[r][c]) continue;
                if (!visited.Add((r, c))) continue;
                if (input[r][c] == '9') {
                    score += 1;
                    continue;
                }
                (int dr, int dc)[] dirs = [(0, 1), (0, -1), (1, 0), (-1, 0)];
                foreach (var (dr, dc) in dirs) {
                    queue.Enqueue((r + dr, c + dc, s + 1, input[r][c]));
                }
            }
            return score;
        }
    }
}