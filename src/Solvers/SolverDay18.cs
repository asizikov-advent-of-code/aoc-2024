using System.Security.Cryptography.X509Certificates;
using aoc_2024.Utils;

namespace aoc_2024.Solvers;

public class SolverDay18 : ISolver {
    [PuzzleInput("18-01")]
    public void Solve(string[] input) {
        var bytes = input.Select(l => l.Split(","))
            .Select(pair => (r: int.Parse(pair[0]), c: int.Parse(pair[1])))
            .ToList();

        (int r, int c) gridSize = new(71, 71);
        
        var grid = new char[gridSize.r][];
        for (var r = 0; r < gridSize.r; r++) {
            grid[r] = new char[gridSize.c];
            for (var c = 0; c < gridSize.c; c++) {
                grid[r][c] = '.';
            }
        }
        
        var nanoseconds = bytes.Count;
        for (var t = 0; t < nanoseconds; t++) {
            var b = bytes[t];
            grid[b.r][b.c] = '#';
            if (Walk((0, 0), (gridSize.r - 1, gridSize.c - 1)) == -1) {
                Console.WriteLine($"{b.r},{b.c}");
                break;
            }
            
        }

        int Walk((int r, int c) start, (int r, int c) end) {
            var visited = new HashSet<(int r, int c)>();
            var queue = new Queue<((int r, int c) p, int steps)>();
            queue.Enqueue((start, 0));

            while (queue.Count != 0) {
                var current = queue.Dequeue();
                if (!visited.Add(current.p)) continue;
                if (current.p == end) {
                    return current.steps;
                }

                foreach ((int dr, int dc) dir in new[] { (0, 1), (0, -1), (1, 0), (-1, 0) }) {
                    (int r, int c) next = (current.p.r + dir.dr, current.p.c + dir.dc);
                    if (grid.IsInBounds(next) == false || grid[next.r][next.c] == '#') continue;
                    queue.Enqueue((next, current.steps + 1));
                }
            }
            
            return -1;
        }
        
    }
}