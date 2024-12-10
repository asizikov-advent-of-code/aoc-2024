using aoc_2024.Utils;

namespace aoc_2024.Solvers;

public class SolverDay10 : ISolver {
    [PuzzleInput("10-01")]
    public void Solve(string[] input) {
        var answer = input.ScanFor('0')
            .Aggregate(0L, (acc, scan) => acc + Walk(scan.r, scan.c));
        
        Console.WriteLine($"Answer: {answer}");

        int Walk(int cr, int cc) {
            var score = 0;
            var queue = new Queue<(int r, int c, char val)>();
            queue.Enqueue((cr, cc, input[cr][cc]));
            while (queue.Count > 0) {
                var (r, c,val) = queue.Dequeue();
                if (!input.IsInBounds(r, c) || input[r][c] != val) continue;
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