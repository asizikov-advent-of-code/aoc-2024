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
            var queue = new Queue<(int r, int c, char val)>();
            queue.Enqueue((cr, cc, input[cr][cc]));
            while (queue.Count > 0) {
                var (r, c,val) = queue.Dequeue();
                if (r < 0 || r >= input.Length || c < 0 || c >= input[r].Length) continue;
                if (input[r][c] != val) continue;
                if (input[r][c] == '9') {
                    score++;
                    continue;
                }
                foreach (var (dr, dc) in new[] {(-1, 0), (1, 0), (0, -1), (0, 1)}) {
                    queue.Enqueue((r + dr, c + dc, (char)(val + 1)));
                }
            }
            return score;
        }
    }
}